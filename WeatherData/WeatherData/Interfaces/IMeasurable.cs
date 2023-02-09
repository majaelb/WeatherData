using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.Interfaces
{
    interface IMeasurable
    {
        void Run();
        Dictionary<string,double> GetAvg(string chosenPlace, string chosenCategory);
        void Print(Dictionary<string, double> dateAndAvg);
        
    }
}
