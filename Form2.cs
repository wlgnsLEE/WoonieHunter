using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WoonieHunter
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            lbRank.ForeColor = Color.White;
            lbRankList.ForeColor = Color.White;

            string filePath = "scores.txt";
            string[] lines = File.ReadAllLines(filePath);

            int i = 0;
            foreach (string line in lines)
            {
                lbRankList.Text += line + "\n"; // label에 한 줄씩 추가
                if (i == 9)
                    break;
                i++;
            }
        }
    }
}
