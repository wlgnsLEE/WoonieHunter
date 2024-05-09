using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoonieHunter
{
    internal class Entity
    {
        private int life;
        private int entityX;
        private int entityY;
        private int speed;

        public Entity()
        {
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
    }

    internal class Enemy : Entity
    {
        
    }
}
