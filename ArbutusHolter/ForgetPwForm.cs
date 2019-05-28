using System;
using System.Windows.Forms;

namespace ArbutusHolter
{
    public partial class ForgetPwForm : Form
    {
        public ForgetPwForm()
        {
            InitializeComponent();
        }

        // after user click submitButton, the finishPanel is visible
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            finishPanel.Visible = true;            
        }

        //after user clicked back button, they will be directed to loginForm
        private void back_Click(object sender, EventArgs e)
        {
            Close();
            LoginForm loginF = new LoginForm();
            loginF.Show();
        }
    }
}
