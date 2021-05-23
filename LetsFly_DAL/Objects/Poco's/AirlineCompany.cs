using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    //POCO Class With Login Token.
    public class AirlineCompany : User, IPoco, IUser
    {
        public long AirlineNumber { get; set; }
        public string AirlineName { get; set; }
        public long CountryCode { get; set; }

        //Empty Constractor For Read From Sql.
        public AirlineCompany()
        {
        }

        //Constractor Without Id For POCO Instance.
        public AirlineCompany(string airline_Name, string user_Name, string password, int country_Code)
        {
            AirlineName = airline_Name;
            UserName = user_Name;
            Password = password;
            CountryCode = country_Code;
        }

        //Full Constractor For Read From Data Base.
        public AirlineCompany(long airline_Number, long id, string airline_Name, string user_Name, string password, long country_Code)
        {
            AirlineNumber = airline_Number;
            Id = id;
            AirlineName = airline_Name;
            UserName = user_Name;
            Password = password;
            CountryCode = country_Code;
        }



        // Function To Change Password For This POCO.
        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }

        // This Function Override The Real Operator == And Check If This.Id And Other.Id Are Equals.
        static public bool operator ==(AirlineCompany me, AirlineCompany other)
        {
            if (ReferenceEquals(me, other) || ReferenceEquals(me, null) && ReferenceEquals(other, null))
                return true;
            return false;
        }

        // This Function Override The Real Operator != And Check If This.Id And Other.Id Are NOT Equals.
        static public bool operator !=(AirlineCompany me, AirlineCompany other)
        {
            return !(me == other);
        }

        // This Function Override The Real Function Equals And Compair Between This.Id And Other.Id.
        public override bool Equals(object obj)
        {
            AirlineCompany otherAirline = obj as AirlineCompany;
            return (this.Id == otherAirline.Id);
        }

        // This Function Override The Real HashCode And Return this Id.
        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public override string ToString()
        {
            return $"Airline Name: {AirlineName}. User Name: {UserName}. Country Number: {CountryCode}.";
        }
    }
}
