//推箱子

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F44084074_吳雨勳Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            int mapH, mapW;
            string temp;
            char[,] map;
            bool[,] isWall;
            Console.Write("請輸入(高,寬): ");
            temp = Console.ReadLine();
            string[] input1 = temp.Split(',');
            mapH = int.Parse(input1[0]);
            mapW = int.Parse(input1[1]);
            map = new char[mapH, mapW];
            isWall = new bool[mapH, mapW];

            int[] playerLoc = new int[2];
            int[] destinationLoc = new int[2];
            int[] boxLoc = new int[2];
            int[] nextLoc = new int[2];

            Console.WriteLine("請輸入地圖: ");
            for (int i = 0; i < mapH; i++)
            {
                temp = Console.ReadLine();
                for (int j = 0; j < temp.Length; j++)
                {
                    map[i, j] = temp[j];
                    if (map[i, j] == 'X') isWall[i, j] = true;
                    else isWall[i, j] = false;
                }
            }

            //find player,destination,box location
            for (int i = 0; i < mapH; i++) for (int j = 0; j < mapW; j++)
                {
                    if (map[i, j] == 'P')
                    {
                        playerLoc[0] = i;
                        playerLoc[1] = j;
                    }
                    else if (map[i, j] == '+')
                    {
                        destinationLoc[0] = i;
                        destinationLoc[1] = j;
                    }
                    else if (map[i, j] == 'O')
                    {
                        boxLoc[0] = i;
                        boxLoc[1] = j;
                    }
                }

            Console.Write("請輸入指令: ");
            temp = Console.ReadLine();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == 'w')
                {
                    nextLoc[0] = playerLoc[0] - 1;
                    nextLoc[1] = playerLoc[1];

                }
                else if (temp[i] == 'a')
                {
                    nextLoc[0] = playerLoc[0];
                    nextLoc[1] = playerLoc[1] - 1;
                }
                else if (temp[i] == 's')
                {
                    nextLoc[0] = playerLoc[0] + 1;
                    nextLoc[1] = playerLoc[1];
                }
                else if (temp[i] == 'd')
                {
                    nextLoc[0] = playerLoc[0];
                    nextLoc[1] = playerLoc[1] + 1;
                }
                if (map[nextLoc[0], nextLoc[1]] == 'X') continue;  //if player collapse wall              
                else if (nextLoc[0] == boxLoc[0] && nextLoc[1] == boxLoc[1])       //if player collapse box
                {
                    if (temp[i] == 'w')
                    {
                        if (map[boxLoc[0] - 1, boxLoc[1]] == 'X') continue;
                        else
                        {
                            boxLoc[0]--;
                            playerLoc[0]--;
                        }
                    }
                    else if (temp[i] == 'a')
                    {
                        if (map[boxLoc[0], boxLoc[1] - 1] == 'X') continue;
                        else
                        {
                            boxLoc[1]--;
                            playerLoc[1]--;
                        }
                    }
                    else if (temp[i] == 's')
                    {
                        if (map[boxLoc[0] + 1, boxLoc[1]] == 'X') continue;
                        else
                        {
                            boxLoc[0]++;
                            playerLoc[0]++;
                        }
                    }
                    else if (temp[i] == 'd')
                    {
                        if (map[boxLoc[0], boxLoc[1] + 1] == 'X') continue;
                        else
                        {
                            boxLoc[1]++;
                            playerLoc[1]++;
                        }
                    }
                }
                else
                {
                    playerLoc[0] = nextLoc[0];
                    playerLoc[1] = nextLoc[1];
                }
                //if box collapse destination
                if (map[boxLoc[0], boxLoc[1]] == '+') break;
            }

            //print
            for (int i = 0; i < mapH; i++)
            {
                for (int j = 0; j < mapW; j++)
                {
                    if (isWall[i, j])
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        if (i == playerLoc[0] && j == playerLoc[1])
                        {
                            Console.Write("P");
                        }
                        else if (i == boxLoc[0] && j == boxLoc[1])
                        {
                            Console.Write("O");
                        }
                        else if (i == destinationLoc[0] && j == destinationLoc[1])
                        {
                            if (!((destinationLoc[0] == playerLoc[0] && destinationLoc[1] == playerLoc[1]) ||
                                 (destinationLoc[0] == boxLoc[0] && destinationLoc[1] == boxLoc[1])))
                            {
                                Console.Write("+");
                            }
                        }
                        else Console.Write(" ");
                    }
                }
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
