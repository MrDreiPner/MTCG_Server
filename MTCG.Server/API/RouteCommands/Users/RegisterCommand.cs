using MTCG_Server.MTCG.BLL;
using MTCG_Server.MTCG.Core.Response;
using MTCG_Server.MTCG.Core.Routing;
using MTCG_Server.MTCG.Models;

namespace MTCG_Server.MTCG.API.RouteCommands.Users
{
    internal class RegisterCommand : IRouteCommand
    {
        private readonly Credentials _credentials;
        private readonly IUserManager _userManager;

        public RegisterCommand(IUserManager userManager, Credentials credentials)
        {
            _credentials = credentials;
            _userManager = userManager;
        }

        public Response Execute()
        {
            var response = new Response();
            try
            {
                _userManager.RegisterUser(_credentials);
                response.StatusCode = StatusCode.Created;
            }
            catch(DuplicateUserException)
            {
                response.StatusCode = StatusCode.Conflict;
            }
            return response;
        }
    }
}
