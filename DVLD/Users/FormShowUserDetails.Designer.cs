namespace DVLD
{
    partial class FormShowUserDetails
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
            this.userControlShowPersonDetails1 = new DVLD.UserControlShowPersonDetails();
            this.userControlUserDetails1 = new DVLD.UserControlUserDetails();
            this.SuspendLayout();
            // 
            // userControlShowPersonDetails1
            // 
            this.userControlShowPersonDetails1.Location = new System.Drawing.Point(12, 337);
            this.userControlShowPersonDetails1.Name = "userControlShowPersonDetails1";
            this.userControlShowPersonDetails1.Size = new System.Drawing.Size(1406, 590);
            this.userControlShowPersonDetails1.TabIndex = 0;
            // 
            // userControlUserDetails1
            // 
            this.userControlUserDetails1.Location = new System.Drawing.Point(3, 104);
            this.userControlUserDetails1.Name = "userControlUserDetails1";
            this.userControlUserDetails1.Size = new System.Drawing.Size(1413, 227);
            this.userControlUserDetails1.TabIndex = 1;
            // 
            // FormShowUserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1428, 939);
            this.Controls.Add(this.userControlUserDetails1);
            this.Controls.Add(this.userControlShowPersonDetails1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormShowUserDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormShowUserDetails";
            this.Load += new System.EventHandler(this.FormShowUserDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControlShowPersonDetails userControlShowPersonDetails1;
        private UserControlUserDetails userControlUserDetails1;
    }
}