using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
//written by mohammad
namespace Hotel_Management_System
{
    //number(unique), type, price(per day), and availability
    //For menu selection of a room type
    enum RoomTypeSelection
    {
        SINGLE,
        DOUBLE,
        SUITE
    }
    [Serializable]
    internal class Room
    {
        private readonly int roomNumber;
        private string roomType;
        private double pricePerDay;
        private bool available;
    }
}
