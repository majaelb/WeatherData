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
        public static string GetPlace()
        {
            while (true)
            {

                int inOrOut = Validator.GetInt("Vill du se data för [1] = inne eller [2] = ute :");
                if (inOrOut == -1) return null;
                string chosenPlace;

                switch (inOrOut)
                {
                    case (int)Enums.PlaceOption.Inne:
                        chosenPlace = "inne";
                        break;
                    case (int)Enums.PlaceOption.Ute:
                        chosenPlace = "ute";
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        continue;
                }
            return chosenPlace;
            }
        }



        internal static void GetAvg(List<Data> weatherList)
        {
            while (true)
            {
                string inOrOut = Validator.RegexCheck("Vill du se data för inne eller ute? ", "^Inne|inne|Ute|ute$");
                if (inOrOut == null) return;
                string date = Validator.GetDate("Vilket datum vill du visa medelvärde för?(yyyyMMdd): ", "^\\d{8}$");
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

        internal static void GetAvgTest(List<Data> weatherList)
        {
            string inOrOut = Validator.RegexCheck("Vill du se data för inne eller ute? ", "^Inne|inne|Ute|ute$");
            List<Data> correctDateandPlace = new();
            Dictionary<string, double> dateAndAvg = new();
            if (inOrOut == null) return;
            var groupByDay = weatherList
                .GroupBy(x => x.Year + x.Month + x.Day);

            foreach (var day in groupByDay)
            {
                correctDateandPlace = weatherList
                                                  .Where(d => (d.Year + d.Month + d.Day)
                                                  .Equals(day.Key) && d.InOrOut == inOrOut.ToLower())
                                                  .ToList();

                double avgTemp = correctDateandPlace.Average(t => t.Temp);
                double avgHum = correctDateandPlace.Average(t => t.Humidity);
                dateAndAvg.Add(day.Key, avgTemp);
                //TODO: Fråga om humidity/temp och gör ternary i dateandavg.add?
            }
            foreach (var item in dateAndAvg.OrderByDescending(t => t.Value))
            {
                Console.WriteLine(item.Key + " medeltemp: " + item.Value);
            }

        }
    }
}
