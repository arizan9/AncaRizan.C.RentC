using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncaRizan.C.RentC
{
    class ValidateUserInput
    {
        public static int ValidateInputiInt(String input)
        {
            int validInputInt;
            while (!int.TryParse(input, out validInputInt))
            {
                Console.WriteLine("Please enter a number");
                input = Console.ReadLine();
            }
            return validInputInt;
        }

        public static DateTime ValidateInputDate(String input)
        {
            DateTime validInputDate;
            while (!DateTime.TryParse(input, out validInputDate))
            {
                Console.WriteLine("Please enter a date");
                input = Console.ReadLine();
            }
            return validInputDate;
        }
    }

}
