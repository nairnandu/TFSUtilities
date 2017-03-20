namespace TFSUserManagementUtil
{
    partial class frmPermMgr
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofdMappingFile = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showErrorLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAddUsers = new System.Windows.Forms.TabPage();
            this.btnAddUsers = new System.Windows.Forms.Button();
            this.btnLoadMap = new System.Windows.Forms.Button();
            this.txtMappingFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabDeleteUsers = new System.Windows.Forms.TabPage();
            this.btnRemoveUsers = new System.Windows.Forms.Button();
            this.btnLoadRemoveUsers = new System.Windows.Forms.Button();
            this.txtRemoveUserFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtCollectionURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabAddUsers.SuspendLayout();
            this.tabDeleteUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 234);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(930, 37);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(625, 32);
            this.toolStripStatusLabel1.Text = "Please enter the TFS Collection URL and click on Connect";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(930, 47);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuideToolStripMenuItem,
            this.showErrorLogToolStripMenuItem});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(89, 43);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Size = new System.Drawing.Size(315, 44);
            this.userGuideToolStripMenuItem.Text = "&User Guide";
            this.userGuideToolStripMenuItem.Click += new System.EventHandler(this.userGuideToolStripMenuItem_Click);
            // 
            // showErrorLogToolStripMenuItem
            // 
            this.showErrorLogToolStripMenuItem.Name = "showErrorLogToolStripMenuItem";
            this.showErrorLogToolStripMenuItem.Size = new System.Drawing.Size(315, 44);
            this.showErrorLogToolStripMenuItem.Text = "Show &Error Log";
            this.showErrorLogToolStripMenuItem.Click += new System.EventHandler(this.showErrorLogToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAddUsers);
            this.tabControl1.Controls.Add(this.tabDeleteUsers);
            this.tabControl1.Location = new System.Drawing.Point(12, 75);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(906, 158);
            this.tabControl1.TabIndex = 8;
            // 
            // tabAddUsers
            // 
            this.tabAddUsers.Controls.Add(this.btnAddUsers);
            this.tabAddUsers.Controls.Add(this.btnLoadMap);
            this.tabAddUsers.Controls.Add(this.txtMappingFilePath);
            this.tabAddUsers.Controls.Add(this.label2);
            this.tabAddUsers.Location = new System.Drawing.Point(8, 53);
            this.tabAddUsers.Name = "tabAddUsers";
            this.tabAddUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddUsers.Size = new System.Drawing.Size(890, 97);
            this.tabAddUsers.TabIndex = 0;
            this.tabAddUsers.Text = "Add Users to Groups";
            this.tabAddUsers.UseVisualStyleBackColor = true;
            // 
            // btnAddUsers
            // 
            this.btnAddUsers.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddUsers.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUsers.Location = new System.Drawing.Point(268, 59);
            this.btnAddUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddUsers.Name = "btnAddUsers";
            this.btnAddUsers.Size = new System.Drawing.Size(306, 48);
            this.btnAddUsers.TabIndex = 12;
            this.btnAddUsers.Text = "3. Add Users to Groups";
            this.btnAddUsers.UseVisualStyleBackColor = true;
            this.btnAddUsers.Click += new System.EventHandler(this.btnAddUsers_Click);
            // 
            // btnLoadMap
            // 
            this.btnLoadMap.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLoadMap.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadMap.Location = new System.Drawing.Point(685, 20);
            this.btnLoadMap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLoadMap.Name = "btnLoadMap";
            this.btnLoadMap.Size = new System.Drawing.Size(194, 32);
            this.btnLoadMap.TabIndex = 11;
            this.btnLoadMap.Text = "2. Load Map File";
            this.btnLoadMap.UseVisualStyleBackColor = true;
            this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
            // 
            // txtMappingFilePath
            // 
            this.txtMappingFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMappingFilePath.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMappingFilePath.Location = new System.Drawing.Point(110, 24);
            this.txtMappingFilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMappingFilePath.Name = "txtMappingFilePath";
            this.txtMappingFilePath.ReadOnly = true;
            this.txtMappingFilePath.Size = new System.Drawing.Size(569, 47);
            this.txtMappingFilePath.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 36);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mapping File:";
            // 
            // tabDeleteUsers
            // 
            this.tabDeleteUsers.Controls.Add(this.btnRemoveUsers);
            this.tabDeleteUsers.Controls.Add(this.btnLoadRemoveUsers);
            this.tabDeleteUsers.Controls.Add(this.txtRemoveUserFile);
            this.tabDeleteUsers.Controls.Add(this.label3);
            this.tabDeleteUsers.Location = new System.Drawing.Point(8, 39);
            this.tabDeleteUsers.Name = "tabDeleteUsers";
            this.tabDeleteUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabDeleteUsers.Size = new System.Drawing.Size(890, 111);
            this.tabDeleteUsers.TabIndex = 1;
            this.tabDeleteUsers.Text = "Remove Users from Groups";
            this.tabDeleteUsers.UseVisualStyleBackColor = true;
            // 
            // btnRemoveUsers
            // 
            this.btnRemoveUsers.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRemoveUsers.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveUsers.Location = new System.Drawing.Point(268, 59);
            this.btnRemoveUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveUsers.Name = "btnRemoveUsers";
            this.btnRemoveUsers.Size = new System.Drawing.Size(306, 48);
            this.btnRemoveUsers.TabIndex = 15;
            this.btnRemoveUsers.Text = "3. Remove Users from Groups";
            this.btnRemoveUsers.UseVisualStyleBackColor = true;
            this.btnRemoveUsers.Click += new System.EventHandler(this.btnRemoveUsers_Click);
            // 
            // btnLoadRemoveUsers
            // 
            this.btnLoadRemoveUsers.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLoadRemoveUsers.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadRemoveUsers.Location = new System.Drawing.Point(685, 20);
            this.btnLoadRemoveUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLoadRemoveUsers.Name = "btnLoadRemoveUsers";
            this.btnLoadRemoveUsers.Size = new System.Drawing.Size(194, 32);
            this.btnLoadRemoveUsers.TabIndex = 14;
            this.btnLoadRemoveUsers.Text = "2. Load User List";
            this.btnLoadRemoveUsers.UseVisualStyleBackColor = true;
            this.btnLoadRemoveUsers.Click += new System.EventHandler(this.btnLoadRemoveUsers_Click);
            // 
            // txtRemoveUserFile
            // 
            this.txtRemoveUserFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemoveUserFile.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemoveUserFile.Location = new System.Drawing.Point(110, 24);
            this.txtRemoveUserFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemoveUserFile.Name = "txtRemoveUserFile";
            this.txtRemoveUserFile.ReadOnly = true;
            this.txtRemoveUserFile.Size = new System.Drawing.Size(569, 47);
            this.txtRemoveUserFile.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 36);
            this.label3.TabIndex = 12;
            this.label3.Text = "Mapping File:";
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnConnect.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(700, 36);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(194, 32);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.Text = "1. Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtCollectionURL
            // 
            this.txtCollectionURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCollectionURL.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCollectionURL.Location = new System.Drawing.Point(126, 40);
            this.txtCollectionURL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCollectionURL.Name = "txtCollectionURL";
            this.txtCollectionURL.Size = new System.Drawing.Size(569, 47);
            this.txtCollectionURL.TabIndex = 11;
            this.txtCollectionURL.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 36);
            this.label1.TabIndex = 10;
            this.label1.Text = "Collection URL:";
            // 
            // frmPermMgr
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(930, 271);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtCollectionURL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmPermMgr";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TFS User Management Utility";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabAddUsers.ResumeLayout(false);
            this.tabAddUsers.PerformLayout();
            this.tabDeleteUsers.ResumeLayout(false);
            this.tabDeleteUsers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdMappingFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showErrorLogToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAddUsers;
        private System.Windows.Forms.Button btnAddUsers;
        private System.Windows.Forms.Button btnLoadMap;
        private System.Windows.Forms.TextBox txtMappingFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabDeleteUsers;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtCollectionURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemoveUsers;
        private System.Windows.Forms.Button btnLoadRemoveUsers;
        private System.Windows.Forms.TextBox txtRemoveUserFile;
        private System.Windows.Forms.Label label3;
    }
}

