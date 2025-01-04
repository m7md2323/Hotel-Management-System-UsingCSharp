using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//written by Zaid
namespace Hotel_Management_System
{
    [Serializable]
    internal class Manager
    {
        string Id = "m2024";
        int password = 00;

        public string ID
        {
            get { return Id; }
        }
        public int Password
        {
            get { return password; }
        }

        public void viewAllGuests() {
            Console.WriteLine("viewing all guests..");

            List<Guest> GuestsList = DatabaseServer.GetAllGuests();
            Guest.PrintHeaderTable();
            for (int i = 0; i < GuestsList.Count; i++) {
                GuestsList[i].DisplayAllInfo();
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Guests displayed successfully");
            SystemHandler.AfterManagerServiceMessage();





        }

        public void ViewAllReservations()
        {


            Console.WriteLine("viewing all reservations..");
           
            List<Reservation> ReservationsList = DatabaseServer.GetAllReservations();
            if (ReservationsList.Count == 0) { Console.WriteLine("There are no Reservations to display!!");SystemHandler.AfterServiceMessage();return; }
             Reservation.PrintHeaderTable();
            for (int i = 0; i < ReservationsList.Count; i++) { ReservationsList[i].DisplayAllInfo(); }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\nReservations displayed successfully");
            SystemHandler.AfterManagerServiceMessage();



        }
        public void ViewAllServices()
        {


            Console.WriteLine("viewing all Services..");
            
            List<Service> ServicessList = DatabaseServer.GetAllServices();
            if (ServicessList.Count == 0) { Console.WriteLine("There are no Services to display!!"); SystemHandler.AfterServiceMessage();return;}
            Service.PrintHeaderTable();
            for (int i = 0; i < ServicessList.Count; i++) { ServicessList[i].DisplayAllInfo(); }
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("\nServices displayed successfully");
            SystemHandler.AfterManagerServiceMessage();


        }
        public void viewAllPayments()
        {
            Console.WriteLine("viewing all payments..");
            
            List<Payment> AllPaymentsList = DatabaseServer.GetAllPayments();
            if (AllPaymentsList.Count == 0) { Console.WriteLine("There are no Payments to display!!"); SystemHandler.AfterServiceMessage();return; }
            Payment.PrintHeaderTable();
            for (int i = 0; i < AllPaymentsList.Count; i++) {
                AllPaymentsList[i].DisplayAllInfo();
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Payments successfully acquired");
            SystemHandler.AfterManagerServiceMessage();
        }
        public void viewAllRooms()
        {
            Console.WriteLine("viewing all rooms..\n");
            Room.PrintHeaderTable();
            List<Room> RoomsList = DatabaseServer.GetAllRooms();
            for (int i = 0; i < RoomsList.Count; i++)
            {
                RoomsList[i].DisplayAllInfo();
            }
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("Rooms successfully acquired");
            SystemHandler.AfterManagerServiceMessage();
        }
        public void updateRoomInfo()
        {
            Console.WriteLine("update room info..\n");
            Console.WriteLine("viewing all rooms..\n");
            List<Room> RoomsList = DatabaseServer.GetAllRooms();
            Room.PrintHeaderTable();
            for (int i = 0; i < RoomsList.Count; i++)
            {
                RoomsList[i].DisplayAllInfo();
            }
            Console.WriteLine("-----------------------------------------------------------------\n");
            Console.WriteLine("enter room number to update its info\n");
            int ChosenRoomNumber = Convert.ToInt32(Console.ReadLine());
            Room ChosenRoom = DatabaseServer.GetRoom(ChosenRoomNumber);
            int FoundRoomIndex=0;
            bool found=false;
            for (int i = 0; i <RoomsList.Count; i++) {
                if (RoomsList[i].RoomNumber == ChosenRoomNumber) {
                    found = true;
                    FoundRoomIndex = i;
                }
            }
            if (found == false)
            {
                Console.WriteLine("Room not found, please try again!!");
                Thread.Sleep(2500);
                return;
            }
                invalidOption:
                Console.WriteLine("Room Found!\nto update room (type) enter [1],to update room (price) enter [2] \n");
                int ChosenRoomUpdate = Convert.ToInt32(Console.ReadLine());
                if (ChosenRoomUpdate == 1) {//fix problem with not updating new type and price
                    tryAgain:
                    Console.WriteLine("enter the new type:");
                    string NewRoomType = Console.ReadLine();
                    if (NewRoomType != "Suite" && NewRoomType != "Double" && NewRoomType != "Single")
                    {
                        Console.WriteLine("room type doesn't exist, try again");
                        goto tryAgain;
                    }
                   if (NewRoomType == "Suite") NewRoomType += " ";
                    RoomsList[FoundRoomIndex].RoomType = NewRoomType;
                    FileStream fs = new FileStream("Room.txt", FileMode.Open, FileAccess.Write);
                    for (int i = 0; i < RoomsList.Count; i++)
                    {
                        DatabaseServer.bf.Serialize(fs, RoomsList[i]);
                    }
                    fs.Close();
                    Console.WriteLine("Rooms successfully Updated!");
                    SystemHandler.AfterManagerServiceMessage();

                }
                else if (ChosenRoomUpdate == 2)
                {
                    Console.WriteLine("enter the new price:");
                    RoomsList[FoundRoomIndex].PricePerDay = Convert.ToInt32(Console.ReadLine());
                     FileStream fs = new FileStream("Room.txt", FileMode.Open, FileAccess.Write);
                     for (int i = 0; i < RoomsList.Count; i++)
                     {
                         DatabaseServer.bf.Serialize(fs, RoomsList[i]);
                     }
                     fs.Close();

                    Console.WriteLine("Rooms successfully Updated!");
                SystemHandler.AfterManagerServiceMessage();
                }
                else { Console.WriteLine("Invalid input! please enter [1] or [2]");goto invalidOption; }
            

        }
        public void GenerateProfitReport()
        {
            Console.WriteLine("generating profit report..\n");


            Console.WriteLine("----------------------------------");
            Console.WriteLine($"|profit from Reservations: {DatabaseServer.GetReservationsRevenue()}$");
            Console.WriteLine($"|profit from Car rental: {DatabaseServer.GetCarRentalRevenue()}$");
            Console.WriteLine($"|profit from Kids zone: {DatabaseServer.GetKidsZoneRevenue()}$");
            Console.WriteLine("----------------------------------");


            Console.WriteLine("Report successfully generated!!");
            SystemHandler.AfterManagerServiceMessage();
        }
                 
                   


    }
}
