//年曆

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F44084074_W3_practice_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] daysInMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            string[] monthName = new string[] { "January", "February","March","April","May","June",
                                                "July","August","September","October","November","December" };
            int jan1, monthBegin;

            //input1 error handling
            Console.Write("1月1日是星期幾(1~7):");
            try
            {
                jan1 = int.Parse(Console.ReadLine());
            }
            catch 
            {
                Console.WriteLine("請輸入範圍內整數");
                Console.ReadKey();
                return;
            }
            if (jan1 > 7 || jan1 < 1)
            {
                Console.WriteLine("超出範圍");
                Console.ReadKey();
                return;
            }

            //input2 error handling
            Console.Write("從幾月開始(1~12):");
            try
            {
                monthBegin = int.Parse(Console.ReadLine());
            }
            catch 
            {
                Console.WriteLine("請輸入範圍內整數");
                Console.ReadKey();
                return;
            }
            if (monthBegin > 12 || monthBegin < 1)
            {
                Console.WriteLine("超出範圍");
                Console.ReadKey();
                return;
            }

            //print calender
            for (int i = monthBegin; i <= 12; i++)
            {
                int dayPast = jan1 - 1, firstDayOfWeek;
                int sevendays = 0;

                //calculate days past and first day of week
                for (int j = 0; j < i - 1; j++)
                {
                    dayPast += daysInMonth[j];
                }
                firstDayOfWeek = (dayPast % 7) + 1;
                //print title
                Console.WriteLine(monthName[i - 1]);
                Console.WriteLine(" Mon Tue Wed Thu Fri Sat Sun");
                //print spaces 
                for (int j = 0; j < firstDayOfWeek - 1; j++)
                {
                    Console.Write("    ");
                    sevendays++;

                }
                //print number
                for (int j = 1; j <= daysInMonth[i - 1]; j++)
                {
                    if (j < 10)
                    {
                        Console.Write("   {0}", j);
                    }
                    else
                    {
                        Console.Write("  {0}", j);
                    }
                    sevendays++;
                    //next line
                    if (sevendays == 7)
                    {
                        Console.WriteLine("");
                        sevendays = 0;
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
