using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Interfaces;
using WeatherData.Logic;

namespace WeatherData.Models
{
    internal class WeatherManager : IMeasurable
    {
        private List<Data> weatherList = Data.CreateOneWeatherDataList();

        private string chosenPlace;
        private Dictionary<string, double> dateAndAvg;
       
        public WeatherManager()
        {
        }

        public void Run()
        {
            chosenPlace = TakeInput();
            dateAndAvg = GetAvg(chosenPlace);
            //TakeInput();
            //GetAvg(chosenPlace);
            Print(dateAndAvg);
        }
        public string TakeInput()
        {
            int lower = (int)Enums.PlaceOption.Inne;
            int upper = (int)Enums.PlaceOption.Ute;

            int inOrOut = Validator.GetIntInRange("Vill du se data för [1] = inne eller [2] = ute :", lower, upper);
            if (inOrOut == -1) return null;

            string chosenPlace;
            if (inOrOut == lower)
            {
                return "inne";
            }
            else if (inOrOut == upper)
            {
                return "ute";
            }
            return null;
            //if (chosenPlace == null) return null;
            
        }
        public Dictionary<string, double> GetAvg(string chosenPlace)
        {
            //string chosenPlace = TakeInput();
            if (chosenPlace == null) return null;
            List<Data> correctDateandPlace = new();
            Dictionary<string, double> dateAndAvg = new();
            var groupByDay = weatherList
                            .GroupBy(x => x.Year + x.Month + x.Day);

            foreach (var day in groupByDay)
            {
                correctDateandPlace = weatherList
                                     .Where(d => (d.Year + d.Month + d.Day)
                                     .Equals(day.Key) && d.InOrOut == chosenPlace)
                                     .ToList();

                double avgTemp = correctDateandPlace.Average(t => t.Temp);
                double avgHum = correctDateandPlace.Average(t => t.Humidity);
                dateAndAvg.Add(day.Key, avgTemp);
                //TODO: Fråga om humidity/temp och gör ternary i dateandavg.add?
            }

            return dateAndAvg;
        }

        public void Print(Dictionary<string, double> dateAndAvg)
        {
            foreach (var item in dateAndAvg.OrderByDescending(t => t.Value))
            {
                Console.WriteLine(item.Key + " medeltemp: " + item.Value);
            }
        }
    }
}
