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
        public double Temp { get; set; }
        public int Humidity { get; set; }



        public static void ReadFile()
        {
            string weatherData = File.ReadAllText("../../../TempData/tempdata5-med fel.txt");
            //string text = "2016-06-01";
            string pattern = "(\\d{4})-(?<month>\\d{2})-(\\d{2}).+,(?<inOrOut>\\w{3,4}),(?<temp>\\d{1,2}\\.\\d),(?<hum>\\d{2})";
            List<string> validData = new();
            foreach (Match m in Regex.Matches(weatherData, pattern).Cast<Match>())
            {

                if (m.Groups.Count > 0)
                {

                    foreach (Group g in m.Groups)
                    {
                        //if(g.Name.Equals(1) && g.Value.Equals(2016))
                        Console.WriteLine(g.Name + " : " + g.Value);

                    }
                }
            }

            //Match match = regex.Match(time);
            //Console.Write(time + ": ");
            //if (match.Success)
            //{
            //    int hour = int.Parse(match.Groups["hour"].Value);
            //    if (hour < 24) // IsValidTime(match)
            //    {
            //        Console.WriteLine("Helt korrekt datum");
            //    }
            //}
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

