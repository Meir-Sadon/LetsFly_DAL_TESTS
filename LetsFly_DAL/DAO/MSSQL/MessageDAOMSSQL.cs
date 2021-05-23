using EasyNetQ;
using LetsFly_DAL.DAO.Interfaces;
using LetsFly_DAL.Objects.Poco_s;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL.DAO.MSSQL
{
    public class MessageDAOMSSQL : IMessageDAO
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader reader;

        public MessageDAOMSSQL()
        {
            conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING);
            cmd = new SqlCommand(null, conn);
        }

        public string CreateMessage(Message newMessage)
        {
            //try
            //{
                using (conn)
                {
                    conn.Open();
                    string sSQL = $"Insert Into Messages(MessageNumByUser, Sender, Reciever, Title, Body, CreateDate, ValidUntil) Values({GetNextMessageNumberForCurrentUser(newMessage.ReceiverId)},{newMessage.SenderId},{newMessage.ReceiverId},'{newMessage.Title}','{newMessage.Body}','{DateTime.Now}','{newMessage.ValidUntil}') ";
                    using (cmd = new SqlCommand(sSQL, conn))
                    {
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.RecordsAffected > 0)
                                return "The Message Was Sent Successfully";
                        }
                    }
                }
                return "There's Something Wrong With Creating A New Message";
            //}
            //catch (Exception ex)
            //{
            //    return "" + ex.Message;
            //}
        }

        public string DeleteMessage(long messageId)
        {
            try
            {
                string sSQL = $"Delete From Messages Where Id = " + messageId;
                using (conn)
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sSQL, conn))
                    {
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.RecordsAffected > 0)
                                return "Message Number " + messageId + " Deleted Successfully";
                            else
                                return "There's Something Wrong With Deleting The Message";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Deleting The Message Was Fail: " + ex.Message;
            }
        }

        public IList<Message> GetAllMessagesByUser(long userId)
        {
            List<Message> messages = new List<Message>(); ;
            try
            {
                string sSQL = $"Select T0.Id, T0.MessageNumByUser, T0.Sender, T1.User_Name AS SenderName, T0.Receiver, T2.User_Name AS ReceiverName, T0.Title, T0.Body, T0.CreateDate, T0.ValidUntil," +
                    $" T0.UpdateDate, T0.DeleteDate, T0.IsReaded, T0.IsMarked, T0.IsDeleted, T0.IsSpammed From Messages T0 " +
                    $"LEFT JOIN Users T1 ON T0.Sender = T1.Id LEFT JOIN Users T2 ON T0.Receiver = T2.Id Where Sender = " + userId + " OR Receiver = " + userId;
                using (conn)
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sSQL, conn))
                    {
                        using (reader = cmd.ExecuteReader())
                        {
                            Message message;
                            int colIndex;
                            while (reader.Read())
                            {
                                colIndex = 0;
                                message = new Message();
                                message.MsgId = (long)reader.GetValue(colIndex++);
                                message.MessageNumByUser = (long)reader.GetValue(colIndex++);
                                message.SenderId = (long)reader.GetValue(colIndex++);
                                message.SenderName = (string)reader.GetValue(colIndex++);
                                message.ReceiverId = (long)reader.GetValue(colIndex++);
                                message.ReceiverName = (string)reader.GetValue(colIndex++);
                                message.Title = (string)reader.GetValue(colIndex++);
                                message.Body = (string)reader.GetValue(colIndex++);
                                message.CreateDate = (DateTime)reader.GetValue(colIndex++);
                                message.ValidUntil = (DateTime)reader.GetValue(colIndex++);
                                message.UpdateDate = (DateTime)reader.GetValue(colIndex++);
                                message.DeletedDate = (DateTime)reader.GetValue(colIndex++);
                                var curVal = reader.GetValue(colIndex++);
                                message.IsReaded = curVal.ToString();//reader.IsDBNull(colIndex) ? "N" : ((char)reader.GetValue(colIndex)).ToString();
                                curVal = reader.GetValue(colIndex++);
                                message.IsMarked = curVal.ToString();//reader.IsDBNull(colIndex) ? "N" : ((char)reader.GetValue(colIndex)).ToString();
                                curVal = reader.GetValue(colIndex++); 
                                message.IsDeleted = curVal.ToString(); //reader.IsDBNull(colIndex) ? "N" : ((char)reader.GetValue(colIndex)).ToString();
                                curVal = reader.GetValue(colIndex++); 
                                message.IsSpammed = curVal.ToString(); //reader.IsDBNull(colIndex) ? "N" : ((char)reader.GetValue(colIndex)).ToString();
                                messages.Add(message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (String.IsNullOrEmpty(errorMessage))
                    return null;
                return null;
            }
            return messages;
        }
        
        public Message GetMessageById(long messageId)
        {
            Message message = new Message();
            try
            {
                string sSQL = $"Select * From Messages Where Id = " + messageId;
                using (conn)
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sSQL, conn))
                    {
                        using (reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                message =  new Message()
                                {
                                    MessageNumByUser = (long)reader.GetValue(1),
                                    SenderId = (long)reader.GetValue(2),
                                    ReceiverId = (long)reader.GetValue(3),
                                    Title = (string)reader.GetValue(4),
                                    Body = (string)reader.GetValue(5),
                                    CreateDate = (DateTime)reader.GetValue(6),
                                    ValidUntil = (DateTime)reader.GetValue(7),
                                    UpdateDate = (DateTime)reader.GetValue(8),
                                    DeletedDate = (DateTime)reader.GetValue(9),
                                    IsReaded = (string)reader.GetValue(10),
                                    IsMarked = (string)reader.GetValue(11),
                                    IsDeleted = (string)reader.GetValue(12),
                                    IsSpammed = (string)reader.GetValue(13),
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return message;
        }

        public string UpdateMessage(long msgId, Message message)
        {
            try
            {
                string sSQL = $"Alter Table Messages set UpdateDate = '{message.UpdateDate}', IsReaded = '{message.IsReaded}', IsMarked = '{message.IsMarked}', IsSpammed = '{message.IsSpammed}' where Id = {message.MsgId} ";
                using (conn)
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sSQL, conn))
                    {
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.RecordsAffected > 0)
                                return $"Message Number {message.MsgId} Successfully Updated";
                            else
                                return "There's Something Wrong With Updating This Message";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Updating The Message Was Fail: " + ex.Message;
            }
        }

        private long GetNextMessageNumberForCurrentUser(long userId)
        {
            cmd.CommandText = "Select max(Id) as LstMsgNumByUsr From Messages Where Receiver = " + userId;
            cmd.ExecuteReader();
            return ((long)reader.GetValue(0))+1;
        }
    }
}
