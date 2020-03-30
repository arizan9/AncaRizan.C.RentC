using AncaRizan.C.RentC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncaRizan.C.RentC.MenuOptions
{
    static class AddAndUpdateCustomer
    {
        public static void AddCustomer()
        {
            Console.Write("Enter Cusomer Name: ");
            var customerName = Console.ReadLine();
            Console.Write("Enter customer Birth Date: ");
            var customerBirthDate = ValidateUserInput.ValidateInputDate(Console.ReadLine());

            Customer customer = new Customer
            {
                Name = customerName,
                BirthDate = customerBirthDate
            };

            using (var db = new RentCDb())
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            Console.WriteLine("Customer saved:");
            DisplayCustomer(customer);
            Console.ReadLine();

            MenuPage.SelectOption();

        }

        private static void DisplayCustomer(Customer customer)
        {
            Console.WriteLine("Customer ID: " + customer.CostumerID);
            Console.WriteLine("Customer Name: " + customer.Name);
            Console.WriteLine("Customer Birthdate : " + customer.BirthDate);
        }

        public static void UpdateCustomer()
        {
            Console.Write("Please enter customerID :");
            var customerID = ValidateUserInput.ValidateInputInt(Console.ReadLine());

            using (var db = new RentCDb())
            {
                while (!db.Customers.Any(c => c.CostumerID == customerID))
                {
                    Console.WriteLine("Please enter an existing customer");
                    customerID = ValidateUserInput.ValidateInputInt(Console.ReadLine());
                }

                Customer customer = db.Customers.Find(customerID);
                DisplayCustomer(customer);
            Options:
                Console.WriteLine("Which field do you want to update?");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. BirthDate");

             
                var ans = Console.ReadLine();
                switch (ans)
                {
                    case "1":
                        Console.Write("Enter Cusomer Name: ");
                        customer.Name = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Enter customer Birth Date: ");
                        customer.BirthDate = ValidateUserInput.ValidateInputDate(Console.ReadLine());
                        break;
                    default:
                        Console.WriteLine("This is not a valid option!");
                        goto Options;
                }

                Console.WriteLine("New Reservation Data:");
                DisplayCustomer(customer);
                Console.WriteLine("Would you like to modify something else? \n" +
                    "If yes, type YES if no type NO to go back to main menu");
                ans = Console.ReadLine();
                if (ans == "NO")
                {
                    MenuPage.SelectOption();
                }
                else if (ans == "YES")
                {
                    goto Options;
                }

                db.SaveChanges();
                Console.WriteLine("Customer saved:");
                DisplayCustomer(customer);
                Console.ReadLine();
                MenuPage.SelectOption();
            }

        }
    }
}
