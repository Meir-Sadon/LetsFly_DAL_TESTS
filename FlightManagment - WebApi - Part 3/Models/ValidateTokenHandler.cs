using FlightManagment___Basic___Part_1;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FlightManagment___WebApi___Part_3
{
    public class ValidateTokenHandler : DelegatingHandler
    {
        //static private ValidateTokenHandler instance;

        //static object key = new object();
        public string ADMIN_TOKEN { get; private set; }
        public string USER_TOKEN { get; private set; }

        //private ValidateTokenHandler()
        //{
        //    User baseAdminUser = new User(FlyingCenterConfig.ADMIN_NAME, FlyingCenterConfig.ADMIN_PASSWORD, UserType.Administrator, true);
        //    string token = "";
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response;
        //    client.BaseAddress = new Uri($"https://localhost:951/api/Auth");
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    string userDetails =
        //        "{" +
        //        $"\"User_Name\":  \"{baseAdminUser.UserName}\"," +
        //        $" \"Password\": \"{baseAdminUser.Password}\"," +
        //        $" \"Type\": \"{baseAdminUser.Type}\"" +
        //        "}";
        //    HttpContent httpContent = new StringContent(userDetails, Encoding.UTF8, "application/json");
        //    response = client.PostAsync($"https://localhost:951/api/Auth", httpContent).Result;
        //    token = response.Content.ReadAsStringAsync().Result;
        //    ADMIN_TOKEN = FixApiResponseString(token);
        //}

        //static public ValidateTokenHandler GetInstance()
        //{
        //    if (instance == null)
        //    {
        //        lock (key)
        //        {
        //            if (instance == null)
        //            {
        //                instance = new ValidateTokenHandler();
        //            }
        //        }
        //    }
        //    return instance;
        //}

        private static string FixApiResponseString(string input)
        {
            input = input.Replace("\\", string.Empty);
            input = input.Trim('"');
            return input;
        }
        internal void SetUserToken(string currentAdminToken, string newUserToken)
        {
            if (String.IsNullOrEmpty(currentAdminToken))
                USER_TOKEN = newUserToken;
        }

        internal void SetAdminToken(string currentAdminToken, string newAdminToken)
        {
            if (String.IsNullOrEmpty(currentAdminToken))
                ADMIN_TOKEN = newAdminToken;
        }

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;
            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
                var now = DateTime.UtcNow;
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));


                SecurityToken securityToken;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = "readers",
                    ValidIssuer = "smesk.in",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.User = handler.ValidateToken(token, validationParameters, out securityToken);

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException ex)
            {
                string Exmessage = ex.Message;
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception ex)
            {
                string Exmessage = ex.Message;
                statusCode = HttpStatusCode.InternalServerError;
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires)
                {
                    return true;
                }
            }
            return false;
        }
    }
}