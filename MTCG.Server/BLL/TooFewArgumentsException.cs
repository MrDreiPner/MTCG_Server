using System.Runtime.Serialization;

namespace MTCG_Server.MTCG.BLL
{
    [Serializable]
    public class TooFewArgumentsException : Exception
    {
        public TooFewArgumentsException()
        {
        }
    }
}