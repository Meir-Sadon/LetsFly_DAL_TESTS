using LetsFly_DAL.Objects.Poco_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    // Class With All The Options That Admin Can Do.
    public class LoggedInAdministratorFacade : AnonymousUserFacade, IUserFacadeBase<Administrator>, ILoggedInAdministratorFacade
    {

        // Create New Administrator.
        public long CreateNewAdmin(LoginToken<Administrator> token, Administrator admin)
        {
            long adminNumber = 0;
            if (token.User.UserName.ToUpper() == FlyingCenterConfig.ADMIN_NAME && token.User.Password == FlyingCenterConfig.ADMIN_PASSWORD && admin != null)
            {
                if (admin.UserName.ToUpper() == FlyingCenterConfig.ADMIN_NAME)
                {
                    throw new UserAlreadyExistException($"Sorry, But {admin.UserName} Already Exist. Please Try Another User Name");
                }
                User adminUser = _userDAO.GetUserById(admin.Id);
                if (adminUser == null)
                {
                    _userDAO.AddUserName(new User(admin.UserName, admin.Password, UserTypes.Administrator, false), out long userId);
                    admin.Id = userId;
                    adminNumber = _adminDAO.Add(admin);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Adds, $"Admin {token.User.UserName} Tried To Create New Administrator. Id: {admin.Id} ({admin.UserName}).", false);
                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Adds, $"Admin {token.User.UserName} Tried To Create New Administrator. Id: {admin.Id} ({admin.UserName}).", false);
                    throw new UserAlreadyExistException($"Sorry, But '{admin.UserName}' Already Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Adds, $"Not Qualified User Tried To Create New Admin.", false);
            return adminNumber;
        }

        // Create New Airline Company.
        public long CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            long airlineNumber = 0;
            if (UserIsValid(token) && airline != null)
            {
                User airlineUser = _userDAO.GetUserById(airline.Id);
                if (airlineUser == null)
                {
                    _userDAO.AddUserName(new User(airline.UserName, airline.Password, UserTypes.Airline, false), out long userId);
                    airline.Id = userId;
                    airlineNumber = _airlineDAO.Add(airline);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Adds, $"Admin {token.User.UserName} Tried To Create New Airline Company. Id: {airline.Id} ({airline.UserName}).", true);
                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Adds, $"Admin {token.User.UserName} Tried To Create New Airline Company. Id: {airline.Id} ({airline.UserName}).", true);
                    throw new UserAlreadyExistException($"Sorry, But '{airline.UserName}' Already Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Adds, $"Anonymous User Tried To Create New Airline Company. Id: {airline.Id} ({airline.UserName}).", false);
            return airlineNumber;
        }

        // Create New Country.
        public long CreateNewCountry(LoginToken<Administrator> token, Country country)
        {
            long newId = 0;
            if (UserIsValid(token) && country != null)
            {
                newId = _countryDAO.Add(country);
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Countries | LogCategories.Adds, $"Admin: {token.User.UserName} Tried To Create New Country. Id: {newId} ({country.Country_Name}).", true);
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Countries | LogCategories.Adds, $"Anonymous User Tried To Create New Country. Id: {country.Id} ({country.Country_Name}).", false);

            return newId;
        }

        // Remove Some DAO Administrator.
        public void RemoveAdministrator(LoginToken<Administrator> token, Administrator admin)
        {
            if (token.User.UserName.ToUpper() == FlyingCenterConfig.ADMIN_NAME && token.User.Password == FlyingCenterConfig.ADMIN_PASSWORD && admin != null)
            {
                User adminUser = _userDAO.GetUserById(admin.Id);
                if (adminUser != null)
                {
                    _adminDAO.Remove(admin);
                    _userDAO.RemoveUserName(adminUser);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Deletions, $"Admin {token.User.UserName} Tried To Delete Some Admin. Id: {admin.Id} ({admin.UserName}).", true);
                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Deletions, $"Admin {token.User.UserName} Tried To Delete Some Admin. Id: {admin.Id} ({admin.UserName}).", false);
                    throw new UserNotExistException($"Sorry, But '{admin.UserName}' Does Not Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Deletions, $"Not Qualified User Tried To Delete Some Admin. Id: {admin.Id} ({admin.UserName}).", false);
        }

        // Remove Some Airline Company.
        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (UserIsValid(token) && airline != null)
            {
                User airlineUser = _userDAO.GetUserById(airline.Id);
                if (airlineUser != null)
                {
                    _airlineDAO.Remove(airline);
                    _userDAO.RemoveUserName(airlineUser);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Deletions, $"Admin {token.User.UserName} Tried Delete Some Admin. Id: {airline.Id} ({airline.UserName}).", true);

                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Deletions, $"Admin {token.User.UserName} Tried Delete Some Admin. Id: {airline.Id} ({airline.UserName}).", false);
                    throw new UserNotExistException($"Sorry, But '{airline.UserName}' Does Not Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Deletions, $"Anonymous User Tried Delete Some Airline Comapny. Id: {airline.Id} ({airline.UserName}).", false);
        }

        //Remove Some Customer.
        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (UserIsValid(token) && customer != null)
            {
                User customerUser = _userDAO.GetUserById(customer.Id);
                if (customerUser != null)
                {
                    _customerDAO.Remove(customer);
                    _userDAO.RemoveUserName(customerUser);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Deletions, $"Admin {token.User.UserName} Tried Delete Some Admin. Id: {customer.Id} ({customer.UserName}).", true);
                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Deletions, $"Admin {token.User.UserName} Tried Delete Some Admin. Id: {customer.Id} ({customer.UserName}).", false);
                    throw new UserNotExistException($"Sorry, But '{customer.UserName}' Does Not Exist.");
                }
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Deletions, $"Admin {token.User.UserName} Tried Delete Some Admin. Id: {customer.Id} ({customer.UserName}).", customerUser != null);
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Deletions, $"Anonymous User Tried Delete Some Airline Comapny. Id: {customer.Id} ({customer.UserName}).", false);
        }

        //Remove Some Country.
        public void RemoveCountry(LoginToken<Administrator> token, Country country)
        {
            if (UserIsValid(token) && country != null)
            {
                if (_countryDAO.Remove(country))
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Countries | LogCategories.Deletions, $"Admin {token.User.UserName} Tried Delete Some Country. Id: {country.Id} ({country.Country_Name}).", true);
                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Countries | LogCategories.Deletions, $"Admin {token.User.UserName} Tried Delete Some Country. Id: {country.Id} ({country.Country_Name}).", false);
                    throw new UserNotExistException($"Sorry, But '{country.Country_Name}' Does Not Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Countries | LogCategories.Deletions, $"Anonymous User Tried Delete Some Country. Id: {country.Id} ({country.Country_Name}).", false);
        }

        // Update Details For Some Administrator.
        public void UpdateAdminDetails(LoginToken<Administrator> token, Administrator admin)
        {
            if (UserIsValid(token))
            {
                if (admin != null && ReferenceEquals(token.User.UserName, admin.UserName) || token.User.UserName.ToUpper() == FlyingCenterConfig.ADMIN_NAME)
                {
                    User adminUser = _userDAO.GetUserById(admin.Id);
                    if (adminUser != null)
                    {
                        if (admin.UserName != FlyingCenterConfig.ADMIN_NAME)
                            _userDAO.UpdateUserName(adminUser.UserName, admin.UserName);
                        else
                            throw new CentralAdministratorException("The UserName Cannot Be Changed To The Same Name As The  Central Administrator UserName.");
                        _adminDAO.Update(admin);
                        //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Updates, $"Admin: {token.User.UserName} Tried Update Some Admin. Id: {admin.Id} ({admin.UserName}).", true);
                    }
                    else
                    {
                        //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Updates, $"Admin: {token.User.UserName} Tried Update Some Admin. Id: {admin.Id} ({admin.UserName}).", false);
                        throw new UserNotExistException($"Sorry, But '{admin.UserName}' Does Not Exist.");
                    }
                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Updates, $"Not Qualified User Tried Update Some Admin. Id: {admin.Id} ({admin.UserName}).", false);
                    throw new CentralAdministratorException("Only Central Administrator Or Who Updateds Himself Can Update Details.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Updates, $"Anonymous User Tried Update Some Admin. Id: {admin.Id} ({admin.UserName}).", false);
        }

        // Update Details For Some Airline Company.
        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (UserIsValid(token) && airline != null)
            {
                User airlineUser = _userDAO.GetUserById(airline.Id);
                if (airlineUser != null)
                {
                    _userDAO.UpdateUserName(airlineUser.UserName, airline.UserName);
                    _airlineDAO.Update(airline);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Updates, $"Admin: {token.User.UserName} Tried To Update Details For Some Airline Company. Id: {airline.Id} ({airline.UserName}).", true);

                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Updates, $"Admin: {token.User.UserName} Tried To Update Details For Some Airline Company. Id: {airline.Id} ({airline.UserName}).", false);
                    throw new UserNotExistException($"Sorry, But '{airline.UserName}' Does Not Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Updates, $"Anonymous User Tried To Update Details For Some Airline Company. Id: {airline.Id} ({airline.UserName}).", false);
        }

        // Update Details For Some Customer.
        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            if (UserIsValid(token) && customer != null)
            {
                User customerUser = _userDAO.GetUserById(customer.Id);
                if (customerUser != null)
                {
                    _userDAO.UpdateUserName(customerUser.UserName, customer.UserName);
                    _customerDAO.Update(customer);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Admin: {token.User.UserName} Tried To Update Some Customer. Id: {customer.Id} ({customer.UserName}).", true);

                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Admin: {token.User.UserName} Tried To Update Some Customer. Id: {customer.Id} ({customer.UserName}).", false);
                    throw new UserNotExistException($"Sorry, But '{customer.UserName}' Does Not Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Anonymous User Tried To Update Some Customer. Id: {customer.Id} ({customer.UserName}).", false);
        }

        // Update Details For Some Country.
        public void UpdateCountryDetails(LoginToken<Administrator> token, Country country)
        {
            if (UserIsValid(token) && country != null)
            {
                if (_countryDAO.Update(country))
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Countries | LogCategories.Updates, $"Admin: {token.User.UserName} Tried To Update Some Country. Id: {country.Id} ({country.Country_Name}).", true);
                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Countries | LogCategories.Updates, $"Admin: {token.User.UserName} Tried To Update Some Country. Id: {country.Id} ({country.Country_Name}).", false);
                    throw new ArgumentException($"Sorry, But This Country Does Not Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Countries | LogCategories.Updates, $"Anonymous User Tried To Update Some Country. Id: {country.Id} ({country.Country_Name}).", false);
        }

        // Try Change Password For Admin.
        public void ChangeMyPassword(LoginToken<Administrator> token, string oldPassword, string newPassword)
        {
            if (token.User.UserName == FlyingCenterConfig.ADMIN_NAME)
            {
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Updates, $"{token.User.UserName} Tried To Change His Password.", false);
                throw new CentralAdministratorException("It's Not possible To Change Password For Central Administrator");
            }
            if (UserIsValid(token))
            {
                User adminUser = _userDAO.GetUserById(token.User.Id);
                if (adminUser != null)
                {
                    if (_userDAO.TryChangePasswordForUser(adminUser, oldPassword, newPassword))
                    {
                        token.User.ChangePassword(newPassword);
                        //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Updates, $"{token.User.UserName} Tried To Change His Password.", true);
                    }
                    else
                    {
                        //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Updates, $"{token.User.UserName} Tried To Change His Password.", false);
                        throw new WrongPasswordException("Your Old Password Is Incorrect!");
                    }
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Administrators | LogCategories.Updates, $"Anonymous User Tried To Change Password For Some Admin. Id: {token.User.Id} ({token.User.UserName}).", false);

        }

        // Force Change Password For Some Airline.
        public void ForceChangePasswordForAirline(LoginToken<Administrator> token, AirlineCompany airline, string newPassword)
        {
            if (UserIsValid(token) && airline != null)
            {
                User airlineUser = _userDAO.GetUserById(airline.Id);
                if (airlineUser != null)
                {
                    _userDAO.ForceChangePasswordForUser(airlineUser, newPassword);
                    airline.ChangePassword(newPassword);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Updates, $"Admin: {token.User.UserName} Tried To Change Password In Force For Some Airline Company. Id: {airline.Id} ({airline.UserName}).", true);

                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Updates, $"Admin: {token.User.UserName} Tried To Change Password In Force For Some Airline Company. Id: {airline.Id} ({airline.UserName}).", false);
                    throw new UserNotExistException($"Sorry, But '{airline.UserName}' Is Not Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.AirlineCompanies | LogCategories.Updates, $"Anonymous User Tried To Change Password In Force For Some Airline Company. Id: {airline.Id} ({airline.UserName}).", false);
        }

        // Force Change Password For Some Customer.
        public void ForceChangePasswordForCustomer(LoginToken<Administrator> token, Customer customer, string newPassword)
        {
            if (UserIsValid(token) && customer != null)
            {
                User customerUser = _userDAO.GetUserById(customer.Id);
                if (customerUser != null)
                {
                    _userDAO.ForceChangePasswordForUser(customerUser, newPassword);
                    customer.ChangePassword(newPassword);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Admin: {token.User.UserName} Tried To Change Password In Force For Some Customer. Id: {customer.Id} ({customer.UserName}).", true);

                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Admin: {token.User.UserName} Tried To Change Password In Force For Some Customer. Id: {customer.Id} ({customer.UserName}).", false);
                    throw new UserNotExistException($"Sorry, But '{customer.UserName}' Is Not Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Anonymous User Tried To Change Password In Force For Some Customer. Id: {customer.Id} ({customer.UserName}).", false);

        }

        // Search Admin By Id.
        public Administrator GetAdminById(LoginToken<Administrator> token, int id)
        {
            Administrator admin = new Administrator();
            if (UserIsValid(token))
            {
                admin = _adminDAO.GetById(id);
            }
            return admin;
        }

        // Search Admin By UserName.
        public Administrator GetAdminByUserName(LoginToken<Administrator> token, string userName)
        {
            Administrator admin = new Administrator();
            if (UserIsValid(token))
            {
                if (userName.ToUpper() == FlyingCenterConfig.ADMIN_NAME.ToUpper())
                {
                    return FlyingCenterConfig.basicToken.User;
                }
                User adminUser = _userDAO.GetUserByUserName(userName);
                if (adminUser != null)
                {
                    admin = _adminDAO.GetById(adminUser.Id);
                    admin.UserName = adminUser.UserName;
                }
            }
            return admin;
        }

        // Search Airline By UserName.
        public AirlineCompany GetAirlineByUserName(LoginToken<Administrator> token, string userName)
        {
            AirlineCompany airline = new AirlineCompany();
            if (UserIsValid(token))
            {
                User airlineUser = _userDAO.GetUserByUserName(userName);
                if (airlineUser != null)
                {
                    airline = _airlineDAO.GetById(airlineUser.Id);
                }
            }
            return airline;
        }

        // Search Customer By UserName.
        public Customer GetCustomerByUserName(LoginToken<Administrator> token, string userName)
        {
            Customer customer = new Customer();
            if (UserIsValid(token))
            {
                User customerUser = _userDAO.GetUserByUserName(userName);
                if (customerUser != null)
                {
                    customer = _customerDAO.GetById(customerUser.Id);
                }
            }
            return customer;
        }

        // Search All Customers.
        public IList<Customer> GetAllCustomers(LoginToken<Administrator> token)
        {
            IList<Customer> customers = new List<Customer>();
            if (UserIsValid(token))
            {
                customers = _customerDAO.GetAll();
            }
            return customers;
        }


        #region Messages Crud Operations

        public string CreateMessage(LoginToken<Administrator> token, Message newMessage)
        {
            string error = null;
            if (UserIsValid(token))
            {

                if (token.User.Id == newMessage.SenderId)
                {
                    if (MessageIsValid(newMessage, out error))
                        return _messageDAO.CreateMessage(newMessage);
                    else
                        return error;
                }
                else
                    return "Sender ID Must Be The Same As User ID";
            }
            return error;
        }

        public string UpdateMessage(long msgId, Message message)
        {
            if (message != null && message.MsgId != 0)
                return "Message Id Is Mandatory Field";
            else
                return _messageDAO.UpdateMessage(msgId, message);
        }

        public string DeleteMessage(long msgId)
        {
            if (msgId == 0)
                return "Message Id Is Mandatory Field";
            else
                return _messageDAO.DeleteMessage(msgId);
        }

        public IList<Message> GetAllMessagesByUser(long userId)
        {
            if (userId != 0)
                return _messageDAO.GetAllMessagesByUser(userId);
            else
                return null;
        }

        public Message GetMessageById(long messageId)
        {
            if (messageId == 0)
                return null;
            else
                return _messageDAO.GetMessageById(messageId);
        }

        private bool MessageIsValid(Message message, out string error)
        {
            error = "";
            if (message != null)
            {
                if (message.SenderId == 0)
                    error += "Message Must Sent With Sender Code";
                if (message.ReceiverId == 0)
                    error += "\nMessage Must Sent With Receiver Code";
                if (String.IsNullOrEmpty(message.Title) && String.IsNullOrEmpty(message.Body))
                    error += "\nMessage Must Sent At Least With Title Or Body Code";
            }
            else
            {
                error += "Message Details Is Empty";
            }
            return String.IsNullOrEmpty(error);
        }

        #endregion



        // Check If User Admin That Sent Is Valid.
        public bool UserIsValid(LoginToken<Administrator> token)
        {
            if (token != null && token.User != null)
                return true;
            return false;
        }

    }
}
