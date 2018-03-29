using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project{

    public class GameSetup{

        public List<int> EnemyDamage { get; set; }

        public List<int> EnemyHealth { get; set; }

        public List<string> EnemyNames { get; set; }

        public List<int> ItemDamages { get; set; }

        public List<string> ItemNames { get; set; }

        public List<string> ItemDescriptions { get; set; }

        public List<string> RoomNames { get; set; }

        public List<string> RoomDescriptions { get; set; }

        public GameSetup(){
            EnemyDamage = new List<int>();
            EnemyHealth = new List<int>();
            EnemyNames = new List<string>();
            ItemDamages = new List<int>();
            ItemNames = new List<string>();
            ItemDescriptions = new List<string>();
            RoomNames = new List<string>();
            RoomDescriptions = new List<string>();
        }
    }
}