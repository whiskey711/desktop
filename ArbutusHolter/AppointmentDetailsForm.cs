﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class AppointmentDetailsForm : Form
    {
        RestModel<Device> restModel;
        private DeviceResource dResource = new DeviceResource();
        private Client inClient;
        List<Device> returnDls;
        string[] nameLs;
        Appointment theApp;
        public Device selectDev { get; set; }
        public string deviceLoc { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public DateTime pickTime { get; set; }
        public DateTime returnTime { get; set; }
        public AppointmentDetailsForm(Client client, Appointment app, string name)
        {
            try
            {
                InitializeComponent();
                inClient = client;
                theApp = app;
                nameLs = name.Split(' ');
                if (app != null)
                {
                    appointStartTimePick.Value = app.AppointmentStartTime.Value;
                    appointEndTimePick.Value = app.AppointmentEndTime.Value;
                    devPickTimePick.Value = app.PickupDate.Value;
                    devReturnTimePick.Value = app.DeviceReturnDate.Value;
                    devLocTB.Text = app.DeviceLocation;
                    firstNameLabel.Text = app.FirstName;
                    lastNameLabel.Text = app.LastName;
                    restModel = dResource.GetAllDevice(inClient);
                    if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                    {
                        returnDls = CreateDevLs(restModel.Feed.Entities);
                        foreach (var returnD in returnDls)
                        {
                            if (returnD.DeviceId == app.DeviceId)
                            {
                                selectDev = returnD;
                                deviceCombo.Text = returnD.DeviceName;
                                break;
                            }
                        }
                    }
                    if (DateTime.Compare(app.AppointmentEndTime.Value, DateTime.Now) <= 0)
                    {
                        appointGroup.Enabled = false;
                        startBtn.Visible = false;
                    }
                }
                else
                {
                    firstNameLabel.Text = nameLs[0];
                    lastNameLabel.Text = nameLs[1];
                    startBtn.Visible = false;
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void OkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                deviceLoc = devLocTB.Text;
                startTime = appointStartTimePick.Value;
                endTime = appointEndTimePick.Value;
                pickTime = devPickTimePick.Value;
                returnTime = devReturnTimePick.Value;
                if (string.IsNullOrWhiteSpace(deviceLoc))
                {
                    MessageBox.Show(ErrorInfo.DeviceLoc.ErrorMessage);
                    return;
                }
                if (DateTime.Compare(appointStartTimePick.Value, devPickTimePick.Value) < 0)
                {
                    MessageBox.Show(ErrorInfo.Before.ErrorMessage);
                    return;
                }
                if (DateTime.Compare(appointEndTimePick.Value, devReturnTimePick.Value) > 0)
                {
                    MessageBox.Show(ErrorInfo.Later.ErrorMessage);
                    return;
                }
                if (selectDev == null)
                {
                    MessageBox.Show(ErrorInfo.SelectDeivce.ErrorMessage);
                    return;
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void DeviceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemName = deviceCombo.SelectedItem.ToString();
            foreach (var item in returnDls)
            {
                if (item.DeviceName == itemName)
                {
                    selectDev = item;
                    break;
                }
            }
        }
        private void DeviceCombo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(devLocTB.Text))
                {
                    MessageBox.Show(ErrorInfo.DeviceLoc.ErrorMessage);
                    return;
                }
                restModel = dResource.GetAvailableDevices(inClient, devPickTimePick.Value, devReturnTimePick.Value, devLocTB.Text);
                if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    deviceCombo.Items.Clear();
                    returnDls = CreateDevLs(restModel.Feed.Entities);
                    foreach (var returnD in returnDls)
                    {
                        if (devLocTB.Text.Equals(returnD.DeviceLocation))
                        {
                            deviceCombo.Items.Add(returnD.DeviceName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private List<Device> CreateDevLs(List<Entity<Device>> entls)
        {
            List<Device> devls = new List<Device>();
            foreach (var ent in entls)
            {
                devls.Add(ent.Model);
            }
            return devls;
        }
        private void StartBtn_Click(object sender, EventArgs e)
        {
            MainInterface mainForm = new MainInterface(inClient, theApp);
            mainForm.Show();
        }
    }
}