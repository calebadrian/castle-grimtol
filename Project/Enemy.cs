using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Enemy
    {
        public string Name { get; set; }

        public int Health { get; set; }

        public int DamageDone { get; set; }

        public Enemy(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            DamageDone = damage;
        }
    }
}