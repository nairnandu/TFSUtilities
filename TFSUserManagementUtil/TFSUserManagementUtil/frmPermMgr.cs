
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using TFSPermissionsHelpers;

namespace TFSUserManagementUtil
{
    public partial class frmPermMgr : Form
    {
        private PermissionManager permissionMgr = null;
        private InputFileType fileType =  InputFileType.CSV;
        public frmPermMgr()
        {
            InitializeComponent();
            InitializeFileTypes();
        }

        private void InitializeFileTypes()
        {
            try
            {
                // Get input file type.
                string strfileType = ConfigurationManager.AppSettings["FileType"];
                if (!string.IsNullOrEmpty(strfileType))
                {
                    // TODO: change implementation to accomodate other formats.
                    fileType = (strfileType.ToUpper().Trim() == "JSON") ? InputFileType.JSON : InputFileType.CSV;
                }

                // Set the filter for open file dialog, based on file type.
                switch (fileType)
                {
                    case InputFileType.CSV:
                        ofdMappingFile.Filter = "csv files (*.csv)|*.csv|All files|*.*";
                        break;
                    case InputFileType.JSON:
                        ofdMappingFile.Filter = "JSon files (*.json)|*.json|All files|*.*";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strUrl = txtCollectionURL.Text.Trim();
            if (string.IsNullOrEmpty(strUrl) || !Uri.IsWellFormedUriString(strUrl, UriKind.Absolute))
            {
                MessageBox.Show("Please specify a valid TFS collection URL.");
                return;
            }

            try
            {
                permissionMgr = new PermissionManager(new System.Uri(strUrl));
                toolStripStatusLabel1.Text = "Connection established. Next, select the file with user mapping by clicking on Load Map File button.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                toolStripStatusLabel1.Text = "Error!";
            }
        }
        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            DialogResult dresult = ofdMappingFile.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                txtMappingFilePath.Text = ofdMappingFile.FileName;
                toolStripStatusLabel1.Text = "User to groups mapping loaded. Next, click the add users to groups button to finish.";
            }
        }

        private void btnAddUsers_Click(object sender, EventArgs e)
        {
            string temp = btnAddUsers.Text;
            btnAddUsers.Text = "Adding users. This might take a minute...";
            btnAddUsers.Enabled = false;
            
            string strUrl = txtCollectionURL.Text.Trim();
            string filePath = txtMappingFilePath.Text.Trim();
            if (string.IsNullOrEmpty(strUrl) || string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please specify TFS collection URL and the User to Groups Mapping file.");
                return;
            }

            try
            {
                List<User2GroupsMap> user2grpMap = 
                    FileHelper.ReadUser2GroupsMapFromFile(filePath, fileType, FileContentType.UsersToGroupsMapping);
                if (permissionMgr != null)
                {
                    ReturnCode returnVal = permissionMgr.AddUsersToGroups(user2grpMap);
                    switch (returnVal)
                    {
                        case ReturnCode.Success:
                            toolStripStatusLabel1.Text = "Successfully added users to the groups!";
                            break;
                        case ReturnCode.PartialSuccess:
                            toolStripStatusLabel1.Text = "Partial success. Some users were added, but some errors were also encountered. Please refer the log file.";
                            break;
                        case ReturnCode.Failure:
                            toolStripStatusLabel1.Text = "Error! There were issues adding users to the groups. Please refer the log file.";
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Could not connect to TFS Server. Please ensure the collection URL is valid.");
                    toolStripStatusLabel1.Text = "Error!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                toolStripStatusLabel1.Text = "Error!";
            }
            btnAddUsers.Text = temp;
            btnAddUsers.Enabled = true;
        }

        private void userGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Constants.USERGUIDE_FILE_NAME))
                    System.Diagnostics.Process.Start(Constants.USERGUIDE_FILE_NAME);
                else
                    toolStripStatusLabel1.Text = "Error! User guide could not be found.";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to open user guide: " + ex.Message);
            }
        }

        private void showErrorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Constants.LOG_FILE_NAME))
                    System.Diagnostics.Process.Start(Constants.LOG_FILE_NAME);
                else
                    toolStripStatusLabel1.Text = "Error! Log file not created or could not be found.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open error log: " + ex.Message);
            }
        }

        private void btnLoadRemoveUsers_Click(object sender, EventArgs e)
        {
            DialogResult dresult = ofdMappingFile.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                txtRemoveUserFile.Text = ofdMappingFile.FileName;
                toolStripStatusLabel1.Text = "User list mapping loaded. Next, click the remove users from groups button to finish.";
            }
        }

        private void btnRemoveUsers_Click(object sender, EventArgs e)
        {
            string temp = btnRemoveUsers.Text;
            btnRemoveUsers.Text = "Removing users. This might take a few minutes...";
            btnRemoveUsers.Enabled = false;

            string strUrl = txtCollectionURL.Text.Trim();
            string filePath = txtRemoveUserFile.Text.Trim();
            if (string.IsNullOrEmpty(strUrl) || string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please specify TFS collection URL and the user list file.");
                return;
            }

            try
            {
                List<User2GroupsMap> user2grpMap = 
                    FileHelper.ReadUser2GroupsMapFromFile(filePath, fileType, FileContentType.UsersList);
                if (permissionMgr != null)
                {
                    ReturnCode returnVal = permissionMgr.RemoveUsers(user2grpMap);
                    switch (returnVal)
                    {
                        case ReturnCode.Success:
                            toolStripStatusLabel1.Text = "Successfully removed users from the groups!";
                            break;
                        case ReturnCode.PartialSuccess:
                            toolStripStatusLabel1.Text = "Partial success. Some users were removed, but some errors were also encountered. Please refer the log file.";
                            break;
                        case ReturnCode.Failure:
                            toolStripStatusLabel1.Text = "Error! There were issues removing users from the groups. Please refer the log file.";
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Could not connect to TFS Server. Please ensure the collection URL is valid.");
                    toolStripStatusLabel1.Text = "Error!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                toolStripStatusLabel1.Text = "Error!";
            }
            btnRemoveUsers.Text = temp;
            btnRemoveUsers.Enabled = true;
        }

    }
}
