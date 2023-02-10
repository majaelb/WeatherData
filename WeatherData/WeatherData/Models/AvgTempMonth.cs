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
        private static double avgTemp;
        private static int avgHum;


        public static void AvgMonth()
        {
            string inside = "inne";
            string outside = "ute";
            List<Data> correctDateandPlace = new();
            Dictionary<string, double> avgTempMonth = new();
            Dictionary<string, double> avgHumMonth = new();
            var groupByMonth = weatherList
                            .GroupBy(x => x.Year + x.Month);

            foreach (var month in groupByMonth)
            {
                correctDateandPlace = weatherList
                                     .Where(d => (d.Year + d.Month)
                                     .Equals(month.Key))
                                     .ToList();

                double avgTemp = correctDateandPlace.Average(t => t.Temp);
                int avgHum = (int)correctDateandPlace.Average(t => t.Humidity);
                avgTempMonth.Add(month.Key, avgTemp);
                avgHumMonth.Add(month.Key, avgHum);
                //if (chosenCategory == "temp") dateAndAvg.Add(month.Key, avgTemp);
                //else if (chosenCategory == "hum") dateAndAvg.Add(month.Key, avgHum);
            }
            foreach (var month in avgTempMonth)
            {

                Console.WriteLine($"Medeltemperaturen för {month.Key} : {Math.Round(month.Value, 1)} grader ");

            }
            foreach (var month in avgHumMonth)
            {

                Console.WriteLine($"Medelluftfuktigheten för {month.Key} :{month.Value}%");

            }

        }
        //public static void AvgMonth()
        //{
        //    string chosenPlace = InputManager.GetPlace();
        //    string chosenCategory = InputManager.ChooseCategory();
        //    List<Data> correctDateandPlace = new();
        //    Dictionary<string, double> dateAndAvg = new();
        //    var groupByDay = weatherList
        //                    .GroupBy(x => x.Year + x.Month);

        //    foreach (var day in groupByDay)
        //    {
        //        correctDateandPlace = weatherList
        //                             .Where(d => (d.Year + d.Month)
        //                             .Equals(day.Key) && d.InOrOut == chosenPlace)
        //                             .ToList();

        //        double avgTemp = correctDateandPlace.Average(t => t.Temp);
        //        int avgHum = (int)correctDateandPlace.Average(t => t.Humidity);
        //        if (chosenCategory == "temp") dateAndAvg.Add(day.Key, avgTemp);
        //        else if (chosenCategory == "hum") dateAndAvg.Add(day.Key, avgHum);
        //    }
        //    foreach (var month in dateAndAvg)
        //    {
        //        Console.WriteLine("Medelvärde för " + month.Key + ": " + month.Value);
        //        //foreach(var day in month.Key)
        //        //if (month.Key.Count() > 30)
        //        //{
        //        //    Console.WriteLine("Denna månad är inte mätningarna fullständiga. Endast data från " + month.Key.Count());
        //        //}
        //    }
        //}
    }
}
