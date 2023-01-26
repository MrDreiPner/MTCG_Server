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
        private const string CheckIfTradeableCommand = "SELECT * FROM cards WHERE ownerid = @username AND cid = @cid AND inDeck = false AND inTrade = false";
        private const string CheckIfTradeExistsCommand = "SELECT * FROM trades WHERE tradeid = @tradeid";
        private const string InsertTradeDealCommand = @"
INSERT INTO trades(tradeid, cid, type, mindmg, creator) VALUES (@tradeid, @cid, @type, @mindmg, @username);
UPDATE cards SET inTrade = true WHERE cid = @cid;
";
        private const string GetAllTradeDealsCommand = "SELECT * FROM trades";
        private const string CheckIfDealExistsCommand = "SELECT * FROM trades WHERE tradeid = @tradeid";
        private const string DeleteTradeDealCommand = @"
UPDATE cards SET intrade = false WHERE cid = (SELECT cid FROM trades WHERE tradeid = @tradeid);
DELETE FROM trades WHERE tradeid = @tradeid;
";
        private const string CheckIfOwnerCommand = "SELECT * FROM trades WHERE creator = @username AND tradeid = @tradeid";
        private const string CompleteTradeCommand = @"
UPDATE cards SET intrade = false, ownerid = @username WHERE cid = (SELECT cid FROM trades WHERE tradeid = @tradeid);
UPDATE cards SET intrade = false, ownerid = (SELECT creator FROM trades WHERE tradeid = @tradeid) WHERE cid = @cardid;
";
        public DatabaseTradeDao(string connectionString) : base(connectionString)
        {
        }
        public bool CheckValidity(string tradeID, string cardID, string username)
        {
            int minDmg = 0;
            int offeredDmg = 0;
            string? requestedType = "2", offeredType = "1";
            
            ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(CheckIfTradeableCommand, connection);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("cid", cardID);
                var result = cmd.ExecuteReader();
                if (result.Read())
                {
                    offeredDmg = Convert.ToInt32(result["dmg"]);
                    offeredType = Convert.ToString(result["type"]);
                }
                else
                {
                    throw new WrongCardOwnerException();
                }
                return 0;
            });
            ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(CheckIfTradeExistsCommand, connection);
                cmd.Parameters.AddWithValue("tradeid", tradeID);
                var result = cmd.ExecuteReader();
                if (result.Read())
                {
                    string? creator = Convert.ToString(result["creator"]);
                    minDmg = Convert.ToInt32(result["mindmg"]);
                    requestedType = Convert.ToString(result["type"]);
                    if (creator.Equals(username, StringComparison.CurrentCultureIgnoreCase))
                        throw new WrongCardOwnerException();
                }
                else
                {
                    throw new MessageNotFoundException();
                }
                return 0;
            });
            Console.WriteLine("TradeOffer:\nOffered DMG | Requested DMG - " + offeredDmg + " | " + minDmg);
            Console.WriteLine("Offered Type | Requested Type - " + offeredType + " | " + requestedType);

            if (minDmg <= offeredDmg && offeredType.Equals(requestedType, StringComparison.CurrentCultureIgnoreCase) && requestedType != null)
            {
                return true;
            }
            else
            {
                throw new WrongCardOwnerException();
            }
        }
        public void CompleteTrade(string tradeID, string cardID, string username)
        {
            ExecuteWithDbConnection((connection) =>
            {
                Console.WriteLine("We are completing the trade");
                using var cmd = new NpgsqlCommand(CompleteTradeCommand, connection);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("tradeid", tradeID);
                cmd.Parameters.AddWithValue("cardid", cardID);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Trade completed");
                return 0;
            });
            ExecuteWithDbConnection((connection) =>
            {
                Console.WriteLine("We are deleting the trade deal");
                using var cmd = new NpgsqlCommand(DeleteTradeDealCommand, connection);
                cmd.Parameters.AddWithValue("tradeid", tradeID);
                cmd.ExecuteNonQuery();
                return 0;
            });
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
                    counter++;
                    TradeDeal newDeal = new TradeDeal(Convert.ToString(result["cid"]),
                                                      Convert.ToString(result["tradeid"]),
                                                      Convert.ToInt32(result["mindmg"]),
                                                      Convert.ToString(result["type"]));
                    newDeals.Add(newDeal);
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
            ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(CheckIfTradeExistsCommand, connection);
                cmd.Parameters.AddWithValue("tradeid", tradeID);
                var result = cmd.ExecuteReader();
                if (!result.Read())
                {
                    throw new MessageNotFoundException();
                }
                return 0;
            });
            ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(CheckIfOwnerCommand, connection);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("tradeid", tradeID);
                var result = cmd.ExecuteReader();
                if (!result.Read())
                {
                    throw new WrongCardOwnerException();
                }
                return 0;
            });
            ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(DeleteTradeDealCommand, connection);
                cmd.Parameters.AddWithValue("tradeid", tradeID);
                var result = cmd.ExecuteNonQuery();
                return 0;
            });
        }

    }
}


