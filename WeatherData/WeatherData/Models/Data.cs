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
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public string InOrOut { get; set; }
        public double Temp { get; set; }
        public double Humidity { get; set; }

        public static List<Data> WeatherDataInside { get; set; } = new List<Data>();
        public static List<Data> WeatherDataOutside { get; set; } = new List<Data>();


        public static void CreateWeatherDataList()
        {
            string[] weatherData = File.ReadAllLines("../../../Files/tempdata5-med fel.txt");
            //WeatherData.RemoveRange(0, WeatherData.Count());
            Regex regex = new("(?<year>\\d{4})-(?<month>\\d{2})-(?<day>\\d{2}) (?<hour>[0-2][0-9]):(?<min>[0-5][0-9]):(?<sec>[0-5][0-9]),(?<inOrOut>\\w{3,4}),(?<temp>\\-?\\d{1,2}\\.\\d),(?<hum>\\d{2})");
            foreach (var line in weatherData)
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    int year = int.Parse(match.Groups["year"].Value);
                    int month = int.Parse(match.Groups["month"].Value);
                    int day = int.Parse(match.Groups["day"].Value);
                    int hour = int.Parse(match.Groups["hour"].Value);
                    int min = int.Parse(match.Groups["min"].Value);
                    int sec = int.Parse(match.Groups["sec"].Value);
                    string inOrOut = match.Groups["inOrOut"].Value;
                    double temp = double.Parse(match.Groups["temp"].Value, System.Globalization.CultureInfo.InvariantCulture);
                    double hum = double.Parse(match.Groups["hum"].Value);

                    if (year == 2016 && month > 05 && month <= 12 && day > 0 && day <= 31 && hour < 24)
                    {
                        if (inOrOut == "Inne" || inOrOut == "inne" && temp > 16 && temp < 30 && hum > 10 && hum <= 100)
                        {
                            WeatherDataInside.Add(new Data()
                            {
                                Year = year.ToString(),
                                Month = month < 10 ? "0" + month.ToString() : month.ToString(),                           
                                Day = day < 10 ? "0" + day.ToString() : day.ToString(),
                                Hour = hour,
                                Minute = min,
                                Second = sec,
                                InOrOut = inOrOut,
                                Temp = temp,
                                Humidity = hum
                            });
                        }
                        else if (inOrOut == "Ute" || inOrOut == "ute" && temp > -35 && temp < 40 && hum > 10 && hum <= 100)
                        {
                            WeatherDataOutside.Add(new Data()
                            {
                                Year = year.ToString(),
                                Month = month < 10 ? "0" + month.ToString() : month.ToString(),
                                Day = day < 10 ? "0" + day.ToString() : day.ToString(),
                                Hour = hour,
                                Minute = min,
                                Second = sec,
                                InOrOut = inOrOut,
                                Temp = temp,
                                Humidity = hum
                            });
                        }
                    }
                }
            }
        }
        public static List<string> GetMonth(string expression, string inOrOut)
        {
            string[] weatherData = File.ReadAllLines("../../../Files/tempdata5-med fel.txt");
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