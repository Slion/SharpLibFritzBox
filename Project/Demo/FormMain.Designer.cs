namespace FritzBoxDemo
{
    partial class FormMain
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
            this.iButtonLogin = new System.Windows.Forms.Button();
            this.iTextBoxLogin = new System.Windows.Forms.TextBox();
            this.iTextBoxPassword = new System.Windows.Forms.TextBox();
            this.iLabelPassword = new System.Windows.Forms.Label();
            this.iLabelLogin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // iButtonLogin
            // 
            this.iButtonLogin.Location = new System.Drawing.Point(197, 226);
            this.iButtonLogin.Name = "iButtonLogin";
            this.iButtonLogin.Size = new System.Drawing.Size(75, 23);
            this.iButtonLogin.TabIndex = 0;
            this.iButtonLogin.Text = "Login";
            this.iButtonLogin.UseVisualStyleBackColor = true;
            this.iButtonLogin.Click += new System.EventHandler(this.iButtonLogin_Click);
            // 
            // iTextBoxLogin
            // 
            this.iTextBoxLogin.Location = new System.Drawing.Point(172, 169);
            this.iTextBoxLogin.Name = "iTextBoxLogin";
            this.iTextBoxLogin.Size = new System.Drawing.Size(100, 20);
            this.iTextBoxLogin.TabIndex = 1;
            // 
            // iTextBoxPassword
            // 
            this.iTextBoxPassword.Location = new System.Drawing.Point(172, 195);
            this.iTextBoxPassword.Name = "iTextBoxPassword";
            this.iTextBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.iTextBoxPassword.TabIndex = 2;
            this.iTextBoxPassword.UseSystemPasswordChar = true;
            // 
            // iLabelPassword
            // 
            this.iLabelPassword.AutoSize = true;
            this.iLabelPassword.Location = new System.Drawing.Point(110, 198);
            this.iLabelPassword.Name = "iLabelPassword";
            this.iLabelPassword.Size = new System.Drawing.Size(56, 13);
            this.iLabelPassword.TabIndex = 3;
            this.iLabelPassword.Text = "Password:";
            // 
            // iLabelLogin
            // 
            this.iLabelLogin.AutoSize = true;
            this.iLabelLogin.Location = new System.Drawing.Point(110, 172);
            this.iLabelLogin.Name = "iLabelLogin";
            this.iLabelLogin.Size = new System.Drawing.Size(36, 13);
            this.iLabelLogin.TabIndex = 4;
            this.iLabelLogin.Text = "Login:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.iLabelLogin);
            this.Controls.Add(this.iLabelPassword);
            this.Controls.Add(this.iTextBoxPassword);
            this.Controls.Add(this.iTextBoxLogin);
            this.Controls.Add(this.iButtonLogin);
            this.Name = "FormMain";
            this.Text = "Fritz!Box Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button iButtonLogin;
        private System.Windows.Forms.TextBox iTextBoxLogin;
        private System.Windows.Forms.TextBox iTextBoxPassword;
        private System.Windows.Forms.Label iLabelPassword;
        private System.Windows.Forms.Label iLabelLogin;
    }
}

