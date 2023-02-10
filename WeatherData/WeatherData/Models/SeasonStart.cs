using Microsoft.VisualBasic;
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

        private static string path = "../../../Files/";
        private static string filename = "statistics.txt";

        public void Run()
        {
            Delegates.MyDelegate del = Delegates.SaveToFile;
            int maxTempAutumn = 10;
            //ändrade temperaturen till 1 för att få ut data. Vintern visades inte p.g.a 0 inte sker 5 dagar i rad.
            //borde gå att ändra vid getSeasonstart
            int maxTempWinter = 1;
            Stopwatch stopWatch = new();
            stopWatch.Start();
            dateAndAvg = GetAvg();
            autumnStart = GetSeasonStart(maxTempAutumn);
            winterStart = GetSeasonStart(maxTempWinter);
            Print();
            //WriteToFile(del);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Helper.TimeCount(ts);
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

                double avgTemp = Math.Round(correctDateandPlace.Average(t => t.Temp),1);
                int avgHum = (int)Math.Round(correctDateandPlace.Average(t => t.Humidity),1);
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
        public void WriteToFile(Delegates.MyDelegate del)
        {
            
            string autumn = "";
            string winter = "";

            foreach(var item in autumnStart.Take(1))
            {
                autumn = "Hösten anlände "+ item.Key + " med temperaturen " + item.Value.ToString();               
            }
            foreach(var item in winterStart.Take(1))
            {
                winter = "Vintern anlände nästan "+ item.Key + " med temperaturen " + item.Value.ToString();
            }
            del(path + filename, autumn);
            del(path + filename, winter);
        }
    }
}
