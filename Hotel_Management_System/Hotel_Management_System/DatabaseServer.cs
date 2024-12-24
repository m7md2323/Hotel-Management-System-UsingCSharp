using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
//written by zaid and mohammad
namespace Hotel_Management_System
{
    internal class DatabaseServer
    {
        //there will be two methods 
        //on for sending data and the other one for requesting data

        //to make sure that all IDs attributes of our classes are unique (Reservation ID, Service ID, Payment bill number).

       static BinaryFormatter bf = new BinaryFormatter();
        public static int LoadLastIdOfObject(string objectType)
        {
            FileStream fs = new FileStream("database.txt", FileMode.Open, FileAccess.Read);
            int Id=1000;
            while (fs.Position < fs.Length)
            {
                //here we will Deserialize all objects in our database and extract the needed ID(the last ID of the object type we sent)
                //so we will keep track of the last ID used in the last run
                //for example if a reservation ID with 1000 was created and then we closed the system. 
                //all we need to find is this reservation ID(1000) and add to it one to make the next reservation ID different from the others
                object temp = bf.Deserialize(fs);
                if (objectType == temp.GetType().Name && objectType== "Reservation")
                {
                    Id = ((Reservation)temp).ID;
                }
                if (objectType == temp.GetType().Name && objectType == "Payment")
                {
                    Id = ((Payment)temp).BillNumber;
                }
                if (objectType == temp.GetType().Name && objectType == "Service")
                {
                    Id = ((Service)temp).ID;
                }
            }
            fs.Close();
            return Id;
        }
        public static void SendData(object obj)
        {

            FileStream fs = new FileStream("database.txt", FileMode.Append,FileAccess.Write);
            switch (obj.GetType().Name)
            {
                case "Room":
                    bf.Serialize(fs,(Room)obj);
                    break;
                case "Guest":
                    bf.Serialize(fs,(Guest)obj);
                    break;
                case "Reservation":
                    bf.Serialize(fs, (Reservation)obj);
                    break;
                    //zaid finish the remaining classes
                default:
                    break;

            }
            fs.Close();
            
        }
        public static List<Room> LoadAvailableRooms()
        {
            List<Room> availableRooms = new List<Room>();
            FileStream fs = new FileStream("database.txt", FileMode.Open, FileAccess.Read);
            while (fs.Position < fs.Length)
            {
                object desObject = bf.Deserialize(fs);
                if(desObject.GetType().Name == "Room")
                {
                    //desObject = (Room)desObject;
                    if (((Room)desObject).Available == true) availableRooms.Add((Room)desObject);
                }
            }
            fs.Close();
            return availableRooms;
        }
        public static Guest LoadGuestUsingId(int NationalId)
        {
            FileStream fs = new FileStream("database.txt", FileMode.Open, FileAccess.Read);
            Guest guest=null;
            while (fs.Position < fs.Length)
            {
                object wantedUser = bf.Deserialize(fs);
                if (wantedUser.GetType().Name == "Guest"&&((Guest)wantedUser).NationalID==NationalId)
                {
                    guest = (Guest)wantedUser;
                }
            }
            fs.Close();
            return guest;
        }
        public static List<Guest> LoadGuests()
        {
            FileStream fs = new FileStream("database.txt", FileMode.Open, FileAccess.Read);
            List<Guest> guestUsers = new List<Guest>();
            while (fs.Position < fs.Length)
            {
                object wantedUser = bf.Deserialize(fs);
                if (wantedUser.GetType().Name == "Guest")
                {
                    guestUsers.Add((Guest)wantedUser);
                }
            }
            fs.Close();
            return guestUsers;
            
        }
        public static Room GetRoom(int roomNumber)
        {
            FileStream fs = new FileStream("database.txt", FileMode.Open, FileAccess.Read);
            List<Guest> guestUsers = new List<Guest>();
            while (fs.Position < fs.Length)
            {
                object wantedRoom = bf.Deserialize(fs);
                if (wantedRoom.GetType().Name == "Room"&&((Room)wantedRoom).RoomNumber==roomNumber)
                {
                    return (Room)wantedRoom;
                }
            }
            fs.Close();
            return null;
        }
        public static void DisplayAllGuests()
        {
            FileStream fs = new FileStream("database.txt", FileMode.Open, FileAccess.Read);
            while (fs.Position < fs.Length)
            {
                object wantedUser = bf.Deserialize(fs);
                if (wantedUser.GetType().Name == "Guest")
                {
                    ((Guest)wantedUser).DisplayAllInfo();
                }
            }
            fs.Close();
        }
        //build the request data function (zaid).


    }
}
