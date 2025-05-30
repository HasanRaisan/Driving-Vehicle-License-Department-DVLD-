namespace DVLD.Serveces
{
    partial class FormReplacementForDamagedOrLostLicenses
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
            this.gbReplaecementFor = new System.Windows.Forms.GroupBox();
            this.rbLostLicense = new System.Windows.Forms.RadioButton();
            this.rbDamagedLicense = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCanel = new System.Windows.Forms.Button();
            this.btnReplae = new System.Windows.Forms.Button();
            this.linkLabelShowLicnesInfo = new System.Windows.Forms.LinkLabel();
            this.linkLabelShoLicensesHistory = new System.Windows.Forms.LinkLabel();
            this.userControlShowReplacementApplicationDetails1 = new DVLD.Applcations_user_controls.UserControlShowReplacementApplicationDetails();
            this.userControlDrivingLicenseInfo1 = new DVLD.UserControlDrivingLicenseInfo();
            this.GroupBoxFilter.SuspendLayout();
            this.gbReplaecementFor.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeadLine
            // 
            this.lblHeadLine.AutoSize = true;
            this.lblHeadLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadLine.ForeColor = System.Drawing.Color.Red;
            this.lblHeadLine.Location = new System.Drawing.Point(475, 9);
            this.lblHeadLine.Name = "lblHeadLine";
            this.lblHeadLine.Size = new System.Drawing.Size(382, 40);
            this.lblHeadLine.TabIndex = 8;
            this.lblHeadLine.Text = "Replacement License";
            // 
            // GroupBoxFilter
            // 
            this.GroupBoxFilter.Controls.Add(this.btnSearch);
            this.GroupBoxFilter.Controls.Add(this.txtLicenseID);
            this.GroupBoxFilter.Controls.Add(this.label1);
            this.GroupBoxFilter.Location = new System.Drawing.Point(328, 75);
            this.GroupBoxFilter.Name = "GroupBoxFilter";
            this.GroupBoxFilter.Size = new System.Drawing.Size(852, 104);
            this.GroupBoxFilter.TabIndex = 7;
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
            // gbReplaecementFor
            // 
            this.gbReplaecementFor.Controls.Add(this.rbLostLicense);
            this.gbReplaecementFor.Controls.Add(this.rbDamagedLicense);
            this.gbReplaecementFor.Location = new System.Drawing.Point(1234, 75);
            this.gbReplaecementFor.Name = "gbReplaecementFor";
            this.gbReplaecementFor.Size = new System.Drawing.Size(269, 104);
            this.gbReplaecementFor.TabIndex = 9;
            this.gbReplaecementFor.TabStop = false;
            this.gbReplaecementFor.Text = "Replacement For:";
            // 
            // rbLostLicense
            // 
            this.rbLostLicense.AutoSize = true;
            this.rbLostLicense.Location = new System.Drawing.Point(31, 64);
            this.rbLostLicense.Name = "rbLostLicense";
            this.rbLostLicense.Size = new System.Drawing.Size(124, 24);
            this.rbLostLicense.TabIndex = 11;
            this.rbLostLicense.Text = "Lost License";
            this.rbLostLicense.UseVisualStyleBackColor = true;
            this.rbLostLicense.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbDamagedLicense
            // 
            this.rbDamagedLicense.AutoSize = true;
            this.rbDamagedLicense.Checked = true;
            this.rbDamagedLicense.Location = new System.Drawing.Point(31, 33);
            this.rbDamagedLicense.Name = "rbDamagedLicense";
            this.rbDamagedLicense.Size = new System.Drawing.Size(163, 24);
            this.rbDamagedLicense.TabIndex = 10;
            this.rbDamagedLicense.TabStop = true;
            this.rbDamagedLicense.Text = "Damaged License";
            this.rbDamagedLicense.UseVisualStyleBackColor = true;
            this.rbDamagedLicense.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.close__3_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(644, 1050);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(255, 50);
            this.btnClose.TabIndex = 145;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // btnCanel
            // 
            this.btnCanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanel.Image = global::DVLD.Properties.Resources.close__3_;
            this.btnCanel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCanel.Location = new System.Drawing.Point(1102, 1050);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(189, 50);
            this.btnCanel.TabIndex = 144;
            this.btnCanel.Text = "Cancel";
            this.btnCanel.UseVisualStyleBackColor = false;
            // 
            // btnReplae
            // 
            this.btnReplae.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnReplae.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplae.Image = global::DVLD.Properties.Resources.diskette__1_;
            this.btnReplae.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReplae.Location = new System.Drawing.Point(1314, 1050);
            this.btnReplae.Name = "btnReplae";
            this.btnReplae.Size = new System.Drawing.Size(189, 50);
            this.btnReplae.TabIndex = 143;
            this.btnReplae.Text = "Replace";
            this.btnReplae.UseVisualStyleBackColor = false;
            this.btnReplae.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // linkLabelShowLicnesInfo
            // 
            this.linkLabelShowLicnesInfo.AutoSize = true;
            this.linkLabelShowLicnesInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelShowLicnesInfo.Location = new System.Drawing.Point(341, 1064);
            this.linkLabelShowLicnesInfo.Name = "linkLabelShowLicnesInfo";
            this.linkLabelShowLicnesInfo.Size = new System.Drawing.Size(172, 25);
            this.linkLabelShowLicnesInfo.TabIndex = 142;
            this.linkLabelShowLicnesInfo.TabStop = true;
            this.linkLabelShowLicnesInfo.Text = "Show License Info";
            this.linkLabelShowLicnesInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShowLicnesInfo_LinkClicked);
            // 
            // linkLabelShoLicensesHistory
            // 
            this.linkLabelShoLicensesHistory.AutoSize = true;
            this.linkLabelShoLicensesHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelShoLicensesHistory.Location = new System.Drawing.Point(9, 1064);
            this.linkLabelShoLicensesHistory.Name = "linkLabelShoLicensesHistory";
            this.linkLabelShoLicensesHistory.Size = new System.Drawing.Size(291, 25);
            this.linkLabelShoLicensesHistory.TabIndex = 141;
            this.linkLabelShoLicensesHistory.TabStop = true;
            this.linkLabelShoLicensesHistory.Text = "Show Person\'s Licenses History";
            this.linkLabelShoLicensesHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShoLicensesHistory_LinkClicked);
            // 
            // userControlShowReplacementApplicationDetails1
            // 
            this.userControlShowReplacementApplicationDetails1.Location = new System.Drawing.Point(12, 753);
            this.userControlShowReplacementApplicationDetails1.Name = "userControlShowReplacementApplicationDetails1";
            this.userControlShowReplacementApplicationDetails1.Size = new System.Drawing.Size(1491, 264);
            this.userControlShowReplacementApplicationDetails1.TabIndex = 1;
            // 
            // userControlDrivingLicenseInfo1
            // 
            this.userControlDrivingLicenseInfo1.Location = new System.Drawing.Point(12, 214);
            this.userControlDrivingLicenseInfo1.Name = "userControlDrivingLicenseInfo1";
            this.userControlDrivingLicenseInfo1.Size = new System.Drawing.Size(1491, 533);
            this.userControlDrivingLicenseInfo1.TabIndex = 0;
            // 
            // FormReplacementForDamagedOrLostLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1515, 1131);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.btnReplae);
            this.Controls.Add(this.linkLabelShowLicnesInfo);
            this.Controls.Add(this.linkLabelShoLicensesHistory);
            this.Controls.Add(this.gbReplaecementFor);
            this.Controls.Add(this.lblHeadLine);
            this.Controls.Add(this.GroupBoxFilter);
            this.Controls.Add(this.userControlShowReplacementApplicationDetails1);
            this.Controls.Add(this.userControlDrivingLicenseInfo1);
            this.Name = "FormReplacementForDamagedOrLostLicenses";
            this.Text = "FormReplacementForDamagedOrLostLicenses";
            this.Load += new System.EventHandler(this.FormReplacementForDamagedOrLostLicenses_Load);
            this.GroupBoxFilter.ResumeLayout(false);
            this.GroupBoxFilter.PerformLayout();
            this.gbReplaecementFor.ResumeLayout(false);
            this.gbReplaecementFor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControlDrivingLicenseInfo userControlDrivingLicenseInfo1;
        private Applcations_user_controls.UserControlShowReplacementApplicationDetails userControlShowReplacementApplicationDetails1;
        private System.Windows.Forms.Label lblHeadLine;
        private System.Windows.Forms.GroupBox GroupBoxFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtLicenseID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbReplaecementFor;
        private System.Windows.Forms.RadioButton rbLostLicense;
        private System.Windows.Forms.RadioButton rbDamagedLicense;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.Button btnReplae;
        private System.Windows.Forms.LinkLabel linkLabelShowLicnesInfo;
        private System.Windows.Forms.LinkLabel linkLabelShoLicensesHistory;
    }
}