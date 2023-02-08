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
        public int Humidity { get; set; }

        public static List<Data> WeatherData { get; set; } = new List<Data>();

        public static List<Data> CreateOneWeatherDataList()
        {
            string[] weatherData = File.ReadAllLines("../../../Files/tempdata5-med fel.txt");
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
                    int hum = int.Parse(match.Groups["hum"].Value);

                    if (year == 2016 && month > 05 && month <= 12 && day > 0 && day <= 31 && hour < 24 && (inOrOut == "Inne" || inOrOut == "inne" && temp > 16 && temp < 30 && hum > 10 && hum <= 100) || (inOrOut == "Ute" || inOrOut == "ute" && temp > -35 && temp < 40 && hum > 10 && hum <= 100))
                    {
                        WeatherData.Add(new Data()
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
            return WeatherData;
        }
    }
}