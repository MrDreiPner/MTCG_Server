using System.Runtime.Serialization;

namespace MTCG.MTCG.DAL
{
    [Serializable]
    public class CardAlreadyExistsException : Exception
    {
        public CardAlreadyExistsException()
        {
        }

        
    }
}