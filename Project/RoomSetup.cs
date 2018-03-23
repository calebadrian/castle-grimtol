using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project{

    public class GameSetup{

        public List<string> RoomNames { get; set; }

        public GameSetup(){
            RoomNames = new List<string>();
        }
    }
}