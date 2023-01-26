using MTCG.MTCG.BLL;
using Newtonsoft.Json;
using MTCG.MTCG.API.RouteCommands.Users;
using MTCG.CardTypes;
using MTCG.MTCG.Core.Request;
using MTCG.MTCG.Core.Routing;
using MTCG.MTCG.Models;
using System.IO;
using HttpMethod = MTCG.MTCG.Core.Request.HttpMethod;
using MTCG.API.RouteCommands.Packages;
using MTCG.API.RouteCommands.Cards;
using MTCG.API.RouteCommands.Battles;
using MTCG.BattleClasses;
using MTCG.API.RouteCommands.Trades;

namespace MTCG.MTCG.API.RouteCommands
{
    class BattleLobby_Mutex
    {
        public static Mutex BattleMutex = new Mutex();
    };
    internal class Router : IRouter
    {
        private readonly IUserManager _userManager;
        private readonly IPackageManager _packageManager;
        private readonly ICardManager _cardManager;
        private readonly IBattleManager _battleManager;
        private readonly ITradeManager _tradeManager;
        private readonly IdentityProvider _identityProvider;
        private readonly IRouteParser _routeParser = new IdRouteParser();
        private List<BattleLobby> _battleLobbies;
        public Router(IUserManager userManager, IPackageManager packageManager, ICardManager cardManager, IBattleManager battleManager, ITradeManager TradeManager, List<BattleLobby> battleLobbies)
        {
            _userManager = userManager;
            _packageManager = packageManager;
            _cardManager = cardManager;
            _battleManager = battleManager;
            _tradeManager = TradeManager;

            _battleLobbies = battleLobbies;
            // better: define IIdentityProvider interface and get concrete implementation passed in as dependency
            _identityProvider = new IdentityProvider(userManager);
        }

        public IRouteCommand? Resolve(RequestContext request)
        {
            var identity = (RequestContext request) => _identityProvider.GetIdentityForRequest(request) ?? throw new RouteNotAuthenticatedException();
            var IsUsersMatch = (string path) => _routeParser.IsUsersMatch(path, "/users/{username}");
            var parseUsername = (string path) => _routeParser.ParseUsernameParameters(path, "/users/{username}")["username"];
            var IsIdMatch = (string path) => _routeParser.IsIdMatch(path, "/tradings/{id}");
            var parseId = (string path) => _routeParser.ParseParameters(path, "/tradings/{id}")["id"];



            IRouteCommand? command = request switch
            {
                //User management
                { Method: HttpMethod.Post, ResourcePath: "/users" } => new RegisterCommand(_userManager, Deserialize<Credentials>(request.Payload)),
                { Method: HttpMethod.Post, ResourcePath: "/sessions" } => new LoginCommand(_userManager, Deserialize<Credentials>(request.Payload)),
                { Method: HttpMethod.Put, ResourcePath: var path } when IsUsersMatch(path) => new UpdateCommand(_userManager, identity(request), Deserialize<UserContent>(request.Payload), parseUsername(path)),
                { Method: HttpMethod.Get, ResourcePath: var path } when IsUsersMatch(path) => new GetUserCommand(_userManager, identity(request), parseUsername(path)),

                //Package management
                { Method: HttpMethod.Post, ResourcePath: "/packages" } => new AddPackageCommand(_packageManager, identity(request), Deserialize<List<CardPrototype>>(request.Payload)),
                { Method: HttpMethod.Post, ResourcePath: "/transactions/packages" } => new BuyPackageCommand(_packageManager, identity(request)),

                //Card management
                { Method: HttpMethod.Get, ResourcePath: "/cards" } => new ShowCardsCommand(_cardManager, identity(request)),
                { Method: HttpMethod.Get, ResourcePath: var path } when path == "/deck?format=json" || path == "/deck" => new ShowDeckCommand(_cardManager, identity(request), 1),
                { Method: HttpMethod.Get, ResourcePath: var path } when path == "/deck?format=plain" => new ShowDeckCommand(_cardManager, identity(request), 2),
                { Method: HttpMethod.Put, ResourcePath: "/deck" } => new ConfigureDeckCommand(_cardManager, identity(request), Deserialize<List<string>>(request.Payload)),

                //Battle management
                { Method: HttpMethod.Get, ResourcePath: "/stats" } => new ShowUserStatsCommand(_battleManager, identity(request)),
                { Method: HttpMethod.Get, ResourcePath: "/score" } => new ShowScoreboardCommand(_battleManager, identity(request)),
                { Method: HttpMethod.Post, ResourcePath: "/battles" } => new StartBattleCommand(_battleManager, identity(request), _battleLobbies),

                //Trade management
                { Method: HttpMethod.Post, ResourcePath: "/tradings" } => new CreateTradeDealCommand(_tradeManager, identity(request), Deserialize<TradeDeal>(request.Payload)),
                { Method: HttpMethod.Get, ResourcePath: "/tradings" } => new FetchTradeDealsCommand(_tradeManager, identity(request)),
                { Method: HttpMethod.Post, ResourcePath: var path }  when IsIdMatch(path) => new CarryOutTradeCommand(_tradeManager, identity(request), Deserialize<string>(request.Payload), parseId(path)),
                { Method: HttpMethod.Delete, ResourcePath: var path } when IsIdMatch(path) => new DeleteTradeDealCommand(_tradeManager, identity(request), parseId(path)),



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
