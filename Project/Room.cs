using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Room : IRoom
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Item> Items { get; set; }

        public void UseItem(Item item)
        {

        }

        public Room(GameSetup gs, int roomCount)
        {
            Random rand = new Random();
            Name = gs.RoomNames[rand.Next(0, roomCount)];
        }
    }
}