using System.Runtime.Serialization;

namespace SWE1.MTCG.BLL
{
    [Serializable]
    public class TooFewArgumentsException : Exception
    {
        public TooFewArgumentsException()
        {
        }
    }
}