//電子雞

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F44084074_W5_practice_2
{
    public partial class Form1 : Form
    {
        Pet mypet = new Pet();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            money.Text = "0";
            health.Text = "0%";
            weight.Text = "0g";
            satisfaction.Text = "0%";
            mood.Text = "0%";
            incident.Text = "請幫寵物取名";
            allButtonOff();
            inputName.Enabled = true;
            petName.Enabled = true;
        }
        //buttons
        private void feed_Click(object sender, EventArgs e)
        {
            if (mypet.MyMoney<10) myRecord.AppendText($"金錢不足無法餵食" + Environment.NewLine);
            else
            {
                mypet.Feed();
                updateNumber();
                myRecord.AppendText($"餵食了{mypet.Name}" + Environment.NewLine);
            }
        }
        private void play_Click(object sender, EventArgs e)
        {
            if(mypet.MyMoney<5) myRecord.AppendText($"金錢不足無法玩耍" + Environment.NewLine);
            else
            {
                mypet.Play();
                updateNumber();
                myRecord.AppendText($"與{mypet.Name}玩耍了" + Environment.NewLine);
            }
        }
        private void clean_Click(object sender, EventArgs e)
        {
            if (mypet.MyMoney < 5) myRecord.AppendText($"金錢不足無法打掃" + Environment.NewLine);
            else
            {
                mypet.Clean();
                updateNumber();
                myRecord.AppendText($"清理了{mypet.Name}" + Environment.NewLine);
            }
        }
        private void doctor_Click(object sender, EventArgs e)
        {
            if (mypet.MyMoney < 20) myRecord.AppendText($"金錢不足無法看醫生" + Environment.NewLine);
            else
            {
                mypet.GoToDoctor();
                updateNumber();
                myRecord.AppendText($"帶{mypet.Name}去看醫生" + Environment.NewLine);
            }
        }
        private void oneDayPast_Click(object sender, EventArgs e)
        {
            mypet.Satisfaction -= 20;
            mypet.Day++;
            if (mypet.Satisfaction == 0) mypet.Weight -= 200;
            if (mypet.Day > 10) mypet.Health -= 10;
            if (mypet.IsDirty) mypet.Health -= 30;
            if (mypet.IsSick)
            {
                mypet.Weight -= 150;
                mypet.Mood -= 20;
            }
            updateNumber();
            myRecord.AppendText(Environment.NewLine+$"第{mypet.Day}天"+Environment.NewLine);
            incident.Text = checkIncident();
        }

        private void inputName_Click(object sender, EventArgs e)
        {
            if (petName.Text == "") mypet.Name = "電子雞";
            else mypet.Name = $"{petName.Text}";
            allButtonOn();
            inputName.Enabled = false;
            petName.Enabled = false;
            updateNumber();
            incident.Text = $"{mypet.Name}出生了";
            myRecord.AppendText($"你開始養了{mypet.Name}"+Environment.NewLine+ Environment.NewLine +
                                $"第{mypet.Day}天" + Environment.NewLine);
        }
        //useful functions
        private void allButtonOff()
        {
            feed.Enabled = false;
            play.Enabled = false;
            clean.Enabled = false;
            doctor.Enabled = false;
            oneDayPast.Enabled = false;
            inputName.Enabled = false;
            petName.Enabled = false;
        }
        private void allButtonOn()
        {
            feed.Enabled = true;
            play.Enabled = true;
            clean.Enabled = true;
            doctor.Enabled = true;
            oneDayPast.Enabled = true;
            inputName.Enabled = true;
            petName.Enabled = true;
        }
        private void updateNumber()
        {
            money.Text = $"{mypet.MyMoney}元";
            health.Text = $"{mypet.Health}%";
            weight.Text = $"{mypet.Weight}g";
            satisfaction.Text = $"{mypet.Satisfaction}%";
            mood.Text = $"{mypet.Mood}%";
        }
        private string checkIncident()
        {
            string incident = "";
            //die
            if ((mypet.Health < 10) && (mypet.Weight < 1000) && (mypet.probability1(mypet.Mood)))
            {
                incident += $"{mypet.Name}死掉了 ";
                myRecord.AppendText($"{mypet.Name}死掉了，遊戲結束"+ Environment.NewLine);
                allButtonOff();
                return incident;
            }
            //shit
            if ( (mypet.IsDirty) || (mypet.Satisfaction > 50) )
            {
                mypet.IsDirty = true;
                incident += $"{mypet.Name}大便了 ";
                myRecord.AppendText($"{mypet.Name}大便了"+ Environment.NewLine);
            } 
            //lay egg
            if ( (mypet.Weight>1000) && (mypet.Health>60) && (mypet.probability2(mypet.Mood)) )
            {
                int earn =mypet.probability3(15,25);
                mypet.Health -= 5;
                mypet.MyMoney += earn;
                updateNumber();
                myRecord.AppendText($"{mypet.Name}下蛋了，賣掉蛋後獲得{earn}塊錢"+ Environment.NewLine);
            }
            //sick
            if( (mypet.IsSick) ||
               ((mypet.Health<50) && (mypet.Mood<=50) && (mypet.probability1(mypet.Health))) )
            {
                mypet.IsSick = true;
                incident += $"{mypet.Name}生病了 ";
                myRecord.AppendText($"{mypet.Name}生病了"+ Environment.NewLine);
            }
            //nothing happened
            if (incident == "")
            {
                incident += $"{mypet.Name}今天很乖";
                myRecord.AppendText("一天平安的過去了"+ Environment.NewLine);
            }
            return incident;
        }
    }
    
    public partial class Pet
    {
        public String Name;
        public int Day=1;
        public int MyMoney = 100;
        private int _health = 70;
        private int _weight = 700;
        private int _satisfaction = 70;
        private int _mood = 50;
        public int Health
        {
            get =>_health;
            set
            {
                if (value < 0) value = 0;
                if (value > 100) value = 100;
                _health = value;
            }
        }
        public int Weight
        {
            get => _weight;
            set
            {
                if (value < 600) value = 600;
                if (value > 4000) value =4000;
                _weight = value;
            }
        }
        public int Satisfaction
        {
            get => _satisfaction;
            set
            {
                if (value < 0) value = 0;
                if (value > 100) value = 100;
                _satisfaction = value;
            }
        }
        public int Mood
        {
            get => _mood;
            set
            {
                if (value < 0) value = 0;
                if (value > 100) value = 0;
                _mood = value;
            }
        }
        public bool IsDirty=false;
        public bool IsSick=false;
        public void Feed()
        {
            MyMoney -= 10;
            Satisfaction +=probability3(0,20);
            Weight += probability3(50, 100);
            if (IsDirty) Health -= 10;

        }
        public void Play()
        {
            MyMoney -= 5;
            Health+= probability3(0, 20);
            Mood+= probability3(0, 20);
            Satisfaction-= probability3(0, 20);
        }
        public void Clean()
        {
            MyMoney -= 5;
            IsDirty = false;
        }
        public void GoToDoctor()
        {
            MyMoney -= 20;
            Health += 30;
            Mood -= 20;
            IsSick = false;
        }
        //probability handler
        public bool probability1(int num)
        {
            Random random = new Random();
            int temp = random.Next(1, 101);
            if (temp <= (100 - num)) return true;
            else return false;
        }
        public bool probability2(int num)
        {
            Random random = new Random();
            int temp = random.Next(1, 101);
            if (temp <= num) return true;
            else return false;
        }
        public int probability3(int num1, int num2)
        {
            Random random = new Random();
            return random.Next(num1, num2 + 1);
        }
    }  
}
