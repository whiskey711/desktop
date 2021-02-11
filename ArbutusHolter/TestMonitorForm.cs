using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using Uvic_Ecg_ArbutusHolter.EcgRawDataProcessing;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class TestMonitorForm : Form
    {
        bool displayBtnclicked = false;
        int h, m, s, timeToWait;
        int fiftySec = 50000;
        int fiveSec = 5000;
        int halfSec = 500;
        int oneSec = 1000;
        int five = 5;
        int seventy = 70;
        Client mainFormClient;
        PatientResource patientResource = new PatientResource();
        EcgDataResources ecgDataResources = new EcgDataResources();
        DeviceResource devResource = new DeviceResource();
        DataProcess dataProcessForChannel1 = new DataProcess();
        DataProcess dataProcessForChannel2 = new DataProcess();
        RestModel<PatientInfo> restmodel;
        RestModel<ResultJson> eRestMod;
        List<PatientInfo> returnPls = new List<PatientInfo>();
        RestModel<EcgRawData> ecgRawDataModel;
        bool statusChanges = false;
        string errorMsg;
        string status;
        string dot = ".";
        string triplets = "...";
        string display = "DISPLAY";
        string pause = "PAUSE";
        string timeFormat = "HH:mm:ss";
        string hookup = "hookup";
        string record = "record";
        string hookupStatus = "Hookup";
        string recordingStatus = "Recording";
        float[] dataInfloat1;
        float[] dataInfloat2;
        int[] dataofChannelOne;
        int[] dataofChannelTwo;
        DateTime date;
        DateTime tempDate;
        PatientInfo updatedPatient;
        bool isFirstTime = true;
        bool recordingStarted = false;
        bool programClosing = false;
        int aDay = 24;
        public EcgTest theEcgTest { get; set; }
        public Appointment theAppoint { get; set; }
        public TestMonitorForm(Client client, Appointment app, EcgTest test)
        {
            InitializeComponent();
            date = Convert.ToDateTime("1996/11/10");
            mainFormClient = client;
            theEcgTest = test;
            status = "";
            theAppoint = app;
            loadingTimer.Interval = oneSec;
            nowTimer.Interval = oneSec;
            nextCalltimer.Interval = oneSec;
            nowTimer.Interval = oneSec;
            countTimer.Interval = oneSec;
            waitingTimer.Interval = oneSec;
            RestoreStatus();
            Task.Run(async () => await PatientInfo_Load());
        }
        /// <summary>
        /// Restore ecg test status
        /// </summary>
        private void RestoreStatus()
        {
            if (theEcgTest != null)
            {
                isFirstTime = false;
                if (theEcgTest.HookupStatus)
                {
                    date = date.AddYears(2);
                    ecgStartBtn.Enabled = true;
                    terminateBtn.Enabled = false;
                    recordBtn.Enabled = true;
                    hookupBtn.Enabled = false;
                    status = hookup;
                    statusChanges = true;
                    statusFlag.Text = hookupStatus;
                }
                else if (theEcgTest.RecordingStatus)
                {
                    indicatorLed.Blink(halfSec);
                    recordBtn.Enabled = false;
                    hookupBtn.Enabled = true;
                    status = record;
                    statusChanges = true;
                    statusFlag.Text = recordingStatus;
                }
                timeLabel.Text = theEcgTest.StartTime.ToString(timeFormat);
                if (theEcgTest.ScheduledEndTime != null)
                {
                    endTimeLabel.Text = theEcgTest.ScheduledEndTime.ToString(timeFormat);
                }
                h = (DateTime.Now - theEcgTest.StartTime).Hours;
                m = (DateTime.Now - theEcgTest.StartTime).Minutes;
                s = (DateTime.Now - theEcgTest.StartTime).Seconds;
                countTimer.Start();
                nowTimer.Stop();
            }
            else
            {
                ecgStartBtn.Enabled = false;
                terminateBtn.Enabled = false;
                recordBtn.Enabled = false;
            }
        }
        /// <summary>
        /// Create ecgtest and assign it to theEcgTest and theAppoint
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CreateEcgTest()
        {
            theEcgTest = new EcgTest(theAppoint.AppointmentStartTime, theAppoint.AppointmentEndTime, null, theAppoint.AppointmentRecordId,
                                     theAppoint.Patient.PatientId, theAppoint.Nurse.NurseId, theAppoint.Device.DeviceId,
                                     null, Config.ClinicId);
            try
            {
                eRestMod = await ecgDataResources.CreateEcgtest(mainFormClient, theEcgTest);
                if (ErrorInfo.OK.ErrorMessage != eRestMod.ErrorMessage)
                {
                    MessageBox.Show(eRestMod.ErrorMessage);
                    return false;
                }
                theEcgTest.EcgTestId = int.Parse(eRestMod.Entity.Model.Message);
                theAppoint.EcgTest = theEcgTest;
                if (DateTime.Compare(theEcgTest.StartTime.AddHours(aDay), theAppoint.AppointmentEndTime) < 0)
                {
                    endTimeLabel.Invoke(new MethodInvoker(delegate
                    {
                        endTimeLabel.Text = theEcgTest.StartTime.AddHours(aDay).ToString(timeFormat);
                    }));
                }
                else
                {
                    endTimeLabel.Invoke(new MethodInvoker(delegate
                    {
                        endTimeLabel.Text = theAppoint.AppointmentEndTime.ToString(timeFormat);
                    }));
                }
                return true;
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
            return false;
        }
        /// <summary>
        /// Load patient information of corresponding appointment
        /// </summary>
        /// <returns></returns>
        private async Task PatientInfo_Load()
        {
            try
            {
                restmodel = await patientResource.GetPatient(theAppoint.Patient.PatientLastName, theAppoint.Patient.PatientFirstName,
                                                             null, null, mainFormClient);
                if (restmodel.ErrorMessage == ErrorInfo.OK.ErrorMessage)
                {
                    foreach (var ent in restmodel.Feed.Entities)
                    {
                        returnPls.Add(ent.Model);
                    }
                    foreach (PatientInfo p in returnPls)
                    {
                        if (p.PatientId == theAppoint.Patient.PatientId)
                        {
                            firstNameTB.Invoke(new MethodInvoker(delegate { firstNameTB.Text = p.PatientFirstName; }));
                            midNameTB.Invoke(new MethodInvoker(delegate { midNameTB.Text = p.PatientMidName; }));
                            lastNameTB.Invoke(new MethodInvoker(delegate { lastNameTB.Text = p.PatientLastName; }));
                            birthDateTB.Invoke(new MethodInvoker(delegate { birthDateTB.Text = p.Birthdate; }));
                            phnTB.Invoke(new MethodInvoker(delegate { phnTB.Text = p.Phn; }));
                            genderTB.Invoke(new MethodInvoker(delegate { genderTB.Text = p.Gender; }));
                            ageTB.Invoke(new MethodInvoker(delegate { ageTB.Text = p.Age; }));
                            address1TB.Invoke(new MethodInvoker(delegate
                            {
                                address1TB.Text = p.Address1;
                            }));
                            address2TB.Invoke(new MethodInvoker(delegate
                            {
                                address2TB.Text = p.Address2;
                            }));
                            cityTB.Invoke(new MethodInvoker(delegate
                            {
                                cityTB.Text = p.City;
                            }));
                            provinceTB.Invoke(new MethodInvoker(delegate
                            {
                                provinceTB.Text = p.Province;
                            }));
                            postCodeTB.Invoke(new MethodInvoker(delegate
                            {
                                postCodeTB.Text = p.PostCode;
                            }));
                            phoneNumTB.Invoke(new MethodInvoker(delegate
                            {
                                phoneNumTB.Text = p.PhoneNumber;
                            }));
                            homeNumTB.Invoke(new MethodInvoker(delegate
                            {
                                homeNumTB.Text = p.HomeNumber;
                            }));
                            mailTB.Invoke(new MethodInvoker(delegate
                            {
                                mailTB.Text = p.Email;
                            }));
                            pacemakerTB.Invoke(new MethodInvoker(delegate
                            {
                                pacemakerTB.Text = p.Pacemaker;
                            }));
                            superPhyTB.Invoke(new MethodInvoker(delegate
                            {
                                superPhyTB.Text = p.SuperPhysician;
                            }));
                            medTB.Invoke(new MethodInvoker(delegate
                            {
                                medTB.Text = p.Medications;
                            }));
                            remarkRichTextBox.Invoke(new MethodInvoker(delegate
                            {
                                remarkRichTextBox.Text = p.Remark;
                            }));
                            break;
                        }
                    }
                    firstNameTB.Invoke(new MethodInvoker(delegate { firstNameTB.ReadOnly = true; }));
                    midNameTB.Invoke(new MethodInvoker(delegate
                    {
                        midNameTB.ReadOnly = true;
                    }));
                    lastNameTB.Invoke(new MethodInvoker(delegate
                    {
                        lastNameTB.ReadOnly = true;
                    }));
                    birthDateTB.Invoke(new MethodInvoker(delegate
                    {
                        birthDateTB.ReadOnly = true;
                    }));
                    phnTB.Invoke(new MethodInvoker(delegate
                    {
                        phnTB.ReadOnly = true;
                    }));
                    genderTB.Invoke(new MethodInvoker(delegate
                    {
                        genderTB.ReadOnly = true;
                    }));
                    ageTB.Invoke(new MethodInvoker(delegate
                    {
                        ageTB.ReadOnly = true;
                    }));
                    address1TB.Invoke(new MethodInvoker(delegate
                    {
                        address1TB.ReadOnly = true;
                    }));
                    address2TB.Invoke(new MethodInvoker(delegate
                    {
                        address2TB.ReadOnly = true;
                    }));
                    cityTB.Invoke(new MethodInvoker(delegate
                    {
                        cityTB.ReadOnly = true;
                    }));
                    provinceTB.Invoke(new MethodInvoker(delegate
                    {
                        provinceTB.ReadOnly = true;
                    }));
                    postCodeTB.Invoke(new MethodInvoker(delegate
                    {
                        postCodeTB.ReadOnly = true;
                    }));
                    phoneNumTB.Invoke(new MethodInvoker(delegate
                    {
                        phoneNumTB.ReadOnly = true;
                    }));
                    homeNumTB.Invoke(new MethodInvoker(delegate
                    {
                        homeNumTB.ReadOnly = true;
                    }));
                    mailTB.Invoke(new MethodInvoker(delegate
                    {
                        mailTB.ReadOnly = true;
                    }));
                    pacemakerTB.Invoke(new MethodInvoker(delegate
                    {
                        pacemakerTB.ReadOnly = true;
                    }));
                    superPhyTB.Invoke(new MethodInvoker(delegate
                    {
                        superPhyTB.ReadOnly = true;
                    }));
                    medTB.Invoke(new MethodInvoker(delegate
                    {
                        medTB.ReadOnly = true;
                    }));
                    remarkRichTextBox.Invoke(new MethodInvoker(delegate
                    {
                        remarkRichTextBox.ReadOnly = true;
                    }));
                }
                else
                {
                    MessageBox.Show(restmodel.ErrorMessage);
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
        /// <summary>
        /// count the recording duration time
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
        /// <summary>
        /// Concatenate two data section
        /// </summary>
        /// <param name="highbits"></param>
        /// <param name="lowbits"></param>
        /// <returns></returns>
        private int Concate(byte highbits, byte lowbits)
        {
            byte convertionBits = Convert.ToByte(0b_0011_1111);
            highbits = (byte)(highbits & convertionBits);
            return ((highbits << 8) | lowbits);
        }
        /// <summary>
        /// Display button clicked, determine current status and call startDisplay()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EcgStartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // 'pause' btn clicked, stop and clean ecg animation
                if (displayBtnclicked)
                {
                    StopTimers();
                    return;
                }
                else if (status.Equals(""))
                {
                    MessageBox.Show(ErrorInfo.NoHookupOrRecord.ErrorMessage);
                    return;
                }
                ecgStartBtn.Invoke(new MethodInvoker(delegate
                {
                    ecgStartBtn.Enabled = false;
                }));
                if (isFirstTime)
                {
                    isFirstTime = false;
                    timeToWait = five;
                }
                else
                {
                    timeToWait = seventy;
                }
                waitingTimer.Start();
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
        /// <summary>
        /// call updateData() and start nextCallTimer
        /// </summary>
        /// <returns></returns>
        private async Task StartDisplay()
        {
            if (!displayBtnclicked)
            {
                bool b = await TryToGetData();
                loadingTimer.Stop();
                if (!b)
                {
                    MessageBox.Show(ErrorInfo.Transmitting.ErrorMessage);
                    return;
                }
                UpdateData();
                if (status == record)
                {
                    nextCalltimer.Interval = fiftySec;
                }
                else if (status == hookup)
                {
                    nextCalltimer.Interval = fiveSec;
                }
                nextCalltimer.Start();
                channel1.StartTick();
                channel2.StartTick();
                displayBtnclicked = true;
                ecgStartBtn.Invoke(new MethodInvoker(delegate
                {
                    ecgStartBtn.Text = pause;
                }));
            }
            else
            {
                //stop and clean the data
                channel1.StopTick();
                channel2.StopTick();
                CleanChannel();
                displayBtnclicked = false;
                ecgStartBtn.Invoke(new MethodInvoker(delegate
                {
                    ecgStartBtn.Text = display;
                }));
            }
        }
        private async Task<bool> TryToGetData()
        {
            for (int i=0; i<3; i++)
            {
                ecgRawDataModel = await ecgDataResources.GetEcgData(mainFormClient, status,
                                                                theAppoint.Patient.PatientId,
                                                                theAppoint.EcgTest.EcgTestId);
                if (ecgRawDataModel.Entity.Model != null)
                {
                    return true;
                }
                await Task.Delay(5000);
            }
            return false;
        }
        /// <summary>
        /// Drawing data to ecg animation
        /// </summary>
        /// <param name="ecgRawDataModel"></param>
        private void DoUpdate(RestModel<EcgRawData> ecgRawDataModel)
        {
            while (true)
            {
                if (ecgRawDataModel.ErrorMessage.Equals(ErrorInfo.OK.ErrorMessage))
                {
                    if (ecgRawDataModel.Entity.Model == null)
                    {
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
        /// <summary>
        /// Clean ecg animation
        /// </summary>
        private void CleanChannel()
        {
            channel1.CleanTheData();
            channel2.CleanTheData();
        }
        /// <summary>
        /// Get data from serv and call DoUpdata()
        /// </summary>
        /// <returns></returns>
        private void UpdateData()
        {
            try
            {
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
                    // which means status is recording and old and new data has different time
                    if (!tempDate.Equals(date) && status.Equals(hookup))
                    {
                        // tempDate is time of 5s new data; date is time of 1m old data
                        if ((tempDate - date).TotalSeconds > 10)
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
        private void NowTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToString(timeFormat);
        }
        /// <summary>
        /// nextCallTimer tick function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NextCalltimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (displayBtnclicked)
                {
                    bool b = await TryToGetData();
                    if (b)
                    {
                        UpdateData();
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
        /// <summary>
        /// hook up btn clicked, and change ecgtest to hookup status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void HookupBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (theAppoint.EcgTest == null)
                {
                    bool b = await CreateEcgTest();
                    if (!b)
                    {
                        return;
                    }
                    nowTimer.Stop();
                    countTimer.Start();
                }
                RestModel<ResultJson> result = await ecgDataResources.SetHookup(mainFormClient, theAppoint.EcgTest.EcgTestId, theAppoint.Device.DeviceId);
                var test = result.ErrorMessage;
                //indicatorLed stop blink
                indicatorLed.Blink(0);
                StopTimers();
                status = hookup;
                statusChanges = true;
                statusFlag.Invoke(new MethodInvoker(delegate
                {
                    statusFlag.Text = hookupStatus;
                }));
                hookupBtn.Invoke(new MethodInvoker(delegate
                {
                    hookupBtn.Enabled = false;
                }));
                recordBtn.Invoke(new MethodInvoker(delegate
                {
                    recordBtn.Enabled = true;
                }));
                ecgStartBtn.Invoke(new MethodInvoker(delegate
                {
                    ecgStartBtn.Enabled = true;
                }));
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
        private void LoadingTimer_Tick(object sender, EventArgs e)
        {
            if (waitTimeLabel.Text == triplets)
            {
                waitTimeLabel.Text = dot;
            }
            else
            {
                waitTimeLabel.Text += dot;
            }
        }
        /// <summary>
        /// interval = 1s, decrement int timeToWait, and change waitTimeLabel
        /// Once timeToWait == 0, call StartDisplay()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void WaitingTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                timeToWait--;
                waitTimeLabel.Text = timeToWait.ToString();
                if (timeToWait == 0)
                {
                    waitingTimer.Stop();
                    waitTimeLabel.Text = dot;
                    loadingTimer.Start();
                    ecgStartBtn.Enabled = true;
                    await StartDisplay();
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
        /// <summary>
        /// stop and clean ecg animation
        /// </summary>
        private void StopTimers()
        {
            try
            {
                channel1.StopTick();
                channel2.StopTick();
                channel1.CleanView();
                channel2.CleanView();
                CleanChannel();
                displayBtnclicked = false;
                ecgStartBtn.Text = display;
                nextCalltimer.Stop();
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
        private async void RecordButton_Click(object sender, EventArgs e)
        {
            try
            {
                theEcgTest.StartTime = DateTime.Now;
                theEcgTest.ScheduledEndTime = DateTime.Now.AddHours(aDay);
                eRestMod = await ecgDataResources.UpdateEcgTest(mainFormClient, theEcgTest);
                if (!ErrorInfo.OK.ErrorMessage.Equals(eRestMod.ErrorMessage))
                {
                    MessageBox.Show(eRestMod.ErrorMessage);
                    return;
                }
                RestModel<ResultJson> result = await ecgDataResources.SetRecord(mainFormClient, theEcgTest.EcgTestId, theEcgTest.DeviceId);
                var test = result.ErrorMessage;
                indicatorLed.Blink(halfSec);
                if (status.Equals(hookup))
                {
                    StopTimers();
                }
                status = record;
                statusChanges = true;
                statusFlag.Invoke(new MethodInvoker(delegate
                {
                    statusFlag.Text = recordingStatus;
                }));
                terminateBtn.Invoke(new MethodInvoker(delegate
                {
                    terminateBtn.Enabled = true;
                }));
                recordBtn.Invoke(new MethodInvoker(delegate
                {
                    recordBtn.Enabled = false;
                }));
                hookupBtn.Invoke(new MethodInvoker(delegate
                {
                    hookupBtn.Enabled = true;
                }));
                recordingStarted = true;

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
        private async void TerminateBtn_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(ErrorInfo.TerminateWarn.ErrorMessage, ErrorInfo.Caption.ErrorMessage, MessageBoxButtons.OKCancel);
            if (res == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                RestModel<ResultJson> result = await ecgDataResources.Terminated(mainFormClient, theEcgTest.EcgTestId, theEcgTest.DeviceId);
                var test = result.ErrorMessage;
                DialogResult = DialogResult.Abort;
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
        private async void SaveRemarkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                updatedPatient = new PatientInfo(theAppoint.Patient.PatientId, lastNameTB.Text, midNameTB.Text, firstNameTB.Text, birthDateTB.Text, address1TB.Text,
                                                 address2TB.Text, provinceTB.Text, cityTB.Text, mailTB.Text, phnTB.Text, phoneNumTB.Text, null, homeNumTB.Text,
                                                 genderTB.Text, postCodeTB.Text, false, 1, pacemakerTB.Text, superPhyTB.Text,
                                                 null, null, null, null, null, remarkRichTextBox.Text, ageTB.Text);
                errorMsg = await patientResource.UpdatePatient(updatedPatient, mainFormClient);
                if (errorMsg == ErrorInfo.OK.ErrorMessage)
                {
                    MessageBox.Show(ErrorInfo.ChangesDone.ErrorMessage);
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
        private void MainInterface_FormClosing(object sender, FormClosingEventArgs e)
        {  
            if (e.CloseReason == CloseReason.UserClosing && !programClosing)
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
