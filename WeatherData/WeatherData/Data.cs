using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherData
{
    internal class Data
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        //time-props?
        public string InOrOut { get; set; }
        public string Temp { get; set; }
        public int Humidity { get; set; }

        public static List<Data> WeatherData { get; set; } = new List<Data>();


        public static void ReadFile()
        {
            string[] weatherData = File.ReadAllLines("../../../TempData/tempdata5-med fel.txt");
            //string text = "2016-06-01";
            //string pattern = "(\\d{4})-(?<month>\\d{2})-(\\d{2}).+,(?<inOrOut>\\w{3,4}),(?<temp>\\d{1,2}\\.\\d),(?<hum>\\d{2})";
            //List<string> validData = new();
            //foreach (Match m in Regex.Matches(weatherData, pattern).Cast<Match>())
            //{
            //    if (m.Groups.Count > 0)
            //    {
            //        foreach (Group g in m.Groups)
            //        {
            //            //if(g.Name.Equals(1) && g.Value.Equals(2016))
            //            Console.WriteLine(g.Name + " : " + g.Value);

            //        }
            //    }
            //}
            //WeatherData.RemoveRange(0, WeatherData.Count());
            Regex regex = new("(?<year>\\d{4})-(?<month>\\d{2})-(?<day>\\d{2}).+,(?<inOrOut>\\w{3,4}),(?<temp>\\d{1,2}\\.\\d),(?<hum>\\d{2})");
            foreach (var line in weatherData)
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    if (int.Parse(match.Groups["year"].Value) == 2016 && int.Parse(match.Groups["month"].Value) > 05)
                    {
                        WeatherData.Add(new Data()
                        {
                            Year = int.Parse(match.Groups["year"].Value),
                            Month = int.Parse(match.Groups["month"].Value),
                            Day = int.Parse(match.Groups["day"].Value),
                            InOrOut = match.Groups["inOrOut"].Value,
                            Temp = match.Groups["temp"].Value,
                            Humidity = int.Parse(match.Groups["hum"].Value)
                        });
                    }
                }
            }
            foreach (var w in WeatherData)
            {
                Console.WriteLine(w.Year + " " + w.Month + " " + w.Day + " " + w.InOrOut + " " + w.Temp + " " + w.Humidity);
                Console.ReadKey();
            }
        }
        public static List<string> GetMonth(string expression, string inOrOut)
        {
            string[] weatherData = File.ReadAllLines("../../../TempData/tempdata5-med fel.txt");
            List<string> month = new();
            Regex regex = new(expression); //"^2016-06.+"
            MatchCollection matches;
            foreach (string line in weatherData.Where(l => l.Contains(inOrOut)))
            {
                matches = regex.Matches(line);
                foreach (Match match in matches.Cast<Match>())
                {
                    month.Add(match.Value);
                }
            }
            return month;
        }
    }
}

