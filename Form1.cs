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
    public partial class Form1 : Form
    {
        Entity player;
        private List<PictureBox> bullets;
        private List<PictureBox> skills;
        private int bulletSpeed = 15;
        private bool isLeftPressed = false;
        private bool isRightPressed = false;
        private bool isUpPressed = false;
        private bool isDownPressed = false;
        private bool canshot = true;
        private int bullettimmer = 0;
        private int skilltimer = 0;
        private int skillcount = 3;
        private bool canuseskill = true;

        public Form1()
        {
            InitializeComponent();

            this.MinimumSize = new Size(800, 1000);//화면 크기
            this.MaximumSize = new Size(800, 1000);

            player = new Entity();
            bullets = new List<PictureBox>();
            skills= new List<PictureBox>();

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            tmr.Start();
            tmr_bullet.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            int X = player.GetEntityX();
            int Y = player.GetEntityY();
            int speed = player.GetSpeed();

            if (isLeftPressed&&character.Left>=50)
            {
                player.SetEntityX(X - speed);
            }
            if (isRightPressed&&character.Right<=750)
            {
                player.SetEntityX(X + speed);
            }
            if (isUpPressed&&character.Top>=250)
            {
                player.SetEntityY(Y - speed);
            }
            if (isDownPressed&&character.Bottom<=950)
            {
                player.SetEntityY(Y + speed);
            }

            character.Location = new System.Drawing.Point(X, Y);
            if (bullettimmer == 15)//총알 발사 내부쿨
            {
                canshot = true;
                bullettimmer = 0;
            }
            if (skilltimer == 300)//스킬 내부쿨
            {
                canuseskill = true;
                skilltimer = 0;
            }
            bullettimmer++;
            skilltimer++;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
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
                if (canshot ==true)
                {
                    int X = player.GetEntityX();
                    int Y = player.GetEntityY();

                    PictureBox new_bullet = new PictureBox();
                    new_bullet.Image = Properties.Resources.bullet;
                    new_bullet.Location = new System.Drawing.Point(X+21, Y);
                    new_bullet.Visible = true;

                    bullets.Add(new_bullet);
                    this.Controls.Add(new_bullet);
                    canshot=false;

                }
            }else if(e.KeyCode == Keys.S)
            {
                if (skillcount > 1 && canuseskill == true)
                {
                    //스킬 사용
                    int X=player.GetEntityX();
                    int Y=player.GetEntityY();

                    PictureBox new_skill = new PictureBox();
                    new_skill.Image = Properties.Resources.skill;
                    new_skill.Location = new System.Drawing.Point(X-80,Y-90);
                    new_skill.Visible = true;
                    new_skill.SizeMode=PictureBoxSizeMode.Zoom;
                    new_skill.Width = 200;
                    new_skill.Height = 200;
                    

                    skills.Add(new_skill);
                    this.Controls.Add(new_skill);
                    skillcount--;
                    canuseskill = false;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
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

        private void tmr_bullet_Tick(object sender, EventArgs e)
        {
            for(int i=0;i<bullets.Count;i++)
            {
                bullets[i].Top -= bulletSpeed;

                if (bullets[i].Top < -bullets[i].Height)//총알이 맵 밖으로 나갔을 때
                {
                    this.Controls.Remove(bullets[i]);

                    bullets.RemoveAt(i);

                    i--;
                }
                /* 
                for(int j = 0; j < enemies.Count; j++)
                {
                    if (IsAttacked(bullet[i], enemies[j])==true)//적과 충돌했을 때
                    {
                        this.Controls.Remove(bullets[i]);//총알 삭제
                        bullets.RemoveAt(i);
                        i--;

                        this.Controls.Remove(enemies[j]);//적 삭제
                        enemies.RemoveAt(j);
                        j--;
                    }
                }
                */
            }
            for(int i=0;i<skills.Count;i++)
            {
                skills[i].Top -= bulletSpeed/5;

            }
        }

        public bool IsAttacked(PictureBox bullet, PictureBox entity)//총알과 객체 충돌 검사
        {
            if (bullet.Top + 3 <= entity.Bottom&&entity.Bottom-3>=bullet.Top)
            {
                if (bullet.Left + 8 <= entity.Right && bullet.Right - 8 >= entity.Left)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
