using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kodGreya
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            radioButton1.Checked = true;
            radioButton3.Checked = true;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }
        public Form1()
        {
           
            InitializeComponent();
        }
        string Gre, Gre1;
        bool Coding = false;
        bool Decoding = false;
        static bool Left = false;
        static bool Right = false;
        static BitArray Array;
        static BitArray Array1;
        static BitArray mDecoded1;
        static int counter;

        static BitArray ConvertFileToBitArray(string path)
        {
            byte[] fileBytes = File.ReadAllBytes(path);
            BitArray Array = new BitArray(fileBytes);

            return Array;
        }
        public static byte[] BitArrayToBytes(System.Collections.BitArray Coded1) // переводимо масив бітів в масив байтів
        {
            if (Coded1.Length == 0)
            {
                throw new System.ArgumentException("must have at least length 1", "bitarray");
            }

            int num_bytes = Coded1.Length / 8;

            if (Coded1.Length % 8 != 0)
            {
                num_bytes += 1;
            }

            var bytes = new byte[num_bytes];
            Coded1.CopyTo(bytes, 0);
            return bytes;
        }

       
        static BitArray MyCoding(BitArray Array)
        {
            if (Left == true)
            {
                int flag = 0;
                int count = Array.Count; // кількість біт в масиві
                BitArray Coded = new BitArray(count, false); // новий пустий масив біт
                for (int i = 0; i < count; i += 5)
                {
                    if (flag == 1)
                    {
                        break;
                    }
                    BitArray dviyc = new BitArray(5);

                    for (int j = 0; j < 5; j++)
                    {
                        if (j + i >= count)
                        {
                            dviyc[j] = false;
                        }
                        else
                        {
                            dviyc[j] = Array[j + i];
                        }
                    }
                    BitArray dviy1 = new BitArray(5);
                    BitArray resultat = new BitArray(5);
                    for (int l = 0; l < 4; l++)
                    {
                        dviy1[l + 1] = dviyc[l];
                    }
                    for (int l = 0; l < 5; l++)
                    {
                        resultat[l] = dviyc[l] ^ dviy1[l];
                    }
                    for (int k = 0; k < 5; k++)
                    {
                        if (k + (5 * (i / 5)) == count)
                        {
                            flag = 1;
                            break;
                        }
                        Coded[k + (5 * (i / 5))] = resultat[k];
                    }
                }
                return Coded;
            }
            if(Right == true)
            {
                int flag = 0;
                int count = Array.Count; // кількість біт в масиві
                BitArray Coded = new BitArray(count, false); // новий пустий масив біт
                for (int i = 0; i < count; i += 5)
                {
                    if (flag == 1)
                    {
                        break;
                    }
                    BitArray dviy = new BitArray(5);

                    for (int j = 0; j < 5; j++)
                    {
                        if (j + i >= count)
                        {
                            dviy[j] = false;
                        }
                        else
                        {
                            dviy[j] = Array[j + i];
                        }
                    }
                    BitArray dviy1 = new BitArray(5);
                    BitArray resultat = new BitArray(5);
                    for (int l = 0; l < 4; l++)
                    {
                        dviy1[l] = dviy[l + 1];
                    }
                    for (int l = 0; l < 5; l++)
                    {
                        resultat[l] = dviy[l] ^ dviy1[l];
                    }
                    for (int k = 0; k < 5; k++)
                    {
                        if (k + (5 * (i / 5)) == count)
                        {
                            flag = 1;
                            break;
                        }
                        Coded[k + (5 * (i / 5))] = resultat[k];
                    }
                }
                return Coded;
            }
            else
            {
                return Array;
            }
        }
        static BitArray MyDeCoding(BitArray Array1)
        {
            if (Left == true)
            {
                int flag = 0;
                int count = Array1.Count; // кількість біт в масиві
                BitArray Codedd = new BitArray(counter, false);
                BitArray inf = new BitArray(5); //11101
                for (int i = 0; i < count; i += 5)
                {
                    if (flag == 1)
                    {
                        break;
                    }
                    for (int j = 0; j < 5; j++)
                    {
                        if (j + i >= count)
                        {
                            inf[j] = false;
                        }
                        else
                        {
                            inf[j] = Array1[j + i];
                        }
                    }
                    BitArray rd0 = new BitArray(5);
                    BitArray rd1 = new BitArray(5);
                    BitArray rd2 = new BitArray(5);
                    BitArray rd3 = new BitArray(5);
                    BitArray rd4 = new BitArray(5);
                    for (int l = 0; l < 5; l++)
                    {
                        rd0[l] = inf[l]; //11101
                    }
                    for (int l = 0; l < 4; l++)
                    {
                        rd1[l + 1] = inf[l]; //01110
                    }
                    for (int l = 0; l < 3; l++)
                    {
                        rd2[l + 2] = inf[l]; //00111
                    }
                    for (int l = 0; l < 2; l++)
                    {
                        rd3[l + 3] = inf[l]; //00011
                    }
                    for (int l = 0; l < 1; l++)
                    {
                        rd4[l + 4] = inf[l]; //00001
                    }
                    for (int l = 0; l < 5; l++)
                    {
                        if (l + (5 * (i / 5)) == count)
                        {
                            flag = 1;
                            break;
                        }
                        Codedd[l + (5 * (i / 5))] = rd0[l] ^ rd1[l] ^ rd2[l] ^ rd3[l] ^ rd4[l];
                    }
                }
                return Codedd;
            }
            if(Right == true)
            {
                int flag = 0;
                int count = Array1.Count; // кількість біт в масиві
                BitArray Coded = new BitArray(count, false);
                BitArray inf = new BitArray(5); //11101
                for (int i = 0; i < count; i += 5)
                {
                    if (flag == 1)
                    {
                        break;
                    }
                    for (int j = 0; j < 5; j++)
                    {
                        if (j + i >= count)
                        {
                            inf[j] = false;
                        }
                        else
                        {
                            inf[j] = Array1[j + i];
                        }
                    }
                    BitArray rd0 = new BitArray(5);
                    BitArray rd1 = new BitArray(5);
                    BitArray rd2 = new BitArray(5);
                    BitArray rd3 = new BitArray(5);
                    BitArray rd4 = new BitArray(5);
                    for (int l = 0; l < 5; l++)
                    {
                        rd0[l] = inf[l]; //11101
                    }
                    for (int l = 0; l < 4; l++)
                    {
                        rd1[l] = inf[l + 1]; //11010
                    }
                    for (int l = 0; l < 3; l++)
                    {
                        rd2[l] = inf[l + 2]; //10100
                    }
                    for (int l = 0; l < 2; l++)
                    {
                        rd3[l] = inf[l + 3]; //01000
                    }
                    for (int l = 0; l < 1; l++)
                    {
                        rd4[l] = inf[l + 4]; //10000
                    }
                    for (int l = 0; l < 5; l++)
                    {
                        if (l + (5 * (i / 5)) == count)
                        {
                            flag = 1;
                            break;
                        }
                        Coded[l + (5 * (i / 5))] = rd0[l] ^ rd1[l] ^ rd2[l] ^ rd3[l] ^ rd4[l];
                    }
                }
                return Coded;
            }
            else
            {
                return Array1;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
          
            if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Необхідно вибрати вхідний файл!");
                return;
               }
            if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Необхідно вибрати вихідний файл!");
                return;
            }

            if (Coding == true)
            {
                BitArray Coded = MyCoding(Array); // кодуємо bitArray
                System.IO.File.WriteAllBytes(Gre, BitArrayToBytes(Coded));
                MessageBox.Show("Файл збережено");
            }
            if(Decoding == true)
            {
                mDecoded1 = MyDeCoding(Array1);
                System.IO.File.WriteAllBytes(Gre1, BitArrayToBytes(mDecoded1));
                MessageBox.Show("Файл збережено");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Coding == true)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                string filename = openFileDialog1.FileName;
                // читаем файл в строку
                //var fileText = System.IO.File.ReadAllBytes(filename);
                Array = ConvertFileToBitArray(filename);
                counter = Array.Count;
                textBox1.Text = filename;
            }
            if (Decoding== true)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                string filename = openFileDialog1.FileName;
                // читаем файл в строку
                Array1 = ConvertFileToBitArray(filename);
                textBox1.Text = filename;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (Coding == true)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                string filename = saveFileDialog1.FileName;
                // сохраняем текст в файл
                Gre = filename;
                textBox2.Text = filename;
            }
            if (Decoding == true)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                string filename = saveFileDialog1.FileName;
                // сохраняем текст в файл
                Gre1 = filename;
                textBox2.Text = filename;
            }     
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Coding = true;
            Decoding = false;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Coding = false;
            Decoding = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Left = true;
            Right = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Right = true;
            Left = false;
        }
    }
}
