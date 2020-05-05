using FlightManagment___Basic___Part_1;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FlightManagment___WebApi___Part_3
{
    /// <summary>
    /// Center Class For All Functions Of The Controllers.
    /// </summary>
    public class ControllersCenter : ApiController
    {
        #region Execute Any Action With All Catch Cases.
        /// <summary>
        /// One Function For All Catch Cases.
        /// </summary>
        /// <param name="myFunc"></param>
        /// <returns>IHttpActionResult</returns>
        public IHttpActionResult ExecuteSafe(Func<IHttpActionResult> myFunc)
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
            catch (CentralAdministratorException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (FlightNotMatchException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (OutOfTicketsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TooLateToCancelTicketException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TicketNotMatchException ex)
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

        #region Encrypt Any String
        /// <summary>
        ///  Function To Encrypt Any String.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="plainInput"></param>
        /// <returns></returns>
        public static string EncryptString(string key, string plainInput)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainInput);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Encoding.UTF8.GetString(array);
        }
        #endregion

        #region Decrypt Some Encrypt String.
        /// <summary>
        /// Function To Decrypt Some Encrypt String.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        #endregion

    }
}