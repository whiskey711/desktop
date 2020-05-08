using Calendar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class AppointmentForm : Form
    {
        List<Calendar.Appointment> appointLs = new List<Calendar.Appointment>();
        Calendar.Appointment appoint, appointStart, appointEnd;
        List<Uvic_Ecg_Model.Appointment> returnAls = new List<Uvic_Ecg_Model.Appointment>();
        List<String> returnDevLocLs = new List<String>();
        Uvic_Ecg_Model.Appointment app;
        DateTime startTime = DateTime.Now;
        RestModel<Uvic_Ecg_Model.Appointment> restModel;
        RestModel<PatientInfo> pRestMod;
        RestModel<ResultJson> eRestMod;
        RestModel<Device> dRestMod;
        NurseResource nResource = new NurseResource();
        PatientResource patientResource = new PatientResource();
        EcgDataResources eResource = new EcgDataResources();
        DeviceResource dResource = new DeviceResource();
        Client appointFormClient;
        bool createIndicator = false;
        string errorMsg;
        PatientInfo selectedP;
        Uvic_Ecg_Model.Appointment selectedA;
        PatientInfo updatedPatient;
        EcgTest ecgTest;
        int occupiedDev = 0;
        int devUpLimit = 5;
        long num;
        int startAfterTo, endBeforeFrom;
        DateTime thisYearStart = DateTime.Parse("1/1/" + DateTime.Today.Year);
        DateTime thisYeaarEnd = DateTime.Parse("12/31/" + DateTime.Today.Year);
        int appointBlockMinLength = 15;
        int invalidPid = -1;
        string monthYear = "MMMM yyyy";
        string dateAndTime = "MM/dd/yyyy HH:mm";
        string allLocation = "All locations";
        string devLoc;
        public AppointmentForm(Client client)
        {
            InitializeComponent();
            SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.StandardDoubleClick, false);
            PatientDetailsGroup.Enabled = false;
            startTimeFilt.Value = DateTime.Today;
            endTimeFilt.Value = DateTime.Today.AddDays(7);
            appointFormClient = client;
            LoadAllAppointments(appointFormClient);
            ClassifyDeviceLocation(appointFormClient);
            pNameCheckBox.Enabled = false;
            yearIndicateLab.Text = DateTime.Today.ToString(monthYear);
        }
        private void SrhBtn_Click(object sender, EventArgs e)
        {
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
                SrhPatient(pLastNameTextBox.Text, pFirstNameTextBox.Text, birthText.Text, phnTextBox.Text, invalidPid);
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void SrhPatient(string lastName, string firstName, string birth, string phn, int pid)
        {
            patientListView.Items.Clear();
            pRestMod = patientResource.GetPatient(lastName, firstName, birth, phn, appointFormClient);
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
                            LoadPatientInfo(returnP, pid);
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
            ClearText(PatientDetailsGroup);
            createIndicator = true;
            PatientDetailsGroup.Enabled = true;
            PatientDetailsGroup.Focus();
        }
        private void PatientListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (patientListView.SelectedItems.Count <= 0)
                {
                    return;
                }
                selectedP = (PatientInfo)patientListView.SelectedItems[0].Tag;
                LoadPatientInfo(selectedP, invalidPid);
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void LoadPatientInfo(PatientInfo theOne, int pid)
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
            genderTB.Text = theOne.Gender;
            postCodeTB.Text = theOne.PostCode;
            pacemakerTB.Text = theOne.Pacemaker;
            superPhyTB.Text = theOne.SuperPhysician;
            ageTB.Text = theOne.Age;
            PatientDetailsGroup.Enabled = true;
            saveBtn.Enabled = true;
            pNameCheckBox.Text = theOne.PatientFirstName + " " + theOne.PatientLastName;
            pNameCheckBox.CheckState = CheckState.Checked;
            pNameCheckBox.Enabled = true;
            if (pid != invalidPid)
            {
                return;
            }
            restModel = nResource.GetPatientAppoint(theOne.PatientId, appointFormClient);
            patientAppointLs.Items.Clear();
            if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
            {
                returnAls = CreateAppointLs(restModel.Feed.Entities);
                foreach (var returnA in returnAls)
                {
                    var row = new string[]
                    {
                        theOne.PatientFirstName + " " + theOne.PatientLastName,
                        returnA.AppointmentStartTime.Value.ToString(dateAndTime),
                        returnA.AppointmentEndTime.Value.ToString(dateAndTime)
                    };
                    var lsitem = new ListViewItem(row);
                    lsitem.Tag = returnA;
                    patientAppointLs.Items.Add(lsitem);
                }
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
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(lastNameTB.Text) &&
                        !string.IsNullOrWhiteSpace(firstNameTB.Text) &&
                        !string.IsNullOrWhiteSpace(birthDateTB.Text) &&
                        !string.IsNullOrWhiteSpace(address1TB.Text) &&
                        !string.IsNullOrWhiteSpace(provinceTB.Text) &&
                        !string.IsNullOrWhiteSpace(phnTB.Text) &&
                        !string.IsNullOrWhiteSpace(genderTB.Text) &&
                        !string.IsNullOrWhiteSpace(cityTB.Text))
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
                    if (createIndicator)
                    {
                        PatientInfo newPatient = new PatientInfo(lastNameTB.Text, midNameTB.Text, firstNameTB.Text, replaceDate, address1TB.Text, null,
                                                                 provinceTB.Text, cityTB.Text, mailTB.Text, phnTB.Text, phoneNumTB.Text, null, homeNumTB.Text,
                                                                 genderTB.Text, postCodeTB.Text, false, 1, pacemakerTB.Text, superPhyTB.Text,
                                                                 null, null, null, null, null, null, ageTB.Text);
                        errorMsg = patientResource.CreatePatient(newPatient, appointFormClient);
                        if (errorMsg == ErrorInfo.OK.ErrorMessage)
                        {
                            MessageBox.Show(ErrorInfo.Created.ErrorMessage);
                        }
                        else
                        {
                            MessageBox.Show(errorMsg);
                        }
                        createIndicator = false;
                    }
                    else
                    {
                        updatedPatient = new PatientInfo(selectedP.PatientId, lastNameTB.Text, midNameTB.Text, firstNameTB.Text, replaceDate, address1TB.Text,
                                                         null, provinceTB.Text, cityTB.Text, mailTB.Text, phnTB.Text, phoneNumTB.Text, null, homeNumTB.Text,
                                                         genderTB.Text, postCodeTB.Text, false, 1, pacemakerTB.Text, superPhyTB.Text,
                                                         null, null, null, null, null, null, ageTB.Text);
                        errorMsg = patientResource.UpdatePatient(updatedPatient, appointFormClient);
                        if (errorMsg == ErrorInfo.OK.ErrorMessage)
                        {
                            MessageBox.Show(ErrorInfo.Updated.ErrorMessage);
                        }
                        else
                        {
                            MessageBox.Show(errorMsg);
                        }
                    }
                    saveBtn.Enabled = false;
                    PatientDetailsGroup.Enabled = false;
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
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
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
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
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
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void LoadAllAppointments(Client client)
        {
            restModel = nResource.GetAppointments(client, thisYearStart, thisYeaarEnd);
            if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
            {
                returnAls = CreateAppointLs(restModel.Feed.Entities);
                appointLs.Clear();
                foreach (var returnA in returnAls)
                {
                    appointStart = new Calendar.Appointment();
                    appointStart.StartDate = returnA.AppointmentStartTime.Value;
                    appointStart.EndDate = appointStart.StartDate.AddMinutes(appointBlockMinLength);
                    appointStart.Color = Color.DeepSkyBlue;
                    appointStart.Appoint = returnA;
                    appointStart.Title = returnA.FirstName + " " + returnA.FirstName;
                    appointEnd = new Calendar.Appointment();
                    appointEnd.StartDate = returnA.AppointmentEndTime.Value;
                    appointEnd.EndDate = appointEnd.StartDate.AddMinutes(appointBlockMinLength);
                    appointEnd.Color = Color.Crimson;
                    appointEnd.Appoint = returnA;
                    appointEnd.Title = returnA.FirstName + " " + returnA.LastName;
                    appointLs.Add(appointStart);
                    appointLs.Add(appointEnd);
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
        private void ClassifyDeviceLocation(Client client)
        {
            dRestMod = dResource.GetAllDevice(client);
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
        private void AddAppointBtn_Click(object sender, EventArgs e)
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
                AddNewAppoinment();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
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
                startTimeFilt.Value = endTimeFilt.Value;
            }
            if (pNameCheckBox.Checked)
            {
                pNameCheckBox.CheckState = CheckState.Unchecked;
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
                endTimeFilt.Value = startTimeFilt.Value;
            }
            if (pNameCheckBox.Checked)
            {
                pNameCheckBox.CheckState = CheckState.Unchecked;
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
                    startAfterTo = DateTime.Compare(returnA.AppointmentStartTime.Value.Date, endTimeFilt.Value.Date);
                    endBeforeFrom = DateTime.Compare(returnA.AppointmentEndTime.Value.Date, startTimeFilt.Value.Date);
                    if (startAfterTo > 0 || endBeforeFrom < 0)
                    {
                        continue; 
                    }
                    var row = new string[]
                        {
                        returnA.FirstName + " " + returnA.LastName,
                        returnA.AppointmentStartTime.Value.ToString(dateAndTime),
                        returnA.AppointmentEndTime.Value.ToString(dateAndTime)
                        };
                    var lsitem = new ListViewItem(row);
                    lsitem.Tag = returnA;
                    patientAppointLs.Items.Add(lsitem);
                }
                dayViewMonthlyCal.SelectionStart = startTimeFilt.Value;
                dayViewMonthlyCal.SelectionEnd = startTimeFilt.Value;
                weeklyCal.StartDate = startTimeFilt.Value;
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void WeeklyCal_Click(object sender, EventArgs e)
        {
            if (weeklyCal.SelectedAppointment == null)
            {
                if (!pNameCheckBox.Checked)
                {
                    MessageBox.Show(ErrorInfo.SelectPatient.ErrorMessage);
                    return;
                }
                AddNewAppoinment();
            }
            else
            {
                appoint = weeklyCal.SelectedAppointment;
                weeklyCal.Enabled = false;
                using (AppointmentDetailsForm appDForm = new AppointmentDetailsForm(appointFormClient, appoint.Appoint,
                                                                    appoint.Appoint.FirstName + " " + appoint.Appoint.LastName))
                {
                    if (appDForm.ShowDialog() == DialogResult.OK)
                    {
                        // Remove the selected appoint to add updated one later
                        List<int> index = new List<int>();
                        foreach (var a in appointLs)
                        {
                            if (a.Appoint.AppointmentRecordId == appoint.Appoint.AppointmentRecordId)
                            {
                                index.Add(appointLs.IndexOf(a));
                            }
                        }
                        appointLs.RemoveAt(index[1]);
                        appointLs.RemoveAt(index[0]);
                        app = new Uvic_Ecg_Model.Appointment((int)appoint.Appoint.AppointmentRecordId, 1, (int)appoint.Appoint.PatientId, appDForm.selectDev.DeviceId,
                                                    appDForm.startTime, appDForm.endTime, (DateTime)appoint.Appoint.ReservationTime, appDForm.pickTime,
                                                    appDForm.returnTime, appDForm.deviceLoc, (string)appoint.Appoint.Instruction, false, 1,
                                                    (string)appoint.Appoint.FirstName, (string)appoint.Appoint.LastName, (int?)appoint.Appoint.EcgTestId);
                        try
                        {
                            errorMsg = nResource.UpdateAppointment(app, appointFormClient);
                        }
                        catch (Exception ex)
                        {
                            using (StreamWriter w = File.AppendText(FileName.Log.Name))
                            {
                                LogHandle.Log(ex.Message, ex.StackTrace, w);
                            }
                        }
                        if (ErrorInfo.OK.ErrorMessage.Equals(errorMsg))
                        {
                            appointStart = new Calendar.Appointment();
                            appointStart.StartDate = appDForm.startTime;
                            appointStart.EndDate = appointStart.StartDate.AddMinutes(appointBlockMinLength);
                            appointStart.Color = Color.DeepSkyBlue;
                            appointStart.Title = occupiedDev.ToString();
                            appointEnd = new Calendar.Appointment();
                            appointEnd.StartDate = appDForm.endTime;
                            appointEnd.EndDate = appointEnd.StartDate.AddMinutes(appointBlockMinLength);
                            appointEnd.Color = Color.Crimson;
                            appointEnd.Title = occupiedDev.ToString();
                            appointLs.Add(appointStart);
                            appointLs.Add(appointEnd);
                            appointStart.Appoint = app;
                            appointEnd.Appoint = app;
                            MessageBox.Show(ErrorInfo.Updated.ErrorMessage);
                        }
                    }
                    weeklyCal.Enabled = true;
                }
                weeklyCal.Invalidate();
            }
        }
        private void DayViewMonthlyCal_DateSelected(object sender, DateRangeEventArgs e)
        {
            weeklyCal.StartDate = dayViewMonthlyCal.SelectionRange.Start;
            yearIndicateLab.Text = dayViewMonthlyCal.SelectionRange.Start.ToString(monthYear);
            startTimeFilt.Value = weeklyCal.StartDate;
            endTimeFilt.Value = startTimeFilt.Value.AddDays(6);
            TimeFilt_Changed();
        }
        private void EditMailBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Email emailForm = new Email(appointFormClient);
                emailForm.Show();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void PNameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckedChanged();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void CheckedChanged()
        {
            if (pNameCheckBox.Checked)
            {
                return;
            }
            patientAppointLs.Items.Clear();
            patientListView.Items.Clear();
            ClearText(PatientDetailsGroup);
            ClearText(srhGroup);
            pNameCheckBox.Text = "Clinic";
            pNameCheckBox.Enabled = false;
            LoadAllAppointments(appointFormClient);
        }
        private void PatientAppointLs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (patientAppointLs.SelectedItems.Count <= 0)
            {
                return;
            }
            selectedA = (Uvic_Ecg_Model.Appointment)patientAppointLs.SelectedItems[0].Tag;
            using (AppointmentDetailsForm appDForm = new AppointmentDetailsForm(appointFormClient, selectedA,
                                                                                selectedA.FirstName + " " + selectedA.LastName))
            {
                if (appDForm.ShowDialog() == DialogResult.OK)
                {
                    // Remove the selected appoint to add updated one later
                    List<int> index = new List<int>();
                    foreach (var a in appointLs)
                    {
                        if (a.Appoint.AppointmentRecordId == selectedA.AppointmentRecordId)
                        {
                            index.Add(appointLs.IndexOf(a));
                        }
                    }
                    appointLs.RemoveAt(index[1]);
                    appointLs.RemoveAt(index[0]);
                    app = new Uvic_Ecg_Model.Appointment((int)selectedA.AppointmentRecordId, 1, (int)selectedA.PatientId, appDForm.selectDev.DeviceId,
                                                appDForm.startTime, appDForm.endTime, (DateTime)selectedA.ReservationTime, appDForm.pickTime,
                                                appDForm.returnTime, appDForm.deviceLoc, (string)selectedA.Instruction, false, 1,
                                                (string)selectedA.FirstName, (string)selectedA.LastName, (int?)selectedA.EcgTestId);
                    try
                    {
                        errorMsg = nResource.UpdateAppointment(app, appointFormClient);
                    }
                    catch (Exception ex)
                    {
                        using (StreamWriter w = File.AppendText(FileName.Log.Name))
                        {
                            LogHandle.Log(ex.Message, ex.StackTrace, w);
                        }
                    }
                    if (ErrorInfo.OK.ErrorMessage.Equals(errorMsg))
                    {
                        appointStart = new Calendar.Appointment();
                        appointStart.StartDate = appDForm.startTime;
                        appointStart.EndDate = appointStart.StartDate.AddMinutes(appointBlockMinLength);
                        appointStart.Color = Color.DeepSkyBlue;
                        appointStart.Title = occupiedDev.ToString();
                        appointEnd = new Calendar.Appointment();
                        appointEnd.StartDate = appDForm.endTime;
                        appointEnd.EndDate = appointEnd.StartDate.AddMinutes(appointBlockMinLength);
                        appointEnd.Color = Color.Crimson;
                        appointEnd.Title = occupiedDev.ToString();
                        appointLs.Add(appointStart);
                        appointLs.Add(appointEnd);
                        appointStart.Appoint = app;
                        appointEnd.Appoint = app;
                        MessageBox.Show(ErrorInfo.Updated.ErrorMessage);
                    }
                }
            }
        }

        private void RegionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                devLoc = regionComboBox.SelectedItem.ToString();
                pNameCheckBox.CheckState = CheckState.Unchecked;
                weeklyCal.Invalidate();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
        private void PatientAppointLs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (patientAppointLs.SelectedItems.Count <= 0)
                {
                    return;
                }
                selectedA = (Uvic_Ecg_Model.Appointment)patientAppointLs.SelectedItems[0].Tag;
                weeklyCal.StartDate = selectedA.AppointmentStartTime.Value;
                string[] name = patientAppointLs.SelectedItems[0].Text.Split(' ');
                SrhPatient(name[1], name[0], null, null, selectedA.PatientId);
                yearIndicateLab.Text = selectedA.AppointmentStartTime.Value.ToString(monthYear);
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
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
        private void AddNewAppoinment()
        {
            try
            {
                using (AppointmentDetailsForm appointDForm = new AppointmentDetailsForm(appointFormClient, null, selectedP.PatientFirstName + " " + selectedP.PatientLastName))
                {
                    if (appointDForm.ShowDialog() == DialogResult.OK)
                    {
                        ecgTest = new EcgTest(appointDForm.startTime, appointDForm.endTime, null, selectedP.PatientId, 1, 1,
                                              appointDForm.selectDev.DeviceId, 1, null, false, false, false);
                        eRestMod = eResource.CreateEcgtest(appointFormClient, ecgTest);
                        if (eRestMod.ErrorMessage != ErrorInfo.OK.ErrorMessage)
                        {
                            MessageBox.Show(eRestMod.ErrorMessage);
                            return;
                        }
                        app = new Uvic_Ecg_Model.Appointment(1, selectedP.PatientId, appointDForm.selectDev.DeviceId, appointDForm.startTime, appointDForm.endTime,
                                          DateTime.Now, appointDForm.pickTime, appointDForm.returnTime, appointDForm.deviceLoc, null, false, 1,
                                          selectedP.PatientFirstName, selectedP.PatientLastName, int.Parse(eRestMod.Entity.Model.Message));
                        errorMsg = nResource.CreateAppointment(app, appointFormClient);
                        if (errorMsg == ErrorInfo.OK.ErrorMessage)
                        {
                            occupiedDev++;
                            appointStart = new Calendar.Appointment();
                            appointStart.StartDate = appointDForm.startTime;
                            appointStart.EndDate = appointStart.StartDate.AddMinutes(appointBlockMinLength);
                            appointStart.Color = Color.DeepSkyBlue;
                            appointStart.Title = app.FirstName + " " + app.LastName;
                            appointEnd = new Calendar.Appointment();
                            appointEnd.StartDate = appointDForm.endTime;
                            appointEnd.EndDate = appointEnd.StartDate.AddMinutes(appointBlockMinLength);
                            appointEnd.Color = Color.Crimson;
                            appointEnd.Title = app.FirstName + " " + app.LastName;
                            appointStart.Appoint = app;
                            appointEnd.Appoint = app;
                            appointLs.Add(appointStart);
                            appointLs.Add(appointEnd);
                            MessageBox.Show(ErrorInfo.Created.ErrorMessage);
                        }
                        else
                        {
                            MessageBox.Show(errorMsg);
                        }
                        restModel = nResource.GetAppointments(appointFormClient, thisYearStart, thisYeaarEnd);
                        if (restModel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                        {
                            returnAls = CreateAppointLs(restModel.Feed.Entities);
                        }
                        else
                        {
                            MessageBox.Show(restModel.ErrorMessage);
                        }
                        weeklyCal.Invalidate();
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
    }
}
