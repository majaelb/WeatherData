using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.Logic
{
    internal class Delegates
    {
        public delegate string MyDelegate(string input, string path);

        public static string SaveToFile(string text, string path)
        {
            File.AppendAllText(path, text + Environment.NewLine);
            return text;
        }
    }
}
