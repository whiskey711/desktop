namespace ArbutusHolter
{
    partial class homeForm
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
            this.toMain = new System.Windows.Forms.Label();
            this.toCheck = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // toMain
            // 
            this.toMain.AutoSize = true;
            this.toMain.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toMain.Location = new System.Drawing.Point(523, 480);
            this.toMain.Name = "toMain";
            this.toMain.Size = new System.Drawing.Size(201, 78);
            this.toMain.TabIndex = 0;
            this.toMain.Text = "To create and\r\n hook up form";
            this.toMain.Click += new System.EventHandler(this.toMain_Click);
            // 
            // toCheck
            // 
            this.toCheck.AutoSize = true;
            this.toCheck.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toCheck.Location = new System.Drawing.Point(1103, 480);
            this.toCheck.Name = "toCheck";
            this.toCheck.Size = new System.Drawing.Size(238, 78);
            this.toCheck.TabIndex = 1;
            this.toCheck.Text = "To check patient \r\n     record form";
            this.toCheck.Click += new System.EventHandler(this.toCheck_Click);
            // 
            // homeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.toCheck);
            this.Controls.Add(this.toMain);
            this.Name = "homeForm";
            this.Text = "Homepage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label toMain;
        private System.Windows.Forms.Label toCheck;
    }
}