﻿using LetsFly_DAL.UserAndPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL
{
    interface ILoggedInCustomerFacade
    {
        long PurchaseTicket(LoginToken<Customer> token, Flight flight);
        void CancelTicket(LoginToken<Customer> token, Ticket ticket);
        void MofidyCustomerDetails(LoginToken<Customer> token);
        void ChangeMyPassword(LoginToken<Customer> token, string oldPassword, string newPassword);
        Ticket GetPurchasedTicketById(LoginToken<Customer> token, int id);
        IList<FullTicketDetails> GetAllMyTicketsDetails(LoginToken<Customer> token);
        IList<Flight> GetAllMyFlights(LoginToken<Customer> token);
        IList<Ticket> GetAllMyTickets(LoginToken<Customer> token);
        bool UserIsValid(LoginToken<Customer> token);
    }
}
