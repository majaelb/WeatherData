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
            int lower = (int)Enums.PlaceOption.Inne;
            int upper = (int)Enums.PlaceOption.Ute;

            int inOrOut = Validator.GetIntInRange("Vill du se data för [1] = inne eller [2] = ute :", lower, upper);
            if (inOrOut == -1) return null;
            
            string chosenPlace;
            if (inOrOut == lower)
            {
                return "inne";
            }
            else if (inOrOut == upper)
            {
                return "ute";
            }             
            return null;

        }

        public static string ChooseCategory()
        {
            int lower = (int)Enums.Category.Temp;
            int middle = (int)Enums.Category.Humidity;
            int upper = (int)Enums.Category.Mold;

            int category = Validator.GetIntInRange("Vill du se data för [1] = temperatur, [2] = luftfuktighet eller [3] = Mögelrisk:", lower, upper);
            if (category == -1) return null;

            if (category == lower)
            {
                return "temp";
            }
            else if (category == middle)
            {
                return "hum";
            }
            else if (category == upper)
            {
                return "mold";
            }
            return null;

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


    }
}
