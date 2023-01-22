using SWE1.MTCG.Core.Request;
using HttpMethod = SWE1.MTCG.Core.Request.HttpMethod;

namespace SWE1.MTCG.API.RouteCommands
{
    internal interface IRouteParser
    {
        bool IsUsersMatch(string resourcePath, string routePattern);
        Dictionary<string, string> ParseParameters(string resourcePath, string routePattern);
        Dictionary<string, string> ParseUsernameParameters(string resourcePath, string routePattern);
    }
}
