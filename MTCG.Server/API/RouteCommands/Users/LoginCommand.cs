using MTCG_Server.MTCG.BLL;
using MTCG_Server.MTCG.Core.Response;
using MTCG_Server.MTCG.Core.Routing;
using MTCG_Server.MTCG.Models;

namespace MTCG_Server.MTCG.API.RouteCommands.Users
{
    internal class LoginCommand : IRouteCommand
    {
        private readonly IUserManager _userManager;

        private readonly Credentials _credentials;

        public LoginCommand(IUserManager userManager, Credentials credentials)
        {
            _credentials = credentials;
            _userManager = userManager;
        }

        public Response Execute()
        {
            User? user;
            try
            {
                user = _userManager.LoginUser(_credentials);
            }
            catch (UserNotFoundException)
            {
                user = null;
            }

            var response = new Response();
            if (user == null)
            {
                response.StatusCode = StatusCode.Unauthorized;
            }
            else
            {
                response.StatusCode = StatusCode.Ok;
                response.Payload = user.Token;
            }

            return response;
        }
    }
}
