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
        public static void SendDataToDatabase(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("database.txt", FileMode.OpenOrCreate,FileAccess.Write);
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

    }
}
