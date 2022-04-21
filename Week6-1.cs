//數字的七段顯示器

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F44084074_W6_practice_1
{
    public partial class 七段顯示器 : Form
    {
        int numX = 0, numY = 0, currentNumX=0, currentNumY=0;
        int boardWidth = 200, boardLength = 300;
        int blockWidth = 0, blockLength = 0;
        int space = 2;
        int inputNumber = 0;
        public 七段顯示器()
        {
            InitializeComponent();
        }
        private void 七段顯示器_Load(object sender, EventArgs e)
        {
            number.Enabled = false;
        }

        private void number_TextChanged(object sender, EventArgs e)
        {
            checkNum();
        }
        private void sure_Click(object sender, EventArgs e)
        {
            try
            {
                numY=int.Parse(height.Text);
                numX=int.Parse(width.Text);
            }
            catch
            {
                number.Enabled = false;
                MessageBox.Show("請輸入數字","警告",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            if (numY < 7 || numY > 15 || numX < 5 || numX > 10)
            {
                number.Enabled = false;
                MessageBox.Show("請輸入範圍內數字", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }   
            else if ((numY % 2) == 0)
            {
                number.Enabled = false;
                MessageBox.Show("高不能為偶數", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }      
            else number.Enabled = true;
            //remove old board
            for (int i = 0; i < currentNumY; i++)
            {
                for (int j = 0; j < currentNumX; j++)
                {
                    Controls.RemoveByKey($"board1-{i *currentNumX + j}");
                    Controls.RemoveByKey($"board2-{i * currentNumX + j}");
                }
            }
            //create board
            blockWidth = (boardWidth - space * (numX - 1)) / numX;
            blockLength = (boardLength - space * (numY - 1)) / numY;
            for (int i = 0; i < numY; i++)
            {
                for (int j = 0; j < numX; j++)
                {
                    Button button = new Button();
                    button.Name = $"board1-{i * numX + j}";
                    button.BackColor = Color.White;
                    button.Size = new Size(blockWidth,blockLength);
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = Color.Silver;
                    button.Location = new Point(200+j*(blockWidth+space) , 50+i*(blockLength+space));
                    Controls.Add(button);
                }
            }
            for (int i = 0; i < numY; i++)
            {
                for (int j = 0; j < numX; j++)
                {
                    Button button = new Button();
                    button.Name = $"board2-{i * numX + j}";
                    button.BackColor = Color.White;
                    button.Size = new Size(blockWidth, blockLength);
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = Color.Silver;
                    button.Location = new Point(420 + j * (blockWidth + space), 50 + i * (blockLength + space));
                    Controls.Add(button);
                }
            }
            currentNumX = numX;
            currentNumY = numY;
            //change color(if input number exist already)
            checkNum();
        }
        //my functions
        private void checkNum()
        {
            try
            {
                if (number.Text == "")
                {
                    initColor();
                    return;
                }
                else inputNumber = int.Parse(number.Text);
            }
            catch
            {
                initColor();
                return;
            }
            int leftNum, rightNum;
            if (inputNumber >= -9 && inputNumber <= 99)
            {
                if (inputNumber < 0)
                {
                    leftNum = 10;
                    rightNum = Math.Abs(inputNumber);
                }
                else
                {
                    leftNum = inputNumber / 10;
                    rightNum = inputNumber % 10;
                }
                changeColor(leftNum, 1);
                changeColor(rightNum, 2);
            }
            else initColor();
        }
        private void initColor()
        {
            for (int i = 0; i < currentNumY; i++)
            {
                for (int j = 0; j < currentNumX; j++)
                {
                    Button leftButton = Controls[$"board1-{i * currentNumX + j}"] as Button;
                    Button rightButton = Controls[$"board2-{i * currentNumX + j}"] as Button;
                    leftButton.BackColor = Color.White;
                    rightButton.BackColor = Color.White;
                }
            }
        }
        private void changeColor(int num,int board)
        {
            switch (num)
            {
                case 0:
                    for (int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if( (i==0||i==(currentNumY-1)) && (j!=0) && (j!=currentNumX-1) )
                                button.BackColor = Color.Blue;
                            else if( (j==0||j==(currentNumX-1)) && (i!=0) && (i!=(currentNumY-1)/2) && (i!=currentNumY-1) )
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;                           
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if ( (j==currentNumX-1) && (i!=0) && (i != (currentNumY - 1) / 2) && (i != currentNumY - 1) )
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if ( (i==0 || i==(currentNumY-1)/2 ||i==currentNumY-1) && (j!=0) && (j!=currentNumX-1) )
                                button.BackColor = Color.Blue;
                            else if( (j==0) && (i>(currentNumY-1)/2) && (i<currentNumY-1) )
                                button.BackColor = Color.Blue;
                            else if ( (j == currentNumX-1) && (i < (currentNumY - 1) / 2) && (i > 0) )
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if ((i == 0 || i == (currentNumY - 1) / 2 || i == currentNumY - 1) && (j != 0) && (j != currentNumX - 1))
                                button.BackColor = Color.Blue;
                            else if ( (j == currentNumX - 1) && (i != (currentNumY - 1) / 2) && (i != 0) && (i!=currentNumY-1) )
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if (( i == (currentNumY - 1) / 2 ) && (j != 0) && (j != currentNumX - 1))
                                button.BackColor = Color.Blue;
                            else if ((j == currentNumX - 1) && (i != (currentNumY - 1) / 2) && (i != 0) && (i != currentNumY - 1))
                                button.BackColor = Color.Blue;
                            else if ((j == 0) && (i < (currentNumY - 1) / 2) && (i > 0))
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;
                        }
                    }
                    break;
                case 5:
                    for (int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if ((i == 0 || i == (currentNumY - 1) / 2 || i == currentNumY - 1) && (j != 0) && (j != currentNumX - 1))
                                button.BackColor = Color.Blue;
                            else if ((j == currentNumX - 1) && (i > (currentNumY - 1) / 2) && (i < currentNumY - 1))
                                button.BackColor = Color.Blue;
                            else if ((j == 0) && (i < (currentNumY - 1) / 2) && (i > 0))
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;
                        }
                    }
                    break;
                case 6:
                    for (int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if ((i == 0 || i == (currentNumY - 1) / 2 || i == currentNumY - 1) && (j != 0) && (j != currentNumX - 1))
                                button.BackColor = Color.Blue;
                            else if ((j == currentNumX - 1) && (i > (currentNumY - 1) / 2) && (i < currentNumY - 1))
                                button.BackColor = Color.Blue;
                            else if ((j == 0) && (i != (currentNumY - 1) / 2) && (i != 0) && (i!=currentNumY-1) )
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;
                        }
                    }
                    break;
                case 7:
                    for (int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if ( (j == currentNumX - 1) && (i != 0) && (i != (currentNumY - 1) / 2) && (i != currentNumY - 1) )
                                button.BackColor = Color.Blue;
                            else if( (i==0) && (j!=0) && (j!=currentNumX-1) )
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;
                        }
                    }
                    break;
                case 8:
                    for (int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if ((i == 0 || i == (currentNumY - 1) / 2 || i == currentNumY - 1) && (j != 0) && (j != currentNumX - 1))
                                button.BackColor = Color.Blue;
                            else if ((j == currentNumX - 1 || j==0) && (i != (currentNumY - 1) / 2) && (i != 0) && (i != currentNumY - 1))
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;
                        }
                    }
                    break;
                case 9:
                    for (int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if ((i == 0 || i == (currentNumY - 1) / 2 || i == currentNumY - 1) && (j != 0) && (j != currentNumX - 1))
                                button.BackColor = Color.Blue;
                            else if ((j == currentNumX - 1) && (i != (currentNumY - 1) / 2) && (i != currentNumY - 1) && (i!=0) )
                                button.BackColor = Color.Blue;
                            else if ((j == 0) && (i < (currentNumY - 1) / 2) && (i > 0))
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;
                        }
                    }
                    break;
                case 10:
                    for(int i = 0; i < currentNumY; i++)
                    {
                        for (int j = 0; j < currentNumX; j++)
                        {
                            Button button = Controls[$"board{board}-{i * currentNumX + j}"] as Button;
                            if ( (i==(currentNumY-1)/2) && (j!=0) && (j!=currentNumX-1) )
                                button.BackColor = Color.Blue;
                            else button.BackColor = Color.White;
                        }
                    }
                    break;
            }
        }
    }
}
