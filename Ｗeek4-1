//21點

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F44084074_W4_practice_1
{
    class Program
    {
        private static string[] cards;
        private static string[] bag1, bag2;
        private static int cardNumInBag1, cardNumInBag2, points1, points2;
        private static void getOneCard(ref string[] bag, ref int cardNum)
        {
            cardNum++;
            Random random = new Random();
            int num = random.Next(0, cards.Length);
            string getCard = cards[num];
            bag[cardNum - 1] = getCard;
            //remove card(change array to list)
            var numberList = cards.ToList();
            numberList.Remove(cards[num]);
            cards = numberList.ToArray();
        }
        private static int calculatePoints(ref string[] bag, ref int cardNum)
        {
            int points = 0, numOfA = 0;
            for (int i = 0; i < cardNum; i++)
            {
                string[] seperate = bag[i].Split(' ');
                if (seperate[1] == "10" || seperate[1] == "J" || seperate[1] == "Q" || seperate[1] == "K") points += 10;
                else if (seperate[1] == "A") numOfA++;
                else points += int.Parse(seperate[1]);
            }
            if (numOfA == 1)
            {
                if (points < 11) points += 11;
                else points++;
            }
            else if (numOfA == 2)
            {
                if (points < 10) points += 12;
                else points += 2;
            }
            else if (numOfA == 3)
            {
                if (points < 9) points += 13;
                else points += 3;
            }
            else if (numOfA == 4)
            {
                if (points < 8) points += 14;
                else points += 4;
            }
            return points;
        }

        static void Main(string[] args)
        {
            try
            {
                int money1, money2, bet1, bet2;
                string[] suits = { "Spade", "Heart", "Diamond", "Club" };
                string[] numbers = { " A", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", " 10", " J", " Q", " K" };
                Console.Write("玩家1初始金錢: ");
                money1 = int.Parse(Console.ReadLine());
                Console.Write("玩家2初始金錢: ");
                money2 = int.Parse(Console.ReadLine());
                Console.WriteLine("");

                while (money1 > 0 && money2 > 0)
                {
                    //init
                    bool gameover = false;
                    cards = new string[52]; bag1 = new string[10]; bag2 = new string[10];
                    cardNumInBag1 = 0; cardNumInBag2 = 0; points1 = 0; points2 = 0;
                    for (int i = 0; i < 4; i++) for (int j = 0; j < 13; j++) cards[i * 13 + j] = suits[i] + numbers[j];
                    //player1 start
                    getOneCard(ref bag1, ref cardNumInBag1);
                    getOneCard(ref bag1, ref cardNumInBag1);
                    Console.WriteLine("玩家1手牌: " + bag1[0] + ", " + bag1[1]);
                    points1 = calculatePoints(ref bag1, ref cardNumInBag1);
                    Console.WriteLine("玩家1目前點數: " + points1);
                    Console.WriteLine("玩家1目前金錢: " + money1);
                    while (true)
                    {
                        Console.Write("請輸入下注金額: ");
                        bet1 = int.Parse(Console.ReadLine());
                        if (bet1 > money1) Console.WriteLine("金錢不足，請重新輸入!");
                        else if (bet1 == 0) Console.WriteLine("金錢不能為零，請重新輸入!");
                        else break;
                    }
                    Console.WriteLine("");
                    //player2 start
                    getOneCard(ref bag2, ref cardNumInBag2);
                    getOneCard(ref bag2, ref cardNumInBag2);
                    Console.WriteLine("玩家2手牌: " + bag2[0] + ", " + bag2[1]);
                    points2 = calculatePoints(ref bag2, ref cardNumInBag2);
                    Console.WriteLine("玩家2目前點數: " + points2);
                    Console.WriteLine("玩家2目前金錢: " + money2);
                    while (true)
                    {
                        Console.Write("請輸入下注金額: ");
                        bet2 = int.Parse(Console.ReadLine());
                        if (bet2 > money2) Console.WriteLine("金錢不足，請重新輸入!");
                        else if (bet2 == 0) Console.WriteLine("金錢不能為零，請重新輸入!");
                        else break;
                    }
                    Console.WriteLine("");
                    //player1 round
                    while (true)
                    {
                        Console.WriteLine("玩家1行動(輸入1抽1張排、輸入P停止抽牌):");
                        string input = Console.ReadLine();
                        if (input == "1")
                        {
                            getOneCard(ref bag1, ref cardNumInBag1);
                            Console.Write("玩家1手牌: ");
                            for (int i = 0; i < cardNumInBag1; i++)
                            {
                                if (i == (cardNumInBag1 - 1)) Console.Write(bag1[i] + "\n");
                                else Console.Write(bag1[i] + ", ");
                            }
                            points1 = calculatePoints(ref bag1, ref cardNumInBag1);
                            Console.WriteLine("玩家1目前點數: " + points1);
                            if (points1 > 21)
                            {
                                money1 -= bet1;
                                money2 += bet1;
                                Console.WriteLine("玩家1爆了，玩家2獲勝！\n");
                                Console.WriteLine("玩家2獲勝，獲得" + bet1 + "金錢\n");
                                gameover = true;
                                break;
                            }
                        }
                        else if (input == "P")
                        {
                            Console.WriteLine("玩家1跳過，目前點數: " + points1 + "\n");
                            break;
                        }
                        else Console.WriteLine("請輸入1或P");
                    }
                    if (gameover == true)
                    {
                        Console.WriteLine("-------------------------------------");
                        continue;
                    }
                    //player2 round
                    while (true)
                    {
                        Console.WriteLine("玩家2行動(輸入1抽1張排、輸入P停止抽牌):");
                        string input = Console.ReadLine();
                        if (input == "1")
                        {
                            getOneCard(ref bag2, ref cardNumInBag2);
                            Console.Write("玩家2手牌: ");
                            for (int i = 0; i < cardNumInBag2; i++)
                            {
                                if (i == (cardNumInBag2 - 1)) Console.Write(bag2[i] + "\n");
                                else Console.Write(bag2[i] + ", ");
                            }
                            points2 = calculatePoints(ref bag2, ref cardNumInBag2);
                            Console.WriteLine("玩家2目前點數: " + points2);
                            if (points2 > 21)
                            {
                                money2 -= bet2;
                                money1 += bet2;
                                Console.WriteLine("玩家2爆了，玩家1獲勝！\n");
                                Console.WriteLine("玩家1獲勝，獲得" + bet2 + "金錢");
                                gameover = true;
                                break;
                            }
                        }
                        else if (input == "P")
                        {
                            Console.WriteLine("玩家2跳過，目前點數: " + points2 + "\n");
                            break;
                        }
                        else Console.WriteLine("請輸入1或P");
                    }
                    if (gameover == true)
                    {
                        Console.WriteLine("-------------------------------------");
                        continue;
                    }
                    //both pause
                    if (gameover == false)
                    {
                        if (points1 > points2)
                        {
                            money2 -= bet2;
                            money1 += bet2;
                            Console.WriteLine("玩家1獲勝，獲得" + bet2 + "金錢");
                        }
                        else if (points1 < points2)
                        {
                            money1 -= bet1;
                            money2 += bet1;
                            Console.WriteLine("玩家2獲勝，獲得" + bet1 + "金錢");
                        }
                        else Console.WriteLine("平手！拿回各自的錢");
                        Console.WriteLine("-------------------------------------");
                        continue;
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("請輸入正確格式");
            }
            Console.ReadKey();
        }
    }
}
