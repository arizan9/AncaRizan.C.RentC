using AncaRizan.C.RentC.Entities;
using AncaRizan.C.RentC.Helpers;
using AncaRizan.C.RentC.MenuOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncaRizan.C.RentC
{
    static class MenuPage
    {

        public static String DisplayMenu()
        {
            Console.WriteLine("RentC Menu");
            Console.WriteLine();
            Console.WriteLine("1. Register new Car Rent");
            Console.WriteLine("2. Update Car Rent");
            Console.WriteLine("3. List Rents");
            Console.WriteLine("4. List Available Cars");
            Console.WriteLine("5. Register new Customer ");
            Console.WriteLine("6. Update Customer ");
            Console.WriteLine("7. List Customer ");
            Console.WriteLine("8. Exit");

            return Console.ReadLine();

        }

        public static void SelectOption()
        {
            Console.Clear();
            var answer = DisplayMenu();

            switch (answer)
            {
                case "1":
                    RegisterNewCarRent.DisplayScreen();
                    break;
                case "2":
                    UpdateCarRent();
                    break;
                case "3":
                    ListRents();
                    break;
                case "4":
                    ListAvailableCars();
                    break;
                case "5":
                    RegisterNewCustomer();
                    break;
                case "6":
                    UpdateCustomer();
                    break;
                case "7":
                    ListCustomer();
                    break;
                case "8":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Enter a valid number!");
                    System.Threading.Thread.Sleep(700);
                    SelectOption();
                    break;
            }
            Console.Clear();
        }


        private static void UpdateCarRent()
        {
            throw new NotImplementedException();
        }

        private static void ListRents()
        {

            //using (var db = new RentCContext())
            {


            }
            SelectOption();
        }

        private static void ListAvailableCars()
        {
            Console.WriteLine("ListAvailableCars");
            SelectOption();
        }

        private static void RegisterNewCustomer()
        {
            Console.WriteLine("Register New Customer");
            SelectOption();
        }

        private static void UpdateCustomer()
        {
            //TO DO: implement UpdateCustomer
            Console.WriteLine("Update Cusomer");
            SelectOption();
        }
        private static void ListCustomer()
        {
            Console.WriteLine("List Cusomer");
            using (var db = new RentCDb())
            {
                var query = (from c in db.Customers
                             select c).ToArray();

                int nrRows = query.Count();
                string[] headers = { "CustomerID", "Name", "Birt Date" };

                string[][] rows = new string[query.Length][];
                 int i = 0;

                foreach (var item in query)
                {
                    string[] row = new string[3];
                    row[0] = item.CostumerID.ToString();
                    row[1] = item.Name.ToString();
                    row[2] = item.BirthDate.ToString();
                    rows[i] = row;
                    i++;
                }



                DrawTable.DrawMyTable(headers, query.Length, rows);

            }       
            Console.ReadKey();
            System.Threading.Thread.Sleep(500);
            SelectOption();
        }
    }
}

