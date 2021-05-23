using LetsFly_DAL.Objects.Poco_s;
using System.Collections.Generic;

namespace LetsFly_DAL
{
    public interface IUserFacadeBase<T>  where T : IUser
    {
        string CreateMessage(LoginToken<T> login, Message newMessage);
        IList<Message> GetAllMessagesByUser(long userId);
        Message GetMessageById(long messageId);
        string UpdateMessage(long msgId, Message message);
        string DeleteMessage(long msgId);
    }
}