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
        internal static void GetAvg()
        {
            List<double> chosenTempData = new();
            List<double> chosenHumData = new();
            while (true)
            {
                string inOrOut = Validator.RegexCheck("Vill du se data för inne eller ute? ", "^Inne|inne|Ute|ute$");
                if (inOrOut == null) return;
                string date = Validator.GetDate("Vilket datum vill du visa medelvärde för?(yyyyMMdd): ", "^\\d{8}$", inOrOut);
                if (date == null) return;

                foreach (var d in inOrOut.ToLower() == "ute" ? Data.WeatherDataOutside : Data.WeatherDataInside)
                {
                    if (d.Year + d.Month + d.Day == date)
                    {
                        chosenTempData.Add(d.Temp);
                        chosenHumData.Add(d.Humidity);
                    }
                }
                double avgTemp = Helper.CountAvg(chosenTempData);
                double avgHum = Helper.CountAvg(chosenHumData);

                Console.WriteLine($"Medeltemperatur för {date} är {Math.Round(avgTemp,2)}.");
                Console.WriteLine($"Medelfuktigheten för {date} är {Math.Round(avgHum,2)}.");             
            }

        }
    }
}
