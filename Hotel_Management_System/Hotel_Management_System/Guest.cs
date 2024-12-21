using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
//written by mohammad
namespace Hotel_Management_System
{
    //national ID(unique), name, password, phone No, and bank balance
    [Serializable]
    internal class Guest
    {
        private int nationalID;
        private string name;
        private int password;
        private string phoneNumber;
        private double bankBalance;

        Reservation reservation;
        public Guest()
        {

        }
        //for the initial build of guests
        public Guest(int nationalID, string name, int password,string phoneNumber, double bankBalance)
        {
            this.nationalID = nationalID;
            this.name = name;
            this.password = password;
            this.phoneNumber = phoneNumber;
            this.bankBalance = bankBalance;
        }
        public void RoomReservation()
        {
            int roomNumber;
            string checkInDate;
            string checkOutDate;
            string meal="";
            int mealSelection;
            Console.WriteLine("Please enter the information below to confirm your reservation.");
            Console.WriteLine("Room Number : ");
            roomNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Check In Date and Check Out date in form of (DD/MM/YYYY) : ");
            //remember to handle the case where check-out is before check-in 
            Console.WriteLine("Check In Date : ");
            checkInDate = Console.ReadLine();
            Console.WriteLine("Check Out Date : ");
            checkOutDate = Console.ReadLine();
            //remember to send a payment record of the reservation
            Console.WriteLine("Choose one of the meals type below : ");
            Console.WriteLine("1. Breakfast.");
            Console.WriteLine("2. Breakfast and Lunch");
            Console.WriteLine("3. Full board.");
            mealSelection= Convert.ToInt32(Console.ReadLine());
            if (mealSelection == 1) meal = "Breakfast";
            if (mealSelection == 2) meal = "Breakfast and Lunch";
            if (mealSelection == 3) meal = "Full board.";
            reservation = new Reservation(roomNumber, nationalID, checkInDate, checkOutDate,meal);
            reservation.ID = DatabaseServer.GenerateUniqueId(reservation);
            DatabaseServer.SendDataToDatabase(reservation);
        }
        public void DisplayAllInfo()
        {
            string spaces = "                   ";
            
            //display info in 
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write($"| {nationalID}"+spaces.Substring(0,9));
            Console.Write($"| {name}"+spaces.Substring(0,14-name.Length));
            Console.Write($"| {password}"+spaces.Substring(0,12));
            Console.Write($"| {phoneNumber}"+spaces.Substring(0,9));
            Console.WriteLine($"| {bankBalance}"+spaces.Substring(0,11)+'|');
        }
        public void DisplayRes()
        {
            reservation.DisplayAllInfo();
        }
        public override string ToString()
        {
            return "Test" + "name : " + name;
        }

    }
}
