using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    interface ILoginService
    {
        bool TryLogin(User userDetails, out ILogin token, out FacadeBase facade);
    }
}
