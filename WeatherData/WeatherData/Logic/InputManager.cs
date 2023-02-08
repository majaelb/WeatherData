using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.Logic
{
    internal class InputManager
    {
        internal static void GetAvg(List<Data> weatherList)
        {
            while (true)
            {
                string inOrOut = Validator.RegexCheck("Vill du se data för inne eller ute? ", "^Inne|inne|Ute|ute$");
                if (inOrOut == null) return;
                string date = Validator.GetDate("Vilket datum vill du visa medelvärde för?(yyyyMMdd): ", "^\\d{8}$", inOrOut);
                if (date == null) return;

                List<Data> correctDateandPlace = weatherList
                    .Where(d => (d.Year + d.Month + d.Day)
                    .Equals(date) && d.InOrOut.ToLower() == inOrOut.ToLower())
                    .ToList();
                double avgTemp = correctDateandPlace.Average(t => t.Temp);
                double avgHum = correctDateandPlace.Average(t => t.Humidity);

                Console.WriteLine($"Medeltemperatur för {date} är {Math.Round(avgTemp, 2)}.");
                Console.WriteLine($"Medelfuktigheten för {date} är {Math.Round(avgHum, 2)}.");
            }
        }
    }
}
