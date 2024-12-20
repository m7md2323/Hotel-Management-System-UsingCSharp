using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System
{

    internal class Program
    {
        static void Main(string[] arg)
        {
            Guest guest = new Guest(123, "Mohammad", 321, "0776690490", 400);
            guest.RoomReservation();
            //Console.WriteLine(guest.ToString());
        }
    }
}
