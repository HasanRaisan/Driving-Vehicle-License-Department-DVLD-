namespace DVLD.International_Applications
{
    partial class FormAddNewInternationlalLicense
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
            this.GroupBoxFilter = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtLicenseID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.userControlDrivingLicenseInfo1 = new DVLD.UserControlDrivingLicenseInfo();
            this.userControlShowInternationalLincenseApplicationDetails1 = new DVLD.UserControlShowInternationalLincenseApplicationDetails();
            this.lblHeadLine = new System.Windows.Forms.Label();
            this.linkLabelShoLicensesHistory = new System.Windows.Forms.LinkLabel();
            this.linkLabelShowLicnesInfo = new System.Windows.Forms.LinkLabel();
            this.btnCanel = new System.Windows.Forms.Button();
            this.btnIssue = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.GroupBoxFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxFilter
            // 
            this.GroupBoxFilter.Controls.Add(this.btnSearch);
            this.GroupBoxFilter.Controls.Add(this.txtLicenseID);
            this.GroupBoxFilter.Controls.Add(this.label1);
            this.GroupBoxFilter.Location = new System.Drawing.Point(301, 164);
            this.GroupBoxFilter.Name = "GroupBoxFilter";
            this.GroupBoxFilter.Size = new System.Drawing.Size(852, 104);
            this.GroupBoxFilter.TabIndex = 2;
            this.GroupBoxFilter.TabStop = false;
            this.GroupBoxFilter.Text = "Filter By License ID";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearch.BackgroundImage = global::DVLD.Properties.Resources.eye;
            this.btnSearch.Location = new System.Drawing.Point(678, 33);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(68, 48);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtLicenseID
            // 
            this.txtLicenseID.BackColor = System.Drawing.SystemColors.Menu;
            this.txtLicenseID.Location = new System.Drawing.Point(346, 44);
            this.txtLicenseID.Multiline = true;
            this.txtLicenseID.Name = "txtLicenseID";
            this.txtLicenseID.Size = new System.Drawing.Size(200, 29);
            this.txtLicenseID.TabIndex = 1;
            this.txtLicenseID.TextChanged += new System.EventHandler(this.txtLicenseID_TextChanged);
            this.txtLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseID_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(178, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "License ID:";
            // 
            // userControlDrivingLicenseInfo1
            // 
            this.userControlDrivingLicenseInfo1.Location = new System.Drawing.Point(12, 274);
            this.userControlDrivingLicenseInfo1.Name = "userControlDrivingLicenseInfo1";
            this.userControlDrivingLicenseInfo1.Size = new System.Drawing.Size(1491, 533);
            this.userControlDrivingLicenseInfo1.TabIndex = 1;
            // 
            // userControlShowInternationalLincenseApplicationDetails1
            // 
            this.userControlShowInternationalLincenseApplicationDetails1.Location = new System.Drawing.Point(12, 813);
            this.userControlShowInternationalLincenseApplicationDetails1.Name = "userControlShowInternationalLincenseApplicationDetails1";
            this.userControlShowInternationalLincenseApplicationDetails1.Size = new System.Drawing.Size(1496, 384);
            this.userControlShowInternationalLincenseApplicationDetails1.TabIndex = 0;
            // 
            // lblHeadLine
            // 
            this.lblHeadLine.AutoSize = true;
            this.lblHeadLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadLine.ForeColor = System.Drawing.Color.Red;
            this.lblHeadLine.Location = new System.Drawing.Point(477, 70);
            this.lblHeadLine.Name = "lblHeadLine";
            this.lblHeadLine.Size = new System.Drawing.Size(543, 40);
            this.lblHeadLine.TabIndex = 3;
            this.lblHeadLine.Text = "New Internationl Driving Licens";
            // 
            // linkLabelShoLicensesHistory
            // 
            this.linkLabelShoLicensesHistory.AutoSize = true;
            this.linkLabelShoLicensesHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelShoLicensesHistory.Location = new System.Drawing.Point(12, 1217);
            this.linkLabelShoLicensesHistory.Name = "linkLabelShoLicensesHistory";
            this.linkLabelShoLicensesHistory.Size = new System.Drawing.Size(291, 25);
            this.linkLabelShoLicensesHistory.TabIndex = 4;
            this.linkLabelShoLicensesHistory.TabStop = true;
            this.linkLabelShoLicensesHistory.Text = "Show Person\'s Licenses History";
            this.linkLabelShoLicensesHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShoLicensesHistory_LinkClicked);
            // 
            // linkLabelShowLicnesInfo
            // 
            this.linkLabelShowLicnesInfo.AutoSize = true;
            this.linkLabelShowLicnesInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelShowLicnesInfo.Location = new System.Drawing.Point(324, 1217);
            this.linkLabelShowLicnesInfo.Name = "linkLabelShowLicnesInfo";
            this.linkLabelShowLicnesInfo.Size = new System.Drawing.Size(172, 25);
            this.linkLabelShowLicnesInfo.TabIndex = 5;
            this.linkLabelShowLicnesInfo.TabStop = true;
            this.linkLabelShowLicnesInfo.Text = "Show License Info";
            this.linkLabelShowLicnesInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShowLicnesInfo_LinkClicked);
            // 
            // btnCanel
            // 
            this.btnCanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanel.Image = global::DVLD.Properties.Resources.close__3_;
            this.btnCanel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCanel.Location = new System.Drawing.Point(1195, 1203);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(155, 50);
            this.btnCanel.TabIndex = 134;
            this.btnCanel.Text = "Cancel";
            this.btnCanel.UseVisualStyleBackColor = false;
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // btnIssue
            // 
            this.btnIssue.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssue.Image = global::DVLD.Properties.Resources.diskette__1_;
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssue.Location = new System.Drawing.Point(1356, 1203);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(155, 50);
            this.btnIssue.TabIndex = 133;
            this.btnIssue.Text = "Issue";
            this.btnIssue.UseVisualStyleBackColor = false;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.close__3_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(638, 1203);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(255, 50);
            this.btnClose.TabIndex = 135;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // FormAddNewInternationlalLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1523, 1266);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.linkLabelShowLicnesInfo);
            this.Controls.Add(this.linkLabelShoLicensesHistory);
            this.Controls.Add(this.lblHeadLine);
            this.Controls.Add(this.GroupBoxFilter);
            this.Controls.Add(this.userControlDrivingLicenseInfo1);
            this.Controls.Add(this.userControlShowInternationalLincenseApplicationDetails1);
            this.Name = "FormAddNewInternationlalLicense";
            this.Text = "FormAddNewInternationlalLicense";
            this.Load += new System.EventHandler(this.FormAddNewInternationlalLicense_Load);
            this.GroupBoxFilter.ResumeLayout(false);
            this.GroupBoxFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControlShowInternationalLincenseApplicationDetails userControlShowInternationalLincenseApplicationDetails1;
        private UserControlDrivingLicenseInfo userControlDrivingLicenseInfo1;
        private System.Windows.Forms.GroupBox GroupBoxFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtLicenseID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHeadLine;
        private System.Windows.Forms.LinkLabel linkLabelShoLicensesHistory;
        private System.Windows.Forms.LinkLabel linkLabelShowLicnesInfo;
        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button btnClose;
    }
}