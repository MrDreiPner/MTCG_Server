using MTCG.MTCG.BLL;
using MTCG.MTCG.Core.Response;
using MTCG.MTCG.Core.Routing;
using MTCG.MTCG.Models;

namespace MTCG.MTCG.API.RouteCommands.Users
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
