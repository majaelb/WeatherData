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
    internal class SeasonStart : IMeasurable
    {
        private static readonly List<Data> weatherList = Data.CreateOneWeatherDataList();
        private static Dictionary<string, double> ? dateAndAvg;
        private static Dictionary<string, double> ? autumnStart;
        private static Dictionary<string, double> ? winterStart;
        public void Run()
        {
            int maxTempAutumn = 10;
            //ändrade temperaturen till 1 för att få ut data. Vintern visades inte p.g.a 0 inte sker 5 dagar i rad.
            //borde gå att ändra vid getSeasonstart
            int maxTempWinter = 1;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            dateAndAvg = GetAvg();
            autumnStart = GetSeasonStart(maxTempAutumn);
            winterStart = GetSeasonStart(maxTempWinter);
            Print();
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                                ts.Hours, ts.Minutes, ts.Seconds,
                                                ts.Milliseconds / 10);
            Console.SetCursorPosition(70, 0);
            Console.WriteLine("RunTime " + elapsedTime);
        }
        public Dictionary<string, double> GetAvg()
        {
            List<Data> correctDateandPlace = new();
            Dictionary<string, double> dateAndAvg = new();
            var groupByDay = weatherList.GroupBy(x => x.Year + x.Month + x.Day);

            foreach (var day in groupByDay)
            {
                correctDateandPlace = weatherList
                                     .Where(d => (d.Year + d.Month + d.Day)
                                     .Equals(day.Key) && d.InOrOut == "ute")
                                     .ToList();

                double avgTemp = correctDateandPlace.Average(t => t.Temp);
                int avgHum = (int)correctDateandPlace.Average(t => t.Humidity);
                dateAndAvg.Add(day.Key, avgTemp);
            }
            return dateAndAvg;
        }

        public static Dictionary<string, double> GetSeasonStart(double maxTemp)
        {
            var sortedDateAndAvg = dateAndAvg.OrderBy(x => x.Key);
            Dictionary<string, double> seasonStart = new();
            int count = 0;

            foreach (var s in sortedDateAndAvg)
            {
                if (s.Value <= maxTemp)
                {
                    count++;
                    seasonStart.Add(s.Key, s.Value);
                    if (count == 5)
                    {
                        break;
                    }
                }
                else
                {
                    count = 0;
                    seasonStart.Clear();
                }
            }
            return seasonStart;
        }
        public void Print()
        {
            if (autumnStart.Count == 5)
            {
                Console.Write("Hösten anlände: ");
                foreach (var s in autumnStart.Take(1))
                {
                    Console.WriteLine($"{s.Key} med {Math.Round(s.Value,1)} grader");
                }
            }
            if (winterStart.Count == 5)
            {
                Console.Write("Vintern anlände: ");
                foreach (var s in winterStart.Take(1))
                {
                    Console.WriteLine($"{s.Key} med {Math.Round(s.Value,1)} grader");
                }
            }
        }        
    }
}
