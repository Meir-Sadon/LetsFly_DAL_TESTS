﻿using System;
using LetsFly_DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestLogin : TestCenter
    {
        /* ========   All Tests ========

           1. GetPasswordExceptionForDefaultAdmin -- WrongPasswordWhenTryLoginAsDefaultAdmin.
           2. GetPasswordExceptionForDAOAdmin     -- WrongPasswordWhenTryLoginAsAdmin.
           3. LoginSuccessfulyForDefaultAdmin     -- LoginSuccesfullyAsDefaultAdmin.
           4. LoginSuccessfulyForDAOAdmin         -- LoginSuccesfullyAsDAOAdmin.
           5. GetPasswordExceptionForAirline      -- WrongPasswordWhenTryLoginAsAirline.
           6. LoginSuccessfulyForAirline          -- LoginSuccesfullyAsAirline.
           7. GetPasswordExceptionForCustomer     -- WrongPasswordWhenTryLoginAsCustomer.
           8. LoginSuccessfulyForCustomer         -- LoginSuccesfullyAsCustomer.

           ======= All Tests ======= */

        // Supposed To Get Password Exception For Login To Default Admin.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryLoginAsDefaultAdmin()
        {
            FlyingCenterSystem.GetUserAndFacade(new User(FlyingCenterConfig.ADMIN_NAME, "WrongPassword", UserTypes.Administrator, false), out ILogin token, out FacadeBase facade);
        }

        // Supposed To Get Password Exception For Login To DAO Admin.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryLoginAsAdmin()
        {
            Administrator admin = CreateRandomAdministrator();
            admin.AdminNumber = adminFacade.CreateNewAdmin(adminToken, admin);
            User user = new User(admin.UserName, "ErrorPassword", UserTypes.Administrator, false);
            FlyingCenterSystem.GetUserAndFacade(user, out ILogin token, out FacadeBase facade);
        }

        // Login Succesfully As Dfault Admin.
        [TestMethod]
        public void LoginSuccesfullyAsDefaultAdmin()
        {
            User user = new User(FlyingCenterConfig.ADMIN_NAME, FlyingCenterConfig.ADMIN_PASSWORD, UserTypes.Administrator, false);
            FlyingCenterSystem.GetUserAndFacade(user , out ILogin token, out FacadeBase facade);
            LoginToken<Administrator> newAdminToken = token as LoginToken<Administrator>;
            Assert.AreNotEqual(newAdminToken, null);
        }

        // Login Succesfully As DAO Admin.
        [TestMethod]
        public void LoginSuccesfullyAsDAOAdmin()
        {
            Administrator admin = CreateRandomAdministrator();
            admin.AdminNumber = adminFacade.CreateNewAdmin(adminToken, admin);
            User user = new User(admin.UserName, admin.Password, UserTypes.Administrator, false);
            FlyingCenterSystem.GetUserAndFacade(user, out ILogin token, out FacadeBase facade);
            LoginToken<Administrator> newAdminToken = token as LoginToken<Administrator>;
            LoggedInAdministratorFacade newAdminFacade = facade as LoggedInAdministratorFacade;
            Assert.AreNotEqual(newAdminToken, null);
            Assert.AreNotEqual(newAdminFacade, null);
        }

        // Supposed To Get Password Exception For Login To Airline.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryLoginAsAirline()
        {
            AirlineCompany airline = CreateRandomCompany();
            airline.AirlineNumber = adminFacade.CreateNewAirline(adminToken, airline);
            User user = new User(airline.UserName, "ErrorPassword", UserTypes.Airline, false);
            FlyingCenterSystem.GetUserAndFacade(user, out ILogin token, out FacadeBase facade);
        }

        // Login Succesfully As Airline.
        [TestMethod]
        public void LoginSuccesfullyAsAirline()
        {
            AirlineCompany airline = CreateRandomCompany();
            airline.AirlineNumber = adminFacade.CreateNewAirline(adminToken, airline);
            User user = new User(airline.UserName, "123", UserTypes.Airline, false);
            FlyingCenterSystem.GetUserAndFacade(user, out ILogin token, out FacadeBase facade);
            LoginToken<AirlineCompany> newAirlineToken = token as LoginToken<AirlineCompany>;
            LoggedInAirlineFacade newAirlineFacade = facade as LoggedInAirlineFacade;
            Assert.AreNotEqual(newAirlineToken, null);
            Assert.AreNotEqual(newAirlineFacade, null);
        }

        // Supposed To Get Password Exception For Login To Customer.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryLoginAsCustomer()
        {
            Customer customer = CreateRandomCustomer();
            adminFacade.CreateNewCustomer(customer);
            User user = new User(customer.UserName, "ErrorPassword", UserTypes.Customer, false);
            FlyingCenterSystem.GetUserAndFacade(user, out ILogin token, out FacadeBase facade);
        }

        // Login Succesfully As Customer.
        [TestMethod]
        public void LoginSuccesfullyAsCustomer()
        {
            Customer customer = CreateRandomCustomer();
            adminFacade.CreateNewCustomer(customer);
            User user = new User(customer.UserName, "123", UserTypes.Customer, false);
            FlyingCenterSystem.GetUserAndFacade(user, out ILogin token, out FacadeBase facade);
            LoggedInCustomerFacade newCustomerFacade = facade as LoggedInCustomerFacade;
            Assert.AreNotEqual(customer, null);
            Assert.AreNotEqual(newCustomerFacade, null);
        }

        [TestMethod]
        public void GetAnonymousUserFacade()
        {
            Customer customer = CreateRandomCustomer();
            adminFacade.CreateNewCustomer(customer);
            User user = new User(customer.UserName, "123", UserTypes.Customer, false);
            FlyingCenterSystem.GetUserAndFacade(user, out ILogin token, out FacadeBase facade);
            LoginToken<IUser> myToken = token as LoginToken<IUser>;
            Assert.AreEqual(myToken, null);
            Assert.AreNotEqual(facade, null);
        }
    }
}
