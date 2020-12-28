using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public interface IUserDAO
    {
        void AddUserName(User user, out long userId);
        bool VerifyNewCustomerEmail(string email);
        void RemoveUserName(User user);
        void UpdateUserName(string oldUserName, string newUserName);
        bool TryChangePasswordForUser(User user, string oldPassword, string newPassword);
        void ForceChangePasswordForUser(User user, string newPassword);
        User GetUserByUserName(string userName);
        User GetUserById(long id);
    }
}
