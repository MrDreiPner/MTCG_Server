using MTCG_Server.MTCG.Core.Request;
using HttpMethod = MTCG_Server.MTCG.Core.Request.HttpMethod;

namespace MTCG_Server.MTCG.API.RouteCommands
{
    internal interface IRouteParser
    {
        bool IsUsersMatch(string resourcePath, string routePattern);
        Dictionary<string, string> ParseParameters(string resourcePath, string routePattern);
        Dictionary<string, string> ParseUsernameParameters(string resourcePath, string routePattern);
    }
}
