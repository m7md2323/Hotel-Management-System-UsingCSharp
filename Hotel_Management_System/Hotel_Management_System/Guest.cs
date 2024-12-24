using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
//written by mohammad
namespace Hotel_Management_System
{
    //national ID(unique), name, password, phone No, and bank balance
    [Serializable]
    internal class Guest
    {
        private int nationalID;
        private string name;
        private int password;
        private string phoneNumber;
        private double bankBalance;

        public Reservation reservation;
        public Guest()
        {

        }
        //for the initial build of guests
        public Guest(int nationalID, string name, int password,string phoneNumber, double bankBalance)
        {
            this.nationalID = nationalID;
            this.name = name;
            this.password = password;
            this.phoneNumber = phoneNumber;
            this.bankBalance = bankBalance;
        }
        public string Name
        {
            get { return name; }
        }
        public int NationalID
        {
            get { return nationalID;}
        }
        public int Password
        {
            get { return password; }
        }
        public double BankBalance
        {
            set { bankBalance = value; }
            get { return bankBalance; }
        }
        public void DisplayAllInfo()
        {
            string spaces = "                   ";
          

            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write($"| {nationalID}"+spaces.Substring(0,9));
            Console.Write($"| {name}"+spaces.Substring(0,14-name.Length));
            Console.Write($"| {password}"+spaces.Substring(0,12));
            Console.Write($"| {phoneNumber}"+spaces.Substring(0,9));
            Console.WriteLine($"| {bankBalance}"+spaces.Substring(0,11)+'|');
        }

    }
}
