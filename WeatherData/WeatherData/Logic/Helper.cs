using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherData.Logic
{
    internal class Helper
    {
        internal static void CountAvg(List<List<double>> avgLists)
        {
            double totalTemp = 0;
            double totalHum = 0;

            foreach (var t in avgLists[0])
            {
                totalTemp += t;
            }
            foreach (var h in avgLists[1])
            {
                totalHum += h;
            }

            double avgTemp = totalTemp / avgLists[0].Count;
            double avgHum = totalHum / avgLists[1].Count;
            
            return
        }
        
        internal static void ActiveChoice(string choice) //Skriver ut var på sidan man befinner sig efter ett knappval i en meny
        {
            Console.WriteLine("Aktivt val:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(choice);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
    }
}
