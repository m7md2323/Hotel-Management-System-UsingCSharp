using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System
{//zaid written
    internal class Service
    {

        public Service(int ID, int reservationID, string description, double cost ){
            this.ID=ID;
            this.reservationID=reservationID;
            this.description=description;
            this.cost=cost;
        }
    }
}
