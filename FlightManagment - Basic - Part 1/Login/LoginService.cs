using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagment___Basic___Part_1
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
            if (userDetails.Type == UserType.Administrator && userDetails.UserName.ToUpper() == FlyingCenterConfig.ADMIN_NAME.ToUpper())
            {
                if (userDetails.Password.ToUpper() == FlyingCenterConfig.ADMIN_PASSWORD.ToUpper())
                {
                    token = new LoginToken<Administrator>
                    {
                        User = new Administrator
                        (
                            0, //Admin Number 
                            0, //Id
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
                            case UserType.Administrator:
                                {
                                    Administrator admin = _administratorDAO.GetById(user.Id);
                                    token = new LoginToken<Administrator>
                                    {
                                        User = new Administrator
                                    (
                                        admin.Admin_Number,
                                        user.Id,
                                        user.UserName,
                                        user.Password
                                    )
                                    };
                                    facade = new LoggedInAdministratorFacade();
                                    return true;
                                }
                            case UserType.Airline:
                                {
                                    AirlineCompany airline = _airlineDAO.GetById(user.Id);
                                    token = new LoginToken<AirlineCompany>
                                    {
                                        User = new AirlineCompany
                                    (
                                        airline.Airline_Number,
                                        user.Id,
                                        user.UserName,
                                        user.Password,
                                        airline.Airline_Name,
                                        airline.Country_Code
                                    )
                                    };
                                    facade = new LoggedInAirlineFacade();
                                    return true;
                                }
                            case UserType.Customer:
                                {
                                    Customer customer = _customerDAO.GetById(user.Id);
                                    token = new LoginToken<Customer>
                                    {
                                        User = new Customer
                                        (
                                            customer.Customer_Number,
                                            user.Id,
                                            user.UserName,
                                            user.Password,
                                            customer.First_Name,
                                            customer.Last_Name,
                                            customer.Address,
                                            customer.Phone_No,
                                            customer.Credit_Card_Number
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
