using MTCG_Server.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.MTCG.DAL
{
    public interface IMessageDao
    {
        IEnumerable<Message> GetMessages(string username);
        Message? GetMessageById(string username, int messageId);
        void InsertMessage(string username, Message message);
        bool UpdateMessage(string username, Message message);
        void DeleteMessage(string username, int messageId);
    }
}
