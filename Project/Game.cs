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
                                int damage;
                                Int32.TryParse(line, out damage);
                                gs.EnemyDamage.Add(damage);
                                break;
                            case 1:
                                int health;
                                Int32.TryParse(line, out health);
                                gs.EnemyHealth.Add(health);
                                break;
                            case 2:
                                gs.EnemyNames.Add(line);
                                break;
                            case 3:
                                gs.ItemDescriptions.Add(line);
                                break;
                            case 4:
                                gs.ItemNames.Add(line);
                                break;
                            case 5:
                                gs.RoomDescriptions.Add(line);
                                break;
                            case 6:
                                gs.RoomNames.Add(line);
                                break;
                        }
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
                            var index1 = rand.Next(0, roomCount);
                            var index2 = rand.Next(0, roomCount);
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
                        int endY = rand.Next(0, roomCount);
                        int endX = rand.Next(0, roomCount);
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
                Console.Clear();
                Console.WriteLine(CurrentRoom.Description);
                Console.WriteLine($"What would you like to do {CurrentPlayer.Name}?");
                bool won = Action();
                if (won && CurrentPlayer.Health > 0)
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
                else if (CurrentPlayer.Health < 0)
                {
                    Console.WriteLine("You Died!");
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
                            var index1 = rand.Next(0, roomCount);
                            var index2 = rand.Next(0, roomCount);
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
                        int endY = rand.Next(0, roomCount);
                        int endX = rand.Next(0, roomCount);
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
            Console.WriteLine($"Used {itemName.ToLower()}");
            for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
            {
                if (CurrentPlayer.Inventory[i].Name == itemName)
                {
                    CurrentRoom.UseItem(CurrentPlayer.Inventory[i]);
                    CurrentPlayer.RemoveItem(CurrentPlayer.Inventory[i]);
                    return;
                }
            }
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
            if (indexes[0] == Rooms.GetLength(0) - 1)
            {
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

        public bool Action()
        {
            string choice = Console.ReadLine().ToLower();
            if (choice == "quit")
            {
                Console.WriteLine("Thanks for Playing!");
                System.Environment.Exit(1);
                return false;
            }
            if (CurrentRoom.Items.Count > 0)
            {
                if (choice == "take " + CurrentRoom.Items[0].Name.ToLower())
                {
                    Console.WriteLine($"Took {CurrentRoom.Items[0].Name}");
                    CurrentPlayer.TakeItem(CurrentRoom.Items[0]);
                    CurrentRoom.RemoveItem(CurrentRoom.Items[0]);
                    return Action();
                }
            }
            if (CurrentPlayer.Inventory.Count > 0)
            {
                for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
                {
                    if (choice == "use " + CurrentPlayer.Inventory[i].Name.ToLower())
                    {
                        UseItem(CurrentPlayer.Inventory[i].Name);
                        return Action();
                    }
                }
            }
            if (CurrentRoom.Enemies.Count > 0)
            {
                for (int i = 0; i < CurrentRoom.Enemies.Count; i++)
                {
                    if (choice == "attack " + CurrentRoom.Enemies[i].Name.ToLower())
                    {
                        Console.WriteLine($"You defeated {CurrentRoom.Enemies[i].Name}");
                        CurrentPlayer.Health -= CurrentRoom.Enemies[i].DamageDone;
                        CurrentRoom.Enemies.Remove(CurrentRoom.Enemies[i]);
                        return Action();
                    }
                }
            }
            switch (choice)
            {
                case "go west":
                    if (CurrentRoom.Enemies.Count > 0)
                    {
                        Console.WriteLine("You can't leave while enemies are still in the room!");
                        return Action();
                    }
                    GoLeft();
                    return CheckWin();
                case "go east":
                    if (CurrentRoom.Enemies.Count > 0)
                    {
                        Console.WriteLine("You can't leave while enemies are still in the room!");
                        return Action();
                    }
                    GoRight();
                    return CheckWin();
                case "go north":
                    if (CurrentRoom.Enemies.Count > 0)
                    {
                        Console.WriteLine("You can't leave while enemies are still in the room!");
                        return Action();
                    }
                    GoUp();
                    return CheckWin();
                case "go south":
                    if (CurrentRoom.Enemies.Count > 0)
                    {
                        Console.WriteLine("You can't leave while enemies are still in the room!");
                        return Action();
                    }
                    GoDown();
                    return CheckWin();
                case "inventory":
                    for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
                    {
                        Console.WriteLine(CurrentPlayer.Inventory[i].Name);

                    }
                    return Action();
                case "help":
                    Console.WriteLine($@"
----------------------------------------------------------------------
go <direction>
This will take you any of the four cardinal directions
----------------------------------------------------------------------
take <item>
This will take whatever item you specify that is in the current room
----------------------------------------------------------------------
use <item>
This will use an item from your inventory and remove it
----------------------------------------------------------------------
attack <enemy>
This will attack the enemy of your choosing in that room
----------------------------------------------------------------------
inventory
This will show all your items in your inventory currently
----------------------------------------------------------------------
look
This will reprint the description of the room for you
----------------------------------------------------------------------
");
                    return Action();
                default:
                    Console.WriteLine($"{choice} is not an option!");
                    return Action();
            }
        }
        public Game()
        {
            Setup();
        }
    }
}