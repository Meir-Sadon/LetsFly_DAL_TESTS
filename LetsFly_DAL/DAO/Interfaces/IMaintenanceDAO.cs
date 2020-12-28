using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public interface IMaintenanceDAO
    {
        void AddNewAction(Categories category, string action, bool isSucceed);
        void UpdateTicketsAndFlightsHistory();
    }
}
