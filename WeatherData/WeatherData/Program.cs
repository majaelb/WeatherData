using WeatherData.Logic;
using WeatherData.Models;

namespace WeatherData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<Data> weatherlist = Data.CreateOneWeatherDataList();
            //InputManager.GetAvgTest(weatherlist);
            //Menu.Run();
            //AvgTempMonth.AvgMonth();
            SortedMoldRisk sortedMoldRisk = new();
            sortedMoldRisk.Run();
        }
    }
}