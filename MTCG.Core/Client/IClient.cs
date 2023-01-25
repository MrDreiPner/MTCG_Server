using MTCG.MTCG.Core.Request;

namespace MTCG.MTCG.Core.Client
{
    public interface IClient
    {
        public RequestContext? ReceiveRequest();
        public void SendResponse(Response.Response response);
        public void Close();
    }
}
