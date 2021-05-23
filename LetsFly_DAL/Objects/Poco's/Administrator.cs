using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    //POCO Class With Login Token.
    public class Administrator : User, IPoco, IUser
    {
        public long AdminNumber { get; set; }


        //Empty Constractor For Read From Sql.
        public Administrator()
        {
        }

        //Constractor Without Id For POCO Instance.
        public Administrator(string user_Name, string password)
        {
            UserName = user_Name;
            Password = password;
        }

        //Full Constractor For Read From Data base.
        public Administrator(long admin_Number, long id, string user_Name, string password)
        {
            AdminNumber = admin_Number;
            Id = id;
            UserName = user_Name;
            Password = password;
        }


        // Function To Change Password For This POCO.
        public void ChangePassword(string newPassword)
        {
            this.Password = newPassword;
        }
        // This Function Override The Real Operator == And Check If This.Id And Other.Id Are Equals.
        static public bool operator ==(Administrator me, Administrator other)
        {
            if (ReferenceEquals(me, other) || ReferenceEquals(me, null) && ReferenceEquals(other, null))
                return true;
            return false;
        }

        // This Function Override The Real Operator != And Check If This.Id And Other.Id Are NOT Equals.
        static public bool operator !=(Administrator me, Administrator other)
        {
            return !(me == other);
        }

        // This Function Override The Real Function Equals And Compair Between This.Id And Other.Id.
        public override bool Equals(object obj)
        {
            Administrator otherAdmin = obj as Administrator;
            return (this.Id == otherAdmin.Id);
        }

        // This Function Override The Real HashCode And Return this Id.
        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public override string ToString()
        {
            return $"Admin Id: {Id}. User Name: {UserName}.";
        }
    }
}
