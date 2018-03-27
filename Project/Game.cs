using System;
using System.IO;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }

        public Room EndingRoom { get; set; }

        public Player CurrentPlayer { get; set; }

        public Room[,] Rooms { get; set; }

        public GameSetup GameSetup { get; set; }

        public void Setup()
        {
            Console.Clear();
            GameSetup gs = new GameSetup();
            GameSetup = gs;
            string[] roomNamePath = Directory.GetFiles(@"Project/Assets/");
            for (int i = 0; i < roomNamePath.Length; i++)
            {
                using (StreamReader sr = File.OpenText(roomNamePath[i]))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        switch (i)
                        {
                            case 0:
                                gs.ItemDescriptions.Add(line);
                                break;
                            case 1:
                                gs.ItemNames.Add(line);
                                break;
                            case 2:
                                gs.RoomDescriptions.Add(line);
                                break;
                            case 3:
                                gs.RoomNames.Add(line);
                                break;
                        }
                        gs.RoomNames.Add(line);
                    }
                }
            }
            Console.WriteLine("Welcome to Castle Grimtol!");
            Console.WriteLine("What is your name?");
            string playerName = Console.ReadLine();
            Player player = new Player(playerName);
            Console.Clear();
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine($"Is the Castle 5x5, 6x6, 7x7, or 8x8 {player.Name}?");
                Console.WriteLine("Just type 5, 6, 7, or 8 to choose!");
                string roomCountString = Console.ReadLine();
                int roomCount;
                if (Int32.TryParse(roomCountString, out roomCount))
                {
                    if (roomCount < 5 || roomCount > 8)
                    {
                        continue;
                    }
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
                        bool roomValid = false;
                        while (!roomValid)
                        {
                            var index1 = rand.Next(0, 5);
                            var index2 = rand.Next(0, 5);
                            if (Rooms[index1, index2] == null)
                            {
                                Rooms[index1, index2] = new Room(gs, i);
                                roomValid = true;
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
                    Console.Clear();
                    valid = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{roomCountString} is not a valid number! Enter a valid number!");
                }
            }
        }

        public void Play()
        {
            bool playing = true;
            while (playing)
            {
                Console.WriteLine(CurrentRoom.Description);
                Console.WriteLine($"What would you like to do {CurrentPlayer.Name}?");
                string choice = Console.ReadLine().ToLower();
                if (choice == "quit")
                {
                    Console.WriteLine("Thanks for Playing!");
                    playing = false;
                }
                bool won = Action(choice, CurrentPlayer);
                if (won)
                {
                    Console.WriteLine("You successfully navigated the castle and came out relatively unscathed!  Lucky you! ...this time");
                    Console.WriteLine("Would you like to play again?");
                    string yorn = Console.ReadLine().ToLower();
                    if (yorn[0] != 'n')
                    {
                        Reset();
                        playing = true;
                    }
                    else
                    {
                        Console.WriteLine("Thanks for Playing!");
                        playing = false;
                    }
                }
                else
                {

                }
            }
        }

        public void Reset()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine($"Is the Castle 5x5, 6x6, 7x7, or 8x8 {CurrentPlayer.Name}?");
                Console.WriteLine("Just type 5, 6, 7, or 8 to choose!");
                string roomCountString = Console.ReadLine();
                int roomCount;
                if (Int32.TryParse(roomCountString, out roomCount))
                {
                    if (roomCount < 5 || roomCount > 8)
                    {
                        continue;
                    }
                    Rooms = new Room[roomCount, roomCount];
                    int loop = roomCount * roomCount;
                    Random rand = new Random();
                    for (int i = 0; i < loop; i++)
                    {
                        if (i == 0)
                        {
                            Rooms[roomCount - 1, (roomCount - 1) / 2] = new Room(GameSetup, i);
                            continue;
                        }
                        bool roomValid = false;
                        while (!roomValid)
                        {
                            var index1 = rand.Next(0, 5);
                            var index2 = rand.Next(0, 5);
                            if (Rooms[index1, index2] == null)
                            {
                                Rooms[index1, index2] = new Room(GameSetup, i);
                                roomValid = true;
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
                    Console.Clear();
                    valid = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{roomCountString} is not a valid number! Enter a valid number!");
                }
            }
        }

        public void UseItem(string itemName)
        {

        }

        public void GoLeft()
        {
            List<int> indexes = IndexOfCurrentRoom();
            if (indexes[1] == 0)
            {
                Console.WriteLine("You run into a wall and go nowhere");
                return;
            }
            CurrentRoom = Rooms[indexes[0], indexes[1] - 1];
        }

        public void GoRight()
        {
            List<int> indexes = IndexOfCurrentRoom();
            if (indexes[1] == Rooms.GetLength(0) - 1)
            {
                Console.WriteLine("You run into a wall and go nowhere");
                return;
            }
            CurrentRoom = Rooms[indexes[0], indexes[1] + 1];
        }

        public void GoUp()
        {
            List<int> indexes = IndexOfCurrentRoom();
            if (indexes[0] == 0)
            {
                Console.WriteLine("You run into a wall and go nowhere");
                return;
            }
            CurrentRoom = Rooms[indexes[0] - 1, indexes[1]];
        }

        public void GoDown()
        {
            List<int> indexes = IndexOfCurrentRoom();
            if (indexes[0] == Rooms.GetLength(0) - 1){
                Console.WriteLine("You run into a wall and go nowhere");
                return;
            }
            CurrentRoom = Rooms[indexes[0] + 1, indexes[1]];
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
                case "go west":
                    Console.WriteLine("Go Left");
                    GoLeft();
                    return CheckWin();
                case "go east":
                    Console.WriteLine("Go Right");
                    GoRight();
                    return CheckWin();
                case "go north":
                    Console.WriteLine("Go Up");
                    GoUp();
                    return CheckWin();
                case "go south":
                    Console.WriteLine("Go Down");
                    GoDown();
                    return CheckWin();
                case "use":
                    Console.WriteLine("Use your Item");
                    return CheckWin();
                case "take":
                    Console.WriteLine("Take the Item");
                    return CheckWin();
                case "inventory":
                    for (var i = 0; i < player.Inventory.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {player.Inventory[i].Name}");
                    }
                    return CheckWin();
                case "help":
                    Console.WriteLine("left, right, up, use, take, help");
                    return CheckWin();
                default:
                    Console.WriteLine($"{choice} is not an option!");
                    return CheckWin();
            }
        }
        public Game()
        {
            Setup();
        }
    }
}