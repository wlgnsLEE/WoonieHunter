using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoonieHunter
{
    internal class Entity
    {
        public PictureBox PB_Entity;
        public int life;
        private int entityX;
        private int entityY;
        private int speed;

        public Entity()
        {
            PB_Entity = new PictureBox();

            life = 3;
            entityX = 350;
            entityY = 700;
            speed = 5;
        }

        public int GetEntityX()
        {
            return entityX;
        }

        public int GetEntityY()
        {
            return entityY;
        }

        public int GetSpeed()
        {
            return speed;
        }

        public void SetEntityX(int x)
        {
            entityX = x; 
        }

        public void SetEntityY(int y)
        {
            entityY = y;
        }

        public void SetSpeed(int speed)
        {
            this.speed = speed;
        }

    }

    internal class boss : Entity
    {
        public int hp = 100;
    }
}
