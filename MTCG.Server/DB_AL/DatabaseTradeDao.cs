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
        private const string CheckIfTradeableCommand = "SELECT * FROM cards WHERE ownerid = @username";
        private const string GetUserDeckCommand = "SELECT * FROM cards WHERE ownerid = @username AND inDeck = true";
        private const string CheckOwnerCommand = "SELECT * FROM cards WHERE ownerid = @username AND inTrade = false AND cid = @cid";
        private const string ClearDeckCommand = "UPDATE cards SET inDeck = false WHERE ownerid = @username AND inDeck = true";
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
           /* ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(CheckIfTradeableCommand, connection);
                var result = cmd.ExecuteReader();
                if (result.Read())
                {
                    packID = Convert.ToInt32(result["pid"]);
                }
                return 0;
            });
            ExecuteWithDbConnection((connection) =>
            {
                Package? package = null;
                try
                {
                    foreach (Card card in packContent)
                    {
                        //Console.WriteLine("Adding Card : " + card.Name + " ID: " + card.GuID + " Damage: " + card.Dmg + " Type: " + card.Type + " Element: " + card.ElementID);
                        using var newcmd = new NpgsqlCommand(InsertCardCommand, connection);
                        newcmd.Parameters.AddWithValue("cid", card.GuID);
                        newcmd.Parameters.AddWithValue("cardname", card.Name);
                        newcmd.Parameters.AddWithValue("elementid", card.ElementID);
                        newcmd.Parameters.AddWithValue("dmg", card.Dmg);
                        newcmd.Parameters.AddWithValue("packid", packID);
                        newcmd.Parameters.AddWithValue("type", card.Type);
                        //cmd.Prepare();
                        int affectedRows = newcmd.ExecuteNonQuery();
                        //cmd.Dispose();

                    }
                }
                catch (PostgresException)
                {
                    ExecuteWithDbConnection((connection) =>
                    {
                        //Console.WriteLine("We are deleting the Package");
                        using var cmd = new NpgsqlCommand(DeleteFaultyPackageCommand, connection);
                        cmd.Parameters.AddWithValue("pid", packID);
                        var result = cmd.ExecuteNonQuery(); ;
                        //cmd.Dispose();
                        return 0;
                    });
                    throw new CardAlreadyExistsException();
                }


                package = new Package(packContent, packID);
                return package;
            });*/
        }
        public List<TradeDeal> GetTradeDeals()
        {
            List<TradeDeal> newDeals = new List<TradeDeal>();
            return newDeals;
        }
        public void DeleteTrade(string tradeID, string username)
        {

        }

    }
}


