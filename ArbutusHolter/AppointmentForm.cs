using Calendar;
using System;
using System.Collections.Generic;
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
        Uvic_Ecg_Model.Appointment app;
        DateTime startTime = DateTime.Now;
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
        string monthYear = "MMMM yyyy";
        string dateAndTime = "MM/dd/yyyy HH:mm";
        string allLocation = "All locations";
        string devLoc;
        string allLoc = "All locations";
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
            Task.Run(async () => await ClassifyDeviceLocation(appointFormClient));
            Task.Run(async () => await LoadAllAppointments());
            Task.Run(async () => await RefreshRunningTest());
            pNameCheckBox.Enabled = false;
            yearIndicateLab.Text = DateTime.Today.ToString(monthYear);
            appointRefreshTimer.Start();
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
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
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
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            patientAppointLs.Items.Clear();
            if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
            {
                returnAls = CreateAppointLs(restModel.Feed.Entities);
                startTimeFilt.Value = returnAls.First().AppointmentStartTime.Date;
                endTimeFilt.Value = returnAls.Last().AppointmentEndTime.Date;
                TimeFilt_Changed();
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
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
            {
                returnAls = CreateAppointLs(restModel.Feed.Entities);
                appointLs.Clear();
                if (pNameCheckBox.Checked)
                {
                    foreach (var returnA in returnAls)
                    {
                        appointStart = new Calendar.Appointment();
                        appointStart.StartDate = returnA.AppointmentStartTime;
                        appointStart.EndDate = appointStart.StartDate.AddMinutes(appointBlockMinLength);
                        appointStart.Color = Color.DeepSkyBlue;
                        appointStart.Appoint = returnA;
                        appointStart.Title = returnA.FirstName + " " + returnA.LastName;
                        appointEnd = new Calendar.Appointment();
                        appointEnd.StartDate = returnA.AppointmentEndTime;
                        appointEnd.EndDate = appointEnd.StartDate.AddMinutes(appointBlockMinLength);
                        appointEnd.Color = Color.Crimson;
                        appointEnd.Appoint = returnA;
                        appointEnd.Title = returnA.FirstName + " " + returnA.LastName;
                        if (returnA.PatientId == selectedP.PatientId)
                        {
                            appointLs.Add(appointStart);
                            appointLs.Add(appointEnd);
                        }    
                    }
                }
                else
                {
                    foreach (var returnA in returnAls)
                    {
                        appointStart = new Calendar.Appointment();
                        appointStart.StartDate = returnA.AppointmentStartTime;
                        appointStart.EndDate = appointStart.StartDate.AddMinutes(appointBlockMinLength);
                        appointStart.Color = Color.DeepSkyBlue;
                        appointStart.Appoint = returnA;
                        appointStart.Title = returnA.FirstName + " " + returnA.LastName;
                        appointEnd = new Calendar.Appointment();
                        appointEnd.StartDate = returnA.AppointmentEndTime;
                        appointEnd.EndDate = appointEnd.StartDate.AddMinutes(appointBlockMinLength);
                        appointEnd.Color = Color.Crimson;
                        appointEnd.Appoint = returnA;
                        appointEnd.Title = returnA.FirstName + " " + returnA.LastName;
                        appointLs.Add(appointStart);
                        appointLs.Add(appointEnd);
                    }
                }
                weeklyCal.StartDate = DateTime.Today;
                weeklyCal.NewAppointment += new NewAppointmentEventHandler(DayView_NewAppointment);
                weeklyCal.ResolveAppointments += new Calendar.ResolveAppointmentsEventHandler(this.DayView_ResolveAppointments);
                TimeFilt_Changed();
            }
            else
            {
                MessageBox.Show(restModel.ErrorMessage);
            }
            weeklyCal.DaysToShow = 7;
            weeklyCal.Invalidate();
        }
        private async Task ClassifyDeviceLocation(Client client)
        {
            dRestMod = await dResource.GetAllDevice(client);
            if (dRestMod.ErrorMessage == ErrorInfo.OK.ErrorMessage)
            {
                returnDevLocLs = CreateDeviceLocLs(dRestMod.Feed.Entities);
                foreach (var returnDevLoc in returnDevLocLs)
                {
                    regionComboBox.Items.Add(returnDevLoc);
                }
                regionComboBox.Items.Add(allLocation);
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
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        private void AppointmentF_Load(object sender, EventArgs e) { }
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
            TimeFilt_Changed();
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
            TimeFilt_Changed();
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
                        returnA.FirstName + " " + returnA.LastName,
                        returnA.AppointmentStartTime.ToString(dateAndTime),
                        returnA.AppointmentEndTime.ToString(dateAndTime)
                        };
                    var lsitem = new ListViewItem(row);
                    lsitem.Tag = returnA;
                    patientAppointLs.Items.Add(lsitem);
                }
                weeklyCal.StartDate = startTimeFilt.Value;
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        private async void WeeklyCal_Click(object sender, EventArgs e)
        {
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
                if (selectedA.EcgTestId.HasValue && runningTestDict.TryGetValue(selectedA.EcgTestId.Value, out EcgTest test))
                {
                    await ShowAppointDetailFormForInProgressAppoint(selectedA, selectedA.EcgTestId.Value);
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
                }
                LoadAllAppointments();
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
            if (selectedA.EcgTestId.HasValue && runningTestDict.TryGetValue(selectedA.EcgTestId.Value, out EcgTest test))
            {
                await ShowAppointDetailFormForInProgressAppoint(selectedA, selectedA.EcgTestId.Value);
            }
            else if (selectedA.EcgTestId.HasValue && !runningTestDict.TryGetValue(selectedA.EcgTestId.Value, out test))
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
                if (allLoc.Equals(devLoc))
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
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        private async void PatientAppointLs_SelectedIndexChanged(object sender, EventArgs e)
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
                await SrhPatient(name[1], name[0], null, null, selectedA.PatientId);
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
                    await RefreshRunningTest();
                    MessageBox.Show(ErrorInfo.TestTerminated.ErrorMessage);
                }
            }
        }
        private async Task ShowAppointDetailFormForFinishedAppoint(Uvic_Ecg_Model.Appointment theApp)
        {
            try
            {
                UseWaitCursor = true;
                ecgTestMod = await eResource.GetTestById(theApp.EcgTestId.Value, theApp.PatientId, appointFormClient);
                pRestMod = await patientResource.GetPatientById(theApp.PatientId, appointFormClient);
                UseWaitCursor = false;
                if (ErrorInfo.OK.ErrorMessage == ecgTestMod.ErrorMessage && ErrorInfo.OK.ErrorMessage == pRestMod.ErrorMessage)
                {
                    EcgTest test = ecgTestMod.Entity.Model;
                    PatientInfo patient = pRestMod.Entity.Model;
                    AppointmentDetailsForm appDForm = new AppointmentDetailsForm(appointFormClient, theApp, test, patient);
                    appDForm.Show();
                }
                else
                {
                    MessageBox.Show(ErrorInfo.ConnectionProblem.ErrorMessage);
                }
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
                restModel = await nResource.GetPatientAppoint(runningTestDict[theTestId].PatientId, appointFormClient);
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            UseWaitCursor = false;
            if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
            {
                returnAls = CreateAppointLs(restModel.Feed.Entities);
                foreach (var returnA in returnAls)
                {
                    if (theTestId == returnA.EcgTestId)
                    {
                        using (TestMonitorForm mainForm = new TestMonitorForm(appointFormClient, returnA, runningTestDict[theTestId]))
                        {
                            // Abort means user click the terminate btn
                            if (mainForm.ShowDialog() == DialogResult.Abort)
                            {
                                await RefreshRunningTest();
                                runningTestDict.Remove(theTestId);
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(restModel.ErrorMessage);
            }
        }
        private void AppointmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
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
                        inProgressTestLs.Items.Add(lisitem);
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

    }
}
