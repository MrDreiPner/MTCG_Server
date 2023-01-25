using System.Runtime.Serialization;

namespace MTCG.MTCG.DAL
{
    [Serializable]
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException()
        {
        }

        
    }
}