using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public interface IAdministratorDAO : IBasicDB<Administrator>
    {
        IList<string> GetAllActionsHistory();
        IList<string> GetAllActionHistoryByDate(DateTime startDate, DateTime endDate);
        IList<string> GetAllActionHistoryByCategory(Categories category);
        IList<string> GetAllActionHistoryByIsSucceed(bool isSucceed);
    }
}
