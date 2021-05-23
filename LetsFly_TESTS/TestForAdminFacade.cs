using System;
using LetsFly_DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestForAdminFacade : TestCenter
    {
        /* ========   All Tests ========

           1. CreateNewAdmin                  -- "TestLogin Class" (LoginSuccesfullyAsDAOAdmin).
           2. CreateNewAirline                -- "TestLogin Class" (LoginSuccesfullyAsAirline).
           3. CreateNewCountry                -- "TestCenter" (PrepareDBForTests).
           4. RemoveAdministrator             -- RemoveAdministratorSuccessfully + TryRemoveAdministratorUserThatNotExist.
           5. RemoveAirline                   -- RemoveAirlineSuccessfully + TryRemoveAirlineUserThatNotExist.
           6. RemoveCustomer                  -- RemoveCustomerSuccessfully + TryRemoveCustomerUserThatNotExist.
           7. RemoveCountry                   -- RemoveCustomerSuccessfully + TryRemoveCustomerUserThatNotExist.
           8. UpdateAirlineDetails            -- UpdateAirline.
           9. UpdateCustomerDetails           -- UpdateCustomer.
           10. UpdateCountryDetails           -- UpdateCountry.
           11. ChangeMyPassword               -- TryChangePasswordForAdministrator + WrongPasswordWhenTryChangePasswordForCentralAdmin + WrongPasswordWhenTryChangePasswordForSomeAdmin.
           12. ForceChangePasswordForAirline  -- ChangePasswordForSomeAirline.
           13. ForceChangePasswordForCustomer -- ChangePasswordForSomeCustomer.
           14. GetAdminByUserName             -- GetAdminByUserName.
           15. GetAdminById                   -- GetAdminById.
           16. GetAirlineByUserName           -- GetAirlineByUserName.
           17. GetCustomerByUserName          -- GetCustomerByUserName.
           18. GetAllCustomers                -- GetAllCustomers.


           ========   All Tests ======== */


        // ===== Remove Successfully =====/

        // Remove Administrator Successfully.
        [TestMethod]
        public void RemoveAdministratorSuccessfully()
        {
            Administrator admin = CreateRandomAdministrator();
            admin.AdminNumber = adminFacade.CreateNewAdmin(adminToken, admin);
            adminFacade.RemoveAdministrator(adminToken, admin);
            Assert.AreEqual(adminFacade.GetAdminByUserName(adminToken, admin.UserName).Id, 0);
        }

        // Remove Airline Successfully.
        [TestMethod]
        public void RemoveAirlineSuccessfully()
        {
            AirlineCompany airline = CreateRandomCompany();
            airline.AirlineNumber = adminFacade.CreateNewAirline(adminToken, airline);
            adminFacade.RemoveAirline(adminToken, airline);
            Assert.AreEqual(adminFacade.GetAdminByUserName(adminToken, airline.UserName).Id, 0);
        }

        // Remove Customer Successfully.
        [TestMethod]
        public void RemoveCustomerSuccessfully()
        {
            Customer customer = CreateRandomCustomer();
            customer.CustomerNumber = adminFacade.CreateNewCustomer(customer);
            adminFacade.RemoveCustomer(adminToken, customer);
            Assert.AreEqual(adminFacade.GetCustomerByUserName(adminToken, customer.UserName).Id, 0);
        }

        // Remove Country Successfully.
        [TestMethod]
        public void RemoveCountrySuccessfully()
        {
            adminFacade.RemoveCountry(adminToken, adminFacade.GetCountryByName("Israel"));
            Assert.AreEqual(adminFacade.GetCountryByName("Israel").Id, 0);
        }


        // ===== Get "UserNotExist" When Try Remove =====//

        // Supposed To Get "UserNotExistException" When Try Remove Administrator.
        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void TryRemoveAdministratorUserThatNotExist()
        {
            Administrator admin = CreateRandomAdministrator();
            adminFacade.RemoveAdministrator(adminToken, admin);
        }

        // Supposed To Get "UserNotExistException" When Try Remove Airline.
        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void TryRemoveAirlineUserThatNotExist()
        {
            
            AirlineCompany airline = CreateRandomCompany();
            adminFacade.RemoveAirline(adminToken, airline);
        }

        // Supposed To Get "UserNotExistException" When Try Remove Customer.
        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void TryRemoveCustomerUserThatNotExist()
        {
            
            Customer customer = CreateRandomCustomer();
            adminFacade.RemoveCustomer(adminToken, customer);
        }

        // Supposed To Get "UserNotExistException" When Try Remove Country.
        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void TryRemoveCountryUserThatNotExist()
        {
            
            Country country = new Country("USA");
            adminFacade.RemoveCountry(adminToken, country);
        }

        // ===== Update Details ===== //

        // Update Details For Airline Company.
        [TestMethod]
        public void UpdateAirline()
        {
            
            AirlineCompany airline = CreateRandomCompany();
            airline.AirlineNumber = adminFacade.CreateNewAirline(adminToken, airline);
            airline.AirlineName = "CHANGED!";
            adminFacade.UpdateAirlineDetails(adminToken, airline);
            Assert.AreEqual(adminFacade.GetAirlineByUserName(adminToken, airline.UserName).AirlineName, "CHANGED!");
        }

        // Update Details For Customer.
        [TestMethod]
        public void UpdateCustomer()
        {
            
            Customer customer = CreateRandomCustomer();
            customer.CustomerNumber = adminFacade.CreateNewCustomer(customer);
            customer = adminFacade.GetCustomerByUserName(adminToken, customer.UserName);
            customer.FirstName = "CHANGED!";
            adminFacade.UpdateCustomerDetails(adminToken, customer);
            Assert.AreEqual(adminFacade.GetCustomerByUserName(adminToken, customer.UserName).FirstName, "CHANGED!");
        }

        // Update Details For Country.
        [TestMethod]
        public void UpdateCountry()
        {
            
            Country country = new Country("USA");
            country.Id = adminFacade.CreateNewCountry(adminToken, country);
            country.Country_Name = "China";
            adminFacade.UpdateCountryDetails(adminToken, country);
            Assert.AreEqual(adminFacade.GetCountryByName(country.Country_Name).Country_Name, "China");
        }


        //  Change Password Succesfully =====//

        // Try Change Password Successfuly For Administrator.
        [TestMethod]
        public void TryChangePasswordForAdministrator()
        {
            
            Administrator admin = CreateRandomAdministrator();
            adminFacade.CreateNewAdmin(adminToken, admin);
            User user = new User(admin.UserName, admin.Password, UserTypes.Administrator, false);
            FlyingCenterSystem.GetUserAndFacade(user, out ILogin token, out FacadeBase facade);
            LoginToken<Administrator> newToken = token as LoginToken<Administrator>;
            LoggedInAdministratorFacade newFacade = facade as LoggedInAdministratorFacade;
            newFacade.ChangeMyPassword(newToken, "123".ToUpper(), "newPassword".ToUpper());
            Assert.AreEqual(newToken.User.Password.ToUpper(), "newPassword".ToUpper());
        }

        // Change Password Successfuly For Airline Company.
        [TestMethod]
        public void ChangePasswordForSomeAirline()
        {
            
            AirlineCompany airline = CreateRandomCompany();
            airline.AirlineNumber = adminFacade.CreateNewAirline(adminToken, airline);
            adminFacade.ForceChangePasswordForAirline(adminToken, airline, "newPassword".ToUpper());
            Assert.AreEqual(adminFacade.GetAirlineByUserName(adminToken, airline.UserName).Password, "newPassword".ToUpper());
        }

        // Change Password Successfuly For Customer.
        [TestMethod]
        public void ChangePasswordForSomeCustomer()
        {
            
            Customer customer = CreateRandomCustomer();
            customer.CustomerNumber = adminFacade.CreateNewCustomer(customer);
            adminFacade.ForceChangePasswordForCustomer(adminToken, customer, "newPassword".ToUpper());
            Assert.AreEqual(adminFacade.GetCustomerByUserName(adminToken, customer.UserName).Password, "newPassword".ToUpper());
        }

        // ===== Get "WrongPasswordException" When Try Change Password =====//

        // Supposed To Get "WrongPasswordException" When Try Change Password For Central Administrator.
        [TestMethod]
        [ExpectedException(typeof(CentralAdministratorException))]
        public void WrongPasswordWhenTryChangePasswordForCentralAdmin()
        {
            
            adminFacade.ChangeMyPassword(adminToken, "123456", "newPassword");
        }

        // Supposed To Get "WrongPasswordException" When Try Change Password For Some Administrator.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryChangePasswordForSomeAdmin()
        {
            
            Administrator admin = CreateRandomAdministrator();
            adminFacade.CreateNewAdmin(adminToken, admin);
            User user = new User(admin.UserName, admin.Password, UserTypes.Administrator, false);
            FlyingCenterSystem.GetUserAndFacade(user, out ILogin token, out FacadeBase facade);
            LoginToken<Administrator> newToken = token as LoginToken<Administrator>;
            adminFacade.ChangeMyPassword(newToken, "123345", "newPassword");
        }


        // ===== Search Functions =====//

        // Search Some Admin By User Name.
        [TestMethod]
        public void GetAdminByUserName()
        {
            
            Administrator admin = CreateRandomAdministrator();
            admin.AdminNumber = adminFacade.CreateNewAdmin(adminToken, admin);
            Assert.AreNotEqual(adminFacade.GetAdminByUserName(adminToken, admin.UserName), null);
        }

        // Search Some Admin By Id.
        [TestMethod]
        public void GetAdminById()
        {
            
            Administrator admin = CreateRandomAdministrator();
            admin.AdminNumber = adminFacade.CreateNewAdmin(adminToken, admin);
            Assert.AreNotEqual(adminFacade.GetAdminById(adminToken, (int)admin.Id), null);
        }

        // Search Some Customer By User Name.
        [TestMethod]
        public void GetAirlineByUserName()
        {
            
            AirlineCompany airline = CreateRandomCompany();
            airline.AirlineNumber = adminFacade.CreateNewAirline(adminToken, airline);
            Assert.AreNotEqual(adminFacade.GetAirlineByUserName(adminToken, airline.UserName), null);
        }

        // Search Some Customer By User Name.
        [TestMethod]
        public void GetCustomerByUserName()
        {
            
            Customer customer = CreateRandomCustomer();
            customer.CustomerNumber = adminFacade.CreateNewCustomer(customer);
            Assert.AreNotEqual(adminFacade.GetCustomerByUserName(adminToken, customer.UserName), null);
        }

        // Get All Customers.
        [TestMethod]
        public void GetAllCustomers()
        {
            
            Customer customer = CreateRandomCustomer();
            customer.CustomerNumber = adminFacade.CreateNewCustomer(customer);
            Assert.AreEqual(adminFacade.GetAllCustomers(adminToken).Count, 2);
        }
    }
}
