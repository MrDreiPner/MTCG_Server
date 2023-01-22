using SWE1.MTCG.Core.Request;

namespace SWE1.MTCG.Core.Client
{
    public interface IClient
    {
        public RequestContext? ReceiveRequest();
        public void SendResponse(Response.Response response);
        public void Close();
    }
}
