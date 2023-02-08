using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.Logic
{
    internal class InputManager
    {
        internal static void GetAvg(List<Data> weatherList)
        {
            while (true)
            {
                string inOrOut = Validator.RegexCheck("Vill du se data för inne eller ute? ", "^Inne|inne|Ute|ute$");
                if (inOrOut == null) return;
                string date = Validator.GetDate("Vilket datum vill du visa medelvärde för?(yyyyMMdd): ", "^\\d{8}$");
                if (date == null) return;

                List<Data> correctDateandPlace = weatherList
                                                .Where(d => (d.Year + d.Month + d.Day)
                                                .Equals(date) && d.InOrOut.ToLower() == inOrOut.ToLower())
                                                .ToList();
                double avgTemp = correctDateandPlace.Average(t => t.Temp);
                double avgHum = correctDateandPlace.Average(t => t.Humidity);

                Console.WriteLine($"Medeltemperatur för {date} är {Math.Round(avgTemp, 2)}.");
                Console.WriteLine($"Medelfuktigheten för {date} är {Math.Round(avgHum, 2)}.");
            }
        }

        internal static void GetAvgTest(List<Data> weatherList)
        {
            //TODO: För varje dag (där datumet är samma men tiden olika?) så ska medeltemperaturen räknas ut. Alla dagars medeltemp ska läggas i en lista          
            List<string> avgTemps = new();
            List<string> avgHums = new();
            string inOrOut = Validator.RegexCheck("Vill du se data för inne eller ute? ", "^Inne|inne|Ute|ute$");
            if (inOrOut == null) return;
            var groupByDay = weatherList.GroupBy(x => x.Year + x.Month + x.Day);
            foreach (var day in groupByDay)
            {
                //Console.WriteLine("Datum: " + day.Key);  //Each group has a key 
                List<Data> correctDateandPlace = weatherList
                                                  .Where(d => (d.Year + d.Month + d.Day)
                                                  .Equals(day.Key) && d.InOrOut == inOrOut.ToLower())
                                                  .ToList();
                double avgTemp = correctDateandPlace.Average(t => t.Temp);
                double avgHum = correctDateandPlace.Average(t => t.Humidity);
                avgTemps.Add(day.Key + " medeltemp: " + avgTemp);
                avgHums.Add(day.Key + avgHum);
            }
            
            foreach(var day in avgTemps)
            {
                Console.WriteLine(day);
            }

        }

        internal static void SortPerMonth(List<Data> weatherList)
        {
            //TODO:
            while (true)
            {
                string inOrOut = Validator.RegexCheck("Vill du se data för inne eller ute? ", "^Inne|inne|Ute|ute$");
                if (inOrOut == null) return;
                string yearMonth = Validator.GetDate("Ange år och månad som du vill se data för (yyMM)? ", "^\\d{4}$");
                if (yearMonth == null) return;

                foreach (var d in Data.WeatherData.Where(d => (d.Year + d.Month) == yearMonth))
                {

                }
                //List<Data> correctDateandPlace = weatherList
                //                                .Where(d => (d.Year + d.Month + d.Day)
                //                                .Equals(date) && d.InOrOut.ToLower() == inOrOut.ToLower())
                //                                .ToList();
                //double avgTemp = correctDateandPlace.Average(t => t.Temp);
                //double avgHum = correctDateandPlace.Average(t => t.Humidity);

                //Console.WriteLine($"Medeltemperatur för {yearMonth} är {Math.Round(avgTemp, 2)}.");
                //Console.WriteLine($"Medelfuktigheten för {yearMonth} är {Math.Round(avgHum, 2)}.");
            }
        }
    }
}
