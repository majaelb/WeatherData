using System;
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
            string date = Validator.RegexCheck("Vilket datum vill du visa medelvärde för?(yyyyMMdd): ", "^\\d{8}$");


            foreach (var d in Data.WeatherDataInside.Where(d => d.Year+d.Month+d.Day == date))
            {
                Console.WriteLine(d.Year +" " +  d.Month +" " +d.Day);
                //Console.ReadKey();
            }
        }
    }
}
