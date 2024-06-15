using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WoonieHunter
{
    public partial class Menu : Form
    {
        private SoundPlayer soundPlayer;
        private System.Timers.Timer timer;
        int gamevolume=5;
        int chspeed=5;
        int enemyspeed = 5;
        int skillnum = 1;
        public Menu()
        {
            InitializeComponent();

            this.MinimumSize = new Size(700, 500);
            this.MaximumSize = new Size(700, 500);

            soundPlayer = new SoundPlayer();
            timer = new System.Timers.Timer();

            Setting.Volume.SetSoundVolume(5);

            // 타이머 설정
            timer.Interval = 114000;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            soundPlayer.Stream = Properties.Resources._1__Track_1;
            soundPlayer.Play();

            timer.Start();

        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            soundPlayer.Stream = Properties.Resources._1__Track_1;
            soundPlayer.Play();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            soundPlayer.Stop();
            Form1 game = new Form1(chspeed,enemyspeed,skillnum);
            game.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 rank = new Form2();
            rank.Show();
        }

        private void Dialog_Changed(object sender, EventArgs e)
        {
            
            
        }
        private void Setting_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(gamevolume,chspeed,enemyspeed,skillnum);
            setting.ValuesUpdated += Setting_ValuesUpdated;
            setting.Show();
            
        }

        private void Setting_ValuesUpdated(int newgamevolume, int newchspeed, int newenemyspeed, int newskillnum)
        {
            gamevolume = newgamevolume;
            chspeed = newchspeed;
            enemyspeed = newenemyspeed;
            skillnum=newskillnum;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 tutorial = new Form4();
            tutorial.Show();
        }
    }
}
