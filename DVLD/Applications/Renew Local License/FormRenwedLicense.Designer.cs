namespace DVLD.Serveces
{
    partial class FormRenwedLicense
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
            this.lblHeadLine = new System.Windows.Forms.Label();
            this.GroupBoxFilter = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtLicenseID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCanel = new System.Windows.Forms.Button();
            this.btnRenew = new System.Windows.Forms.Button();
            this.linkLabelShowLicnesInfo = new System.Windows.Forms.LinkLabel();
            this.linkLabelShoLicensesHistory = new System.Windows.Forms.LinkLabel();
            this.userControlShowRenwedApplicationsInfo1 = new DVLD.Applcations_user_controls.UserControlShowRenwedApplicationsInfo();
            this.userControlDrivingLicenseInfo1 = new DVLD.UserControlDrivingLicenseInfo();
            this.GroupBoxFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeadLine
            // 
            this.lblHeadLine.AutoSize = true;
            this.lblHeadLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadLine.ForeColor = System.Drawing.Color.Red;
            this.lblHeadLine.Location = new System.Drawing.Point(508, 37);
            this.lblHeadLine.Name = "lblHeadLine";
            this.lblHeadLine.Size = new System.Drawing.Size(406, 40);
            this.lblHeadLine.TabIndex = 6;
            this.lblHeadLine.Text = "Renew Driving License";
            // 
            // GroupBoxFilter
            // 
            this.GroupBoxFilter.Controls.Add(this.btnSearch);
            this.GroupBoxFilter.Controls.Add(this.txtLicenseID);
            this.GroupBoxFilter.Controls.Add(this.label1);
            this.GroupBoxFilter.Location = new System.Drawing.Point(301, 103);
            this.GroupBoxFilter.Name = "GroupBoxFilter";
            this.GroupBoxFilter.Size = new System.Drawing.Size(852, 104);
            this.GroupBoxFilter.TabIndex = 5;
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
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.close__3_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(647, 1348);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(255, 50);
            this.btnClose.TabIndex = 140;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // btnCanel
            // 
            this.btnCanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanel.Image = global::DVLD.Properties.Resources.close__3_;
            this.btnCanel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCanel.Location = new System.Drawing.Point(1190, 1348);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(155, 50);
            this.btnCanel.TabIndex = 139;
            this.btnCanel.Text = "Cancel";
            this.btnCanel.UseVisualStyleBackColor = false;
            // 
            // btnRenew
            // 
            this.btnRenew.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnRenew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRenew.Image = global::DVLD.Properties.Resources.diskette__1_;
            this.btnRenew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRenew.Location = new System.Drawing.Point(1351, 1348);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(155, 50);
            this.btnRenew.TabIndex = 138;
            this.btnRenew.Text = "Renew";
            this.btnRenew.UseVisualStyleBackColor = false;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // linkLabelShowLicnesInfo
            // 
            this.linkLabelShowLicnesInfo.AutoSize = true;
            this.linkLabelShowLicnesInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelShowLicnesInfo.Location = new System.Drawing.Point(344, 1362);
            this.linkLabelShowLicnesInfo.Name = "linkLabelShowLicnesInfo";
            this.linkLabelShowLicnesInfo.Size = new System.Drawing.Size(172, 25);
            this.linkLabelShowLicnesInfo.TabIndex = 137;
            this.linkLabelShowLicnesInfo.TabStop = true;
            this.linkLabelShowLicnesInfo.Text = "Show License Info";
            this.linkLabelShowLicnesInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShowLicnesInfo_LinkClicked);
            // 
            // linkLabelShoLicensesHistory
            // 
            this.linkLabelShoLicensesHistory.AutoSize = true;
            this.linkLabelShoLicensesHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelShoLicensesHistory.Location = new System.Drawing.Point(12, 1362);
            this.linkLabelShoLicensesHistory.Name = "linkLabelShoLicensesHistory";
            this.linkLabelShoLicensesHistory.Size = new System.Drawing.Size(291, 25);
            this.linkLabelShoLicensesHistory.TabIndex = 136;
            this.linkLabelShoLicensesHistory.TabStop = true;
            this.linkLabelShoLicensesHistory.Text = "Show Person\'s Licenses History";
            this.linkLabelShoLicensesHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShoLicensesHistory_LinkClicked);
            // 
            // userControlShowRenwedApplicationsInfo1
            // 
            this.userControlShowRenwedApplicationsInfo1.Location = new System.Drawing.Point(12, 752);
            this.userControlShowRenwedApplicationsInfo1.Name = "userControlShowRenwedApplicationsInfo1";
            this.userControlShowRenwedApplicationsInfo1.Size = new System.Drawing.Size(1505, 582);
            this.userControlShowRenwedApplicationsInfo1.TabIndex = 7;
            // 
            // userControlDrivingLicenseInfo1
            // 
            this.userControlDrivingLicenseInfo1.Location = new System.Drawing.Point(12, 213);
            this.userControlDrivingLicenseInfo1.Name = "userControlDrivingLicenseInfo1";
            this.userControlDrivingLicenseInfo1.Size = new System.Drawing.Size(1491, 533);
            this.userControlDrivingLicenseInfo1.TabIndex = 4;
            // 
            // FormRenwedLicense
            // 
            this.AcceptButton = this.btnRenew;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1518, 1410);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.btnRenew);
            this.Controls.Add(this.linkLabelShowLicnesInfo);
            this.Controls.Add(this.linkLabelShoLicensesHistory);
            this.Controls.Add(this.userControlShowRenwedApplicationsInfo1);
            this.Controls.Add(this.lblHeadLine);
            this.Controls.Add(this.GroupBoxFilter);
            this.Controls.Add(this.userControlDrivingLicenseInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormRenwedLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormRenwedLicense";
            this.Load += new System.EventHandler(this.FormRenwedLicense_Load);
            this.GroupBoxFilter.ResumeLayout(false);
            this.GroupBoxFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeadLine;
        private System.Windows.Forms.GroupBox GroupBoxFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtLicenseID;
        private System.Windows.Forms.Label label1;
        private UserControlDrivingLicenseInfo userControlDrivingLicenseInfo1;
        private Applcations_user_controls.UserControlShowRenwedApplicationsInfo userControlShowRenwedApplicationsInfo1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.Button btnRenew;
        private System.Windows.Forms.LinkLabel linkLabelShowLicnesInfo;
        private System.Windows.Forms.LinkLabel linkLabelShoLicensesHistory;
    }
}