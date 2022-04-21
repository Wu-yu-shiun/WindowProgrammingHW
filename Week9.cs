//音樂播放器

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace F44084074_W9_practice_2
{
    public partial class Form1 : Form
    {
        MusicPlayer musicplayer = new MusicPlayer();
        RadioButton[] radioButtons;
        RadioButton currentRadioButton = new RadioButton();
        Form2 Spectrum = new Form2();
        public static int index;
        public static string filename;
        public bool checksound = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "wav files(*.wav)|*.wav";
            fontDialog1.MaxSize = 24;
            saveFileDialog1.Filter = "txt files(*.txt)|*.txt";
            openFileDialog2.Filter = "txt files(*.txt)|*.txt";         
        }
        private void loopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (loopCheckBox.Checked) musicplayer.Loop = true;
            else musicplayer.Loop = false;
        }
        private void playButton_Click(object sender, EventArgs e)
        {
            if (musicplayer.Playlist != null && checksound)
            {
                musicplayer.PlayMusic();
                Spectrum.Show();
                Spectrum.BringToFront();
            }
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            musicplayer.Stop();
            Spectrum.Hide();
        }
        private void selectFilesButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                musicplayer.Stop();
                Spectrum.Hide();
                playlistGroupBox.Controls.Clear();
                Spectrum.Hide();
                index = 0;
                radioButtons = new RadioButton[openFileDialog1.FileNames.Length];
                musicplayer.Playlist = new string[openFileDialog1.FileNames.Length];
                foreach (string file in openFileDialog1.FileNames.Take(4))
                {
                    musicplayer.Playlist[index] = file;
                    radioButtons[index] = new RadioButton();
                    radioButtons[index].Text = file;
                    radioButtons[index].Size = new Size(520, 40);
                    radioButtons[index].Location = new Point(10, 20 + 40 * index);
                    radioButtons[index].Font = fontDialog1.Font;
                    radioButtons[index].ForeColor = colorDialog1.Color;
                    radioButtons[index].CheckedChanged += new EventHandler(sound_Checked);
                    playlistGroupBox.Controls.Add(radioButtons[index]);
                    index++;
                }
            }
        }
        private void sound_Checked(object sender, EventArgs e)
        {
            checksound = true;
            if (!currentRadioButton.Checked)
            {
                musicplayer.Stop();
                Spectrum.Hide();
            }             
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.Checked)
            {
                musicplayer.CurrentSound = radioButton.Text;
                currentRadioButton = radioButton;
            }
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.ToString())
            {
                case "Color":
                    if (colorDialog1.ShowDialog() == DialogResult.OK)
                    {
                        for (int i = 0; i < index; i++) radioButtons[i].ForeColor = colorDialog1.Color;
                    }
                    break;
                case "Font":
                    if (fontDialog1.ShowDialog() == DialogResult.OK)
                    {
                        for (int i = 0; i < index; i++) radioButtons[i].Font = fontDialog1.Font;
                    }
                    break;
            }
        }
        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (musicplayer.Playlist == null) MessageBox.Show("請建立播放清單");
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = saveFileDialog1.FileName;
                    musicplayer.SavePlayList();
                }
            }
        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                musicplayer.Stop();
                Spectrum.Hide();
                playlistGroupBox.Controls.Clear();
                Spectrum.Hide();
                filename = openFileDialog2.FileName;
                musicplayer.LoadPlayList();
                radioButtons = new RadioButton[musicplayer.Playlist.Length];
                index = 0;
                foreach (string file in musicplayer.Playlist)
                {
                    radioButtons[index] = new RadioButton();
                    radioButtons[index].Text = file;
                    radioButtons[index].Size = new Size(1000, 40);
                    radioButtons[index].Location = new Point(10, 20 + 40 * index);
                    radioButtons[index].Font = fontDialog1.Font;
                    radioButtons[index].ForeColor = colorDialog1.Color;
                    radioButtons[index].CheckedChanged += new EventHandler(sound_Checked);
                    playlistGroupBox.Controls.Add(radioButtons[index]);
                    index++;
                }
            }
        }      
    }
    public class MusicPlayer : SoundPlayer
    {
        public bool Loop = false;
        public string[] Playlist;
        public string[] Temp = new string[4];
        public string CurrentSound = null;
        public void PlayMusic()
        {
            this.SoundLocation = CurrentSound;
            if (this.Loop) this.PlayLooping();
            else this.Play();
        }
        public void SavePlayList()
        {
            using (FileStream filestream = new FileStream(Form1.filename, FileMode.Append))
            {
                for (int i = 0; i < Playlist.Length; i++)
                {
                    byte[] buffer = Encoding.Default.GetBytes(this.Playlist[i] + Environment.NewLine);
                    filestream.Write(buffer, 0, buffer.Length);
                }
            }
        }
        public void LoadPlayList()
        {
            StreamReader streamreader = new StreamReader(Form1.filename);
            string msg = null;
            int num = 0;
            while ((msg = streamreader.ReadLine()) != null)
            {
                Temp[num] = msg;
                num++;
            }
            this.Playlist = new string[num];
            for (int i = 0; i < num; i++) this.Playlist[i] = this.Temp[i];
        }
    }
}

//模擬spectrum in form2


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F44084074_W9_practice_2
{
    public partial class Form2 : Form
    {
        Button[,] buttons;
        Random random = new Random();
        int[] ranNum = new int[4];
        int temp;
        int maxheight=10;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Width = 800;
            this.Height = 500;
            buttons = new Button[maxheight, 4];
            for(int i=0;i<maxheight;i++) for(int j = 0; j < 4; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Text =i+","+j;
                    buttons[i, j].Size = new Size(80, 20);
                    buttons[i, j].Location = new Point(80+85*j, 40 + 25 * i);
                    buttons[i, j].ForeColor = Color.White;
                    Controls.Add(buttons[i, j]);
                }
            timer1.Start();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            temp = maxheight;
            maxheight = trackBar1.Value;
            //delete old button
            for (int i = 0; i < temp; i++) for (int j = 0; j < 4; j++) Controls.Remove(buttons[i, j]);
            //create new button
            buttons = new Button[maxheight, 4];
            for (int i = 0; i < maxheight; i++) for (int j = 0; j < 4; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Text = i + "," + j;
                    buttons[i, j].Size = new Size(80, 20);
                    buttons[i, j].Location = new Point(80 + 85 * j, 40 + 25 * i);
                    buttons[i, j].ForeColor = Color.White;
                    Controls.Add(buttons[i, j]);
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < maxheight; i++) for (int j = 0; j < 4; j++) buttons[i, j].BackColor = Color.Blue;
            for(int i = 0; i < 4; i++)
            {
                ranNum[i] = random.Next(-1, maxheight);
                for(int j = 0; j < maxheight; j++) if (j <= ranNum[i]) buttons[j, i].BackColor = Color.Transparent;
            }
        }
    }
}
