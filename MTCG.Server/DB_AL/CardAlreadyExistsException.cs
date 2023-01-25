using System.Runtime.Serialization;

namespace MTCG_Server.MTCG.DAL
{
    [Serializable]
    public class CardAlreadyExistsException : Exception
    {
        public CardAlreadyExistsException()
        {
        }

        
    }
}