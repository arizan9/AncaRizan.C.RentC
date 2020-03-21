using AncaRizan.C.RentC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncaRizan.C.RentC
{
    static class ReservationManagement 
    {

        public static (DateTime, DateTime)  ValidateDates(DateTime startDate,DateTime endDate)
        {
            while (startDate.CompareTo(endDate) > 0)
            {
                Console.WriteLine("The end date is sooner then the start date, plase enter a new period" +
                       "\n or type quit to go to main menu!");
                Console.WriteLine("StartDate:");
                var ans = Console.ReadLine();
                if (ans == "quit")
                {
                    MenuPage.SelectOption();
                }
                else
                {
                    startDate = ValidateUserInput.ValidateInputDate(Console.ReadLine());
                }
                Console.WriteLine("StartDate:");
                ans = Console.ReadLine();
                if (ans == "quit")
                {
                    MenuPage.SelectOption();
                }
                else
                {
                    endDate = ValidateUserInput.ValidateInputDate(Console.ReadLine());
                }

            }

            return (startDate, endDate);
        }


        public static int ValidateClient(int id)
        {
            using (var db = new RentCDb())
            {
                while (!db.Customers.Any(c => c.CostumerID == id))
                {
                    Console.WriteLine("Client doesn't exist! Enter an existing client" +
                        "\n or type quit to go to main menu!");
                    Console.WriteLine("Client:");
                    var ans = Console.ReadLine();
                    if (ans == "quit")
                    {
                        MenuPage.SelectOption();
                    }
                    else
                    {
                        id = ValidateUserInput.ValidateInputiInt(Console.ReadLine());
                    }
                }
                return id;
            }
       
        }

        public static int ValidateCar(string carPlate)
        {
            using (var db = new RentCDb())
            {
                while (!db.Cars.Any(c => c.Plate == carPlate))
                {
                    Console.WriteLine("Car doesn't exist! Enter an existing car" +
                        "\n or type quit to go to main menu!");
                    Console.WriteLine("Car Plate:");
                    var ans = Console.ReadLine();
                    if (ans == "quit")
                    {
                        MenuPage.SelectOption();
                    }
                    else
                    {
                        carPlate = ans;
                    }
                }
                var stringID = from c in db.Cars
                               where c.Plate == carPlate
                               select c.CarID;

                return stringID.FirstOrDefault();
               
            }
            
        }

        public static int ValidateCarLocation(int id, String location)
        {
            using (var db = new RentCDb())
            {
                var query = from l in db.Locations
                            where l.Name == location
                            select l;
                var loc = query.First().LocationID;

                var carLocation = (from c in db.Cars
                                   where c.CarID == id
                                   select c).First().LocationID;

                while (!(carLocation== loc))
                {
                    Console.WriteLine("The car is not in the same location Please select another car" +
                        "\n or type quit to go to main menu!");

                    Console.WriteLine("Car Plate:");
                    var ans = Console.ReadLine();
                    if (ans == "quit")
                    {
                        MenuPage.SelectOption();
                    }
                    else
                    {
                        ValidateCar(Console.ReadLine());
                    }
                }

                var locationID = from l in db.Locations
                                 where l.Name == location
                                 select l;
                return locationID.First().LocationID;
            }
        }

        static public List<Nullable<DateTime>> IsCarAvailabe(int carId, DateTime startDate, DateTime endDate)
        {
            List<Nullable<DateTime>> carReserved = new List<Nullable<DateTime>>();
            using (var db = new RentCDb())
            {
                var query = from r in db.Reservations
                            where ((r.StartDate == startDate) && (r.EndDate == endDate) && (r.CarID == carId))
                            select r;

                foreach (var item in query)
                {
                    if (item.ReservStatsID!=1)
                    {
                        carReserved.Add(item.StartDate);
                    }
                }
            }
            return carReserved;
        }

        public static void DisplayReservationInfo(Reservation reservation)
        {
            Car car; Location location;

            using (var db = new RentCDb())
            {
                car = db.Cars.Find(reservation.CarID);
                location = db.Locations.Find(reservation.LocationID);
            }
                    
            Console.WriteLine("Car Plate: "+ car.Plate);
            Console.WriteLine("Client ID: " + reservation.CostumerID);
            Console.WriteLine("Start Date: " + reservation.StartDate);
            Console.WriteLine("End Date: " + reservation.EndDate);
            Console.WriteLine("Location: " + location.Name);
        }   
    }
}
