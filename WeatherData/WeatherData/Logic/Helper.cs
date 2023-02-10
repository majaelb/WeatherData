using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherData.Logic
{
    internal class Helper
    {
        internal static void ActiveChoice(string choice) //Skriver ut var på sidan man befinner sig efter ett knappval i en meny
        {
            Console.WriteLine("Aktivt val:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(choice);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        public static void TimeCount(TimeSpan ts)
        {
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                               ts.Hours, ts.Minutes, ts.Seconds,
                                               ts.Milliseconds / 10);
            Console.SetCursorPosition(70, 0);
            Console.WriteLine("RunTime " + elapsedTime);
        }

    }
}
