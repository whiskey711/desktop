using System;
using System.Windows.Forms;
using Uvic_Ecg_ArbutusHolter.HttpRequests;
using Uvic_Ecg_Model;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class ErrorForm : Form
    {
        NurseResource nurseResource = new NurseResource();
        RestModel<Nurse> restmodel;
        Client erroFormClient;
        public ErrorForm(string erroMsg,Client client)
        {
            InitializeComponent();
            msgLabel.Text = erroMsg;
            erroFormClient = client;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm lForm = new LoginForm();
            lForm.Show(this);
        }
        
    }
}
