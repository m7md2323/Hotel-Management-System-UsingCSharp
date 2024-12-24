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
           SystemHandler.ChooseUser();
            Room r = new Room();
            Console.WriteLine(r.GetType().Name);
            //TimeSpan totalResidanceDays = TimeSpan.Parse("10/4/2024") - TimeSpan.Parse(Convert.ToString("8/4/2024"));
            //Console.WriteLine(totalResidanceDays);

            /*
             *  442 single 25 True
                102 double 30 True
                506 double 32 False
                702 suite 40 True

               12345 Omar 11 05215 450
               12546 Khaled 22 01459 550
               16556 Salma 33 04122 660
               18730 Ahmad 44 02250 720
            
            Room room1 = new Room(442, "Single", 25, true);
            Room room2 = new Room(102, "Double", 30, true);
            Room room3 = new Room(506, "Double", 25, false);
            Room room4 = new Room(702, "Single", 40, true);
            DatabaseServer.SendData(room1);
            DatabaseServer.SendData(room2);
            DatabaseServer.SendData(room3);
            DatabaseServer.SendData(room4);
            */


            /*Guest guest1 = new Guest(12546, "Khaled", 22, "01459", 550);
            Guest guest2 = new Guest(16556, "Salma", 33, "04122", 660);
            Guest guest13 = new Guest(18730, "Ahmad", 44, "02250", 720);
             Guest guest = new Guest(12345, "Omar", 11, "05215", 450);
            DatabaseServer.SendData(guest1);
            DatabaseServer.SendData(guest2);
            DatabaseServer.SendData(guest13);
             DatabaseServer.SendData(guest);
            Console.WriteLine("Guests");
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("| National ID   ");
            Console.Write($"|     Name      ");
            Console.Write($"|   Password    ");
            Console.Write($"| Phone Number  ");
            Console.WriteLine($"|  Bank Balance |");
            //DatabaseServer.DisplayAllGuests();
            Console.WriteLine("---------------------------------------------------------------------------------");
           // Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
          
            
            


            // Console.WriteLine("---------------------------------------------------------------------------------");
            //Console.WriteLine(guest.ToString());
             Payment bill = new Payment(1234,11111,"Kids zone",400,"paid");
             bill.DisplayAllInfo();
            Service Se = new Service(1212,1111,"kidsZone",2992.211);
            Se.DisplayAllInfo();*/
        }
    }
}
