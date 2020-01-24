namespace Uvic_Ecg_ArbutusHolter
{
    partial class ErrorForm
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
            this.msgLabel = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // msgLabel
            // 
            this.msgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgLabel.Location = new System.Drawing.Point(0, 0);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(469, 270);
            this.msgLabel.TabIndex = 0;
            this.msgLabel.Text = "label1";
            this.msgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.okBtn.Location = new System.Drawing.Point(185, 218);
            this.okBtn.Name = "button1";
            this.okBtn.Size = new System.Drawing.Size(98, 40);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "okay";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.Button1_Click);
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 270);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.msgLabel);
            this.Name = "ErrorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ErrorForm";
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Label msgLabel;
        private System.Windows.Forms.Button okBtn;
    }
}