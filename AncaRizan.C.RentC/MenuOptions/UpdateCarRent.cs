using AncaRizan.C.RentC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncaRizan.C.RentC.MenuOptions
{
    static class UpdateCarRent
    {

        static Reservation GetReservation()
        {
            Console.WriteLine("Please enter a reservationID");
            var reservationID = ValidateUserInput.ValidateInputInt(Console.ReadLine());

            using (var db = new RentCDb())
            {

                while (!db.Reservations.Any(r => r.ReservationID == reservationID))
                {
                    Console.WriteLine("Please enter a valid reservationID");
                    reservationID = ValidateUserInput.ValidateInputInt(Console.ReadLine());

                }
                var reservation = db.Reservations.Find(reservationID);
                ReservationManagement.DisplayReservationInfo(reservation);
                return reservation;
            }

        }


        static String UpdateCarRentalOptions()
        {
            Console.WriteLine("Which field would you like to update? ");
            Console.WriteLine("1.Car Plate:");
            Console.WriteLine("2.ClientID");
            Console.WriteLine("3.Start Date:");
            Console.WriteLine("4.End Date:");
            Console.WriteLine("5. Location");
            Console.WriteLine("Please remember that if you change location you should also change the car");

            var ans = Console.ReadLine();

              return (ans);
        }


        public static void UpdateRental()
        {
          //  Reservation reservation = GetReservation();

      
            Console.WriteLine("Please enter a reservationID");
            var reservationID = ValidateUserInput.ValidateInputInt(Console.ReadLine());

            using (var db = new RentCDb())
            {
                while (!db.Reservations.Any(r => r.ReservationID == reservationID))
                {
                    Console.WriteLine("Please enter a valid reservationID");
                    reservationID = ValidateUserInput.ValidateInputInt(Console.ReadLine());

                }
                var reservation = db.Reservations.Find(reservationID);
                ReservationManagement.DisplayReservationInfo(reservation);

            Options:
                var ans = UpdateCarRentalOptions();

                switch (ans)
                {
                    case "1":
                        Console.WriteLine("Please enter a new car Plate");
                        var carPlate = Console.ReadLine();
                        var carID = ReservationManagement.ValidateCar(carPlate);
                        reservation.CarID = carID;
                        db.SaveChanges();
                        break;
                    case "2":
                        Console.WriteLine("Please enter a Client ID:");
                        var clientID = ReservationManagement.ValidateClient
                            (ValidateUserInput.ValidateInputInt(Console.ReadLine()));
                        reservation.CostumerID = clientID;
                        db.SaveChanges();
                        break;
                    case "3":
                        Console.WriteLine("Please enter Start Date (mm.dd.yyy):");
                        var enteredStartDate = ValidateUserInput.ValidateInputDate(Console.ReadLine());
                        var validStartDate = ReservationManagement.ValidateDates(enteredStartDate, reservation.EndDate).Item1;
                        reservation.StartDate = validStartDate;
                        db.SaveChanges();
                        break;
                    case "4":
                        Console.WriteLine("Please enter End Date (mm.dd.yyy):");
                        var enteredEndDate = ValidateUserInput.ValidateInputDate(Console.ReadLine());
                        var validEndDate = ReservationManagement.ValidateDates(reservation.StartDate, enteredEndDate).Item2;
                        reservation.EndDate = validEndDate;
                        db.SaveChanges();
                        break;
                    case "5":
                        Console.WriteLine("Please enter a new location: ");
                        var enteredLocation = Console.ReadLine();
                        var newLocation = ReservationManagement.ValidateCarLocation(reservation.CarID, enteredLocation);
                        reservation.LocationID = newLocation;
                        db.SaveChanges();
                        break;
                    default:
                        Console.WriteLine("Option not available");
                        goto Options;

            
                }

                Console.WriteLine("New Reservation Data:");
                ReservationManagement.DisplayReservationInfo(reservation);
                Console.WriteLine("Would you like to modify something else? \n" +
                    "If yes, type YES if no type NO to go back to main menu");
                ans = Console.ReadLine();
                if (ans.ToUpper() == "NO")
                {
                    MenuPage.SelectOption();
                }
                else if (ans.ToUpper() == "YES")
                {
                    goto Options;
                }


                db.SaveChanges();
            }
            
            

        }
    }
}

