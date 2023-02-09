using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Logic;

namespace WeatherData.Models
{
    internal class SeasonStart
    {
        private static readonly List<Data> weatherList = Data.CreateOneWeatherDataList();
        private static Dictionary<string, double> ? dateAndAvg;
        private static Dictionary<string, double> ? autumnStart;
        private static Dictionary<string, double> ? winterStart;
        public static void RunTest()
        {
            int maxTempAutumn = 10;
            //ändrade temperaturen till 1 för att få ut data. Vintern visades inte p.g.a 0 inte sker 5 dagar i rad.
            //borde gå att ändra vid getSeasonstart
            int maxTempWinter = 1;
            dateAndAvg = GetAvgTemperatures();
            //seasonStart = GetSeasonStart();
            autumnStart = GetSeasonStart(maxTempAutumn);
            winterStart = GetSeasonStart(maxTempWinter);
            Print();
        }
        public static Dictionary<string, double> GetAvgTemperatures()
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
            Dictionary<string, double> seasonStart = new Dictionary<string, double>();
            int count = 0;

            foreach (var item in sortedDateAndAvg)
            {
                if (item.Value <= maxTemp)
                {
                    count++;
                    seasonStart.Add(item.Key, item.Value);
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
        //Fungerar ej hämtar inte data.
        //public static Dictionary<string, double> GetSeasonStart(double maxTemp)
        //{
        //    var sortedDateAndAvg = dateAndAvg
        //                           .OrderBy(x => x.Key)
        //                           .TakeWhile((t, index) => t.Value <= maxTemp && (index + 1) % 5 != 0)
        //                           .ToDictionary(x => x.Key, x => x.Value);

        //    return sortedDateAndAvg;
        //}

        public static void Print()
        {
            if (autumnStart.Count == 5)
            {
                Console.Write("Hösten anlände: ");
                foreach (var s in autumnStart.Take(1))
                {
                    Console.WriteLine($"{s.Key} med {Math.Round(s.Value,2)} grader");
                }
            }
            if (winterStart.Count == 5)
            {
                Console.Write("Vintern anlände: ");
                foreach (var s in winterStart.Take(1))
                {
                    Console.WriteLine($"{s.Key} med {Math.Round(s.Value,2)} grader");
                }
            }
        }
        
        //public static void Run()
        //{

        //    List<Data> correctDateandPlace = new();
        //    Dictionary<string, double> dateAndAvg = new();
        //    var groupByDay = weatherList
        //                    .GroupBy(x => x.Year + x.Month + x.Day);

        //    foreach (var day in groupByDay)
        //    {
        //        correctDateandPlace = weatherList
        //                             .Where(d => (d.Year + d.Month + d.Day)
        //                             .Equals(day.Key) && d.InOrOut == "ute")
        //                             .ToList();

        //        double avgTemp = correctDateandPlace.Average(t => t.Temp);
        //        int avgHum = (int)correctDateandPlace.Average(t => t.Humidity);
        //        dateAndAvg.Add(day.Key, avgTemp);
        //    }


        //    //Hur sortera för att jämföra fem datum i rad?
        //    //Dessa funkar, men mest för att det är tur att datumen låg i rad?
        //    var fallResult = dateAndAvg
        //         .Where(t => t.Value < 10 && int.Parse(t.Key) > int.Parse("20160801"))
        //         .ToList()
        //         .Take(1);
        //    foreach (var r in fallResult)
        //    {
        //        Console.WriteLine("Hösten kom den: " + r.Key);
        //    }

        //    var winterResult = dateAndAvg
        //        .Where(t => t.Value < 0)
        //        .ToList()
        //        .Take(1);

        //    foreach (var r in winterResult)
        //    {
        //        Console.WriteLine("Vintern kom den: " + r.Key);
        //    }


        //    //Sorterar endast på datum
        //    //foreach (var item in dateAndAvg)
        //    //{
        //    //    Console.WriteLine(item.Key + " medelvärde: " + Math.Round(item.Value, 2));
        //    //}

    }
}
