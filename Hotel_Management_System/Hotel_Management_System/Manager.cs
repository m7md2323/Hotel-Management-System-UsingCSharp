using System;
using System.Collections.Generic;
using System.IO;
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

            List<Guest> GuestsList = new List<Guest>();
            FileStream fs = new FileStream("Guest.txt",FileMode.Open,FileAccess.Read);
            while (fs.Position < fs.Length)
            {
                object requiredGuest = DatabaseServer.bf.Deserialize(fs);
                GuestsList.Add((Guest)requiredGuest);
            
;                
            }
            for (int i=0;i<GuestsList.Count;i++) {
                GuestsList[i].DisplayAllInfo();
            }
            Console.WriteLine("guests successfully, enter [1] to get another manager service or [0] to logOut");
            int choice =Convert.ToInt32(Console.ReadLine());
            if (choice == 1) { SystemHandler.ChooseManagerService(); }
            else
                SystemHandler.systemState = SystemState.MANAGER_LOGIN;




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
