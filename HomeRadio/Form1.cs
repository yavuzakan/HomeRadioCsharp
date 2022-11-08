using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace HomeRadio
{
    //NAudio - Nuget  
    public partial class Form1 : Form
    {
        private IWavePlayer waveOut;
        private Mp3FileReader mp3FileReader;
        public string[] songs = { };
        public List<string> songlist = new List<string>();

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        // RegisterHotKey(this.Handle, this.GetType().GetHashCode(), 1, (int)'A')


        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        const int MYACTION_HOTKEY_ID = 1;
        const int MYACTION_HOTKEY_ID2 = 2;
        const int MYACTION_HOTKEY_ID3 = 3;
        const int MYACTION_HOTKEY_ID4 = 4;

        public Form1()
        {
            InitializeComponent();
            allsong();
            PlayMp3();

            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, 0, (int)Keys.F11);
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID2, 0, (int)Keys.F10);
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID3, 0, (int)Keys.F9);
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID4, 0, (int)Keys.F8);

        }
        public void allsong()
        {
            /*
            List<int> termsList = new List<int>();
            for (int runs = 0; runs < 400; runs++)
            {
                termsList.Add(value);
            }

            // You can convert it back to an array if you would like to
            int[] terms = termsList.ToArray();
             
             */
            DirectoryInfo d = new DirectoryInfo(@"mp3"); //Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles("*.mp3"); //Getting Text files
            string str = "";
            
            foreach (FileInfo file in Files)
            {
                songlist.Add(file.Name);
            }
            songs = songlist.ToArray();

          

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            

            


        }

        private void PlayMp3()
        {

            try {

                Random random = new Random();
                int start2 = random.Next(0, songs.Length);
                String playsong = "mp3/" + songs[start2];
                this.Text = songs[start2];

                this.waveOut = new WaveOut(); // or new WaveOutEvent() if you are not using WinForms/WPF
                this.mp3FileReader = new Mp3FileReader(playsong);
                this.waveOut.Init(mp3FileReader);

                this.waveOut.Play();
                this.waveOut.PlaybackStopped += OnPlaybackStopped;
            }
            catch (Exception e)
            {

                Random random = new Random();
                int start2 = random.Next(0, songs.Length);
                String playsong = "mp3/" + songs[start2];
                this.Text = songs[start2];

                this.waveOut = new WaveOut(); // or new WaveOutEvent() if you are not using WinForms/WPF
                this.mp3FileReader = new Mp3FileReader(playsong);
                this.waveOut.Init(mp3FileReader);

                this.waveOut.Play();
                this.waveOut.PlaybackStopped += OnPlaybackStopped;
            }



        }

        private void OnPlaybackStopped(object sender, EventArgs e)
        {
            this.waveOut.Dispose();
            this.mp3FileReader.Dispose();
            PlayMp3();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.waveOut.Pause();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.waveOut.Play();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.waveOut.Stop();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                // notifyIcon1.ShowBalloonTip(1000);
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID)
            {
                this.waveOut.Stop();


            }
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID2)
            {
                this.waveOut.Pause();

            }
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID3)
            {
                this.waveOut.Play();


            }

            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID4)
            {
                this.Show();
                notifyIcon1.Visible = false;
                WindowState = FormWindowState.Normal;


            }




            base.WndProc(ref m);


        }


    }
}
