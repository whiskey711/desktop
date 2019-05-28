namespace ArbutusHolter
{
    partial class Record
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Record));
            this.recordTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.GbofSearch = new System.Windows.Forms.GroupBox();
            this.patientList = new System.Windows.Forms.ListBox();
            this.labOfFN = new System.Windows.Forms.Label();
            this.btnOfSearch = new System.Windows.Forms.Button();
            this.labOfLN = new System.Windows.Forms.Label();
            this.txtOfPhn = new System.Windows.Forms.TextBox();
            this.labOfDOB = new System.Windows.Forms.Label();
            this.txtOfBOD = new System.Windows.Forms.TextBox();
            this.labOfPhn = new System.Windows.Forms.Label();
            this.txtOfLN = new System.Windows.Forms.TextBox();
            this.txtOfFN = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AddrLabel = new System.Windows.Forms.Label();
            this.PnLabel = new System.Windows.Forms.Label();
            this.PhnLabel = new System.Windows.Forms.Label();
            this.DobLabel = new System.Windows.Forms.Label();
            this.LnLabel = new System.Windows.Forms.Label();
            this.FnLabel = new System.Windows.Forms.Label();
            this.ecgRecordList = new System.Windows.Forms.ListBox();
            this.recordTableLayout.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.GbofSearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // recordTableLayout
            // 
            this.recordTableLayout.ColumnCount = 2;
            this.recordTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.recordTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.recordTableLayout.Controls.Add(this.groupBox2, 0, 1);
            this.recordTableLayout.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.recordTableLayout.Controls.Add(this.groupBox1, 1, 0);
            this.recordTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recordTableLayout.Location = new System.Drawing.Point(0, 0);
            this.recordTableLayout.Name = "recordTableLayout";
            this.recordTableLayout.RowCount = 2;
            this.recordTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.recordTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.recordTableLayout.Size = new System.Drawing.Size(1904, 1041);
            this.recordTableLayout.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ecgRecordList);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(479, 523);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1422, 515);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ecg Record";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.GbofSearch);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.recordTableLayout.SetRowSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(470, 1035);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // GbofSearch
            // 
            this.GbofSearch.Controls.Add(this.patientList);
            this.GbofSearch.Controls.Add(this.labOfFN);
            this.GbofSearch.Controls.Add(this.btnOfSearch);
            this.GbofSearch.Controls.Add(this.labOfLN);
            this.GbofSearch.Controls.Add(this.txtOfPhn);
            this.GbofSearch.Controls.Add(this.labOfDOB);
            this.GbofSearch.Controls.Add(this.txtOfBOD);
            this.GbofSearch.Controls.Add(this.labOfPhn);
            this.GbofSearch.Controls.Add(this.txtOfLN);
            this.GbofSearch.Controls.Add(this.txtOfFN);
            this.GbofSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GbofSearch.Location = new System.Drawing.Point(3, 3);
            this.GbofSearch.Name = "GbofSearch";
            this.GbofSearch.Size = new System.Drawing.Size(467, 1035);
            this.GbofSearch.TabIndex = 10;
            this.GbofSearch.TabStop = false;
            this.GbofSearch.Text = "Search Patient";
            // 
            // patientList
            // 
            this.patientList.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patientList.FormattingEnabled = true;
            this.patientList.ItemHeight = 23;
            this.patientList.Items.AddRange(new object[] {
            "Charles Liu 2019-01-01 10001",
            "James Xu 2019-02-01 10000"});
            this.patientList.Location = new System.Drawing.Point(0, 289);
            this.patientList.Name = "patientList";
            this.patientList.Size = new System.Drawing.Size(461, 740);
            this.patientList.TabIndex = 9;
            this.patientList.SelectedIndexChanged += new System.EventHandler(this.patientList_SelectedIndexChanged);
            // 
            // labOfFN
            // 
            this.labOfFN.AutoSize = true;
            this.labOfFN.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labOfFN.Location = new System.Drawing.Point(37, 39);
            this.labOfFN.Name = "labOfFN";
            this.labOfFN.Size = new System.Drawing.Size(105, 26);
            this.labOfFN.TabIndex = 0;
            this.labOfFN.Text = "First Name";
            // 
            // btnOfSearch
            // 
            this.btnOfSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnOfSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOfSearch.BackgroundImage")));
            this.btnOfSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOfSearch.Location = new System.Drawing.Point(380, 105);
            this.btnOfSearch.Name = "btnOfSearch";
            this.btnOfSearch.Size = new System.Drawing.Size(55, 55);
            this.btnOfSearch.TabIndex = 8;
            this.btnOfSearch.UseVisualStyleBackColor = false;
            this.btnOfSearch.Click += new System.EventHandler(this.btnOfSearch_Click);
            // 
            // labOfLN
            // 
            this.labOfLN.AutoSize = true;
            this.labOfLN.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labOfLN.Location = new System.Drawing.Point(37, 86);
            this.labOfLN.Name = "labOfLN";
            this.labOfLN.Size = new System.Drawing.Size(102, 26);
            this.labOfLN.TabIndex = 1;
            this.labOfLN.Text = "Last Name";
            // 
            // txtOfPhn
            // 
            this.txtOfPhn.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOfPhn.Location = new System.Drawing.Point(169, 177);
            this.txtOfPhn.Name = "txtOfPhn";
            this.txtOfPhn.Size = new System.Drawing.Size(157, 33);
            this.txtOfPhn.TabIndex = 7;
            // 
            // labOfDOB
            // 
            this.labOfDOB.AutoSize = true;
            this.labOfDOB.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labOfDOB.Location = new System.Drawing.Point(37, 134);
            this.labOfDOB.Name = "labOfDOB";
            this.labOfDOB.Size = new System.Drawing.Size(84, 26);
            this.labOfDOB.TabIndex = 2;
            this.labOfDOB.Text = "Birthday";
            // 
            // txtOfBOD
            // 
            this.txtOfBOD.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOfBOD.Location = new System.Drawing.Point(169, 131);
            this.txtOfBOD.Name = "txtOfBOD";
            this.txtOfBOD.Size = new System.Drawing.Size(157, 33);
            this.txtOfBOD.TabIndex = 6;
            // 
            // labOfPhn
            // 
            this.labOfPhn.AutoSize = true;
            this.labOfPhn.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labOfPhn.Location = new System.Drawing.Point(37, 180);
            this.labOfPhn.Name = "labOfPhn";
            this.labOfPhn.Size = new System.Drawing.Size(50, 26);
            this.labOfPhn.TabIndex = 3;
            this.labOfPhn.Text = "PHN";
            // 
            // txtOfLN
            // 
            this.txtOfLN.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOfLN.Location = new System.Drawing.Point(169, 83);
            this.txtOfLN.Name = "txtOfLN";
            this.txtOfLN.Size = new System.Drawing.Size(157, 33);
            this.txtOfLN.TabIndex = 5;
            // 
            // txtOfFN
            // 
            this.txtOfFN.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOfFN.Location = new System.Drawing.Point(169, 32);
            this.txtOfFN.Name = "txtOfFN";
            this.txtOfFN.Size = new System.Drawing.Size(157, 33);
            this.txtOfFN.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AddrLabel);
            this.groupBox1.Controls.Add(this.PnLabel);
            this.groupBox1.Controls.Add(this.PhnLabel);
            this.groupBox1.Controls.Add(this.DobLabel);
            this.groupBox1.Controls.Add(this.LnLabel);
            this.groupBox1.Controls.Add(this.FnLabel);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(479, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1422, 514);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Infomation";
            // 
            // AddrLabel
            // 
            this.AddrLabel.AutoSize = true;
            this.AddrLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddrLabel.Location = new System.Drawing.Point(288, 90);
            this.AddrLabel.Name = "AddrLabel";
            this.AddrLabel.Size = new System.Drawing.Size(79, 26);
            this.AddrLabel.TabIndex = 12;
            this.AddrLabel.Text = "Address";
            // 
            // PnLabel
            // 
            this.PnLabel.AutoSize = true;
            this.PnLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnLabel.Location = new System.Drawing.Point(288, 39);
            this.PnLabel.Name = "PnLabel";
            this.PnLabel.Size = new System.Drawing.Size(141, 26);
            this.PnLabel.TabIndex = 11;
            this.PnLabel.Text = "Phone Number";
            // 
            // PhnLabel
            // 
            this.PhnLabel.AutoSize = true;
            this.PhnLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhnLabel.Location = new System.Drawing.Point(65, 205);
            this.PhnLabel.Name = "PhnLabel";
            this.PhnLabel.Size = new System.Drawing.Size(50, 26);
            this.PhnLabel.TabIndex = 10;
            this.PhnLabel.Text = "PHN";
            // 
            // DobLabel
            // 
            this.DobLabel.AutoSize = true;
            this.DobLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DobLabel.Location = new System.Drawing.Point(65, 150);
            this.DobLabel.Name = "DobLabel";
            this.DobLabel.Size = new System.Drawing.Size(84, 26);
            this.DobLabel.TabIndex = 10;
            this.DobLabel.Text = "Birthday";
            // 
            // LnLabel
            // 
            this.LnLabel.AutoSize = true;
            this.LnLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LnLabel.Location = new System.Drawing.Point(65, 90);
            this.LnLabel.Name = "LnLabel";
            this.LnLabel.Size = new System.Drawing.Size(102, 26);
            this.LnLabel.TabIndex = 10;
            this.LnLabel.Text = "Last Name";
            // 
            // FnLabel
            // 
            this.FnLabel.AutoSize = true;
            this.FnLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FnLabel.Location = new System.Drawing.Point(65, 39);
            this.FnLabel.Name = "FnLabel";
            this.FnLabel.Size = new System.Drawing.Size(105, 26);
            this.FnLabel.TabIndex = 10;
            this.FnLabel.Text = "First Name";
            // 
            // ecgRecordList
            // 
            this.ecgRecordList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ecgRecordList.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ecgRecordList.FormattingEnabled = true;
            this.ecgRecordList.ItemHeight = 23;
            this.ecgRecordList.Location = new System.Drawing.Point(3, 23);
            this.ecgRecordList.Name = "ecgRecordList";
            this.ecgRecordList.Size = new System.Drawing.Size(1416, 489);
            this.ecgRecordList.TabIndex = 0;
            this.ecgRecordList.SelectedIndexChanged += new System.EventHandler(this.ecgRecordList_SelectedIndexChanged);
            // 
            // Record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.recordTableLayout);
            this.Name = "Record";
            this.Text = "Record";
            this.recordTableLayout.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.GbofSearch.ResumeLayout(false);
            this.GbofSearch.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel recordTableLayout;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox GbofSearch;
        private System.Windows.Forms.Label labOfFN;
        private System.Windows.Forms.Button btnOfSearch;
        private System.Windows.Forms.Label labOfLN;
        private System.Windows.Forms.TextBox txtOfPhn;
        private System.Windows.Forms.Label labOfDOB;
        private System.Windows.Forms.TextBox txtOfBOD;
        private System.Windows.Forms.Label labOfPhn;
        private System.Windows.Forms.TextBox txtOfLN;
        private System.Windows.Forms.TextBox txtOfFN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label AddrLabel;
        private System.Windows.Forms.Label PnLabel;
        private System.Windows.Forms.Label PhnLabel;
        private System.Windows.Forms.Label DobLabel;
        private System.Windows.Forms.Label LnLabel;
        private System.Windows.Forms.Label FnLabel;
        public System.Windows.Forms.ListBox patientList;
        private System.Windows.Forms.ListBox ecgRecordList;
    }
}