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

            List<Guest> GuestsList = new List<Guest>();
            FileStream fs = new FileStream("Guest.txt", FileMode.Open, FileAccess.Read);
            while (fs.Position < fs.Length)
            {
                object requiredGuest = DatabaseServer.bf.Deserialize(fs);
                GuestsList.Add((Guest)requiredGuest);

                ;
            }
            for (int i = 0; i < GuestsList.Count; i++) {
                GuestsList[i].DisplayAllInfo();
            }
            Console.WriteLine("guests successfully");
            SystemHandler.AfterManagerServiceMessage();





        }

        public void ViewAllReservations()
        {


            Console.WriteLine("viewing all reservations..");
            List<Reservation> ReservationsList = DatabaseServer.GetAllReservations();

            for (int i = 0; i < ReservationsList.Count; i++) { ReservationsList[i].DisplayAllInfo(); }
            Console.WriteLine("\nreservations displayed successfully");
            SystemHandler.AfterManagerServiceMessage();



        }
        public void ViewAllServices()
        {


            Console.WriteLine("viewing all Services..");
            List<Service> ServicessList = DatabaseServer.GetAllServices();

            for (int i = 0; i < ServicessList.Count; i++) { ServicessList[i].DisplayAllInfo(); }
            Console.WriteLine("\nServices displayed successfully");
            SystemHandler.AfterManagerServiceMessage();


        }
        public void viewAllPayments()
        {
            Console.WriteLine("viewing all payments..");
            List<Payment> AllPaymentsList = DatabaseServer.GetAllPayments();
            for (int i = 0; i < AllPaymentsList.Count; i++) {
                AllPaymentsList[i].DisplayAllInfo();
            }
            Console.WriteLine("Payments successfully aquired");
            SystemHandler.AfterManagerServiceMessage();


        }
        public void viewAllRooms()
        {
            Console.WriteLine("viewing all rooms..\n");
            List<Room> RoomsList = DatabaseServer.GetAllRooms();
            for (int i = 0; i < RoomsList.Count; i++)
            {
                RoomsList[i].DisplayAllInfo();
            }
            Console.WriteLine("Rooms successfully aquired,type [1] to use another manager service or [0] To exit");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1) { SystemHandler.ChooseManagerService(); }
            else SystemHandler.ChooseUser();




        }
        public void updateRoomInfo()
        {
            Console.WriteLine("update room info..\n");
            Console.WriteLine("viewing all rooms..\n");
            List<Room> RoomsList = DatabaseServer.GetAllRooms();
            for (int i = 0; i < RoomsList.Count; i++)
            {
                RoomsList[i].DisplayAllInfo();
            }
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
                   
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1) { SystemHandler.ChooseManagerService(); }
                    else SystemHandler.ChooseUser();
                }
                else { Console.WriteLine("Invalid input! please enter [1] or [2]");goto invalidOption; }
            

        }
        public void GenerateProfitReport()
        {
            Console.WriteLine("generating profit report..\n");


            Console.WriteLine("----------------------------------");
            Console.WriteLine($"|profit from Reservations: {DatabaseServer.GetReservationsRevenue()}|");
            Console.WriteLine($"|profit from Car rental: {DatabaseServer.GetCarRentalRevenue()}|");
            Console.WriteLine($"|profit from Kids zone: {DatabaseServer.GetKidsZoneRevenue()}|");
            Console.WriteLine("----------------------------------");


            Console.WriteLine("Rooms successfully Updated! ,type [1] to use another manager service or [0] To exit");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1) { SystemHandler.AfterManagerServiceMessage(); return; }
            else {SystemHandler.ChooseUser(); return;}
        }
                 
                   


    }
}
