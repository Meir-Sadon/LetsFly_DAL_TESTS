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
        public UserTypes Type { get; set; }
        public bool IsVerified { get; set; }

        //Empty Constractor.
        public User()
        {

        }

        //Constractor Without Id.
        public User(string userName, string password, UserTypes type, bool isVerified)
        {
            UserName = userName;
            Password = password;
            Type = type;
            IsVerified = isVerified;
        }

        //Full Constractor.
        public User(long id, string userName, string password, UserTypes myType, bool isVerified)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Type = myType;
            IsVerified = isVerified;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
