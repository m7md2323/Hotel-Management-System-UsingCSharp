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
        private int Id=1000;
        private int roomNumber;
        private int guestNationalID;
        private string checkInDate;
        private string checkOutDate;
        private int reservationStatus;
        private int meal;//all the meals that the guest ordered(check MealsMenu enum)

        //Methods
        public Reservation(int roomNumber,int guestNationalID, string checkInDate, string checkOutDate,int meal)
        {
            this.Id++;
            this.roomNumber = roomNumber;
            this.guestNationalID = guestNationalID;
            this.checkInDate = checkInDate;
            this.checkOutDate = checkOutDate;
            this.meal = meal;
            reservationStatus =(int)ResStatus.CONFIRMED;
        }
        public int ID
        {
            get { return Id;}
        }
        public int ReservationStatus
        {
            set { reservationStatus = value;}
            get { return reservationStatus;}
        }
        public int Meal
        {
            get { return meal; }
        }
        public string CheckInDate
        {
            get { return checkInDate;}
        }
        public void DisplayAllInfo()
        {
            Console.WriteLine($"ID : {Id}\nRoom Number : {roomNumber}\nGuest National ID : {guestNationalID}");
            Console.WriteLine($"Check-In Date : {checkInDate}\nCheck-Out Date : {checkOutDate}\nReservation Status : {reservationStatus}");
            Console.WriteLine($"Meal : {meal}");
        }


        


    }
}
