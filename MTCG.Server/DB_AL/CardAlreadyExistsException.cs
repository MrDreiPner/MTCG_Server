using System.Runtime.Serialization;

namespace SWE1.MTCG.DAL
{
    [Serializable]
    public class CardAlreadyExistsException : Exception
    {
        public CardAlreadyExistsException()
        {
        }
    }
}