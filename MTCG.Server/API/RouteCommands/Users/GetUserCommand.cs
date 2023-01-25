using MTCG.MTCG.BLL;
using MTCG.MTCG.Core.Response;
using MTCG.MTCG.Core.Routing;
using MTCG.MTCG.Models;
using System.Security.Principal;

namespace MTCG.MTCG.API.RouteCommands.Users
{
    internal class GetUserCommand : AuthenticatedRouteCommand
    {
        private readonly IUserManager _userManager;
        private readonly string _passedUsername;

        public GetUserCommand(IUserManager userManager, User identity, string username) : base(identity)
        {
            //Console.WriteLine("Get User command aufgerufen");
            _userManager = userManager;
            _passedUsername = username;
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                User checkExist = _userManager.GetUserByUsername(_passedUsername);
                UserContent user = null;
                Console.WriteLine("passed Username: " + _passedUsername);
                if(_passedUsername == Identity.Username || Identity.Username == "admin")
                {
                    user = _userManager.GetUser(_passedUsername);
                    response.StatusCode = StatusCode.Ok;
                    response.Payload = "Name: " + user.Name;
                    response.Payload += "\nBio: " + user.Bio;
                    response.Payload += "\nImage: " + user.Image;
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