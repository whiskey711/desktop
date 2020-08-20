using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class AppointmentDetailsForm : Form
    {
        RestModel<Device> restModel;
        private DeviceResource dResource = new DeviceResource();
        private NurseResource nResource = new NurseResource();
        private Client inClient;
        List<Device> returnDls;
        string[] nameLs;
        string errorMsg;
        public Device selectDev { get; set; }
        public string deviceLoc { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public DateTime pickTime { get; set; }
        public DateTime returnTime { get; set; }
        public EcgTest theTest { get; set; }
        public Appointment theAppoint { get; set; }
        private PatientInfo thePat;
        public AppointmentDetailsForm(Client client, Appointment app, EcgTest runningTest, PatientInfo patient)
        {
            try
            {
                InitializeComponent();
                inClient = client;
                theAppoint = app;
                theTest = runningTest;
                thePat = patient;
                continueBtn.Visible = false;
                ClassifyDeviceLocation(client);
                if (app != null)
                {
                    appointStartTimePick.Value = app.AppointmentStartTime;
                    appointEndTimePick.Value = app.AppointmentEndTime;
                    devPickTimePick.Value = app.PickupDate.Value;
                    devReturnTimePick.Value = app.DeviceReturnDate.Value;
                    deviceLocCB.Text = app.DeviceLocation;
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
                    if (runningTest != null)
                    {
                        startBtn.Visible = false;
                        continueBtn.Visible = true;
                    }
                    else if (DateTime.Compare(app.AppointmentEndTime, DateTime.Now) <= 0 || app.EcgTestId.HasValue)
                    {
                        appointGroup.Enabled = false;
                        startBtn.Visible = false;
                        editMailBtn.Enabled = false;
                        generateReportBtn.Enabled = true;
                    }  
                }
                else
                {
                    firstNameLabel.Text = thePat.PatientFirstName;
                    lastNameLabel.Text = thePat.PatientLastName;
                    startBtn.Visible = false;
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        private void EditMailBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Email emailForm = new Email(inClient);
                emailForm.Show();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        private void OkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                deviceLoc = deviceLocCB.Text;
                startTime = appointStartTimePick.Value;
                endTime = appointEndTimePick.Value;
                pickTime = devPickTimePick.Value;
                returnTime = devReturnTimePick.Value;
                if (string.IsNullOrWhiteSpace(deviceLocCB.Text))
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
                if (DateTime.Compare(devPickTimePick.Value, DateTime.Now) < 0)
                {
                    MessageBox.Show(ErrorInfo.EarlyThanNow.ErrorMessage);
                    return;
                }
                if (selectDev == null)
                {
                    MessageBox.Show(ErrorInfo.SelectDeivce.ErrorMessage);
                    return;
                }
                // Ok means user only click save btn which ecgtest is not created
                if (theAppoint == null)
                {
                    Appointment newApp = new Appointment(inClient.NurseId, thePat.PatientId, selectDev.DeviceId,
                                                         startTime, endTime, DateTime.Now, pickTime,
                                                         returnTime, deviceLoc, null, false, Config.ClinicId,
                                                         thePat.PatientFirstName, thePat.PatientLastName, null);
                    errorMsg = nResource.CreateAppointment(newApp, inClient);
                    if (ErrorInfo.OK.ErrorMessage == errorMsg)
                    {
                        MessageBox.Show(ErrorInfo.Updated.ErrorMessage);
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(errorMsg);
                        DialogResult = DialogResult.Cancel;
                    }
                    return;
                }
                Appointment updatedApp = new Appointment((int)theAppoint.AppointmentRecordId, inClient.NurseId, 
                                                        (int)theAppoint.PatientId, selectDev.DeviceId, 
                                                        startTime, endTime,
                                                        (DateTime)theAppoint.ReservationTime, pickTime,
                                                        returnTime, deviceLoc, (string)theAppoint.Instruction, 
                                                        false, Config.ClinicId, (string)theAppoint.FirstName,
                                                        (string)theAppoint.LastName, (int?)theAppoint.EcgTestId);
                errorMsg = nResource.UpdateAppointment(updatedApp, inClient);
                if (ErrorInfo.OK.ErrorMessage == errorMsg)
                {
                    MessageBox.Show(ErrorInfo.Updated.ErrorMessage);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(errorMsg);
                    DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
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
                if (string.IsNullOrWhiteSpace(deviceLocCB.Text))
                {
                    MessageBox.Show(ErrorInfo.DeviceLoc.ErrorMessage);
                    return;
                }
                restModel = dResource.GetAvailableDevices(inClient, devPickTimePick.Value, devReturnTimePick.Value, deviceLocCB.Text);
                if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    deviceCombo.Items.Clear();
                    returnDls = CreateDevLs(restModel.Feed.Entities);
                    foreach (var returnD in returnDls)
                    {
                        deviceCombo.Items.Add(returnD.DeviceName);
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
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
        private List<string> CreateDeviceLocLs(List<Entity<Device>> entls)
        {
            List<String> devLocLs = new List<String>();
            foreach (var ent in entls)
            {
                devLocLs.Add(ent.Model.DeviceLocation);
            }
            List<String> devLocDistinctLs = devLocLs.Distinct().ToList();
            return devLocDistinctLs;
        }
        private void StartBtn_Click(object sender, EventArgs e)
        {
            using (MainInterface mainForm = new MainInterface(inClient, theAppoint, null))
            {
                DialogResult res = mainForm.ShowDialog();
                if (res == DialogResult.Yes)
                {
                    theTest = mainForm.theEcgTest;
                    theAppoint = mainForm.theAppoint;
                    // Yes means user clicked the start btn which gurantees ecgtest is created
                    DialogResult = DialogResult.Yes;
                }else if (res == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
        }
        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            using (MainInterface mainForm = new MainInterface(inClient, theAppoint, theTest))
            {
                DialogResult res = mainForm.ShowDialog();
                if (res == DialogResult.Abort)
                {
                    theTest = mainForm.theEcgTest;
                    theAppoint = mainForm.theAppoint;
                    // Abort means user clicked the terminate btn
                    DialogResult = DialogResult.Abort;
                }else if (res == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
        }
        private void ClassifyDeviceLocation(Client client)
        {
            try
            {
                restModel = dResource.GetAllDevice(client);
                if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    List<string> returnDevLocLs = CreateDeviceLocLs(restModel.Feed.Entities);
                    foreach (var returnDevLoc in returnDevLocLs)
                    {
                        deviceLocCB.Items.Add(returnDevLoc);
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        private void ViewNoteBtn_Click(object sender, EventArgs e)
        {
            NoteForm noteF = new NoteForm();
            noteF.ShowDialog();
        }
    }
}
