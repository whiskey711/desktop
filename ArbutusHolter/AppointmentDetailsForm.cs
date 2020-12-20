using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class AppointmentDetailsForm : Form
    {
        RestModel<Device> restModel;
        RestModel<ResultJson> jsonRestMod;
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
            InitializeComponent();
            inClient = client;
            theAppoint = app;
            theTest = runningTest;
            thePat = patient;
            continueBtn.Visible = false;
            Task.Run(async () => await PrepareForm());
            Task.Run(async () => await ClassifyDeviceLocation());
        }
        private async Task PrepareForm()
        {
            appointGroup.UseWaitCursor = true;
            try
            {
                if (theAppoint != null)
                {
                    appointStartTimePick.Value = theAppoint.AppointmentStartTime;
                    appointEndTimePick.Value = theAppoint.AppointmentEndTime;
                    devPickTimePick.Value = theAppoint.PickupDate.Value;
                    devReturnTimePick.Value = theAppoint.DeviceReturnDate.Value;
                    firstNameLabel.Text = theAppoint.Patient.PatientFirstName;
                    lastNameLabel.Text = theAppoint.Patient.PatientLastName;
                    restModel = await dResource.GetAllDevice(inClient);
                    if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                    {
                        returnDls = CreateDevLs(restModel.Feed.Entities);
                        foreach (var returnD in returnDls)
                        {
                            if (returnD.DeviceId == theAppoint.Device.DeviceId)
                            {
                                selectDev = returnD;
                                deviceCombo.Invoke(new MethodInvoker(delegate { deviceCombo.Text = returnD.DeviceName; }));
                                break;
                            }
                        }
                    }
                    if (theAppoint.DeviceActualReturnTime.HasValue)
                    {
                        returnDevBtn.Enabled = false;
                    }
                    if (theTest != null)
                    {
                        startBtn.Visible = false;
                        continueBtn.Visible = true;
                    }
                    if (DateTime.Compare(theAppoint.AppointmentEndTime, DateTime.Now) <= 0 && theAppoint.EcgTest != null)
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
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            appointGroup.UseWaitCursor = false;
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
        private async void OkBtn_Click(object sender, EventArgs e)
        {
            appointGroup.UseWaitCursor = true;
            try
            {
                deviceLoc = deviceLocCB.Text;
                startTime = appointStartTimePick.Value
                                .AddSeconds(-appointStartTimePick.Value.Second)
                                .AddMilliseconds(-appointStartTimePick.Value.Millisecond);
                endTime = appointEndTimePick.Value
                                .AddSeconds(-appointEndTimePick.Value.Second)
                                .AddMilliseconds(-appointEndTimePick.Value.Millisecond);
                pickTime = devPickTimePick.Value
                                .AddSeconds(-devPickTimePick.Value.Second)
                                .AddMilliseconds(-devPickTimePick.Value.Millisecond);
                returnTime = devReturnTimePick.Value
                                .AddSeconds(-devReturnTimePick.Value.Second)
                                .AddMilliseconds(-devReturnTimePick.Value.Millisecond);
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
                    Appointment newApp = new Appointment(new Nurse(inClient.NurseId), thePat, selectDev,
                                                         startTime, endTime, DateTime.Now, pickTime,
                                                         returnTime, deviceLoc, null, null);
                    errorMsg = await nResource.CreateAppointment(newApp, inClient);
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
                Appointment updatedApp = new Appointment(theAppoint.AppointmentRecordId, new Nurse(inClient.NurseId), 
                                                        theAppoint.Patient, selectDev, 
                                                        startTime, endTime,
                                                        (DateTime)theAppoint.ReservationTime, pickTime,
                                                        returnTime, theAppoint.DeviceActualReturnTime, deviceLoc, 
                                                        (string)theAppoint.Instruction, theAppoint.EcgTest);
                errorMsg = await nResource.UpdateAppointment(updatedApp, inClient);
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
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            appointGroup.UseWaitCursor = false;
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
        private async void DeviceCombo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (string.IsNullOrWhiteSpace(deviceLocCB.Text))
                {
                    MessageBox.Show(ErrorInfo.DeviceLoc.ErrorMessage);
                    return;
                }
                deviceCombo.Items.Clear();
                restModel = await dResource.GetAvailableDevices(inClient, devPickTimePick.Value, devReturnTimePick.Value, deviceLocCB.Text);
                if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    returnDls = CreateDevLs(restModel.Feed.Entities);
                    foreach (var returnD in returnDls)
                    {
                        deviceCombo.Items.Add(returnD.DeviceName);
                    }
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            Cursor.Current = Cursors.Default;
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
            using (TestMonitorForm mainForm = new TestMonitorForm(inClient, theAppoint, null))
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
            using (TestMonitorForm mainForm = new TestMonitorForm(inClient, theAppoint, theTest))
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
        private async Task ClassifyDeviceLocation()
        {
            try
            {
                restModel = await dResource.GetAllDevice(inClient);
                if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                { 
                    if (theAppoint != null)
                    {
                        List<Device> returnDevLs = CreateDevLs(restModel.Feed.Entities);
                        foreach (var returnDev in returnDevLs)
                        {
                            if (theAppoint.Device.DeviceId == returnDev.DeviceId)
                            {
                                deviceLocCB.Invoke(new MethodInvoker(delegate { deviceLocCB.Text = returnDev.DeviceLocation; }));
                                break;
                            }
                        }
                    }
                    else
                    {
                        List<string> returnDevLocLs = CreateDeviceLocLs(restModel.Feed.Entities);
                        foreach (var returnDevLoc in returnDevLocLs)
                        {
                            deviceLocCB.Invoke(new MethodInvoker(delegate { deviceLocCB.Items.Add(returnDevLoc); }));
                        }
                    } 
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                Close();
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
            NoteForm noteF = new NoteForm(theTest);
            noteF.ShowDialog();
        }
        private async void ReturnDevBtn_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            try
            {
                jsonRestMod = await dResource.ReturnPhoneAndDevice(inClient, theAppoint.Device.DeviceId);
                if (ErrorInfo.OK.ErrorMessage == jsonRestMod.ErrorMessage)
                {
                    MessageBox.Show(ErrorInfo.DeviceReturned.ErrorMessage);
                    returnDevBtn.Enabled = false;
                }
                else
                {
                    MessageBox.Show(jsonRestMod.ErrorMessage);
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
                MessageBox.Show(ex.ToString());
            }
            UseWaitCursor = false;
        }
    }
}
