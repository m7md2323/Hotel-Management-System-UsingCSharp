using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//written by Mohammad 
namespace Hotel_Management_System
{
    // enums give more clear understanding
    enum ResStatus
    {
        CONFIRMED,//0
        CHECKED_IN,//1
        CHECKED_OUT//2
    }
    enum MealsMenu
    {
        BREAKFAST=1,//1
        BREAKFAST_AND_LUNCH,//2
        FULLBOARD//3
    }
    //: ID (unique), room number, guest's national ID, check-in date, check-out date,status, and meals
    [Serializable]
    internal class Reservation
    {
        //Properties
        private int Id;
        private int roomNumber;
        private int guestNationalID;
        private string checkInDate;
        private string checkOutDate;
        private string reservationStatus;
        private string meal;

        //Methods
        public Reservation(int roomNumber,int guestNationalID, string checkInDate, string checkOutDate,string meal)
        {
            this.roomNumber = roomNumber;
            this.guestNationalID = guestNationalID;
            this.checkInDate = checkInDate;
            this.checkOutDate = checkOutDate;
            this.meal = meal;
            reservationStatus ="Confirmed";
        }
        public int ID
        {
            set { Id = value; }
            get { return Id;}
        }
        public string ReservationStatus
        {
            set { reservationStatus = value;}
            get { return reservationStatus;}
        }
        public string Meal
        {
            get { return meal; }
        }
        public string CheckInDate
        {
            get { return checkInDate;}
        }
        public string CheckOutDate
        {
            get { return checkOutDate; }
        }
        public void DisplayAllInfo()
        {
            //this method will display all the information of this reservation in form of a table.
            string spaces = "                   ";
            // spaces variable to calculate the width of the cell accurately (to make all cells width equal).
            //if the width of each cell i 15 (character) then we need to add the proper amount of spaces after the attribute
            //15 - the size of the attribute = the number of needed spaces.
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.Write($"| {Id}" + spaces.Substring(0, 9));
            Console.Write($"| {roomNumber}" + spaces.Substring(0, 11));
            Console.Write($"| {guestNationalID}" + spaces.Substring(0, 9));
            Console.Write($"| {checkInDate}" + spaces.Substring(0, 14-checkInDate.Length));
            Console.Write($"| {checkOutDate}" + spaces.Substring(0, 14 - checkOutDate.Length));
            Console.Write($"| {reservationStatus}" + spaces.Substring(0, 14 - reservationStatus.Length));
            Console.WriteLine($"| {meal}"+spaces.Substring(0,19-meal.Length)+'|');
        }





    }
}
