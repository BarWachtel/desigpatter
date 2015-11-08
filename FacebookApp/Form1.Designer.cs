namespace FacebookApp
{
    partial class Form1
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
            this.usersName_label = new System.Windows.Forms.Label();
            this.profilePicture_smallPictureBox = new System.Windows.Forms.PictureBox();
            this.login_button = new System.Windows.Forms.Button();
            this.logout_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.profilePicture_smallPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // usersName_label
            // 
            this.usersName_label.AutoSize = true;
            this.usersName_label.Location = new System.Drawing.Point(13, 13);
            this.usersName_label.Name = "usersName_label";
            this.usersName_label.Size = new System.Drawing.Size(0, 13);
            this.usersName_label.TabIndex = 0;
            // 
            // profilePicture_smallPictureBox
            // 
            this.profilePicture_smallPictureBox.Location = new System.Drawing.Point(16, 29);
            this.profilePicture_smallPictureBox.Name = "profilePicture_smallPictureBox";
            this.profilePicture_smallPictureBox.Size = new System.Drawing.Size(149, 135);
            this.profilePicture_smallPictureBox.TabIndex = 1;
            this.profilePicture_smallPictureBox.TabStop = false;
            // 
            // login_button
            // 
            this.login_button.Location = new System.Drawing.Point(12, 170);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(75, 23);
            this.login_button.TabIndex = 2;
            this.login_button.Text = "Login";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // logout_button
            // 
            this.logout_button.Location = new System.Drawing.Point(90, 170);
            this.logout_button.Name = "logout_button";
            this.logout_button.Size = new System.Drawing.Size(75, 23);
            this.logout_button.TabIndex = 3;
            this.logout_button.Text = "Logout";
            this.logout_button.UseVisualStyleBackColor = true;
            this.logout_button.Click += new System.EventHandler(this.logout_button_Click);
           
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(903, 417);
            this.Controls.Add(this.logout_button);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.profilePicture_smallPictureBox);
            this.Controls.Add(this.usersName_label);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.profilePicture_smallPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usersName_label;
        private System.Windows.Forms.PictureBox profilePicture_smallPictureBox;
        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.Button logout_button;
    }
}

