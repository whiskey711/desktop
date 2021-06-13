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
            this.deferLabel = new System.Windows.Forms.Label();
            this.appointmentLabel = new System.Windows.Forms.Label();
            this.appointEndTimePick = new System.Windows.Forms.DateTimePicker();
            this.okBtn = new System.Windows.Forms.Button();
            this.appointStartTimePick = new System.Windows.Forms.DateTimePicker();
            this.devPickTimePick = new System.Windows.Forms.DateTimePicker();
            this.devReturnTimePick = new System.Windows.Forms.DateTimePicker();
            this.deferTimePick = new System.Windows.Forms.DateTimePicker();
            this.nextAppointTimeLabel = new System.Windows.Forms.Label();
            this.appointGroup = new System.Windows.Forms.GroupBox();
            this.deviceLocCB = new System.Windows.Forms.ComboBox();
            this.deviceNameLab = new System.Windows.Forms.Label();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.deviceCombo = new System.Windows.Forms.ComboBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.continueBtn = new System.Windows.Forms.Button();
            this.generateReportBtn = new System.Windows.Forms.Button();
            this.viewNoteBtn = new System.Windows.Forms.Button();
            this.returnDevBtn = new System.Windows.Forms.Button();
            this.mailBtn = new System.Windows.Forms.Button();
            this.deferBtn = new System.Windows.Forms.Button();
            this.uploadReportBtn = new System.Windows.Forms.Button();
            this.backgroundPanel = new System.Windows.Forms.Panel();
            this.appointGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // startTimeLabel
            // 
            this.startTimeLabel.AutoSize = true;
            this.startTimeLabel.Location = new System.Drawing.Point(55, 90);
            this.startTimeLabel.Name = "startTimeLabel";
            this.startTimeLabel.Size = new System.Drawing.Size(51, 13);
            this.startTimeLabel.TabIndex = 0;
            this.startTimeLabel.Text = "Start time";
            // 
            // endTimeLabel
            // 
            this.endTimeLabel.AutoSize = true;
            this.endTimeLabel.Location = new System.Drawing.Point(55, 130);
            this.endTimeLabel.Name = "endTimeLabel";
            this.endTimeLabel.Size = new System.Drawing.Size(48, 13);
            this.endTimeLabel.TabIndex = 1;
            this.endTimeLabel.Text = "End time";
            // 
            // devPickLabel
            // 
            this.devPickLabel.AutoSize = true;
            this.devPickLabel.Location = new System.Drawing.Point(55, 170);
            this.devPickLabel.Name = "devPickLabel";
            this.devPickLabel.Size = new System.Drawing.Size(98, 13);
            this.devPickLabel.TabIndex = 2;
            this.devPickLabel.Text = "Device pickup time";
            // 
            // devReturnLabel
            // 
            this.devReturnLabel.AutoSize = true;
            this.devReturnLabel.Location = new System.Drawing.Point(55, 210);
            this.devReturnLabel.Name = "devReturnLabel";
            this.devReturnLabel.Size = new System.Drawing.Size(93, 13);
            this.devReturnLabel.TabIndex = 3;
            this.devReturnLabel.Text = "Device return time";
            // 
            // devLocLabel
            // 
            this.devLocLabel.AutoSize = true;
            this.devLocLabel.Location = new System.Drawing.Point(55, 250);
            this.devLocLabel.Name = "devLocLabel";
            this.devLocLabel.Size = new System.Drawing.Size(81, 13);
            this.devLocLabel.TabIndex = 4;
            this.devLocLabel.Text = "Device location";
            //
            // deferLabel
            //
            this.deferLabel.AutoSize = true;
            this.deferLabel.Location = new System.Drawing.Point(55, 330);
            this.deferLabel.Name = "deferLabel";
            this.deferLabel.Text = "Deferred return time";
            this.deferLabel.Visible = false;
            //
            // appointmentLabel
            //
            this.appointmentLabel.AutoSize = true;
            this.appointmentLabel.Location = new System.Drawing.Point(55, 360);
            this.appointmentLabel.Name = "appointmentLabel";
            this.appointmentLabel.Text = "The next appointment that current device is assigned to will start at";
            this.appointmentLabel.Visible = false;
            //
            // nextAppointTimeLabel
            //
            this.nextAppointTimeLabel.Location = new System.Drawing.Point(55, 380);
            this.nextAppointTimeLabel.Name = "nextAppointTimeLabel";
            this.nextAppointTimeLabel.Size = new System.Drawing.Size(200, 20);
            this.nextAppointTimeLabel.Visible = false;
            // 
            // appointEndTimePick
            // 
            this.appointEndTimePick.CustomFormat = "MM/dd/yyyy HH:mm";
            this.appointEndTimePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.appointEndTimePick.Location = new System.Drawing.Point(175, 130);
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
            this.appointStartTimePick.Location = new System.Drawing.Point(175, 90);
            this.appointStartTimePick.Name = "appointStartTimePick";
            this.appointStartTimePick.Size = new System.Drawing.Size(200, 20);
            this.appointStartTimePick.TabIndex = 12;
            // 
            // devPickTimePick
            // 
            this.devPickTimePick.CustomFormat = "MM/dd/yyyy HH:mm";
            this.devPickTimePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.devPickTimePick.Location = new System.Drawing.Point(175, 170);
            this.devPickTimePick.Name = "devPickTimePick";
            this.devPickTimePick.Size = new System.Drawing.Size(200, 20);
            this.devPickTimePick.TabIndex = 13;
            // 
            // devReturnTimePick
            // 
            this.devReturnTimePick.CustomFormat = "MM/dd/yyyy HH:mm";
            this.devReturnTimePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.devReturnTimePick.Location = new System.Drawing.Point(175, 210);
            this.devReturnTimePick.Name = "devReturnTimePick";
            this.devReturnTimePick.Size = new System.Drawing.Size(200, 20);
            this.devReturnTimePick.TabIndex = 14;
            //
            // deferTimePick
            //
            this.deferTimePick.CustomFormat = "MM/dd/yyyy HH:mm";
            this.deferTimePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.deferTimePick.Location = new System.Drawing.Point(175, 330);
            this.deferTimePick.Name = "deferTimePick";
            this.deferTimePick.Size = new System.Drawing.Size(200, 20);
            this.deferTimePick.Visible = false;
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
            this.appointGroup.Controls.Add(this.deferTimePick);
            this.appointGroup.Controls.Add(this.appointStartTimePick);
            this.appointGroup.Controls.Add(this.nextAppointTimeLabel);
            this.appointGroup.Controls.Add(this.devLocLabel);
            this.appointGroup.Controls.Add(this.appointmentLabel);
            this.appointGroup.Controls.Add(this.deferLabel);
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
            this.deviceLocCB.Location = new System.Drawing.Point(175, 250);
            this.deviceLocCB.Name = "deviceLocCB";
            this.deviceLocCB.Size = new System.Drawing.Size(200, 21);
            this.deviceLocCB.TabIndex = 20;
            // 
            // deviceNameLab
            // 
            this.deviceNameLab.AutoSize = true;
            this.deviceNameLab.Location = new System.Drawing.Point(55, 290);
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
            this.deviceCombo.Location = new System.Drawing.Point(175, 290);
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
            // generateReportBtn
            //
            this.generateReportBtn.Location = new System.Drawing.Point(170, 465);
            this.generateReportBtn.Name = "generateReportBtn";
            this.generateReportBtn.Size = new System.Drawing.Size(100, 23);
            this.generateReportBtn.TabIndex = 23;
            this.generateReportBtn.Text = "Generate Report";
            this.generateReportBtn.UseVisualStyleBackColor = true;
            this.generateReportBtn.Enabled = false;
            this.generateReportBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            //
            // viewNoteBtn
            //
            this.viewNoteBtn.Location = new System.Drawing.Point(272, 465);
            this.viewNoteBtn.Name = "viewNoteBtn";
            this.viewNoteBtn.Size = new System.Drawing.Size(75, 23);
            this.viewNoteBtn.TabIndex = 24;
            this.viewNoteBtn.Text = "Note";
            this.viewNoteBtn.UseVisualStyleBackColor = true;
            this.viewNoteBtn.Click += new System.EventHandler(this.ViewNoteBtn_Click);
            //
            // returnDevBtn
            //
            this.returnDevBtn.Location = new System.Drawing.Point(12, 490);
            this.returnDevBtn.Name = "returnDevBtn";
            this.returnDevBtn.Size = new System.Drawing.Size(100, 23);
            this.returnDevBtn.Text = "Return Device";
            this.returnDevBtn.UseVisualStyleBackColor = true;
            this.returnDevBtn.Click += new System.EventHandler(this.ReturnDevBtn_Click);
            //
            // deferBtn
            //
            this.deferBtn.Location = new System.Drawing.Point(120, 490);
            this.deferBtn.Name = "deferBtn";
            this.deferBtn.AutoSize = true;
            this.deferBtn.Text = "Defer return time";
            this.deferBtn.UseVisualStyleBackColor = true;
            this.deferBtn.Click += new System.EventHandler(this.DeferBtn_Click);
            //
            // uploadReportBtn
            //
            this.uploadReportBtn.Location = new System.Drawing.Point(225, 490);
            this.uploadReportBtn.Name = "uploadReportBtn";
            this.uploadReportBtn.AutoSize = true;
            this.uploadReportBtn.Text = "Upload Report";
            this.uploadReportBtn.UseVisualStyleBackColor = true;
            this.uploadReportBtn.Enabled = false;
            this.uploadReportBtn.Click += new System.EventHandler(this.UploadReportBtn_Click);
            //
            // mailBtn
            //
            this.mailBtn.AutoSize = true;
            this.mailBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mailBtn.Location = new System.Drawing.Point(12, 465);
            this.mailBtn.Name = "editMailBtn";
            this.mailBtn.Size = new System.Drawing.Size(154, 23);
            this.mailBtn.TabIndex = 21;
            this.mailBtn.Text = "Appoinment notification email";
            this.mailBtn.UseVisualStyleBackColor = true;
            this.mailBtn.Click += new System.EventHandler(this.EditMailBtn_Click);
            //
            // backgroundPanel
            //
            this.backgroundPanel.BackColor = System.Drawing.SystemColors.Control;
            this.backgroundPanel.Controls.Add(this.startBtn);
            this.backgroundPanel.Controls.Add(this.continueBtn);
            this.backgroundPanel.Controls.Add(this.mailBtn);
            this.backgroundPanel.Controls.Add(this.appointGroup);
            this.backgroundPanel.Controls.Add(this.generateReportBtn);
            this.backgroundPanel.Controls.Add(this.viewNoteBtn);
            this.backgroundPanel.Controls.Add(this.returnDevBtn);
            this.backgroundPanel.Controls.Add(this.deferBtn);
            this.backgroundPanel.Controls.Add(this.uploadReportBtn);
            this.backgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(436, 520);
            this.appointGroup.ResumeLayout(false);
            this.appointGroup.PerformLayout();
            // 
            // AppointmentDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(436, 520);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Controls.Add(backgroundPanel);
            this.Name = "AppointmentDetailsForm";
            this.Text = "Appointment details";
            this.Load += new System.EventHandler(this.Form_Loaded);
            this.backgroundPanel.ResumeLayout(false);
            this.backgroundPanel.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Label startTimeLabel;
        private System.Windows.Forms.Label endTimeLabel;
        private System.Windows.Forms.Label devPickLabel;
        private System.Windows.Forms.Label devReturnLabel;
        private System.Windows.Forms.Label devLocLabel;
        private System.Windows.Forms.Label deferLabel;
        private System.Windows.Forms.Label appointmentLabel;
        private System.Windows.Forms.DateTimePicker appointEndTimePick;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.DateTimePicker appointStartTimePick;
        private System.Windows.Forms.DateTimePicker devPickTimePick;
        private System.Windows.Forms.DateTimePicker devReturnTimePick;
        private System.Windows.Forms.DateTimePicker deferTimePick;
        private System.Windows.Forms.Label nextAppointTimeLabel;
        private System.Windows.Forms.GroupBox appointGroup;
        private System.Windows.Forms.ComboBox deviceCombo;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button continueBtn;
        private System.Windows.Forms.Label deviceNameLab;
        private System.Windows.Forms.ComboBox deviceLocCB;
        private System.Windows.Forms.Button generateReportBtn;
        private System.Windows.Forms.Button viewNoteBtn;
        private System.Windows.Forms.Button returnDevBtn;
        private System.Windows.Forms.Button mailBtn;
        private System.Windows.Forms.Button deferBtn;
        private System.Windows.Forms.Button uploadReportBtn;
        private System.Windows.Forms.Panel backgroundPanel;
    }
}
