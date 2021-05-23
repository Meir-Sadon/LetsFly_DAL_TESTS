using LetsFly_DAL.Objects;
using LetsFly_DAL.Objects.Poco_s;
using LetsFly_DAL.UserAndPoco;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, IUserFacadeBase<Customer>, ILoggedInCustomerFacade
    {

        // Buy New Ticket For Current Customer.
        public long PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            long newId = 0;
            if (UserIsValid(token) && flight != null)
            {
                if (flight.Remaining_Tickets > 0)
                {
                    newId = _ticketDAO.Add(new Ticket { Customer_Id = token.User.Id, Flight_Id = flight.Id });
                    flight.Remaining_Tickets--;
                    _flightDAO.Update(flight);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Tickets | LogCategories.Adds, $"Customer: {token.User.UserName} Tried To Purchase New Ticket (Flight: {flight.Id}).", true);
                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Tickets | LogCategories.Adds, $"Customer: {token.User.UserName} Tried To Purchase New Ticket (Flight: {flight.Id}).", false);
                    throw new OutOfTicketsException($"Sorry But The Tickets Are Over For This Flight (Flight Number: {flight.Id}. From: {flight.Origin_Country_Code} To: {flight.Destination_Country_Code} At: {flight.Departure_Time}.)");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Tickets | LogCategories.Adds, $"Anonymous User Tried To Purchase New Ticket (Flight: {flight.Id}).", true);
            return newId;
        }

        // Cancel Ticket From Current Customer.
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            Flight flight = _flightDAO.GetById((int)ticket.Flight_Id);
            if (UserIsValid(token) && ticket != null)
            {
                if (token.User.Id == ticket.Customer_Id)
                {
                    if (flight.Departure_Time < DateTime.Now.AddHours(1))
                        throw new TooLateToCancelTicketException("You Can't Cancel Your Ticket From One Hour Before The Flight");
                    _ticketDAO.Remove(ticket);
                    flight.Remaining_Tickets++;
                    _flightDAO.Update(flight);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Tickets | LogCategories.Deletions, $"Customer: {token.User.UserName} Tried To Cancel His Ticket (Flight: {flight.Id}).", true);
                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Tickets | LogCategories.Deletions, $"Customer: {token.User.UserName} Tried To Cancel His Ticket (Flight: {flight.Id}).", false);
                    throw new TicketNotMatchException("Sorry But You Can't Cancel Ticket Of Another User.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Tickets | LogCategories.Deletions, $"Anonymous User Tried To Cancel His Ticket (Flight: {flight.Id}).", false);
        }

        // Change Details Of Current Customer (Without Password).
        public void MofidyCustomerDetails(LoginToken<Customer> token)
        {
            if (UserIsValid(token))
            {
                User customerUser = _userDAO.GetUserById(token.User.Id);
                if (customerUser != null)
                {
                    _userDAO.UpdateUserName(customerUser.UserName, token.User.UserName);
                    _customerDAO.Update(token.User);
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Customer: {token.User.UserName} Tried To Update His Details.", true);

                }
                else
                {
                    //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Customer: {token.User.UserName} Tried To Update His Details.", false);
                    throw new UserNotExistException($"Sorry, But '{token.User.UserName}' Doe's Not Exist.");
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Anonymous User Tried To Update Some Customer.", false);
        }

        // Change Password For Current Customer.
        public void ChangeMyPassword(LoginToken<Customer> token, string oldPassword, string newPassword)
        {
            if (UserIsValid(token))
            {
                User customerUser = _userDAO.GetUserById(token.User.Id);
                if (customerUser != null)
                {
                    if (_userDAO.TryChangePasswordForUser(customerUser, oldPassword, newPassword))
                    {
                        token.User.ChangePassword(newPassword);
                        //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Customer: {token.User.UserName} Tried To Change His Password.", true);
                    }
                    else
                    {
                        //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Customer: {token.User.UserName} Tried To Change His Password.", false);
                        throw new WrongPasswordException("Your Old Password Is Incorrect!");
                    }
                }
            }
            //else
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Updates, $"Anonymous User Tried To Change Password For Some Custmer. Id: {token.User.Id} ({token.User.UserName}).", false);

        }

        // Search Ticket For Current Customer By Id(If The Ticket Purchased By Current Customer).
        public Ticket GetPurchasedTicketById(LoginToken<Customer> token, int id)
        {
            Ticket ticket = new Ticket();
            if (UserIsValid(token))
            {
                ticket = _ticketDAO.GetById(id);
            }
            if (ticket != null && ticket.Customer_Id == token.User.Id)
                return ticket;
            else
                throw new TicketNotMatchException("No Flight Ticket Found In Your List Tickets With The Sent ID.");
        }

        //Get Full Details For All Tickets By Current Customer.
        public IList<FullTicketDetails> GetAllMyTicketsDetails(LoginToken<Customer> token)
        {
            IList<FullTicketDetails> fullFlightsByCustomer = new List<FullTicketDetails>();
            if (UserIsValid(token))
            {
                fullFlightsByCustomer = _ticketDAO.GetFullTicketsByCustomerId(token.User.Id);
            }
            return fullFlightsByCustomer;
        }

        // Search All The Flights For Current Customer.
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            IList<Flight> flightsByCustomer = new List<Flight>();
            if (UserIsValid(token))
            {
                flightsByCustomer = _flightDAO.GetFlightsByCustomer(token.User);
            }
            return flightsByCustomer;
        }

        // Search All The Tickets For Current Customer.
        public IList<Ticket> GetAllMyTickets(LoginToken<Customer> token)
        {
            IList<Ticket> ticketsByCustomer = new List<Ticket>();
            if (UserIsValid(token))
            {
                ticketsByCustomer = _ticketDAO.GetTicketsByCustomer(token.User);
            }
            return ticketsByCustomer;
        }

        #region Messages Crud Operations

        public ReponseDetails CreateMessage(LoginToken<Customer> token, Message newMessage)
        {
            ReponseDetails response = new ReponseDetails();
            try
            {
                string error = null;
                if (UserIsValid(token))
                {
                    if (token.User.Id == newMessage.SenderId)
                    {
                        if (MessageIsValid(newMessage, out error))
                            response.ResponseMessage = _messageDAO.CreateMessage(newMessage);
                        else
                            response.ResponseMessage += "You're Message Is Invalid: " + error;
                    }
                    else
                        throw new UnauthorizedAccessException("Sorry But You're Unauthorized For This Action");
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage += "Sent A Message Was Fail: " + ex.Message; 
            }
            finally
            {
                //ImHere_backgroundDAO.WriteRequestToLog(LogCategories.Customers | LogCategories.Messages | LogCategories.Adds, $"Customer: {token.User.UserName} Tried To Sent A New Message (Message: {newMessage.Id}).", response.ResponseCode == Enum.ResponseCode.Success,newMessage.ToString(),response.ToString());
            }
            return response;
        }

        public string UpdateMessage(LoginToken<Customer> token, long msgId, Message message)
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

        // Check If Customer User That Sent Is Valid.
        public bool UserIsValid(LoginToken<Customer> token)
        {
            if (token != null && token.User != null)
                return true;
            return false;
        }

        string IUserFacadeBase<Customer>.CreateMessage(LoginToken<Customer> login, Message newMessage)
        {
            throw new NotImplementedException();
        }

        public string UpdateMessage(long msgId, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
