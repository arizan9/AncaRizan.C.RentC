using AncaRizan.C.RentC.Entities;
using AncaRizan.C.RentC.Helpers;
using AncaRizan.C.RentC.MenuOptions;
using Newtonsoft.Json;
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
                    UpdateCarRent.UpdateRental();
                    break;
                case "3":
                    ListRents();
                    break;
                case "4":
                    ListAvailableCars();
                    break;
                case "5":
                    AddAndUpdateCustomer.AddCustomer();
                    break;
                case "6":
                    AddAndUpdateCustomer.UpdateCustomer(); 
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

       
        private static void ListRents()
        {

            Console.WriteLine("List Rentals");
            using (var db = new RentCDb())
            {
                var query = (from r in db.Reservations
                             select r).ToArray();

                int nrRows = query.Count();
                string[] headers = { "CarPlate", "ClientID", "Start Date","End Date","Location" };

                string[][] rows = new string[query.Length][];
                int i = 0;

                foreach (var item in query)
                {
                    string[] row = new string[5];
                    row[0] = item.Car.Plate;
                    row[1] = item.CostumerID.ToString();
                    row[2] = item.StartDate.Date.ToString();
                    row[3] = item.EndDate.ToShortDateString();

                    var location = db.Locations.Find(item.LocationID).Name;
                    row[4] = location;

                    rows[i] = row;
                    i++;
                }
                DrawTable.DrawMyTable(headers, query.Length, rows);

            }
            Console.ReadKey();
            SelectOption();
        }

        private static void ListAvailableCars()
        {

            localhost.WebService1 proxy = new localhost.WebService1();
            var jsonAvailableCars = proxy.AvalialbleCars();

            Car[] availableCars = JsonConvert.DeserializeObject<List<Car>>(jsonAvailableCars).ToArray();

            using (var db = new RentCDb())
            {

                int nrRows = availableCars.Length;
                string[] headers = {"CarID","Plate","Manufacturer","Model","PricePerDay","Location"};

                string[][] rows = new string[availableCars.Length][];
                int i = 0;

                foreach (var item in availableCars)
                {
                    string[] row = new string[5];
                    row[0] = item.CarID.ToString();
                    row[1] = item.Plate;
                    row[2] = item.Manufacturer;
                    row[3] = item.Model;
                    row[3] = item.PricePerDay.ToString();
                    row[3] = db.Locations.Find(item.LocationID).Name;

                    var location = db.Locations.Find(item.LocationID).Name;
                    row[4] = location;

                    rows[i] = row;
                    i++;
                }
                DrawTable.DrawMyTable(headers, availableCars.Length, rows);
                SelectOption();
            }
        }
        private static void ListCustomer()
        {
            Console.WriteLine("List Cusomers");
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
                    row[2] = item.BirthDate.Date.ToString();
                    rows[i] = row;
                    i++;
                }
                DrawTable.DrawMyTable(headers, query.Length, rows);

            }       
            Console.ReadKey();
            SelectOption();
        }
    }
}

