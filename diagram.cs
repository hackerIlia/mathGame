using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_10_game
{
    public partial class diagram : Form
    {
        public diagram(List<gamer> gm)
        {
            InitializeComponent();
            chart1.Titles.Add("Статистика игры: ");
            foreach (gamer g in gm)
                chart1.Series["Правильные ответы"].Points.AddXY(g.name, g.count);
        }
    }
}
