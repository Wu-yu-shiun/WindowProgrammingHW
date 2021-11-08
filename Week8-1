//記憶翻牌＋匯入檔案

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

namespace F44084074_W8_practice_1
{
    public partial class Form1 : Form
    {
        int row, column, p1, p2, point1, point2,time1 = 3, time2 = 2, round = 1, stage = 1;
        int[,] numbers ;
        Card[,] cards;
        Card card1, card2;
        Timer timer1 = new Timer();
        Timer timer2 = new Timer();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //read file
            StreamReader streamreader = new StreamReader("card.map");
            string msg = null;
            int index = 0;
            while ((msg = streamreader.ReadLine()) != null)
            {
                string[] a = msg.Split(' ');    
                if (a.Length == 2)
                {
                    row = int.Parse(a[0]);
                    column = int.Parse(a[1]);
                    numbers = new int[row,column];
                }
                else
                {
                    for(int i = 0; i < a.Length; i++) numbers[index, i] = int.Parse(a[i]);
                    index++;
                }          
            }
            // ---
            Console.WriteLine($"{row},{column}");
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++) Console.Write($"{numbers[i, j]} ");
                Console.WriteLine();
            }
            //panel off
            panel1.Visible = false;
            // generate start button 
            Button startGame = new Button();
            startGame.Text="開始遊戲";
            startGame.Size = new Size(100,40);
            startGame.FlatStyle = FlatStyle.Flat;
            startGame.Click += new EventHandler(start_Click);
            startGame.Location = new Point((this.Width - startGame.Width) / 2, 150);
            Controls.Add(startGame);   
        }

        private void start_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            //generate card array
            cards = new Card[row, column];
            for (int i = 0; i < row; i++) for (int j = 0; j < column; j++)
                {
                    cards[i, j] = new Card();
                    cards[i, j].number =numbers[i,j];
                    cards[i, j].Text =$"{cards[i,j].number}";
                    cards[i, j].Size = new Size(60, 60);
                    cards[i, j].Location = new Point((this.Width-246)/2 + 62 * j, 60 + 62 * i);
                    cards[i, j].FlatStyle = FlatStyle.Flat;
                    cards[i, j].FlatAppearance.BorderSize = 0;
                    cards[i, j].BackColor = Color.LightBlue;
                    cards[i, j].Enabled = false;
                    panel1.Controls.Add(cards[i, j]);                  
                }
            //timer set
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Tick += new EventHandler(timer2_Tick);
            timer1.Interval = 1000;
            timer2.Interval = 1000;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time1 == 0)
            {
                timer1.Stop();
                for (int i = 0; i < row; i++) for (int j = 0; j < column; j++)
                    {
                        cards[i, j].Text = "";
                        cards[i, j].BackColor = Color.LightGray;
                        cards[i, j].Enabled = true;
                        cards[i, j].Click += new EventHandler(button_Click);
                    }
            }
            else time1--;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {          
            if (time2 == 0)
            {
                timer2.Stop();
                for (int i = 0; i < row; i++) for (int j = 0; j < column; j++) cards[i, j].Enabled = true;
                time2 = 2;
                check();
                if (round%2!=0)
                {
                    stage = 1;
                    round++;
                    label1.Text = $"第{round}回合 輪到P2";
                }
                else
                {
                    stage = 1;
                    round++;
                    label1.Text = $"第{round}回合 輪到P1";
                }
            }
            else time2--;
        }
        private void button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < row; i++) for (int j = 0; j < column; j++)
                {
                    if (sender == this.cards[i, j])
                    {
                        cards[i, j].showNumber();
                        cards[i, j].Enabled = false;
                        if (round % 2 != 0)
                        {                          
                            if (stage == 1)
                            {
                                stage++;
                                p1 = cards[i, j].number;
                                card1 = cards[i, j];
                                label1.Text = $"第{round}回合 輪到P2";
                            }
                            else
                            {
                                p2 = cards[i, j].number;
                                card2 = cards[i, j];
                                for (int a = 0; a < row; a++) for (int b = 0; b < column; b++) cards[a, b].Enabled = false;
                                timer2.Start();                             
                            }
                        }
                        else
                        {
                            if (stage == 1)
                            {
                                stage++;
                                p2 = cards[i, j].number;
                                card2 = cards[i, j];
                                label1.Text = $"第{round}回合 輪到P1";
                            }
                            else
                            {
                                p1 = cards[i, j].number;
                                card1 = cards[i, j];
                                for (int a = 0; a < row; a++) for (int b = 0; b < column; b++) cards[a, b].Enabled = false;
                                timer2.Start();
                            }
                        }
                        return;
                    }                    
                }
        }
        private void check()
        {
            if (round % 2 != 0)
            {
                if (p1 < p2)
                {
                    point2 += (p2 - p1);
                    label3.Text = $"P2: {point2}分";                   
                    card1.Visible = false;
                    card2.Visible = false;
                }
                else
                {
                    card1.hideNumber();
                    card2.hideNumber();
                }
            }
            else
            {
                if (p2 < p1)
                {
                    point1 += (p1 - p2);
                    label2.Text = $"P1: {point1}分";
                    card1.Visible = false;
                    card2.Visible = false;
                }
                else
                {
                    card1.hideNumber();
                    card2.hideNumber();
                }
            }
            //if cards less than 8
            int count = 0;
            for (int i = 0; i < row; i++) for (int j = 0; j < column; j++)
                {
                    if (!cards[i, j].Visible) count++;
                }
            if (count >= 8)
            {
                if (point1 > point2) MessageBox.Show("玩家一獲勝");
                else if(point2>point1) MessageBox.Show("玩家二獲勝");
                else MessageBox.Show("雙方平手");
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }
    }
    class Card : System.Windows.Forms.Button
    {
        public int number;
        public void hideNumber()
        {
            this.Text = "";
            this.BackColor = Color.LightGray;
        }
        public void showNumber()
        {
            this.Text = $"{this.number}";
            this.BackColor = Color.LightBlue;
        }
    }
}
