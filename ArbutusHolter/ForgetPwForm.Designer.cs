namespace Uvic_Ecg_ArbutusHolter
{
    partial class ForgetPwForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgetPwForm));
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.mailTextbox = new System.Windows.Forms.TextBox();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.finishPanel = new System.Windows.Forms.Panel();
            this.back = new System.Windows.Forms.Button();
            this.CodeText = new System.Windows.Forms.TextBox();
            this.confirm = new System.Windows.Forms.Button();
            this.finsihLabel = new System.Windows.Forms.Label();
            this.finishPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.BackColor = System.Drawing.Color.Transparent;
            this.descriptionLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.descriptionLabel.Location = new System.Drawing.Point(737, 532);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(457, 52);
            this.descriptionLabel.TabIndex = 1;
            this.descriptionLabel.Text = "Enter your email address, we will sent you an email\r\nto set-up a new password";
            // 
            // mailTextbox
            // 
            this.mailTextbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mailTextbox.Font = new System.Drawing.Font("Calibri Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailTextbox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.mailTextbox.Location = new System.Drawing.Point(827, 650);
            this.mailTextbox.Name = "mailTextbox";
            this.mailTextbox.Size = new System.Drawing.Size(266, 50);
            this.mailTextbox.TabIndex = 2;
            this.mailTextbox.Text = "Email address";
            // 
            // SubmitButton
            // 
            this.SubmitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SubmitButton.Font = new System.Drawing.Font("Calibri Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitButton.Location = new System.Drawing.Point(903, 792);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(114, 42);
            this.SubmitButton.TabIndex = 3;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Calibri Light", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.titleLabel.Location = new System.Drawing.Point(647, 366);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(626, 78);
            this.titleLabel.TabIndex = 4;
            this.titleLabel.Text = "Forget Your Password ?";
            // 
            // finishPanel
            // 
            this.finishPanel.BackColor = System.Drawing.Color.Transparent;
            this.finishPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("finishPanel.BackgroundImage")));
            this.finishPanel.Controls.Add(this.back);
            this.finishPanel.Controls.Add(this.CodeText);
            this.finishPanel.Controls.Add(this.confirm);
            this.finishPanel.Controls.Add(this.finsihLabel);
            this.finishPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.finishPanel.Location = new System.Drawing.Point(0, 0);
            this.finishPanel.Name = "finishPanel";
            this.finishPanel.Size = new System.Drawing.Size(1904, 1041);
            this.finishPanel.TabIndex = 5;
            this.finishPanel.Visible = false;
            // 
            // back
            // 
            this.back.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.back.Font = new System.Drawing.Font("Calibri Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back.Location = new System.Drawing.Point(874, 875);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(172, 51);
            this.back.TabIndex = 5;
            this.back.Text = "Back to Login";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.Back_Click_1);
            // 
            // CodeText
            // 
            this.CodeText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CodeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeText.Location = new System.Drawing.Point(843, 614);
            this.CodeText.Name = "CodeText";
            this.CodeText.Size = new System.Drawing.Size(250, 44);
            this.CodeText.TabIndex = 4;
            this.CodeText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // confirm
            // 
            this.confirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.confirm.Font = new System.Drawing.Font("Calibri Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirm.Location = new System.Drawing.Point(874, 787);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(172, 51);
            this.confirm.TabIndex = 3;
            this.confirm.Text = "Confirm";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // finsihLabel
            // 
            this.finishPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.finsihLabel.AutoSize = true;
            this.finsihLabel.Font = new System.Drawing.Font("Calibri Light", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finsihLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.finsihLabel.Location = new System.Drawing.Point(244, 347);
            this.finsihLabel.Name = "finsihLabel";
            this.finsihLabel.Size = new System.Drawing.Size(1502, 156);
            this.finsihLabel.TabIndex = 0;
            this.finsihLabel.Text = "                       We have sent you an email\r\nPlease check your mailbox and e" +
    "nter the verification code";
            // 
            // ForgetPwForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.finishPanel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.mailTextbox);
            this.Controls.Add(this.descriptionLabel);
            this.Name = "ForgetPwForm";
            this.Text = "ForgetPwForm";
            this.finishPanel.ResumeLayout(false);
            this.finishPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox mailTextbox;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel finishPanel;
        private System.Windows.Forms.Label finsihLabel;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.TextBox CodeText;
        private System.Windows.Forms.Button back;
    }
}