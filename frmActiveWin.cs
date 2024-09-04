using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmActiveWin : Form
    {
        public frmActiveWin()
        {
            InitializeComponent();
        }

        private void frmActiveWin_Load(object sender, EventArgs e)
        {

        }

        //Move the mouse 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //Simulate left mouse button press 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //Simulate the left mouse button up 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //Simulate the right mouse button press 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //Simulate the right mouse button up 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //Simulate the middle mouse button press 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //Simulate the middle mouse button up 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //Mark whether to use absolute coordinates 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        //Simulate the scrolling operation of the mouse wheel, must cooperate with the dwData parameter
        const int MOUSEEVENTF_WHEEL = 0x0800;
        static uint DOWN = 0x0002;
        static uint UP = 0x0004;
        //static uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        //static uint MOUSEEVENTF_MOVE = 0x0001;
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        static void MouseMove()
        {
            Thread.Sleep(100);
            if ((Count % 2) == 0)
            {
                //mouse_event(DOWN | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, 800, 800, 0, 0);
                mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            }
            else
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                //Thread.Sleep(100);
                //mouse_event(UP | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, 800, 800, 0, 0);
            }
        }

        static int Count = 0;
        static int intervalTime = 60000;
        static int intervalmin = 2;
        private int waitTime = intervalTime * intervalmin;
        private DateTime startTime;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private void btnStart_Click(object sender, EventArgs e)
        {
            // t = new Timer { Interval = 1000 };
            rtbText.Text = String.Format("0{0}", intervalmin);
            timer.Interval = intervalTime;
            timer.Tick += timer_Tick;
            timer.Start(); // start timer (you can do it on form load, if you need)
            startTime = DateTime.Now; // and remember start time
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int elapsedTime = (int)(DateTime.Now - startTime).TotalMinutes;
            int remainingTime = intervalmin - elapsedTime;
            string pre = string.Empty;
            if (remainingTime <= 0)
            {
                Count++;
                // run your function
                //timer.Stop();
                MouseMove();
                startTime = DateTime.Now;
                remainingTime = intervalmin;
            }
            if (remainingTime < 10)
            {
                pre = "0";
            }

            rtbText.Text = String.Format("{0}",(pre + remainingTime));
               // String.Format("{0} seconds remaining...", remainingSeconds);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

       
    }
}
