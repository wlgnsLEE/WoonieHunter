using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Media;
using System.Timers;
using System.IO;

namespace WoonieHunter
{
    public partial class Form1 : Form
    {
        Entity player;
        private List<Entity> enemies;
        private List<PictureBox> bullets;
        private List<PictureBox> skills;
        private List<Entity> meteors;
        private List<boss> Boss;
        private int enemyspawn = 0;
        private int bosstimer = 0;
        private int bulletSpeed = 15;
        private bool isLeftPressed = false;
        private bool isRightPressed = false;
        private bool isUpPressed = false;
        private bool isDownPressed = false;
        private bool isP_Pressed = false;
        private bool canshot = true;
        private int bullettimmer = 0;
        private int skilltimer = 0;
        private int skillcount = 3;
        private bool canuseskill = true;
        private int score = 0;
        Label lbScoreText = new Label();
        Label lbScore = new Label();
        


        private System.Timers.Timer tmr_bgm;

        private SoundPlayer soundPlayer;
        private System.Timers.Timer timer;

        public Form1()
        {
            InitializeComponent();

            this.MinimumSize = new Size(800, 1000);//화면 크기
            this.MaximumSize = new Size(800, 1000);

            player = new Entity();
            player.PB_Entity.Image = Properties.Resources.character;
            player.PB_Entity.Visible = true;
            player.PB_Entity.Size = new Size(50, 80);
            player.PB_Entity.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(player.PB_Entity);

            player.PB_Entity.BringToFront();

            bullets = new List<PictureBox>();
            skills = new List<PictureBox>();
            enemies = new List<Entity>();
            meteors = new List<Entity>();
            Boss = new List<boss>();

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            tmr.Start();
            tmr_bullet.Start();
            tmr_spawn_enemy.Start();
            //tmr_spawn_meteor.Start();

            soundPlayer = new SoundPlayer();
            timer = new System.Timers.Timer();

            // 타이머 설정
            timer.Interval = 123000;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;


            // 인게임 배경화면
            InitBackGround1();
            InitBackGround2();

            // 스코어
            
            lbScoreText.Font = new Font("Arial", 30, FontStyle.Bold);
            lbScoreText.Width = 150;
            lbScoreText.Height = 50;
            lbScoreText.Text = score.ToString();
            lbScoreText.Location = new System.Drawing.Point(175, 0);
            lbScoreText.ForeColor = Color.White;
            lbScoreText.Parent = this;

            lbScore.Font = new Font("Arial", 30, FontStyle.Bold);
            lbScore.Width = 200;
            lbScore.Height = 50;
            lbScore.Text = "SCORE";
            
            lbScore.ForeColor = Color.White;
            lbScore.Parent = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            soundPlayer.Stream = Properties.Resources._10__Track_10;
            soundPlayer.Play();

            timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            soundPlayer.Stream = Properties.Resources._10__Track_10;
            soundPlayer.Play();
        }
        private void tmr_Tick(object sender, EventArgs e)
        {
            background2.SendToBack();
            background2_.SendToBack();
            background1.SendToBack();
            background1_.SendToBack();

            int X = player.GetEntityX();
            int Y = player.GetEntityY();
            int speed = player.GetSpeed();

            if (isLeftPressed && player.PB_Entity.Left >= 50)
            {
                player.SetEntityX(X - speed);
            }
            if (isRightPressed && player.PB_Entity.Right <= 750)
            {
                player.SetEntityX(X + speed);
            }
            if (isUpPressed && player.PB_Entity.Top >= 250)
            {
                player.SetEntityY(Y - speed);
            }
            if (isDownPressed && player.PB_Entity.Bottom <= 950)
            {
                player.SetEntityY(Y + speed);
            }

            player.PB_Entity.Location = new System.Drawing.Point(X, Y);

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
            if (bosstimer == 300)//보스 소환시간
            {
                enemyspawn++;
            }

            bosstimer++;
            bullettimmer++;
            skilltimer++;

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].PB_Entity.Top += enemies[i].GetSpeed();

                if (enemies[i].PB_Entity.Bottom > 1000)
                {
                    Controls.Remove(enemies[i].PB_Entity);

                    enemies.RemoveAt(i);

                    i--;
                }
            }

            // 배경 무한 이동
            MoveBackGround1(1);
            MoveBackGround2(2);

            // 점수
            lbScoreText.Text =  score.ToString();


            // 랭킹 입력용
            if (isP_Pressed)
            {
                isP_Pressed = false;
                string userName;
                Form3 nameForm = new Form3();
         
                if (nameForm.ShowDialog() == DialogResult.OK)
                {
                    userName = nameForm.UserName;
                    // userName 변수에 Form3에서 입력한 이름이 저장됨

                    string filePath1 = "scores.txt";

                    using (StreamWriter writer = new StreamWriter(filePath1, true))
                    {
                        writer.WriteLine(score + " " + userName);
                    }
                }

                string filePath = "scores.txt";
                
                if (File.Exists(filePath)) // 파일이 존재 할 때
                {
                    // scores.txt 파일을 읽어와서 List에 저장
                    List<string> lines = File.ReadAllLines("scores.txt").ToList();

                    // List를 점수에 따라 내림차순으로 정렬
                    lines.Sort((x, y) => Convert.ToInt32(y.Split(' ')[0]).CompareTo(Convert.ToInt32(x.Split(' ')[0])));

                    // 정렬된 List를 scores.txt 파일에 다시 쓰기
                    using (StreamWriter writer = new StreamWriter("scores.txt"))
                    {
                        foreach (string line in lines)
                        {
                            writer.WriteLine(line);
                        }
                    }

                }
            }

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
                if (canshot == true)
                {
                    int X = player.GetEntityX();
                    int Y = player.GetEntityY();

                    PictureBox new_bullet = new PictureBox();
                    new_bullet.Size = new Size(10, 20);
                    new_bullet.Image = Properties.Resources.bullet;
                    new_bullet.Location = new System.Drawing.Point(X + 21, Y);
                    new_bullet.Visible = true;

                    bullets.Add(new_bullet);
                    this.Controls.Add(new_bullet);
                    canshot = false;

                }
            }
            else if (e.KeyCode == Keys.S)
            {
                if (skillcount > 1 && canuseskill == true)
                {
                    //스킬 사용
                    int X = player.GetEntityX();
                    int Y = player.GetEntityY();

                    PictureBox new_skill = new PictureBox();
                    new_skill.Image = Properties.Resources.skill;
                    new_skill.Location = new System.Drawing.Point(X-25 , Y - 90);
                    new_skill.Visible = true;
                    new_skill.SizeMode = PictureBoxSizeMode.Zoom;
                    new_skill.Width = 100;
                    new_skill.Height = 200;


                    skills.Add(new_skill);
                    this.Controls.Add(new_skill);
                    skillcount--;
                    canuseskill = false;
                }
            }

            // 게임 강제 종료 점수 기록용
            if (e.KeyCode == Keys.P)
            {
                isP_Pressed = true;
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

            // 랭킹입력용 @@@@@@
            if (e.KeyCode == Keys.P) 
                isP_Pressed = false;
        }

        private void tmr_bullet_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Top -= bulletSpeed;

                if (bullets[i].Top < -bullets[i].Height)//총알이 맵 밖으로 나갔을 때
                {
                    this.Controls.Remove(bullets[i]);

                    bullets.RemoveAt(i);

                    i--;
                }

                for (int j = 0; j < enemies.Count; j++)
                {
                    if (i >= 0 && j >= 0)
                    {
                        if (IsAttacked(bullets[i], enemies[j].PB_Entity) == true)//적과 충돌했을 때
                        {
                            this.Controls.Remove(bullets[i]);//총알 삭제
                            bullets.RemoveAt(i);
                            i--;

                            this.Controls.Remove(enemies[j].PB_Entity);//적 삭제
                            enemies.RemoveAt(j);
                            j--;
                            score = score + 50;
                        }
                    }
                   
                }
                if (enemyspawn > 0 && Boss.Count > 0&& i >=0)
                {
                    if (IsAttacked(bullets[i], Boss[0].PB_Entity) == true)
                    {
                        this.Controls.Remove(bullets[i]);//총알 삭제
                        bullets.RemoveAt(i);
                        i--;

                        Boss[0].hp--;
                        if (Boss[0].hp < 0)//보스 hp가 0이되면 제거
                        {
                            this.Controls.Remove(Boss[0].PB_Entity);
                            Boss.RemoveAt(0);
                        }
                    }
                }

            }

            for (int i = 0; i < skills.Count; i++)
            {
                skills[i].Top -= bulletSpeed / 5;

                if (skills[i].Top < -skills[i].Height)//스킬이 맵 밖으로 나갔을 때
                {
                    this.Controls.Remove(skills[i]);

                    skills.RemoveAt(i);

                    i--;
                }

                for (int j = 0; j < enemies.Count; j++)
                {
                    if (i >= 0 && j >= 0)
                    {
                        if (IsAttacked(skills[i], enemies[j].PB_Entity) == true)//적과 충돌했을 때
                        {
                            this.Controls.Remove(enemies[j].PB_Entity);//적 삭제
                            enemies.RemoveAt(j);
                            j--;
                        }
                    }
                }

                if (enemyspawn > 0 && Boss.Count > 0)
                {
                    if (IsAttacked(skills[i], Boss[0].PB_Entity) == true)
                    {
                        this.Controls.Remove(skills[i]);//총알 삭제
                        skills.RemoveAt(i);
                        i--;

                        Boss[0].hp -= 5;
                        if (Boss[0].hp < 0)//보스 hp가 0이되면 제거
                        {
                            this.Controls.Remove(Boss[0].PB_Entity);
                            Boss.RemoveAt(0);
                        }
                    }
                }
            }
        }

        public bool IsAttacked(PictureBox bullet, PictureBox entity)//총알과 객체 충돌 검사
        {
            if (bullet.Top + 3 <= entity.Bottom && entity.Bottom - 3 >= bullet.Top)
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

        private void create_enemy()
        {
            Random generateRandom;
            DateTime curTime = DateTime.Now;
            generateRandom = new Random(curTime.Millisecond);

            int rand_pos_x = generateRandom.Next(10, 790);
            int rand_pos_y = generateRandom.Next(0, 50);

            Entity new_enemy = new Entity();
            new_enemy.SetSpeed(5);
            new_enemy.PB_Entity.Size = new Size(35, 35);
            new_enemy.PB_Entity.Image = Properties.Resources.asteroid;
            new_enemy.PB_Entity.Visible = true;
            new_enemy.PB_Entity.Location = new System.Drawing.Point(rand_pos_x, rand_pos_y);

            enemies.Add(new_enemy);
            Controls.Add(new_enemy.PB_Entity);
        }

        private void create_boss()
        {
            boss new_boss = new boss();
            new_boss.PB_Entity.Size = new Size(150, 200);
            new_boss.PB_Entity.SizeMode = PictureBoxSizeMode.Zoom;
            new_boss.PB_Entity.Image = Properties.Resources.boss;
            new_boss.PB_Entity.Visible = true;
            new_boss.PB_Entity.Location = new System.Drawing.Point(300, 10);
            new_boss.SetSpeed(0);
            new_boss.PB_Entity.BringToFront();

            Boss.Add(new_boss);
            Controls.Add(new_boss.PB_Entity);

        }


        private void tmr_spawn_enemy_Tick(object sender, EventArgs e)
        {
            if (enemyspawn == 0)
            {
                create_enemy();
            }
            else if (enemyspawn == 1)
            {
                create_boss();
                enemyspawn++;
            }
        }

        private void tmr_spawn_meteor_Tick(object sender, EventArgs e)
        {
            create_meteor();
        }

        public void InitBackGround1()
        {
            BackColor = Color.FromArgb(0, 0, 0);
            background1.Image = Properties.Resources.bg_back;
            background1.SizeMode = PictureBoxSizeMode.StretchImage;
            background1.Location = new Point(0, 0);
            background1.Width = Width;
            background1.Height = Height;
            background1.SendToBack();

            background1_.Image = Properties.Resources.bg_back;
            background1_.SizeMode = PictureBoxSizeMode.StretchImage;
            background1_.Location = new Point(0, 1000);
            background1_.Width = Width;
            background1_.Height = Height;
            background1_.SendToBack();
        }

        public void InitBackGround2()
        {

            background2.Image = Properties.Resources.bg_planet;
            background2.Location = new Point(728, 0);
            background2.Width = 72;
            background2.Height = 272;

            background2_.Image = Properties.Resources.bg_planet2;
            background2_.Location = new Point(0, 1000);
            background2_.Width = 72;
            background2_.Height = 272;

        }

        public void create_meteor()
        {
            Random generateRandom;
            DateTime curTime = DateTime.Now;
            generateRandom = new Random(curTime.Millisecond);

            int rand_pos_x = generateRandom.Next(10, 790);
            int rand_pos_y = generateRandom.Next(0, 20);

            Entity meteor = new Entity();
            meteor.SetSpeed(2);
            meteor.PB_Entity.Size = new Size(35, 35);
            meteor.PB_Entity.Image = Properties.Resources.asteroid_small;
            meteor.PB_Entity.Visible = true;
            meteor.PB_Entity.Location = new System.Drawing.Point(rand_pos_x, rand_pos_y);

            meteors.Add(meteor);
            Controls.Add(meteor.PB_Entity);
        }

        private void MoveBackGround1(int speed)
        {
            if (background1.Top >= 1000)
            {
                background1.Top = -1000;
            }
            else
            {
                background1.Top = background1.Top + speed;
            }

            if (background1_.Top >= 1000)
            {
                background1_.Top = -1000;
            }
            else
            {
                background1_.Top = background1_.Top + speed;
            }

        }
        private void MoveBackGround2(int speed)
        {
            if (background2.Top >= 1000)
            {
                background2.Top = -1000;
            }
            else
            {
                background2.Top = background2.Top + speed;
            }

            if (background2_.Top >= 1000)
            {
                background2_.Top = -1000;
            }
            else
            {
                background2_.Top = background2_.Top + speed;
            }

        }
    }
}
