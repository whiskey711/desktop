using System;
using System.Windows.Forms;

namespace ArbutusHolter
{
    public partial class MainInterface : Form
    {
        bool startBtnBool;
        private srhForm sForm;
        int h, m, s;

        public MainInterface()
        {
            InitializeComponent();
            startBtnBool = true;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            indicatorLed.Blink(500);
            channel1.DrawTimer_Stop();
            channel2.DrawTimer_Stop();
            countTImer.Start();
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

            nowTimer.Stop();
        }


        private void ecgStartBtn_Click(object sender, EventArgs e)
        {
            if (startBtnBool == true)
            {
                channel1.DrawTimer_Start();
                channel2.Generate_Diff_RamNum();
                channel2.DrawTimer_Start();
                startBtnBool = false;
                ecgStartBtn.Text = "PAUSE";
            }
            else
            {
                channel1.DrawTimer_Stop();
                channel2.DrawTimer_Stop();
                startBtnBool = true;
                ecgStartBtn.Text = "CONTINUE";
            }
        }

        private void nowTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }

            
        
    }
}
