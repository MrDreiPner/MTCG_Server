using MTCG.CardTypes;
using MTCG.MTCG.DAL;
using Npgsql;
using MTCG.MTCG.Models;
using System.Data;
using System.Linq.Expressions;
using MTCG.MTCG.BLL;
using MTCG;
using MTCG.DeckStack;
using System.Reflection.PortableExecutable;

namespace MTCG.MTCG.DAL
{
    public class DatabaseTradeDao : DatabaseBaseDao, ITradeDao
    {
        private const string CheckIfTradeableCommand = "SELECT * FROM cards WHERE ownerid = @username AND cid = @cid AND inDeck = false";
        private const string CheckIfTradeExistsCommand = "SELECT * FROM trades WHERE tradeid = @tradeid";
        private const string InsertTradeDealCommand = @"
INSERT INTO trades(tradeid, cid, type, mindmg, creator) VALUES (@tradeid, @cid, @type, @mindmg, @username);
UPDATE cards SET inTrade = true WHERE cid = @cid;
";
        private const string GetAllTradeDealsCommand = "SELECT * FROM trades";
        private const string UpdateDeckCommand = "UPDATE cards SET inDeck = true WHERE cid = @cid";
        public DatabaseTradeDao(string connectionString) : base(connectionString)
        {
        }
        public bool CheckValidity(string tradeID, string cardID, string username)
        {
            return true;
        }
        public void CompleteTrade(string tradeID, string cardID, string username)
        {

        }
        public void CreateTrade(TradeDeal tradeDeal, string username)
        {
           ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(CheckIfTradeableCommand, connection);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("cid", tradeDeal.CardToTrade);
                var result = cmd.ExecuteReader();
                if (!result.Read())
                {
                    throw new WrongCardOwnerException();
                }
                return 0;
            });
            ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(CheckIfTradeExistsCommand, connection);
                cmd.Parameters.AddWithValue("tradeid", tradeDeal.Id);
                var result = cmd.ExecuteReader();
                if (result.Read())
                {
                    throw new CardAlreadyExistsException();
                }
                return 0;
            });
            ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(InsertTradeDealCommand, connection);
                cmd.Parameters.AddWithValue("tradeid", tradeDeal.Id);
                cmd.Parameters.AddWithValue("cid", tradeDeal.CardToTrade);
                cmd.Parameters.AddWithValue("type", tradeDeal.Type);
                cmd.Parameters.AddWithValue("mindmg", tradeDeal.MinimumDamage);
                cmd.Parameters.AddWithValue("username", username);
                var result = cmd.ExecuteNonQuery();
                return 0;
            });
        }

        public List<TradeDeal> GetTradeDeals()
        {
            List<TradeDeal> newDeals = new List<TradeDeal>();
            return ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(GetAllTradeDealsCommand, connection);
                var result = cmd.ExecuteReader();
                int counter = 0;
                while (result.Read())
                {
                    
                }
                if (counter == 0)
                {
                    throw new MessageNotFoundException();
                }
                return newDeals;
            });
        }
        public void DeleteTrade(string tradeID, string username)
        {

        }

    }
}


