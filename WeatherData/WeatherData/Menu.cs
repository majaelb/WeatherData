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
        //private static List<Data> weatherList = Data.CreateOneWeatherDataList();

        public static void Run()
        {
            WeatherManager weatherManager= new WeatherManager();
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
                        Helper.ActiveChoice("Medeltemperatur");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        Helper.ActiveChoice("Sortering");
                        weatherManager.Run();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        runProgram = false;
                        break;
                }
            }
        }
        private static void PrintChoices()
        {
            List<string> menuOptions = new List<string> { "Medeltemperatur", "Sortering", "Avsluta" };
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
