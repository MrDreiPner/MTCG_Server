using MTCG_Server.CardTypes;
using SWE1.MTCG.DAL;
using Npgsql;
using SWE1.MTCG.API.RouteCommands.Messages;
using SWE1.MTCG.Models;
using System.Data;
using System.Linq.Expressions;
using SWE1.MTCG.BLL;
using MTCG_Server;
using MTCG_Server.DeckStack;
using System.Reflection.PortableExecutable;

namespace SWE1.MTCG.DAL
{
    internal class DatabaseBattleDao : DatabaseBaseDao, IBattleDao
    {
        private const string ShowUserStatsCommand = "";
        public DatabaseBattleDao(string connectionString) : base(connectionString)
        {
        }

    }
}


