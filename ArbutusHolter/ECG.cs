using System;
using System.Windows.Forms;
namespace Uvic_Ecg_ArbutusHolter
{
    public partial class ECG : Form
    {
        public ECG()
        {
            InitializeComponent();
            ecgView.PlotAll();
        }
        private void LeftBtn_Click(object sender, EventArgs e)
        {
            ecgView.MoveRight();
        }
        private void RightBtn_Click(object sender, EventArgs e)
        {
            ecgView.MoveLeft();
        }
    }
}
