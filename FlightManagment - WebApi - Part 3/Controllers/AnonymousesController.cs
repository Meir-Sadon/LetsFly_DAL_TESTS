using EasyNetQ;
using FlightManagment___Basic___Part_1;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace FlightManagment___WebApi___Part_3
{
    /// <summary>
    /// Controller For All Functions Of Anonymous Facade.
    /// </summary>
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class AnonymousController : ApiController
    {
        private AnonymousUserFacade facade = new AnonymousUserFacade();
        //private const string DEFAULT_DATE = "2000-01-01 00:00:00.000";
        private ControllersCenter controllersCenter = new ControllersCenter();
        const string sec = "b14ca5898a4e4133bbce2ea2315a1916";


        #region Create New Customer.
        /// <summary>
        /// Create New Customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Customer))]
        [Route("create/customer", Name = "CreateNewCustomer")]
        [HttpPost]
        public IHttpActionResult CreateNewCustomer([FromBody]Customer customer)
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                facade.CreateNewCustomer(customer);
                SendEmailToNewCustomer(customer);
                return Ok("You Have Successfully Signed Up !! You Only Have To Verify Your Email.");
            });
            return result; // for debug - break point here  
        }
        #endregion

        #region Add Company To Companies Queue.
        /// <summary>
        /// Add Company To Companies Queue.
        /// </summary>
        /// <param name="company"></param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(AirlineCompany))]
        [Route("toqueue/company", Name = "AddComopanyToQueue")]
        [HttpPost]
        public IHttpActionResult AddComopanyToQueue([FromBody]AirlineCompany company)
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                string companyAsJson = JsonConvert.SerializeObject(company);
                RabbitHutch.CreateBus("host=localhost").Publish(companyAsJson, "Companies_Requests");
                return Ok("Your Request Was Successful !! Please Wait For The Admin Approval.");
            });
            return result; // for debug - break point here  
        }
        #endregion

        #region Get Company By Id.
        /// <summary>
        /// Get Company By Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(AirlineCompany))]
        [Route("search/companies/{id}", Name = "GetCompanyById")]
        [HttpGet]
        public IHttpActionResult GetCompanyById([FromUri]int id)
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                AirlineCompany company = facade.GetAirlineById(id);
                return GetSuccessResponse(company, "No Company With The Received Id Was Found.");
            });
            return result; // for debug - break point here
        }
        #endregion

        #region Get Company By Company Name.
        /// <summary>
        /// Get Company By Company Name.
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(AirlineCompany))]
        [Route("search/company/byname", Name = "GetCompanyByAirlineName")]
        [HttpGet]
        public IHttpActionResult GetCompanyByCompanyName([FromUri]string companyName)
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                AirlineCompany company = facade.GetAirlineByAirlineName(companyName);
                return GetSuccessResponse(company, "No Company With The Received UserName Was Found.");
            });
            return result; // for debug - break point here
        }
        #endregion

        #region Get All Companies.
        /// <summary>
        /// Get All Companies.
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(IList<AirlineCompany>))]
        [Route("search/companies", Name = "GetAllCompanies")]
        [HttpGet]
        public IHttpActionResult GetAllCompanies()
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                IList<AirlineCompany> companies = facade.GetAllAirlineCompanies();
                if (companies.Count < 1)
                    return Content(HttpStatusCode.NoContent, "Company List Is Empty.");
                return Content(HttpStatusCode.OK, companies);
            });
            return result; // for debug - break point here
        }
        #endregion

        #region Get Customer By Id.
        /// <summary>
        /// Get Customer By Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Customer))]
        [Route("search/customers/{id}", Name = "GetCustomerById")]
        [HttpGet]
        public IHttpActionResult GetCustomerById([FromUri]int id)
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                Customer customer = facade.GetCustomerById(id);
                return GetSuccessResponse(customer, "No Customer With The Received Id Was Found.");
            });
            return result; // for debug - break point here
        }
        #endregion

        #region Get Flight By Id.
        /// <summary>
        /// Get Flight By Id.
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Flight))]
        [Route("search/flights/{id}", Name = "GetFlightById")]
        [HttpGet]
        public IHttpActionResult GetFlightById([FromUri]int id)
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                Flight flight = facade.GetFlightById(id);
                return GetSuccessResponse(flight, "No Flight With The Received ID Was Found.");
            });
            return result; // for debug - break point here
        }
        #endregion

        #region Get All Flights Vacancy.
        /// <summary>
        /// Get All Flights Vacancy.
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Dictionary<Flight, int>))]
        [Route("search/flights/vacancy", Name = "GetAllFlightsVacancy")]
        [HttpGet]
        public IHttpActionResult GetAllFlightsVacancy()
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                Dictionary<Flight, int> vacancyFlights = facade.GetAllFlightsVacancy();
                if (vacancyFlights.Count < 1)
                    return Content(HttpStatusCode.NoContent, "Sorry, But Currently, Has No Flights With Tickets To Buy... Please Try Later.");
                return Content(HttpStatusCode.OK, vacancyFlights);
            });
            return result; // for debug - break point here
        }
        #endregion

        #region Get Flights By Filters.
        /// <summary>
        /// Function That Get All Flights That Matches The Filters.
        /// /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(IList<Flight>))]
        [Route("search/flights/byfilters", Name = "GetFlightsByFilters")]
        [HttpGet]
        public IHttpActionResult GetFlightsByFilters([FromUri]string fromCountry = "", [FromUri]string toCountry = "", [FromUri]string flightNumber = "",[FromUri]string byCompany = "", [FromUri]string depInHours = "", [FromUri]string landInHours = "", [FromUri]string flightDurationByHours = "", [FromUri]string fromDepDate = "", [FromUri]string upToDepDate = "", [FromUri]string fromLandDate = "", [FromUri]string upToLandDate = "", [FromUri]bool onlyVacancy = true)
        {
            //return StatusCode(HttpStatusCode.NotFound);
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
        {
            IList<Flight> resultFlightsSearch = facade.GetFlightsByFilters(fromCountry, toCountry, flightNumber, byCompany, depInHours, landInHours, flightDurationByHours, fromDepDate, upToDepDate, fromLandDate, upToLandDate, onlyVacancy);
            if (resultFlightsSearch.Count < 1)
                return Content(HttpStatusCode.NoContent, "No Flight Found Matching Sent Parameters.");
            return Content(HttpStatusCode.OK, resultFlightsSearch);
        });
            return result; // for debug - break point here
        }
        #endregion

        #region Get All Flights.
        /// <summary>
        /// Get All Flights.
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(IList<Flight>))]
        [Route("search/flights", Name = "GetAllFlights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                IList<Flight> flights = facade.GetAllFlights();
                if (flights.Count < 1)
                    return Content(HttpStatusCode.NoContent, "Sorry, But Currently, Has No Flights... Please Try Later.");
                return Content(HttpStatusCode.OK, flights);
            });
            return result; // for debug - break point here
        }
        #endregion

        #region Get Country By Id.
        /// <summary>
        /// Get Country By Id.
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Country))]
        [Route("search/countries/{id}", Name = "GetCountryById")]
        [HttpGet]
        public IHttpActionResult GetCountryById([FromUri]int id)
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                Country country = facade.GetCountryById(id);
                return GetSuccessResponse(country, "No Country Found With ID That Recived...");
            });
            return result; // for debug - break point here
        }
        #endregion

        #region Get Country By Name.
        /// <summary>
        /// Get Country By Name.
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(Country))]
        [Route("search/countries/byname", Name = "GetCountryByName")]
        [HttpGet]
        public IHttpActionResult GetCountryByName([FromUri]string name = "")
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                Country country = facade.GetCountryByName(name);
                return GetSuccessResponse(country, "No Country Found With Name That Recived...");
            });
            return result; // for debug - break point here
        }
        #endregion

        #region Get All Countries.
        /// <summary>
        /// Get All Countries.
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [ResponseType(typeof(IList<Country>))]
        [Route("search/countries", Name = "GetAllCountries")]
        [HttpGet]
        public IHttpActionResult GetAllCountries()
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                IList<Country> countries = facade.GetAllCountries();
                if (countries.Count < 1)
                    return Content(HttpStatusCode.NoContent, "Sorry, But No Countries Have Been Added To The Site Yet.");
                return Content(HttpStatusCode.OK, countries);
            });
            return result; // for debug - break point here
        }
        #endregion

        #region Get The Search Result For Single Instance.
        /// <summary>
        ///  Function That Return Success Message(200 Family) With Difference Ways For Single Instance Search.
        /// </summary>
        /// <param name="poco" name="notFoundResponse"></param>
        /// <returns>IHttpActionResult</returns>
        private IHttpActionResult GetSuccessResponse(IPoco poco, string notFoundResponse)
        {
            IHttpActionResult successResponse;
            if (poco.GetHashCode() == 0)
                successResponse = Content(HttpStatusCode.NoContent, notFoundResponse);
            else
                successResponse = Content(HttpStatusCode.OK, poco);
            return successResponse;
        }
        #endregion` 

        #region Send Email To New Customer
        /// <summary>
        /// Function To Send Email To New Customer
        /// </summary>
        /// <param name="customer"></param>
        static void SendEmailToNewCustomer(Customer customer)
        {
            //string encryptDetails = ControllersCenter.EncryptString(sec, $"{customer.User_Name}|{customer.First_Name}|{customer.Last_Name}");
            string encryptDetails = $"{customer.User_Name}|{customer.First_Name}|{customer.Last_Name}";
            string apiKey = Environment.GetEnvironmentVariable("Flights_Managment_Emails_Key",EnvironmentVariableTarget.Machine);
            ISendGridClient client = new SendGridClient(apiKey);
            EmailAddress from = new EmailAddress("test@example.com", "Let's Fly");
            string subject = $"Hey {customer.First_Name} Welcome To Let's Fly";
            EmailAddress to = new EmailAddress("meir7595@gmail.com", $"{customer.First_Name} {customer.Last_Name}");
            string emailContent = $"You Have successfully Registered To Let's Fly!</br> <strong><a href='https://localhost:951/api/confirmemail?token={encryptDetails}'>Please Click Here To Confirm Your Email</a></strong>";
            string htmlContent = $"You Have successfully Registered To Let's Fly!</br> <strong><a href='https://localhost:951/api/confirmemail?token={encryptDetails}'>Please Click Here To Confirm Your Email</a></strong>";
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, emailContent, htmlContent);
            Response response = client.SendEmailAsync(msg).Result;
        }
        #endregion

        #region Confirm New Email Address
        /// <summary>
        /// Confirm New Email Address
        /// </summary>
        [Route("confirmemail", Name = "ConfirmEmail")]
        [HttpGet]
        public IHttpActionResult VerifyNewCustomerEmail([FromUri]string token)
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
            //if (facade.VerifyNewCustomerEmail(ControllersCenter.DecryptString(sec, token).Split('|')[0]))
            string userName = token.Split('|')[0];
                if (facade.VerifyNewCustomerEmail(userName))
                return Content(HttpStatusCode.OK, "Your Account Has Been Successfully Verified");
                return Content(HttpStatusCode.NotFound, "Sorry, Your Account Verification Failed");
            });
            return result; // for debug - break point here
        }
        #endregion

        #region
        [ResponseType(typeof(object))]
        [Route("search/statesOrCities", Name = "GetStatesOrCities")]
        [HttpGet]
        public IHttpActionResult GetDataFromGeoApi([FromUri]string url = null, string state = null)
        {
            IHttpActionResult result = controllersCenter.ExecuteSafe(() =>
            {
                string dataResponse;
                string currentUrl = state == null ? url : url + "&state=" + state;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage response = client.GetAsync(currentUrl).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                             dataResponse = content.ReadAsStringAsync().Result;
                        }
                    }
                }
                return Ok(dataResponse);
            });
            return result;
            
        }
        #endregion
    }
}
