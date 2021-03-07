using Calendar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class AppointmentForm : Form
    {
        List<Calendar.Appointment> appointLs = new List<Calendar.Appointment>();
        Calendar.Appointment appointStart, appointEnd;
        List<Uvic_Ecg_Model.Appointment> returnAls = new List<Uvic_Ecg_Model.Appointment>();
        List<String> returnDevLocLs = new List<String>();
        List<int> notReturnedAppointmentIdLs = new List<int>();
        Uvic_Ecg_Model.Appointment app;
        DateTime startTime = DateTime.Now;
        DownloadRawData download = new DownloadRawData();
        RestModel<Uvic_Ecg_Model.Appointment> restModel;
        RestModel<PatientInfo> pRestMod;
        RestModel<Device> dRestMod;
        RestModel<EcgTest> ecgTestMod;
        NurseResource nResource = new NurseResource();
        PatientResource patientResource = new PatientResource();
        DeviceResource dResource = new DeviceResource();
        EcgDataResources eResource = new EcgDataResources();
        Client appointFormClient;
        Dictionary<int, EcgTest> runningTestDict = new Dictionary<int, EcgTest>();
        string errorMsg;
        PatientInfo selectedP;
        Uvic_Ecg_Model.Appointment selectedA;
        PatientInfo updatedPatient;
        int occupiedDev = 0;
        int devUpLimit = 5;
        long num;
        int startAfterTo, endBeforeFrom;
        static int halfYear = 183;
        DateTime thisYearStart = DateTime.Today.AddDays(-halfYear);
        DateTime thisYeaarEnd = DateTime.Today.AddDays(halfYear);
        int appointBlockMinLength = 15;
        int invalidPid = -1;
        int oneMin = 60000;
        int tenMin = 600000;
        string monthYear = "MMMM yyyy";
        string dateAndTime = "MM/dd/yyyy HH:mm";
        string allLocation = "All locations";
        string devLoc;
        bool programClosing = false;
        public AppointmentForm(Client client)
        {
            InitializeComponent();
            SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.StandardDoubleClick, false);
            foreach (var gen in Enum.GetValues(typeof(Config.Gender)))
            {
                genderCB.Items.Add(gen);
            }
            PatientDetailsGroup.Enabled = false;
            startTimeFilt.Value = DateTime.Today;
            endTimeFilt.Value = DateTime.Today.AddDays(7);
            appointFormClient = client;
            pNameCheckBox.Enabled = false;
            yearIndicateLab.Text = DateTime.Today.ToString(monthYear);
            appointRefreshTimer.Interval = oneMin;
            appointRefreshTimer.Start();
            runningTestRefreshTimer.Interval = oneMin;
            runningTestRefreshTimer.Start();
            rawDataRefreshTimer.Interval = tenMin;
            rawDataRefreshTimer.Start();
        }
        private async void SrhBtn_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            try
            {
                if (string.IsNullOrWhiteSpace(pLastNameTextBox.Text) && string.IsNullOrWhiteSpace(pFirstNameTextBox.Text) &&
                    string.IsNullOrWhiteSpace(birthText.Text) && string.IsNullOrWhiteSpace(phnTextBox.Text))
                {
                    MessageBox.Show(ErrorInfo.FillAll.ErrorMessage);
                    return;
                }
                else if (!string.IsNullOrWhiteSpace(birthText.Text)) 
                {
                    if (!DateFormat(birthText.Text, "/"))
                    {
                        MessageBox.Show(ErrorInfo.WrongDate.ErrorMessage);
                        return;
                    }                   
                }
                await SrhPatient(pLastNameTextBox.Text, pFirstNameTextBox.Text, birthText.Text, phnTextBox.Text, invalidPid);
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
                Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            UseWaitCursor = false;
        }
        private async Task SrhPatient(string lastName, string firstName, string birth, string phn, int pid)
        {
            patientListView.Items.Clear();
            try
            {
                pRestMod = await patientResource.GetPatient(lastName, firstName, birth, phn, appointFormClient);
                if (pRestMod.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    List<Entity<PatientInfo>> returnEls = pRestMod.Feed.Entities;
                    List<PatientInfo> returnPls = CreatePatientLs(returnEls);
                    if (pid == invalidPid)
                    {
                        foreach (var returnP in returnPls)
                        {
                            var row = new string[] {returnP.PatientLastName,
                                                    returnP.PatientMidName,
                                                    returnP.PatientFirstName,
                                                    ChangeFormat(returnP.Birthdate.ToString()),
                                                    returnP.Phn};
                            var lsitem = new ListViewItem(row);
                            lsitem.Tag = returnP;
                            patientListView.Items.Add(lsitem);
                        }
                    }
                    else
                    {
                        foreach (var returnP in returnPls)
                        {
                            if (returnP.PatientId == pid)
                            {
                                var row = new string[] {returnP.PatientLastName,
                                                    returnP.PatientMidName,
                                                    returnP.PatientFirstName,
                                                    returnP.Birthdate.ToString(),
                                                    returnP.Phn};
                                var lsitem = new ListViewItem(row);
                                lsitem.Tag = returnP;
                                selectedP = returnP;
                                patientListView.Items.Add(lsitem);
                                LoadPatientInfo(returnP);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(ErrorInfo.NoResult.ErrorMessage);
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private void CreateBtn_Click(object sender, EventArgs e)
        {
            CreatePatientForm cpForm = new CreatePatientForm(appointFormClient);
            cpForm.Show();
        }
        private async void PatientListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            try
            {
                if (patientListView.SelectedItems.Count <= 0)
                {
                    return;
                }
                selectedP = (PatientInfo)patientListView.SelectedItems[0].Tag;
                LoadPatientInfo(selectedP);
                await LoadPatientAppointment(selectedP);
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
                Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            UseWaitCursor = false;
        }
        private void LoadPatientInfo(PatientInfo theOne)
        {
            lastNameTB.Text = theOne.PatientLastName;
            midNameTB.Text = theOne.PatientMidName;
            firstNameTB.Text = theOne.PatientFirstName;
            birthDateTB.Text = ChangeFormat(theOne.Birthdate);
            address1TB.Text = theOne.Address1;
            provinceTB.Text = theOne.Province;
            cityTB.Text = theOne.City;
            mailTB.Text = theOne.Email;
            phnTB.Text = theOne.Phn;
            phoneNumTB.Text = theOne.PhoneNumber;
            homeNumTB.Text = theOne.HomeNumber;
            genderCB.Text = theOne.Gender;
            postCodeTB.Text = theOne.PostCode;
            pacemakerTB.Text = theOne.Pacemaker;
            superPhyTB.Text = theOne.SuperPhysician;
            ageTB.Text = theOne.Age;
            PatientDetailsGroup.Enabled = true;
            saveBtn.Enabled = true;
            pNameCheckBox.Text = theOne.PatientFirstName + " " + theOne.PatientLastName;
            pNameCheckBox.CheckState = CheckState.Checked;
            pNameCheckBox.Enabled = true;
        }
        private async Task LoadPatientAppointment(PatientInfo theOne)
        {
            try
            {
                restModel = await nResource.GetPatientAppoint(theOne.PatientId, appointFormClient);
            
            patientAppointLs.Items.Clear();
            if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
            {
                returnAls = CreateAppointLs(restModel.Feed.Entities);
                startTimeFilt.Value = returnAls.First().AppointmentStartTime.Date;
                endTimeFilt.Value = returnAls.Last().AppointmentEndTime.Date;
            }
            else if (restModel.ErrorMessage == "Others")
            {
                return;
            }
            else
            {
                MessageBox.Show(restModel.ErrorMessage);
            }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            try
            {
                if (!string.IsNullOrWhiteSpace(lastNameTB.Text) &&
                        !string.IsNullOrWhiteSpace(firstNameTB.Text) &&
                        !string.IsNullOrWhiteSpace(birthDateTB.Text) &&
                        !string.IsNullOrWhiteSpace(address1TB.Text) &&
                        !string.IsNullOrWhiteSpace(provinceTB.Text) &&
                        !string.IsNullOrWhiteSpace(phnTB.Text) &&
                        !string.IsNullOrWhiteSpace(genderCB.Text) &&
                        !string.IsNullOrWhiteSpace(cityTB.Text) &&
                        !string.IsNullOrWhiteSpace(homeNumTB.Text) &&
                        !string.IsNullOrWhiteSpace(mailTB.Text))
                {
                    if (phnTB.Text.Length < 10 && !long.TryParse(phnTB.Text, out num))
                    {
                        MessageBox.Show(ErrorInfo.WrongPhn.ErrorMessage);
                        return;
                    }
                    if (!DateFormat(birthDateTB.Text, "-"))
                    {
                        MessageBox.Show(ErrorInfo.WrongDate.ErrorMessage);
                        return;
                    }
                    string replaceDate = ChangeFormat(birthDateTB.Text);
                    if (!string.IsNullOrWhiteSpace(mailTB.Text) && !RegexUtilities.IsValidEmail(mailTB.Text))
                    {
                        MessageBox.Show(ErrorInfo.WrongMail.ErrorMessage);
                        return;
                    }                    
                    updatedPatient = new PatientInfo(selectedP.PatientId, lastNameTB.Text, midNameTB.Text, firstNameTB.Text, replaceDate, address1TB.Text,
                                                     null, provinceTB.Text, cityTB.Text, mailTB.Text, phnTB.Text, phoneNumTB.Text, null, homeNumTB.Text,
                                                     genderCB.Text, postCodeTB.Text, false, Config.ClinicId, pacemakerTB.Text, superPhyTB.Text,
                                                     null, null, null, null, null, null, ageTB.Text);
                    errorMsg = await patientResource.UpdatePatient(updatedPatient, appointFormClient);
                    if (errorMsg == ErrorInfo.OK.ErrorMessage)
                    {
                        MessageBox.Show(ErrorInfo.Updated.ErrorMessage);
                        selectedP = updatedPatient;
                        await SrhPatient(selectedP.PatientLastName, selectedP.PatientFirstName, null, null, selectedP.PatientId);
                        await LoadPatientAppointment(selectedP);
                    }
                    else
                    {
                        MessageBox.Show(errorMsg);
                    }
                    saveBtn.Enabled = false;
                    //PatientDetailsGroup.Enabled = false;
                }
                else
                {
                    MessageBox.Show(ErrorInfo.FillAll.ErrorMessage);
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
                Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            UseWaitCursor = false;
        }
        private string ChangeFormat(string originDate)
        {
            string[] ls;
            string posChange;
            if (originDate.Contains('/'))
            {
                ls = originDate.Split('/');
                posChange = ls[2] + "-" + ls[0] + "-" + ls[1];
            }
            else
            {
                ls = originDate.Split('-');
                posChange = ls[1] + "/" + ls[2] + "/" + ls[0];
            }          
            return posChange;
        }
        private void DayView_NewAppointment(object sender, NewAppointmentEventArgs args)
        {
            try
            {
                Calendar.Appointment Appointment = new Calendar.Appointment();
                Appointment.StartDate = args.StartDate;
                Appointment.EndDate = args.EndDate;
                Appointment.Title = args.Title;
                Appointment.Group = "2";
                appointLs.Add(Appointment);
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private void DayView_ResolveAppointments(object sender, ResolveAppointmentsEventArgs args)
        {
            try
            {
                List<Calendar.Appointment> Apps = new List<Calendar.Appointment>();
                foreach (Calendar.Appointment App in appointLs)
                {
                    if ((App.StartDate >= args.StartDate) &&
                        (App.StartDate <= args.EndDate))
                        Apps.Add(App);
                }
                args.Appointments = Apps;
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private async Task LoadAllAppointments()
        {
            try
            { 
                restModel = await nResource.GetAppointments(appointFormClient, thisYearStart, thisYeaarEnd, devLoc);
                if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    returnAls = CreateAppointLs(restModel.Feed.Entities);
                    notReturnedAppointmentIdLs = DeviceNotReturnedAppointments(returnAls);
                    appointLs.Clear();
                    foreach (var returnA in returnAls)
                    {
                        appointStart = new Calendar.Appointment();
                        appointStart.StartDate = returnA.AppointmentStartTime;
                        appointStart.EndDate = appointStart.StartDate.AddMinutes(appointBlockMinLength); 
                        appointStart.Appoint = returnA;
                        appointStart.Title = returnA.Patient.PatientFirstName + " " + returnA.Patient.PatientLastName;
                        appointEnd = new Calendar.Appointment();
                        appointEnd.StartDate = returnA.AppointmentEndTime;
                        appointEnd.EndDate = appointEnd.StartDate.AddMinutes(appointBlockMinLength);
                        appointEnd.Appoint = returnA;
                        appointEnd.Title = returnA.Patient.PatientFirstName + " " + returnA.Patient.PatientLastName;
                        if (notReturnedAppointmentIdLs.Contains(returnA.AppointmentRecordId))
                        {
                            appointStart.Color = Color.Red;
                            appointEnd.Color = Color.Red;
                        }
                        else
                        {
                            appointStart.Color = Color.DeepSkyBlue;
                            appointEnd.Color = Color.Black;
                        }
                        if (pNameCheckBox.Checked && returnA.Patient.PatientId != selectedP.PatientId)
                        {
                            continue;
                        }
                        appointLs.Add(appointStart);
                        appointLs.Add(appointEnd);
                    }
                    weeklyCal.StartDate = DateTime.Today;
                    weeklyCal.NewAppointment += new NewAppointmentEventHandler(DayView_NewAppointment);
                    weeklyCal.ResolveAppointments += new ResolveAppointmentsEventHandler(DayView_ResolveAppointments);
                    TimeFilt_Changed();
                }
                else
                {
                    MessageBox.Show(restModel.ErrorMessage);
                }
                weeklyCal.DaysToShow = 7;
                weeklyCal.Invalidate();
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private async Task ClassifyDeviceLocation(Client client)
        {
            try
            {
                dRestMod = await dResource.GetAllDevice(client);
                if (dRestMod.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    returnDevLocLs = CreateDeviceLocLs(dRestMod.Feed.Entities);
                    foreach (var returnDevLoc in returnDevLocLs)
                    {
                        regionComboBox.Invoke(new MethodInvoker(delegate { regionComboBox.Items.Add(returnDevLoc); }));
                    }
                    regionComboBox.Invoke(new MethodInvoker(delegate { regionComboBox.Items.Add(allLocation); }));
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private void PatientDetailsGroup_Leave(object sender, EventArgs e)
        {
            if (!saveBtn.Enabled)
            {
                return;
            }
            string msg = "Do you want to leave without saving patient info?";
            string caption = "Leave patient details";
            var result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                PatientDetailsGroup.Focus();
            }
            else if (result == DialogResult.Yes)
            {
                ClearText(PatientDetailsGroup);
                PatientDetailsGroup.Enabled = false;
            }
        }
        private List<Uvic_Ecg_Model.Appointment> CreateAppointLs(List<Entity<Uvic_Ecg_Model.Appointment>> entls)
        {
            List<Uvic_Ecg_Model.Appointment> als = new List<Uvic_Ecg_Model.Appointment>();
            if (string.IsNullOrWhiteSpace(devLoc) || devLoc.Equals(allLocation))
            {
                foreach (var ent in entls)
                {
                    als.Add(ent.Model);
                }
            }
            else
            {
                foreach (var ent in entls)
                {
                    if (devLoc.Equals(ent.Model.DeviceLocation))
                    {
                        als.Add(ent.Model);
                    }
                    
                }
            }
            als.Sort((x, y) => x.AppointmentStartTime.CompareTo(y.AppointmentStartTime));
            return als;
        }
        private List<PatientInfo> CreatePatientLs(List<Entity<PatientInfo>> entls)
        {
            List<PatientInfo> pls = new List<PatientInfo>();
            foreach (var ent in entls)
            {
                pls.Add(ent.Model);
            }
            return pls;
        }
        private List<String> CreateDeviceLocLs(List<Entity<Device>> entls)
        {
            List<String> devLocLs = new List<String>();
            foreach (var ent in entls)
            {
                devLocLs.Add(ent.Model.DeviceLocation);
            }
            List<String> devLocDistinctLs = devLocLs.Distinct().ToList();
            return devLocDistinctLs;
        }
        private List<EcgTest> CreateTestLs(List<Entity<EcgTest>> entls)
        {
            List<EcgTest> els = new List<EcgTest>();
            foreach (var ent in entls)
            {
                els.Add(ent.Model);
            }
            return els;
        }
        private bool DateFormat(string date, string type)
        {
            DateTime result;
            if (DateTime.TryParseExact(
                date,
                "MM/dd/yyyy",
                null,
                DateTimeStyles.AssumeUniversal,
                out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void DayBtn_Click(object sender, EventArgs e)
        {
            weeklyCal.DaysToShow = 1;
        }
        private void WeekBtn_Click(object sender, EventArgs e)
        {
            weeklyCal.DaysToShow = 7;
        }
        private async void AddAppointBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!pNameCheckBox.Checked)
                {
                    MessageBox.Show(ErrorInfo.SelectPatient.ErrorMessage);
                    return;
                }
                if (occupiedDev == devUpLimit)
                {
                    MessageBox.Show(ErrorInfo.Occuiped.ErrorMessage);
                }
                await AddNewAppoinment();
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private async void AppointmentF_Load(object sender, EventArgs e) 
        {
            Application.UseWaitCursor = true;
            await LoadAllAppointments();
            await ClassifyDeviceLocation(appointFormClient);
            await RefreshRunningTest();
            await download.MainProcess(appointFormClient);
            Application.UseWaitCursor = false;
        }
        private void GoleftBtn_Click(object sender, EventArgs e)
        {
            endTimeFilt.Value = weeklyCal.StartDate;
            if (weeklyCal.DaysToShow == 7)
            {
                weeklyCal.StartDate = weeklyCal.StartDate.AddDays(-7);
            }
            else
            {
                weeklyCal.StartDate = weeklyCal.StartDate.AddDays(-1);
            }
            yearIndicateLab.Text = weeklyCal.StartDate.ToString(monthYear);
            startTimeFilt.Value = weeklyCal.StartDate;
        }
        private void GorightBtn_Click(object sender, EventArgs e)
        {
            if (weeklyCal.DaysToShow == 7)
            {
                weeklyCal.StartDate = weeklyCal.StartDate.AddDays(7);
                startTimeFilt.Value = weeklyCal.StartDate;
                endTimeFilt.Value = startTimeFilt.Value.AddDays(7);
            }
            else
            {
                weeklyCal.StartDate = weeklyCal.StartDate.AddDays(1);
                startTimeFilt.Value = weeklyCal.StartDate;
                endTimeFilt.Value = startTimeFilt.Value.AddDays(7);
            }
            yearIndicateLab.Text = weeklyCal.StartDate.ToString(monthYear);
        }
        private void StartTimeFilt_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(startTimeFilt.Value, endTimeFilt.Value) > 0)
            {
                endTimeFilt.Value = startTimeFilt.Value;
            }
            else
            {
                TimeFilt_Changed();
            }         
        }
        private void EndTimeFilt_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(startTimeFilt.Value, endTimeFilt.Value) > 0)
            {
                startTimeFilt.Value = endTimeFilt.Value;
            }
            else
            {
                TimeFilt_Changed();
            }
        }
        private void TimeFilt_Changed()
        {
            try
            {
                patientAppointLs.Items.Clear();
                foreach (var returnA in returnAls)
                {
                    startAfterTo = DateTime.Compare(returnA.AppointmentStartTime.Date, endTimeFilt.Value.Date);
                    endBeforeFrom = DateTime.Compare(returnA.AppointmentEndTime.Date, startTimeFilt.Value.Date);
                    if (startAfterTo > 0 || endBeforeFrom < 0)
                    {
                        continue; 
                    }
                    var row = new string[]
                        {
                        returnA.Patient.PatientFirstName + " " + returnA.Patient.PatientLastName,
                        returnA.AppointmentStartTime.ToString(dateAndTime),
                        returnA.AppointmentEndTime.ToString(dateAndTime)
                        };
                    var lsitem = new ListViewItem(row);
                    lsitem.Tag = returnA;
                    if (notReturnedAppointmentIdLs.Contains(returnA.AppointmentRecordId))
                    {
                        lsitem.ForeColor = Color.Red;
                    }
                    if (pNameCheckBox.Checked && returnA.Patient.PatientId != selectedP.PatientId)
                    {
                        continue;
                    }
                    patientAppointLs.Invoke(new MethodInvoker(delegate { patientAppointLs.Items.Add(lsitem); }));
                }
                weeklyCal.StartDate = startTimeFilt.Value;
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private async void WeeklyCal_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            if (weeklyCal.SelectedAppointment == null)
            {
                if (!pNameCheckBox.Checked)
                {
                    MessageBox.Show(ErrorInfo.SelectPatient.ErrorMessage);
                    return;
                }
                await AddNewAppoinment();
            }
            else
            {
                selectedA = weeklyCal.SelectedAppointment.Appoint;
                weeklyCal.Enabled = false;
                if (selectedA.EcgTest != null && runningTestDict.TryGetValue(selectedA.EcgTest.EcgTestId, out EcgTest test))
                {
                    await ShowAppointDetailFormForInProgressAppoint(selectedA, selectedA.EcgTest.EcgTestId);
                }
                else
                {
                    await ShowAppointDetailFormForUpcomingAppoint(selectedA);
                }
                weeklyCal.Enabled = true;
                weeklyCal.Invalidate();
            }

        }
        private async void PNameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // uncheck
                if (!pNameCheckBox.Checked)
                {
                    patientAppointLs.Items.Clear();
                    patientListView.Items.Clear();
                    ClearText(PatientDetailsGroup);
                    ClearText(srhGroup);
                    pNameCheckBox.Text = "Clinic";
                    pNameCheckBox.Enabled = false;
                    startTimeFilt.Value = DateTime.Today;
                    endTimeFilt.Value = DateTime.Today.AddDays(7);
                    await LoadAllAppointments();
                } 
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private async void PatientAppointLs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (patientAppointLs.SelectedItems.Count <= 0)
            {
                return;
            }
            selectedA = (Uvic_Ecg_Model.Appointment)patientAppointLs.SelectedItems[0].Tag;
            if (selectedA.EcgTest != null && runningTestDict.TryGetValue(selectedA.EcgTest.EcgTestId, out EcgTest test))
            {
                await ShowAppointDetailFormForInProgressAppoint(selectedA, selectedA.EcgTest.EcgTestId);
            }
            else if (selectedA.EcgTest != null && !runningTestDict.TryGetValue(selectedA.EcgTest.EcgTestId, out test))
            {
                await ShowAppointDetailFormForFinishedAppoint(selectedA);
            }
            else
            {
                await ShowAppointDetailFormForUpcomingAppoint(selectedA);
            }

        }
        private async void RegionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                devLoc = regionComboBox.SelectedItem.ToString();
                if (allLocation.Equals(devLoc))
                {
                    devLoc = null;
                }
                if (pNameCheckBox.Checked)
                {
                    await LoadPatientAppointment(selectedP);
                }
                else
                {
                    await LoadAllAppointments();
                }               
                weeklyCal.Invalidate();
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private async void PatientAppointLs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    if (patientAppointLs.SelectedItems.Count <= 0)
                    {
                        return;
                    }
                    selectedA = (Uvic_Ecg_Model.Appointment)patientAppointLs.SelectedItems[0].Tag;
                    weeklyCal.StartDate = selectedA.AppointmentStartTime;
                    string[] name = patientAppointLs.SelectedItems[0].Text.Split(' ');
                    await SrhPatient(name[1], name[0], null, null, selectedA.Patient.PatientId);
                    yearIndicateLab.Text = selectedA.AppointmentStartTime.ToString(monthYear);
                }
                catch (Exception ex)
                {
                    using (StreamWriter w = File.AppendText(FileName.Log.Name))
                    {
                        LogHandle.Log(ex.ToString(), ex.StackTrace, w);   
                    }
                }
            }
        }
        private void ClearText(Control cons)
        {
            foreach (Control c in cons.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearText(c);
            }
        }
        private async Task AddNewAppoinment()
        {
            try
            {
                using (AppointmentDetailsForm appointDForm = new AppointmentDetailsForm(appointFormClient, null, null, selectedP))
                {
                    // If dialogresult is ok, user didn't start a test
                    if (appointDForm.ShowDialog() == DialogResult.OK)
                    {
                        UseWaitCursor = true;
                        await LoadAllAppointments();
                    }
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
                Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            UseWaitCursor = false;
        }
        private async Task ShowAppointDetailFormForUpcomingAppoint(Uvic_Ecg_Model.Appointment theApp)
        {
            using (AppointmentDetailsForm appDForm = new AppointmentDetailsForm(appointFormClient, theApp, null,
                                                                                selectedP))
            {
                DialogResult res = appDForm.ShowDialog();
                // If dialogresult is cancel, user did not do anything
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                // If dialogresult is ok, user didn't start test
                else if (res == DialogResult.OK)
                {
                    await LoadAllAppointments();
                }
                // If dialogresult is yes, user must created a ecgtest
                else if (res == DialogResult.Yes)
                {
                    await RefreshRunningTest();
                }
            }
        }
        private async Task ShowAppointDetailFormForInProgressAppoint(Uvic_Ecg_Model.Appointment theApp, int testid)
        {
            using (AppointmentDetailsForm appDForm = new AppointmentDetailsForm(appointFormClient, theApp,
                                                                                runningTestDict[testid], null))
            {
                if (appDForm.ShowDialog() == DialogResult.Abort)
                {
                    runningTestDict.Remove(testid);
                    await LoadAllAppointments();
                    await RefreshRunningTest();
                    MessageBox.Show(ErrorInfo.TestTerminated.ErrorMessage);
                }
            }

        }
        private async Task ShowAppointDetailFormForFinishedAppoint(Uvic_Ecg_Model.Appointment theApp)
        {
            UseWaitCursor = true;
            try
            {
                ecgTestMod = await eResource.GetTestById(theApp.EcgTest.EcgTestId, theApp.Patient.PatientId, appointFormClient);
                pRestMod = await patientResource.GetPatientById(theApp.Patient.PatientId, appointFormClient);
                if (ErrorInfo.OK.ErrorMessage == ecgTestMod.ErrorMessage && ErrorInfo.OK.ErrorMessage == pRestMod.ErrorMessage)
                {
                    EcgTest test = ecgTestMod.Entity.Model;
                    PatientInfo patient = pRestMod.Entity.Model;
                    AppointmentDetailsForm appDForm = new AppointmentDetailsForm(appointFormClient, theApp, test, patient);
                    appDForm.ShowDialog();
                    appDForm.FormClosed += (s, args) => UseWaitCursor = false;
                    await LoadAllAppointments();
                }
                else
                {
                    MessageBox.Show(ErrorInfo.ConnectionProblem.ErrorMessage);
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private async void InProgressTestLs_MouseDoubleClick(object sender, EventArgs e)
        {
            if (inProgressTestLs.SelectedItems.Count <= 0)
            {
                return;
            }
            int theTestId = (int)inProgressTestLs.SelectedItems[0].Tag;
            try
            {
                UseWaitCursor = true;
                bool matched = false;
                restModel = await nResource.GetPatientAppoint(runningTestDict[theTestId].PatientId, appointFormClient);
                if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    returnAls = CreateAppointLs(restModel.Feed.Entities);
                    foreach (var returnA in returnAls)
                    {
                        if (returnA.EcgTest != null && theTestId == returnA.EcgTest.EcgTestId)
                        {
                            matched = true;
                            using (TestMonitorForm mainForm = new TestMonitorForm(appointFormClient, returnA, runningTestDict[theTestId]))
                            {
                                // Abort means user click the terminate btn
                                if (mainForm.ShowDialog() == DialogResult.Abort)
                                {
                                    runningTestDict.Remove(theTestId);
                                    break;
                                }
                            }
                        }
                    }
                    await RefreshRunningTest();
                    if (!matched)
                    {
                        MessageBox.Show(ErrorInfo.NoMatched.ErrorMessage);
                    }
                }
                else
                {
                    MessageBox.Show(restModel.ErrorMessage);
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
                Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            UseWaitCursor = false;
        }
        private void AppointmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !programClosing)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                notify.Visible = true;
                notify.ShowBalloonTip(1000);
            }
        }
        private void Notify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            notify.Visible = false;
        }
        private async void AppointRefreshTimer_Tick(object sender, EventArgs e)
        {
            await LoadAllAppointments();
            await RefreshRunningTest();
        }
        private async Task RefreshRunningTest()
        {
            try
            {
                ecgTestMod = await eResource.GetRunningTest(appointFormClient);
                if (ErrorInfo.OK.ErrorMessage == ecgTestMod.ErrorMessage)
                {
                    List<EcgTest> returnEls = CreateTestLs(ecgTestMod.Feed.Entities);
                    PatientInfo p;
                    runningTestDict.Clear();
                    inProgressTestLs.Items.Clear();
                    foreach (EcgTest returnE in returnEls)
                    {
                        runningTestDict.Add(returnE.EcgTestId, returnE);
                        pRestMod = await patientResource.GetPatientById(returnE.PatientId, appointFormClient);
                        p = pRestMod.Entity.Model;
                        var row = new string[] { p.PatientFirstName + "\t" + p.PatientLastName, returnE.ScheduledEndTime.ToString() };
                        var lisitem = new ListViewItem(row);
                        lisitem.Tag = returnE.EcgTestId;
                        inProgressTestLs.Invoke(new MethodInvoker(delegate { inProgressTestLs.Items.Add(lisitem); }));
                    }
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private async void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (weeklyCal.SelectedAppointment != null)
            {
                await DeleteAppointment(weeklyCal.SelectedAppointment.Appoint);
            }
        }
        private async void PalcmDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (patientAppointLs.SelectedItems[0] != null)
            {
                await DeleteAppointment((Uvic_Ecg_Model.Appointment)patientAppointLs.SelectedItems[0].Tag);
            }
        }
        private async void RunningTestRefreshTimer_Tick(object sender, EventArgs e)
        {
            await RefreshRunningTest();
        }
        private async void RawDataRefreshTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                await download.MainProcess(appointFormClient);
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private async Task DeleteAppointment(Uvic_Ecg_Model.Appointment deleteAppointment)
        {
            try
            {
                DialogResult res = MessageBox.Show(ErrorInfo.DeleteWarn.ErrorMessage, ErrorInfo.Caption.ErrorMessage, MessageBoxButtons.YesNo);
                if (DialogResult.No == res)
                {
                    return;
                }
                errorMsg = await nResource.DeleteAppointment(deleteAppointment, appointFormClient);
                if (ErrorInfo.OK.ErrorMessage == errorMsg)
                {
                    MessageBox.Show(ErrorInfo.Deleted.ErrorMessage);
                    await LoadAllAppointments();
                }
                else
                {
                    MessageBox.Show(errorMsg);
                }
            }
            catch (TokenExpiredException teex)
            {
                MessageBox.Show(teex.Message);
                programClosing = true;
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
        private void ContextMenu_Open(object sender, CancelEventArgs e)
        {
            e.Cancel = weeklyCal.SelectedAppointment == null;
        }
        private void PatientAppointLsContextMenu_Open(object sender, CancelEventArgs e)
        {
            e.Cancel = patientAppointLs.SelectedItems.Count <= 0;
        }
        private List<int> DeviceNotReturnedAppointments(IEnumerable<Uvic_Ecg_Model.Appointment> appointments)
        {
            var nullActualReturnAppointLs = appointments.Where(appoint => appoint.DeviceActualReturnTime == null);
            DateTime returnTime;
            List<int> notReturnedAppointIdLs = new List<int>();
            foreach(var nullActualReturnAppoint in nullActualReturnAppointLs)
            {
                if (nullActualReturnAppoint.DeferReturnTime != null)
                {
                    returnTime = nullActualReturnAppoint.DeferReturnTime.Value;
                }
                else
                {
                    returnTime = nullActualReturnAppoint.DeviceReturnDate.Value;
                }
                if (returnTime < DateTime.Now)
                {
                    notReturnedAppointIdLs.Add(nullActualReturnAppoint.AppointmentRecordId);
                }
            }
            return notReturnedAppointIdLs;
        }
    }
}
