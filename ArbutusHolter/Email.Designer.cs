namespace Uvic_Ecg_ArbutusHolter
{
    partial class Email
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
            this.mailContentRichTB = new System.Windows.Forms.RichTextBox();
            this.ToLab = new System.Windows.Forms.Label();
            this.subjectLab = new System.Windows.Forms.Label();
            this.toTextBox = new System.Windows.Forms.TextBox();
            this.subjectTextBox = new System.Windows.Forms.TextBox();
            this.sendMailBtn = new System.Windows.Forms.Button();
            this.savePdfBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mailContentRichTB
            // 
            this.mailContentRichTB.Location = new System.Drawing.Point(12, 139);
            this.mailContentRichTB.Name = "mailContentRichTB";
            this.mailContentRichTB.Size = new System.Drawing.Size(441, 675);
            this.mailContentRichTB.TabIndex = 0;
            this.mailContentRichTB.Text = "";
            // 
            // ToLab
            // 
            this.ToLab.AutoSize = true;
            this.ToLab.Location = new System.Drawing.Point(12, 28);
            this.ToLab.Name = "ToLab";
            this.ToLab.Size = new System.Drawing.Size(26, 13);
            this.ToLab.TabIndex = 1;
            this.ToLab.Text = "To :";
            // 
            // subjectLab
            // 
            this.subjectLab.AutoSize = true;
            this.subjectLab.Location = new System.Drawing.Point(12, 74);
            this.subjectLab.Name = "subjectLab";
            this.subjectLab.Size = new System.Drawing.Size(49, 13);
            this.subjectLab.TabIndex = 2;
            this.subjectLab.Text = "Subject :";
            // 
            // toTextBox
            // 
            this.toTextBox.Location = new System.Drawing.Point(67, 25);
            this.toTextBox.Name = "toTextBox";
            this.toTextBox.Size = new System.Drawing.Size(386, 20);
            this.toTextBox.TabIndex = 3;
            // 
            // subjectTextBox
            // 
            this.subjectTextBox.Location = new System.Drawing.Point(67, 71);
            this.subjectTextBox.Name = "subjectTextBox";
            this.subjectTextBox.Size = new System.Drawing.Size(386, 20);
            this.subjectTextBox.TabIndex = 4;
            // 
            // sendMailBtn
            // 
            this.sendMailBtn.Location = new System.Drawing.Point(186, 820);
            this.sendMailBtn.Name = "sendMailBtn";
            this.sendMailBtn.Size = new System.Drawing.Size(75, 23);
            this.sendMailBtn.TabIndex = 5;
            this.sendMailBtn.Text = "Send";
            this.sendMailBtn.UseVisualStyleBackColor = true;
            this.sendMailBtn.Click += new System.EventHandler(this.SendMailBtn_Click);
            // 
            // savePdfBtn
            // 
            this.savePdfBtn.AutoSize = true;
            this.savePdfBtn.Location = new System.Drawing.Point(401, 97);
            this.savePdfBtn.Name = "savePdfBtn";
            this.savePdfBtn.Size = new System.Drawing.Size(52, 36);
            this.savePdfBtn.TabIndex = 6;
            this.savePdfBtn.Text = " Save \r\nas PDF";
            this.savePdfBtn.UseVisualStyleBackColor = true;
            this.savePdfBtn.Click += new System.EventHandler(this.SavePdfBtn_Click);
            // 
            // Email
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 855);
            this.Controls.Add(this.savePdfBtn);
            this.Controls.Add(this.sendMailBtn);
            this.Controls.Add(this.subjectTextBox);
            this.Controls.Add(this.toTextBox);
            this.Controls.Add(this.subjectLab);
            this.Controls.Add(this.ToLab);
            this.Controls.Add(this.mailContentRichTB);
            this.Name = "Email";
            this.Text = "Email";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.RichTextBox mailContentRichTB;
        private System.Windows.Forms.Label ToLab;
        private System.Windows.Forms.Label subjectLab;
        private System.Windows.Forms.TextBox toTextBox;
        private System.Windows.Forms.TextBox subjectTextBox;
        private System.Windows.Forms.Button sendMailBtn;
        private System.Windows.Forms.Button savePdfBtn;
    }
}