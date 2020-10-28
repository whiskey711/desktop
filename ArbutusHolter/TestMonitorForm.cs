using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.EcgRawDataProcessing;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class TestMonitorForm : Form
    {
        bool displayBtnclicked;
        int h, m, s, nextCallTimer, timeToWait;
        Client mainFormClient;
        PatientResource patientResource = new PatientResource();
        EcgDataResources ecgDataResources = new EcgDataResources();
        DeviceResource devResource = new DeviceResource();
        DataProcess dataProcessForChannel1 = new DataProcess();
        DataProcess dataProcessForChannel2 = new DataProcess();
        RestModel<PatientInfo> restmodel;
        RestModel<ResultJson> eRestMod;
        List<PatientInfo> returnPls = new List<PatientInfo>();
        bool statusChanges = false;
        string errorMsg;
        string status;
        float[] dataInfloat1;
        float[] dataInfloat2;
        int[] dataofChannelOne;
        int[] dataofChannelTwo;
        DateTime date;
        DateTime tempDate;
        PatientInfo updatedPatient;
        bool isFirstTime = true;
        bool recordingStarted = false;
        int aDay = 24;
        public EcgTest theEcgTest { get; set; }
        public Appointment theAppoint { get; set; }
        public TestMonitorForm(Client client, Appointment app, EcgTest test)
        {
            InitializeComponent();
            displayBtnclicked = false;
            date = Convert.ToDateTime("1996/11/10");
            mainFormClient = client;
            theEcgTest = test;
            status = "";
            theAppoint = app;
            PatientInfo_Load();
            if (test != null)
            {
                indicatorLed.Blink(500);
                recordBtn.Enabled = false;
                // ToDo: Duration and start time should be restored
            }
            else
            {
                ecgStartBtn.Enabled = false;
                terminateBtn.Enabled = false;
                recordBtn.Enabled = false;
            }
        }
        private bool CreateEcgTest()
        {
            theEcgTest = new EcgTest(theAppoint.AppointmentStartTime, theAppoint.AppointmentEndTime, null, theAppoint.PatientId, theAppoint.NurseId, theAppoint.DeviceId, null, theAppoint.AppointmentRecordId, Config.ClinicId);
            try
            {
                eRestMod = ecgDataResources.CreateEcgtest(mainFormClient, theEcgTest);
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
            if (ErrorInfo.OK.ErrorMessage != eRestMod.ErrorMessage)
            {
                MessageBox.Show(eRestMod.ErrorMessage);
                return false;
            }
            theEcgTest.EcgTestId = int.Parse(eRestMod.Entity.Model.Message);
            theAppoint.EcgTestId = int.Parse(eRestMod.Entity.Model.Message);
            if (DateTime.Compare(theEcgTest.StartTime.AddHours(aDay), theAppoint.AppointmentEndTime) < 0)
            {
                endTimeLabel.Text = theEcgTest.StartTime.AddHours(aDay).ToString("hh:mm:ss tt");
            }
            else
            {
                endTimeLabel.Text = theAppoint.AppointmentEndTime.ToString("hh:mm:ss tt");
            }
            return true;
        }
        private void PatientInfo_Load()
        {
            try
            {
                restmodel = patientResource.GetPatient(theAppoint.LastName, theAppoint.FirstName, null, null, mainFormClient);
                if (restmodel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    foreach (var ent in restmodel.Feed.Entities)
                    {
                        returnPls.Add(ent.Model);
                    }
                    foreach (PatientInfo p in returnPls)
                    {
                        if (p.PatientId == theAppoint.PatientId)
                        {
                            firstNameTB.Text = p.PatientFirstName;
                            midNameTB.Text = p.PatientMidName;
                            lastNameTB.Text = p.PatientLastName;
                            birthDateTB.Text = p.Birthdate;
                            phnTB.Text = p.Phn;
                            genderTB.Text = p.Gender;
                            ageTB.Text = p.Age;
                            address1TB.Text = p.Address1;
                            address2TB.Text = p.Address2;
                            cityTB.Text = p.City;
                            provinceTB.Text = p.Province;
                            postCodeTB.Text = p.PostCode;
                            phoneNumTB.Text = p.PhoneNumber;
                            homeNumTB.Text = p.HomeNumber;
                            mailTB.Text = p.Email;
                            pacemakerTB.Text = p.Pacemaker;
                            superPhyTB.Text = p.SuperPhysician;
                            medTB.Text = p.Medications;
                            remarkRichTextBox.Text = p.Remark;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(restmodel.ErrorMessage);
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
        /// <summary>
        /// make next Call for get EcgData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextCall(object sender, EventArgs e)
        {
            try
            {
                if (true.Equals(displayBtnclicked))
                {
                    if (statusChanges)
                    {
                        statusChanges = false;
                        nextCallTimer = 0;
                    }
                    nextCallTimer++;
                    if (status.Equals("hookup"))
                    {
                        if (nextCallTimer == 5)
                        {
                            if (TryToGetData())
                            {
                                UpdateData();
                            }
                            nextCallTimer = 0;
                        }
                    }
                    else if (status.Equals("record"))
                    {
                        if (nextCallTimer == 50)
                        {
                            UpdateData();
                            nextCallTimer = 0;
                        }
                    }
                    else
                    {
                        //show please start hook up or record first
                        //done in somewhere else
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
        /// <summary>
        /// count the start recording time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountTimer_Tick(object sender, EventArgs e)
        {
            s++;
            if (s >= 60)
            {
                s = 0;
                m++;
            }
            if (m >= 60)
            {
                m = 0;
                h++;
            }
            durationLabel.Text = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
        }
        private int Concate(byte highbits, byte lowbits)
        {
            byte convertionBits = Convert.ToByte(0b_0011_1111);
            highbits = (byte)(highbits & convertionBits);
            return ((highbits << 8) | lowbits);
        }
        /// <summary>
        /// display button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EcgStartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (displayBtnclicked)
                {
                    StopTimers();
                    return;
                }
                else if (status.Equals(""))
                {
                    MessageBox.Show("please start hook up or record first");
                    return;
                }
                else if (!isFirstTime)
                {
                    StartDisplay();
                    return;
                }
                else if (status.Equals("record"))// && !TryToGetData()
                {
                    timeToWait = 70;
                }
                else if (status.Equals("rehookup"))
                {
                    timeToWait = 70;
                }
                else if (!TryToGetData())
                {
                    MessageBox.Show("No data is transmitting. Please check connection");
                    return;
                }
                else if (status.Equals("hookup"))//&& !TryToGetData()
                {
                    //timeToWait = 10;
                    StartDisplay();
                    return;
                }
                ecgStartBtn.Enabled = false;
                isFirstTime = false;
                waitingTimer.Start();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        private void StartDisplay()
        {
            if (displayBtnclicked == false)
            {
                UpdateData();
                nextCalltimer.Start();
                channel1.StartTick();
                channel2.StartTick();
                displayBtnclicked = true;
                ecgStartBtn.Text = "PAUSE";
            }
            else
            {
                //stop and clean the data
                channel1.StopTick();
                channel2.StopTick();
                CleanChannel();
                displayBtnclicked = false;
                ecgStartBtn.Text = "DISPLAY";
            }
        }
        private bool TryToGetData()
        {
            RestModel<EcgRawData> ecgRawDataModel = ecgDataResources.GetEcgData(mainFormClient, status, theAppoint.PatientId, theAppoint.EcgTestId.Value);
            if (ecgRawDataModel.Entity == null)
            {
                return false;
            }
            else if (ecgRawDataModel.Entity.Model == null)
            {
                return false;
            }
            return true;
        }
        private void DoUpdate(RestModel<EcgRawData> ecgRawDataModel)
        {
            while (true)
            {
                if (ecgRawDataModel.ErrorMessage.Equals(ErrorInfo.OK.ErrorMessage))
                {
                    if (ecgRawDataModel.Entity.Model == null)
                    {
                        //MessageBox.Show("No Data is transmitting now");
                        break;
                    }
                    EcgRawData ecgRawData = ecgRawDataModel.Entity.Model;
                    string stringOfData = ecgRawData.RawData;
                    byte[] data = Convert.FromBase64String(stringOfData);
                    int length = (data.Length - data.Length % 5) / 5;
                    dataofChannelOne = new int[length];
                    dataofChannelTwo = new int[length];
                    for (int i = 0; i < length; i++)
                    {
                        dataofChannelOne[i] = Concate(data[(5 * i) + 1], data[(5 * i) + 2]);
                        dataofChannelTwo[i] = Concate(data[(5 * i) + 3], data[(5 * i) + 4]);
                    }
                    dataInfloat1 = new float[dataofChannelOne.Length];
                    dataInfloat2 = new float[dataofChannelTwo.Length];
                    for (int i = 0; i < dataofChannelOne.Length; i++)
                    {
                        dataInfloat1[i] = (float)dataProcessForChannel1.dataConvert(dataofChannelOne[i]);
                    }
                    for (int i = 0; i < dataofChannelOne.Length; i++)
                    {
                        dataInfloat2[i] = (float)dataProcessForChannel2.dataConvert(dataofChannelTwo[i]);
                    }
                    var test = dataInfloat1;
                    channel1.UpdateValue(dataInfloat1);
                    channel2.UpdateValue(dataInfloat2);
                    break;
                }
            }
        }
        private void CleanChannel()
        {
            channel1.CleanTheData();
            channel2.CleanTheData();
        }
        private void UpdateData()
        {
            RestModel<EcgRawData> ecgRawDataModel = ecgDataResources.GetEcgData(mainFormClient, status, theAppoint.PatientId, theAppoint.EcgTestId.Value);
            //Here, we are trying to update the newest data in reHookup peroid, so we have to determine which one is the newest data.
            //determine it's the first data or not.PS, the date would be stored as Nov.10th 1996 before the first data arrive.
            if (date.Year == 1996)
            {
                //if it's first data, update the date to be the first data's date.
                date = Convert.ToDateTime(ecgRawDataModel.Entity.Model.StartTime);
                DoUpdate(ecgRawDataModel);
            }
            else
            {
                //if it's not the first data, record the date of data in tempDate for future comparision.
                tempDate = Convert.ToDateTime(ecgRawDataModel.Entity.Model.StartTime);
                if (status.Equals("rehookup"))
                {
                    date = tempDate;
                    status = "hookup";
                    DoUpdate(ecgRawDataModel);
                }
                else
                {
                    if (!tempDate.Equals(date) && status.Equals("hookup"))
                    {
                        if ((tempDate - date).TotalSeconds > 10)
                        {
                            //if it's not duplicate with the last data and it's the faster frequence data, clean the view and update data and date.
                            var test = channel1.values;
                            CleanChannel();
                            test = channel1.values;
                            date = tempDate;
                            DoUpdate(ecgRawDataModel);
                        }
                        else
                        {
                            date = tempDate;
                            DoUpdate(ecgRawDataModel);
                        }
                    }
                    else
                    {
                        date = tempDate;
                        DoUpdate(ecgRawDataModel);
                    }
                }
            }
        }
        private void NowTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            NextCall(sender, e);
        }
        /// <summary>
        /// hook up btn clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HookupBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (theAppoint.EcgTestId.HasValue)
                {
                    MessageBox.Show(ErrorInfo.OngoingTest.ErrorMessage);
                    return;
                }
                else
                {
                    if (!CreateEcgTest())
                    {
                        return;
                    }
                    recordBtn.Enabled = true;
                    ecgStartBtn.Enabled = true;
                }
                RestModel<ResultJson> result = ecgDataResources.SetHookup(mainFormClient, theAppoint.EcgTestId.Value, theAppoint.DeviceId);
                var test = result.ErrorMessage;
                //indicatorLed stop blink
                if (status.Equals("record"))
                {
                    StopTimers();
                    status = "rehookup";
                }
                else
                {
                    status = "hookup";
                }
                statusChanges = true;
                statusFlag.Text = status;
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        private void WaitingTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                timeToWait--;
                waitTimeLabel.Text = timeToWait.ToString();
                if (timeToWait == 0)
                {
                    waitingTimer.Stop();
                    ecgStartBtn.Enabled = true;
                    if (!TryToGetData())
                    {
                        MessageBox.Show("No data is transmitting check your connection");
                        return;
                    }
                    StartDisplay();
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
        private void StopTimers()
        {
            try
            {
                //stop and clean the data
                channel1.StopTick();
                channel2.StopTick();
                channel1.CleanView();
                channel2.CleanView();
                CleanChannel();
                displayBtnclicked = false;
                ecgStartBtn.Text = "DISPLAY";
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        /// <summary>
        /// click the record button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                theEcgTest.StartTime = DateTime.Now;
                theEcgTest.ScheduledEndTime = DateTime.Now.AddHours(aDay);
                //theEcgTest.EcgTestId = 28; //test only, remove later
                eRestMod = ecgDataResources.UpdateEcgTest(mainFormClient, theEcgTest);
                if (!ErrorInfo.OK.ErrorMessage.Equals(eRestMod.ErrorMessage))
                {
                    MessageBox.Show(eRestMod.ErrorMessage);
                    return;
                }
                RestModel<ResultJson> result = ecgDataResources.SetRecord(mainFormClient, theEcgTest.EcgTestId, theEcgTest.DeviceId);
                var test = result.ErrorMessage;
                indicatorLed.Blink(500);
                countTImer.Start();
                if (status.Equals("hookup") || status.Equals("rehookup"))
                {
                    StopTimers();
                }
                status = "record";
                statusChanges = true;
                statusFlag.Text = status;
                nowTimer.Stop();
                terminateBtn.Enabled = true;
                recordingStarted = true;
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        private void TerminateBtn_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(ErrorInfo.TerminateWarn.ErrorMessage, ErrorInfo.Caption.ErrorMessage, MessageBoxButtons.OKCancel);
            if (res == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                RestModel<ResultJson> result = ecgDataResources.Terminated(mainFormClient, theEcgTest.EcgTestId, theEcgTest.DeviceId);
                var test = result.ErrorMessage;
                StopTimers();
                //indicatorLed stop blink
                indicatorLed.Blink(0);
                status = "terminated";
                statusChanges = true;
                statusFlag.Text = status;
                terminateBtn.Enabled = false;
                DialogResult = DialogResult.Abort;
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(FileName.Log.Name))
                {
                    LogHandle.Log(ex.ToString(), ex.StackTrace, w);
                }
            }
        }
        private void SaveRemarkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                updatedPatient = new PatientInfo(theAppoint.PatientId, lastNameTB.Text, midNameTB.Text, firstNameTB.Text, birthDateTB.Text, address1TB.Text,
                                                 address2TB.Text, provinceTB.Text, cityTB.Text, mailTB.Text, phnTB.Text, phoneNumTB.Text, null, homeNumTB.Text,
                                                 genderTB.Text, postCodeTB.Text, false, 1, pacemakerTB.Text, superPhyTB.Text,
                                                 null, null, null, null, null, remarkRichTextBox.Text, ageTB.Text);
                errorMsg = patientResource.UpdatePatient(updatedPatient, mainFormClient);
                if (errorMsg == ErrorInfo.OK.ErrorMessage)
                {
                    MessageBox.Show("The changes on the current patient has successfully saved");
                }
                else
                {
                    MessageBox.Show(errorMsg);
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
        private void MainInterface_FormClosing(object sender, FormClosingEventArgs e)
        {  
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult res = MessageBox.Show(ErrorInfo.ClosingWarn.ErrorMessage, ErrorInfo.Caption.ErrorMessage, MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (recordingStarted)
                {
                    DialogResult = DialogResult.Yes;
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }          
            }
        }
    }
}
