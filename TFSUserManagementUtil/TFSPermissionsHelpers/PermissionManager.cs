
////Copyright 2017 Nandu Muralidharan

////Licensed under the Apache License, Version 2.0 (the "License");
////you may not use this file except in compliance with the License.
////You may obtain a copy of the License at

////    http://www.apache.org/licenses/LICENSE-2.0

////Unless required by applicable law or agreed to in writing, software
////distributed under the License is distributed on an "AS IS" BASIS,
////WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
////See the License for the specific language governing permissions and
////limitations under the License.

using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;

namespace TFSPermissionsHelpers
{
    // TODO: This class is designed for use with a thick client application.
    // TODO: Ensure scalability for web model
    public class PermissionManager
    {
        private TfsTeamProjectCollection tpCollection = null;
        private IIdentityManagementService idMgmtSvc = null;
        private IGroupSecurityService groupSecuritySvc = null;
        // TODO: ICommonStructureService3 is being used for backward compatibility with TFS 2010.
        // Replace with ICommonStructureService4, after migration to TFS15
        private ICommonStructureService3 structureSvc = null;
        private const string RES_WindowsIdentity = "System.Security.Principal.WindowsIdentity";
        private const string GROUP_NAME_FORMAT = "[{0}]\\{1}";

        private PermissionManager() { }
        public PermissionManager(Uri tfsCollectionUri)
        {
            FileHelper.CreateLogFile();
            TextWriterTraceListener myTextListener = new TextWriterTraceListener(Constants.LOG_FILE_NAME);
            myTextListener.TraceOutputOptions = TraceOptions.Timestamp;
            Trace.Listeners.Add(myTextListener);

            tpCollection = new TfsTeamProjectCollection(tfsCollectionUri);
            tpCollection.Authenticate();
            
            // TODO: ICommonStructureService3 is being used for backward compatibility with TFS 2010.
            // Replace with ICommonStructureService4, after migration to TFS15
            structureSvc = tpCollection.GetService<ICommonStructureService3>();
            // TODO: IGroupSecurityService is deprecated as of TFS 2012 and needs to be removed, after migration to TFS15
            // Use IIdentityManagementService for TFS15
            groupSecuritySvc = tpCollection.GetService<IGroupSecurityService>();
            // TODO: use for TFS15
            idMgmtSvc = tpCollection.GetService<IIdentityManagementService>();

            FileHelper.Log("Connection established and Identity Management Service instance created.");
        }

        /// <summary>
        /// Adds a domain user to a TFS Security group, at a collection level. If the group does not exist, it will be created.
        /// The method will assume the context of the executing user. 
        /// i.e.: To add a domain user, execute in the context of a domain user.
        /// Usage Scenarios:
        /// 1. Use this method to add a given user to multiple groups
        /// 2. Use this method to add multiple users to multiple groups
        /// </summary>
        /// <param name="user2Groups">A list of user to groups mapping</param>
        /// <returns></returns>
        public ReturnCode AddUsersToGroups(List<User2GroupsMap> user2Groups)
        {
            if (tpCollection == null)
            {
                FileHelper.Log("Collection not initialized.");
                return ReturnCode.Failure;
            }

            int errorCount = 0;
            int iteration = 0;
            try
            {
                if (user2Groups == null || user2Groups.Capacity <= 0)
                {
                    FileHelper.Log("Users 2 Groups mapping is not available.");
                    return ReturnCode.Failure;
                }

                // For each User to Groups mapping
                foreach (User2GroupsMap u2gmap in user2Groups)
                {
                    string userName = u2gmap.UserName;
                    // For each team project listed
                    // Note that the tool assumes that if there are more than one team project specified,
                    // the user wants to add all groups listed to all team projects
                    foreach (string teamProjName in u2gmap.TeamProjectNames)
                    {
                        ProjectInfo objTeamProj = GetProject(teamProjName);
                        iteration++;
                        if (objTeamProj != null)
                        {
                            FileHelper.Log("Team Project: {0} found", objTeamProj.Name);
                            // For each group name specified
                            foreach (string grpName in u2gmap.GroupNames)
                            {
                                iteration++;
                                string searchableGroupName = string.Format(GROUP_NAME_FORMAT, teamProjName, grpName);
#if TFS10
                                bool success =  CheckIfGroupExists_TFS10(objTeamProj.Uri, searchableGroupName, grpName, true);
                                if (success)
                                {
                                    iteration++;
                                    if (!AddMemberToGroup_TFS10(objTeamProj.Name, searchableGroupName, userName))
                                    {
                                        FileHelper.Log("Failed to add user {0} to group {1}", userName, grpName);
                                        errorCount++;
                                    }
                                }
                                else
                                    errorCount++;
#else
                                IdentityDescriptor groupId = CheckIfGroupExists(objTeamProj.Uri, searchableGroupName, true);

                                if (groupId != null)
                                {
                                    if (AddMemberToGroup(groupId, userName))
                                        FileHelper.Log("User {0} added to group {1}", userName, searchableGroupName);
                                    else
                                    {
                                        FileHelper.Log("Failed to add user {0} to group {1}", userName, searchableGroupName);
                                        errorCount++;
                                    }
                                }
                                else
                                    errorCount++;
#endif
                            }
                        }
                        else
                            errorCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                FileHelper.Log(ex.Message);
                return ReturnCode.Failure;
            }
            return GetReturnCode(iteration, errorCount);
        }

        /// <summary>
        /// Checks if a group exists and returns the descriptor. 
        /// If the group does not exist, it is created (if the createIfNew flag is set to true).
        /// </summary>
        /// <param name="scope">Scope Id to limit the search area - default (empty string) is collection level.</param>
        /// <param name="groupName">Name of the group. Also used as group description for create.</param>
        /// <param name="createIfNew">Set to true for create. Default is false.</param>
        /// <returns>The IdentityDescriptor of the group. Returns null in case of any errors.</returns>
        private IdentityDescriptor CheckIfGroupExists(string scope, string groupName, bool createIfNew)
        {
            TeamFoundationIdentity tfi = null;
            try
            {
                // Find the group using Account Name
                tfi = idMgmtSvc.ReadIdentity(IdentitySearchFactor.AccountName, groupName,
                    MembershipQuery.Direct, ReadIdentityOptions.None);

                if (tfi == null && createIfNew)
                {
                    FileHelper.Log("Creating new group..." + groupName);

                    // SCOPE: If a Team Project is found, add the group in that TP.
                    // If not, add the group at collection level.
                    // string scopeId = (teamProjectIdentity == null) ? string.Empty : teamProjectIdentity.Descriptor.Identifier;

                    //string scopeId = tpCollection.Uri.ToString() + "/" + scope;

                    IdentityDescriptor idDesc = idMgmtSvc.CreateApplicationGroup(scope, groupName, groupName);
                    FileHelper.Log("Group creation successful..." + groupName);
                    return idDesc;
                }
                else
                {
                    FileHelper.Log("Group identity found..." + groupName);
                    return tfi.Descriptor;
                }
            }
            catch (Exception ex)
            {
                FileHelper.Log(ex.Message);
                FileHelper.Log(ex.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// NOTE: call for TFS 2010 only.
        /// Checks if a group exists, in the team project. Creates the group, based on createIfNew flag.
        /// </summary>
        /// <param name="projectUri">The team project Uri</param>
        /// <param name="searchableGroupName">The name of the group in [TeamProject]\GroupName format</param>
        /// <param name="groupName">The name of the group</param>
        /// <param name="createIfNew">Flag to create the group, if new</param>
        /// <returns>True, if successful</returns>
        private bool CheckIfGroupExists_TFS10(string projectUri, string searchableGroupName, string groupName, bool createIfNew)
        {
            try
            {
                Identity groupIdentity =
                    groupSecuritySvc.ReadIdentity(SearchFactor.AccountName, searchableGroupName, QueryMembership.Direct);

                if (groupIdentity == null && createIfNew)
                {
                    string result = groupSecuritySvc.CreateApplicationGroup(projectUri, groupName, groupName);
                    FileHelper.Log("Security Group created..." + groupName);
                }
                else
                    FileHelper.Log("Security Group identity found..." + groupName);
            }
            catch (Exception ex)
            {
                FileHelper.Log(ex.Message);
                FileHelper.Log(ex.StackTrace);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the project info object
        /// </summary>
        /// <param name="teamProjectName">The name of the team project.</param>
        /// <returns></returns>
        private ProjectInfo GetProject(string teamProjectName)
        {
            try
            {
                if (structureSvc != null)
                {
                    return structureSvc.GetProjectFromName(teamProjectName);
                }
            }
            catch(Exception ex)
            {
                FileHelper.Log(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Adds the specified user to the TFS security group
        /// </summary>
        /// <param name="groupID">The TFS Security Group identifier</param>
        /// <param name="userName">The User name</param>
        /// <returns>true, if successful.</returns>
        private bool AddMemberToGroup(IdentityDescriptor groupId, string userName)
        {
            try
            {
                    TeamFoundationIdentity tfiUser = 
                        idMgmtSvc.ReadIdentity(IdentitySearchFactor.AccountName, userName, MembershipQuery.Direct, ReadIdentityOptions.IncludeReadFromSource);

                if (idMgmtSvc.IsMember(groupId, tfiUser.Descriptor))
                    FileHelper.Log("User {0} already part of group {1}", userName, groupId.Identifier);
                else
                {
                    idMgmtSvc.AddMemberToApplicationGroup(groupId, tfiUser.Descriptor);
                    FileHelper.Log("User {0} added to group {1}", userName, groupId.Identifier);
                }

                
            }
            catch (Exception ex)
            {
                FileHelper.Log(ex.Message);
                return false;
            }
            return true;   
        }

        /// <summary>
        /// NOTE: call for TFS 2010 only.
        /// Adds a user to a TFS security group
        /// </summary>
        /// <param name="teamProjName">The name of the team project</param>
        /// <param name="searchableGroupName">The name of the group in [TeamProject]\GroupName format</param>
        /// <param name="userName">The name of the user to add</param>
        /// <returns>True, if successful</returns>
        private bool AddMemberToGroup_TFS10(string teamProjName, string searchableGroupName, string userName)
        {
            try
            {
                
                Identity userIdentity =
                    groupSecuritySvc.ReadIdentityFromSource(SearchFactor.AccountName, userName);
                Identity groupIdentity =
                    groupSecuritySvc.ReadIdentity(SearchFactor.AccountName, searchableGroupName, QueryMembership.Direct);

                if (groupSecuritySvc.IsMember(groupIdentity.Sid, userIdentity.Sid))
                    FileHelper.Log("User {0} already part of group {1}", userName, searchableGroupName);
                else
                {
                    groupSecuritySvc.AddMemberToApplicationGroup(groupIdentity.Sid, userIdentity.Sid);
                    FileHelper.Log("User {0} added to group {1}", userName, searchableGroupName);
                }
            }
            catch (Exception ex)
            {
                FileHelper.Log(ex.Message);
                FileHelper.Log(ex.StackTrace);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Removes a user account from TFS Security groups.
        /// </summary>
        /// <param name="usersList">List of users and corresponding projects.</param>
        /// <returns>ReturnCode</returns>
        public ReturnCode RemoveUsers(List<User2GroupsMap> usersList)
        {
            if (tpCollection == null)
            {
                FileHelper.Log("Collection not initialized.");
                return ReturnCode.Failure;
            }

            int errorCount = 0;
            int iteration = 0;
            try
            {
                if (usersList == null || usersList.Capacity <= 0)
                {
                    FileHelper.Log("Users 2 Groups mapping is not available.");
                    return ReturnCode.Failure;
                }

                // For each User to Groups mapping
                foreach (User2GroupsMap u2gmap in usersList)
                {
                    iteration++;
                    // Find the user identity
                    UserPrincipal user = GetUserIdentity(u2gmap.UserName);

                    if (user != null && user.Sid != null)
                    {
                        FileHelper.Log("User found..." + user.Name);
                        
                        // Remove user at Project level
                        if (u2gmap.TeamProjectNames.Count > 0)
                        {
                            foreach(string teamProjectName in u2gmap.TeamProjectNames)
                            {
                                ProjectInfo prjInfo = GetProject(teamProjectName);
                                if (prjInfo != null)
                                {
                                    iteration++;
                                    FileHelper.Log("Removing user from team project " + prjInfo.Name);
                                    TeamFoundationIdentity[] applicationGroups =
                                        idMgmtSvc.ListApplicationGroups(prjInfo.Uri, ReadIdentityOptions.None);

                                    foreach (TeamFoundationIdentity grpIdentity in applicationGroups)
                                    {
                                        iteration++;
                                        bool result = RemoveMemberFromGroup(grpIdentity.Descriptor,
                                            new IdentityDescriptor(RES_WindowsIdentity, user.Sid.Value));
                                        if (!result) errorCount++;
                                    }
                                }
                                else
                                    errorCount++;
                            }
                            FileHelper.Log("Operation completed.");
                        }
                        else
                        {
                            FileHelper.Log("Removing user from all team projects...");
                            // Remove user from all groups, at collection level - 
                            // use "Project Collection Valid Users" as the one source.
                            TeamFoundationIdentity tfiGrp = GetProjCollectionValidUsersGroup();
                            bool result = RemoveUserFromAllGroupsInCollection(tfiGrp,
                                new IdentityDescriptor(RES_WindowsIdentity, user.Sid.Value));
                            if (!result) errorCount++;
                            FileHelper.Log("Operation completed.");
                        }

                    }
                    else
                    {
                        errorCount++;
                        FileHelper.Log("User not found..." + u2gmap.UserName);
                    }
                }

            }
            catch (Exception ex)
            {
                FileHelper.Log(ex.Message);
                FileHelper.Log(ex.StackTrace);
                return ReturnCode.Failure;
            }
            return GetReturnCode(iteration, errorCount);
        }

        /// <summary>
        /// Gets the identity of Project Collection Valid Users group
        /// </summary>
        /// <returns></returns>
        private TeamFoundationIdentity GetProjCollectionValidUsersGroup()
        {
            try
            {
                string[] collectionNameParts = tpCollection.Name.Split(new char[] { '/' });
                int len = collectionNameParts.Length;
                string groupName = string.Format(@"[{0}]\Project Collection Valid Users", collectionNameParts[len - 1]);
                TeamFoundationIdentity tfiGroupId = idMgmtSvc.ReadIdentity(IdentitySearchFactor.DisplayName, groupName,
                                    MembershipQuery.Direct, ReadIdentityOptions.None);
                return tfiGroupId;
            }
            catch (Exception ex) { FileHelper.Log(ex.Message); FileHelper.Log(ex.StackTrace); }
            return null;
        }

        /// <summary>
        /// Remove a particular user from all groups at a collection level
        /// </summary>
        /// <param name="tfiPCValidUsers">The identity of ProjectCollectionValidUsers group</param>
        /// <param name="userIdentity">The identity of the user to be removed</param>
        /// <returns>True, if successful</returns>
        private bool RemoveUserFromAllGroupsInCollection(TeamFoundationIdentity tfiPCValidUsers, IdentityDescriptor userIdentity)
        {
            foreach (IdentityDescriptor groupDesc in tfiPCValidUsers.Members)
            {
                if (idMgmtSvc.IsMember(groupDesc, userIdentity))
                {
                    try
                    {
                        idMgmtSvc.RemoveMemberFromApplicationGroup(groupDesc, userIdentity);
                    }
                    catch (Exception ex)
                    {
                        FileHelper.Log(ex.Message);
                        FileHelper.Log(ex.StackTrace);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        private bool RemoveMemberFromGroup(IdentityDescriptor groupID, IdentityDescriptor userID)
        {
            try
            {
                if (groupID != null && userID != null && idMgmtSvc.IsMember(groupID, userID))
                {
                    idMgmtSvc.RemoveMemberFromApplicationGroup(groupID, userID);
                }
            }
            catch (Exception ex)
            {
                FileHelper.Log(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the UserPrincipal object for the given user name (domain/username).
        /// </summary>
        /// <param name="userName">User name in domain/username format</param>
        /// <returns>UserPrincipal object</returns>
        private UserPrincipal GetUserIdentity(string userName)
        {
            // Get the context of execution
            // i.e.: user context would be domain based, when executed in the context of a domain user 
            // user context would be "local", when executed in the context of a system/local machine user
            // Added special case for amrs - assumption is that not many amrs users would have execution rights
            if (userName.StartsWith("amrs"))
                return UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain, "amrs.win.ml.com"), userName);
            
            return UserPrincipal.FindByIdentity(new PrincipalContext(UserPrincipal.Current.ContextType), userName);
        }

        /// <summary>
        /// Gets the return code, based on iterations and error count
        /// </summary>
        /// <param name="iterations">The count of iterations</param>
        /// <param name="errorCount">The count of errors reported</param>
        /// <returns>ReturnCode</returns>
        private ReturnCode GetReturnCode(int iterations, int errorCount)
        {
            if (errorCount > 0 && errorCount < iterations)
                return ReturnCode.PartialSuccess;
            else if (errorCount >= iterations)
                return ReturnCode.Failure;
            else
                return ReturnCode.Success;
        }
    }
}
