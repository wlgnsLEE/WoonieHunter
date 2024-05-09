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
        private int bulletSpeed = 15;
        private bool isLeftPressed = false;
        private bool isRightPressed = false;
        private bool isUpPressed = false;
        private bool isDownPressed = false;

        public Form1()
        {
            InitializeComponent();

            this.MinimumSize = new Size(1200, 800);
            this.MaximumSize = new Size(1200, 800);

            player = new Entity();
            bullets = new List<PictureBox>();

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
            if (isRightPressed&&character.Right<=1150)
            {
                player.SetEntityX(X + speed);
            }
            if (isUpPressed&&character.Top>=250)
            {
                player.SetEntityY(Y - speed);
            }
            if (isDownPressed&&character.Bottom<=750)
            {
                player.SetEntityY(Y + speed);
            }

            character.Location = new System.Drawing.Point(X, Y);
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
            else if (e.KeyCode == Keys.Space)
            {
                int X = player.GetEntityX();
                int Y = player.GetEntityY();

                PictureBox new_bullet = new PictureBox();
                new_bullet.Image = Properties.Resources.bullet;
                new_bullet.Location = new System.Drawing.Point(X, Y);
                new_bullet.Visible = true;

                bullets.Add(new_bullet);
                this.Controls.Add(new_bullet);
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

                if (bullets[i].Top < -bullets[i].Height)
                {
                    this.Controls.Remove(bullets[i]);

                    bullets.RemoveAt(i);

                    i--;
                }
            }
        }
    }
}
