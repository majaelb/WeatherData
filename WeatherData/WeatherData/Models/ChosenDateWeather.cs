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
    internal class ChosenDateWeather : IMeasurable
    {
        private readonly List<Data> weatherList = Data.CreateOneWeatherDataList();

        private string? chosenPlace;
        private string? chosenCategory;
        private Dictionary<string, double>? dateAndAvg;
        private string? chosenDate;
        public void Run()
        {
            chosenPlace = InputManager.GetPlace();
            if (chosenPlace == null) return;
            chosenDate = Validator.GetDate("Vilket datum vill du visa medelvärde för?(yyyyMMdd): ", "^\\d{8}$");
            if(chosenDate == null) return;
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

            correctDateandPlace = weatherList
                                 .Where(d => (d.Year + d.Month + d.Day)
                                 .Equals(chosenDate) && d.InOrOut == chosenPlace)
                                 .ToList();

            double avgTemp = correctDateandPlace.Average(t => t.Temp);
            int avgHum = (int)correctDateandPlace.Average(t => t.Humidity);
            if (chosenCategory == "temp") dateAndAvg.Add(chosenDate, avgTemp);
            else if (chosenCategory == "hum") dateAndAvg.Add(chosenDate, avgHum);

            return dateAndAvg;
        }
        public void Print()
        {
            foreach (var item in dateAndAvg.OrderByDescending(t => t.Value))
            {
                Console.WriteLine(item.Key + " medelvärde: " + Math.Round(item.Value,1));
            }
        }
    }
}

