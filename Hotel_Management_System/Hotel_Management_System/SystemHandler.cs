using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Management_System
{
    public enum SystemState
    {
        USER_SELECTION=1,
        MANAGER_LOGIN,
        GUEST_LOGIN,
        GUEST_MENU,
        MANAGER_MENU,
        EXIT
    }
    public enum UserType{
        MANAGER=1,
        GUEST,
        EXIT,
        INVALID_SELECTION

    }
    public enum GuestServiceSelection
    {
        RESERVE_A_ROOM=1,//int const RESERVE_A_ROOM=1;
        REQUEST_A_SERVICE,//int const REQUEST_A_SERVICE=2;
        CHECK_IN,
        CHECK_OUT,
        PAY_FOR_A_RESERVATION,
        PAY_FOR_A_SERVICE,
        LOGOUT
    }
    public enum ManagerServiceSelection
    {
        VIEW_ALL_GUESTS=1,
        VIEW_ALL_RESERVATIONS,
        VIEW_ALL_SERVICES,
        VIEW_ALL_PAYMENTS,
        VIEW_ALL_ROOMS,
        UPDATE_ROOM_INFO,
        GENERATE_PROFIT_REPORT,
        LOGOUT
    }
    internal class SystemHandler
    {   
        static Guest loggedInGuest;
        static Manager manager;
        static public SystemState systemState=SystemState.USER_SELECTION;
        public static UserType ChooseUser() {
            systemState = SystemState.USER_SELECTION;
            Console.Clear();
            Console.WriteLine("--------------------[ Hotel Management System ]--------------------");
            Console.WriteLine("Please select the account type : ");
            Console.WriteLine("[1] Manager \n[2] Guest \n[3] To Exit the System.");
            Console.WriteLine("-------------------------------------------------------------------");
            int userChoice = 0;
            try
            {
              userChoice = Convert.ToInt32(Console.ReadLine());
            }
            catch(FormatException e) { Console.WriteLine(e.Message);}
            return (UserType)userChoice;
            
        }
        public static void GuestLogin()
        {
                int guestID=0, Password=0; 
                systemState = SystemState.GUEST_LOGIN;
                Console.Clear();
                Console.WriteLine("-------------------------[ Guest Login ]-------------------------");
                Console.WriteLine("Please enter your National ID and Password (Type 0 to get back) ");
                Console.Write("National ID : ");
                try
                {
                    guestID = Convert.ToInt32(Console.ReadLine());
                    if (guestID == 0) { changeState(SystemState.USER_SELECTION); return; }
                    Console.Write("Password : ");
                    Password = Convert.ToInt32(Console.ReadLine());
                }
                catch(FormatException){ Console.WriteLine("Wrong format, Please type an integer!!");Thread.Sleep(2000); return;}
                Console.Write("Verifying");LineOfDots();
                Console.Clear();
                if (GuestValidator(guestID, Password))
                {
                    Console.WriteLine("Successful Login!!");
                    Console.WriteLine();
                    loggedInGuest = DatabaseServer.GetGuestUsingId(guestID);
                    Console.WriteLine($"Hello Mr.{loggedInGuest.Name} and welcome to our hotel system.");
                    Console.WriteLine();
                    EnterGuestSystem();
                }
                else
                {
                    Console.WriteLine("Unsuccessful Login attempt!!");
                    Console.WriteLine("National ID or Password is wrong, please try again!!");
                    Console.WriteLine("Press (1) to try again, (0) to login using different account type.");
                    if (Console.ReadLine() == "1") changeState(SystemState.GUEST_LOGIN);
                    else changeState(SystemState.USER_SELECTION);

                }

        }
        public static void ManagerHotelServices()
        {
            Console.Clear();
            
            Console.WriteLine($"Hello manager and welcome to the hotel system.");
            Console.WriteLine("1. View all guests\n2. View all reservations\n3. View all services\n4. View all payments\n5. View all rooms\n6. Update room information\n7. Generate profit report\n8. Log Out");
        }
       public static void  ManagerLogin()
        {
            systemState = SystemState.MANAGER_LOGIN;
            Console.Clear();
            manager = new Manager();
            Console.WriteLine("---------[Manager login]--------");
            Console.WriteLine("Please enter your ID and Password");
            Console.Write(" ID : ");
            string ManagerID = Console.ReadLine();
            Console.Write("Password : ");
            int Password = Convert.ToInt32(Console.ReadLine());
            if (ManagerID != manager.ID || Password != manager.Password)
            {
                Console.WriteLine("Unsuccessful Login attempt!!");
                Console.WriteLine("ID or Password is wrong, please try again!!");
                Console.WriteLine("Press (1) to try again, (0) to Exit the system.");
                MoreManagerOrLeave();
                void MoreManagerOrLeave()
                { string choice = Console.ReadLine();
                
                    if (choice == "1") { changeState(SystemState.MANAGER_LOGIN); }
                    else if (choice == "0") { changeState(SystemState.EXIT); }
                    else { Console.WriteLine("invalid choice, choose 1 or 0");Thread.Sleep(500) ; MoreManagerOrLeave(); }
                }
            }
            //
            else
            {
                Console.Write("verifying");
                LineOfDots();
                Console.WriteLine("Successful Login!!");
                Thread.Sleep(1200);
                Console.WriteLine();
                ChooseManagerService();
            }

            //return false;
        }
            public static void ChooseManagerService() 
            {
                
                systemState = SystemState.MANAGER_MENU;
                Console.WriteLine("choose a service: \n");
                ManagerHotelServices();
                int managerchoice = Convert.ToInt32(Console.ReadLine());
                switch (managerchoice)
                {
                    case (int)ManagerServiceSelection.VIEW_ALL_GUESTS:
                        manager.viewAllGuests();
                        break;
                    case (int)ManagerServiceSelection.VIEW_ALL_RESERVATIONS:
                        manager.ViewAllReservations();

                        break;
                    case (int)ManagerServiceSelection.VIEW_ALL_SERVICES:
                    manager.ViewAllServices();
                        
                        break;
                    case (int)ManagerServiceSelection.VIEW_ALL_PAYMENTS:
                        manager.viewAllPayments();
                        break;
                    case (int)ManagerServiceSelection.VIEW_ALL_ROOMS:
                        manager.viewAllRooms();
                        break;
                    case (int)ManagerServiceSelection.UPDATE_ROOM_INFO:
                        manager.updateRoomInfo();
                        break;
                    case (int)ManagerServiceSelection.GENERATE_PROFIT_REPORT:
                        manager.GenerateProfitReport();
                        break;
                    case (int)ManagerServiceSelection.LOGOUT:
                    Console.Write("Logging out"); LineOfDots();
                    changeState(SystemState.USER_SELECTION);
                    break;
                default: Console.WriteLine("invalid input, try again..");
                    Thread.Sleep(2000);
                    ChooseManagerService();
                    break;

                }

            }
        public static bool GuestValidator(int NationalId,int Password)
        {
            List<Guest> guests = DatabaseServer.GetAllGuests();
            foreach(Guest g in guests)
            {
                if (g.CheckLogin(NationalId, Password)) return true;
            }
            return false;
        }
        public static void EnterGuestSystem()
        {
            systemState = SystemState.GUEST_MENU;
            LoadGuestServicesMenu();
            int guestChoice = Convert.ToInt32(Console.ReadLine());
            switch (guestChoice)
            {
                case (int)GuestServiceSelection.RESERVE_A_ROOM:
                    loggedInGuest.ReserveRoom();
                    break;
                case (int)GuestServiceSelection.REQUEST_A_SERVICE:
                    loggedInGuest.RequestService();
                    break;
                case (int)GuestServiceSelection.CHECK_IN:
                    loggedInGuest.CheckIn();
                    break;
                case (int)GuestServiceSelection.CHECK_OUT:
                    loggedInGuest.CheckOut();
                    break;
                case (int)GuestServiceSelection.PAY_FOR_A_RESERVATION:
                    loggedInGuest.PayForReservation();
                    break;
                case (int)GuestServiceSelection.PAY_FOR_A_SERVICE:
                    loggedInGuest.PayForService();
                    break;
                case (int)GuestServiceSelection.LOGOUT:
                    loggedInGuest = null;
                    Console.Write("Logging out");LineOfDots();
                    changeState(SystemState.USER_SELECTION);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option, please try again!!");
                    EnterGuestSystem();
                    break;
            }
        }
        public static void LoadGuestServicesMenu()
        {
            Console.WriteLine("------------------------[ Guest Hotel Services ]------------------------");
            Console.WriteLine("Please choose a service: ");
            Console.WriteLine("[1] Reserve a room");
            Console.WriteLine("[2] Request a service");
            Console.WriteLine("[3] Check in");
            Console.WriteLine("[4] Check out");
            Console.WriteLine("[5] Pay for a reservation");
            Console.WriteLine("[6] Pay for a service");
            Console.WriteLine("[7] To Logout");
            Console.WriteLine("------------------------------------------------------------------------");
        }
        //Guest functions///////////////////////////////////////////////////////
       
        public static bool ShowAvailableRooms()
        {
            List<Room> allRooms = DatabaseServer.GetAllRooms();
            Console.WriteLine("Currently available rooms : ");
            bool valid=false;
            //to check if there are available rooms
            foreach(Room r in allRooms)
            {
                if (r.Available == true)valid = true; 
            }
            if (!valid) return false;
            Room.PrintHeaderTable();
            foreach (Room r in allRooms)
            {
                if (r.Available == true) { r.DisplayAllInfo(); }
            }
            Console.WriteLine("-----------------------------------------------------------------");
            return true;
        }
        public static void changeState(SystemState to)
        {
            Console.Clear();
            if (to == SystemState.GUEST_LOGIN) loggedInGuest = null;
            systemState = to;
        }
        public static float CalculateService(string service,int number) 
        {
            if(service=="Car rental")
            {
                return number * 10;
            }
            return number * 5;
        }
        public static float calculateReservation(Reservation reservation,float roomPrice,string meal)
        {
            var dateFormat = new CultureInfo("en-JO");
            DateTime checkInDate = DateTime.ParseExact(reservation.CheckInDate, "dd/MM/yyyy", dateFormat);
            DateTime checkOutDate = DateTime.ParseExact(reservation.CheckOutDate, "dd/MM/yyyy", dateFormat);
            TimeSpan totalResidenceDays = checkOutDate - checkInDate;
            Console.WriteLine(totalResidenceDays.Days);
            float amount=0;
            if (meal == "Breakfast")
            {
                amount= totalResidenceDays.Days * roomPrice;
            }
            if(meal=="Breakfast and Lunch")
            {
                amount= 1.2f *totalResidenceDays.Days * roomPrice;
            }
            if (meal == "Full Board")
            {
                amount = 1.4f*totalResidenceDays.Days * roomPrice;
            }
            if(reservation.CheckInDate=="01/02/2025"|| reservation.CheckInDate == "22/04/2025"|| reservation.CheckInDate == "10/10/2025")
            { return 0.6f * amount; }
            return amount;
        }
        //to make sure that all IDs attributes of our classes unique (Reservation ID, Service ID, Payment bill number).
        public static int GenerateId(string objectType)
        {
            return DatabaseServer.LoadLastIdOfObject(objectType)+1;            
        }
        public static void AfterServiceMessage()
        {
            Console.WriteLine("Press (1) to get back to menu, (0) To logout.");
            string userChoice=Console.ReadLine();
            if (userChoice == "1") changeState(SystemState.GUEST_MENU); 
            else { 
                changeState(SystemState.USER_SELECTION);
                Console.Write("Logging out"); LineOfDots();
            }
        }
        public static void AfterManagerServiceMessage()
        {
            Console.WriteLine("Press (1) to get back to menu, (0) To logout.");
            string userChoice = Console.ReadLine();
            if (userChoice == "1") changeState(SystemState.MANAGER_MENU);
            else
            {
                changeState(SystemState.USER_SELECTION);
                Console.Write("Logging out"); LineOfDots();
            }
        }
        public static bool UpdateBankBalance(float amount) 
        {
            List<Guest> guests = DatabaseServer.GetAllGuests();
            for(int i = 0; i < guests.Count; i++)
            {
                if (guests[i].NationalID == loggedInGuest.NationalID)
                {
                    if (guests[i].BankBalance < amount) return false;
                    guests[i].BankBalance -= amount;
                    loggedInGuest.BankBalance -= amount;
                }
            }
            DatabaseServer.SaveUpdatedGuests(guests);
            return true;
            
        }
        public static void LineOfDots()
        {
            for (int i = 0; i < 35; i++)
            {
                Console.Write('.');
                Thread.Sleep(12);
            }
            Console.WriteLine();
        }
    }
}
