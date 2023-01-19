// See https://aka.ms/new-console-template for more information
using SWE1.MessageServer.API.RouteCommands;
using SWE1.MessageServer.BLL;
using SWE1.MessageServer.Core.Server;
using SWE1.MessageServer.DAL;
using System.Net;

// PostgreSQL DAOs
// better: fetch from config file -> next semester
var connectionString = "Host=localhost;Port=10002;Username=postgres;Password=Aurora;Database=MTCG_DB";
var database = new Database(connectionString);
var userDao = database.UserDao;
var messageDao = database.MessageDao;

// In Memory DAOs
//var userDao = new InMemoryUserDao();
//var messageDao = new InMemoryMessageDao();

var userManager = new UserManager(userDao);
var messageManager = new MessageManager(messageDao);

var router = new Router(userManager, messageManager);
var server = new HttpServer(IPAddress.Any, 10001, router);
server.Start();
