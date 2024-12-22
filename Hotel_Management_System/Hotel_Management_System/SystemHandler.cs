using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System
{
    
    internal class SystemHandler
    {   
        static Guest currentlyLoggedIn;
        static Manager manager;
        public static void ReserveRoom()
        {
            int roomNumber;
            string checkInDate;
            string checkOutDate;
            string meal = "";
            int mealSelection;
            Console.WriteLine("Please enter the information below to confirm your reservation.");
            Console.WriteLine("Room Number : ");
            roomNumber = Convert.ToInt32(Console.ReadLine());
            //change room availability to false
            Console.WriteLine("Check In Date and Check Out date in form of (DD/MM/YYYY) : ");
            //remember to handle the case where check-out is before check-in 
            Console.WriteLine("Check In Date : ");
            checkInDate = Console.ReadLine();
            Console.WriteLine("Check Out Date : ");
            checkOutDate = Console.ReadLine();
            //remember to send a payment record of the reservation
            Console.WriteLine("Choose one of the meals type below : ");
            Console.WriteLine("1. Breakfast.");
            Console.WriteLine("2. Breakfast and Lunch.");
            Console.WriteLine("3. Full board.");
            mealSelection = Convert.ToInt32(Console.ReadLine());
            if (mealSelection == 1) meal = "Breakfast";
            if (mealSelection == 2) meal = "Breakfast and Lunch";
            if (mealSelection == 3) meal = "Full Board";
            Reservation reservation = new Reservation(roomNumber, currentlyLoggedIn.NationalID, checkInDate, checkOutDate, meal);
            reservation.ID = DatabaseServer.GenerateUniqueId(reservation);
            DatabaseServer.SendDataToDatabase(reservation);
        }
        public static void RequestService()
        {
            Console.WriteLine("Enter the service you want below : ");
            Console.WriteLine("1. Car rental.");
            Console.WriteLine("2. Kids zone.");
        }
    }
}
