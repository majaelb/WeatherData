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
        public static string GetPlace()
        {
            int lower = (int)Enums.PlaceOption.Inne;
            int upper = (int)Enums.PlaceOption.Ute;

            int inOrOut = Validator.GetIntInRange("Vill du se data för [1] = inne eller [2] = ute: ", lower, upper);
            if (inOrOut == -1) return null;
            
            string chosenPlace;
            if (inOrOut == lower)
            {
                return "inne";
            }
            else if (inOrOut == upper)
            {
                return "ute";
            }             
            return null;
        }
        public static string GetCategory()
        {
            int lower = (int)Enums.Category.Temp;
            int upper = (int)Enums.Category.Humidity;
            //int upper = (int)Enums.Category.Mold;

            int category = Validator.GetIntInRange("Vill du se data för [1] = temperatur eller [2] = luftfuktighet: ", lower, upper);
            if (category == -1) return null;

            if (category == lower)
            {
                return "temp";
            }
            else if (category == upper)
            {
                return "hum";
            }
            //else if (category == upper)
            //{
            //    return "mold";
            //}
            return null;

        }
    }
}
