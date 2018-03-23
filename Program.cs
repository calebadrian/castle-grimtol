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
                Console.WriteLine($"How many rooms does Castle Grimtol have this time {player.Name}?");
                string roomCountString = Console.ReadLine();
                int roomCount;
                if (Int32.TryParse(roomCountString, out roomCount))
                {
                    Game currentGame = new Game(player, roomCount, gs);
                    for (int i = 0; i < currentGame.Rooms.Count; i++){
                        Console.WriteLine($"Room Name: {currentGame.Rooms[i].Name}");
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
