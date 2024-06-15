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
using System.Threading;

namespace WoonieHunter
{
    public partial class Form1 : Form
    {
        private int backgroundY;
        private int planetY;
        private Image backgroundImage;
        private Image backgroundPlanet;
        //--------
        Entity player;
        private Image[] enemy_images = { Properties.Resources.enemy1, Properties.Resources.enemy2, Properties.Resources.enemy3 };
        private List<Entity> enemies;
        private List<Entity> items;
        private List<PictureBox> bullets;
        private List<PictureBox> skills;
        private List<Entity> meteors;
        private List<boss> Boss;
        private List<Entity> bossbullet;
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
        private int skillcount = 0;
        private bool canuseskill = true;
        private bool bossmove = true;
        private int score = 0;
        private int enemySpeed = 5;
        private int playerSpeed = 5;
        private bool gameOver = false; // Game over 상태

        Label lbScoreText = new Label();
        Label lbScore = new Label();
        private List<PictureBox> HPUI;
        private int[] bpattern1 = { 10, 130, 250, 370, 490, 610, 730 };
        private int[] bpattern2 = { 750, 630, 510, 390, 270, 150, 30 };
        PictureBox boss_lb = new PictureBox();

        System.Windows.Forms.Timer bossSpawnTimer = new System.Windows.Forms.Timer();
        //체력
        PictureBox bossHPBarBG = new PictureBox();
        PictureBox bossHPBar = new PictureBox();
        int newHPBarWidth = 0;


        private System.Timers.Timer tmr_bgm;

        private SoundPlayer soundPlayer;
        private System.Timers.Timer timer;

        public Form1(int chspeed,int enemyspeed, int skillnum)
        {
            InitializeComponent();

            this.skillcount=skillnum;
            this.playerSpeed = chspeed;
            this.enemySpeed = enemyspeed;

            this.DoubleBuffered = true; // 화면 깜빡임 방지
            this.backgroundY = 0;

            // 배경 이미지 로드
            this.backgroundImage = Properties.Resources.bg_back;
            this.backgroundPlanet = Properties.Resources.bg_planet;

            // 폼 설정
            this.ClientSize = new Size(800, 600);
            this.Paint += new PaintEventHandler(this.OnPaint);

            //---------
            this.MinimumSize = new Size(800, 1000);//화면 크기
            this.MaximumSize = new Size(800, 1000);

            player = new Entity();
            player.PB_Entity.Image = Properties.Resources.character;
            player.PB_Entity.Visible = true;
            player.PB_Entity.Size = new Size(50, 80);
            player.PB_Entity.SizeMode = PictureBoxSizeMode.Zoom;
            player.PB_Entity.BackColor = Color.Transparent;
            Controls.Add(player.PB_Entity);

            player.PB_Entity.BringToFront();

            bullets = new List<PictureBox>();
            skills = new List<PictureBox>();
            enemies = new List<Entity>();
            meteors = new List<Entity>();
            Boss = new List<boss>();
            items = new List<Entity>();
            bossbullet = new List<Entity>();
            HPUI = new List<PictureBox>();

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            tmr.Start();
            tmr_bullet.Start();
            tmr_spawn_enemy.Start();
            tmr_item_spawn.Start();
            //tmr_spawn_meteor.Start();

            soundPlayer = new SoundPlayer();
            timer = new System.Timers.Timer();

            // 타이머 설정
            timer.Interval = 123000;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;


            // 인게임 배경화면
            //InitBackGround1();
            //InitBackGround2();

            // 스코어
            
            lbScoreText.Font = new Font("Arial", 30, FontStyle.Bold);
            lbScoreText.Width = 150;
            lbScoreText.Height = 50;
            lbScoreText.Text = score.ToString();
            lbScoreText.Location = new System.Drawing.Point(175, 0);
            lbScoreText.BackColor = Color.Transparent;
            lbScoreText.ForeColor = Color.White;
            lbScoreText.Parent = this;

            lbScore.Font = new Font("Arial", 30, FontStyle.Bold);
            lbScore.Width = 200;
            lbScore.Height = 50;
            lbScore.Text = "SCORE";
            
            lbScore.BackColor = Color.Transparent;
            lbScore.ForeColor = Color.White;
            lbScore.Parent = this;

            //캐릭터 HPUI생성
            for(int i = 0; i < 3; i++)
            {
                PictureBox Heart = new PictureBox();
                Heart.Image = Properties.Resources.heart;
                Heart.Size = new System.Drawing.Size(40, 40);
                Heart.BackColor = Color.Transparent;
                Heart.SizeMode = PictureBoxSizeMode.Zoom;
                Heart.Location = new System.Drawing.Point(330 + i * 40, 20);
                Heart.Visible = true;

                HPUI.Add( Heart );
                Controls.Add( Heart );
            }
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

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = this.backgroundY - this.backgroundImage.Height;

            // 배경 이미지 그리기
            while (y < this.ClientSize.Height)
            {
                g.DrawImage(this.backgroundImage, 0, y);
                y += this.backgroundImage.Height;
            }
        }
        //--------
        private void tmr_Tick(object sender, EventArgs e)
        {
            // 배경 이미지의 Y 좌표 업데이트
            this.backgroundY += 1; // 배경이 내려가는 속도 조절
            if (this.backgroundY >= this.backgroundImage.Height)
            {
                this.backgroundY = 0;
            }

            // 화면 다시 그리기
            this.Invalidate();
            //--------
            /*background2.SendToBack();
            background2_.SendToBack();
            background1.SendToBack();
            background1_.SendToBack();*/

            player.SetSpeed(playerSpeed);
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
                else if (IsCollided(player.PB_Entity, enemies[i].PB_Entity)) // 충돌 검사
                {
                    player.life--;

                    HandlePlayerCollision();

                    Controls.Remove(enemies[i].PB_Entity);
                    enemies.RemoveAt(i);
                    i--;
                }

            }

            for (int i = 0; i < bossbullet.Count; i++)
            {
                bossbullet[i].PB_Entity.Top += bossbullet[i].GetSpeed();

                if (bossbullet[i].PB_Entity.Bottom > 1000)
                {
                    Controls.Remove(bossbullet[i].PB_Entity);

                    bossbullet.RemoveAt(i);

                    i--;
                }
                else if (IsCollided(player.PB_Entity, bossbullet[i].PB_Entity))
                {
                    player.life--;

                    HandlePlayerCollision();
                    Controls.Remove(bossbullet[i].PB_Entity);
                    bossbullet.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                items[i].PB_Entity.Top += items[i].GetSpeed();

                if (items[i].PB_Entity.Bottom > 1000)
                {
                    Controls.Remove(items[i].PB_Entity);

                    items.RemoveAt(i);

                    i--;

                    continue;
                }

                if (IsAttacked(player.PB_Entity, items[i].PB_Entity))
                {
                    if (items[i].life == 0)
                    {
                        skillcount++;
                    }else if (items[i].life == 1 && player.life < 3)
                    {
                        player.life++;
                    }
                    Controls.Remove(items[i].PB_Entity);

                    items.RemoveAt(i);

                    i--;
                }
            }

            // 배경 무한 이동
            //MoveBackGround1(1);
            //MoveBackGround2(2);

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

            if (Boss.Count > 0)
            {
                if (Boss[0].GetEntityX() > 650)
                {
                    bossmove = true;
                }
                else if (Boss[0].GetEntityX() < 50)
                {
                    bossmove = false;
                }

                if (bossmove == true)
                {
                    Boss[0].SetEntityX(Boss[0].GetEntityX() - 2);
                }
                else
                {
                    Boss[0].SetEntityX(Boss[0].GetEntityX() + 2);
                }

                Boss[0].PB_Entity.Location = new System.Drawing.Point(Boss[0].GetEntityX(), Boss[0].GetEntityY());
            }

        }
        private void HandlePlayerCollision()
        {
            if (player.life == 3)
            {
                HPUI[0].Visible = true;
                HPUI[1].Visible = true;
                HPUI[2].Visible = true;
            }
            else if (player.life == 2)
            {
                HPUI[0].Visible = true;
                HPUI[1].Visible = true;
                HPUI[2].Visible = false;
            }
            else if (player.life == 1)
            {
                HPUI[0].Visible = true;
                HPUI[1].Visible = false;
                HPUI[2].Visible = false;
            }
            else if (player.life == 0)
            {
                HPUI[0].Visible = false;
                HPUI[1].Visible = false;
                HPUI[2].Visible = false;

                tmr.Stop();
                tmr_bullet.Stop();
                tmr_spawn_enemy.Stop();
                timer.Stop();
                // Optionally, stop the game or show a game over message
                MessageBox.Show("게임 오버!");

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
                this.Close();
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
                    new_bullet.BackColor = Color.Transparent;

                    bullets.Add(new_bullet);
                    this.Controls.Add(new_bullet);
                    canshot = false;

                }
            }
            else if (e.KeyCode == Keys.S)
            {
                if (skillcount > 0 && canuseskill == true)
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
                    new_skill.BackColor = Color.Transparent;


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
                if (enemyspawn > 0 && Boss.Count > 0 && i >=0)
                {
                    if (IsAttacked(bullets[i], Boss[0].PB_Entity) == true)
                    {
                        this.Controls.Remove(bullets[i]);//총알 삭제
                        bullets.RemoveAt(i);
                        i--;

                        Boss[0].hp--;

                        newHPBarWidth = (int)(200 * (Boss[0].hp / 10.0));
                        bossHPBar.Size = new Size(newHPBarWidth, 20);

                        if (Boss[0].hp < 0)//보스 hp가 0이되면 제거
                        {
                            score = score + 500;
                            this.Controls.Remove(Boss[0].PB_Entity);
                            Boss.RemoveAt(0);

                            tmr.Stop();
                            tmr_bullet.Stop();
                            tmr_spawn_enemy.Stop();
                            timer.Stop();

                            MessageBox.Show("게임 클리어!");

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
                            this.Close();
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

                    continue;
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

                for (int j = 0; j < bossbullet.Count; j++)
                {
                    if (i >= 0 && j >= 0)
                    {
                        if (IsAttacked(skills[i], bossbullet[j].PB_Entity) == true)//적과 충돌했을 때
                        {
                            this.Controls.Remove(bossbullet[j].PB_Entity);//적 삭제
                            bossbullet.RemoveAt(j);
                            j--;
                        }
                    }
                }

                if (enemyspawn > 0 && Boss.Count > 0)
                {
                    if (IsAttacked(skills[i], Boss[0].PB_Entity))
                    {
                        this.Controls.Remove(skills[i]);//총알 삭제
                        skills.RemoveAt(i);
                        i--;

                        Boss[0].hp -= 5;

                        newHPBarWidth = (int)(200 * (Boss[0].hp / 10.0));
                        bossHPBar.Size = new Size(newHPBarWidth, 20);

                        if (Boss[0].hp < 0)//보스 hp가 0이되면 제거
                        {
                            score = score + 500;
                            this.Controls.Remove(Boss[0].PB_Entity);
                            Boss.RemoveAt(0);

                            tmr.Stop();
                            tmr_bullet.Stop();
                            tmr_spawn_enemy.Stop();
                            timer.Stop();

                            MessageBox.Show("게임 클리어!");

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

                            this.Close();
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
            }

            return false;
        }
        public bool IsCollided(PictureBox player, PictureBox enemy)//플레이어와 적과 충돌 검사
        {
            if (player.Top + 3 <= enemy.Bottom && enemy.Bottom - 3 >= player.Top)
            {
                if (player.Left  <= enemy.Right && player.Right  >= enemy.Left)
                {
                    if (player.Top < enemy.Top)
                    {
                        return false;
                    }
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
            int rand_enemy = generateRandom.Next(0, 3);

            Entity new_enemy = new Entity();
            new_enemy.SetSpeed(enemySpeed);
            new_enemy.PB_Entity.Size = new Size(40, 40);
            new_enemy.PB_Entity.Image = enemy_images[rand_enemy];
            new_enemy.PB_Entity.SizeMode = PictureBoxSizeMode.Zoom;
            new_enemy.PB_Entity.Visible = true;
            new_enemy.PB_Entity.Location = new System.Drawing.Point(rand_pos_x, rand_pos_y);
            new_enemy.PB_Entity.BackColor = Color.Transparent;

            enemies.Add(new_enemy);
            Controls.Add(new_enemy.PB_Entity);
        }

        private void bosspatternbase()
        {
            Random generateRandom;

            for (int i = 0; i < 5; i++)
            {
                DateTime curTime = DateTime.Now;
                generateRandom = new Random(curTime.Millisecond);

                int rand_pos_x = generateRandom.Next(10, 790);
                int rand_pos_y = generateRandom.Next(0, 50);

                Entity new_enemy = new Entity();
                new_enemy.SetSpeed(10);
                new_enemy.PB_Entity.SizeMode = PictureBoxSizeMode.Zoom;
                new_enemy.PB_Entity.Size = new Size(30, 30);
                new_enemy.PB_Entity.Image = Properties.Resources.bossbullet;
                new_enemy.PB_Entity.Visible = true;
                new_enemy.PB_Entity.Location = new System.Drawing.Point(rand_pos_x, rand_pos_y);
                new_enemy.PB_Entity.BackColor = Color.Transparent;
                bossbullet.Add(new_enemy);
                Controls.Add(new_enemy.PB_Entity);
            }
        }

        private void bosspattern1()
        {
            for (int i=0; i < 6; i++)
            {
                int rand_pos_x = bpattern1[i];
                int rand_pos_y = 50;
                Entity new_enemy = new Entity();
                new_enemy.SetSpeed(10);
                new_enemy.PB_Entity.SizeMode = PictureBoxSizeMode.Zoom;
                new_enemy.PB_Entity.Size = new Size(30, 30);
                new_enemy.PB_Entity.Image = Properties.Resources.bossbullet;
                new_enemy.PB_Entity.Visible = true;
                new_enemy.PB_Entity.Location = new System.Drawing.Point(rand_pos_x, rand_pos_y);
                new_enemy.PB_Entity.BackColor = Color.Transparent;
                bossbullet.Add(new_enemy);
                Controls.Add(new_enemy.PB_Entity);
            }
        }

        private void bosspattern2()
        {
            for (int i = 0; i < 6; i++)
            {
                int rand_pos_x = bpattern2[i];
                int rand_pos_y = 50;
                Entity new_enemy = new Entity();
                new_enemy.SetSpeed(10);
                new_enemy.PB_Entity.SizeMode = PictureBoxSizeMode.Zoom;
                new_enemy.PB_Entity.Size = new Size(30, 30);
                new_enemy.PB_Entity.Image = Properties.Resources.bossbullet;
                new_enemy.PB_Entity.Visible = true;
                new_enemy.PB_Entity.Location = new System.Drawing.Point(rand_pos_x, rand_pos_y);
                new_enemy.PB_Entity.BackColor = Color.Transparent;
                bossbullet.Add(new_enemy);
                Controls.Add(new_enemy.PB_Entity);
            }
        }
        private void create_boss()
        {
            boss new_boss = new boss();
            new_boss.PB_Entity.Size = new Size(150, 200);
            new_boss.PB_Entity.SizeMode = PictureBoxSizeMode.Zoom;
            new_boss.PB_Entity.Image = Properties.Resources.boss;
            new_boss.PB_Entity.Visible = true;
            new_boss.PB_Entity.Location = new System.Drawing.Point(300, 65);
            new_boss.SetEntityY(65);
            new_boss.SetSpeed(0);
            new_boss.PB_Entity.BringToFront();
            new_boss.PB_Entity.BackColor = Color.Transparent;

            Boss.Add(new_boss);
            Controls.Add(new_boss.PB_Entity);


            // 보스 출현 문구 
            boss_lb.Image = Properties.Resources.warning;
            boss_lb.Width = 800;
            boss_lb.Height = 200;
            boss_lb.Location = new System.Drawing.Point(30, 250);
            boss_lb.BackColor = Color.Transparent;
            boss_lb.Parent = this;
            boss_lb.BringToFront();

            // 2초 후에 boss_lb 제거

            bossSpawnTimer.Interval = 2000; // 2초
            bossSpawnTimer.Tick += BossSpawnTimer_Tick;
            bossSpawnTimer.Start();

            //체력바
            bossHPBar.Location = new Point(10, 40);
            bossHPBar.Size = new Size(200, 20);
            bossHPBar.BackColor = Color.Red;
            this.Controls.Add(bossHPBar);

            bossHPBarBG.Location = new Point(10, 40);
            bossHPBarBG.Size = new Size(200, 20);
            bossHPBarBG.BackColor = Color.Gray;
            this.Controls.Add(bossHPBarBG);

        }
        private void BossSpawnTimer_Tick(object sender, EventArgs e)
        {
            Controls.Remove(boss_lb);
            bossSpawnTimer.Stop();
        }

    

        private void create_item()
        {
            Random generateRandom;
            DateTime curTime = DateTime.Now;
            generateRandom = new Random(curTime.Millisecond);

            int rand_pos_x = generateRandom.Next(10, 790);
            int rand_pos_y = generateRandom.Next(0, 50);

            Entity item = new Entity();
            item.SetSpeed(5);
            item.PB_Entity.Size = new Size(28,21);
            item.PB_Entity.Image = Properties.Resources.item1;
            item.PB_Entity.SizeMode = PictureBoxSizeMode.Zoom;
            item.PB_Entity.Visible = true;
            item.PB_Entity.Location = new System.Drawing.Point(rand_pos_x, rand_pos_y);
            item.PB_Entity.BackColor = Color.Transparent;
            item.life = 0;//0이면 스킬횟수추가
            items.Add(item);
            Controls.Add(item.PB_Entity);
        }

        private void create_heart()
        {
            Random generateRandom;
            DateTime curTime = DateTime.Now;
            generateRandom = new Random(curTime.Millisecond);

            int rand_pos_x = generateRandom.Next(10, 790);
            int rand_pos_y = generateRandom.Next(0, 50);

            Entity item = new Entity();
            item.SetSpeed(5);
            item.PB_Entity.Size = new Size(16, 12);
            item.PB_Entity.Image = Properties.Resources.heart;
            item.PB_Entity.Visible = true;
            item.PB_Entity.Location = new System.Drawing.Point(rand_pos_x, rand_pos_y);
            item.PB_Entity.BackColor = Color.Transparent;
            item.life = 1;//1이면 회복아이템
            items.Add(item);
            Controls.Add(item.PB_Entity);
        }

        int bossbulletcall = 0;
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
            else 
            {
                Random generateRandom;
                DateTime curTime = DateTime.Now;
                generateRandom = new Random(curTime.Millisecond);
         
                    int num = generateRandom.Next(0, 50) % 3;
                    if (num==0)
                    {
                        bosspatternbase();
                    }else if (num == 1)
                    {
                        bosspattern1();
                    }
                    else
                    {
                        bosspattern2();
                    }

            }
        }

        private void tmr_spawn_meteor_Tick(object sender, EventArgs e)
        {
            create_meteor();
        }

        public void InitBackGround1()
        {
            PictureBox background1 = new PictureBox();
            PictureBox background1_ = new PictureBox();

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
            PictureBox background2 = new PictureBox();
            PictureBox background2_ = new PictureBox();

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
            PictureBox background1 = new PictureBox();
            PictureBox background1_ = new PictureBox();

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
            PictureBox background2 = new PictureBox();
            PictureBox background2_ = new PictureBox();

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

        int heartsptime = 10;
        private void tmr_item_spawn_Tick(object sender, EventArgs e)
        {
       
            create_item();
            if (heartsptime == 0)
            {
                create_heart();
                heartsptime = 10;
            }
            heartsptime--;
        }
    }
}