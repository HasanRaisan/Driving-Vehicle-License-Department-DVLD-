namespace DVLD.Mange_Applications
{
    partial class FormShowApplicationDetails
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
            this.UserConShowBasicApplicationInfo1 = new DVLD.UserConShowLocalApplicationInfo();
            this.SuspendLayout();
            // 
            // UserConShowBasicApplicationInfo1
            // 
            this.UserConShowBasicApplicationInfo1.Location = new System.Drawing.Point(12, 12);
            this.UserConShowBasicApplicationInfo1.Name = "UserConShowBasicApplicationInfo1";
            this.UserConShowBasicApplicationInfo1.Size = new System.Drawing.Size(1280, 588);
            this.UserConShowBasicApplicationInfo1.TabIndex = 0;
            // 
            // FormShowApplicationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 618);
            this.Controls.Add(this.UserConShowBasicApplicationInfo1);
            this.Name = "FormShowApplicationDetails";
            this.Text = "FormShowApplicationDetails";
            this.ResumeLayout(false);

        }

        #endregion

        private UserConShowLocalApplicationInfo UserConShowBasicApplicationInfo1;
    }
}