namespace ArbutusHolter
{
    partial class MainInterface
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
            this.components = new System.ComponentModel.Container();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PatientDetailsGroup = new System.Windows.Forms.GroupBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.modifyBtn = new System.Windows.Forms.Button();
            this.detailListBox = new System.Windows.Forms.ListBox();
            this.PatientLsGroup = new System.Windows.Forms.GroupBox();
            this.createBtn = new System.Windows.Forms.Button();
            this.patientListBox = new System.Windows.Forms.ListBox();
            this.reamrkFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.remarkGroup = new System.Windows.Forms.GroupBox();
            this.deviceComboBox = new System.Windows.Forms.ComboBox();
            this.remarkRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ecgFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ecgGroup = new System.Windows.Forms.GroupBox();
            this.channel2 = new Test.ECGAnimationView();
            this.channel1 = new Test.ECGAnimationView();
            this.chanel2Label = new System.Windows.Forms.Label();
            this.channel1Label = new System.Windows.Forms.Label();
            this.ecgStartBtn = new System.Windows.Forms.Button();
            this.indicatorFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.indicatorGroup = new System.Windows.Forms.GroupBox();
            this.startTitle = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.durationTitle = new System.Windows.Forms.Label();
            this.indicatorLed = new Bulb.LedBulb();
            this.startButton = new System.Windows.Forms.Button();
            this.srhGroup = new System.Windows.Forms.GroupBox();
            this.srhBtn = new System.Windows.Forms.Button();
            this.phnTextBox = new System.Windows.Forms.TextBox();
            this.birthTextBox = new System.Windows.Forms.TextBox();
            this.pLastNameTextBox = new System.Windows.Forms.TextBox();
            this.phnLabel = new System.Windows.Forms.Label();
            this.birthLabel = new System.Windows.Forms.Label();
            this.PLastNameLabel = new System.Windows.Forms.Label();
            this.nowTimer = new System.Windows.Forms.Timer(this.components);
            this.countTImer = new System.Windows.Forms.Timer(this.components);
            this.endTimeTitle = new System.Windows.Forms.Label();
            this.endTimeTxtBox = new System.Windows.Forms.TextBox();
            this.mainTableLayoutPanel.SuspendLayout();
            this.PatientDetailsGroup.SuspendLayout();
            this.PatientLsGroup.SuspendLayout();
            this.reamrkFlowLayoutPanel.SuspendLayout();
            this.remarkGroup.SuspendLayout();
            this.ecgFlowLayoutPanel.SuspendLayout();
            this.ecgGroup.SuspendLayout();
            this.indicatorFlowLayoutPanel.SuspendLayout();
            this.indicatorGroup.SuspendLayout();
            this.srhGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 3;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.mainTableLayoutPanel.Controls.Add(this.PatientDetailsGroup, 2, 0);
            this.mainTableLayoutPanel.Controls.Add(this.PatientLsGroup, 1, 0);
            this.mainTableLayoutPanel.Controls.Add(this.reamrkFlowLayoutPanel, 0, 2);
            this.mainTableLayoutPanel.Controls.Add(this.ecgFlowLayoutPanel, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.indicatorFlowLayoutPanel, 0, 2);
            this.mainTableLayoutPanel.Controls.Add(this.srhGroup, 0, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 3;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(1904, 1041);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // PatientDetailsGroup
            // 
            this.PatientDetailsGroup.Controls.Add(this.saveBtn);
            this.PatientDetailsGroup.Controls.Add(this.modifyBtn);
            this.PatientDetailsGroup.Controls.Add(this.detailListBox);
            this.PatientDetailsGroup.Location = new System.Drawing.Point(1259, 3);
            this.PatientDetailsGroup.Name = "PatientDetailsGroup";
            this.PatientDetailsGroup.Size = new System.Drawing.Size(642, 358);
            this.PatientDetailsGroup.TabIndex = 7;
            this.PatientDetailsGroup.TabStop = false;
            this.PatientDetailsGroup.Text = "PatientDetails";
            // 
            // saveBtn
            // 
            this.saveBtn.AutoSize = true;
            this.saveBtn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.Location = new System.Drawing.Point(405, 326);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 29);
            this.saveBtn.TabIndex = 3;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // modifyBtn
            // 
            this.modifyBtn.AutoSize = true;
            this.modifyBtn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyBtn.Location = new System.Drawing.Point(161, 326);
            this.modifyBtn.Name = "modifyBtn";
            this.modifyBtn.Size = new System.Drawing.Size(75, 29);
            this.modifyBtn.TabIndex = 2;
            this.modifyBtn.Text = "Modify";
            this.modifyBtn.UseVisualStyleBackColor = true;
            // 
            // detailListBox
            // 
            this.detailListBox.FormattingEnabled = true;
            this.detailListBox.Location = new System.Drawing.Point(3, 19);
            this.detailListBox.Name = "detailListBox";
            this.detailListBox.Size = new System.Drawing.Size(633, 303);
            this.detailListBox.TabIndex = 1;
            // 
            // PatientLsGroup
            // 
            this.PatientLsGroup.Controls.Add(this.createBtn);
            this.PatientLsGroup.Controls.Add(this.patientListBox);
            this.PatientLsGroup.Location = new System.Drawing.Point(631, 3);
            this.PatientLsGroup.Name = "PatientLsGroup";
            this.PatientLsGroup.Size = new System.Drawing.Size(622, 358);
            this.PatientLsGroup.TabIndex = 6;
            this.PatientLsGroup.TabStop = false;
            this.PatientLsGroup.Text = "Patients";
            // 
            // createBtn
            // 
            this.createBtn.AutoSize = true;
            this.createBtn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createBtn.Location = new System.Drawing.Point(271, 326);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(75, 29);
            this.createBtn.TabIndex = 1;
            this.createBtn.Text = "Create";
            this.createBtn.UseVisualStyleBackColor = true;
            // 
            // patientListBox
            // 
            this.patientListBox.FormattingEnabled = true;
            this.patientListBox.Location = new System.Drawing.Point(0, 19);
            this.patientListBox.Name = "patientListBox";
            this.patientListBox.Size = new System.Drawing.Size(616, 303);
            this.patientListBox.TabIndex = 0;
            // 
            // reamrkFlowLayoutPanel
            // 
            this.reamrkFlowLayoutPanel.Controls.Add(this.remarkGroup);
            this.reamrkFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reamrkFlowLayoutPanel.Location = new System.Drawing.Point(3, 887);
            this.reamrkFlowLayoutPanel.Name = "reamrkFlowLayoutPanel";
            this.reamrkFlowLayoutPanel.Size = new System.Drawing.Size(622, 151);
            this.reamrkFlowLayoutPanel.TabIndex = 4;
            // 
            // remarkGroup
            // 
            this.remarkGroup.Controls.Add(this.deviceComboBox);
            this.remarkGroup.Controls.Add(this.remarkRichTextBox);
            this.remarkGroup.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remarkGroup.Location = new System.Drawing.Point(3, 3);
            this.remarkGroup.Name = "remarkGroup";
            this.remarkGroup.Size = new System.Drawing.Size(1270, 148);
            this.remarkGroup.TabIndex = 0;
            this.remarkGroup.TabStop = false;
            this.remarkGroup.Text = "Remark";
            // 
            // deviceComboBox
            // 
            this.deviceComboBox.FormattingEnabled = true;
            this.deviceComboBox.Items.AddRange(new object[] {
            "Device A",
            "Device B",
            "Device C"});
            this.deviceComboBox.Location = new System.Drawing.Point(0, 121);
            this.deviceComboBox.Name = "deviceComboBox";
            this.deviceComboBox.Size = new System.Drawing.Size(1264, 27);
            this.deviceComboBox.TabIndex = 1;
            this.deviceComboBox.Text = "Choose a device";
            // 
            // remarkRichTextBox
            // 
            this.remarkRichTextBox.Location = new System.Drawing.Point(3, 19);
            this.remarkRichTextBox.Name = "remarkRichTextBox";
            this.remarkRichTextBox.Size = new System.Drawing.Size(1261, 96);
            this.remarkRichTextBox.TabIndex = 0;
            this.remarkRichTextBox.Text = "";
            // 
            // ecgFlowLayoutPanel
            // 
            this.mainTableLayoutPanel.SetColumnSpan(this.ecgFlowLayoutPanel, 3);
            this.ecgFlowLayoutPanel.Controls.Add(this.ecgGroup);
            this.ecgFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ecgFlowLayoutPanel.Location = new System.Drawing.Point(3, 367);
            this.ecgFlowLayoutPanel.Name = "ecgFlowLayoutPanel";
            this.ecgFlowLayoutPanel.Size = new System.Drawing.Size(1898, 514);
            this.ecgFlowLayoutPanel.TabIndex = 2;
            // 
            // ecgGroup
            // 
            this.ecgGroup.Controls.Add(this.channel2);
            this.ecgGroup.Controls.Add(this.channel1);
            this.ecgGroup.Controls.Add(this.chanel2Label);
            this.ecgGroup.Controls.Add(this.channel1Label);
            this.ecgGroup.Controls.Add(this.ecgStartBtn);
            this.ecgGroup.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ecgGroup.Location = new System.Drawing.Point(3, 3);
            this.ecgGroup.Name = "ecgGroup";
            this.ecgGroup.Size = new System.Drawing.Size(1895, 511);
            this.ecgGroup.TabIndex = 0;
            this.ecgGroup.TabStop = false;
            this.ecgGroup.Text = "ECG";
            // 
            // channel2
            // 
            this.channel2.Location = new System.Drawing.Point(87, 235);
            this.channel2.Margin = new System.Windows.Forms.Padding(4);
            this.channel2.Name = "channel2";
            this.channel2.Size = new System.Drawing.Size(1801, 200);
            this.channel2.TabIndex = 6;
            // 
            // channel1
            // 
            this.channel1.Location = new System.Drawing.Point(87, 26);
            this.channel1.Margin = new System.Windows.Forms.Padding(4);
            this.channel1.Name = "channel1";
            this.channel1.Size = new System.Drawing.Size(1801, 200);
            this.channel1.TabIndex = 5;
            // 
            // chanel2Label
            // 
            this.chanel2Label.AutoSize = true;
            this.chanel2Label.Location = new System.Drawing.Point(6, 317);
            this.chanel2Label.Name = "chanel2Label";
            this.chanel2Label.Size = new System.Drawing.Size(70, 19);
            this.chanel2Label.TabIndex = 4;
            this.chanel2Label.Text = "Channel2";
            // 
            // channel1Label
            // 
            this.channel1Label.AutoSize = true;
            this.channel1Label.Location = new System.Drawing.Point(6, 113);
            this.channel1Label.Name = "channel1Label";
            this.channel1Label.Size = new System.Drawing.Size(74, 19);
            this.channel1Label.TabIndex = 3;
            this.channel1Label.Text = "Channel 1";
            // 
            // ecgStartBtn
            // 
            this.ecgStartBtn.AutoSize = true;
            this.ecgStartBtn.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ecgStartBtn.Location = new System.Drawing.Point(870, 456);
            this.ecgStartBtn.Name = "ecgStartBtn";
            this.ecgStartBtn.Size = new System.Drawing.Size(90, 36);
            this.ecgStartBtn.TabIndex = 2;
            this.ecgStartBtn.Text = "DISPLAY";
            this.ecgStartBtn.UseVisualStyleBackColor = true;
            this.ecgStartBtn.Click += new System.EventHandler(this.ecgStartBtn_Click);
            // 
            // indicatorFlowLayoutPanel
            // 
            this.mainTableLayoutPanel.SetColumnSpan(this.indicatorFlowLayoutPanel, 2);
            this.indicatorFlowLayoutPanel.Controls.Add(this.indicatorGroup);
            this.indicatorFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.indicatorFlowLayoutPanel.Location = new System.Drawing.Point(631, 887);
            this.indicatorFlowLayoutPanel.Name = "indicatorFlowLayoutPanel";
            this.indicatorFlowLayoutPanel.Size = new System.Drawing.Size(1270, 151);
            this.indicatorFlowLayoutPanel.TabIndex = 3;
            // 
            // indicatorGroup
            // 
            this.indicatorGroup.Controls.Add(this.endTimeTxtBox);
            this.indicatorGroup.Controls.Add(this.endTimeTitle);
            this.indicatorGroup.Controls.Add(this.startTitle);
            this.indicatorGroup.Controls.Add(this.durationLabel);
            this.indicatorGroup.Controls.Add(this.timeLabel);
            this.indicatorGroup.Controls.Add(this.durationTitle);
            this.indicatorGroup.Controls.Add(this.indicatorLed);
            this.indicatorGroup.Controls.Add(this.startButton);
            this.indicatorGroup.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.indicatorGroup.Location = new System.Drawing.Point(3, 3);
            this.indicatorGroup.Name = "indicatorGroup";
            this.indicatorGroup.Size = new System.Drawing.Size(1270, 148);
            this.indicatorGroup.TabIndex = 0;
            this.indicatorGroup.TabStop = false;
            this.indicatorGroup.Text = "Indicator";
            // 
            // startTitle
            // 
            this.startTitle.AutoSize = true;
            this.startTitle.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startTitle.Location = new System.Drawing.Point(288, 71);
            this.startTitle.Name = "startTitle";
            this.startTitle.Size = new System.Drawing.Size(75, 26);
            this.startTitle.TabIndex = 4;
            this.startTitle.Text = "Start at";
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.durationLabel.Location = new System.Drawing.Point(711, 71);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(90, 26);
            this.durationLabel.TabIndex = 3;
            this.durationLabel.Text = "00:00:00";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(421, 71);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(54, 26);
            this.timeLabel.TabIndex = 2;
            this.timeLabel.Text = "Time";
            // 
            // durationTitle
            // 
            this.durationTitle.AutoSize = true;
            this.durationTitle.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.durationTitle.Location = new System.Drawing.Point(549, 71);
            this.durationTitle.Name = "durationTitle";
            this.durationTitle.Size = new System.Drawing.Size(87, 26);
            this.durationTitle.TabIndex = 1;
            this.durationTitle.Text = "Duration";
            // 
            // indicatorLed
            // 
            this.indicatorLed.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.indicatorLed.Location = new System.Drawing.Point(214, 76);
            this.indicatorLed.Name = "indicatorLed";
            this.indicatorLed.On = false;
            this.indicatorLed.Size = new System.Drawing.Size(18, 15);
            this.indicatorLed.TabIndex = 0;
            this.indicatorLed.Text = "ledBulb1";
            // 
            // startButton
            // 
            this.startButton.AutoSize = true;
            this.startButton.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(32, 53);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(124, 62);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "START\r\nRECORDING";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // srhGroup
            // 
            this.srhGroup.Controls.Add(this.srhBtn);
            this.srhGroup.Controls.Add(this.phnTextBox);
            this.srhGroup.Controls.Add(this.birthTextBox);
            this.srhGroup.Controls.Add(this.pLastNameTextBox);
            this.srhGroup.Controls.Add(this.phnLabel);
            this.srhGroup.Controls.Add(this.birthLabel);
            this.srhGroup.Controls.Add(this.PLastNameLabel);
            this.srhGroup.Location = new System.Drawing.Point(3, 3);
            this.srhGroup.Name = "srhGroup";
            this.srhGroup.Size = new System.Drawing.Size(622, 358);
            this.srhGroup.TabIndex = 5;
            this.srhGroup.TabStop = false;
            this.srhGroup.Text = "Search";
            // 
            // srhBtn
            // 
            this.srhBtn.AutoSize = true;
            this.srhBtn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.srhBtn.Location = new System.Drawing.Point(223, 293);
            this.srhBtn.Name = "srhBtn";
            this.srhBtn.Size = new System.Drawing.Size(75, 29);
            this.srhBtn.TabIndex = 6;
            this.srhBtn.Text = "Search";
            this.srhBtn.UseVisualStyleBackColor = true;
            // 
            // phnTextBox
            // 
            this.phnTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phnTextBox.Location = new System.Drawing.Point(223, 209);
            this.phnTextBox.Name = "phnTextBox";
            this.phnTextBox.Size = new System.Drawing.Size(274, 27);
            this.phnTextBox.TabIndex = 5;
            // 
            // birthTextBox
            // 
            this.birthTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.birthTextBox.Location = new System.Drawing.Point(223, 140);
            this.birthTextBox.Name = "birthTextBox";
            this.birthTextBox.Size = new System.Drawing.Size(274, 27);
            this.birthTextBox.TabIndex = 4;
            // 
            // pLastNameTextBox
            // 
            this.pLastNameTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pLastNameTextBox.Location = new System.Drawing.Point(223, 71);
            this.pLastNameTextBox.Name = "pLastNameTextBox";
            this.pLastNameTextBox.Size = new System.Drawing.Size(274, 27);
            this.pLastNameTextBox.TabIndex = 3;
            // 
            // phnLabel
            // 
            this.phnLabel.AutoSize = true;
            this.phnLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phnLabel.Location = new System.Drawing.Point(55, 212);
            this.phnLabel.Name = "phnLabel";
            this.phnLabel.Size = new System.Drawing.Size(37, 19);
            this.phnLabel.TabIndex = 2;
            this.phnLabel.Text = "PHN";
            // 
            // birthLabel
            // 
            this.birthLabel.AutoSize = true;
            this.birthLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.birthLabel.Location = new System.Drawing.Point(55, 143);
            this.birthLabel.Name = "birthLabel";
            this.birthLabel.Size = new System.Drawing.Size(75, 19);
            this.birthLabel.TabIndex = 1;
            this.birthLabel.Text = "Birth Date";
            // 
            // PLastNameLabel
            // 
            this.PLastNameLabel.AutoSize = true;
            this.PLastNameLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PLastNameLabel.Location = new System.Drawing.Point(55, 74);
            this.PLastNameLabel.Name = "PLastNameLabel";
            this.PLastNameLabel.Size = new System.Drawing.Size(128, 19);
            this.PLastNameLabel.TabIndex = 0;
            this.PLastNameLabel.Text = "Patient Last Name";
            // 
            // nowTimer
            // 
            this.nowTimer.Enabled = true;
            this.nowTimer.Interval = 1000;
            this.nowTimer.Tick += new System.EventHandler(this.nowTimer_Tick);
            // 
            // countTImer
            // 
            this.countTImer.Interval = 1000;
            this.countTImer.Tick += new System.EventHandler(this.startButton_Click);
            // 
            // endTimeTitle
            // 
            this.endTimeTitle.AutoSize = true;
            this.endTimeTitle.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endTimeTitle.Location = new System.Drawing.Point(895, 71);
            this.endTimeTitle.Name = "endTimeTitle";
            this.endTimeTitle.Size = new System.Drawing.Size(83, 26);
            this.endTimeTitle.TabIndex = 5;
            this.endTimeTitle.Text = "Endtime";
            // 
            // endTimeTxtBox
            // 
            this.endTimeTxtBox.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endTimeTxtBox.Location = new System.Drawing.Point(1063, 73);
            this.endTimeTxtBox.Name = "endTimeTxtBox";
            this.endTimeTxtBox.Size = new System.Drawing.Size(100, 33);
            this.endTimeTxtBox.TabIndex = 6;
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "MainInterface";
            this.Text = "MainInterface";
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.PatientDetailsGroup.ResumeLayout(false);
            this.PatientDetailsGroup.PerformLayout();
            this.PatientLsGroup.ResumeLayout(false);
            this.PatientLsGroup.PerformLayout();
            this.reamrkFlowLayoutPanel.ResumeLayout(false);
            this.remarkGroup.ResumeLayout(false);
            this.ecgFlowLayoutPanel.ResumeLayout(false);
            this.ecgGroup.ResumeLayout(false);
            this.ecgGroup.PerformLayout();
            this.indicatorFlowLayoutPanel.ResumeLayout(false);
            this.indicatorGroup.ResumeLayout(false);
            this.indicatorGroup.PerformLayout();
            this.srhGroup.ResumeLayout(false);
            this.srhGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel ecgFlowLayoutPanel;
        private System.Windows.Forms.GroupBox ecgGroup;
        private System.Windows.Forms.FlowLayoutPanel indicatorFlowLayoutPanel;
        private System.Windows.Forms.GroupBox indicatorGroup;
        private System.Windows.Forms.Button startButton;
        private Bulb.LedBulb indicatorLed;
        private System.Windows.Forms.Button ecgStartBtn;
        private System.Windows.Forms.Label chanel2Label;
        private System.Windows.Forms.Label channel1Label;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label durationTitle;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Timer nowTimer;
        private System.Windows.Forms.Timer countTImer;
        private Test.ECGAnimationView channel2;
        private Test.ECGAnimationView channel1;
        private System.Windows.Forms.Label startTitle;
        private System.Windows.Forms.FlowLayoutPanel reamrkFlowLayoutPanel;
        private System.Windows.Forms.GroupBox remarkGroup;
        private System.Windows.Forms.ComboBox deviceComboBox;
        private System.Windows.Forms.RichTextBox remarkRichTextBox;
        private System.Windows.Forms.GroupBox PatientDetailsGroup;
        private System.Windows.Forms.ListBox detailListBox;
        private System.Windows.Forms.GroupBox PatientLsGroup;
        private System.Windows.Forms.ListBox patientListBox;
        private System.Windows.Forms.GroupBox srhGroup;
        private System.Windows.Forms.Button srhBtn;
        private System.Windows.Forms.TextBox phnTextBox;
        private System.Windows.Forms.TextBox birthTextBox;
        private System.Windows.Forms.TextBox pLastNameTextBox;
        private System.Windows.Forms.Label phnLabel;
        private System.Windows.Forms.Label birthLabel;
        private System.Windows.Forms.Label PLastNameLabel;
        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button modifyBtn;
        private System.Windows.Forms.TextBox endTimeTxtBox;
        private System.Windows.Forms.Label endTimeTitle;
    }
}