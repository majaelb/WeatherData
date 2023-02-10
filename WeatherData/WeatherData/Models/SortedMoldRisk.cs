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
        private string? chosenCategory;
        private Dictionary<string, string>? dateAndRisk;

        public void Run()
        {
            chosenPlace = InputManager.GetPlace();
            if (chosenPlace == null) return;
            //chosenCategory = InputManager.ChooseCategory();
            //if (chosenCategory == null) return;
            //Timer att köra programmet
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            dateAndRisk = GetAvg();
            //Print();
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                                ts.Hours, ts.Minutes, ts.Seconds,
                                                ts.Milliseconds / 10);
            //Console.SetCursorPosition(70, 0);
            //Console.WriteLine("RunTime " + elapsedTime);

        }

        public Dictionary<string, string> GetAvg()
        {
            List<Data> correctDateandPlace = new();
            List<double> dateAndAvg = new();
            Dictionary<string, string> dateAndAvgTempAndHum = new();
            string noRisk = "Ingen mögelrisk";
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
                if (avgTemp < 10 && avgHum < 60)
                {
                    dateAndAvgTempAndHum.Add(day.Key, low);

                }
                else if (avgTemp < 10 && avgHum > 60)
                {
                    dateAndAvgTempAndHum.Add(day.Key, middle);

                }
                else if (avgTemp < 10 && avgHum > 90)
                {
                    dateAndAvgTempAndHum.Add(day.Key, high);

                }
                else
                {
                    dateAndAvgTempAndHum.Add(day.Key, noRisk);
                }


            }
            if (dateAndAvgTempAndHum.Any())
            {

                foreach (var item in dateAndAvgTempAndHum)
                {
                    Console.WriteLine(item.Key + ": " + item.Value);

                }
            }
            else
            {
                Console.WriteLine("Det fanns ingen risk för mögel under perioden");
            }
            return dateAndAvgTempAndHum;
        }

        //public void GetAvgMoldRisk()
        //{
        //    foreach (var day in dateAndAvg)
        //    {
        //        if(day.Value)
        //    }
        //}

        //public void Print()
        //{
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.WriteLine();
        //    Console.WriteLine(chosenCategory == "temp" ? "Varmast till kallast" : "Torrast till fuktigast");
        //    Console.ResetColor();
        //    foreach (var item in chosenCategory == "temp" ? dateAndAvg.OrderByDescending(t => t.Value) : dateAndAvg.OrderBy(t => t.Value))
        //    {
        //        Console.WriteLine(item.Key + " medelvärde: " + Math.Round(item.Value, 1));
        //    }

        //    //Sorterar endast på datum
        //    //foreach (var item in dateAndAvg)
        //    //{
        //    //    Console.WriteLine(item.Key + " medelvärde: " + Math.Round(item.Value, 2));
        //    //}
        //}
    }
}
