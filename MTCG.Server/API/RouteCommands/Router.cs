using SWE1.MTCG.BLL;
using Newtonsoft.Json;
using SWE1.MTCG.API.RouteCommands.Messages;
using SWE1.MTCG.API.RouteCommands.Users;
using SWE1.MTCG.BLL;
using MTCG_Server.CardTypes;
using SWE1.MTCG.Core.Request;
using SWE1.MTCG.Core.Routing;
using SWE1.MTCG.Models;
using System.IO;
using HttpMethod = SWE1.MTCG.Core.Request.HttpMethod;
using MTCG_Server.API.RouteCommands.Packages;

namespace SWE1.MTCG.API.RouteCommands
{
    internal class Router : IRouter
    {
        private readonly IUserManager _userManager;
        private readonly IMessageManager _messageManager;
        private readonly IPackageManager _packageManager;
        private readonly IdentityProvider _identityProvider;
        private readonly IRouteParser _routeParser = new IdRouteParser();

        public Router(IUserManager userManager, IMessageManager messageManager, IPackageManager packageManager)
        {
            _userManager = userManager;
            _messageManager = messageManager;
            _packageManager = packageManager;

            // better: define IIdentityProvider interface and get concrete implementation passed in as dependency
            _identityProvider = new IdentityProvider(userManager);
        }

        public IRouteCommand? Resolve(RequestContext request)
        {
            var identity = (RequestContext request) => _identityProvider.GetIdentityForRequest(request) ?? throw new RouteNotAuthenticatedException();
            var IsUsersMatch = (string path) => _routeParser.IsUsersMatch(path, "/users/{username}");
            var parseId = (string path) => int.Parse(_routeParser.ParseParameters(path, "/messages/{id}")["id"]);
            var parseUsername = (string path) => _routeParser.ParseUsernameParameters(path, "/users/{username}")["username"];


            IRouteCommand? command = request switch
            {
                //User management
                { Method: HttpMethod.Post, ResourcePath: "/users" } => new RegisterCommand(_userManager, Deserialize<Credentials>(request.Payload)),
                { Method: HttpMethod.Post, ResourcePath: "/sessions" } => new LoginCommand(_userManager, Deserialize<Credentials>(request.Payload)),
                { Method: HttpMethod.Put, ResourcePath: var path } when IsUsersMatch(path) => new UpdateCommand(_userManager, identity(request), Deserialize<UserContent>(request.Payload), parseUsername(path)),
                { Method: HttpMethod.Get, ResourcePath: var path } when IsUsersMatch(path) => new GetUserCommand(_userManager, identity(request), parseUsername(path)),

                //Package management
                { Method: HttpMethod.Post, ResourcePath: "/packages" } => new AddPackageCommand(_packageManager, identity(request), Deserialize<List<CardPrototype>>(request.Payload)),



                //{ Method: HttpMethod.Post, ResourcePath: "/messages"} => new AddMessageCommand(_messageManager, identity(request), EnsureBody(request.Payload)),
                //{ Method: HttpMethod.Get, ResourcePath: "/messages" } => new ListMessagesCommand(_messageManager, identity(request)),

                //{ Method: HttpMethod.Get, ResourcePath: var path} when isMatch(path) => new ShowMessageCommand(_messageManager, identity(request), parseId(path)),
                //{ Method: HttpMethod.Put, ResourcePath: var path } when isMatch(path) => new UpdateMessageCommand(_messageManager, identity(request), parseId(path), EnsureBody(request.Payload)),
                //{ Method: HttpMethod.Delete, ResourcePath: var path } when isMatch(path) => new RemoveMessageCommand(_messageManager, identity(request), parseId(path)),

                _ => null
            };

            return command;
        }

        private string EnsureBody(string? body)
        {
            if (body == null)
            {
                throw new InvalidDataException();
            }
            return body;
        }

        private T Deserialize<T>(string? body) where T : class
        {
            var data = body != null ? JsonConvert.DeserializeObject<T>(body) : null;
            if (data == null)
            {
                throw new InvalidDataException();
            }
            return data;
        }
    }
}
