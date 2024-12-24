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
    public enum UserType{
        MANAGER=1,
        GUEST,
        INVALID_SELECTION
    }
    public enum GuestServiceSelection
    {
        RESERVE_A_ROOM=1,
        REQUEST_A_SERVICE,
        LOGOUT
    }
    internal class SystemHandler
    {   

        static Guest loggedInGuest;
        static Manager manager;
        public static void ChooseUser() {
            Console.WriteLine("--------------------[ Hotel Management System ]--------------------");
            Console.WriteLine("Please select the account type : ");
            Console.WriteLine("1. Manager \n2. Guest \n3. To Exit the System.");
            int userChoice=Convert.ToInt32(Console.ReadLine());
            UserLogin(userChoice);
            
        }
        public static bool UserLogin(int userType)
        {
            if (userType == (int)UserType.GUEST)
            {
                Console.Clear();
                Console.WriteLine("--------------------[ Guest Login ]--------------------");
                Console.WriteLine("Please enter your National ID and Password");
                Console.Write("National ID : ");
                int guestID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Password : ");
                int Password= Convert.ToInt32(Console.ReadLine());
                Console.Write("Verifying");LineOfDots();
                Console.Clear();
                if (GuestValidator(guestID, Password))
                {
                    Console.WriteLine("Successful Login!!");
                    Console.WriteLine();
                    loggedInGuest = DatabaseServer.LoadGuestUsingId(guestID);
                    Console.WriteLine($"Hello Mr.{loggedInGuest.Name} and welcome to our hotel system.");
                    Console.WriteLine();
                    EnterGuestSystem();
                }
                else
                {
                    Console.WriteLine("Unsuccessful Login attempt!!");
                    Console.WriteLine("National ID or Password is wrong, please try again!!");
                    Console.WriteLine("Press (1) to try again, (0) to Exit the system.");
                    if (Console.ReadLine() == "1") UserLogin((int)UserType.GUEST);
                    else return false; 
                    
                }
            }
            else if (userType == (int)UserType.MANAGER)
            {
                //zaid work
            }
            else
            {
                Console.WriteLine("Please enter a valid Choice!!");
                ChooseUser();
            }
            
            return true;
        }
        public static bool GuestValidator(int NationalId,int Password)
        {
            Guest guest = DatabaseServer.LoadGuestUsingId(NationalId);
            if (guest == null) return false;
            if (guest.Password != Password) return false;
            return true;
        }
        public static void EnterGuestSystem()
        {
            LoadGuestServicesMenu();
            int guestChoice = Convert.ToInt32(Console.ReadLine());
            switch (guestChoice)
            {
                case (int)GuestServiceSelection.RESERVE_A_ROOM:
                    ReserveRoom();
                    break;
                case (int)GuestServiceSelection.REQUEST_A_SERVICE:
                    RequestService();
                    break;
                case (int)GuestServiceSelection.LOGOUT:
                    loggedInGuest = null;
                    UserLogin((int)UserType.GUEST);
                    break;

            }
        }
        public static void LoadGuestServicesMenu()
        {
            Console.WriteLine("-----------------------[ Guest Hotel Services ]-----------------------");
            //Console.WriteLine("Please select the service you want below: ");
            Console.WriteLine("1. Reserve a room");
            Console.WriteLine("2. Request a service");
            Console.WriteLine("3. Check in");
            Console.WriteLine("4. Check out");
            Console.WriteLine("5. Pay for a reservation");
            Console.WriteLine("6. Pay for a service");
            Console.WriteLine("7. To Logout");
            Console.WriteLine("-----------------------------------------------------------------------");
        }
        //Guest functions///////////////////////////////////////////////////////
        public static void ReserveRoom()
        {
            int roomNumber;
            DateTime  checkInDate;
            DateTime checkOutDate;
            string meal = "";
            int mealSelection;
            var dateFormat = new CultureInfo("en-JO");
            Console.Clear();
            Console.WriteLine("---------------------------[ Room Reservation ]---------------------------");
            if (ShowAvailableRooms()==false){ 
                Console.WriteLine("We are sorry, there is no available rooms at the moment!!");
                EnterGuestSystem();
            }
            Console.WriteLine("Please fill the information below to confirm your reservation.");
            Console.Write("Room Number : ");
            roomNumber = Convert.ToInt32(Console.ReadLine());
            //get room info from database
            Room chosenRoom = DatabaseServer.GetRoom(roomNumber);
            if (chosenRoom == null)
            {
                Console.WriteLine("Wrong room number, please try again");
                Thread.Sleep(2000);
                ReserveRoom();
            }
            //change room availability to false
            Console.WriteLine("Check In Date and Check Out date in form of (DD/MM/YYYY) : ");
            //remember to handle the case where check-out is before check-in 
            Console.Write("Check In Date : ");
            checkInDate = DateTime.ParseExact(Console.ReadLine(),"dd/MM/yyyy",dateFormat);
            Console.Write("Check Out Date : ");
            checkOutDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", dateFormat);
            //remember to send a payment record of the reservation
            Console.WriteLine("Choose one of the meals type below : ");
            Console.WriteLine("1. Breakfast.");
            Console.WriteLine("2. Breakfast and Lunch.");
            Console.WriteLine("3. Full board.");
            mealSelection = Convert.ToInt32(Console.ReadLine());
            if (mealSelection == 1) meal = "Breakfast";
            if (mealSelection == 2) meal = "Breakfast and Lunch";
            if (mealSelection == 3) meal = "Full Board";
            //calculate the total amount of reservation (total room reservation + the price of the meal)
            Reservation res = null;
   
            Reservation reservation = new Reservation(GenerateId("Reservation"),roomNumber, loggedInGuest.NationalID, checkInDate, checkOutDate, meal);
            Payment resPaymentRecord = new Payment(GenerateId("Payment"), loggedInGuest.NationalID, "Reservation",calculateReservation(reservation,chosenRoom.PricePerDay,meal),"Unpaid");
            resPaymentRecord.DisplayAllInfo();
            loggedInGuest.reservation = reservation;
            //DatabaseServer.SendData(reservation);
        }
        public static void RequestService()
        {
            Console.Clear();
            Console.WriteLine("---------------------------[ Request a service ]---------------------------");
            Console.WriteLine("Please choose the service you want below : ");
            Console.WriteLine("1. Car rental.");
            Console.WriteLine("2. Kids zone.");
            Console.WriteLine("3. Get back to menu");
            int guestSelection = Convert.ToInt32(Console.ReadLine());
            if (guestSelection == 1)
            {
                Console.Write("Please enter the number of rental days : ");
                int numberOfRentDays= Convert.ToInt32(Console.ReadLine());
                Service newServiceRecord = new Service(GenerateId("Service"), loggedInGuest.reservation.ID, "Car rental", 24.5);
            }
            if (guestSelection == 2)
            {
                Console.Write("Please enter the number of children : ");
                int numberOfRentDays = Convert.ToInt32(Console.ReadLine());
                Service newServiceRecord = new Service(GenerateId("Service"), loggedInGuest.reservation.ID, "Kids zone", 24.5);
            }
            if (guestSelection == 3)
            {
                Console.Clear();
                EnterGuestSystem();
            }

        }
        public static bool ShowAvailableRooms()
        {
            List<Room> availableRooms = DatabaseServer.LoadAvailableRooms();
            if (availableRooms.Count == 0) {return false; }
            Console.WriteLine("Currently available rooms : ");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.Write($"|     Number    ");
            Console.Write($"|      Type     ");
            Console.Write($"| Price per day ");
            Console.WriteLine($"|   Available   |");
            for (int i = 0; i < availableRooms.Count; i++)
            {
                availableRooms[i].DisplayAllInfo();
            }
            Console.WriteLine("-----------------------------------------------------------------");
            return true;
        }
        public static double calculateReservation(Reservation reservation,double roomPrice,string meal)
        {
            TimeSpan totalResidenceDays = reservation.CheckOutDate.ToLocalTime() - reservation.CheckInDate.ToLocalTime();
            Console.WriteLine(totalResidenceDays.Days);
            double amount=0;
            if (meal == "Breakfast")
            {
                amount= totalResidenceDays.Days * roomPrice;
            }
            if(meal=="Breakfast and Lunch")
            {
                amount= 1.2 *totalResidenceDays.Days * roomPrice;
            }
            if (meal == "Full Board")
            {
                amount = 1.4*totalResidenceDays.Days * roomPrice;
            }
            return amount;
        }
        //to make sure that all IDs attributes of our classes unique (Reservation ID, Service ID, Payment bill number).
        public static int GenerateId(string objectType)
        {
            return DatabaseServer.LoadLastIdOfObject(objectType)+1;            
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
