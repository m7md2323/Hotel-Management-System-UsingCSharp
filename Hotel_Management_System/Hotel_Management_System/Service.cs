using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System
{//zaid written
    [Serializable]
    internal class Service
    {
        int id; 
        int gustNationalID;
        string description;
        double cost;

        public Service(int id, int gustNationalID, string description, double cost ){
            this.id = id;
            this.gustNationalID = gustNationalID;
            this.description=description;
            this.cost=cost;
        }
        public int ID { set{ id=value; } get { return id; } }
        public int GustNationalID { set { gustNationalID = value; } get { return gustNationalID; } }
        public string Description { set { description = value; } get{ return description; } }
        public double Cost { set { cost = value; } get { return cost; } }
        public void DisplayAllInfo()
        {
            string spaces = "                    ";


            Console.WriteLine("-----------------------------------------------------------------");
            Console.Write($"| {ID}" + spaces.Substring(0, 10));
            Console.Write($"| {GustNationalID}" + spaces.Substring(0, 9));
            Console.Write($"| {Description}" + spaces.Substring(0, 14 - Description.Length));
            Console.WriteLine($"| {Cost}" + spaces.Substring(0, 14 - Convert.ToString(Cost).Length) + '|');
        }

    }
}
