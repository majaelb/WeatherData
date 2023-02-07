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
        internal static List<List<double>> GetAvg()
        {
            List<double> chosenTempData = new();
            List<double> chosenHumData = new();
            List<List<double>> avgLists = new();
            while (true)
            {
                string inOrOut = Validator.RegexCheck("Vill du se data för inne eller ute? ", "^Inne|inne|Ute|ute$");
                if (inOrOut == null) return null;
                string date = Validator.GetDate("Vilket datum vill du visa medelvärde för?(yyyyMMdd): ", "^\\d{8}$", inOrOut);
                if (date == null) return null;

                foreach (var d in inOrOut.ToLower() == "ute" ? Data.WeatherDataOutside : Data.WeatherDataInside)
                {
                    if (d.Year + d.Month + d.Day == date)
                    {
                        chosenTempData.Add(d.Temp);
                        chosenHumData.Add(d.Humidity);
                    }
                }
                avgLists.Add(chosenTempData);
                avgLists.Add(chosenHumData);
                return avgLists;

                //double totalTemp = 0;
                //double totalHum = 0;

                //foreach (var t in chosenTempData)
                //{
                //    totalTemp += t;
                //}
                //foreach (var h in chosenHumData)
                //{
                //    totalHum += h;
                //}

                //double avgTemp = totalTemp / chosenTempData.Count;
                //double avgHum = totalHum / chosenHumData.Count;

                //Console.WriteLine($"Medeltemperatur för {date} är {Math.Round(avgTemp,2)}.");
                //Console.WriteLine($"Medelfuktigheten för {date} är {Math.Round(avgHum,2)}.");
            }

        }
    }
}
