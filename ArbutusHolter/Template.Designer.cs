namespace Uvic_Ecg_ArbutusHolter
{
    partial class Template
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
            this.tempRichTb = new System.Windows.Forms.RichTextBox();
            this.saveTempBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tempRichTb
            // 
            this.tempRichTb.Location = new System.Drawing.Point(12, 12);
            this.tempRichTb.Name = "tempRichTb";
            this.tempRichTb.Size = new System.Drawing.Size(484, 383);
            this.tempRichTb.TabIndex = 0;
            this.tempRichTb.Text = "";
            // 
            // saveTempBtn
            // 
            this.saveTempBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveTempBtn.Location = new System.Drawing.Point(214, 401);
            this.saveTempBtn.Name = "saveTempBtn";
            this.saveTempBtn.Size = new System.Drawing.Size(75, 23);
            this.saveTempBtn.TabIndex = 1;
            this.saveTempBtn.Text = "&Save";
            this.saveTempBtn.UseVisualStyleBackColor = true;
            this.saveTempBtn.Click += new System.EventHandler(this.SaveTempBtn_Click);
            // 
            // Template
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 449);
            this.Controls.Add(this.saveTempBtn);
            this.Controls.Add(this.tempRichTb);
            this.Name = "Template";
            this.Text = "Template";
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.RichTextBox tempRichTb;
        private System.Windows.Forms.Button saveTempBtn;
    }
}