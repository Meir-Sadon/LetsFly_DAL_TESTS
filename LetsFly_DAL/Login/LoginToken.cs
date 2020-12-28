using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public class LoginToken<T> : ILogin where T : IUser
    {
        public T User { get; set; }
    }
}
