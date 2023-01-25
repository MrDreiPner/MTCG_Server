using System.Runtime.Serialization;

namespace MTCG.MTCG.DAL
{
    [Serializable]
    public class WrongCardOwnerException : Exception
    {
        public WrongCardOwnerException()
        {
        }

        
    }
}