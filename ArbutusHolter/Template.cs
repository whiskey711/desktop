using System;
using System.Windows.Forms;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class Template : Form
    {
        public String mailTemplate { get; set; }
        public Template()
        {
            InitializeComponent();
        }
        private void SaveTempBtn_Click(object sender, EventArgs e)
        {
            mailTemplate = tempRichTb.Text;
        }
    }
}
