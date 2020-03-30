using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncaRizan.C.RentC
{
    class ValidateUserInput
    {
        public static int ValidateInputInt(String input)
        {
            int validInputInt;
            while (!int.TryParse(input, out validInputInt))
            {
                Console.Write("Please enter a number");
                input = Console.ReadLine();
            }
            return validInputInt;
        }

        public static DateTime ValidateInputDate(String input)
        {
            DateTime validInputDate;
            while (!DateTime.TryParse(input, out validInputDate))
            {
                Console.Write("Please enter a valid date format (mm.dd.yyyy)");
                input = Console.ReadLine();
            }
            return validInputDate;
        }
    }

}
