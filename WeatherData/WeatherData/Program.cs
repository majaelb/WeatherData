using WeatherData.Logic;

namespace WeatherData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ReadTempFile.GetYearData();
            Data.CreateWeatherDataList();
            InputManager.GetAvg();
        }
    }
}