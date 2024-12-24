using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//written by zaid
namespace Hotel_Management_System
{
    //: bill
   // number(unique), guest’s national ID, source, amount, and status
        

        [Serializable]
    internal class Payment
    {
        int billNumber;
        int guestNationalID;
        string source;
        double amount;
        string status; 
        public Payment(int billNumber,int guestNationalID, string source, double amount, string status)
        {
            this.billNumber = billNumber;
            this.guestNationalID = guestNationalID;
            this.source = source;
            this.amount = amount;
            this.status = status;
        }
        public void DisplayAllInfo() {
            string spaces = "                    ";

            
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write($"| {billNumber}" + spaces.Substring(0, 10));
            Console.Write($"| {guestNationalID}" + spaces.Substring(0, 9));
            Console.Write($"| {source}" + spaces.Substring(0, 14-source.Length));
            Console.Write($"| {amount}" + spaces.Substring(0, 14-Convert.ToString(amount).Length));
            Console.WriteLine($"| {status}" + spaces.Substring(0, 14-status.Length) + '|');
        }
        public int BillNumber { set { billNumber = value; } get { return billNumber; } }
        public string Source { set { source = value; } get { return source; } }
        public double Amount { set {  amount = value; } get { return amount; } }
        public string Status { set { status = value; } get { return status; } }
        public int GuestNationalID { get { return guestNationalID; } }
    }
}
