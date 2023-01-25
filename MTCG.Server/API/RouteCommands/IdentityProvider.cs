using MTCG.MTCG.BLL;
using MTCG.MTCG.Core.Request;
using MTCG.MTCG.Models;

namespace MTCG.MTCG.API.RouteCommands
{
    internal class IdentityProvider
    {
        private readonly IUserManager _userManager;

        public IdentityProvider(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public User? GetIdentityForRequest(RequestContext request)
        {
            User? currentUser = null;

            if (request.Header.TryGetValue("Authorization", out var authToken))
            {
                const string prefix = "Bearer ";
                if (authToken.StartsWith(prefix))
                {
                    Console.WriteLine("Auth token is " + authToken.Substring(prefix.Length));
                    try
                    {

                        currentUser = _userManager.GetUserByAuthToken(authToken.Substring(prefix.Length));
                    }
                    catch { }
                }
            }

            return currentUser;
        }
    }
}
