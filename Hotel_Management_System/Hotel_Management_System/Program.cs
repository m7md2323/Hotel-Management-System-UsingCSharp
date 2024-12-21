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

            /*
                12345 Omar 11 05215 450
                12546 Khaled 22 01459 550
                16556 Salma 33 04122 660
                18730 Ahmad 44 02250 720
             */
           Guest guest = new Guest(12345, "Omar", 11, "05215", 450);
            guest.RoomReservation();
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("| National ID   ");
            Console.Write($"|     Name      ");
            Console.Write($"|   Password    ");
            Console.Write($"| Phone Number  ");
            Console.WriteLine($"|  Bank Balance |");
            guest.DisplayAllInfo();
            Console.WriteLine("---------------------------------------------------------------------------------");
            guest.DisplayRes();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            /*
            
            Guest guest1 = new Guest(12546, "Khaled", 22, "01459", 550);
            Guest guest2 = new Guest(16556, "Salma", 33, "04122", 660);
            Guest guest13 = new Guest(18730, "Ahmad", 44, "02250", 720);

            Room room = new Room(442, "single", 25, true);
            Room room1 = new Room(102, "double", 30, false);
            guest.DisplayAllInfo();
            guest1.DisplayAllInfo();
            guest2.DisplayAllInfo();
            guest13.DisplayAllInfo();
            Console.WriteLine("---------------------------------------------------------------------------------");
            room.DisplayAllInfo();
            room1.DisplayAllInfo();*/
            //Console.WriteLine(guest.ToString());
        }
    }
}
