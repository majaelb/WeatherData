using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Logic;

namespace WeatherData.Models
{
    internal class FallOrWinter
    {
        private static readonly List<Data> weatherList = Data.CreateOneWeatherDataList();

        public static void Run()
        {
            List<Data> correctDateandPlace = new();
            Dictionary<string, double> dateAndAvg = new();
            var groupByDay = weatherList
                            .GroupBy(x => x.Year + x.Month + x.Day);

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

            //Hur sortera för att jämföra fem datum i rad?
            //Dessa funkar, men mest för att det är tur att datumen låg i rad?
            var fallResult = dateAndAvg
                 .Where(t => t.Value < 10 && int.Parse(t.Key) > int.Parse("20160801"))
                 .ToList()
                 .Take(1);
            foreach (var r in fallResult)
            {
                Console.WriteLine("Hösten kom den: " + r.Key);
            }

            var winterResult = dateAndAvg
                .Where(t => t.Value < 0)
                .ToList()
                .Take(1);

            foreach(var r in winterResult)
            {
                Console.WriteLine("Vintern kom den: " + r.Key);
            }


            //Sorterar endast på datum
            //foreach (var item in dateAndAvg)
            //{
            //    Console.WriteLine(item.Key + " medelvärde: " + Math.Round(item.Value, 2));
            //}


            //Test för att jämföra sista siffran i datumet på något vis. Funkar ej..
            //string date;
            //foreach (var r in result)
            //{
            //    date = r.Key;
            //    string v = date.Last().ToString();
            //    int lastnr = int.Parse(v);
            //    if(r.Key.Last().ToString() == (lastnr.ToString()))
            //    Console.WriteLine(r.Key + " " + r.Value);
            //    lastnr++;
            //}

            //Test från nätet
            //var duplicates = dateAndAvg
            //    .GroupBy(i=>i.Value < 10)
            //    .Where(g => g.Count() > 5)
            //    .Select(g => g.Key) .ToList();
            //foreach (var item in duplicates)
            //{
            //    Console.WriteLine(item);
            //}


        }



    }
}
