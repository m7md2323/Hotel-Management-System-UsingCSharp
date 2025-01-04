using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
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

      public static BinaryFormatter bf = new BinaryFormatter();
 
        public static int LoadLastIdOfObject(string objectType)
        {
            int Id = 1000;
            using (FileStream fs = new FileStream(objectType+".txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                while (fs.Position < fs.Length)
                {
                    try
                    {
                        object temp = bf.Deserialize(fs);
                        if (objectType == "Reservation" && temp is Reservation reservation)
                        {
                            Id = reservation.ID;
                        }
                        else if (objectType == "Payment" && temp is Payment payment)
                        {
                            Id = payment.BillNumber;
                        }
                        else if (objectType == "Service" && temp is Service service)
                        {
                            Id = service.ID;
                        }
                    }
                    catch (SerializationException ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message + ", skipping...");
                    }
                }
            }

            return Id;
        }

        public static void SaveData(string filePath,object obj)
        {

            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                switch (obj.GetType().Name)
                {
                    case "Room":
                        bf.Serialize(fs, (Room)obj);
                        break;
                    case "Guest":
                        bf.Serialize(fs, (Guest)obj);
                        break;
                    case "Reservation":
                        bf.Serialize(fs, (Reservation)obj);
                        break;
                    case "Payment":
                        bf.Serialize(fs, (Payment)obj);
                        break;
                    case "Service":
                        bf.Serialize(fs, (Service)obj);
                        break;
                    default:
                        break;

                }
            }
            
        }
        public static void SaveUpdatedReservations(List<Reservation> data)
        {
            using (FileStream fs = new FileStream("Reservation.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                for (int i = 0; i < data.Count; i++) {

                    bf.Serialize(fs, data[i]);
                }
            }
        }
        public static void SaveUpdatedRoom(List<Room> data)
        {
            using (FileStream fs = new FileStream("Room.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                for (int i = 0; i < data.Count; i++)
                {

                    bf.Serialize(fs, data[i]);
                }
            }
        }
        public static void SaveUpdatedPayments(List<Payment> data)
        {
            FileStream fs = new FileStream("Payment.txt",FileMode.OpenOrCreate,FileAccess.Write);
            for(int i=0;i<data.Count ;i++)
            {
                bf.Serialize(fs, data[i]);
            }
            fs.Close();
        }
        public static void SaveUpdatedGuests(List<Guest> data)
        {
            using (FileStream fs = new FileStream("Guest.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                for (int i = 0; i < data.Count; i++)
                {

                    bf.Serialize(fs, data[i]);
                }
            }
        }
        
        public static List<Guest> GetAllGuests()
        {
            List<Guest> guest = new List<Guest>();
            using (FileStream fs = new FileStream("Guest.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                while (fs.Position < fs.Length)
                {
                    try
                    {
                        guest.Add((Guest)bf.Deserialize(fs));
                    }
                    catch (SerializationException e)
                    {
                        Console.WriteLine(e.Message + "in Guest.txt");
                    }
                }
            }
            return guest;
        }
        /////
        public static List<Reservation> GetAllReservations()
        {
            List<Reservation> AllReservations = new List<Reservation>();
            FileStream fs = new FileStream("Reservation.txt", FileMode.OpenOrCreate, FileAccess.Read);
            
                while (fs.Position < fs.Length)
                {
                    try { AllReservations.Add((Reservation)bf.Deserialize(fs));   }
                    catch (SerializationException e) { Console.WriteLine(e.Message + "in Reservation.txt");}
                }
                          fs.Close();
            
            return AllReservations;
        }
        public static List<Service> GetAllServices() {
            List<Service> AllServicesList = new List<Service>();
            FileStream fs = new FileStream("Service.txt", FileMode.OpenOrCreate, FileAccess.Read);
            while (fs.Position<fs.Length) {
                try { AllServicesList.Add((Service)bf.Deserialize(fs)); }
                catch(SerializationException e) { Console.WriteLine(e.Message + "in Service.txt"); }
            }
            fs.Close();
            return AllServicesList;
        
        }
        public static List<Payment> GetAllPayments()
        {
            List<Payment> AllPaymentsList = new List<Payment>();
            FileStream fs = new FileStream("Payment.txt",FileMode.OpenOrCreate,FileAccess.Read);
            while (fs.Position<fs.Length) {
                try { AllPaymentsList.Add((Payment)bf.Deserialize(fs)); }
                catch(SerializationException e) { Console.WriteLine(e.Message + "in Payment.txt"); }
            }
            fs.Close ();
            return AllPaymentsList;
        }
        public static List<Room> GetAllRooms()
        {
            List<Room> RoomsList = new List<Room>();
            FileStream fs = new FileStream("Room.txt", FileMode.OpenOrCreate, FileAccess.Read);
            while (fs.Position < fs.Length)
            {
                try { RoomsList.Add((Room)bf.Deserialize(fs)); }
                catch (SerializationException e) { Console.WriteLine(e.Message+"in Room.txt"); }
            }
            fs.Close();
            return RoomsList;
        }

        public static Guest GetGuestUsingId(int NationalId)
        {   
            Guest guest = null;
            using (FileStream fs = new FileStream("Guest.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                
                while (fs.Position < fs.Length)
                {
                    try
                    {
                        object wantedUser = bf.Deserialize(fs);
                        Guest obj = (Guest)wantedUser;
                        if (obj.NationalID == NationalId) guest = obj;
                    }
                    catch(SerializationException e) { Console.WriteLine(e.Message+ "in DatabaseServer.GetGuestUsingId()"); } 
                }
            }
            return guest;
        }
        public static Room GetRoom(int roomNumber)
        { 
            using (FileStream fs = new FileStream("Room.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {

                while (fs.Position < fs.Length)
                {
                    try
                    {
                        object wantedRoom = bf.Deserialize(fs);
                        if (((Room)wantedRoom).RoomNumber == roomNumber)
                        {
                            return (Room)wantedRoom;
                        }
                    }
                    catch (SerializationException e) { Console.WriteLine(e.Message + "in DatabaseServer.GetRoom()"); }
                }
            }
            return null;
        }
        public static float GetReservationsRevenue()
        {
            
            List<Payment> AllPaymentsList = DatabaseServer.GetAllPayments();
            float ReservationsRevenue = 0;
            for (int i = 0; i < AllPaymentsList.Count; i++)
            {
                if (AllPaymentsList[i].Status == "  Paid" && AllPaymentsList[i].Source=="Reservation")
                {
                    ReservationsRevenue += AllPaymentsList[i].Amount;
                }
            }
            return ReservationsRevenue;
        }

        public static float GetKidsZoneRevenue()
        {
            
            List<Payment> AllPaymentsList = DatabaseServer.GetAllPayments();
            float KidsZoneRevenue = 0;
            for (int i = 0; i < AllPaymentsList.Count; i++)
            {
                if (AllPaymentsList[i].Status == "  Paid" && AllPaymentsList[i].Source == "Kids zone")
                {
                    KidsZoneRevenue += AllPaymentsList[i].Amount;
                }
            }
            return KidsZoneRevenue;
        }
        public static float GetCarRentalRevenue()
        {
            
            List<Payment> AllPaymentsList = DatabaseServer.GetAllPayments();
            float CarRentalRevenue = 0;
            for (int i = 0; i < AllPaymentsList.Count; i++)
            {
                if (AllPaymentsList[i].Status == "  Paid" && AllPaymentsList[i].Source == "Car rental")
                {
                    CarRentalRevenue += AllPaymentsList[i].Amount;
                }
            }
            return CarRentalRevenue;
        }
        

    }
}
