using SWE1.MTCG.Core.Request;

namespace SWE1.MTCG.Core.Routing
{
    public interface IRouter
    {
        IRouteCommand? Resolve(RequestContext request);
    }
}