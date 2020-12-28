using FlightManagment___Basic___Part_1;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FlightManagment___WebApi___Part_3
{
    [EnableCors("*", "*", "*")]
    public class AuthController : ApiController
    {

        #region Check The Details Of Current User For Token.
        /// <summary>
        /// Function That Check The Details Of The User That Request To Get Token.
        /// </summary>
        /// <param name="userAsJson"></param>
        /// <returns>IHttpActionResult</returns>
        [HttpPost]
        [Route("api/auth", Name = "GetToken")]
        public IHttpActionResult Authenticate([FromBody]User userDetails)
        {
            IHttpActionResult result = ExecuteSafe(() =>
            {
                if (FlyingCenterSystem.GetUserAndFacade(userDetails, out ILogin loginUser, out FacadeBase myFacade))
                {
                    lock (this)
                    {
                        //ValidateTokenHandler validateTokenHandler = ValidateTokenHandler.GetInstance();
                        ValidateTokenHandler validateTokenHandler = new ValidateTokenHandler();
                        string token = CreateToken(userDetails);
                        if (userDetails.Type == UserType.Administrator)
                        {

                            validateTokenHandler.SetAdminToken(validateTokenHandler.ADMIN_TOKEN, token);
                            return Content(HttpStatusCode.Created, validateTokenHandler.ADMIN_TOKEN);
                        }
                        else
                        {
                            validateTokenHandler.SetUserToken(validateTokenHandler.ADMIN_TOKEN, token);
                            return Content(HttpStatusCode.Created, token);
                        }
                        //long returnId;
                        //switch (userDetails.Type)
                        //{
                        //    case UserType.Administrator:
                        //        LoginToken<Administrator> adminUser = loginUser as LoginToken<Administrator>;
                        //        returnId = adminUser.User.Id;
                        //        break;
                        //    case UserType.Airline:
                        //        LoginToken<AirlineCompany> companyUser = loginUser as LoginToken<AirlineCompany>;
                        //        returnId = companyUser.User.Id;
                        //        break;
                        //    case UserType.Customer:
                        //        LoginToken<Customer> customerUser = loginUser as LoginToken<Customer>;
                        //        returnId = customerUser.User.Id;
                        //        break;
                        //    default:
                        //        return null;
                        //}
                        // "{" +
                        //    $"\"token\":  \"{token}\"," +
                        //    $" \"user\": \"{user}\"" +
                        //"}"
                        //);
                    }
                }
                return Content(HttpStatusCode.Unauthorized, "Sorry, But Your Username Or Password Is Incorrect");
            });
            return result;
        }
        #endregion

        #region Create Token For Valid User.
        /// <summary>
        /// Function For Create New Token For Valid User.
        /// </summary>
        /// <param name="tokenUser"></param>
        /// <returns>string</returns>
        private string CreateToken(User tokenUser)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddDays(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("Id", tokenUser.Id.ToString()),
                new Claim(ClaimTypes.Name, tokenUser.UserName),
                new Claim(ClaimTypes.Role, tokenUser.Type.ToString()),
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            JwtSecurityToken token = (JwtSecurityToken)tokenHandler.CreateJwtSecurityToken
                (
                    issuer: "smesk.in",
                    audience: "readers",
                    subject: claimsIdentity,
                    notBefore: issuedAt,
                    expires: expires,
                    signingCredentials: signingCredentials
                );

            var tokenString = tokenHandler.WriteToken(token);
            return tokenString; //Break Point Here - For Debuging.
        }
        #endregion

        #region Execute Any Action With All Catch Cases.
        /// <summary>
        /// One Function For All Catch Cases.
        /// </summary>
        /// <param name="myFunc"></param>
        /// <returns>IHttpActionResult</returns>
        private IHttpActionResult ExecuteSafe(Func<IHttpActionResult> myFunc)
        {
            try
            {
                return myFunc.Invoke();
            }
            catch (UserNotExistException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
            catch (UserAlreadyExistException ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
            catch (WrongPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                return Content(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
