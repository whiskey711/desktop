using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArbutusHolter
{
    public partial class Record : Form
    {
        public Record()
        {
            InitializeComponent();
        }

       

        private void patientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //should change to Select them from database
            if (patientList.SelectedItem.ToString() == "James Xu 2019-02-01 10000")
            {
                LnLabel.Text = "Xu";
                FnLabel.Text = "James";
                DobLabel.Text = "2019-02-01";
                PhnLabel.Text = "10000";
                PnLabel.Text = "7787787778";
                AddrLabel.Text = "Uvic";

                ecgRecordList.Items.Add(patientList.SelectedItem);
            }
            if (patientList.SelectedItem.ToString() == "Charles Liu 2019-01-01 10001")
            {
                LnLabel.Text = "Liu";
                FnLabel.Text = "Charles";
                DobLabel.Text = "2019-01-01";
                PhnLabel.Text = "10001";
                PnLabel.Text = "2502502250";
                AddrLabel.Text = "Uvic";

                ecgRecordList.Items.Add(patientList.SelectedItem);
            }
            //MessageBox.Show(patientList.SelectedItem.ToString());
        }

        private void btnOfSearch_Click(object sender, EventArgs e)
        {
            if (txtOfFN.Text == "select FirstName from database" && txtOfLN.Text == "Select LastName from databse" && txtOfBOD.Text == "Select BOD from databse" && txtOfPhn.Text == "Select phn from databse")
            {
                patientList.Items.Add(txtOfFN.Text + " " + txtOfLN.Text + " " + txtOfBOD.Text + " " + txtOfPhn.Text);
            }
            //just used to demo.
            patientList.Items.Add(txtOfFN.Text + " " + txtOfLN.Text + " " + txtOfBOD.Text + " " + txtOfPhn.Text);
        }

        

        private void ecgRecordList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ECG eForm = new ECG();
            eForm.Show();
        }
    }
}
