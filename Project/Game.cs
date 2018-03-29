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
                Console.WriteLine($"Name: {CurrentPlayer.Name} | Health: {CurrentPlayer.Health}");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine(CurrentRoom.Description);
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"What would you like to do {CurrentPlayer.Name}?");
                bool won = Action();
                if (won && CurrentPlayer.Health > 0)
                {
                    Console.WriteLine($"Name: {CurrentPlayer.Name} | Health: {CurrentPlayer.Health}");
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
                else if (CurrentPlayer.Health <= 0)
                {
                    Console.Clear();
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
            CurrentPlayer.Health = 100;
            CurrentPlayer.Inventory = new List<Item>();
            bool valid = false;
            while (!valid)
            {
                Console.Clear();
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
            if (itemName == "sword" || itemName == "dagger" || itemName == "5 iron")
            {
                Console.WriteLine("You can't use that you must attack with it!");
                return;
            }
            else if (itemName == "ham")
            {
                CurrentPlayer.Health += 25;
            }
            else if (itemName == "potion")
            {
                CurrentPlayer.Health += 50;
            }
            Console.Clear();
            Console.WriteLine($"Name: {CurrentPlayer.Name} | Health: {CurrentPlayer.Health}");
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine(CurrentRoom.Description);
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine($"Used {itemName}");
            Console.WriteLine($"What would you like to do {CurrentPlayer.Name}?");
            for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
            {
                if (CurrentPlayer.Inventory[i].Name.ToLower() == itemName)
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
                for (int i = 0; i < CurrentRoom.Items.Count; i++)
                {
                    if (choice == "take " + CurrentRoom.Items[i].Name.ToLower())
                    {
                        if (CurrentRoom.Items[i].Name.ToLower() == "jar")
                        {
                            CurrentPlayer.Health = 0;
                            return false;
                        }
                        Console.WriteLine($"Took {CurrentRoom.Items[i].Name}");
                        CurrentPlayer.TakeItem(CurrentRoom.Items[i]);
                        CurrentRoom.RemoveItem(CurrentRoom.Items[i]);
                        return Action();
                    }

                }
            }
            if (CurrentPlayer.Inventory.Count > 0)
            {
                for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
                {
                    if (choice == "use " + CurrentPlayer.Inventory[i].Name.ToLower())
                    {
                        UseItem(CurrentPlayer.Inventory[i].Name.ToLower());
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
                        while (CurrentRoom.Enemies[i].Health > 0)
                        {
                            if (CurrentPlayer.Inventory.Count > 0)
                            {
                                Console.WriteLine("With which item?");
                                for (var j = 0; j < CurrentPlayer.Inventory.Count; j++)
                                {
                                    Console.WriteLine($"{CurrentPlayer.Inventory[j].Name.ToLower()} | {CurrentPlayer.Inventory[j].Description} | {CurrentPlayer.Inventory[j].Damage}");
                                }
                                string weaponChoice = Console.ReadLine().ToLower();
                                if (weaponChoice == "orb" && CurrentRoom.Enemies[i].Name == "The Reaper")
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Name: {CurrentPlayer.Name} | Health: {CurrentPlayer.Health}");
                                    Console.WriteLine("---------------------------------------------------------------------------------");
                                    Console.WriteLine(CurrentRoom.Description);
                                    Console.WriteLine("---------------------------------------------------------------------------------");
                                    Console.WriteLine("The orb magically defeats the reaper and he dissapears!");
                                    CurrentRoom.Enemies[i].Health = 0;
                                    break;
                                }
                                for (var k = 0; k < CurrentPlayer.Inventory.Count; k++)
                                {
                                    if (CurrentPlayer.Inventory[k].Name.ToLower() == weaponChoice)
                                    {
                                        CurrentPlayer.Health -= CurrentRoom.Enemies[i].DamageDone;
                                        CurrentRoom.Enemies[i].Health -= CurrentPlayer.Inventory[k].Damage;
                                        if (CurrentPlayer.Health <= 0)
                                        {
                                            return false;
                                        }
                                        Console.Clear();
                                        Console.WriteLine($"Name: {CurrentPlayer.Name} | Health: {CurrentPlayer.Health}");
                                        Console.WriteLine("---------------------------------------------------------------------------------");
                                        Console.WriteLine(CurrentRoom.Description);
                                        Console.WriteLine("---------------------------------------------------------------------------------");
                                        Console.WriteLine($"You attack with the {CurrentPlayer.Inventory[k].Name.ToLower()} and do {CurrentPlayer.Inventory[k].Damage} damage.");
                                        Console.WriteLine($"{CurrentRoom.Enemies[i].Name.ToLower()} health: {CurrentRoom.Enemies[i].Health}");
                                    }
                                }

                            }
                            else
                            {
                                CurrentPlayer.Health -= CurrentRoom.Enemies[i].DamageDone;
                                CurrentRoom.Enemies[i].Health -= 5;
                                if (CurrentPlayer.Health <= 0)
                                {
                                    return false;
                                }
                                Console.Clear();
                                Console.WriteLine($"Name: {CurrentPlayer.Name} | Health: {CurrentPlayer.Health}");
                                Console.WriteLine("---------------------------------------------------------------------------------");
                                Console.WriteLine(CurrentRoom.Description);
                                Console.WriteLine("---------------------------------------------------------------------------------");
                                Console.WriteLine($"You flail at {CurrentRoom.Enemies[i].Name.ToLower()} with your fists but it only does 5 damage!");
                                Console.WriteLine($"{CurrentRoom.Enemies[i].Name.ToLower()} health: {CurrentRoom.Enemies[i].Health}");
                            }
                        }
                        Console.WriteLine($"You defeated {CurrentRoom.Enemies[i].Name}!");
                        CurrentRoom.Enemies.Remove(CurrentRoom.Enemies[i]);
                        Console.WriteLine($"What would you like to do {CurrentPlayer.Name}?");
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
                    if (CurrentPlayer.Inventory.Count > 0)
                    {
                        for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
                        {
                            Console.WriteLine(CurrentPlayer.Inventory[i].Name.ToLower());

                        }
                    }
                    else
                    {
                        Console.WriteLine("There is nothing in your inventory");
                    }
                    return Action();
                case "items":
                    if (CurrentRoom.Items.Count > 0)
                    {
                        for (int i = 0; i < CurrentRoom.Items.Count; i++)
                        {
                            Console.WriteLine($"{CurrentRoom.Items[i].Name.ToLower()} | {CurrentRoom.Items[i].Description.ToLower()} | {CurrentRoom.Items[i].Damage}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no items in this room!");
                    }
                    return Action();
                case "enemies":
                    if (CurrentRoom.Enemies.Count > 0)
                    {
                        for (int i = 0; i < CurrentRoom.Enemies.Count; i++)
                        {
                            Console.WriteLine($"{CurrentRoom.Enemies[i].Name.ToLower()} | {CurrentRoom.Enemies[i].Health} | {CurrentRoom.Enemies[i].DamageDone}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no enemies in this room!");
                    }
                    return Action();
                case "help":
                    Console.WriteLine($@"
--------------------------------------------------------------------------
go <direction>
This will take you any of the four cardinal directions
--------------------------------------------------------------------------
take <item>
This will take whatever item you specify that is in the current room
--------------------------------------------------------------------------
use <item>
This will use an item from your inventory and remove it
--------------------------------------------------------------------------
attack <enemy>
This will attack the enemy of your choosing in that room
--------------------------------------------------------------------------
inventory
This will show all your items in your inventory currently
--------------------------------------------------------------------------
items
This will show you all the items avaliable to pick up in the current room
--------------------------------------------------------------------------
enemies
This will show you all the enemies that are in the room
--------------------------------------------------------------------------
look
This will reprint the description of the room for you
--------------------------------------------------------------------------
");
                    return Action();
                case "look":
                    Console.WriteLine(CurrentRoom.Description);
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