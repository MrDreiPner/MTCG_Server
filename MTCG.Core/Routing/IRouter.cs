using MTCG.MTCG.Core.Request;

namespace MTCG.MTCG.Core.Routing
{
    public interface IRouter
    {
        IRouteCommand? Resolve(RequestContext request);
    }
}