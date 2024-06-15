using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoonieHunter
{
    public partial class Form4 : Form
    {
        Label text1 = new Label();
        Label text2 = new Label();
        private List<PictureBox> bullets;
        private List<PictureBox> skills;
        Entity player;
        private bool isLeftPressed = false;
        private bool isRightPressed = false;
        private bool isUpPressed = false;
        private bool isDownPressed = false;
        public Form4()
        {
            InitializeComponent();

            text1.Font = new Font("AniMe Matrix - MB_EN", 20, FontStyle.Bold);
            text1.Width = 400;
            text1.Height = 50;
            text1.Text = "이동 : → ← ↑ ↓";
            text1.Location = new System.Drawing.Point(30, 50);
            text1.BackColor = Color.Transparent;
            text1.ForeColor = Color.Black;
            text1.Parent = this;
            text1.SendToBack();

            text2.Font = new Font("AniMe Matrix - MB_EN", 20, FontStyle.Bold);
            text2.Width = 400;
            text2.Height = 50;
            text2.Text = "공격 : SPACE BAR  스킬 : S ";
            text2.Location = new System.Drawing.Point(30, 100);
            text2.BackColor = Color.Transparent;
            text2.ForeColor = Color.Black;
            text2.Parent = this;
            text2.SendToBack();

            player = new Entity();
            player.PB_Entity.Image = Properties.Resources.character;
            player.PB_Entity.Visible = true;
            player.PB_Entity.Size = new Size(50, 80);
            player.PB_Entity.SizeMode = PictureBoxSizeMode.Zoom;
            player.PB_Entity.BackColor = Color.Transparent;
            player.PB_Entity.Location = new System.Drawing.Point(20, 100);
            player.SetEntityX(300);
            player.SetEntityY(200);
            Controls.Add(player.PB_Entity);

            player.PB_Entity.BringToFront();

            bullets = new List<PictureBox>();
            skills = new List<PictureBox>();

            this.KeyDown += new KeyEventHandler(Form4_KeyDown);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int X = player.GetEntityX();
            int Y = player.GetEntityY();
            int speed = 5;
            if (isLeftPressed && player.PB_Entity.Left >= 0)
            {
                player.SetEntityX(X - speed);
            }
            if (isRightPressed && player.PB_Entity.Right <= 800)
            {
                player.SetEntityX(X + speed);
            }
            if (isUpPressed && player.PB_Entity.Top >= 150)
            {
                player.SetEntityY(Y - speed);
            }
            if (isDownPressed && player.PB_Entity.Bottom <= 400)
            {
                player.SetEntityY(Y + speed);
            }

            player.PB_Entity.Location = new System.Drawing.Point(X, Y);

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Top -= 5;

                if (bullets[i].Top < -bullets[i].Height)//총알이 맵 밖으로 나갔을 때
                {
                    this.Controls.Remove(bullets[i]);

                    bullets.RemoveAt(i);

                    i--;
                }
            }

            for (int i = 0; i < skills.Count; i++)
            {
                skills[i].Top -= 1;

                if (skills[i].Top < -skills[i].Height)//스킬이 맵 밖으로 나갔을 때
                {
                    this.Controls.Remove(skills[i]);

                    skills.RemoveAt(i);

                    i--;
                }
            }
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                isLeftPressed = true;
            else if (e.KeyCode == Keys.Right)
                isRightPressed = true;
            else if (e.KeyCode == Keys.Up)
                isUpPressed = true;
            else if (e.KeyCode == Keys.Down)
                isDownPressed = true;
            else if (e.KeyCode == Keys.Space)//공격키
            {

                int X = player.GetEntityX();
                int Y = player.GetEntityY();

                PictureBox new_bullet = new PictureBox();
                new_bullet.Size = new Size(10, 20);
                new_bullet.Image = Properties.Resources.bullet;
                new_bullet.Location = new System.Drawing.Point(X + 21, Y);
                new_bullet.Visible = true;
                new_bullet.BackColor = Color.Transparent;
                new_bullet.BringToFront();

                bullets.Add(new_bullet);
                this.Controls.Add(new_bullet);


            }

            else if (e.KeyCode == Keys.S)
            {
                //스킬 사용
                int X = player.GetEntityX();
                int Y = player.GetEntityY();

                PictureBox new_skill = new PictureBox();
                new_skill.Image = Properties.Resources.skill;
                new_skill.Location = new System.Drawing.Point(X - 25, Y - 90);
                new_skill.Visible = true;
                new_skill.SizeMode = PictureBoxSizeMode.Zoom;
                new_skill.Width = 100;
                new_skill.Height = 200;
                new_skill.BackColor = Color.Transparent;
                new_skill.BringToFront();

                skills.Add(new_skill);
                this.Controls.Add(new_skill);


            }
        }

        private void Form4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                isLeftPressed = false;
            else if (e.KeyCode == Keys.Right)
                isRightPressed = false;
            else if (e.KeyCode == Keys.Up)
                isUpPressed = false;
            else if (e.KeyCode == Keys.Down)
                isDownPressed = false;
        }
    }
}
