using MTCG.MTCG.BLL;
using MTCG.MTCG.Core.Response;
using MTCG.MTCG.Core.Routing;
using MTCG.MTCG.Models;
using System.Security.Principal;

namespace MTCG.MTCG.API.RouteCommands.Users
{
    internal class UpdateCommand : AuthenticatedRouteCommand
    {
        private readonly IUserManager _userManager;
        private readonly UserContent _userContent;
        private readonly string _passedUsername;

        public UpdateCommand(IUserManager userManager, User identity, UserContent userContent, string username) : base(identity)
        {
            _userManager = userManager;
            _userContent = userContent;
            _passedUsername = username;
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                User checkExist = _userManager.GetUserByUsername(_passedUsername);
                if(_passedUsername == Identity.Username || Identity.Username == "admin")
                {
                    _userManager.UpdateUser(Identity, _userContent, _passedUsername);
                    response.StatusCode = StatusCode.Ok;
                }
                else
                {
                    response.StatusCode = StatusCode.Unauthorized;
                }
            }
            catch (Exception ex)
            {
                if(ex is DuplicateUserException)
                    response.StatusCode = StatusCode.Conflict;
                else if(ex is UserNotFoundException) 
                    response.StatusCode = StatusCode.NotFound;
            }
            return response;
        }
    }
}