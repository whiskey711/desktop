namespace Uvic_Ecg_ArbutusHolter
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.registerButton = new System.Windows.Forms.Button();
            this.submit = new System.Windows.Forms.Button();
            this.completeNotify = new System.Windows.Forms.Label();
            this.finishPanel = new System.Windows.Forms.Panel();
            this.regVerifyTextBox = new System.Windows.Forms.TextBox();
            this.lastN = new System.Windows.Forms.PlaceholderTextBox();
            this.firstN = new System.Windows.Forms.PlaceholderTextBox();
            this.confirmPass = new System.Windows.Forms.PlaceholderTextBox();
            this.password = new System.Windows.Forms.PlaceholderTextBox();
            this.email = new System.Windows.Forms.PlaceholderTextBox();
            this.finishPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // registerButton
            // 
            this.registerButton.Font = new System.Drawing.Font("Calibri Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerButton.Location = new System.Drawing.Point(888, 657);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(128, 44);
            this.registerButton.TabIndex = 15;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // submit
            // 
            this.submit.AutoSize = true;
            this.submit.Font = new System.Drawing.Font("Calibri Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submit.Location = new System.Drawing.Point(874, 695);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(172, 51);
            this.submit.TabIndex = 2;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // completeNotify
            // 
            this.completeNotify.AutoSize = true;
            this.completeNotify.BackColor = System.Drawing.Color.Transparent;
            this.completeNotify.Font = new System.Drawing.Font("Calibri Light", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.completeNotify.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.completeNotify.Location = new System.Drawing.Point(336, 362);
            this.completeNotify.Name = "completeNotify";
            this.completeNotify.Size = new System.Drawing.Size(1222, 156);
            this.completeNotify.TabIndex = 3;
            this.completeNotify.Text = "               Please enter the verification code \r\nthat we sent to your registra" +
    "tion email address";
            // 
            // finishPanel
            // 
            this.finishPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("finishPanel.BackgroundImage")));
            this.finishPanel.Controls.Add(this.regVerifyTextBox);
            this.finishPanel.Controls.Add(this.completeNotify);
            this.finishPanel.Controls.Add(this.submit);
            this.finishPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.finishPanel.Location = new System.Drawing.Point(0, 0);
            this.finishPanel.Name = "finishPanel";
            this.finishPanel.Size = new System.Drawing.Size(1904, 1041);
            this.finishPanel.TabIndex = 16;
            this.finishPanel.Visible = false;
            // 
            // regVerifyTextBox
            // 
            this.regVerifyTextBox.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regVerifyTextBox.Location = new System.Drawing.Point(829, 601);
            this.regVerifyTextBox.Name = "regVerifyTextBox";
            this.regVerifyTextBox.Size = new System.Drawing.Size(261, 50);
            this.regVerifyTextBox.TabIndex = 4;
            // 
            // lastN
            // 
            this.lastN.Font = new System.Drawing.Font("Calibri Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastN.Location = new System.Drawing.Point(790, 555);
            this.lastN.Margin = new System.Windows.Forms.Padding(2);
            this.lastN.Name = "lastN";
            this.lastN.PlaceholderText = "Last Name";
            this.lastN.PlaceholderTextColor = System.Drawing.Color.Gray;
            this.lastN.Size = new System.Drawing.Size(325, 50);
            this.lastN.TabIndex = 14;
            this.lastN.TextColor = System.Drawing.Color.Black;
            // 
            // firstN
            // 
            this.firstN.Font = new System.Drawing.Font("Calibri Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstN.Location = new System.Drawing.Point(790, 501);
            this.firstN.Margin = new System.Windows.Forms.Padding(2);
            this.firstN.Name = "firstN";
            this.firstN.PlaceholderText = "First Name";
            this.firstN.PlaceholderTextColor = System.Drawing.Color.Gray;
            this.firstN.Size = new System.Drawing.Size(325, 50);
            this.firstN.TabIndex = 13;
            this.firstN.TextColor = System.Drawing.Color.Black;
            // 
            // confirmPass
            // 
            this.confirmPass.Font = new System.Drawing.Font("Calibri Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmPass.Location = new System.Drawing.Point(790, 447);
            this.confirmPass.Margin = new System.Windows.Forms.Padding(2);
            this.confirmPass.Name = "confirmPass";
            this.confirmPass.PlaceholderText = "Confirm Password";
            this.confirmPass.PlaceholderTextColor = System.Drawing.Color.Gray;
            this.confirmPass.Size = new System.Drawing.Size(325, 50);
            this.confirmPass.TabIndex = 12;
            this.confirmPass.TextColor = System.Drawing.Color.Black;
            this.confirmPass.TextChanged += new System.EventHandler(this.ConfirmPass_TextChanged);
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Calibri Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.Location = new System.Drawing.Point(790, 393);
            this.password.Margin = new System.Windows.Forms.Padding(2);
            this.password.Name = "password";
            this.password.PlaceholderText = "Password";
            this.password.PlaceholderTextColor = System.Drawing.Color.Gray;
            this.password.Size = new System.Drawing.Size(325, 50);
            this.password.TabIndex = 11;
            this.password.TextColor = System.Drawing.Color.Black;
            this.password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // email
            // 
            this.email.Font = new System.Drawing.Font("Calibri Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.Location = new System.Drawing.Point(790, 339);
            this.email.Margin = new System.Windows.Forms.Padding(2);
            this.email.Name = "email";
            this.email.PlaceholderText = "Email";
            this.email.PlaceholderTextColor = System.Drawing.Color.Gray;
            this.email.Size = new System.Drawing.Size(325, 50);
            this.email.TabIndex = 10;
            this.email.TextColor = System.Drawing.Color.Black;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.finishPanel);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.lastN);
            this.Controls.Add(this.firstN);
            this.Controls.Add(this.confirmPass);
            this.Controls.Add(this.password);
            this.Controls.Add(this.email);
            this.Name = "RegisterForm";
            this.Text = "Register";
            this.finishPanel.ResumeLayout(false);
            this.finishPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.PlaceholderTextBox lastN;
        private System.Windows.Forms.PlaceholderTextBox firstN;
        private System.Windows.Forms.PlaceholderTextBox confirmPass;
        private System.Windows.Forms.PlaceholderTextBox password;
        private System.Windows.Forms.PlaceholderTextBox email;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Label completeNotify;
        private System.Windows.Forms.Panel finishPanel;
        private System.Windows.Forms.TextBox regVerifyTextBox;
    }
}