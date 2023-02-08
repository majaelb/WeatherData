using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherData.Logic
{
    internal class Validator
    {
        /*Alla validators tar in en instruction, instället för en console writeline innan den anropas. */

        internal static string RegexCheck(string instruction, string pattern)
        {
            while (true)
            {
                Regex regex = new(pattern);
                string input = GetString(instruction);
                if (input == null) return null;
                MatchCollection matches = regex.Matches(input);

                if (matches.Any())
                {
                    return input;
                }
                else
                {
                    WrongInput("Felaktig inmatning");
                    if (ExitChoice())
                    {
                        return null;
                    }
                }
            }
        }

        
        internal static string GetDate(string instruction, string pattern, string inOrOut)
        {
            while (true)
            {
                string date = Validator.RegexCheck("Vilket datum vill du visa medelvärde för?(yyyyMMdd): ", "^\\d{8}$");
                if (date == null) return null;
                foreach (var d in Data.WeatherData)
                {
                    if (d.Year + d.Month + d.Day == date)
                    {
                        return date;
                    }
                }
                WrongInput("Datumet finns tyvärr inte i statistiken");
                if (Validator.ExitChoice())
                {
                    return null;
                }
            }
        }

        internal static string? GetString(string instruction)
        {
            while (true)
            {
                Console.Write(instruction);
                string? input = Console.ReadLine();
                if (input.Length > 0 || input != null)
                {
                    return input;
                }
                else
                {
                    Validator.WrongInput("Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return null;
                    }
                }
            }
        }

        internal static int GetIntInRange(string instruction, int lower, int upper)
        {
            while (true)
            {
                int input = GetInt(instruction);
                if (input == -1) return -1;

                if (input <= upper && input >= lower)
                {
                    return input;
                }
                else
                {
                    Validator.WrongInput("Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return -1;
                    }
                }
            }
        }

        internal static int GetInt(string instruction)
        {
            while (true)
            {
                Console.Write(instruction);
                string? input = Console.ReadLine();
                if (input != null && int.TryParse(input, out int number))
                {
                    return number;
                }
                else
                {
                    Validator.WrongInput("Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return -1;
                    }
                }
            }
        }

        internal static double GetDouble(string instruction)
        {
            while (true)
            {
                Console.Write(instruction);
                string? input = Console.ReadLine();
                if (input != null && double.TryParse(input, out double number))
                {
                    return number;
                }
                else
                {
                    Validator.WrongInput("Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return -1;
                    }
                }
            }
        }

        internal static int GetIntList(IEnumerable<int> validationList, string instruction)
        {
            while (true)
            {
                int input = GetInt(instruction);
                if (input == -1) return -1;
                if (validationList.Contains(input))
                {
                    return input;
                }
                else
                {
                    Validator.WrongInput("Felaktig inmatning");
                    if (Validator.ExitChoice())
                    {
                        return -1;
                    }
                }
            }
        }

        internal static void WrongInput(string instruction) //Text som visas vid felinmatning
        {
            Console.WriteLine(instruction + ", försök igen eller tryck <TAB> för att gå tillbaka");
        }

        internal static bool ExitChoice() //Möjliggör för användaren att ta sig ur en inmatning utan att fylla i resten
        {
            var key = Console.ReadKey(true).Key;
            return key == ConsoleKey.Tab;
        }
    }
}
