using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArbutusHolter
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }


        // after user click registerButton, the finishPanel is visible
        private void registerButton_Click(object sender, EventArgs e)
        {
            finishPanel.Visible = true;
        }

        // after user clicked back button, they will be directed to loginForm
        private void back_Click(object sender, EventArgs e)
        {
            Close();
            LoginForm loginF = new LoginForm(); 
            loginF.Show();
        }
    }
}
