using MTCG.MTCG.Core.Request;
using HttpMethod = MTCG.MTCG.Core.Request.HttpMethod;

namespace MTCG.MTCG.API.RouteCommands
{
    internal interface IRouteParser
    {
        bool IsUsersMatch(string resourcePath, string routePattern);
        Dictionary<string, string> ParseParameters(string resourcePath, string routePattern);
        Dictionary<string, string> ParseUsernameParameters(string resourcePath, string routePattern);
    }
}
