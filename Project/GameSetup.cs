using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project{

    public class GameSetup{

        public List<string> ItemNames { get; set; }

        public List<string> ItemDescriptions { get; set; }

        public List<string> RoomNames { get; set; }

        public List<string> RoomDescriptions { get; set; }

        public GameSetup(){
            ItemNames = new List<string>();
            ItemDescriptions = new List<string>();
            RoomNames = new List<string>();
            RoomDescriptions = new List<string>();
        }
    }
}