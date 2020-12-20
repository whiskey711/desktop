using System;
using System.IO;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class ForgetPwForm : Form
    {
        private PublicResources publicResources = new PublicResources();
        private Client forgetPwFormClinet;
        private RestModel<Nurse> restModel;
        private Nurse nurse;
        private string errorMsg;
        public ForgetPwForm(Client client)
        {
            forgetPwFormClinet = client;
            InitializeComponent();
        }
        // After user click submitButton, the finishPanel is visible
        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            try
            {
                errorMsg = await publicResources.ForgetPassword(mailTextbox.Text, forgetPwFormClinet);
                if (!ErrorInfo.OK.ErrorMessage.Equals(errorMsg))
                {
                    MessageBox.Show(errorMsg);
                }
                else
                {
                    finishPanel.Visible = true;
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
            UseWaitCursor = false;
        }
        // After user clicked back button, they will be directed to loginForm
        private async void Confirm_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            try
            {
                if (string.IsNullOrWhiteSpace(CodeText.Text))
                {
                    MessageBox.Show(ErrorInfo.FillAll.ErrorMessage);
                    return;
                }
                restModel = await publicResources.VeriFycode(CodeText.Text, forgetPwFormClinet);
                errorMsg = restModel.ErrorMessage;
                if (ErrorInfo.OK.ErrorMessage.Equals(errorMsg))
                {
                    MessageBox.Show(ErrorInfo.Updated.ErrorMessage);
                    this.Hide();
                    nurse = restModel.Entity.Model;
                    ResetPassword rpForm = new ResetPassword(forgetPwFormClinet, nurse);
                    rpForm.Show();
                }
                else
                {
                    MessageBox.Show(ErrorInfo.Incorrect.ErrorMessage);
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
            UseWaitCursor = false;
        }
        private void Back_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
