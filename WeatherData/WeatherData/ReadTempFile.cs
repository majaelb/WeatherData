using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherData
{
    internal class ReadTempFile
    {
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

        public static void GetYearData()
        {
            List<string> weatherData = File.ReadAllLines("../../../TempData/tempdata5-med fel.txt").ToList();

            foreach (var item in weatherData.Where(x => x.StartsWith("2016-06-01")))
            {
                Console.WriteLine(item);
            }





            List<string> juneInside = GetMonth("^2016-06.+", "Inne");
            List<string> juneOutside = GetMonth("^2016-06.+", "Ute");
            List<string> allOfJune = new();
            foreach (var item in juneInside)
            {
                allOfJune.Add(item);
            }
            foreach (var item in juneOutside)
            {
                allOfJune.Add(item);
            }
            foreach (var item in allOfJune)
            {
                Console.WriteLine(item);
            }
        }
    }
}
