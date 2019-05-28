namespace ArbutusHolter
{
    partial class ECG
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
            this.ecgView = new Test.ECGAnimationView();
            this.leftBtn = new System.Windows.Forms.Button();
            this.rightBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ecgView
            // 
            this.ecgView.Location = new System.Drawing.Point(250, 100);
            this.ecgView.Name = "ecgView";
            this.ecgView.Size = new System.Drawing.Size(701, 200);
            this.ecgView.TabIndex = 0;
            // 
            // leftBtn
            // 
            this.leftBtn.AutoSize = true;
            this.leftBtn.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftBtn.Location = new System.Drawing.Point(100, 196);
            this.leftBtn.Name = "leftBtn";
            this.leftBtn.Size = new System.Drawing.Size(110, 36);
            this.leftBtn.TabIndex = 1;
            this.leftBtn.Text = "Move left";
            this.leftBtn.UseVisualStyleBackColor = true;
            this.leftBtn.Click += new System.EventHandler(this.leftBtn_Click);
            // 
            // rightBtn
            // 
            this.rightBtn.AutoSize = true;
            this.rightBtn.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightBtn.Location = new System.Drawing.Point(995, 196);
            this.rightBtn.Name = "rightBtn";
            this.rightBtn.Size = new System.Drawing.Size(115, 36);
            this.rightBtn.TabIndex = 2;
            this.rightBtn.Text = "Move right";
            this.rightBtn.UseVisualStyleBackColor = true;
            this.rightBtn.Click += new System.EventHandler(this.rightBtn_Click);
            // 
            // ECG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 461);
            this.Controls.Add(this.rightBtn);
            this.Controls.Add(this.leftBtn);
            this.Controls.Add(this.ecgView);
            this.Name = "ECG";
            this.Text = "ECG";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Test.ECGAnimationView ecgView;
        private System.Windows.Forms.Button leftBtn;
        private System.Windows.Forms.Button rightBtn;
    }
}