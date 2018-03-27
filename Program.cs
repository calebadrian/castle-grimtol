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
                        switch (i)
                        {
                            case 0:
                                gs.RoomDescriptions.Add(line);
                                break;
                            case 1:
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
                    Game currentGame = new Game(player, roomCount, gs);
                    Console.Clear();
                    bool playing = true;
                    while (playing)
                    {
                        Console.WriteLine(currentGame.CurrentRoom.Description);
                        Console.WriteLine($"What would you like to do {player.Name}?");
                        string choice = Console.ReadLine().ToLower();
                        bool won = currentGame.Action(choice, player);
                        if (won)
                        {
                            Console.WriteLine("You successfully navigated the castle and came out relatively unscathed!  Lucky you! ...this time");
                            Console.WriteLine("Would you like to play again?");
                            string yorn = Console.ReadLine().ToLower();
                            if (yorn[0] != 'n')
                            {
                                playing = false;
                            }
                            else
                            {
                                valid = true;
                            }
                        } else if (!won && choice == "quit"){
                            Console.WriteLine("Thanks for Playing!");
                            valid = true;
                        }
                    }
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
