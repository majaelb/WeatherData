using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Logic;

namespace WeatherData.Models
{
    internal class FileManager
    {
        private static readonly List<Data> weatherList = Data.CreateOneWeatherDataList();
        private static double avgTemp;
        private static int avgHum;

        private static string path = "../../../Files/";
        private static string filename = "statistics.txt";

        public static void AvgMonth()
        {
            string chosenPlace = InputManager.GetPlace();
            List<Data> correctDateandPlace = new();
            List<string> avgTempMonth = new();
            List<string> avgHumMonth = new();
            var groupByMonth = weatherList
                            .GroupBy(x => x.Year + x.Month);

            string path = "../../../Files/";
            string filename = "statistics.txt";

            if (chosenPlace == "inne")
            {
                avgTempMonth.Add("Medeltemperatur inomhus");
                avgHumMonth.Add("Medelluftfuktighet inomhus");
            }
            else if (chosenPlace == "ute")
            {
                avgTempMonth.Add("Medeltemperatur utomhus");
                avgHumMonth.Add("Medelluftfuktighet utomhus");
            }
            foreach (var month in groupByMonth)
            {
                correctDateandPlace = weatherList
                                     .Where(d => (d.Year + d.Month)
                                     .Equals(month.Key) && d.InOrOut == chosenPlace)
                                     .ToList();

                string avgTemp = Math.Round(correctDateandPlace.Average(t => t.Temp), 1).ToString();
                string avgHum = Math.Round(correctDateandPlace.Average(t => t.Humidity)).ToString();

                avgTempMonth.Add(month.Key + " " + avgTemp);
                avgHumMonth.Add(month.Key + " " + avgHum);
            }
            if (!File.Exists(path + filename))
            {
                File.WriteAllLines(path + filename, avgTempMonth);

            }
            else 
            {
                File.AppendAllLines(path + filename, avgTempMonth);
            }
            File.AppendAllLines(path + filename, avgHumMonth);
        }
    }
}
