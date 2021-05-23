using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    //POCO Class With Login Token.
    public class Customer : User, IPoco, IUser
    {
        public long CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CreditCardNumber { get; set; }

        //Empty Constractor For Read From Sql.
        public Customer()
        {
        }

        //Constractor Without Id For POCO Instance.
        public Customer(string first_Name, string last_Name, string user_Name, string password, string address, string phone_No, string credit_Card_Number)
        {
            FirstName = first_Name;
            LastName = last_Name;
            UserName = user_Name;
            Password = password;
            Address = address;
            PhoneNo = phone_No;
            CreditCardNumber = credit_Card_Number;
        }

        //Full Constractor For Read From Data Base.
        public Customer(long customer_Number, long id, string first_Name, string last_Name, string user_Name, string password, string address, string phone_No, string credit_Card_Number)
        {
            CustomerNumber = customer_Number;
            Id = id;
            FirstName = first_Name;
            LastName = last_Name;
            UserName = user_Name;
            Password = password;
            Address = address;
            PhoneNo = phone_No;
            CreditCardNumber = credit_Card_Number;
        }



        // Function To Change Password For This POCO.
        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }
        // This Function Override The Real Operator == And Check If This.Id And Other.Id Are Equals.
        static public bool operator ==(Customer me, Customer other)
        {
            if (ReferenceEquals(me, other) || ReferenceEquals(me, null) && ReferenceEquals(other, null))
                return true;
            return false;
        }

        // This Function Override The Real Operator != And Check If This.Id And Other.Id Are NOT Equals.
        static public bool operator !=(Customer me, Customer other)
        {
            return !(me == other);
        }

        // This Function Override The Real Function Equals And Compair Between This.Id And Other.Id.
        public override bool Equals(object obj)
        {
            Customer otherCustomer = obj as Customer;
            return (this.Id == otherCustomer.Id);
        }

        // This Function Override The Real HashCode And Return this Id.
        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public override string ToString()
        {
            return $"Customer Id: {Id}. Full Name: {FirstName} {LastName}. User Name: {UserName}. Address: {Address}. Phone Number: {PhoneNo}. Credit Card Number: {CreditCardNumber}.";
        }
    }
}
