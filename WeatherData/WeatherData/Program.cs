using WeatherData.Logic;

namespace WeatherData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Data> weatherlist = Data.CreateOneWeatherDataList();
            InputManager.GetAvgTest(weatherlist);
            //Menu.Run();
            
        }
    }
}