using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab_10_game
{
    public partial class Form1 : Form
    {
        int n1, n2, timeleft = 60, count = 0;
        Random rand = new Random();
        List<gamer> gamers = new List<gamer>();

        public Form1()
        {
            InitializeComponent();
        }

        private void стартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage= Image.FromFile(@"..\..\Resources\bg.png");
            label1.Visible = true;
            textBox1.Visible = true;
            progressBar2.Value = 100;
            ChangeEquation();
            labelTimeLeft.Visible = true;
            textBox1.Focus();
            timer1.Start();
            progressBar2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            count = 0;
            timeleft = 60;
            label5.Text = count.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(timeleft==0)
            {
                timer1.Stop();
                MessageBox.Show("Правильных ответов: "+count.ToString(), "Результаты: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            labelTimeLeft.Text = "Оставшееся время: " + timeleft--.ToString() + " секунд";
            progressBar2.PerformStep();
        }

        private void стопToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.BackgroundImage = null;
            label1.Visible = false;
            textBox1.Visible = false;
            progressBar2.Value = 0;
            labelTimeLeft.Visible = false;
            timeleft = 60;
            progressBar2.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label3.Visible = false;
            count = 0;
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if(colorDialog.ShowDialog() == DialogResult.OK)
                label1.ForeColor = colorDialog.Color;
        }

        private void размерИТипШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if(fontDialog.ShowDialog() == DialogResult.OK)
                label1.Font = fontDialog.Font;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = Interaction.InputBox("Введите Ваше имя: ");

            DateTime now = DateTime.Now;
            string date = now.ToString("D").ToString();

            //if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            //    return;

            string FileName = @"..\..\statistics.txt";

            FileStream fs = new FileStream(FileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(date + ","+ name + "," + count.ToString());
                sw.Close();
                fs.Close();
                MessageBox.Show("Информация успешно добавлена!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void построитьДиаграммуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gamers.Clear();
            string NameOfFile = @"..\..\statistics.txt";

            try
            {
                using (StreamReader sr = new StreamReader(NameOfFile))
                {
                    string line;
                    string[] ar = new string[255];

                    while ((line = sr.ReadLine()) != null)
                    {
                        ar = line.Split(',');
                        gamers.Add(new gamer()
                        {
                            date = ar[0],
                            name = ar[1],
                            count = ar[2]
                        }); 

                    }
                    sr.Close();
                }
            }
            catch
            {
                MessageBox.Show("Cannot open a file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            diagram diag = new diagram(gamers);
            diag.ShowDialog();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
                if (!VerifyAnswer()) 
                {
                    label2.Text = "Неверно!";
                    label2.Visible = true;
                }
                else
                {
                    label2.Visible = false;
                    ChangeEquation();
                    textBox1.Text = null;
                    label5.Text=(++count).ToString();
                }

        }

        public bool VerifyAnswer()
        {
            try
            {
                return n1 * n2 == Convert.ToInt32(textBox1.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void ChangeEquation()
        {
            n1 = rand.Next(0, 9);
            n2 = rand.Next(0, 9);
            label1.Text = n1.ToString() + " * " + n2.ToString() + " = ";
        }
    }
}
