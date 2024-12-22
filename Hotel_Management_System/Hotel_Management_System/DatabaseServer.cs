using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static int GenerateUniqueId(object obj)
        {
            FileStream fs = new FileStream("database.txt", FileMode.OpenOrCreate, FileAccess.Read);
            int Id=1000;
            while (fs.Position < fs.Length)
            {
                //here we will Deserialize all objects in our database and extract the needed ID(the last ID of the object type we sent)
                //so we will keep track of the last ID used in the last run
                //for example if a reservation ID with 1000 was created and then we closed the system. 
                //all we need to find is this reservation ID(1000) and add to it one to make the next reservation ID different from the others
                object temp = bf.Deserialize(fs);
                if (obj.ToString()==temp.ToString()&&obj.ToString()== "Hotel_Management_System.Reservation")
                { 
                    Id = ((Reservation)temp).ID + 1;
                }
                if (obj.ToString() == temp.ToString()&& obj.ToString() == "Hotel_Management_System.Payment")
                {
                    //Id = ((Payment)temp).ID + 1;
                }
                if (obj.ToString() == temp.ToString() && obj.ToString() == "Hotel_Management_System.Service")
                {
                    //Id = ((Service)temp).ID + 1;
                }
            }
            fs.Close();
            return Id;
        }
        public static void SendDataToDatabase(object obj)
        {

            FileStream fs = new FileStream("database.txt", FileMode.Append,FileAccess.Write);
            switch (obj.ToString())
            {
                case "Hotel_Management_System.Room":
                    bf.Serialize(fs,(Room)obj);
                    break;
                case "Hotel_Management_System.Guest":
                    bf.Serialize(fs,(Guest)obj);
                    break;
                case "Hotel_Management_System.Reservation":
                    bf.Serialize(fs, (Reservation)obj);
                    break;
                    //zaid finish the remaining classes
                default:
                    break;

            }
            fs.Close();
            
        }
        //build the request data function (zaid).


    }
}
