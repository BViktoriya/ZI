using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // кодировать
        private void button1_Click(object sender, EventArgs e)
        {
            Shifr();
        }

        // декодировать
        private void button2_Click(object sender, EventArgs e)
        {
            DeShifr();
        }

        void Shifr()
        {
            string str = textBox1.Text;
            string[] str_array = str.Split(' ');
            str = "";
            for (int i = 0; i < str_array.Length; i++)
            {
                if (str_array[i] != "")
                {
                    str += str_array[i].Trim();
                    str += " ";
                }

            }

            string empty_conteiner = str;
            string message = textBox2.Text;
            string[] fill_conteiner = new string[empty_conteiner.Length * 2];

            string message_bin = "";

            StringBuilder textBinary = new StringBuilder();
            foreach (char c in message.ToCharArray())
                textBinary.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            message_bin = textBinary.ToString();
            message_bin += "00101010";
            
            int j = 0, k = 0; 

            for (int i = 0; i < empty_conteiner.Length; i++) 
            {
                fill_conteiner[j] = empty_conteiner[i].ToString();
                j++;
                if (k < message_bin.Length && (empty_conteiner[i] == '.' || empty_conteiner[i] == '!' || empty_conteiner[i] == '?') && empty_conteiner[i + 1] == ' ')
                {    // замена
                    if (message_bin[k] == '0')
                    {
                        fill_conteiner[j] = " ";
                        j++;
                    }
                    k++;
                }
            }
            for (int i = 0; i < fill_conteiner.Length; i++)
                textBox3.Text += fill_conteiner[i];
        }

        void DeShifr()
        {
            string fill_container = textBox3.Text;
            string message_bin = "";
            string message = "";
            int j = 0;

            for (int i = 0; i < fill_container.Length-2; i++)
            {
                if ((fill_container[i] == '.' || fill_container[i] == '!' || fill_container[i] == '?') && fill_container[i + 1] == ' ')
                {
                    if (fill_container[i + 2] == ' ')
                    {
                        //message_bin[j] = "0";
                        message_bin = message_bin.Insert(j, "0");
                        j++;
                    }
                    else
                    {
                        //message_bin[j] = "1";
                        message_bin = message_bin.Insert(j, "1");
                        j++;
                    }
                }
            }

            while (message_bin.Length > 0)
            {
                string char_binary = message_bin.Substring(0, 8);
                if (char_binary == "00101010")
                { break; }
                message_bin = message_bin.Remove(0, 8);

                int a = 0;
                int degree = char_binary.Length - 1;

                foreach (char c in char_binary)
                    a += Convert.ToInt32(c.ToString()) * (int)Math.Pow(2, degree--);

                message += ((char)a).ToString();
            }

            textBox4.Text = message;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
