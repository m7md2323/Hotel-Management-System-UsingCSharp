using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//written by mohammad
namespace Hotel_Management_System
{
    //national ID(unique), name, password, phone No, and bank balance
    [Serializable]
    internal class Guest
    {
        private int nationalID;
        private string name;
        private int password;
        private string phoneNumber;
        private double bankBalance;

        public Guest()
        {

        }
        //for the initial build of guests
        public Guest(int nationalID, string name, int password,string phoneNumber, double bankBalance)
        {
            this.nationalID = nationalID;
            this.name = name;
            this.password = password;
            this.phoneNumber = phoneNumber;
            this.bankBalance = bankBalance;
        }
        public string Name
        {
            get { return name; }
        }
        public int NationalID
        {
            get { return nationalID;}
        }
        public int Password
        {
            get { return password; }
        }
        public double BankBalance
        {
            set { bankBalance = value; }
            get { return bankBalance; }
        }
        public void DisplayAllInfo()
        {
            string spaces = "                   ";
          

            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write($"| {nationalID}"+spaces.Substring(0,9));
            Console.Write($"| {name}"+spaces.Substring(0,14-name.Length));
            Console.Write($"| {password}"+spaces.Substring(0,12));
            Console.Write($"| {phoneNumber}"+spaces.Substring(0,9));
            Console.WriteLine($"| {bankBalance}"+spaces.Substring(0,11)+'|');
        }
        public void ReserveRoom()
        {
            int roomNumber;
            string checkInDate;
            string checkOutDate;
            string meal = "";
            int mealSelection;
            Console.Clear();
            Console.WriteLine("---------------------------[ Room Reservation ]---------------------------");
            if (SystemHandler.ShowAvailableRooms() == false)
            {
                Console.WriteLine("We are sorry, there is no available rooms at the moment!!");
                Thread.Sleep(2000);
                SystemHandler.changeState(SystemState.GUEST_MENU);
                return;
            }
            Console.WriteLine("Please fill the information below to confirm your reservation (Type 0 to cancel the reservation).");
            Console.Write("Room Number : ");
            roomNumber = Convert.ToInt32(Console.ReadLine());
            //get room info from database
            Room chosenRoom = DatabaseServer.GetRoom(roomNumber);
            if (roomNumber == 0) { SystemHandler.changeState(SystemState.GUEST_MENU); return; }
            if (chosenRoom == null)
            {
                Console.WriteLine("Wrong room number, please try again");
                Thread.Sleep(2000);
                ReserveRoom();
                return;
            }
            //change room availability to false
            Console.WriteLine("Check In Date and Check Out date in form of (DD/MM/YYYY) : ");
            //remember to handle the case where check-out is before check-in 
            Console.Write("Check In Date : ");
            checkInDate = Console.ReadLine();
            Console.Write("Check Out Date : ");
            checkOutDate = Console.ReadLine();
            try
            {
                DateTime checkTimeValidity = DateTime.ParseExact(checkInDate, "dd/MM/yyyy", new CultureInfo("en-Jo"));
                checkTimeValidity = DateTime.ParseExact(checkOutDate, "dd/MM/yyyy", new CultureInfo("en-Jo"));
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(2000);
                ReserveRoom();
                return;
            }
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
            Reservation reservation = new Reservation(SystemHandler.GenerateId("Reservation"), roomNumber, NationalID, checkInDate, checkOutDate, meal);
            Payment resPaymentRecord = new Payment(SystemHandler.GenerateId("Payment"), NationalID, "Reservation", SystemHandler.calculateReservation(reservation, chosenRoom.PricePerDay, meal), "Unpaid");
            Console.WriteLine("Your receipt : ");
            //PrintHeaderTable();
            resPaymentRecord.DisplayAllInfo();
            Console.WriteLine("---------------------------------------------------------------------------------");
            DatabaseServer.SaveData("Reservation.txt", reservation);
            DatabaseServer.SaveData("Payment.txt", resPaymentRecord);
            Console.WriteLine("Room reservation went successfully, Thank you for using our Hotel!!");
            SystemHandler.AfterServiceMessage();
        }
        public void RequestService()
        {
            Payment servPaymentRecord = null;
            Service newServiceRecord = null;
            Console.Clear();
            Console.WriteLine("---------------------------[ Request a service ]---------------------------");
            Console.WriteLine("Please choose the service you want below : ");
            Console.WriteLine("1. Car rental.");
            Console.WriteLine("2. Kids zone.");
            Console.WriteLine("Press (0) to get back.");
            Console.WriteLine("---------------------------------------------------------------------------");
            string guestSelection = Console.ReadLine();
            if (guestSelection == "1")
            {
                Console.Write("Please enter the number of rental days : ");
                int numberOfRentDays = Convert.ToInt32(Console.ReadLine());
                newServiceRecord = new Service(SystemHandler.GenerateId("Service"), NationalID, "Car rental", SystemHandler.CalculateService("Car rental", numberOfRentDays));
                servPaymentRecord = new Payment(SystemHandler.GenerateId("Payment"), NationalID, "Car rental", SystemHandler.CalculateService("Car rental", numberOfRentDays), "Unpaid");
            }
            if (guestSelection == "2")
            {
                Console.Write("Please enter the number of children : ");
                int numberOfChildren = Convert.ToInt32(Console.ReadLine());
                newServiceRecord = new Service(SystemHandler.GenerateId("Service"), NationalID, "Kids zone", SystemHandler.CalculateService("Kids zone", numberOfChildren));
                servPaymentRecord = new Payment(SystemHandler.GenerateId("Payment"), NationalID, "Kids zone", SystemHandler.CalculateService("Kids zone", numberOfChildren), "Unpaid");
            }
            if (guestSelection == "0")
            {
                Console.Clear();
                SystemHandler.changeState(SystemState.GUEST_MENU);
                return;
            }
            Console.WriteLine("Your receipt : ");
            //PrintHeaderTable();
            servPaymentRecord.DisplayAllInfo();
            Console.WriteLine("---------------------------------------------------------------------------------");
            DatabaseServer.SaveData("Payment.txt", servPaymentRecord);
            DatabaseServer.SaveData("Service.txt", newServiceRecord);
            SystemHandler.AfterServiceMessage();


        }
        public  void CheckIn()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------[ Check In ]--------------------------------------------------");
            List<Reservation> reservations = DatabaseServer.GetReservations();
            Console.WriteLine("All Confirmed reservations in your name: ");
            Reservation.PrintHeaderTable();
            int numberOfRes = 0;
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations[i].NationalId ==NationalID && reservations[i].ReservationStatus == "Confirmed")
                {
                    numberOfRes++;
                    reservations[i].DisplayAllInfo();
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            if (numberOfRes == 0)
            {
                Console.Clear();
                Console.WriteLine("There is no Confirmed reservations in your name, please reserve a room and try again!!");
                SystemHandler.AfterServiceMessage();
                return;

            }
            Console.WriteLine("Please enter the reservation Id to check in: ");
            int resId = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations[i].ID == resId)
                {
                    reservations[i].ReservationStatus = "Checked In";
                    
                }
            }
            DatabaseServer.SaveUpdatedReservations(reservations);
            Console.WriteLine("Checking In went successfully, Thank you for using our Hotel!!");
            SystemHandler.AfterServiceMessage();
        }
        public  void CheckOut()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------[ Check Out ]--------------------------------------------------");
            List<Reservation> reservations = DatabaseServer.GetReservations();
            Console.WriteLine("All Checked In reservations in your name: ");
            Reservation.PrintHeaderTable();
            int numberOfRes = 0;
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations[i].NationalId == NationalID && reservations[i].ReservationStatus == "Checked In")
                {
                    numberOfRes++;
                    reservations[i].DisplayAllInfo();
                }
            }
            if (numberOfRes == 0)
            {
                Console.Clear();
                Console.WriteLine("There is no Checked In reservations in your name, please Check in first and try again!!");
                SystemHandler.AfterServiceMessage();
                return;

            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Please enter the reservation Id to check out: ");
            int resId = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations[i].ID == resId)
                {
                    reservations[i].ReservationStatus = "Checked Out";
                    ;
                }
            }
            DatabaseServer.SaveUpdatedReservations(reservations);
            Console.WriteLine("Checking out went successfully, Thank you for using our Hotel!!");
            SystemHandler.AfterServiceMessage();
        }
        public void PayForReservation()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------[ Pay For Reservation ]--------------------------------------------------");
            List<Payment> payments = DatabaseServer.GetPayments();
            if (payments == null) Console.WriteLine("No payments available");
            Console.WriteLine("All Unpaid reservations in your name: ");
            // Reservation.PrintHeaderTable();
            int numberOfPayments = 0;
            for (int i = 0; i < payments.Count; i++)
            {
                if (payments[i].GuestNationalID == NationalID && payments[i].Status == "Unpaid" && payments[i].Source == "Reservation")
                {
                    numberOfPayments++;
                    payments[i].DisplayAllInfo();
                }
            }
            if (numberOfPayments == 0)
            {
                Console.Clear();
                Console.WriteLine("There is no Unpaid reservations in your name!!");
                SystemHandler.AfterServiceMessage();
                return;

            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Please enter the bill number to pay: ");
            int billNumber = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < payments.Count; i++)
            {
                if (payments[i].BillNumber == billNumber)
                {
                    //Check guest Bank balance and if enough pay the bill, and update the guest bank balance
                    payments[i].Status = "  Paid";

                }
            }
            DatabaseServer.SaveUpdatedPayments(payments);
            Console.WriteLine("Paying for reservation went successfully, Thank you for using our Hotel!!");
            SystemHandler.AfterServiceMessage();
        }
        public void PayForService()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------[ Pay For Service ]--------------------------------------------------");
            List<Payment> payments = DatabaseServer.GetPayments();
            if (payments == null) Console.WriteLine("No payments available");
            Console.WriteLine("All Unpaid Services in your name: ");
            // Reservation.PrintHeaderTable();
            int numberOfPayments = 0;
            for (int i = 0; i < payments.Count; i++)
            {
                if (payments[i].GuestNationalID == NationalID && payments[i].Status == "Unpaid" && payments[i].Source != "Reservation")
                {
                    numberOfPayments++;
                    payments[i].DisplayAllInfo();
                }
            }
            if (numberOfPayments == 0)
            {
                Console.Clear();
                Console.WriteLine("There is no Unpaid Services in your name!!");
                SystemHandler.AfterServiceMessage();
                return;

            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Please enter the bill number to pay: ");
            int billNumber = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < payments.Count; i++)
            {
                if (payments[i].BillNumber == billNumber)
                {
                    //Check guest Bank balance and if enough pay the bill, and update the guest bank balance
                    payments[i].Status = "  Paid";

                }
            }
            DatabaseServer.SaveUpdatedPayments(payments);
            Console.WriteLine("Paying for Service went successfully, Thank you for using our Hotel!!");
            SystemHandler.AfterServiceMessage();
        }
    }
}
