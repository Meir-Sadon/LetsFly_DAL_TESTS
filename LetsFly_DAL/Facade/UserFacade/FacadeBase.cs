using LetsFly_DAL.DAO.Interfaces;
using LetsFly_DAL.DAO.MSSQL;
using LetsFly_DAL.Objects.Poco_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public abstract class FacadeBase
    {
        protected IAdministratorDAO _adminDAO;
        protected IAirlineDAO _airlineDAO;
        protected ICustomerDAO _customerDAO;
        protected IUserDAO _userDAO;
        protected IFlightDAO _flightDAO;
        protected ITicketDAO _ticketDAO;
        protected ICountryDAO _countryDAO;
        protected IMessageDAO _messageDAO;
        protected static IMaintenanceDAO _backgroundDAO;

        public FacadeBase()
        {
            _adminDAO = new AdministratorDAOMSSQL();
            _airlineDAO = new AirlineDAOMSSQL();
            _customerDAO = new CustomerDAOMSSQL();
            _userDAO = new UserDAOMSSQL();
            _flightDAO = new FlightDAOMSSQL();
            _ticketDAO = new TicketDAOMSSQL();
            _countryDAO = new CountryDAOMSSQL();
            _messageDAO = new MessageDAOMSSQL();
            _backgroundDAO = new MaintenanceDAOMSSQL();
        }

        public static void WriteToLog(Log logRow)
        {
            _backgroundDAO.WriteToLog(logRow);
        }
    }
}
