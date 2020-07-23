using System.Windows.Forms.VisualStyles;

namespace Uvic_Ecg_ArbutusHolter
{
    partial class AppointmentDetailsForm
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
            this.startTimeLabel = new System.Windows.Forms.Label();
            this.endTimeLabel = new System.Windows.Forms.Label();
            this.devPickLabel = new System.Windows.Forms.Label();
            this.devReturnLabel = new System.Windows.Forms.Label();
            this.devLocLabel = new System.Windows.Forms.Label();
            this.appointEndTimePick = new System.Windows.Forms.DateTimePicker();
            this.okBtn = new System.Windows.Forms.Button();
            this.appointStartTimePick = new System.Windows.Forms.DateTimePicker();
            this.devPickTimePick = new System.Windows.Forms.DateTimePicker();
            this.devReturnTimePick = new System.Windows.Forms.DateTimePicker();
            this.appointGroup = new System.Windows.Forms.GroupBox();
            this.deviceLocCB = new System.Windows.Forms.ComboBox();
            this.deviceNameLab = new System.Windows.Forms.Label();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.deviceCombo = new System.Windows.Forms.ComboBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.continueBtn = new System.Windows.Forms.Button();
            this.editMailBtn = new System.Windows.Forms.Button();
            this.generateReportBtn = new System.Windows.Forms.Button();
            this.appointGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // startTimeLabel
            // 
            this.startTimeLabel.AutoSize = true;
            this.startTimeLabel.Location = new System.Drawing.Point(56, 113);
            this.startTimeLabel.Name = "startTimeLabel";
            this.startTimeLabel.Size = new System.Drawing.Size(51, 13);
            this.startTimeLabel.TabIndex = 0;
            this.startTimeLabel.Text = "Start time";
            // 
            // endTimeLabel
            // 
            this.endTimeLabel.AutoSize = true;
            this.endTimeLabel.Location = new System.Drawing.Point(56, 165);
            this.endTimeLabel.Name = "endTimeLabel";
            this.endTimeLabel.Size = new System.Drawing.Size(48, 13);
            this.endTimeLabel.TabIndex = 1;
            this.endTimeLabel.Text = "End time";
            // 
            // devPickLabel
            // 
            this.devPickLabel.AutoSize = true;
            this.devPickLabel.Location = new System.Drawing.Point(56, 212);
            this.devPickLabel.Name = "devPickLabel";
            this.devPickLabel.Size = new System.Drawing.Size(98, 13);
            this.devPickLabel.TabIndex = 2;
            this.devPickLabel.Text = "Device pickup time";
            // 
            // devReturnLabel
            // 
            this.devReturnLabel.AutoSize = true;
            this.devReturnLabel.Location = new System.Drawing.Point(56, 274);
            this.devReturnLabel.Name = "devReturnLabel";
            this.devReturnLabel.Size = new System.Drawing.Size(93, 13);
            this.devReturnLabel.TabIndex = 3;
            this.devReturnLabel.Text = "Device return time";
            // 
            // devLocLabel
            // 
            this.devLocLabel.AutoSize = true;
            this.devLocLabel.Location = new System.Drawing.Point(56, 327);
            this.devLocLabel.Name = "devLocLabel";
            this.devLocLabel.Size = new System.Drawing.Size(81, 13);
            this.devLocLabel.TabIndex = 4;
            this.devLocLabel.Text = "Device location";
            // 
            // appointEndTimePick
            // 
            this.appointEndTimePick.CustomFormat = "MM/dd/yyyy HH:mm";
            this.appointEndTimePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.appointEndTimePick.Location = new System.Drawing.Point(174, 159);
            this.appointEndTimePick.Name = "appointEndTimePick";
            this.appointEndTimePick.Size = new System.Drawing.Size(200, 20);
            this.appointEndTimePick.TabIndex = 10;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(165, 418);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 11;
            this.okBtn.Text = "Save";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // appointStartTimePick
            // 
            this.appointStartTimePick.CustomFormat = "MM/dd/yyyy HH:mm";
            this.appointStartTimePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.appointStartTimePick.Location = new System.Drawing.Point(175, 107);
            this.appointStartTimePick.Name = "appointStartTimePick";
            this.appointStartTimePick.Size = new System.Drawing.Size(200, 20);
            this.appointStartTimePick.TabIndex = 12;
            // 
            // devPickTimePick
            // 
            this.devPickTimePick.CustomFormat = "MM/dd/yyyy HH:mm";
            this.devPickTimePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.devPickTimePick.Location = new System.Drawing.Point(174, 206);
            this.devPickTimePick.Name = "devPickTimePick";
            this.devPickTimePick.Size = new System.Drawing.Size(200, 20);
            this.devPickTimePick.TabIndex = 13;
            // 
            // devReturnTimePick
            // 
            this.devReturnTimePick.CustomFormat = "MM/dd/yyyy HH:mm";
            this.devReturnTimePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.devReturnTimePick.Location = new System.Drawing.Point(175, 268);
            this.devReturnTimePick.Name = "devReturnTimePick";
            this.devReturnTimePick.Size = new System.Drawing.Size(200, 20);
            this.devReturnTimePick.TabIndex = 14;
            // 
            // appointGroup
            // 
            this.appointGroup.Controls.Add(this.deviceLocCB);
            this.appointGroup.Controls.Add(this.deviceNameLab);
            this.appointGroup.Controls.Add(this.lastNameLabel);
            this.appointGroup.Controls.Add(this.firstNameLabel);
            this.appointGroup.Controls.Add(this.okBtn);
            this.appointGroup.Controls.Add(this.deviceCombo);
            this.appointGroup.Controls.Add(this.startTimeLabel);
            this.appointGroup.Controls.Add(this.devReturnTimePick);
            this.appointGroup.Controls.Add(this.appointStartTimePick);
            this.appointGroup.Controls.Add(this.devLocLabel);
            this.appointGroup.Controls.Add(this.devPickTimePick);
            this.appointGroup.Controls.Add(this.endTimeLabel);
            this.appointGroup.Controls.Add(this.appointEndTimePick);
            this.appointGroup.Controls.Add(this.devReturnLabel);
            this.appointGroup.Controls.Add(this.devPickLabel);
            this.appointGroup.Location = new System.Drawing.Point(12, 12);
            this.appointGroup.Name = "appointGroup";
            this.appointGroup.Size = new System.Drawing.Size(412, 447);
            this.appointGroup.TabIndex = 15;
            this.appointGroup.TabStop = false;
            this.appointGroup.Text = "Appointment Detail";
            // 
            // deviceLocCB
            // 
            this.deviceLocCB.FormattingEnabled = true;
            this.deviceLocCB.Location = new System.Drawing.Point(175, 324);
            this.deviceLocCB.Name = "deviceLocCB";
            this.deviceLocCB.Size = new System.Drawing.Size(200, 21);
            this.deviceLocCB.TabIndex = 20;
            // 
            // deviceNameLab
            // 
            this.deviceNameLab.AutoSize = true;
            this.deviceNameLab.Location = new System.Drawing.Point(56, 372);
            this.deviceNameLab.Name = "deviceNameLab";
            this.deviceNameLab.Size = new System.Drawing.Size(70, 13);
            this.deviceNameLab.TabIndex = 19;
            this.deviceNameLab.Text = "Device name";
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(69, 43);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(35, 13);
            this.lastNameLabel.TabIndex = 18;
            this.lastNameLabel.Text = "label2";
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Location = new System.Drawing.Point(18, 43);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(35, 13);
            this.firstNameLabel.TabIndex = 17;
            this.firstNameLabel.Text = "label1";
            // 
            // deviceCombo
            // 
            this.deviceCombo.FormattingEnabled = true;
            this.deviceCombo.Location = new System.Drawing.Point(174, 369);
            this.deviceCombo.Name = "deviceCombo";
            this.deviceCombo.Size = new System.Drawing.Size(201, 21);
            this.deviceCombo.TabIndex = 16;
            this.deviceCombo.Text = "Select a device";
            this.deviceCombo.SelectedIndexChanged += new System.EventHandler(this.DeviceCombo_SelectedIndexChanged);
            this.deviceCombo.Click += new System.EventHandler(this.DeviceCombo_Click);
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(349, 465);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 16;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartBtn_Click);
            //
            // continueBtn
            //
            this.continueBtn.Location = new System.Drawing.Point(349, 465);
            this.continueBtn.Name = "continueBtn";
            this.continueBtn.Size = new System.Drawing.Size(75, 23);
            this.continueBtn.TabIndex = 22;
            this.continueBtn.Text = "Continue";
            this.continueBtn.UseVisualStyleBackColor = true;
            this.continueBtn.Click += new System.EventHandler(this.ContinueBtn_Click);
            // 
            // editMailBtn
            // 
            this.editMailBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.editMailBtn.AutoSize = true;
            this.editMailBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editMailBtn.Location = new System.Drawing.Point(12, 465);
            this.editMailBtn.Name = "editMailBtn";
            this.editMailBtn.Size = new System.Drawing.Size(154, 23);
            this.editMailBtn.TabIndex = 21;
            this.editMailBtn.Text = "Appoinment notification email";
            this.editMailBtn.UseVisualStyleBackColor = true;
            this.editMailBtn.Click += new System.EventHandler(this.EditMailBtn_Click);
            //
            // generateReportBtn
            //
            this.generateReportBtn.Location = new System.Drawing.Point(200, 465);
            this.generateReportBtn.Name = "generateReportBtn";
            this.generateReportBtn.Size = new System.Drawing.Size(100, 23);
            this.generateReportBtn.TabIndex = 23;
            this.generateReportBtn.Text = "Generate Report";
            this.generateReportBtn.UseVisualStyleBackColor = true;
            this.generateReportBtn.Enabled = false;
            // 
            // AppointmentDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(436, 500);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.continueBtn);
            this.Controls.Add(this.appointGroup);
            this.Controls.Add(this.editMailBtn);
            this.Controls.Add(this.generateReportBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppointmentDetailsForm";
            this.Text = "Appointment details";
            this.appointGroup.ResumeLayout(false);
            this.appointGroup.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Label startTimeLabel;
        private System.Windows.Forms.Label endTimeLabel;
        private System.Windows.Forms.Label devPickLabel;
        private System.Windows.Forms.Label devReturnLabel;
        private System.Windows.Forms.Label devLocLabel;
        private System.Windows.Forms.DateTimePicker appointEndTimePick;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.DateTimePicker appointStartTimePick;
        private System.Windows.Forms.DateTimePicker devPickTimePick;
        private System.Windows.Forms.DateTimePicker devReturnTimePick;
        private System.Windows.Forms.GroupBox appointGroup;
        private System.Windows.Forms.ComboBox deviceCombo;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button continueBtn;
        private System.Windows.Forms.Label deviceNameLab;
        private System.Windows.Forms.ComboBox deviceLocCB;
        private System.Windows.Forms.Button editMailBtn;
        private System.Windows.Forms.Button generateReportBtn;
    }
}