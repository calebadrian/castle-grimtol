using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }

        public Player CurrentPlayer { get; set; }

        public List<Room> Rooms { get; set; }

        public void Setup()
        {

        }

        public void Reset()
        {

        }

        public void UseItem(string itemName)
        {

        }

        public Game(Player player, int roomCount, GameSetup gs)
        {
            CurrentPlayer = player;
            Rooms = new List<Room>();
            for (int i = 0; i < roomCount; i++){
                Room newRoom = new Room(gs, roomCount);
                Rooms.Add(newRoom);
            }
        }
    }
}