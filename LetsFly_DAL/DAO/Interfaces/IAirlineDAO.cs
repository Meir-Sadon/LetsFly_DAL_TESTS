using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public interface IAirlineDAO : IBasicDB<AirlineCompany>
    {
        IList<AirlineCompany> GetAllAirlinesByCountry(int countryId);
        AirlineCompany GetByName(string companyName);
        void ChangePassword(AirlineCompany airline);
    }
}
