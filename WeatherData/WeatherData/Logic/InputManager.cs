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
        internal static void GetAvg()
        {
            while (true)
            {
                string inOrOut = Validator.RegexCheck("Vill du se data för inne eller ute?: ", "^Inne|inne|Ute|ute$");
                if (inOrOut == null) return;
                string date = Validator.GetDate("Vilket datum vill du visa medelvärde för?(yyyyMMdd): ", "^\\d{8}$");
                if (date == null) return;
                List<double> chosenData = new();
                if (inOrOut == "Inne" || inOrOut == "inne")
                {
                    foreach (var d in Data.WeatherDataInside)
                    {
                        if (d.Year + d.Month + d.Day == date)
                        {
                            chosenData.Add(d.Temp);
                            chosenData.Add(d.Humidity);
                        }

                    }
                }
                else if (inOrOut == "Ute" || inOrOut == "ute")
                {

                    foreach (var d in Data.WeatherDataOutside)
                    {
                        if (d.Year + d.Month + d.Day == date)
                        {
                            chosenData.Add(d.Temp);
                            chosenData.Add(d.Humidity);
                        }

                    }
                }
                foreach (var d in chosenData)
                {
                    Console.WriteLine(d);
                }
            }


            //chosenData.Average();
        }
    }
}
