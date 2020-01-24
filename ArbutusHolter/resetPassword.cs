using System;
using System.IO;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class ResetPassword : Form
    {
        private PublicResources publicResources = new PublicResources();
        private Client resetPswFormClinet;
        private Nurse updatedNurse;
        private string errorMsg;
        public ResetPassword(Client client, Nurse nurse)
        {
            InitializeComponent();
            resetPswFormClinet = client;
            updatedNurse = nurse;
        }
        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newPswd.Text))
                {
                    MessageBox.Show(ErrorInfo.FillAll.ErrorMessage);
                    return;
                }
                if (!newPswd.Text.Equals(newPswdConfirm.Text))
                {
                    MessageBox.Show(ErrorInfo.Confirm.ErrorMessage);
                    return;
                }
                updatedNurse.Password = newPswd.Text;
                errorMsg = publicResources.ResetPassword(updatedNurse, resetPswFormClinet).ErrorMessage;
                if (ErrorInfo.OK.ErrorMessage.Equals(errorMsg))
                {
                    MessageBox.Show(ErrorInfo.Complete.ErrorMessage);
                    Close();
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
                    LogHandle.Log(ex.Message, ex.StackTrace, w);
                }
            }
        }
    }
}
