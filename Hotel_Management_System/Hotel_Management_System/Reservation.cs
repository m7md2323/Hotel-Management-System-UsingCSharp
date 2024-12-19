using System;
using System.Collections.Generic;
using System.Linq;
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

    internal class Reservation
    {
        private int ID;
        private int roomNumber;
        private int guestNationalID;
        private string checkInDate;
        private string checkOutDate;
        private int reservationStatus;
        private int[] meals;//all the meals that the guest ordered(check MealsMenu enum)
    }
}
