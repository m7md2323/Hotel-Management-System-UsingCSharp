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
            int meal;
            Console.WriteLine("Please enter the information below to confirm your reservation.");
            Console.WriteLine("Room Number : ");
            roomNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Check In Date and Check Out date in form of (DD/MM/YYYY) : ");
            Console.WriteLine("Check In Date : ");
            checkInDate = Console.ReadLine();
            Console.WriteLine("Check Out Date : ");
            checkOutDate = Console.ReadLine();
            Console.WriteLine("Choose one of the meals type below : ");
            Console.WriteLine("1. Breakfast.");
            Console.WriteLine("2. Breakfast and Lunch");
            Console.WriteLine("3. Full board.");
            meal= Convert.ToInt32(Console.ReadLine());
            Reservation reservation = new Reservation(roomNumber, nationalID, checkInDate, checkOutDate,meal);
            DatabaseServer.SendDataToDatabase(reservation);
        }
        public override string ToString()
        {
            return "Test" + "name : " + name;
        }

    }
}
