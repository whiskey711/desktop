namespace Uvic_Ecg_ArbutusHolter
{
    partial class TestMonitorForm
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
            this.detailPanel = new System.Windows.Forms.Panel();
            this.birthDateTB = new System.Windows.Forms.PlaceholderTextBox();
            this.address2TB = new System.Windows.Forms.TextBox();
            this.address2Label = new System.Windows.Forms.Label();
            this.ageLabel = new System.Windows.Forms.Label();
            this.medLabel = new System.Windows.Forms.Label();
            this.superPhyLabel = new System.Windows.Forms.Label();
            this.pacemakerLabel = new System.Windows.Forms.Label();
            this.ageTB = new System.Windows.Forms.TextBox();
            this.medTB = new System.Windows.Forms.TextBox();
            this.superPhyTB = new System.Windows.Forms.TextBox();
            this.pacemakerTB = new System.Windows.Forms.TextBox();
            this.postCodeTB = new System.Windows.Forms.TextBox();
            this.mailTB = new System.Windows.Forms.TextBox();
            this.postCodeLabel = new System.Windows.Forms.Label();
            this.mailLabel = new System.Windows.Forms.Label();
            this.homeNumLabel = new System.Windows.Forms.Label();
            this.phoneNumLabel = new System.Windows.Forms.Label();
            this.homeNumTB = new System.Windows.Forms.TextBox();
            this.phoneNumTB = new System.Windows.Forms.TextBox();
            this.midNameLabel = new System.Windows.Forms.Label();
            this.midNameTB = new System.Windows.Forms.TextBox();
            this.cityTB = new System.Windows.Forms.TextBox();
            this.cityLabel = new System.Windows.Forms.Label();
            this.genderTB = new System.Windows.Forms.TextBox();
            this.phnTB = new System.Windows.Forms.TextBox();
            this.provinceTB = new System.Windows.Forms.TextBox();
            this.address1TB = new System.Windows.Forms.TextBox();
            this.firstNameTB = new System.Windows.Forms.TextBox();
            this.phnumLabel = new System.Windows.Forms.Label();
            this.genderLabel = new System.Windows.Forms.Label();
            this.provinceLabel = new System.Windows.Forms.Label();
            this.address1Label = new System.Windows.Forms.Label();
            this.birthDateLabel = new System.Windows.Forms.Label();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.lastNameTB = new System.Windows.Forms.TextBox();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.indicatorGroup = new System.Windows.Forms.GroupBox();
            this.terminateBtn = new System.Windows.Forms.Button();
            this.hookupBtn = new System.Windows.Forms.Button();
            this.startTitle = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.endTimeLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.durationTitle = new System.Windows.Forms.Label();
            this.endTimeTitile = new System.Windows.Forms.Label();
            this.indicatorLed = new Bulb.LedBulb();
            this.recordBtn = new System.Windows.Forms.Button();
            this.ecgPanel = new System.Windows.Forms.Panel();
            this.ecgGroup = new System.Windows.Forms.GroupBox();
            this.statusFlag = new System.Windows.Forms.Label();
            this.waitTimeLabel = new System.Windows.Forms.Label();
            this.channel2 = new Uvic_Ecg_EcgAnimationView.ECGAnimationView();
            this.channel1 = new Uvic_Ecg_EcgAnimationView.ECGAnimationView();
            this.chanel2Label = new System.Windows.Forms.Label();
            this.channel1Label = new System.Windows.Forms.Label();
            this.ecgStartBtn = new System.Windows.Forms.Button();
            this.remarkGroup = new System.Windows.Forms.GroupBox();
            this.saveRemarkBtn = new System.Windows.Forms.Button();
            this.remarkRichTextBox = new System.Windows.Forms.RichTextBox();
            this.nowTimer = new System.Windows.Forms.Timer(this.components);
            this.countTImer = new System.Windows.Forms.Timer(this.components);
            this.nextCalltimer = new System.Windows.Forms.Timer(this.components);
            this.waitingTimer = new System.Windows.Forms.Timer(this.components);
            this.backWorker = new System.ComponentModel.BackgroundWorker();
            this.ecgLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ecgSidePanel = new System.Windows.Forms.Panel();
            this.mainTableLayoutPanel.SuspendLayout();
            this.detailPanel.SuspendLayout();
            this.indicatorGroup.SuspendLayout();
            this.ecgPanel.SuspendLayout();
            this.ecgGroup.SuspendLayout();
            this.remarkGroup.SuspendLayout();
            this.ecgLayoutPanel.SuspendLayout();
            this.ecgSidePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 3;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.mainTableLayoutPanel.Controls.Add(this.detailPanel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.indicatorGroup, 2, 0);
            this.mainTableLayoutPanel.Controls.Add(this.ecgPanel, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.remarkGroup, 1, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 3;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(1904, 1041);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // detailPanel
            // 
            this.detailPanel.AutoScroll = true;
            this.detailPanel.Controls.Add(this.birthDateTB);
            this.detailPanel.Controls.Add(this.address2TB);
            this.detailPanel.Controls.Add(this.address2Label);
            this.detailPanel.Controls.Add(this.ageLabel);
            this.detailPanel.Controls.Add(this.medLabel);
            this.detailPanel.Controls.Add(this.superPhyLabel);
            this.detailPanel.Controls.Add(this.pacemakerLabel);
            this.detailPanel.Controls.Add(this.ageTB);
            this.detailPanel.Controls.Add(this.medTB);
            this.detailPanel.Controls.Add(this.superPhyTB);
            this.detailPanel.Controls.Add(this.pacemakerTB);
            this.detailPanel.Controls.Add(this.postCodeTB);
            this.detailPanel.Controls.Add(this.mailTB);
            this.detailPanel.Controls.Add(this.postCodeLabel);
            this.detailPanel.Controls.Add(this.mailLabel);
            this.detailPanel.Controls.Add(this.homeNumLabel);
            this.detailPanel.Controls.Add(this.phoneNumLabel);
            this.detailPanel.Controls.Add(this.homeNumTB);
            this.detailPanel.Controls.Add(this.phoneNumTB);
            this.detailPanel.Controls.Add(this.midNameLabel);
            this.detailPanel.Controls.Add(this.midNameTB);
            this.detailPanel.Controls.Add(this.cityTB);
            this.detailPanel.Controls.Add(this.cityLabel);
            this.detailPanel.Controls.Add(this.genderTB);
            this.detailPanel.Controls.Add(this.phnTB);
            this.detailPanel.Controls.Add(this.provinceTB);
            this.detailPanel.Controls.Add(this.address1TB);
            this.detailPanel.Controls.Add(this.firstNameTB);
            this.detailPanel.Controls.Add(this.phnumLabel);
            this.detailPanel.Controls.Add(this.genderLabel);
            this.detailPanel.Controls.Add(this.provinceLabel);
            this.detailPanel.Controls.Add(this.address1Label);
            this.detailPanel.Controls.Add(this.birthDateLabel);
            this.detailPanel.Controls.Add(this.firstNameLabel);
            this.detailPanel.Controls.Add(this.lastNameTB);
            this.detailPanel.Controls.Add(this.lastNameLabel);
            this.detailPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailPanel.Location = new System.Drawing.Point(3, 3);
            this.detailPanel.Name = "detailPanel";
            this.detailPanel.Size = new System.Drawing.Size(660, 254);
            this.detailPanel.TabIndex = 9;
            // 
            // birthDateTB
            // 
            this.birthDateTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.birthDateTB.Location = new System.Drawing.Point(79, 52);
            this.birthDateTB.Name = "birthDateTB";
            this.birthDateTB.PlaceholderText = "MM/DD/YYYY";
            this.birthDateTB.Size = new System.Drawing.Size(146, 23);
            this.birthDateTB.TabIndex = 42;
            // 
            // address2TB
            // 
            this.address2TB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.address2TB.Location = new System.Drawing.Point(302, 78);
            this.address2TB.Name = "address2TB";
            this.address2TB.Size = new System.Drawing.Size(353, 23);
            this.address2TB.TabIndex = 41;
            // 
            // address2Label
            // 
            this.address2Label.AutoSize = true;
            this.address2Label.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.address2Label.Location = new System.Drawing.Point(238, 84);
            this.address2Label.Name = "address2Label";
            this.address2Label.Size = new System.Drawing.Size(58, 15);
            this.address2Label.TabIndex = 40;
            this.address2Label.Text = "Address2";
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageLabel.Location = new System.Drawing.Point(436, 115);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(28, 15);
            this.ageLabel.TabIndex = 38;
            this.ageLabel.Text = "Age";
            // 
            // medLabel
            // 
            this.medLabel.AutoSize = true;
            this.medLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.medLabel.Location = new System.Drawing.Point(9, 224);
            this.medLabel.Name = "medLabel";
            this.medLabel.Size = new System.Drawing.Size(68, 15);
            this.medLabel.TabIndex = 37;
            this.medLabel.Text = "Medication";
            // 
            // superPhyLabel
            // 
            this.superPhyLabel.AutoSize = true;
            this.superPhyLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superPhyLabel.Location = new System.Drawing.Point(309, 195);
            this.superPhyLabel.Name = "superPhyLabel";
            this.superPhyLabel.Size = new System.Drawing.Size(121, 15);
            this.superPhyLabel.TabIndex = 36;
            this.superPhyLabel.Text = "Supervising Physician";
            // 
            // pacemakerLabel
            // 
            this.pacemakerLabel.AutoSize = true;
            this.pacemakerLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pacemakerLabel.Location = new System.Drawing.Point(10, 195);
            this.pacemakerLabel.Name = "pacemakerLabel";
            this.pacemakerLabel.Size = new System.Drawing.Size(67, 15);
            this.pacemakerLabel.TabIndex = 35;
            this.pacemakerLabel.Text = "Pacemaker";
            // 
            // ageTB
            // 
            this.ageTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageTB.Location = new System.Drawing.Point(466, 111);
            this.ageTB.Name = "ageTB";
            this.ageTB.Size = new System.Drawing.Size(57, 23);
            this.ageTB.TabIndex = 34;
            // 
            // medTB
            // 
            this.medTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.medTB.Location = new System.Drawing.Point(83, 221);
            this.medTB.Name = "medTB";
            this.medTB.Size = new System.Drawing.Size(568, 23);
            this.medTB.TabIndex = 33;
            // 
            // superPhyTB
            // 
            this.superPhyTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superPhyTB.Location = new System.Drawing.Point(436, 192);
            this.superPhyTB.Name = "superPhyTB";
            this.superPhyTB.Size = new System.Drawing.Size(215, 23);
            this.superPhyTB.TabIndex = 32;
            // 
            // pacemakerTB
            // 
            this.pacemakerTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pacemakerTB.Location = new System.Drawing.Point(83, 192);
            this.pacemakerTB.Name = "pacemakerTB";
            this.pacemakerTB.Size = new System.Drawing.Size(221, 23);
            this.pacemakerTB.TabIndex = 31;
            // 
            // postCodeTB
            // 
            this.postCodeTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.postCodeTB.Location = new System.Drawing.Point(592, 111);
            this.postCodeTB.Name = "postCodeTB";
            this.postCodeTB.Size = new System.Drawing.Size(59, 23);
            this.postCodeTB.TabIndex = 30;
            // 
            // mailTB
            // 
            this.mailTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailTB.Location = new System.Drawing.Point(469, 166);
            this.mailTB.Name = "mailTB";
            this.mailTB.Size = new System.Drawing.Size(182, 23);
            this.mailTB.TabIndex = 29;
            // 
            // postCodeLabel
            // 
            this.postCodeLabel.AutoSize = true;
            this.postCodeLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.postCodeLabel.Location = new System.Drawing.Point(529, 115);
            this.postCodeLabel.Name = "postCodeLabel";
            this.postCodeLabel.Size = new System.Drawing.Size(62, 15);
            this.postCodeLabel.TabIndex = 28;
            this.postCodeLabel.Text = "Post Code";
            // 
            // mailLabel
            // 
            this.mailLabel.AutoSize = true;
            this.mailLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailLabel.Location = new System.Drawing.Point(433, 169);
            this.mailLabel.Name = "mailLabel";
            this.mailLabel.Size = new System.Drawing.Size(36, 15);
            this.mailLabel.TabIndex = 27;
            this.mailLabel.Text = "Email";
            // 
            // homeNumLabel
            // 
            this.homeNumLabel.AutoSize = true;
            this.homeNumLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeNumLabel.Location = new System.Drawing.Point(12, 145);
            this.homeNumLabel.Name = "homeNumLabel";
            this.homeNumLabel.Size = new System.Drawing.Size(89, 15);
            this.homeNumLabel.TabIndex = 26;
            this.homeNumLabel.Text = "Home Number";
            // 
            // phoneNumLabel
            // 
            this.phoneNumLabel.AutoSize = true;
            this.phoneNumLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneNumLabel.Location = new System.Drawing.Point(10, 169);
            this.phoneNumLabel.Name = "phoneNumLabel";
            this.phoneNumLabel.Size = new System.Drawing.Size(91, 15);
            this.phoneNumLabel.TabIndex = 25;
            this.phoneNumLabel.Text = "Phone Number";
            // 
            // homeNumTB
            // 
            this.homeNumTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeNumTB.Location = new System.Drawing.Point(107, 137);
            this.homeNumTB.Name = "homeNumTB";
            this.homeNumTB.Size = new System.Drawing.Size(544, 23);
            this.homeNumTB.TabIndex = 24;
            // 
            // phoneNumTB
            // 
            this.phoneNumTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneNumTB.Location = new System.Drawing.Point(107, 166);
            this.phoneNumTB.Name = "phoneNumTB";
            this.phoneNumTB.Size = new System.Drawing.Size(323, 23);
            this.phoneNumTB.TabIndex = 23;
            // 
            // midNameLabel
            // 
            this.midNameLabel.AutoSize = true;
            this.midNameLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.midNameLabel.Location = new System.Drawing.Point(256, 26);
            this.midNameLabel.Name = "midNameLabel";
            this.midNameLabel.Size = new System.Drawing.Size(64, 15);
            this.midNameLabel.TabIndex = 20;
            this.midNameLabel.Text = "Mid Name";
            // 
            // midNameTB
            // 
            this.midNameTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.midNameTB.Location = new System.Drawing.Point(326, 20);
            this.midNameTB.Name = "midNameTB";
            this.midNameTB.Size = new System.Drawing.Size(143, 23);
            this.midNameTB.TabIndex = 19;
            // 
            // cityTB
            // 
            this.cityTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cityTB.Location = new System.Drawing.Point(223, 111);
            this.cityTB.Name = "cityTB";
            this.cityTB.Size = new System.Drawing.Size(117, 23);
            this.cityTB.TabIndex = 17;
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cityLabel.Location = new System.Drawing.Point(183, 115);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(34, 15);
            this.cityLabel.TabIndex = 16;
            this.cityLabel.Text = "*City";
            // 
            // genderTB
            // 
            this.genderTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genderTB.Location = new System.Drawing.Point(377, 112);
            this.genderTB.Name = "genderTB";
            this.genderTB.Size = new System.Drawing.Size(53, 23);
            this.genderTB.TabIndex = 14;
            // 
            // phnTB
            // 
            this.phnTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phnTB.Location = new System.Drawing.Point(48, 81);
            this.phnTB.Name = "phnTB";
            this.phnTB.Size = new System.Drawing.Size(184, 23);
            this.phnTB.TabIndex = 13;
            // 
            // provinceTB
            // 
            this.provinceTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.provinceTB.Location = new System.Drawing.Point(65, 111);
            this.provinceTB.Name = "provinceTB";
            this.provinceTB.Size = new System.Drawing.Size(112, 23);
            this.provinceTB.TabIndex = 12;
            // 
            // address1TB
            // 
            this.address1TB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.address1TB.Location = new System.Drawing.Point(302, 46);
            this.address1TB.Name = "address1TB";
            this.address1TB.Size = new System.Drawing.Size(353, 23);
            this.address1TB.TabIndex = 11;
            // 
            // firstNameTB
            // 
            this.firstNameTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstNameTB.Location = new System.Drawing.Point(546, 20);
            this.firstNameTB.Name = "firstNameTB";
            this.firstNameTB.Size = new System.Drawing.Size(109, 23);
            this.firstNameTB.TabIndex = 9;
            // 
            // phnumLabel
            // 
            this.phnumLabel.AutoSize = true;
            this.phnumLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phnumLabel.Location = new System.Drawing.Point(8, 84);
            this.phnumLabel.Name = "phnumLabel";
            this.phnumLabel.Size = new System.Drawing.Size(37, 15);
            this.phnumLabel.TabIndex = 8;
            this.phnumLabel.Text = "*PHN";
            // 
            // genderLabel
            // 
            this.genderLabel.AutoSize = true;
            this.genderLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genderLabel.Location = new System.Drawing.Point(346, 115);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Size = new System.Drawing.Size(32, 15);
            this.genderLabel.TabIndex = 7;
            this.genderLabel.Text = "*Sex";
            // 
            // provinceLabel
            // 
            this.provinceLabel.AutoSize = true;
            this.provinceLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.provinceLabel.Location = new System.Drawing.Point(8, 118);
            this.provinceLabel.Name = "provinceLabel";
            this.provinceLabel.Size = new System.Drawing.Size(60, 15);
            this.provinceLabel.TabIndex = 5;
            this.provinceLabel.Text = "*Province";
            // 
            // address1Label
            // 
            this.address1Label.AutoSize = true;
            this.address1Label.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.address1Label.Location = new System.Drawing.Point(232, 54);
            this.address1Label.Name = "address1Label";
            this.address1Label.Size = new System.Drawing.Size(64, 15);
            this.address1Label.TabIndex = 4;
            this.address1Label.Text = "*Address1";
            // 
            // birthDateLabel
            // 
            this.birthDateLabel.AutoSize = true;
            this.birthDateLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.birthDateLabel.Location = new System.Drawing.Point(8, 54);
            this.birthDateLabel.Name = "birthDateLabel";
            this.birthDateLabel.Size = new System.Drawing.Size(65, 15);
            this.birthDateLabel.TabIndex = 3;
            this.birthDateLabel.Text = "*Birthdate";
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstNameLabel.Location = new System.Drawing.Point(475, 26);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(73, 15);
            this.firstNameLabel.TabIndex = 2;
            this.firstNameLabel.Text = "*First Name";
            // 
            // lastNameTB
            // 
            this.lastNameTB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastNameTB.Location = new System.Drawing.Point(79, 23);
            this.lastNameTB.Name = "lastNameTB";
            this.lastNameTB.Size = new System.Drawing.Size(171, 23);
            this.lastNameTB.TabIndex = 1;
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastNameLabel.Location = new System.Drawing.Point(10, 28);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(71, 15);
            this.lastNameLabel.TabIndex = 0;
            this.lastNameLabel.Text = "*Last Name";
            // 
            // indicatorGroup
            // 
            this.indicatorGroup.Controls.Add(this.terminateBtn);
            this.indicatorGroup.Controls.Add(this.hookupBtn);
            this.indicatorGroup.Controls.Add(this.startTitle);
            this.indicatorGroup.Controls.Add(this.durationLabel);
            this.indicatorGroup.Controls.Add(this.endTimeLabel);
            this.indicatorGroup.Controls.Add(this.timeLabel);
            this.indicatorGroup.Controls.Add(this.endTimeTitile);
            this.indicatorGroup.Controls.Add(this.durationTitle);
            this.indicatorGroup.Controls.Add(this.indicatorLed);
            this.indicatorGroup.Controls.Add(this.recordBtn);
            this.indicatorGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.indicatorGroup.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.indicatorGroup.Location = new System.Drawing.Point(1335, 3);
            this.indicatorGroup.Name = "indicatorGroup";
            this.indicatorGroup.Size = new System.Drawing.Size(566, 254);
            this.indicatorGroup.TabIndex = 0;
            this.indicatorGroup.TabStop = false;
            this.indicatorGroup.Text = "Indicator";
            // 
            // terminateBtn
            // 
            this.terminateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.terminateBtn.AutoSize = true;
            this.terminateBtn.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.terminateBtn.Location = new System.Drawing.Point(453, 23);
            this.terminateBtn.Name = "terminateBtn";
            this.terminateBtn.Size = new System.Drawing.Size(107, 36);
            this.terminateBtn.TabIndex = 6;
            this.terminateBtn.Text = "Terminate";
            this.terminateBtn.UseVisualStyleBackColor = true;
            this.terminateBtn.Click += new System.EventHandler(this.TerminateBtn_Click);
            // 
            // hookupBtn
            // 
            this.hookupBtn.AutoSize = true;
            this.hookupBtn.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hookupBtn.Location = new System.Drawing.Point(6, 23);
            this.hookupBtn.Name = "hookupBtn";
            this.hookupBtn.Size = new System.Drawing.Size(89, 36);
            this.hookupBtn.TabIndex = 5;
            this.hookupBtn.Text = "Hookup";
            this.hookupBtn.UseVisualStyleBackColor = true;
            this.hookupBtn.Click += new System.EventHandler(this.HookupBtn_Click);
            // 
            // startTitle
            // 
            this.startTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startTitle.AutoSize = true;
            this.startTitle.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startTitle.Location = new System.Drawing.Point(342, 216);
            this.startTitle.Name = "startTitle";
            this.startTitle.Size = new System.Drawing.Size(75, 26);
            this.startTitle.TabIndex = 4;
            this.startTitle.Text = "Start at";
            // 
            // durationLabel
            // 
            this.durationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.durationLabel.AutoSize = true;
            this.durationLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.durationLabel.Location = new System.Drawing.Point(448, 175);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(90, 26);
            this.durationLabel.TabIndex = 3;
            this.durationLabel.Text = "00:00:00";
            //
            // endTimeLabel
            //
            this.endTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.endTimeLabel.AutoSize = true;
            this.endTimeLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endTimeLabel.Location = new System.Drawing.Point(342, 140);
            this.endTimeLabel.Name = "endTimeLabel";
            this.endTimeLabel.Size = new System.Drawing.Size();
            this.endTimeLabel.Text = "endTime";
            // 
            // timeLabel
            // 
            this.timeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(448, 216);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(54, 26);
            this.timeLabel.TabIndex = 2;
            this.timeLabel.Text = "Time";
            // 
            // durationTitle
            // 
            this.durationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.durationTitle.AutoSize = true;
            this.durationTitle.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.durationTitle.Location = new System.Drawing.Point(342, 175);
            this.durationTitle.Name = "durationTitle";
            this.durationTitle.Size = new System.Drawing.Size(87, 26);
            this.durationTitle.TabIndex = 1;
            this.durationTitle.Text = "Duration";
            //
            // endTimeTitle
            //
            this.endTimeTitile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.endTimeTitile.AutoSize = true;
            this.endTimeTitile.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endTimeTitile.Location = new System.Drawing.Point(342, 110);
            this.endTimeTitile.Name = "endTimeTitle";
            this.endTimeTitile.Size = new System.Drawing.Size();
            this.endTimeTitile.Text = "Schedule End Time";
            // 
            // indicatorLed
            // 
            this.indicatorLed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.indicatorLed.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.indicatorLed.Location = new System.Drawing.Point(119, 224);
            this.indicatorLed.Name = "indicatorLed";
            this.indicatorLed.On = false;
            this.indicatorLed.Size = new System.Drawing.Size(18, 15);
            this.indicatorLed.TabIndex = 0;
            this.indicatorLed.Text = "ledBulb1";
            // 
            // recordBtn
            // 
            this.recordBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.recordBtn.AutoSize = true;
            this.recordBtn.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recordBtn.Location = new System.Drawing.Point(6, 211);
            this.recordBtn.Name = "recordBtn";
            this.recordBtn.Size = new System.Drawing.Size(107, 36);
            this.recordBtn.TabIndex = 0;
            this.recordBtn.Text = "Recording";
            this.recordBtn.UseVisualStyleBackColor = true;
            this.recordBtn.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ecgPanel
            // 
            this.mainTableLayoutPanel.SetColumnSpan(this.ecgPanel, 3);
            this.ecgPanel.Controls.Add(this.ecgGroup);
            this.ecgPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ecgPanel.Location = new System.Drawing.Point(3, 263);
            this.ecgPanel.Name = "ecgPanel";
            this.mainTableLayoutPanel.SetRowSpan(this.ecgPanel, 2);
            this.ecgPanel.Size = new System.Drawing.Size(1898, 775);
            this.ecgPanel.TabIndex = 8;
            // 
            // ecgGroup
            // 
            this.ecgGroup.Controls.Add(this.ecgLayoutPanel);
            this.ecgGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ecgGroup.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ecgGroup.Location = new System.Drawing.Point(0, 0);
            this.ecgGroup.Name = "ecgGroup";
            this.ecgGroup.Size = new System.Drawing.Size(1898, 775);
            this.ecgGroup.TabIndex = 2;
            this.ecgGroup.TabStop = false;
            this.ecgGroup.Text = "ECG";
            // 
            // statusFlag
            // 
            this.statusFlag.AutoSize = true;
            this.statusFlag.Location = new System.Drawing.Point(17, 172);
            this.statusFlag.Name = "statusFlag";
            this.statusFlag.Size = new System.Drawing.Size(45, 19);
            this.statusFlag.TabIndex = 8;
            this.statusFlag.Text = "ready";
            // 
            // waitTimeLabel
            // 
            this.waitTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.waitTimeLabel.AutoSize = true;
            this.waitTimeLabel.Location = new System.Drawing.Point(17, 529);
            this.waitTimeLabel.Name = "waitTimeLabel";
            this.waitTimeLabel.Size = new System.Drawing.Size(69, 19);
            this.waitTimeLabel.TabIndex = 7;
            this.waitTimeLabel.Text = "waitTime";
            // 
            // channel2
            // 
            this.channel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channel2.Location = new System.Drawing.Point(385, 383);
            this.channel2.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.channel2.Name = "channel2";
            this.channel2.Size = new System.Drawing.Size(1500, 357);
            this.channel2.TabIndex = 6;
            // 
            // channel1
            // 
            this.channel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channel1.Location = new System.Drawing.Point(385, 9);
            this.channel1.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.channel1.Name = "channel1";
            this.channel1.Size = new System.Drawing.Size(1500, 356);
            this.channel1.TabIndex = 5;
            // 
            // chanel2Label
            // 
            this.chanel2Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chanel2Label.AutoSize = true;
            this.chanel2Label.Location = new System.Drawing.Point(299, 529);
            this.chanel2Label.Name = "chanel2Label";
            this.chanel2Label.Size = new System.Drawing.Size(70, 19);
            this.chanel2Label.TabIndex = 4;
            this.chanel2Label.Text = "Channel2";
            // 
            // channel1Label
            // 
            this.channel1Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.channel1Label.AutoSize = true;
            this.channel1Label.Location = new System.Drawing.Point(292, 172);
            this.channel1Label.Name = "channel1Label";
            this.channel1Label.Size = new System.Drawing.Size(74, 19);
            this.channel1Label.TabIndex = 3;
            this.channel1Label.Text = "Channel 1";
            // 
            // ecgStartBtn
            // 
            this.ecgStartBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ecgStartBtn.AutoSize = true;
            this.ecgStartBtn.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ecgStartBtn.Location = new System.Drawing.Point(5, 356);
            this.ecgStartBtn.Name = "ecgStartBtn";
            this.ecgStartBtn.Size = new System.Drawing.Size(90, 36);
            this.ecgStartBtn.TabIndex = 2;
            this.ecgStartBtn.Text = "DISPLAY";
            this.ecgStartBtn.UseVisualStyleBackColor = true;
            this.ecgStartBtn.Click += new System.EventHandler(this.EcgStartBtn_Click);
            // 
            // remarkGroup
            // 
            this.remarkGroup.Controls.Add(this.saveRemarkBtn);
            this.remarkGroup.Controls.Add(this.remarkRichTextBox);
            this.remarkGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remarkGroup.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remarkGroup.Location = new System.Drawing.Point(669, 3);
            this.remarkGroup.Name = "remarkGroup";
            this.remarkGroup.Size = new System.Drawing.Size(660, 254);
            this.remarkGroup.TabIndex = 0;
            this.remarkGroup.TabStop = false;
            this.remarkGroup.Text = "Remark";
            // 
            // saveRemarkBtn
            // 
            this.saveRemarkBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.saveRemarkBtn.AutoSize = true;
            this.saveRemarkBtn.Location = new System.Drawing.Point(298, 210);
            this.saveRemarkBtn.Name = "saveRemarkBtn";
            this.saveRemarkBtn.Size = new System.Drawing.Size(75, 29);
            this.saveRemarkBtn.TabIndex = 1;
            this.saveRemarkBtn.Text = "Save";
            this.saveRemarkBtn.UseVisualStyleBackColor = true;
            this.saveRemarkBtn.Click += new System.EventHandler(this.SaveRemarkBtn_Click);
            // 
            // remarkRichTextBox
            // 
            this.remarkRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remarkRichTextBox.Location = new System.Drawing.Point(7, 15);
            this.remarkRichTextBox.Name = "remarkRichTextBox";
            this.remarkRichTextBox.Size = new System.Drawing.Size(647, 186);
            this.remarkRichTextBox.TabIndex = 0;
            this.remarkRichTextBox.Text = "";
            // 
            // nowTimer
            // 
            this.nowTimer.Enabled = true;
            this.nowTimer.Interval = 1000;
            this.nowTimer.Tick += new System.EventHandler(this.NowTimer_Tick);
            // 
            // countTImer
            // 
            this.countTImer.Interval = 1000;
            this.countTImer.Tick += new System.EventHandler(this.CountTimer_Tick);
            // 
            // nextCalltimer
            // 
            this.nextCalltimer.Interval = 1000;
            this.nextCalltimer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // waitingTimer
            // 
            this.waitingTimer.Interval = 1000;
            this.waitingTimer.Tick += new System.EventHandler(this.WaitingTimer_Tick);
            // 
            // ecgLayoutPanel
            // 
            this.ecgLayoutPanel.ColumnCount = 2;
            this.ecgLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ecgLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.ecgLayoutPanel.Controls.Add(this.ecgSidePanel, 0, 0);
            this.ecgLayoutPanel.Controls.Add(this.channel2, 1, 1);
            this.ecgLayoutPanel.Controls.Add(this.channel1, 1, 0);
            this.ecgLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ecgLayoutPanel.Location = new System.Drawing.Point(3, 23);
            this.ecgLayoutPanel.Name = "ecgLayoutPanel";
            this.ecgLayoutPanel.RowCount = 2;
            this.ecgLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ecgLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ecgLayoutPanel.Size = new System.Drawing.Size(1892, 749);
            this.ecgLayoutPanel.TabIndex = 9;
            // 
            // ecgSidePanel
            // 
            this.ecgSidePanel.Controls.Add(this.ecgStartBtn);
            this.ecgSidePanel.Controls.Add(this.waitTimeLabel);
            this.ecgSidePanel.Controls.Add(this.statusFlag);
            this.ecgSidePanel.Controls.Add(this.channel1Label);
            this.ecgSidePanel.Controls.Add(this.chanel2Label);
            this.ecgSidePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ecgSidePanel.Location = new System.Drawing.Point(3, 3);
            this.ecgSidePanel.Name = "ecgSidePanel";
            this.ecgLayoutPanel.SetRowSpan(this.ecgSidePanel, 2);
            this.ecgSidePanel.Size = new System.Drawing.Size(372, 743);
            this.ecgSidePanel.TabIndex = 0;
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainInterface_FormClosing);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "MainInterface";
            this.Text = "Test monitoring";
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.detailPanel.ResumeLayout(false);
            this.detailPanel.PerformLayout();
            this.indicatorGroup.ResumeLayout(false);
            this.indicatorGroup.PerformLayout();
            this.ecgPanel.ResumeLayout(false);
            this.ecgGroup.ResumeLayout(false);
            this.remarkGroup.ResumeLayout(false);
            this.remarkGroup.PerformLayout();
            this.ecgLayoutPanel.ResumeLayout(false);
            this.ecgSidePanel.ResumeLayout(false);
            this.ecgSidePanel.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.GroupBox indicatorGroup;
        private System.Windows.Forms.Button recordBtn;
        private Bulb.LedBulb indicatorLed;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label durationTitle;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Timer nowTimer;
        private System.Windows.Forms.Timer countTImer;
        private System.Windows.Forms.Label startTitle;
        private System.Windows.Forms.GroupBox remarkGroup;
        private System.Windows.Forms.RichTextBox remarkRichTextBox;
        private System.Windows.Forms.Button saveRemarkBtn;
        private System.Windows.Forms.Panel ecgPanel;
        private System.Windows.Forms.GroupBox ecgGroup;
        private Uvic_Ecg_EcgAnimationView.ECGAnimationView channel2;
        private Uvic_Ecg_EcgAnimationView.ECGAnimationView channel1;
        private System.Windows.Forms.Label chanel2Label;
        private System.Windows.Forms.Label channel1Label;
        private System.Windows.Forms.Button ecgStartBtn;
        private System.Windows.Forms.Timer nextCalltimer;
        private System.Windows.Forms.Button hookupBtn;
        private System.Windows.Forms.Button terminateBtn;
        private System.Windows.Forms.Timer waitingTimer;
        private System.Windows.Forms.Label waitTimeLabel;
        private System.Windows.Forms.Label statusFlag;
        private System.ComponentModel.BackgroundWorker backWorker;
        private System.Windows.Forms.Panel detailPanel;
        private System.Windows.Forms.PlaceholderTextBox birthDateTB;
        private System.Windows.Forms.TextBox address2TB;
        private System.Windows.Forms.Label address2Label;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.Label medLabel;
        private System.Windows.Forms.Label superPhyLabel;
        private System.Windows.Forms.Label pacemakerLabel;
        private System.Windows.Forms.TextBox ageTB;
        private System.Windows.Forms.TextBox medTB;
        private System.Windows.Forms.TextBox superPhyTB;
        private System.Windows.Forms.TextBox pacemakerTB;
        private System.Windows.Forms.TextBox postCodeTB;
        private System.Windows.Forms.TextBox mailTB;
        private System.Windows.Forms.Label postCodeLabel;
        private System.Windows.Forms.Label mailLabel;
        private System.Windows.Forms.Label homeNumLabel;
        private System.Windows.Forms.Label phoneNumLabel;
        private System.Windows.Forms.TextBox homeNumTB;
        private System.Windows.Forms.TextBox phoneNumTB;
        private System.Windows.Forms.Label midNameLabel;
        private System.Windows.Forms.TextBox midNameTB;
        private System.Windows.Forms.TextBox cityTB;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.TextBox genderTB;
        private System.Windows.Forms.TextBox phnTB;
        private System.Windows.Forms.TextBox provinceTB;
        private System.Windows.Forms.TextBox address1TB;
        private System.Windows.Forms.TextBox firstNameTB;
        private System.Windows.Forms.Label phnumLabel;
        private System.Windows.Forms.Label genderLabel;
        private System.Windows.Forms.Label provinceLabel;
        private System.Windows.Forms.Label address1Label;
        private System.Windows.Forms.Label birthDateLabel;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.TextBox lastNameTB;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.TableLayoutPanel ecgLayoutPanel;
        private System.Windows.Forms.Panel ecgSidePanel;
        private System.Windows.Forms.Label endTimeTitile;
        private System.Windows.Forms.Label endTimeLabel;
    }
}