using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Logic;

namespace WeatherData.Models
{
    internal class FallOrWinter
    {
        private static readonly List<Data> weatherList = Data.CreateOneWeatherDataList();

        public static void Run()
        {
            string chosenPlace = InputManager.GetPlace();
            string chosenCategory = InputManager.ChooseCategory();
            List<Data> correctDateandPlace = new();
            Dictionary<string, double> dateAndAvg = new();
            var groupByDay = weatherList
                            .GroupBy(x => x.Year + x.Month + x.Day);

            foreach (var day in groupByDay)
            {
                correctDateandPlace = weatherList
                                     .Where(d => (d.Year + d.Month + d.Day)
                                     .Equals(day.Key) && d.InOrOut == chosenPlace)
                                     .ToList();

                double avgTemp = correctDateandPlace.Average(t => t.Temp);
                int avgHum = (int)correctDateandPlace.Average(t => t.Humidity);
                if (chosenCategory == "temp") dateAndAvg.Add(day.Key, avgTemp);
                else if (chosenCategory == "hum") dateAndAvg.Add(day.Key, avgHum);
                //else if (chosenCategory == "mold") dateAndAvg.Add(day.Key, avgMold);
            }

            var duplicates = dateAndAvg
                .GroupBy(i=>i.Value < 10)
                .Where(g => g.Count() > 5)
                .Select(g => g.Key) .ToList();

            foreach (var item in duplicates)
            {
                Console.WriteLine(item);
            }
            //Sorterar endast på datum
            //foreach (var item in dateAndAvg)
            //{
            //    Console.WriteLine(item.Key + " medelvärde: " + Math.Round(item.Value, 2));
            //}
        }


 
    }
}
