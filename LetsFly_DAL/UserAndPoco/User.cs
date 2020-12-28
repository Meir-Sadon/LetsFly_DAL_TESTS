using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public class User : IUser
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public bool Is_Verified { get; set; }

        //Empty Constractor.
        public User()
        {

        }

        //Constractor Without Id.
        public User(string userName, string password, UserType type, bool isVerified)
        {
            UserName = userName;
            Password = password;
            Type = type;
            Is_Verified = isVerified;
        }

        //Full Constractor.
        public User(long id, string userName, string password, UserType myType, bool isVerified)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Type = myType;
            Is_Verified = isVerified;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
