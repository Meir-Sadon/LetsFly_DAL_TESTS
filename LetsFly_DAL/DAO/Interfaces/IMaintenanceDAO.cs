using LetsFly_DAL.Objects.Poco_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public interface IMaintenanceDAO
    {
        void WriteToLog(Log logRequest);
                
        void UpdateTicketsAndFlightsHistory();
    }
}
