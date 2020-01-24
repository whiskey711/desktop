namespace Uvic_Ecg_ArbutusHolter
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.loginButton = new System.Windows.Forms.Button();
            this.registerButton = new System.Windows.Forms.Button();
            this.forgetPWButton = new System.Windows.Forms.Button();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.PlaceholderTextBox();
            this.userEmail = new System.Windows.Forms.PlaceholderTextBox();
            this.SuspendLayout();
            // 
            // loginButton
            // 
            this.loginButton.Font = new System.Drawing.Font("Calibri Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginButton.Location = new System.Drawing.Point(838, 660);
            this.loginButton.Margin = new System.Windows.Forms.Padding(2);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(114, 42);
            this.loginButton.TabIndex = 4;
            this.loginButton.Text = "Log in";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // registerButton
            // 
            this.registerButton.Font = new System.Drawing.Font("Calibri Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerButton.Location = new System.Drawing.Point(968, 660);
            this.registerButton.Margin = new System.Windows.Forms.Padding(2);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(114, 42);
            this.registerButton.TabIndex = 6;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // forgetPWButton
            // 
            this.forgetPWButton.Font = new System.Drawing.Font("Calibri Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forgetPWButton.Location = new System.Drawing.Point(838, 706);
            this.forgetPWButton.Margin = new System.Windows.Forms.Padding(2);
            this.forgetPWButton.Name = "forgetPWButton";
            this.forgetPWButton.Size = new System.Drawing.Size(244, 42);
            this.forgetPWButton.TabIndex = 7;
            this.forgetPWButton.Text = "Forget password ?";
            this.forgetPWButton.UseVisualStyleBackColor = true;
            this.forgetPWButton.Click += new System.EventHandler(this.ForgetPWButton_Click);
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.BackColor = System.Drawing.Color.Transparent;
            this.welcomeLabel.Font = new System.Drawing.Font("Calibri Light", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.welcomeLabel.Location = new System.Drawing.Point(755, 300);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(405, 156);
            this.welcomeLabel.TabIndex = 8;
            this.welcomeLabel.Text = "WELCOME TO \r\n    ARBUTUS";
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Calibri Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.Location = new System.Drawing.Point(798, 569);
            this.password.Margin = new System.Windows.Forms.Padding(2);
            this.password.Name = "password";
            this.password.PlaceholderText = "Password";
            this.password.PlaceholderTextColor = System.Drawing.Color.Gray;
            this.password.Size = new System.Drawing.Size(325, 50);
            this.password.TabIndex = 3;
            this.password.TextColor = System.Drawing.Color.Black;
            this.password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // userEmail
            // 
            this.userEmail.Font = new System.Drawing.Font("Calibri Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userEmail.Location = new System.Drawing.Point(798, 515);
            this.userEmail.Margin = new System.Windows.Forms.Padding(2);
            this.userEmail.Name = "userEmail";
            this.userEmail.PlaceholderText = "Email";
            this.userEmail.PlaceholderTextColor = System.Drawing.Color.Gray;
            this.userEmail.Size = new System.Drawing.Size(325, 50);
            this.userEmail.TabIndex = 2;
            this.userEmail.TextColor = System.Drawing.Color.Black;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.loginButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.welcomeLabel);
            this.Controls.Add(this.forgetPWButton);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.password);
            this.Controls.Add(this.userEmail);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LoginForm";
            this.Text = "Arbutus Holter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.PlaceholderTextBox userEmail;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.PlaceholderTextBox password;
        private System.Windows.Forms.Button forgetPWButton;
        private System.Windows.Forms.Label welcomeLabel;
    }
}

