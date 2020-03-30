using AncaRizan.C.RentC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncaRizan.C.RentC.MenuOptions
{
    public static class RegisterNewCarRent
    {
        public static void DisplayScreen()
        {
            Reservation reservation = new Reservation();

            Console.Write("Car Plate:");
            var carPlate = Console.ReadLine();
            var carID = ReservationManagement.ValidateCar(carPlate);
            reservation.CarID = carID;


            Console.Write("Client ID:");
            var clientID = ValidateUserInput.ValidateInputInt(Console.ReadLine());
            reservation.CostumerID = ReservationManagement.ValidateClient(clientID);


            Console.Write("Start Date(mm.dd.yyyy):");
            var startDate = ValidateUserInput.ValidateInputDate(Console.ReadLine());
            Console.Write("End Date(mm.dd.yyyy):");
            var endDate = ValidateUserInput.ValidateInputDate(Console.ReadLine());

            reservation.StartDate = ReservationManagement.ValidateDates(startDate, endDate).Item1;
            reservation.EndDate = ReservationManagement.ValidateDates(startDate, endDate).Item2;


            Console.Write("Location:");
            var location = Console.ReadLine();
            reservation.LocationID = ReservationManagement.ValidateCarLocation(carID, location);


            List<Nullable<DateTime>> carReserved = ReservationManagement.IsCarAvailabe(carID, startDate, endDate);
            if (carReserved.Count()>0)
            {
                Console.WriteLine("Car is not available on: " + carReserved);
                ReservationManagement.DisplayReservationInfo(reservation);

            }

            using (var db = new RentCDb())
            {
                Car car = db.Cars.Find(reservation.CarID);

                var price = car.PricePerDay * (decimal)((endDate - startDate).TotalDays);
                decimal totalPrice;

                Console.WriteLine("The car is available for " + car.PricePerDay + "/day");
                Console.Write("Do you have a cupone code?\nIf yes enter it, if no type NO: ");
                var ans = Console.ReadLine();
                if (ans.ToUpper() == "NO")
                {
                    totalPrice = price;
                }
                else
                {
                    var customerCupon = Console.ReadLine();
                    var cupon = db.Coupons.FirstOrDefault(c => c.CouponCode == customerCupon);
                    if (cupon != null)
                    {
                        Console.WriteLine("Discount: " + cupon.Discount + ": " + cupon.Description);
                        totalPrice = price - (cupon.Discount * price);
                    }
                    totalPrice = price;
                }

                Console.WriteLine("Total price: " + totalPrice);
                Console.WriteLine("Save the reservation? Type YES to save \nNO to go back to main menu: ");

                ans = Console.ReadLine();
                if (ans == "NO")
                {
                    MenuPage.SelectOption();
                }
                else
                {
            
                    reservation.ReservStatsID = 1;
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                }
            }



        }
    }



}

