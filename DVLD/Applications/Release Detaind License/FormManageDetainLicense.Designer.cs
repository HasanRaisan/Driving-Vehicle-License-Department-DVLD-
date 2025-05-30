namespace DVLD.Serveces
{
    partial class FormManageDetainLicense
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
            this.btnDetainLicense = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.txbFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.lbHeadLine = new System.Windows.Forms.Label();
            this.dgvDetains = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseDetaiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.releasDetainedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRleaseLicense = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetains)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDetainLicense
            // 
            this.btnDetainLicense.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDetainLicense.BackColor = System.Drawing.Color.White;
            this.btnDetainLicense.ForeColor = System.Drawing.Color.Blue;
            this.btnDetainLicense.Image = global::DVLD.Properties.Resources.administrator__7_;
            this.btnDetainLicense.Location = new System.Drawing.Point(1265, 231);
            this.btnDetainLicense.Name = "btnDetainLicense";
            this.btnDetainLicense.Size = new System.Drawing.Size(107, 63);
            this.btnDetainLicense.TabIndex = 37;
            this.btnDetainLicense.UseVisualStyleBackColor = false;
            this.btnDetainLicense.Click += new System.EventHandler(this.btnDetainLicense_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.ForeColor = System.Drawing.Color.Blue;
            this.btnClose.Location = new System.Drawing.Point(1265, 720);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 38);
            this.btnClose.TabIndex = 36;
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
            this.lblRecords.Location = new System.Drawing.Point(61, 720);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(138, 26);
            this.lblRecords.TabIndex = 35;
            this.lblRecords.Text = "Records ???";
            // 
            // txbFilter
            // 
            this.txbFilter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txbFilter.Location = new System.Drawing.Point(410, 266);
            this.txbFilter.Name = "txbFilter";
            this.txbFilter.Size = new System.Drawing.Size(261, 26);
            this.txbFilter.TabIndex = 34;
            this.txbFilter.TextChanged += new System.EventHandler(this.txbFilter_TextChanged);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(175, 266);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(177, 28);
            this.cbFilterBy.TabIndex = 33;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::DVLD.Properties.Resources.couple;
            this.pictureBox1.Location = new System.Drawing.Point(642, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 127);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblFilterBy.Location = new System.Drawing.Point(56, 268);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(113, 26);
            this.lblFilterBy.TabIndex = 31;
            this.lblFilterBy.Text = "Filter By:";
            // 
            // lbHeadLine
            // 
            this.lbHeadLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbHeadLine.AutoSize = true;
            this.lbHeadLine.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeadLine.ForeColor = System.Drawing.Color.Red;
            this.lbHeadLine.Location = new System.Drawing.Point(458, 162);
            this.lbHeadLine.Name = "lbHeadLine";
            this.lbHeadLine.Size = new System.Drawing.Size(569, 41);
            this.lbHeadLine.TabIndex = 30;
            this.lbHeadLine.Text = "Detain Driving License Applications ";
            // 
            // dgvDetains
            // 
            this.dgvDetains.AllowUserToAddRows = false;
            this.dgvDetains.AllowUserToDeleteRows = false;
            this.dgvDetains.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvDetains.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetains.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetains.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetains.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDetains.Location = new System.Drawing.Point(61, 312);
            this.dgvDetains.Name = "dgvDetains";
            this.dgvDetains.ReadOnly = true;
            this.dgvDetains.RowHeadersWidth = 62;
            this.dgvDetains.RowTemplate.Height = 28;
            this.dgvDetains.Size = new System.Drawing.Size(1311, 388);
            this.dgvDetains.TabIndex = 29;
            this.dgvDetains.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDetains_CellMouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonDetailsToolStripMenuItem,
            this.showLicenseDetaiToolStripMenuItem,
            this.lToolStripMenuItem,
            this.toolStripSeparator1,
            this.releasDetainedLicenseToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(310, 171);
            // 
            // showPersonDetailsToolStripMenuItem
            // 
            this.showPersonDetailsToolStripMenuItem.Name = "showPersonDetailsToolStripMenuItem";
            this.showPersonDetailsToolStripMenuItem.Size = new System.Drawing.Size(309, 32);
            this.showPersonDetailsToolStripMenuItem.Text = "Show Person Details";
            this.showPersonDetailsToolStripMenuItem.Click += new System.EventHandler(this.showPersonDetailsToolStripMenuItem_Click);
            // 
            // showLicenseDetaiToolStripMenuItem
            // 
            this.showLicenseDetaiToolStripMenuItem.Name = "showLicenseDetaiToolStripMenuItem";
            this.showLicenseDetaiToolStripMenuItem.Size = new System.Drawing.Size(309, 32);
            this.showLicenseDetaiToolStripMenuItem.Text = "Show License Details";
            this.showLicenseDetaiToolStripMenuItem.Click += new System.EventHandler(this.showLicenseDetaiToolStripMenuItem_Click);
            // 
            // lToolStripMenuItem
            // 
            this.lToolStripMenuItem.Name = "lToolStripMenuItem";
            this.lToolStripMenuItem.Size = new System.Drawing.Size(309, 32);
            this.lToolStripMenuItem.Text = "Show Person License History";
            this.lToolStripMenuItem.Click += new System.EventHandler(this.lToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(306, 6);
            // 
            // releasDetainedLicenseToolStripMenuItem
            // 
            this.releasDetainedLicenseToolStripMenuItem.Name = "releasDetainedLicenseToolStripMenuItem";
            this.releasDetainedLicenseToolStripMenuItem.Size = new System.Drawing.Size(309, 32);
            this.releasDetainedLicenseToolStripMenuItem.Text = "Releas Detained License";
            this.releasDetainedLicenseToolStripMenuItem.Click += new System.EventHandler(this.releasDetainedLicenseToolStripMenuItem_Click);
            // 
            // btnRleaseLicense
            // 
            this.btnRleaseLicense.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRleaseLicense.BackColor = System.Drawing.Color.White;
            this.btnRleaseLicense.ForeColor = System.Drawing.Color.Blue;
            this.btnRleaseLicense.Image = global::DVLD.Properties.Resources.administrator__7_;
            this.btnRleaseLicense.Location = new System.Drawing.Point(1152, 231);
            this.btnRleaseLicense.Name = "btnRleaseLicense";
            this.btnRleaseLicense.Size = new System.Drawing.Size(107, 63);
            this.btnRleaseLicense.TabIndex = 39;
            this.btnRleaseLicense.UseVisualStyleBackColor = false;
            this.btnRleaseLicense.Click += new System.EventHandler(this.btnRleaseLicense_Click);
            // 
            // FormManageDetainLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 784);
            this.Controls.Add(this.btnRleaseLicense);
            this.Controls.Add(this.btnDetainLicense);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.txbFilter);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblFilterBy);
            this.Controls.Add(this.lbHeadLine);
            this.Controls.Add(this.dgvDetains);
            this.Name = "FormManageDetainLicense";
            this.Text = "FormManageDetainLicense";
            this.Load += new System.EventHandler(this.FormManageDetainLicense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetains)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDetainLicense;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.TextBox txbFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.Label lbHeadLine;
        private System.Windows.Forms.DataGridView dgvDetains;
        private System.Windows.Forms.Button btnRleaseLicense;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPersonDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetaiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem releasDetainedLicenseToolStripMenuItem;
    }
}