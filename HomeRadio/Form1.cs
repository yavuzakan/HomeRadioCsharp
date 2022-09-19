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

namespace HomeRadio
{
    //NAudio - Nuget  
    public partial class Form1 : Form
    {
        private IWavePlayer waveOut;
        private Mp3FileReader mp3FileReader;
        public string[] songs = { };
        public List<string> songlist = new List<string>();

        public Form1()
        {
            InitializeComponent();
            allsong();
            PlayMp3();
         

 
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
    }
}
