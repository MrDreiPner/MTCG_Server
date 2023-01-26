using System.Text.RegularExpressions;

namespace MTCG.MTCG.API.RouteCommands
{
    internal class IdRouteParser : IRouteParser
    {
        public bool IsUsersMatch(string resourcePath, string routePattern)
        {
            var pattern = "^" + routePattern.Replace("{username}", ".*").Replace("/", "\\/") + "(\\?.*)?$";
            return Regex.IsMatch(resourcePath, pattern);
        }
        public bool IsIdMatch(string resourcePath, string routePattern)
        {
            var pattern = "^" + routePattern.Replace("{id}", ".*").Replace("/", "\\/") + "(\\?.*)?$";
            return Regex.IsMatch(resourcePath, pattern);
        }

        public Dictionary<string, string> ParseParameters(string resourcePath, string routePattern)
        {
            // query parameters
            var parameters = ParseQueryParameters(resourcePath);

            // id parameter
            var id = ParseIdParameter(resourcePath, routePattern);
            if (id != null)
            {
                parameters["id"] = id;
            }

            return parameters;
        }

        private string? ParseIdParameter(string resourcePath, string routePattern)
        {
            var pattern = "^" + routePattern.Replace("{id}", "(?<id>[^\\?\\/]*)").Replace("/", "\\/") + "$";
            var result = Regex.Match(resourcePath, pattern);
            return result.Groups["id"].Success ? result.Groups["id"].Value : null;
        }

        public Dictionary<string, string> ParseUsernameParameters(string resourcePath, string routePattern)
        {
            // query parameters
            var parameters = ParseQueryParameters(resourcePath);

            // id parameter
            var username = ParseUsernameParameter(resourcePath, routePattern);
            if (username != null)
            {
                parameters["username"] = username;
            }

            return parameters;
        }
        private string? ParseUsernameParameter(string resourcePath, string routePattern)
        {
            var pattern = "^" + routePattern.Replace("{username}", "(?<username>[^\\?\\/]*)").Replace("/", "\\/") + "$";
            var result = Regex.Match(resourcePath, pattern);
            return result.Groups["username"].Success ? result.Groups["username"].Value : null;
        }

        private Dictionary<string, string> ParseQueryParameters(string route)
        {
            var parameters = new Dictionary<string, string>();

            var query = route
                .Split("?", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .FirstOrDefault();

            if (query != null)
            {
                var keyValues = query
                    .Split("&", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Split("=", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                    .Where(p => p.Length == 2);

                foreach (var p in keyValues)
                {
                    parameters[p[0]] = p[1];
                }
            }

            return parameters;
        }


    }
}
