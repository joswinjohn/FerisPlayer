using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace FerisPlayer
{
    public partial class Form1 : Form
    {



        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        public Form1()
        {
            InitializeComponent();
        }

        bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectpath = AppDomain.CurrentDomain.BaseDirectory + "Music" + listBox1.SelectedItem;
            if (selectpath.Contains(".mp3") || selectpath.Contains(".MP3"))
            {
                wplayer.URL = $"./Music/{listBox1.SelectedItem}";
                wplayer.controls.stop();
            } else
            {
                MessageBox.Show("Select an MP3 File");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Music";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            listBox1.Items.Clear();
            Functions.PopulateListBox(listBox1, "./Music", "*.mp3");

            if (IsDirectoryEmpty(path))
            {
                listBox1.Items.Add("Add MP3's to \"Music\" Folder");
            }

            wplayer.settings.volume = trackBar2.Value;
        }

        string dur = "0:00";
        string cdur = "0:00";
        public void PlayButton_Click(object sender, EventArgs e)
        {
            wplayer.controls.play();
            autoplaytimer.Enabled = true;

            timer3.Enabled = true;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            wplayer.controls.pause();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (wplayer.settings.getMode("loop") == true)
            {
                wplayer.settings.setMode("loop", false);
            } else
            {
                wplayer.settings.setMode("loop", true);
            }
        }

        private void looptimer_Tick(object sender, EventArgs e)
        {
            if (autoplaytimer.Enabled)
            {
                wplayer.settings.setMode("loop", true);
            } else
            {
                wplayer.settings.setMode("loop", false);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wplayer.controls.fastReverse();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            wplayer.controls.fastForward();
        }

        private void autoplay_Tick(object sender, EventArgs e)
        {
	        if (wplayer.settings.getMode("loop") == false && wplayer.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                int index = listBox1.SelectedIndex + 1;
                listBox1.SetSelected(index, true);
                wplayer.controls.play();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex - 1;
            listBox1.SetSelected(index, true);
            wplayer.controls.play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex + 1;
            listBox1.SetSelected(index, true);
            wplayer.controls.play();
            
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            dur = wplayer.currentMedia.durationString;
            cdur = wplayer.controls.currentPositionString;
            trackBar1.Maximum = Convert.ToInt32(wplayer.currentMedia.duration);
            
            textBox1.Text = cdur + " / " + dur;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            trackBar1.Value = Convert.ToInt32(wplayer.controls.currentPosition);
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                wplayer.controls.pause();
                timer4.Enabled = false;
                wplayer.controls.currentPosition = trackBar1.Value;
            } else
            {
                timer4.Enabled = true;
            }

        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            wplayer.controls.currentPosition = trackBar1.Value;
            timer4.Enabled = true;
            wplayer.controls.play();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            wplayer.settings.volume = trackBar2.Value * 2;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int sufl = rnd.Next(1, listBox1.Items.Count);

            listBox1.SetSelected(sufl, true);
            wplayer.controls.play();
            autoplaytimer.Enabled = true;

            timer3.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string repath = AppDomain.CurrentDomain.BaseDirectory + "Music";

            listBox1.Items.Clear();
            Functions.PopulateListBox(listBox1, "./Music", "*.mp3");

            if (IsDirectoryEmpty(repath))
            {
                listBox1.Items.Add("Add MP3's to \"Music\" Folder");
            }

            wplayer.settings.volume = trackBar2.Value;
        }
    }
}
