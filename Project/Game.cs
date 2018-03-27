using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }

        public Room EndingRoom { get; set; }

        public Player CurrentPlayer { get; set; }

        public Room[,] Rooms { get; set; }

        public void Setup()
        {

        }

        public void Reset()
        {

        }

        public void UseItem(string itemName)
        {

        }

        public void GoLeft()
        {
            List<int> indexes = IndexOfCurrentRoom();
            if (indexes[1] == 0){
                Console.WriteLine("You run into a wall and go nowhere");
                return;
            }
            CurrentRoom = Rooms[indexes[0], indexes[1] - 1];
        }

        public void GoRight()
        {
            List<int> indexes = IndexOfCurrentRoom();
            if (indexes[1] == Rooms.GetLength(0) - 1){
                Console.WriteLine("You run into a wall and go nowhere");
                return;
            }
            CurrentRoom = Rooms[indexes[0], indexes[1] + 1];
        }

        public void GoUp()
        {
            List<int> indexes = IndexOfCurrentRoom();
            if (indexes[0] == 0){
                Console.WriteLine("You run into a wall and go nowhere");
                return;
            }
            CurrentRoom = Rooms[indexes[0] - 1, indexes[1]];
        }

        public List<int> IndexOfCurrentRoom()
        {
            List<int> temp = new List<int>();
            for (int i = 0; i < Rooms.GetLength(0); i++)
            {
                for (int j = 0; j < Rooms.GetLength(1); j++)
                {
                    if (Rooms[i, j].Equals(CurrentRoom))
                    {
                        temp.Add(i);
                        temp.Add(j);
                        return temp;
                    }
                }
            }
            return temp;
        }

        public bool CheckWin()
        {
            if (CurrentRoom.Equals(EndingRoom))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Action(string choice, Player player)
        {
            Console.Clear();
            switch (choice)
            {
                case "left":
                    Console.WriteLine("Go Left");
                    GoLeft();
                    return CheckWin();
                case "right":
                    Console.WriteLine("Go Right");
                    GoRight();
                    return true;
                case "up":
                    Console.WriteLine("Go Up");
                    GoUp();
                    return true;
                case "use":
                    Console.WriteLine("Use your Item");
                    return true;
                case "take":
                    Console.WriteLine("Take the Item");
                    return true;
                case "inventory":
                    for (var i = 0; i < player.Inventory.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {player.Inventory[i].Name}");
                    }
                    return true;
                case "help":
                    Console.WriteLine("left, right, up, use, take, help");
                    return true;
                case "quit":
                    Console.WriteLine("Thanks for playing!");
                    return false;
                default:
                    Console.WriteLine($"{choice} is not an option!");
                    return true;
            }
        }

        public Game(Player player, int roomCount, GameSetup gs)
        {
            CurrentPlayer = player;
            Rooms = new Room[roomCount, roomCount];
            int loop = roomCount * roomCount;
            Random rand = new Random();
            for (int i = 0; i < loop; i++)
            {
                if (i == 0)
                {
                    Rooms[roomCount - 1, (roomCount - 1) / 2] = new Room(gs, i);
                    continue;
                }
                bool valid = false;
                while (!valid)
                {
                    var index1 = rand.Next(0, 5);
                    var index2 = rand.Next(0, 5);
                    if (Rooms[index1, index2] == null)
                    {
                        Rooms[index1, index2] = new Room(gs, i);
                        valid = true;
                    }
                }
            }
            CurrentRoom = Rooms[roomCount - 1, (roomCount - 1) / 2];
            bool notStart = true;
            while (notStart)
            {
                int endY = rand.Next(0, 5);
                int endX = rand.Next(0, 5);
                if (Rooms[endY, endX].Equals(CurrentRoom))
                {
                    continue;
                }
                else
                {
                    EndingRoom = Rooms[endY, endX];
                    notStart = false;
                }
            }
        }
    }
}