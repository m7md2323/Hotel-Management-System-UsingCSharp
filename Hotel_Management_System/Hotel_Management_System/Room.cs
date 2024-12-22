using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
//written by mohammad
namespace Hotel_Management_System
{
    //number(unique), type, price(per day), and availability
    //For menu selection of a room type
    enum RoomTypeSelection
    {
        SINGLE,
        DOUBLE,
        SUITE
    }
    [Serializable]
    internal class Room
    {
        private readonly int roomNumber;
        private string roomType;
        private double pricePerDay;
        private bool available;

        public Room(int roomNumber,string roomType,double pricePerDay,bool available)
        {
            this.roomNumber = roomNumber;
            this.roomType = roomType;
            this.pricePerDay = pricePerDay;
            this.available = available;
        }

        public void DisplayAllInfo()
        {
            string spaces = "                   ";

            //display info in 
            Console.WriteLine("-----------------------------------------------------------------");
            Console.Write($"| {roomNumber}" + spaces.Substring(0, 11));
            Console.Write($"| {roomType}" + spaces.Substring(0, 14 - roomType.Length));
            Console.Write($"| {pricePerDay}" + spaces.Substring(0, 12));
            Console.WriteLine($"| {available}" + ((available==true)?spaces.Substring(0,10):spaces.Substring(0,9))+'|');
        }
    }
}
