// See https://aka.ms/new-console-template for more information
using MTCG.MTCG.DAL;
using MTCG.MTCG.BLL;
using MTCG.MTCG.API.RouteCommands;
using MTCG.MTCG.BLL;
using MTCG.MTCG.Core.Server;
using MTCG.MTCG.DAL;
using System.Net;
using MTCG.BattleClasses;

// PostgreSQL DAOs
// better: fetch from config file -> next semester
var connectionString = "Host=localhost;Port=10002;Username=postgres;Password=Aurora;Database=MTCG_DB";
var database = new Database(connectionString);
var userDao = database.UserDao;
var packageDao = database.PackageDao;
var cardDao = database.CardDao;
var battleDao = database.BattleDao;
var tradeDao = database.TradeDao;

// In Memory DAOs
//var userDao = new InMemoryUserDao();
//var messageDao = new InMemoryMessageDao();

var userManager = new UserManager(userDao);
var packageManager = new PackageManager(packageDao);
var cardManager = new CardManager(cardDao);
var battleManager = new BattleManager(battleDao);
var tradeManager = new TradeManager(tradeDao);

//Battle Lobbies
List<BattleLobby> battleLobbies = new List<BattleLobby>();

Thread workerThread = new Thread(new ThreadStart(MaintainLobbies));
workerThread.Start();
var router = new Router(userManager, packageManager, cardManager, battleManager, tradeManager, battleLobbies);
var server = new HttpServer(IPAddress.Any, 10001, router);
server.Start();

//Worker Thread to clean up finished lobbies
void MaintainLobbies()
{
    while (true)
    {
        Thread.Sleep(2000);
        for (int i = battleLobbies.Count; i > 0; i--)
        {
            if (battleLobbies[i-1].LobbyDone && battleLobbies[i-1].pickedUpBattleresults == 2)
            {
                battleLobbies.RemoveAt(i - 1);
                Console.WriteLine("Lobby has been removed by workerthread");
            }
        }
    }
}