using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Uvic_Ecg_Model
{
    public class ErrorInfo
    {
        private string errorMessage;
        public ErrorInfo(string errorMsg)
        {
            this.errorMessage = errorMsg;
        }
        public static readonly ErrorInfo DeviceReturned = new ErrorInfo("Device has been returned");
        public static readonly ErrorInfo FillAll = new ErrorInfo("Please fill in all required field");
        public static readonly ErrorInfo OK = new ErrorInfo("OK");
        public static readonly ErrorInfo Failed = new ErrorInfo("Login failed");
        public static readonly ErrorInfo Confirm = new ErrorInfo("Please confirm your password");
        public static readonly ErrorInfo Complete = new ErrorInfo("Thanks, you have completed");
        public static readonly ErrorInfo Incorrect = new ErrorInfo("Incorrect code");
        public static readonly ErrorInfo WrongDate = new ErrorInfo("Please enter correct date");
        public static readonly ErrorInfo NoResult = new ErrorInfo("No result");
        public static readonly ErrorInfo WrongPhn = new ErrorInfo("Invalid phn");
        public static readonly ErrorInfo WrongMail = new ErrorInfo("Invalid mail address");
        public static readonly ErrorInfo Created = new ErrorInfo("Created successfully");
        public static readonly ErrorInfo Updated = new ErrorInfo("Updated successfully");
        public static readonly ErrorInfo SelectPatient = new ErrorInfo("Please select a patient first");
        public static readonly ErrorInfo Occuiped = new ErrorInfo("All device have been occupied");
        public static readonly ErrorInfo SelectDeivce = new ErrorInfo("Please select a device");
        public static readonly ErrorInfo Before = new ErrorInfo("Device pick up time has to be earlier than appointment start time");
        public static readonly ErrorInfo Later = new ErrorInfo("Device return time has to be later than appointment end time");
        public static readonly ErrorInfo DeviceLoc = new ErrorInfo("Please enter device location");
        public static readonly ErrorInfo TimeConflict = new ErrorInfo("Start time should be earlier than end time");
        public static readonly ErrorInfo RegistrationComlete = new ErrorInfo("Thank you for completing the registration. You may log in now.");
        public static readonly ErrorInfo EarlyThanNow = new ErrorInfo("Device pick-up time is earlier than now");
        public static readonly ErrorInfo TerminateWarn = new ErrorInfo("Are you sure to terminate ongoing test ?");
        public static readonly ErrorInfo Caption = new ErrorInfo("?");
        public static readonly ErrorInfo DownloadExTooMuch = new ErrorInfo("Background downloading process had unknown exception and stopped, please contact support");
        public static readonly ErrorInfo ClosingWarn = new ErrorInfo("Are youy sure to close the form ?");
        public static readonly ErrorInfo ReordingStart = new ErrorInfo("The ECG test has begun");
        public static readonly ErrorInfo TestTerminated = new ErrorInfo("The ECG test has been terminated");
        public static readonly ErrorInfo OngoingTest = new ErrorInfo("Sorry, the test is in progress, you cannot hoopkup now");
        public static readonly ErrorInfo PinTooShort = new ErrorInfo("Sorry, your password has to be longer than 10 digits");
        public static readonly ErrorInfo ConnectionProblem = new ErrorInfo("There are some internet connection problem, please try it later");
        public static readonly ErrorInfo Deleted = new ErrorInfo("Appointment is deleted successfully");
        public static readonly ErrorInfo DeleteWarn = new ErrorInfo("Are your sure to delete the appointment ?");
        public static readonly ErrorInfo DownloadAllData = new ErrorInfo("This is the first time you use the software, do you want to sync all data locally from the server ?");
        public static readonly ErrorInfo TokenExpired = new ErrorInfo("Your token has expired, please login again");
        public static readonly ErrorInfo NoMatched = new ErrorInfo("No matched appointemnt");
        public static readonly ErrorInfo Transmitting = new ErrorInfo("Data is transmitting. Please wait");
        public static readonly ErrorInfo NoHookupOrRecord = new ErrorInfo("Please start hook up or record first");
        public static readonly ErrorInfo ChangesDone = new ErrorInfo("The changes on the current patient has successfully saved");
        public static readonly ErrorInfo PathNotFound = new ErrorInfo("Cannot find CER-S application, please check path");
        public static readonly ErrorInfo DownloadProblem = new ErrorInfo("Unable to download data now, try again later");
        public static readonly ErrorInfo GenerateProblem = new ErrorInfo("Unable to gernerate report for this appointment, please contact support");
        public static readonly ErrorInfo Downloading = new ErrorInfo("System is downloading data and generate report, please wait");

        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
    }
}
