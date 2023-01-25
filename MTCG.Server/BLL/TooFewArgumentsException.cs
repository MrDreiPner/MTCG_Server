using System.Runtime.Serialization;

namespace MTCG.MTCG.BLL
{
    [Serializable]
    public class TooFewArgumentsException : Exception
    {
        public TooFewArgumentsException()
        {
        }
    }
}