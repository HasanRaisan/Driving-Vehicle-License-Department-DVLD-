namespace DVLD.International_Applications
{
    partial class FormInternationalLicenseApplications
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
            this.components = new System.ComponentModel.Container();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.txbFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.lbHeadLine = new System.Windows.Forms.Label();
            this.dgvInternationalApplications = new System.Windows.Forms.DataGridView();
            this.AddPerson = new System.Windows.Forms.Button();
            this.cbFliterByActvation = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalApplications)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.ForeColor = System.Drawing.Color.Blue;
            this.btnClose.Location = new System.Drawing.Point(1234, 707);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 38);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRecords
            // 
            this.lblRecords.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblRecords.Location = new System.Drawing.Point(30, 707);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(138, 26);
            this.lblRecords.TabIndex = 25;
            this.lblRecords.Text = "Records ???";
            // 
            // txbFilter
            // 
            this.txbFilter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txbFilter.Location = new System.Drawing.Point(379, 253);
            this.txbFilter.Name = "txbFilter";
            this.txbFilter.Size = new System.Drawing.Size(261, 26);
            this.txbFilter.TabIndex = 24;
            this.txbFilter.TextChanged += new System.EventHandler(this.txbFilter_TextChanged);
            this.txbFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKeyPressOnlyNumbers);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(144, 253);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(177, 28);
            this.cbFilterBy.TabIndex = 23;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::DVLD.Properties.Resources.couple;
            this.pictureBox1.Location = new System.Drawing.Point(611, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 127);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblFilterBy.Location = new System.Drawing.Point(25, 255);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(113, 26);
            this.lblFilterBy.TabIndex = 21;
            this.lblFilterBy.Text = "Filter By:";
            // 
            // lbHeadLine
            // 
            this.lbHeadLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbHeadLine.AutoSize = true;
            this.lbHeadLine.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeadLine.ForeColor = System.Drawing.Color.Red;
            this.lbHeadLine.Location = new System.Drawing.Point(357, 158);
            this.lbHeadLine.Name = "lbHeadLine";
            this.lbHeadLine.Size = new System.Drawing.Size(668, 41);
            this.lbHeadLine.TabIndex = 20;
            this.lbHeadLine.Text = "International Driving License Applications ";
            // 
            // dgvDetains
            // 
            this.dgvInternationalApplications.AllowUserToAddRows = false;
            this.dgvInternationalApplications.AllowUserToDeleteRows = false;
            this.dgvInternationalApplications.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvInternationalApplications.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInternationalApplications.BackgroundColor = System.Drawing.Color.White;
            this.dgvInternationalApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalApplications.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvInternationalApplications.Location = new System.Drawing.Point(30, 299);
            this.dgvInternationalApplications.Name = "dgvDetains";
            this.dgvInternationalApplications.ReadOnly = true;
            this.dgvInternationalApplications.RowHeadersWidth = 62;
            this.dgvInternationalApplications.RowTemplate.Height = 28;
            this.dgvInternationalApplications.Size = new System.Drawing.Size(1311, 388);
            this.dgvInternationalApplications.TabIndex = 19;
            // 
            // AddPerson
            // 
            this.AddPerson.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AddPerson.BackColor = System.Drawing.Color.White;
            this.AddPerson.ForeColor = System.Drawing.Color.Blue;
            this.AddPerson.Image = global::DVLD.Properties.Resources.administrator__7_;
            this.AddPerson.Location = new System.Drawing.Point(1234, 218);
            this.AddPerson.Name = "AddPerson";
            this.AddPerson.Size = new System.Drawing.Size(107, 63);
            this.AddPerson.TabIndex = 27;
            this.AddPerson.UseVisualStyleBackColor = false;
            this.AddPerson.Click += new System.EventHandler(this.AddPerson_Click);
            // 
            // cbFliterByActvation
            // 
            this.cbFliterByActvation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbFliterByActvation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFliterByActvation.FormattingEnabled = true;
            this.cbFliterByActvation.Location = new System.Drawing.Point(379, 251);
            this.cbFliterByActvation.Name = "cbFliterByActvation";
            this.cbFliterByActvation.Size = new System.Drawing.Size(154, 28);
            this.cbFliterByActvation.TabIndex = 28;
            this.cbFliterByActvation.SelectedIndexChanged += new System.EventHandler(this.cbFliterByStatus_SelectedValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonDetailsToolStripMenuItem,
            this.showLicenseDetailsToolStripMenuItem,
            this.showPersonLicenseHistoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(310, 134);
            // 
            // showPersonDetailsToolStripMenuItem
            // 
            this.showPersonDetailsToolStripMenuItem.Name = "showPersonDetailsToolStripMenuItem";
            this.showPersonDetailsToolStripMenuItem.Size = new System.Drawing.Size(309, 32);
            this.showPersonDetailsToolStripMenuItem.Text = "Show Person Details";
            this.showPersonDetailsToolStripMenuItem.Click += new System.EventHandler(this.showPersonDetailsToolStripMenuItem_Click);
            // 
            // showLicenseDetailsToolStripMenuItem
            // 
            this.showLicenseDetailsToolStripMenuItem.Name = "showLicenseDetailsToolStripMenuItem";
            this.showLicenseDetailsToolStripMenuItem.Size = new System.Drawing.Size(309, 32);
            this.showLicenseDetailsToolStripMenuItem.Text = "Show License Details";
            this.showLicenseDetailsToolStripMenuItem.Click += new System.EventHandler(this.showLicenseDetailsToolStripMenuItem_Click);
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(309, 32);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // FormInternationalLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 761);
            this.Controls.Add(this.cbFliterByActvation);
            this.Controls.Add(this.AddPerson);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.txbFilter);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblFilterBy);
            this.Controls.Add(this.lbHeadLine);
            this.Controls.Add(this.dgvInternationalApplications);
            this.Name = "FormInternationalLicenseApplications";
            this.Text = "FormInternationalLicenseApplications";
            this.Load += new System.EventHandler(this.FormInternationalLicenseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalApplications)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.TextBox txbFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.Label lbHeadLine;
        private System.Windows.Forms.DataGridView dgvInternationalApplications;
        private System.Windows.Forms.Button AddPerson;
        private System.Windows.Forms.ComboBox cbFliterByActvation;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPersonDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
    }
}