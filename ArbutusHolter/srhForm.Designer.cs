namespace ArbutusHolter
{
    partial class srhForm
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
            this.matchList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // matchList
            // 
            this.matchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.matchList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matchList.FormattingEnabled = true;
            this.matchList.ItemHeight = 19;
            this.matchList.Location = new System.Drawing.Point(0, 0);
            this.matchList.Name = "matchList";
            this.matchList.Size = new System.Drawing.Size(653, 841);
            this.matchList.TabIndex = 0;
            this.matchList.SelectedIndexChanged += new System.EventHandler(this.matchList_SelectedIndexChanged);
            // 
            // srhForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 841);
            this.Controls.Add(this.matchList);
            this.Name = "srhForm";
            this.Text = "srhForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox matchList;
    }
}