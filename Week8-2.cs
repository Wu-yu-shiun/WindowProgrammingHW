//五子棋＋角色選擇

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F44084074_W8_practice_2
{
    public partial class Form1 : Form
    {
        Chess[,] chesses = new Chess[19, 19];
        Character p1 = new Character();
        Character p2 = new Character();
        Warrior warrior = new Warrior();
        Witcher witcher = new Witcher();
        Archer archer = new Archer();
        char takeWhatChess;
        bool takeChess=false;
        int chessNumOnBoard = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            right4.Visible = false; left4.Visible = false;
            // generate start button 
            Button startGame = new Button();
            startGame.Name = "start";
            startGame.Text = "開始遊戲";
            startGame.Size = new Size(80, 30);
            startGame.FlatStyle = FlatStyle.Flat;
            startGame.Click += new EventHandler(start_Click);
            startGame.Location = new Point((this.Width - startGame.Width) / 2, 300);
            Controls.Add(startGame);
            
        }
        private void chooseCharacter_Click(object sender, EventArgs e)
        {
            if (sender == left1) label1.Text = "P2: 戰士";
            if (sender == left2) label1.Text = "P2: 法師";
            if (sender == left3) label1.Text = "P2: 弓箭手";
            if (sender == right1) label2.Text = "P1: 戰士";
            if (sender == right2) label2.Text = "P1: 法師";
            if (sender == right3) label2.Text = "P1: 弓箭手";
        }
        private void start_Click(object sender, EventArgs e)
        {
            //check role
            string[] a = label2.Text.Split(' ');
            if (a[1] == "戰士" )
            {              
                p1.numB = warrior.numB; p1.numC = warrior.numC; p1.numD = warrior.numD;
                p1.color = Color.DeepSkyBlue;
            }
            else if (a[1] == "法師")
            {
                p1.numB = witcher.numB; p1.numC = witcher.numC; p1.numD = witcher.numD;
                p1.color = Color.DarkBlue;
            }
            else if (a[1] == "弓箭手")
            {
                p1.numB = archer.numB; p1.numC = archer.numC; p1.numD = archer.numD;
                p1.color = Color.BlueViolet;
            }
            string[] b = label1.Text.Split(' ');
            if (b[1] == "戰士")
            {
                p2.numB = warrior.numB; p2.numC = warrior.numC; p2.numD = warrior.numD+1;
                p2.color = Color.Orange;
            }
            else if (b[1] == "法師")
            {
                p2.numB = witcher.numB; p2.numC = witcher.numC; p2.numD = witcher.numD+1;
                p2.color = Color.DarkOrange;
            }
            else if (b[1] == "弓箭手")
            {
                p2.numB = archer.numB; p2.numC = archer.numC; p2.numD = archer.numD+1;
                p2.color = Color.OrangeRed;
            }
            //set left,right button
            Font font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            right4.Visible = true;
            left4.Visible = true;
            left1.Text = "普通棋子"; left1.Font = font; left1.Click -= chooseCharacter_Click; left1.Click += chooseChess_Click;
            right1.Text = "普通棋子"; right1.Font = font; right1.Click -= chooseCharacter_Click; right1.Click += chooseChess_Click;
            left2.Text = $"橫向棋子:{p2.numB}顆"; left2.Font = font; left2.Click -= chooseCharacter_Click; left2.Click += chooseChess_Click;
            right2.Text = $"橫向棋子:{p1.numB}顆"; right2.Font = font; right2.Click -= chooseCharacter_Click; right2.Click += chooseChess_Click;
            left3.Text = $"縱向棋子:{p2.numC}顆"; left3.Font = font; left3.Click -= chooseCharacter_Click; left3.Click += chooseChess_Click;
            right3.Text = $"縱向棋子:{p1.numC}顆"; right3.Font = font; right3.Click -= chooseCharacter_Click; right3.Click += chooseChess_Click;
            left4.Text = $"覆蓋棋子:{p2.numD}顆"; left4.Click += chooseChess_Click;
            right4.Text = $"覆蓋棋子:{p1.numD}顆"; right4.Click += chooseChess_Click;
            //hide start button 
            Controls["start"].Visible = false;
            //generate chessboard    
            for (int i = 0; i < 19; i++) for (int j = 0; j < 19; j++)
                {
                    chesses[i, j] = new Chess();
                    chesses[i, j].Size = new Size(21, 21);
                    chesses[i, j].Location = new Point((this.Width - 410) / 2 + 21 * j, 20 + 21 * i);
                    chesses[i, j].FlatStyle = FlatStyle.Flat;
                    chesses[i, j].FlatAppearance.BorderColor = Color.White;
                    chesses[i, j].FlatAppearance.BorderSize = 1;
                    chesses[i, j].BackColor = Color.LightGray;
                    chesses[i, j].Click += new EventHandler(putChess_Click);
                    Controls.Add(chesses[i, j]);
                }
            //game start
            buttonEnabled();
        }
        private void chooseChess_Click(object sender, EventArgs e)
        {
            takeChess = true;
            if (sender == left1|| sender==right1) takeWhatChess = 'a';
            else if (sender == left2 || sender == right2) takeWhatChess = 'b';
            else if (sender == left3 || sender == right3) takeWhatChess = 'c';
            else if (sender == left4 || sender == right4) takeWhatChess = 'd';
        }
        private void putChess_Click(object sender, EventArgs e)
        {         
            //put chess
            for (int i = 0; i < 19; i++) for (int j = 0; j < 19; j++)
                {
                    if (sender == chesses[i, j])
                    {
                        if (!takeChess || takeWhatChess == 'a')
                        {
                            if (chessNumOnBoard % 2 == 0)
                            {
                                if (!chesses[i, j].isUsed)
                                {
                                    chesses[i, j].BackColor = p1.color;
                                    chesses[i, j].isUsedByP1 = true;
                                    chesses[i, j].isUsed = true;
                                    checkAndChange();
                                }                             
                            }
                            else
                            {
                                if (!chesses[i, j].isUsed)
                                {
                                    chesses[i, j].BackColor = p2.color;
                                    chesses[i, j].isUsedByP2 = true;
                                    chesses[i, j].isUsed = true;
                                    checkAndChange();
                                }
                            }                         
                        }
                        else if (takeWhatChess == 'b')
                        {
                            if (j == 18)
                            {
                                if (chessNumOnBoard % 2 == 0)
                                {
                                    if(!chesses[i, j].isUsed)
                                    {
                                        chesses[i, j].BackColor = p1.color; 
                                        chesses[i, j].isUsedByP1 = true; 
                                        chesses[i, j].isUsed = true; 
                                        p1.numB--;
                                        right2.Text = $"橫向棋子:{p1.numB}顆";
                                        checkAndChange();
                                    }
                                }
                                else
                                {
                                    if (!chesses[i, j].isUsed)
                                    {
                                        chesses[i, j].BackColor = p2.color;
                                        chesses[i, j].isUsedByP2 = true;
                                        chesses[i, j].isUsed = true;
                                        p2.numB--;
                                        left2.Text = $"橫向棋子:{p2.numB}顆";
                                        checkAndChange();
                                    }
                                }
                            }
                            else
                            {
                                if (chessNumOnBoard % 2 == 0)
                                {
                                    if ((!chesses[i, j].isUsed) && (!chesses[i, j + 1].isUsed))
                                    {
                                        chesses[i, j].BackColor = p1.color; chesses[i, j + 1].BackColor = p1.color;
                                        chesses[i, j].isUsedByP1 = true; chesses[i, j + 1].isUsedByP1 = true;
                                        chesses[i, j].isUsed = true; chesses[i, j + 1].isUsed = true;
                                        p1.numB--;
                                        right2.Text = $"橫向棋子:{p1.numB}顆";
                                        checkAndChange();
                                    }
                                }
                                else
                                {
                                    if ((!chesses[i, j].isUsed) && (!chesses[i, j + 1].isUsed))
                                    {
                                        chesses[i, j].BackColor = p2.color; chesses[i, j + 1].BackColor = p2.color;
                                        chesses[i, j].isUsedByP2 = true; chesses[i, j + 1].isUsedByP2 = true;
                                        chesses[i, j].isUsed = true; chesses[i, j + 1].isUsed = true;
                                        p2.numB--;
                                        left2.Text = $"橫向棋子:{p2.numB}顆";
                                        checkAndChange();
                                    }
                                }
                            }
                            
                        }
                        else if (takeWhatChess == 'c')
                        {
                            if (i == 18)
                            {
                                if (chessNumOnBoard % 2 == 0)
                                {
                                    if (!chesses[i, j].isUsed)
                                    {
                                        chesses[i, j].BackColor = p1.color;
                                        chesses[i, j].isUsedByP1 = true;
                                        chesses[i, j].isUsed = true;
                                        p1.numC--;
                                        right3.Text = $"橫向棋子:{p1.numC}顆";
                                        checkAndChange();
                                    }
                                }
                                else
                                {
                                    if (!chesses[i, j].isUsed)
                                    {
                                        chesses[i, j].BackColor = p2.color;
                                        chesses[i, j].isUsedByP2 = true;
                                        chesses[i, j].isUsed = true;
                                        p2.numC--;
                                        left3.Text = $"橫向棋子:{p2.numC}顆";
                                        checkAndChange();
                                    }
                                }
                            }
                            else
                            {
                                if (chessNumOnBoard % 2 == 0)
                                {
                                    if ((!chesses[i, j].isUsed) && (!chesses[i + 1, j].isUsed))
                                    {
                                        chesses[i, j].BackColor = p1.color; chesses[i + 1, j].BackColor = p1.color;
                                        chesses[i, j].isUsedByP1 = true; chesses[i + 1, j].isUsedByP1 = true;
                                        chesses[i, j].isUsed = true; chesses[i + 1, j].isUsed = true;
                                        p1.numC--;
                                        right3.Text = $"縱向棋子:{p1.numC}顆";
                                        checkAndChange();
                                    }
                                }
                                else
                                {
                                    if ((!chesses[i, j].isUsed) && (!chesses[i + 1, j].isUsed))
                                    {
                                        chesses[i, j].BackColor = p2.color; chesses[i + 1, j].BackColor = p2.color;
                                        chesses[i, j].isUsedByP2 = true; chesses[i + 1, j].isUsedByP2 = true;
                                        chesses[i, j].isUsed = true; chesses[i + 1, j].isUsed = true;
                                        p2.numC--;
                                        left3.Text = $"縱向棋子:{p2.numC}顆";
                                        checkAndChange();
                                    }
                                }
                            }
                            
                        }
                        else if (takeWhatChess == 'd')
                        {
                            if (chessNumOnBoard % 2 == 0)
                            {
                                chesses[i, j].BackColor = p1.color;
                                chesses[i, j].isUsedByP1 = true; chesses[i, j].isUsedByP2 = false;
                                chesses[i, j].isUsed = true;
                                p1.numD--;
                                right4.Text = $"覆蓋棋子:{p1.numD}顆";
                                checkAndChange();
                            }
                            else
                            {
                                chesses[i, j].BackColor = p2.color;
                                chesses[i, j].isUsedByP2 = true; chesses[i, j].isUsedByP1 = false;
                                chesses[i, j].isUsed = true;
                                p2.numD--;
                                left4.Text = $"覆蓋棋子:{p2.numD}顆";
                                checkAndChange();
                            }
                        }
                        break;
                    }              
                }         
        }
        private void checkAndChange()
        {
            //check if gameover
            checkEnd();
            //change player
            chessNumOnBoard++;
            takeChess = false;
            //button disabled
            buttonEnabled();
        }
        private void buttonEnabled()
        {
            right1.Enabled = false; right2.Enabled = false; right3.Enabled = false; right4.Enabled = false;
            left1.Enabled = false; left2.Enabled = false; left3.Enabled = false; left4.Enabled = false;
            if (chessNumOnBoard % 2 != 0)
            {               
                left1.Enabled = true;
                if (p2.numB != 0) left2.Enabled = true;
                if (p2.numC != 0) left3.Enabled = true;
                if (p2.numD != 0) left4.Enabled = true;
            }
            else
            {              
                right1.Enabled = true;
                if (p1.numB != 0) right2.Enabled = true;
                if (p1.numC != 0) right3.Enabled = true;
                if (p1.numD != 0) right4.Enabled = true;
            }
        }
        private void checkEnd()
        {
            int[] dx = {-1, -1, -1,  0, 0,  1, 1, 1};
            int[] dy = {-1,  0,  1, -1, 1, -1, 0, 1};
            int currentX, currentY, heap=1,chessNum=0;
            for (int i = 0; i < 19; i++) for (int j = 0; j < 19; j++)
                {
                    if (chesses[i, j].isUsed)
                    {
                        chessNum++;
                        if (chesses[i, j].isUsedByP1)
                        {   
                            for (int direc = 0; direc < 8; direc++)
                            {
                                //border handler
                                if ((i >= 0) && (i <= 3) && (direc == 0 || direc == 1 || direc == 2)) continue;
                                if ((i >= 15) && (i <= 18) && (direc == 5 || direc == 6 || direc == 7)) continue;
                                if ((j >= 0) && (j <= 3) && (direc == 0 || direc == 3 || direc == 5)) continue;
                                if ((j >= 15) && (j <= 18) && (direc == 2 || direc == 4 || direc == 7)) continue;                               
                                currentX = (i + dx[direc]);
                                currentY = (j + dy[direc]);
                                while (chesses[currentX, currentY].isUsedByP1)
                                {                                 
                                    heap++;
                                    currentX += dx[direc];
                                    currentY += dy[direc];
                                    if (heap == 5)
                                    {
                                        MessageBox.Show("P1贏了");
                                        System.Environment.Exit(System.Environment.ExitCode);
                                    }
                                }
                                heap = 1;
                            }
                        }
                        else if (chesses[i, j].isUsedByP2)
                        {
                            for (int direc = 0; direc < 8; direc++)
                            {
                                //border handler
                                if ((i >= 0) && (i <= 3) && (direc == 0 || direc == 1 || direc == 2)) continue;
                                if ((i >= 15) && (i <= 18) && (direc == 5 || direc == 6 || direc == 7)) continue;
                                if ((j >= 0) && (j <= 3) && (direc == 0 || direc == 3 || direc == 5)) continue;
                                if ((j >= 15) && (j <= 18) && (direc == 2 || direc == 4 || direc == 7)) continue;
                                currentX = (i + dx[direc]);
                                currentY = (j + dy[direc]);
                                while (chesses[currentX, currentY].isUsedByP2)
                                {
                                    heap++;
                                    currentX += dx[direc];
                                    currentY += dy[direc];
                                    if (heap == 5)
                                    {
                                        MessageBox.Show("P2贏了");
                                        System.Environment.Exit(System.Environment.ExitCode);
                                    }
                                }
                                heap = 1;
                            }
                        }
                    }
                    if(chessNum==361)
                    {
                        MessageBox.Show("平手");
                        System.Environment.Exit(System.Environment.ExitCode);
                    }
                }
        }
    }
    class Chess : System.Windows.Forms.Button
    {
        public bool isUsed = false;
        public bool isUsedByP1=false;
        public bool isUsedByP2=false;
    }
    class Character
    {
        public int numA=int.MaxValue;
        public int numB;
        public int numC;
        public int numD;
        public Color color;
    }
    class Warrior : Character
    {
        public Warrior()
        {
            this.numB = 0;
            this.numC = 0;
            this.numD = 5;
        }
        
    }
    class Witcher : Character
    {
        public Witcher()
        {
            this.numB = 1;
            this.numC = 2;
            this.numD = 2;
        }
    }
    class Archer : Character
    {
        public Archer()
        {
            this.numB = 1;
            this.numC = 1;
            this.numD = 3;
        }
    }
}
