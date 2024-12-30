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


          bool Exit = false;
            while (!Exit) {
                switch (SystemHandler.systemState)
                {
                    case SystemState.USER_SELECTION: //(case number 1)
                        //
                    switch (SystemHandler.ChooseUser())
                    {
                        case UserType.GUEST:
                            SystemHandler.GuestLogin();
                            break;
                            case UserType.MANAGER:
                                SystemHandler.ManagerLogin();
                                break;
                        case UserType.INVALID_SELECTION:
                            Console.WriteLine("Please enter a valid option(1, 2, 3)");
                            break;
                        case UserType.EXIT:
                            Console.WriteLine("Exiting the system........");
                            Exit = true;
                            break;
                    }
                        break;
                        //
                    case SystemState.GUEST_LOGIN: //(case number 2)
                    SystemHandler.GuestLogin();
                        break;
                    case SystemState.GUEST_MENU: //(case number 3) 
                    SystemHandler.EnterGuestSystem();
                        break;
                    case SystemState.MANAGER_MENU:
                        SystemHandler.ChooseManagerService();
                        break;
                    case SystemState.EXIT: //(case number 4)
                        Exit = true;
                        Console.WriteLine("Exiting the system........");
                        break;
                }
            }

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
            */
            
            /*Room room1 = new Room(442, "Single", 25f, true);
            Room room2 = new Room(102, "Double", 30f, true);
            Room room3 = new Room(506, "Double", 32f, false);
            Room room4 = new Room(702, "Suite", 40f, true);
            Room room5 = new Room(333, "Double", 34f, true);
            DatabaseServer.SaveData("Room.txt",room1);
            DatabaseServer.SaveData("Room.txt", room2);
            DatabaseServer.SaveData("Room.txt", room3);
            DatabaseServer.SaveData("Room.txt", room4);
            DatabaseServer.SaveData("Room.txt", room5);

            Guest guest = new Guest(12345, "Omar", 11, "05215", 450f);
            DatabaseServer.SaveData("Guest.txt", guest);

            Guest guest1 = new Guest(12546, "Khaled", 22, "01459", 550f);
            Guest guest2 = new Guest(16556, "Salma", 33, "04122", 660f);
            Guest guest13 = new Guest(18730, "Ahmad", 44, "02250", 720f);
             
            DatabaseServer.SaveData("Guest.txt", guest1);
            DatabaseServer.SaveData("Guest.txt", guest2);
            DatabaseServer.SaveData("Guest.txt", guest13);
            
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
          
            */



            // Console.WriteLine("---------------------------------------------------------------------------------");
            //Console.WriteLine(guest.ToString());
            Payment bill = new Payment(1234,11111,"Kids zone",400,"paid");
             bill.DisplayAllInfo();
            Service Se = new Service(1212,11111,"kidsZone",2992.211,10000);
            Se.DisplayAllInfo();
        }
    }
}
