using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncaRizan.C.RentC
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstPage();
            MenuPage.DisplayMenu();
            MenuPage.SelectOption();
        }

        public static void FirstPage()
        {
            String fp= "Wellcome to RentC, your brand new solution to \nmanage and control your company's data" +
                "\nwithout missing anything \n\n\n\n\n\n\n\n\n Press ENTER to continue or ESC to quit";
            Console.WriteLine(fp); ;
            var answer=Console.ReadKey();

            switch (answer.Key)
            {
                case ConsoleKey.Escape: Environment.Exit(0);
                    break;
                case ConsoleKey.Enter: MenuPage.SelectOption();
                    break;
            }

        }
    }
}
