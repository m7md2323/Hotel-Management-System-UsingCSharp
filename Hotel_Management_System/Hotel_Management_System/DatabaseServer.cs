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

       static BinaryFormatter bf = new BinaryFormatter();
       /* public static int LoadLastIdRes()
        {
            int Id = 1000;
            using (FileStream fs = new FileStream("Reservation.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fs.Length == 0)
                {
                    return Id; // Return default ID if the file is empty
                }

                while (fs.Position < fs.Length)
                {
                    try
                    {
                        Reservation temp = (Reservation)bf.Deserialize(fs);  
                        Id = temp.ID;
                    }
                    catch (SerializationException ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message + ", skipping...");
                    }
                }
            }
            return Id;
        }
        public static int LoadLastIdPayment()
        {
            int Id = 1000;
            using (FileStream fs = new FileStream("Payment.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fs.Length == 0)
                {
                    return Id; // Return default ID if the file is empty
                }

                while (fs.Position < fs.Length)
                {
                    try
                    {
                        Payment temp = (Payment)bf.Deserialize(fs);
                        Id = temp.BillNumber;
                    }
                    catch (SerializationException ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message + ", skipping...");
                    }
                }
            }
            return Id;
        }*/
        public static int LoadLastIdOfObject(string objectType)
        {
            int Id = 1000;
            BinaryFormatter bff = new BinaryFormatter();

            using (FileStream fs = new FileStream(objectType+".txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fs.Length == 0)
                {
                    return Id; // Return default ID if the file is empty
                }

                while (fs.Position < fs.Length)
                {
                    try
                    {
                        object temp = bff.Deserialize(fs);

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
                    //zaid finish the remaining classes
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
        public static void SaveUpdatedPayments(List<Payment> data)
        {
            using (FileStream fs = new FileStream("Payment.txt", FileMode.Open, FileAccess.Write))
            {
                for (int i = 0; i < data.Count; i++)
                {

                    bf.Serialize(fs, data[i]);
                }
            }
        }
        public static void SaveUpdatedGuests(List<Guest> data)
        {
            using (FileStream fs = new FileStream("Guest.txt", FileMode.Open, FileAccess.Write))
            {
                for (int i = 0; i < data.Count; i++)
                {

                    bf.Serialize(fs, data[i]);
                }
            }
        }
        public static List<Type> GetData(string filePath, string obj)
        {
            List<Type> Data = new List<Type>();
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                while (fs.Position < fs.Length) {
                    object data = bf.Deserialize(fs);
                    Data.Add((Type)data);
                    /*switch (obj)
                    {
                        case "Room":
                            Data.Add((Type)data);
                            break;
                        case "Guest":
                            bf.Serialize(fs, (Guest)obj);
                            break;
                        case "Reservation":
                            bf.Serialize(fs, (Reservation)obj);
                            break;
                        //zaid finish the remaining classes
                        default:
                            break;

                    } */
                }
            }
            return Data;

        }
        public static List<Reservation> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            using (FileStream fs = new FileStream("Reservation.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                while (fs.Position < fs.Length)
                {
                    try 
                    {
                        reservations.Add((Reservation)bf.Deserialize(fs)); 
                    }
                    catch(SerializationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return reservations;
        }
        public static List<Payment> GetPayments()
        {
            List<Payment> payments = new List<Payment>();
            using (FileStream fs = new FileStream("Payment.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                while (fs.Position < fs.Length)
                {
                    try
                    {
                        Payment payment = (Payment)bf.Deserialize(fs);
                        payments.Add(payment);
                    }
                    catch (SerializationException)
                    {
                        // Handle or log the error and continue
                        Console.WriteLine("Encountered a corrupted entry, skipping...");
                    }
                }
            }
            return payments;
        }
        public static List<Room> LoadAvailableRooms()
        {
            List<Room> availableRooms = new List<Room>();
            using (FileStream fs = new FileStream("Room.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                while (fs.Position < fs.Length)
                {
                    object desObject = bf.Deserialize(fs);
                    if (desObject.GetType().Name == "Room")
                    {
                        //desObject = (Room)desObject;
                        if (((Room)desObject).Available == true) availableRooms.Add((Room)desObject);
                    }
                }
            }
            return availableRooms;
        }
        public static Guest GetGuestUsingId(int NationalId)
        {   
            Guest guest = null;
            using (FileStream fs = new FileStream("Guest.txt", FileMode.Open, FileAccess.Read))
            {
                
                while (fs.Position < fs.Length)
                {
                    object wantedUser = bf.Deserialize(fs);
                    if (wantedUser.GetType().Name == "Guest" ) 
                    {
                        Guest obj = (Guest)wantedUser;
                        if(obj.NationalID==NationalId)guest = obj;
                        
                    }
                }
            }
            return guest;
        }
        public static List<Guest> LoadGuests()
        {
            List<Guest> guestUsers = new List<Guest>();
            using (FileStream fs = new FileStream("Guest.txt", FileMode.Open, FileAccess.Read))
            {

                while (fs.Position < fs.Length)
                {
                    object wantedUser = bf.Deserialize(fs);
                    if (wantedUser.GetType().Name == "Guest")
                    {
                        guestUsers.Add((Guest)wantedUser);
                    }
                }
            }
            return guestUsers;
            
        }
        public static Room GetRoom(int roomNumber)
        { 
            List<Guest> guestUsers = new List<Guest>();
            using (FileStream fs = new FileStream("Room.txt", FileMode.Open, FileAccess.Read))
            {

                while (fs.Position < fs.Length)
                {
                    object wantedRoom = bf.Deserialize(fs);
                    if (wantedRoom.GetType().Name == "Room" && ((Room)wantedRoom).RoomNumber == roomNumber)
                    {
                        return (Room)wantedRoom;
                    }
                }
            }
            return null;
        }
        public static List<Guest> GetAllGuests()
        {
            List<Guest> guests = new List<Guest>();
            using (FileStream fs = new FileStream("Guest.txt", FileMode.Open, FileAccess.Read))
            {
                while (fs.Position < fs.Length)
                {
                    guests.Add((Guest)bf.Deserialize(fs));
                    
                }
            }
            return guests;
        }
        //build the request data function (zaid).


    }
}
