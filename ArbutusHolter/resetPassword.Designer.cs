namespace Uvic_Ecg_ArbutusHolter
{
    partial class ResetPassword
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
            this.newPswd = new System.Windows.Forms.TextBox();
            this.newPswdConfirm = new System.Windows.Forms.TextBox();
            this.indicationLabel = new System.Windows.Forms.Label();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.newPswdLabel = new System.Windows.Forms.Label();
            this.newPswdConfirmLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newPswd
            // 
            this.newPswd.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPswd.Location = new System.Drawing.Point(792, 494);
            this.newPswd.Name = "newPswd";
            this.newPswd.Size = new System.Drawing.Size(309, 62);
            this.newPswd.TabIndex = 0;
            // 
            // newPswdConfirm
            // 
            this.newPswdConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPswdConfirm.Location = new System.Drawing.Point(792, 613);
            this.newPswdConfirm.Name = "newPswdConfirm";
            this.newPswdConfirm.Size = new System.Drawing.Size(309, 62);
            this.newPswdConfirm.TabIndex = 1;
            // 
            // label1
            // 
            this.indicationLabel.AutoSize = true;
            this.indicationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.indicationLabel.Location = new System.Drawing.Point(677, 345);
            this.indicationLabel.Name = "label1";
            this.indicationLabel.Size = new System.Drawing.Size(518, 39);
            this.indicationLabel.TabIndex = 2;
            this.indicationLabel.Text = "Please enter your new password";
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmBtn.Location = new System.Drawing.Point(878, 763);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(128, 64);
            this.ConfirmBtn.TabIndex = 3;
            this.ConfirmBtn.Text = "Confirm";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            this.ConfirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // label2
            // 
            this.newPswdLabel.AutoSize = true;
            this.newPswdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPswdLabel.Location = new System.Drawing.Point(606, 511);
            this.newPswdLabel.Name = "label2";
            this.newPswdLabel.Size = new System.Drawing.Size(152, 25);
            this.newPswdLabel.TabIndex = 4;
            this.newPswdLabel.Text = "New password";
            // 
            // label3
            // 
            this.newPswdConfirmLabel.AutoSize = true;
            this.newPswdConfirmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPswdConfirmLabel.Location = new System.Drawing.Point(574, 628);
            this.newPswdConfirmLabel.Name = "label3";
            this.newPswdConfirmLabel.Size = new System.Drawing.Size(184, 25);
            this.newPswdConfirmLabel.TabIndex = 5;
            this.newPswdConfirmLabel.Text = "Confirm password";
            // 
            // ResetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.newPswdConfirmLabel);
            this.Controls.Add(this.newPswdLabel);
            this.Controls.Add(this.ConfirmBtn);
            this.Controls.Add(this.indicationLabel);
            this.Controls.Add(this.newPswdConfirm);
            this.Controls.Add(this.newPswd);
            this.Name = "ResetPassword";
            this.Text = "resetPassword";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.TextBox newPswd;
        private System.Windows.Forms.TextBox newPswdConfirm;
        private System.Windows.Forms.Label indicationLabel;
        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.Label newPswdLabel;
        private System.Windows.Forms.Label newPswdConfirmLabel;
    }
}