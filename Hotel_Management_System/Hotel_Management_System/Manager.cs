using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//written by Zaid
namespace Hotel_Management_System
{
    [Serializable]
    internal class Manager
    {
        string Id = "m2022";
        int password = 00;
        
        public string ID
        {
            get { return Id; }
        }
        public int Password
        {
            get { return password;}
        }
      
        public  void viewAllGuests() {
            Console.WriteLine("viewing all guests..");
            DatabaseServer db = new DatabaseServer();
            DatabaseServer.GetAllGuests();///////



        }
        public void viewAllReservations() {
            Console.WriteLine("viewing all reservations..");
        }
        public void viewAllServices()
        {
            Console.WriteLine("viewing all services..");

        }
        public void viewAllPayments()
        {
            Console.WriteLine("viewing all payments..");

        }
        public void viewAllRooms()
        {
            Console.WriteLine("viewing all rooms..");

        }
        public void updateRoomInfo()
        {
            Console.WriteLine("update room info..");

        }
        public void generateProfitReport()
        {
            Console.WriteLine("generating profit report..");

        }
    }
}
