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
    internal class SortedWeather : IMeasurable
    {
        private readonly List<Data> weatherList = Data.CreateOneWeatherDataList();

        private string? chosenPlace;
        private string? chosenCategory;
        private Dictionary<string, double>? dateAndAvg;
       
        public void Run()
        {
            chosenPlace = InputManager.GetPlace();
            if (chosenPlace == null) return;
            chosenCategory = InputManager.GetCategory();
            if (chosenCategory == null) return;
            Stopwatch stopWatch = new();
            stopWatch.Start();
            dateAndAvg = GetAvg();
            Print();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Helper.TimeCount(ts);
            Console.ReadKey();
        }
   
        public Dictionary<string, double> GetAvg()
        {
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
            }
            return dateAndAvg;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(chosenCategory == "temp" ? "Varmast till kallast" : "Torrast till fuktigast");
            Console.ResetColor();
            foreach (var item in chosenCategory == "temp" ? dateAndAvg.OrderByDescending(t => t.Value) : dateAndAvg.OrderBy(t => t.Value))
            {
                $"{item.Key} medelvärde: {Math.Round(item.Value, 1)}".Cw();
            }
        }
    }
}
