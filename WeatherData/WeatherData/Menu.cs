using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Logic;
using WeatherData.Models;

namespace WeatherData
{
    internal class Menu
    {
        public static void Run()
        {
            SortedWeather weatherManager = new();
            ChosenDateWeather chosenDate = new();
            SeasonStart seasonStart = new();
            SortedMoldRisk sortedMoldRisk = new();
            bool runProgram = true;
            while (runProgram)
            {
                PrintChoices();
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Helper.ActiveChoice("Medelvärden med datumsök");
                        chosenDate.Run();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        Helper.ActiveChoice("Sortering av medelvärden");
                        weatherManager.Run();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        Helper.ActiveChoice("Sortering av mögelrisk");
                        sortedMoldRisk.Run();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.Clear();
                        Helper.ActiveChoice("Meterologisk säsongsstart");
                        seasonStart.Run();
                        Console.ReadKey();
                        Console.Clear();
                        break;                
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        runProgram = false;
                        break;
                }
            }
        }
        private static void PrintChoices()
        {
            List<string> menuOptions = new List<string> { "Medelvärden med datumsök", "Sortering av medelvärden", "Sortering av mögelrisk", "Meterologisk säsongsstart", "Avsluta" };
            PrintList(menuOptions);
        }

        private static void PrintList(List<string> menuOptions)
        {
            Console.WriteLine("Välj funktion");
            Console.WriteLine("=====");
            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] " + menuOptions[i]);
            }
        }
    }
}
