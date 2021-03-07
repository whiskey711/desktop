using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
using Uvic_Ecg_ArbutusHolter.DownloadData;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class AppointmentDetailsForm : Form
    {
        RestModel<Device> restModel;
        RestModel<ResultJson> jsonRestMod;
        RestModel<EcgRawData> rawDataMod;
        RestModel<PatientInfo> pMod;
        RestModel<Appointment> appointRestMod;
        private PatientResource pResource = new PatientResource();
        private EcgDataResources eResource = new EcgDataResources();
        private DeviceResource dResource = new DeviceResource();
        private NurseResource nResource = new NurseResource();
        private Client inClient;
        List<Device> returnDls;
        IEnumerable<Appointment> appointLs;
        string[] nameLs;
        string errorMsg;
        int seven = 7;
        string available = "Current device is avaliable with in a week";
        public Device selectDev { get; set; }
        public string deviceLoc { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public DateTime pickTime { get; set; }
        public DateTime returnTime { get; set; }
        public DateTime? deferTime { get; set; }
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
            deferBtn.Enabled = false;
            returnDevBtn.Enabled = false;
        }
        private void PrepareForm()
        {
            UseWaitCursor = true;
            try
            {
                // upcoming, inprogress, finsished
                if (theAppoint != null)
                {
                    appointStartTimePick.Value = theAppoint.AppointmentStartTime;
                    appointStartTimePick.Enabled = appointStartTimePick.Value < DateTime.Now ? false : true;
                    appointEndTimePick.Value = theAppoint.AppointmentEndTime;
                    appointEndTimePick.Enabled = appointEndTimePick.Value < DateTime.Now ? false : true;
                    devPickTimePick.Value = theAppoint.PickupDate.Value;
                    if (devPickTimePick.Value < DateTime.Now)
                    {
                        devPickTimePick.Enabled = false;
                        deviceLocCB.Enabled = false;
                        deviceCombo.Enabled = false;
                    }
                    devReturnTimePick.Value = theAppoint.DeviceReturnDate.Value;
                    devReturnTimePick.Enabled = devReturnTimePick.Value < DateTime.Now ? false : true;
                    if (theAppoint.DeferReturnTime.HasValue)
                    {
                        deferLabel.Visible = true;
                        deferTimePick.Visible = true;
                        deferTimePick.Value = theAppoint.DeferReturnTime.Value;
                        deferTimePick.Enabled = false;
                        deferBtn.Enabled = false;
                    }
                    else if (DateTime.Compare(theAppoint.DeviceReturnDate.Value, DateTime.Now) <= 0)
                    {
                        deferBtn.Enabled = true;
                    }
                    firstNameLabel.Text = theAppoint.Patient.PatientFirstName;
                    lastNameLabel.Text = theAppoint.Patient.PatientLastName;
                    // inprogress or finished
                    if (theTest != null)
                    {
                        startBtn.Visible = false;
                        continueBtn.Visible = true;
                        if (!theAppoint.DeviceActualReturnTime.HasValue)
                        {
                            returnDevBtn.Enabled = true;
                        }
                    }
                    if (DateTime.Compare(theAppoint.AppointmentEndTime, DateTime.Now) <= 0 && theTest != null)
                    {
                        startBtn.Visible = false;
                        mailBtn.Enabled = false;
                        generateReportBtn.Enabled = true;
                    }
                }
                // appointment is creating
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
            finally
            {
                UseWaitCursor = false;
            }
        }
        private async void Form_Loaded(object sender, EventArgs e)
        {
            PrepareForm();
            await ClassifyDeviceLocationAndName();
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
                deferTime = deferTimePick.Value
                                .AddSeconds(-deferTimePick.Value.Second)
                                .AddMilliseconds(-deferTimePick.Value.Millisecond);
                if (deferTimePick.Visible != true)
                {
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
                }  
                // Ok means user only click save btn which ecgtest is not created
                if (theAppoint == null)
                {
                    await CreateAppointment();
                }
                else
                {
                    await UpdateAppointment();
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
        private async Task CreateAppointment()
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
        }
        private async Task UpdateAppointment()
        {
            Appointment updatedApp = new Appointment(theAppoint.AppointmentRecordId, new Nurse(inClient.NurseId),
                                                        theAppoint.Patient, selectDev,
                                                        startTime, endTime,
                                                        (DateTime)theAppoint.ReservationTime, pickTime,
                                                        returnTime, theAppoint.DeviceActualReturnTime, deviceLoc,
                                                        theAppoint.Instruction, theAppoint.EcgTest);
            if (deferTimePick.Visible == true)
            {
                if (nextAppointTimePick.Value > deferTime)
                {
                    updatedApp.DeferReturnTime = deferTime;
                }
                else if (nextAppointTimePick.Visible == true)
                {
                    MessageBox.Show(ErrorInfo.Toolate.ErrorMessage);
                    return;
                }
                else
                {
                    MessageBox.Show(ErrorInfo.WithinOneWeek.ErrorMessage);
                    return;
                }
            }
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
            List<string> devLocLs = new List<string>();
            foreach (var ent in entls)
            {
                devLocLs.Add(ent.Model.DeviceLocation);
            }
            List<string> devLocDistinctLs = devLocLs.Distinct().ToList();
            return devLocDistinctLs;
        }
        private List<Appointment> CreateAppointmentLs(List<Entity<Appointment>> entls)
        {
            List<Appointment> aLs = new List<Appointment>();
            foreach (var ent in entls)
            {
                aLs.Add(ent.Model);
            }
            return aLs;
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
        private async Task ClassifyDeviceLocationAndName()
        {
            try
            {
                restModel = await dResource.GetAllDevice(inClient);
                if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    List<Device> returnDevLs = CreateDevLs(restModel.Feed.Entities);
                    List<string> returnDevLocLs = CreateDeviceLocLs(restModel.Feed.Entities);
                    foreach (var returnDevLoc in returnDevLocLs)
                    {
                        deviceLocCB.Invoke(new MethodInvoker(delegate { deviceLocCB.Items.Add(returnDevLoc); }));
                    }
                    if (theAppoint != null)
                    { 
                        foreach (var returnDev in returnDevLs)
                        {
                            if (theAppoint.Device.DeviceId == returnDev.DeviceId)
                            {
                                deviceLocCB.Invoke(new MethodInvoker(delegate { deviceLocCB.SelectedItem = returnDev.DeviceLocation; }));
                                deviceCombo.Invoke(new MethodInvoker(delegate { deviceCombo.Text = returnDev.DeviceName; }));
                                selectDev = returnDev;
                            }
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
                jsonRestMod = await dResource.ReturnPhoneAndDevice(inClient, theAppoint);
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
        private async void GenerateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(ErrorInfo.Downloading.ErrorMessage);
                Application.UseWaitCursor = true;
                backgroundPanel.Enabled = false;
                bool b;
                DownloadMethod method = new DownloadMethod(inClient);
                DirectoryInfo dir = new DirectoryInfo(ManageFile.folderName + ManageFile.testFolderName + theTest.EcgTestId);
                if (!dir.Exists || !ManageFile.CheckPatient(dir))
                {
                    b = await method.GetOneTestAndPatient(theTest);
                    if (!b)
                    {
                        MessageBox.Show(ErrorInfo.DownloadProblem.ErrorMessage);
                        return;
                    }
                }
                b = await method.CheckIntegrity(dir);
                if (!b)
                {
                    b = await method.GetData(theTest);
                    if (!b)
                    {
                        MessageBox.Show(ErrorInfo.DownloadProblem.ErrorMessage);
                        return;
                    }
                }
                if (!ManageFile.CheckIsne(dir))
                {
                    DownloadMethod.ConverToIsne(dir);
                }
                GenerateReport(dir);
                Close();
            }
            catch (HttpRequestException hrex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(hrex.ToString(), hrex.StackTrace, w);
                }
                MessageBox.Show(ErrorInfo.ConnectionProblem.ErrorMessage);
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
                MessageBox.Show(ErrorInfo.GenerateProblem.ErrorMessage + "\n" + ex.ToString());
            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }
        private void GenerateReport(DirectoryInfo dir)
        {
            try
            {
                string cmd = ManageFile.importKey + dir.FullName + ManageFile.ishneName;
                Process proc = new Process();
                proc.StartInfo.FileName = "CER-S.exe";
                proc.StartInfo.Arguments = cmd;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async void DeferBtn_Click(object sender, EventArgs e)
        {
            deferTimePick.Visible = true;
            deferLabel.Visible = true;
            nextAppointLabel.Visible = true;
            appointRestMod = await nResource.GetAppointments(inClient, theAppoint.AppointmentStartTime, 
                                                                theAppoint.AppointmentStartTime.AddDays(seven), null);
            if (appointRestMod.ErrorMessage == ErrorInfo.OK.ErrorMessage)
            {
                appointLs = CreateAppointmentLs(appointRestMod.Feed.Entities);
            }
            Appointment nextEarliestAppoint = appointLs.OrderBy(app => app.AppointmentStartTime)
                                                       .Where(app => app.Device.DeviceId == theAppoint.Device.DeviceId &&
                                                                     app.AppointmentRecordId != theAppoint.AppointmentRecordId &&
                                                                     app.AppointmentStartTime > DateTime.Now)
                                                       .FirstOrDefault();
            // display time limit for defer return time
            if (nextEarliestAppoint != null)
            {
                nextAppointTimePick.Visible = true;
                nextAppointTimePick.Value = nextEarliestAppoint.AppointmentStartTime;
            }
            else
            {
                nextAppointLabel.Text = available;
                nextAppointTimePick.Value = theAppoint.AppointmentStartTime.AddDays(seven);
            }
            if (nextAppointTimePick.Value < DateTime.Now)
            {
                deferTimePick.Enabled = false;
            }
        }
    }
}
