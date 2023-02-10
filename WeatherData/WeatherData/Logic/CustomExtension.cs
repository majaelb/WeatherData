using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.Logic
{
    public static class CustomExtension
    {
        public static void Cw(this string input)
        {
            Console.WriteLine(input);
        }
    }
}
