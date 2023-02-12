using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Interfaces;
using WeatherData.Logic;

namespace WeatherData.Models
{
    internal class SortedMoldRisk
    {
        private readonly List<Data> weatherList = Data.CreateOneWeatherDataList();

        private string? chosenPlace;
        private Dictionary<string, string>? dateAndRisk;

        public void Run()
        {
            chosenPlace = InputManager.GetPlace();
            if (chosenPlace == null) return;
            Stopwatch stopWatch = new();
            stopWatch.Start();
            dateAndRisk = GetAvg();
            Print();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Helper.TimeCount(ts);
            Console.ReadKey();
        }

        public Dictionary<string, string> GetAvg()
        {
            List<Data> correctDateandPlace = new();
            List<double> dateAndAvg = new();
            Dictionary<string, string> dateAndMoldRisk = new();
            //string noRisk = "Ingen mögelrisk";
            string low = "Låg mögelrisk";
            string middle = "Medelhög mögelrisk";
            string high = "Hög mögelrisk";

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
                //if (avgTemp < 0 || avgTemp > 50 || avgHum < 65)
                //{
                //    dateAndMoldRisk.Add(day.Key, noRisk);
                //}
                if (avgTemp > 0 && avgHum > 65 && avgHum < 81)
                {
                    dateAndMoldRisk.Add(day.Key, low);
                }
                else if (avgTemp > 2 && avgHum > 80 && avgHum < 86)
                {
                    dateAndMoldRisk.Add(day.Key, middle);
                }
                else if (avgTemp > 5 && avgHum > 85)
                {
                    dateAndMoldRisk.Add(day.Key, high);
                }
            }
            return dateAndMoldRisk;
        }

        public void Print()
        {
            if (dateAndRisk.Any())
            {
                foreach (var item in dateAndRisk)
                {
                    Console.WriteLine(item.Key + ": " + item.Value);
                }
            }
            else
            {
                Console.WriteLine("Det fanns ingen risk för mögel under perioden");
            }
        }

    }
}
