using System;
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            wplayer.URL = $"./Music/{listBox1.SelectedItem}";
            wplayer.controls.stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Functions.PopulateListBox(listBox1, "./Music", "*.mp3");
            string path = AppDomain.CurrentDomain.BaseDirectory + "Music";
            wplayer.newPlaylist("pMain", path);
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
            if (looptimer.Enabled == true)
            {
                looptimer.Enabled = false;
            } else
            {
                looptimer.Enabled = true;
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
	        if (looptimer.Enabled == false && wplayer.playState == WMPLib.WMPPlayState.wmppsStopped)
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
    }
}
