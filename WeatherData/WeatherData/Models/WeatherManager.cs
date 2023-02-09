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
        private string chosenCategory;
        private Dictionary<string, double> dateAndAvg;
       
        public WeatherManager()
        {
        }

        public void Run()
        {
            chosenPlace = InputManager.GetPlace();
            chosenCategory = InputManager.ChooseCategory();
            dateAndAvg = GetAvg(chosenPlace, chosenCategory);         
            //TakeInput();
            //GetAvg(chosenPlace);
            Print(dateAndAvg);
        }
      
        public Dictionary<string, double> GetAvg(string chosenPlace, string chosenCategory)
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
                int avgHum = (int)correctDateandPlace.Average(t => t.Humidity);
                if (chosenCategory == "temp") dateAndAvg.Add(day.Key, avgTemp);               
                else if (chosenCategory == "hum") dateAndAvg.Add(day.Key, avgHum);
                //else if (chosenCategory == "mold") dateAndAvg.Add(day.Key, avgMold);

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
