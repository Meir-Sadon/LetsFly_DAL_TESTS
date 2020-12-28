using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public static class FlyingCenterConfig
    {
        public const string ADMIN_NAME = "ADMIN";
        public const string ADMIN_PASSWORD = "9999";
        public static LoginToken<Administrator> basicToken = new LoginToken<Administrator> { User = new Administrator(0, 0, "ADMIN", "9999") };

        // Azure Connection.
        //public const string CONNECTION_STRING = @"Server=tcp:flight-managment-srv.database.windows.net,1433;Initial Catalog=FlightManagmentDB;Persist Security Info=False;User ID=meir;Password=Password1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // SmarterAsp Connection.
        //public const string CONNECTION_STRING = @"Data Source=SQL5053.site4now.net;Initial Catalog=DB_A63C41_FlightManagmentDBs;User Id=DB_A63C41_FlightManagmentDBs_admin;Password=2040608090mm";
        
        // My Computer Connection.
        //public const string CONNECTION_STRING = @"Data Source=LAPTOP-U96L8M1H;Initial Catalog=FlightManagmentDB;Integrated Security=True";

        // IIS Connection String
        public const string CONNECTION_STRING = "Server=LAPTOP-U96L8M1H;Database=FlightManagmentDB;Integrated Security=true";

        public const int TIME_FOR_THREAD_HISTORY = 1000 * 3600 * 24;
    }
}
