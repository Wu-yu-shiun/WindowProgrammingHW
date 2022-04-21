//俄羅斯方塊＋1024

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F44084074_W7_practice_1
{
    public partial class Form1 : Form
    {
        int myPoint = 0, tmpPoint = 0, currentNum = 0 ,time = 3;
        Timer timer = new Timer();
        Button[,] button = new Button[6, 4];
        bool customizeNumber = false;
        public Form1()
        {
            InitializeComponent();
        }
        //start
        private void Form1_Load(object sender, EventArgs e)
        {
            //create mode buttons
            string[] modes = {"簡單","普通"};
            for(int i = 1; i <= 2; i++)
            {
                Button button = new Button();
                button.Name = $"mode{i}";
                button.Text = $"{modes[i-1]}模式";
                button.Size = new Size(100, 40);
                button.Location = new Point( (this.Width-100)/2, (this.Height - 40) / 2 + 60*(i-2) );
                button.Click += new EventHandler(mode_Click);   
                Controls.Add(button);
            }
            //invoke keypress
            this.KeyPreview = true;
        }
        //after choosing mode
        private void mode_Click(object sender, EventArgs e)
        {
            //remove mode button
            for (int i = 1; i <= 2; i++)
            {
                Button button = Controls[$"mode{i}"] as Button;
                button.Visible = false;
            }

            // create All Cubes
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    button[i, j] = new Button();
                    button[i, j].Text = "0";
                    button[i, j].Size = new Size(40, 40);
                    button[i, j].Location = new Point(30 + 50 * j, 50 + 50 * i);
                    button[i, j].FlatStyle = FlatStyle.Flat;
                    button[i, j].Visible = false;
                    Controls.Add(button[i, j]);
                }
            }

            //create point and number label
            Label label1 = new Label();
            label1.Text = $"你的分數: {myPoint}";
            label1.Location = new Point(470, 40);
            label1.Name = "pointLabel";
            Label label2 = new Label();
            currentNum = randomNumber();
            label2.Text = $"當前數字: {currentNum}";
            label2.Name = "numberLabel";
            label2.Location = new Point(470, 70);
            Controls.Add(label1);
            Controls.Add(label2);

            //difference between two models
            if (sender.ToString() == "System.Windows.Forms.Button, Text: 普通模式")
            {
                //create timer label
                Label label3 = new Label();
                label3.Name = "timerLabel";
                label3.Text = $"倒數計時: {time}";
                label3.Location = new Point(470, 100);
                Controls.Add(label3);
                //start timer
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = 1000;
                timer.Start();
            }
            else customizeNumber = true;
        }
        //timer tick
        private void timer_Tick(object sender, EventArgs e)
        {
            if (time == 0)  addCube(1);
            else
            {
                time--;
                Label label1=Controls["timerLabel"] as Label;
                label1.Text = $"倒數計時: {time}";
            }
        }
        //keydown q,w,e,r and a,s,d
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (c == 'q'|| c == 'w'|| c == 'e'|| c == 'r')
            {
                if (c == 'q') addCube(0);
                else if (c == 'w') addCube(1);
                else if (c == 'e') addCube(2);
                else if (c == 'r') addCube(3);
            }
            if (customizeNumber)
            {
                if (c == 'a') currentNum = 2;
                else if (c == 's') currentNum = 4;
                else if (c == 'd') currentNum = 8;
                Label label = Controls["numberLabel"] as Label;
                label.Text = $"當前數字: {currentNum}";
            }      
        }
        private void addCube(int num)
        {
            //button heap
            int cubeHeap = 5;
            while (button[cubeHeap, num].Text != "0")
            {
                cubeHeap--;
                if (cubeHeap < 0)
                {
                    if (button[cubeHeap + 1, num].Text == $"{currentNum}")
                    {
                        button[cubeHeap + 1, num].Text = $"{2 * int.Parse(button[cubeHeap + 1, num].Text)}";
                        return;
                    }
                    timer.Stop();
                    MessageBox.Show("遊戲結束!!!\n" + $"我的分數: {myPoint}");
                    System.Environment.Exit(System.Environment.ExitCode);
                }
            }
            button[cubeHeap, num].Visible = true;
            button[cubeHeap, num].Text = currentNum.ToString();
            //cube logic
            cubeLogic();
            //check again
            while (tmpPoint != myPoint)
            {
                myPoint = tmpPoint;
                cubeLogic();
            }
            //update data
            Label label1 = Controls["pointLabel"] as Label;
            label1.Text = $"你的分數: {myPoint}";
            currentNum = randomNumber();
            Label label2 = Controls["numberLabel"] as Label;
            label2.Text = $"當前數字: {currentNum}";
            if (Controls["timerLabel"] != null)
            {
                time = 3;
                Label label3 = Controls["timerLabel"] as Label;
                label3.Text = $"倒數計時: {time}";
            }
        }
        private void cubeLogic()
        {
            //merge cubes in column
            for (int i = 0; i < 4; i++)
            {
                for (int j = 5; j > 0; j--)
                {
                    if ((button[j, i].Text == button[j - 1, i].Text) && (button[j, i].Text != "0"))
                    {
                        for (int k = 0; k <= j; k++)
                        {
                            if (k == 0) button[k, i].Text = "0";
                            else if (k == j) button[k, i].Text = (int.Parse(button[k, i].Text) * 2).ToString();
                            else button[k, i].Text = button[k - 1, i].Text;
                        }
                        j = 6;
                    }
                }
            }

            //remove cubes in row 
            for (int i = 5; i >= 0; i--)
            {
                string temp = button[i, 0].Text;
                if ((temp == button[i, 1].Text) && (temp == button[i, 2].Text) && (temp == button[i, 3].Text) && (temp != "0"))
                {
                    tmpPoint += Convert.ToInt32(Math.Pow(Convert.ToDouble(temp), 2));
                    for (int j = i; j >= 0; j--)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if (j == 0) button[j, k].Text = "0";
                            else button[j, k].Text = button[j - 1, k].Text;
                        }
                    }
                }
            }

            //hide cube if zero
            for (int i = 0; i < 4; i++) for (int j = 0; j < 6; j++) if (button[j, i].Text == "0") button[j, i].Visible = false;
        }
        //random number 2,4,8 in cube
        private int randomNumber()
        {
            Random random = new Random();
            int num = random.Next(1, 4);
            if (num == 1) return 2;
            else if (num == 2) return 4;
            else return 8;
        }
    }
}
