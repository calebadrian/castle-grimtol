using System;
using System.IO;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            GameSetup gs = new GameSetup();
            string[] roomNamePath = Directory.GetFiles(@"Project/Assets/");
            for (int i = 0; i < roomNamePath.Length; i++)
            {
                using (StreamReader sr = File.OpenText(roomNamePath[i]))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
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
                    Game currentGame = new Game(player, roomCount, gs);
                    Console.Clear();
                    bool playing = true;
                    while (playing)
                    {
                        Console.WriteLine($"You are currently in the {currentGame.CurrentRoom.Name}");
                        Console.WriteLine("What would you like to do?");
                        string choice = Console.ReadLine().ToLower();
                        playing = currentGame.Action(choice, player);
                        valid = true;
                    }
                    valid = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{roomCountString} is not a valid number! Enter a valid number!");
                }
            }
        }
    }
}
