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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TFSPermissionsHelpers
{
    public class FileHelper
    {
        private static char[] User2GroupsAndProjectsSplitter = new char[] { ',' };
        private static char[] GroupsAndProjectsSplitter = new char[] { '|' };
        private FileHelper() { }

        /// <summary>
        /// Parses the user-to-groups json file
        /// </summary>
        /// <param name="path">Path to the JSon file.</param>
        /// <returns>A list of User2GroupsMap objects. Returns null in case of errors.</returns>
        public static List<User2GroupsMap> ReadUser2GroupsMapFromFile(string path, InputFileType fileType, FileContentType fileContentType)
        {
            try
            {
                if (!File.Exists(path)) return null;

                if (fileType == InputFileType.JSON)
                {
                    return JsonConvert.DeserializeObject<List<User2GroupsMap>>(File.ReadAllText(path));
                }
                else if (fileType == InputFileType.CSV)
                {
                    string[] strAllText = File.ReadAllLines(path);
                    List<User2GroupsMap> user2GroupsMap = new List<User2GroupsMap>();

                    foreach (string strLine in strAllText)
                    {
                        if (!strLine.StartsWith("#") && !string.IsNullOrEmpty(strLine.Trim()))
                        {
                            if (fileContentType == FileContentType.UsersToGroupsMapping)
                            {
                                string[] arr = strLine.Split(User2GroupsAndProjectsSplitter, StringSplitOptions.RemoveEmptyEntries);
                                if (arr.Length < 3)
                                {
                                    FileHelper.Log("Invalid entry in mapping file ->" + strLine);
                                }
                                else
                                {
                                    User2GroupsMap userMap = new User2GroupsMap(arr[0], new List<string>(arr[1].Split(GroupsAndProjectsSplitter)),
                                        new List<string>(arr[2].Split(GroupsAndProjectsSplitter)));
                                    user2GroupsMap.Add(userMap);
                                }
                            }
                            else if (fileContentType == FileContentType.UsersList)
                            {
                                string[] arr = strLine.Split(User2GroupsAndProjectsSplitter, StringSplitOptions.RemoveEmptyEntries);
                                User2GroupsMap user = null;
                                if (arr.Length > 1)
                                {
                                    string[] teamProjectNames = arr[1].Split(GroupsAndProjectsSplitter);
                                    user = new User2GroupsMap(arr[0], new List<string>(), 
                                        new List<string>(teamProjectNames));
                                }
                                else
                                {
                                    user = new User2GroupsMap(arr[0], new List<string>(), new List<string>());
                                }

                                if (user != null)
                                    user2GroupsMap.Add(user);
                            }
                        }
                    }
                    return user2GroupsMap;
                }
            }
            catch(Exception ex)
            {
                FileHelper.Log(ex.Message);
            }
            return null;
        }

        internal static bool CreateLogFile()
        {
            try
            {
                Debug.AutoFlush = true;
                using (FileStream fs = File.Create(Constants.LOG_FILE_NAME))
                {
                    fs.Close();
                }
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public static void Log(string logText, params string[] args)
        {
            Trace.WriteLine(string.Format("{0}: {1}", DateTime.Now.ToString(), string.Format(logText, args)));
        }
    }

}
