using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        private int roomNumber;
        private string roomType;
        private float pricePerDay;
        private bool available;

        public Room(int roomNumber,string roomType, float pricePerDay, bool available)
        {
            this.roomNumber = roomNumber;
            this.roomType = roomType;
            this.pricePerDay = pricePerDay;
            this.available = available;
        }
        public int RoomNumber
        {
            get { return roomNumber;}
        }
        public string RoomType
        {
            get { return roomType;}
        }
        public float PricePerDay
        {
            set { pricePerDay = value; }
            get { return pricePerDay; }
        }
        public bool Available
        {
            set { available = value;}
            get { return available;}
        }
        public static void PrintHeaderTable()
        {
            Console.WriteLine("-----------------------------------------------------------------");
            Console.Write($"|     Number    ");
            Console.Write($"|      Type     ");
            Console.Write($"| Price per day ");
            Console.WriteLine($"|   Available   |");
        }
        public void DisplayAllInfo()
        {
            string spaces = "                   ";


            Console.WriteLine("-----------------------------------------------------------------");
            Console.Write($"| {roomNumber}" + spaces.Substring(0, 11));
            Console.Write($"| {roomType}" + spaces.Substring(0, 14 - roomType.Length));
            Console.Write($"| {pricePerDay}$" + spaces.Substring(0, 11));
            Console.WriteLine($"| {available}" + ((available==true)?spaces.Substring(0,10):spaces.Substring(0,9))+'|');
        }
        
    }
}
