using System.Runtime.Serialization;

namespace SWE1.MTCG.DAL
{
    [Serializable]
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException()
        {
        }

        
    }
}