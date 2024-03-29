//踩地雷

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F44084074_W3_practice_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] coorX = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] coorY = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int mapSize, bombNum;
            int[] loc = new int[2];

            //input1 error handling
            Console.Write("地圖大小(1~10):");
            try
            {
                mapSize = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("請輸入範圍內的整數");
                Console.ReadKey();
                return;
            }
            if (mapSize > 10 || mapSize < 1)
            {
                Console.WriteLine("超出範圍");
                Console.ReadKey();
                return;
            }
            //map and map's border
            int[,] hint = new int[mapSize + 2, mapSize + 2];
            bool[,] isbombed = new bool[mapSize + 2, mapSize + 2];

            //input2 error handling
            Console.Write("地雷數量(1~10):");
            try
            {
                bombNum = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("請輸入範圍內的整數");
                Console.ReadKey();
                return;
            }
            if (bombNum > 10 || bombNum < 1)
            {
                Console.WriteLine("超出範圍");
                Console.ReadKey();
                return;
            }
            // bomb's location
            int[,] bombLoc = new int[bombNum, 2];

            //input3 error handling
            for (int i = 0; i < bombNum; i++)
            {
                try
                {
                    Console.Write("第{0}個地雷的位置:", i + 1);
                    string input = Console.ReadLine();//input a string
                    string[] stringLoc = input.Split(' ');//split the string to string array
                    for (int j = 0; j < stringLoc.Length; j++)
                    {
                        loc[j] = int.Parse(stringLoc[j]);//sting->int
                    }
                }
                catch
                {
                    Console.WriteLine("請輸入兩個以空白區隔的整數");
                    Console.ReadKey();
                    return;
                }

                bombLoc[i, 0] = loc[0] + 1;
                bombLoc[i, 1] = loc[1] + 1;
                if (bombLoc[i, 0] > mapSize || bombLoc[i, 0] < 0 || bombLoc[i, 1] > mapSize || bombLoc[i, 1] < 0)
                {
                    Console.WriteLine("地雷位置超出範圍");
                    Console.ReadKey();
                    return;
                }
            }
            //initialize
            for (int i = 0; i < mapSize + 2; i++)
            {
                for (int j = 0; j < mapSize + 2; j++)
                {
                    hint[i, j] = 0;
                    isbombed[i, j] = false;
                }
            }

            //put bomb and hint
            for (int i = 0; i < bombNum; i++)
            {
                isbombed[bombLoc[i, 0], bombLoc[i, 1]] = true;
                for (int j = 0; j < 8; j++)
                {
                    hint[bombLoc[i, 0] + coorX[j], bombLoc[i, 1] + coorY[j]]++;
                }
            }

            //print
            Console.WriteLine("---");
            for (int j = 1; j < mapSize + 1; j++)
            {
                for (int i = 1; i < mapSize + 1; i++)
                {
                    Console.Write(isbombed[i, j] ? "x" : "{0}", hint[i, j]);
                }
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
