using MTCG_Server.MTCG.Core.Request;

namespace MTCG_Server.MTCG.Core.Client
{
    public interface IClient
    {
        public RequestContext? ReceiveRequest();
        public void SendResponse(Response.Response response);
        public void Close();
    }
}
