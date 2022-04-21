//迷因顯示器

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F44084074_W6_practice_2
{
    public partial class 迷因產生器 : Form
    {
        MemeGenerator memeGenerator = new MemeGenerator();
        public 迷因產生器()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox.Image= Image.FromFile(@"..\..\picture\pic_0" + memeGenerator.currentPic + ".png");
            loc1.Checked = true;
            radioButton1.Checked = true;
            changeLocation();
            memeGenerator.font = "標楷體";
            background.Font = new Font(memeGenerator.font, memeGenerator.fontSize, memeGenerator.fontstyle);
            textBox2.Text = "Hello World";
        }
        //font
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            changeFont();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            changeFont();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            changeFont();
        }
        private void changeFont()
        {
            if (radioButton1.Checked) memeGenerator.font = "DFKai-SB";
            else if (radioButton2.Checked) memeGenerator.font = "PMingLiU";
            else if (radioButton3.Checked) memeGenerator.font = "Microsoft JhengHei";
                background.Font = new Font(memeGenerator.font, memeGenerator.fontSize, memeGenerator.fontstyle);
        }
        //checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (checkBox2.Checked) memeGenerator.fontstyle = FontStyle.Bold | FontStyle.Italic;
                else memeGenerator.fontstyle = FontStyle.Bold;
            }
            else
            {
                if (checkBox2.Checked) memeGenerator.fontstyle = FontStyle.Italic;
                else memeGenerator.fontstyle = FontStyle.Regular;
            }
            background.Font = new Font(memeGenerator.font, memeGenerator.fontSize, memeGenerator.fontstyle);
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                if (checkBox1.Checked) memeGenerator.fontstyle = FontStyle.Bold | FontStyle.Italic;
                else memeGenerator.fontstyle = FontStyle.Italic;
            }
            else
            {
                if (checkBox1.Checked) memeGenerator.fontstyle = FontStyle.Bold;
                else memeGenerator.fontstyle = FontStyle.Regular;
            }
            background.Font = new Font(memeGenerator.font, memeGenerator.fontSize, memeGenerator.fontstyle);
        }
        //font-size
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                memeGenerator.fontSize = int.Parse(textBox1.Text);
                if (memeGenerator.fontSize >=1 && memeGenerator.fontSize <=32) background.Font = new Font(memeGenerator.font, memeGenerator.fontSize, memeGenerator.fontstyle);
                else background.Font = new Font(memeGenerator.font, 12,memeGenerator.fontstyle);
            }
            catch
            {
                background.Font = new Font(memeGenerator.font, 12, memeGenerator.fontstyle);
            }
        }
        //word
        private void textBox2_textChanged(object sender, EventArgs e)
        {
            background.Text=textBox2.Text;      
        }
        //location
        private void loc1_CheckedChanged(object sender, EventArgs e)
        {
            changeLocation();
        }
        private void loc2_CheckedChanged(object sender, EventArgs e)
        {
            changeLocation();
        }
        private void loc3_CheckedChanged(object sender, EventArgs e)
        {
            changeLocation();
        }
        private void loc4_CheckedChanged(object sender, EventArgs e)
        {
            changeLocation();
        }
        private void loc5_CheckedChanged(object sender, EventArgs e)
        {
            changeLocation();
        }
        private void loc6_CheckedChanged(object sender, EventArgs e)
        {
            changeLocation();
        }
        private void changeLocation()
        {
            if (loc1.Checked) background.TextAlign = ContentAlignment.TopLeft;
            else if (loc2.Checked) background.TextAlign = ContentAlignment.TopCenter;
            else if (loc3.Checked) background.TextAlign = ContentAlignment.TopRight;
            else if (loc4.Checked) background.TextAlign = ContentAlignment.BottomLeft;
            else if (loc5.Checked) background.TextAlign = ContentAlignment.BottomCenter;
            else if (loc6.Checked) background.TextAlign = ContentAlignment.BottomRight;
        }
        //picture
        private void nextpic_Click(object sender, EventArgs e)
        {
            if (memeGenerator.currentPic == 5) memeGenerator.currentPic = 1;
            else memeGenerator.currentPic++;
            pictureBox.Image = Image.FromFile(@"..\..\picture\pic_0" + memeGenerator.currentPic + ".png");
        }
        private void lastpic_Click(object sender, EventArgs e)
        {
            if (memeGenerator.currentPic == 1) memeGenerator.currentPic = 5;
            else memeGenerator.currentPic--;
            pictureBox.Image = Image.FromFile(@"..\..\picture\pic_0" + memeGenerator.currentPic + ".png");
        } 
        //class
        class MemeGenerator
        {
            public int currentPic = 1;
            public int fontSize = 12;
            public string font ;
            public FontStyle fontstyle = FontStyle.Regular;
        }
    }
}
