//走迷宮的最短路徑

using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F44084074_W4_practice_2
{
    class Program
    {
        private static int mapX, mapY, step = 0, starNum = 0;
        private static bool findRoute = false;
        private static int[] dx = { -1, 1, 0, 0 };
        private static int[] dy = { 0, 0, -1, 1 };
        private static char[,] map;
        private static bool[,] isUsed;
        private static Queue queue = new Queue();
        private static void bfs()
        {
            int[] start = new int[2];
            int[] end = new int[2];
            string[] record = new string[100];
            //find start and end
            for (int i = 0; i < mapY; i++)
            {
                for (int j = 0; j < mapX; j++)
                {
                    if (map[j, i] == '0')
                    {
                        start[0] = j;
                        start[1] = i;
                    }
                    else if (map[j, i] == 'x')
                    {
                        end[0] = j;
                        end[1] = i;
                    }
                }
            }
            //search route
            SearchNextRoute(start[0], start[1]);
            record[0] = $"{start[0]},{start[1]}";
            while (queue.Count > 0 && !findRoute)
            {
                record[step] = (string)queue.Peek();
                string[] nextLoc = ((string)queue.Dequeue()).Split(',');
                SearchNextRoute(int.Parse(nextLoc[0]), int.Parse(nextLoc[1]));
            }
            //mark the shortest route(backtracking the record)
            if (findRoute)
            {
                int[] nowLoc = { end[0], end[1] };
                while (!((nowLoc[0] == start[0]) && (nowLoc[1] == start[1])))
                {
                    //four direction:i=0(left),i=1(right),i=2(up),i=3(down)
                    for (int i = 0; i < 4; i++)
                    {
                        int x = nowLoc[0] + dx[i];
                        int y = nowLoc[1] + dy[i];
                        bool findRecord = false;
                        int searchRecordRound = 0;
                        //check every record
                        for (int j = step - 1; j >= 0; j--)
                        {
                            searchRecordRound++;
                            string[] loc = record[j].Split(',');
                            int[] locInt = new int[2];
                            locInt[0] = int.Parse(loc[0]);
                            locInt[1] = int.Parse(loc[1]);
                            //find record
                            if (x == locInt[0] && y == locInt[1])
                            {
                                findRecord = true;
                                if (!((x == start[0]) && (y == start[1]))) map[x, y] = '*';
                                nowLoc[0] = x;
                                nowLoc[1] = y;
                                step -= searchRecordRound;
                                break;
                            }
                        }
                        if (findRecord) break;
                    }
                }
            }
        }
        private static void SearchNextRoute(int x, int y)
        {
            step++;
            //four direction:i=0(left),i=1(right),i=2(up),i=3(down)
            for (int i = 0; i < 4; i++)
            {
                //neglect boundry
                if (findRoute) break;
                if (x == 0) if (i == 0) continue;
                if (x == (mapX - 1)) if (i == 1) continue;
                if (y == 0) if (i == 2) continue;
                if (y == (mapY - 1)) if (i == 3) continue;

                int[] nextLoc = { x + dx[i], y + dy[i] };
                if (map[nextLoc[0], nextLoc[1]] == 'x')
                {
                    findRoute = true;
                    return;
                }
                if (!isUsed[nextLoc[0], nextLoc[1]])
                {
                    queue.Enqueue($"{nextLoc[0]},{nextLoc[1]}");
                    isUsed[nextLoc[0], nextLoc[1]] = true;
                }
            }
        }
        static void Main(string[] args)
        {
            //input            
            Console.Write("請輸入迷宮大小(底,高): ");
            string[] mapSize = Console.ReadLine().Split(',');
            mapX = int.Parse(mapSize[0]);
            mapY = int.Parse(mapSize[1]);
            map = new char[mapX, mapY];
            isUsed = new bool[mapX, mapY];
            for (int i = 0; i < mapY; i++)
            {
                string inputRow = Console.ReadLine();
                for (int j = 0; j < mapX; j++)
                {
                    map[j, i] = (char)inputRow[j];
                    if (map[j, i] != ' ') isUsed[j, i] = true;
                    else isUsed[j, i] = false;
                }
            }
            //search route
            bfs();
            //print
            Console.WriteLine("\nOutput:");
            for (int i = 0; i < mapY; i++)
            {
                for (int j = 0; j < mapX; j++)
                {
                    Console.Write(map[j, i]);
                    if (map[j, i] == '*') starNum++;
                }
                Console.WriteLine("");
            }
            Console.WriteLine(findRoute ? $"{starNum}" : "沒有路徑");
            Console.ReadKey();
        }
    }
}
