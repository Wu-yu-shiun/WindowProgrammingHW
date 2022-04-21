//密碼替換表

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F44084074_W5_practice_1
{
    public partial class Form1 : Form
    {
        Sublist sublist = new Sublist();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            allSwitchOff();
            alphabet_in_order.Visible = true;
            alphabet_random.Visible = true;
            arrow.Visible = true;
            hint.Visible = true;
            random_generate.Visible = true;
            sure.Visible = true;
            alphabet_random.Text = sublist.Generate();
            sublist.Substitution = alphabet_random.Text;
            myRecord.AppendText("新的替換表" + Environment.NewLine + sublist.Substitution + Environment.NewLine + Environment.NewLine);
        }
        //buttons and textchanged handler
        private void random_generate_Click(object sender, EventArgs e)
        {
            if (sublist.IsValid(alphabet_random.Text)) alphabet_random.Text = sublist.Generate();
        }
        private void sure_Click(object sender, EventArgs e)
        {
            sublist.Substitution = alphabet_random.Text;
            myRecord.AppendText("新的替換表" + Environment.NewLine + sublist.Substitution + Environment.NewLine + Environment.NewLine);
        }
        private void alphabet_random_TextChanged(object sender, EventArgs e)
        {
            if (sublist.IsValid(alphabet_random.Text)) hint.Text = "合法替換表";
            else hint.Text = "替換表不合法，請重新輸入 ";
        }
        private void change_list_Click(object sender, EventArgs e)
        {
            title.Text = "替換表";
            allSwitchOff();
            alphabet_in_order.Visible = true;
            alphabet_random.Visible = true;
            arrow.Visible = true;
            hint.Visible = true;
            random_generate.Visible = true;
            sure.Visible = true;
        }
        private void encryption_Click(object sender, EventArgs e)
        {
            title.Text = "加密";
            allSwitchOff();
            panel1.Visible = true;
            input.Text = "輸入字串";
            output.Text = "加密結果";
        }
        private void decryption_Click(object sender, EventArgs e)
        {
            title.Text = "解密";
            allSwitchOff();
            panel1.Visible = true;
            input.Text = "輸入密文";
            output.Text = "解密結果";
        }
        private void record_Click(object sender, EventArgs e)
        {
            title.Text = "歷史紀錄";
            allSwitchOff();
            panel2.Visible = true;
        }
        //useful functions
        private void allSwitchOff()
        {
            alphabet_in_order.Visible = false;
            alphabet_random.Visible = false;
            arrow.Visible = false;
            hint.Visible = false;
            random_generate.Visible = false;
            sure.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            outputBox.Text = "";
            inputBox.Text = "";
        }
        private void confirm_Click(object sender, EventArgs e)
        {
            if (title.Text == "加密")
            {
                outputBox.Text = sublist.Encrypt(inputBox.Text);
                myRecord.AppendText("加密" + Environment.NewLine + 
                                    "明文: "+inputBox.Text + Environment.NewLine +
                                    "密文: "+outputBox.Text + Environment.NewLine + Environment.NewLine);
            }
            else
            {
                outputBox.Text = sublist.Decrypt(inputBox.Text);
                myRecord.AppendText("解密" + Environment.NewLine +
                                    "密文: " + inputBox.Text + Environment.NewLine +
                                    "明文: " + outputBox.Text + Environment.NewLine + Environment.NewLine);
            }
        }
    }

    public partial class Sublist
    {
        public String Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public String Substitution;
        public String Generate()
        {
            Random random = new Random();
            string temp = "";
            for (int i = 0; i < 52; i++)
            {
                int num = random.Next(0, Alphabet.Length);
                temp += Alphabet[num];
                Alphabet = Alphabet.Remove(num, 1);
            }
            //MessageBox.Show(Alphabet + "\n" + Substitution);
            Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return temp;
        }
        public bool IsValid(String crypto)
        {
            if (crypto.Length != 52) return false;
            for (int i = 0; i < 52; i++)
            {
                if (!crypto.Contains(this.Alphabet[i])) return false;
            }
            return true;
        }
        public String Encrypt(String text)
        {
            string myOutput = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ') myOutput += ' ';
                else myOutput += Substitution[Alphabet.IndexOf(text[i])];
            }
            return myOutput;
        }
        public String Decrypt(String text)
        {
            string myOutput = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ') myOutput += ' ';
                else myOutput += Alphabet[Substitution.IndexOf(text[i])];
            }
            return myOutput;
        }
    }
}
