using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }

        public Player CurrentPlayer { get; set; }

        public void Setup()
        {

        }

        public void Reset()
        {

        }

        public void UseItem(string itemName)
        {

        }
    }
}