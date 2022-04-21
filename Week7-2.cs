//射擊遊戲＋屬性

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F44084074_W7_practice_2
{
    public partial class Form1 : Form
    {
        int columnLength = 50;
        string playerMode;
        int time=60, temp=60;
        int playerPoint = 0;
        Random random = new Random();
        Timer timer1 = new Timer();    //coun
        Timer timer2 = new Timer();   //enemy speed
        Timer timer3 = new Timer();   //bullet speed
        Player player = new Player();
        Button playerButton = new Button();//Controls["player"] as Button;
        Button[] enemyButton = new Button[4];
        Button[] bulletButton = new Button[10];
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 400;
            this.Width = 500;
            panel1.Height = 200;
            panel2.Visible = false;
            panel1.Location = new Point((this.Width - panel1.Width) / 2, (this.Height - panel1.Height) / 2);
            panel2.Location = new Point(this.Width - 130, 20);
            button1.Click += new EventHandler(start_Click);
            this.KeyPreview = true;
        }
        private void start_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) playerMode = "水";
            else if (radioButton2.Checked) playerMode = "火";
            else playerMode = "木";
            //init player,bullet,enemy
            generatePlayer();
            generateBullet();
            for (int i = 0; i < 4; i++) generateRandomEnemy(i);
            //panel change
            panel2.Visible = true;
            panel1.Visible = false;
            label2.Text += playerMode;
            label3.Text += $"{playerPoint}";
            label4.Text += $"{time}";        
            //start timer
            timer1.Tick += new EventHandler(timer_Tick);
            timer2.Tick += new EventHandler(enemyMove_Tick);
            timer3.Tick += new EventHandler(bulletMove_Tick);
            timer1.Interval = 1000;
            timer2.Interval = 100;
            timer3.Interval = 10;
            timer1.Start();
            timer2.Start();
            timer3.Start();   
        }
        private void bulletMove_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)  
            {
                if (bulletButton[i].Visible) 
                {
                    bulletButton[i].Top -= 8; 
                    if (bulletButton[i].Top <= -6) bulletButton[i].Visible = false;
                }
                //if collapse enemy
                for (int j = 0; j < 4; j++)
                {
                    if (bulletButton[i].Left == (45 + 50 * j) && bulletButton[i].Top <= enemyButton[j].Bottom)
                    {
                       
                        if (bulletButton[i].BackColor == Color.Blue)
                        {
                            if (enemyButton[j].BackColor == Color.Red)
                            {
                                playerPoint += 2;
                                label3.Text = $"目前分數: {playerPoint}";
                            }
                            else if(enemyButton[j].BackColor == Color.Green)
                            {
                                playerPoint -= 2;
                                label3.Text = $"目前分數: {playerPoint}";
                                if (playerPoint < 0) endGame();
                            }
                        }
                        else if (bulletButton[i].BackColor == Color.Green)
                        {
                            if (enemyButton[j].BackColor == Color.Blue)
                            {
                                playerPoint += 2;
                                label3.Text = $"目前分數: {playerPoint}";
                            }
                            else if (enemyButton[j].BackColor == Color.Red)
                            {
                                playerPoint -= 2;
                                label3.Text = $"目前分數: {playerPoint}";
                                if (playerPoint < 0) endGame();
                            }
                        }
                       else
                        {
                            if (enemyButton[j].BackColor == Color.Green)
                            {
                                playerPoint += 2;
                                label3.Text = $"目前分數: {playerPoint}";
                            }
                            else if (enemyButton[j].BackColor == Color.Blue)
                            {
                                playerPoint -= 2;
                                label3.Text = $"目前分數: {playerPoint}";
                                if (playerPoint < 0) endGame();
                            }
                        }
                        bulletButton[i].Visible = false;
                        bulletButton[i].Location = new Point(500, 500);
                        Controls.Remove(enemyButton[j]);
                        generateRandomEnemy(j);
                    }
                }
            }
      
        }
        private void enemyMove_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < 4; i++)
            {
                if (enemyButton[i].BackColor == Color.Blue) enemyButton[i].Top += 1;
                else if (enemyButton[i].BackColor == Color.Red) enemyButton[i].Top += 2;
                else enemyButton[i].Top += 3;
                //if drop on the floor
                if (enemyButton[i].Top >= 400) generateRandomEnemy(i);
                //if collapse player
                if (enemyButton[i].Left == playerButton.Left &&
                    playerButton.Top <= enemyButton[i].Bottom &&
                    playerButton.Bottom >= enemyButton[i].Top)
                {
                    if (enemyButton[i].BackColor != playerButton.BackColor)
                    {
                        if (playerButton.BackColor == Color.Blue)
                        {
                            if (enemyButton[i].BackColor == Color.Red)
                            {
                                playerButton.BackColor = Color.Red;
                                label2.Text = "目前屬性: 火";  
                                playerPoint += 5;
                                label3.Text = $"目前分數: {playerPoint}";
                            }
                            else
                            {
                                playerPoint -= 5;
                                if (playerPoint < 0) endGame();
                                label3.Text = $"目前分數: {playerPoint}";                            
                            }
                        }
                        else if (playerButton.BackColor == Color.Red)
                        {
                            if (enemyButton[i].BackColor == Color.Green)
                            {
                                playerButton.BackColor = Color.Green;
                                label2.Text = "目前屬性: 木";                                     
                                playerPoint += 5;
                                label3.Text = $"目前分數: {playerPoint}";
                            }
                            else
                            {
                                playerPoint -= 5;
                                if (playerPoint < 0) endGame();
                                label3.Text = $"目前分數: {playerPoint}"; 
                            }
                        }
                        else
                        {
                            if (enemyButton[i].BackColor == Color.Blue)
                            {
                                playerButton.BackColor = Color.Blue;
                                label2.Text = "目前屬性: 水";                              
                                playerPoint += 5;
                                label3.Text = $"目前分數: {playerPoint}";
                            }
                            else
                            {
                                playerPoint -= 5;
                                if (playerPoint < 0) endGame();
                                label3.Text = $"目前分數: {playerPoint}";                       
                            }
                        }
                        Controls.Remove(enemyButton[i]);
                        generateRandomEnemy(i);
                    }
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                timer1.Stop();
                endGame(); 
            } 
            else
            {
                time--;
                label4.Text = $"剩餘時間: {time}";
            }
        }
        private void Form1_Keyprss(object sender, KeyPressEventArgs e)
        {
            if (panel2.Visible)
            {            
                char c = e.KeyChar;
                if (c == 'a') if (playerButton.Location.X != 35) playerButton.Left -= columnLength;
                if (c == 'd') if (playerButton.Location.X != 185) playerButton.Left += columnLength;
                if (c == 'w' && time<temp)
                {
                    temp = time ; //cooling time
                    for (int i = 0; i < 10; i++)
                    {
                        if (!bulletButton[i].Visible)
                        {
                            bulletButton[i].Visible = true;
                            bulletButton[i].BackColor = playerButton.BackColor;
                            bulletButton[i].Location = new Point(playerButton.Left + 10, playerButton.Top + 6);
                            break;
                        }
                    }
                }
            }
        }
        private void generatePlayer()
        {
            player.size = 40;
            playerButton.Size = new Size(player.size, player.size);
            playerButton.Location = new Point(85, 300);
            playerButton.Text = "你";
            playerButton.ForeColor = Color.White;
            if (playerMode == "水") player.color = Color.Blue;
            else if (playerMode == "火") player.color = Color.Red;
            else player.color = Color.Green;
            playerButton.BackColor = player.color;
            Controls.Add(playerButton);
        }
        private void generateRandomEnemy(int column)
        { 
            Enemy enemy = new Enemy();
            enemy.size = 40;
            int num = random.Next(1, 4);
            if (num == 1)
            {
                enemy.speed = 10;
                enemy.color = Color.Blue;
                enemy.name = "水";
            } 
            else if (num == 2)
            {
                enemy.speed = 20;
                enemy.color = Color.Red;
                enemy.name = "火";
            }
            else
            {
                enemy.speed = 30;
                enemy.color = Color.Green;
                enemy.name = "木";
            }
            enemyButton[column] = new Button();
            enemyButton[column].BackColor = enemy.color;
            enemyButton[column].Text = enemy.name;
            enemyButton[column].ForeColor = Color.White;
            enemyButton[column].Location = new Point(35 + column*50, 0);
            enemyButton[column].Size = new Size(enemy.size, enemy.size);
            Controls.Add(enemyButton[column]);
        }
        private void generateBullet()
        {
            Bullet bullet = new Bullet();
            bullet.size = 20;
            for(int i = 0; i < 10; i++)
            {
                bulletButton[i] = new Button();
                bulletButton[i].Size = new Size(bullet.size,bullet.size);                
                bulletButton[i].Visible = false;
                Controls.Add(bulletButton[i]);
            }
        }
        private void endGame()
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            if (time == 0) MessageBox.Show($"遊戲結束!\n你的分數: {playerPoint}");
            else
            {
                if (playerButton.BackColor==Color.Blue) MessageBox.Show("你枯竭了!\n你的分數: 0");
                else if (playerButton.BackColor == Color.Red) MessageBox.Show("你被熄滅了!\n你的分數: 0");
                else MessageBox.Show("你燒起來了!\n你的分數: 0");
            }
            System.Environment.Exit(System.Environment.ExitCode);
        }   
    }
}
