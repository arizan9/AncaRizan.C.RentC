using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncaRizan.C.RentC.Helpers
{
    static class DrawTable
    {
      
        public static void  DrawMyTable(String[] columns, int count, String[][] rows)
        {
            Console.Clear();
            PrintLine();
            PrintRow(columns);
            PrintLine();
            for (int i = 0; i < count; i++)
            {
                PrintRow(rows[i]);
            }
            PrintLine();
            Console.ReadLine();
        }

        static void PrintLine()
        {
            Console.WriteLine(new string('-', 75));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (75 - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

    }
}
