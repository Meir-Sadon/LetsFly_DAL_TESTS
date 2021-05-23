using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public class LoginService : ILoginService
    {
        readonly private IAirlineDAO _airlineDAO = new AirlineDAOMSSQL();
        readonly private ICustomerDAO _customerDAO = new CustomerDAOMSSQL();
        readonly private IAdministratorDAO _administratorDAO = new AdministratorDAOMSSQL();
        IUserDAO _userDAO = new UserDAOMSSQL();

        public bool TryLogin(User userDetails, out ILogin token, out FacadeBase facade)
        {
            token = null;
            facade = new AnonymousUserFacade();
            // Default Admin.
            if (userDetails.Type == UserTypes.Administrator && userDetails.UserName.ToUpper() == FlyingCenterConfig.ADMIN_NAME.ToUpper())
            {
                if (userDetails.Password.ToUpper() == FlyingCenterConfig.ADMIN_PASSWORD.ToUpper())
                {
                    token = new LoginToken<IUser>
                    {
                        User = new Administrator
                        (
                            0, //Admin Number 
                            0, //UserId
                            FlyingCenterConfig.ADMIN_NAME,
                            FlyingCenterConfig.ADMIN_PASSWORD
                        )
                    };
                    facade = new LoggedInAdministratorFacade();
                    return true;
                }
                else
                {
                    throw new WrongPasswordException("Sorry, But Your Password Isn't Match To Your User Name.");
                }
            }

            // DAO Users.
            User user = _userDAO.GetUserByUserName(userDetails.UserName);
            if (user != null)
            {
                if (user.UserName.ToUpper() == userDetails.UserName.ToUpper())
                {
                    if (userDetails.Password.ToUpper() == user.Password.ToUpper())
                    {
                        switch (userDetails.Type)
                        {
                            case UserTypes.Administrator:
                                {
                                    Administrator admin = _administratorDAO.GetById(user.Id);
                                    token = new LoginToken<IUser>
                                    {
                                        User = new Administrator
                                    (
                                        admin.AdminNumber,
                                        user.Id,
                                        user.UserName,
                                        user.Password
                                    )
                                    };
                                    facade = new LoggedInAdministratorFacade();
                                    return true;
                                }
                            case UserTypes.Airline:
                                {
                                    AirlineCompany airline = _airlineDAO.GetById(user.Id);
                                    token = new LoginToken<IUser>
                                    {
                                        User = new AirlineCompany
                                    (
                                        airline.AirlineNumber,
                                        user.Id,
                                        user.UserName,
                                        user.Password,
                                        airline.AirlineName,
                                        airline.CountryCode
                                    )
                                    };
                                    facade = new LoggedInAirlineFacade();
                                    return true;
                                }
                            case UserTypes.Customer:
                                {
                                    Customer customer = _customerDAO.GetById(user.Id);
                                    token = new LoginToken<IUser>
                                    {
                                        User = new Customer
                                        (
                                            customer.CustomerNumber,
                                            user.Id,
                                            user.UserName,
                                            user.Password,
                                            customer.FirstName,
                                            customer.LastName,
                                            customer.Address,
                                            customer.PhoneNo,
                                            customer.CreditCardNumber
                                        )
                                    };
                                    return true;
                                }
                            default:
                                    return false;
                        }
                    }
                    else
                        throw new WrongPasswordException("Sorry, But Your Password Doe's Not Match To Your User Name.");
                }
                else
                    throw new UserNotExistException($"Sorry, But {userDetails.UserName} Does Not Exist.");

            }
            return false;
        }
    }
}
