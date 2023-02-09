using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Logic;

namespace WeatherData.Models
{

    internal class AvgTempMonth
    {
        private static readonly List<Data> weatherList = Data.CreateOneWeatherDataList();
        public static void AvgMonth()
        {
            string chosenPlace = InputManager.GetPlace();
            string chosenCategory = InputManager.ChooseCategory();
            List<Data> correctDateandPlace = new();
            Dictionary<string, double> dateAndAvg = new();
            var groupByDay = weatherList
                            .GroupBy(x => x.Year + x.Month);

            foreach (var day in groupByDay)
            {
                correctDateandPlace = weatherList
                                     .Where(d => (d.Year + d.Month)
                                     .Equals(day.Key) && d.InOrOut == chosenPlace)
                                     .ToList();

                double avgTemp = correctDateandPlace.Average(t => t.Temp);
                int avgHum = (int)correctDateandPlace.Average(t => t.Humidity);
                if (chosenCategory == "temp") dateAndAvg.Add(day.Key, avgTemp);
                else if (chosenCategory == "hum") dateAndAvg.Add(day.Key, avgHum);
            }
            foreach (var month in dateAndAvg)
            {
                Console.WriteLine("Medelvärde för: " + month.Key + ": " + month.Value);
                //foreach(var day in month.Key)
                //if (month.Key.Count() > 30)
                //{
                //    Console.WriteLine("Denna månad är inte mätningarna fullständiga. Endast data från " + month.Key.Count());
                //}
            }
        }
    }
}
