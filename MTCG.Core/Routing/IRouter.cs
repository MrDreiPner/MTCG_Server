using MTCG_Server.MTCG.Core.Request;

namespace MTCG_Server.MTCG.Core.Routing
{
    public interface IRouter
    {
        IRouteCommand? Resolve(RequestContext request);
    }
}