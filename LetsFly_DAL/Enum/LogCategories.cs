using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    [Flags]
    public enum LogCategories
    {
        None = 0,
        Administrators = 1,
        AirlineCompanies = 2,
        Customers = 4,
        Flights = 8,
        Tickets = 16,
        Countries = 32,
        Messages = 64,
        Adds = 128,
        Logins = 256,
        Deletions = 512,
        Updates = 1024,

        MovedToHIstory = 2048
    }
}
