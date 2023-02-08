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

        internal static void GetAvgTest(List<Data> weatherList, string inOrOut)
        {
            //TODO: För varje dag (där datumet är samma men tiden olika?) så ska medeltemperaturen räknas ut. Alla dagars medeltemp ska läggas i en lista
            foreach (var day in weatherList)
            {   
                //while(day.Day == weatherList.)
                List<Data> correctDateandPlace = weatherList
                                                .Where(d => (d.Year + d.Month + d.Day)
                                                .Equals(day.Year + day.Month + day.Day) && d.InOrOut.ToLower() == inOrOut.ToLower())
                                                .ToList();
                double avgTemp = correctDateandPlace.Average(t => t.Temp);
                double avgHum = correctDateandPlace.Average(t => t.Humidity);
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
