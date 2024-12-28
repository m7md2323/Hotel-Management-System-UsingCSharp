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
        
        }
        public void viewAllReservations() {
        
        }
        public void viewAllServices()
        {

        }
        public void viewAllPayments()
        {

        }
        public void viewAllRooms()
        {

        }
        public void updateRoomInfo()
        {

        }
        public void generateProfitReport()
        {

        }
    }
}
