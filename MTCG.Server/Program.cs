// See https://aka.ms/new-console-template for more information
using MTCG_Server.MTCG.DAL;
using MTCG_Server.MTCG.BLL;
using MTCG_Server.MTCG.API.RouteCommands;
using MTCG_Server.MTCG.BLL;
using MTCG_Server.MTCG.Core.Server;
using MTCG_Server.MTCG.DAL;
using System.Net;
using MTCG_Server.BattleClasses;

// PostgreSQL DAOs
// better: fetch from config file -> next semester
var connectionString = "Host=localhost;Port=10002;Username=postgres;Password=Aurora;Database=MTCG_DB";
var database = new Database(connectionString);
var userDao = database.UserDao;
var messageDao = database.MessageDao;
var packageDao = database.PackageDao;
var cardDao = database.CardDao;
var battleDao = database.BattleDao;

// In Memory DAOs
//var userDao = new InMemoryUserDao();
//var messageDao = new InMemoryMessageDao();

var userManager = new UserManager(userDao);
var messageManager = new MessageManager(messageDao);
var packageManager = new PackageManager(packageDao);
var cardManager = new CardManager(cardDao);
var battleManager = new BattleManager(battleDao);

//Battle Lobbies
List<BattleLobby> battleLobbies = new List<BattleLobby>();

var router = new Router(userManager, messageManager, packageManager, cardManager, battleManager, battleLobbies);
var server = new HttpServer(IPAddress.Any, 10001, router);
server.Start();
