using MTCG_Server.CardTypes;
using SWE1.MTCG.DAL;
using Npgsql;
using SWE1.MTCG.API.RouteCommands.Messages;
using SWE1.MTCG.Models;
using System.Data;
using System.Linq.Expressions;
using SWE1.MTCG.BLL;
using MTCG_Server;

namespace SWE1.MTCG.DAL
{
    internal class DatabasePackageDao : DatabaseBaseDao, IPackageDao
    {
        private const string InsertPackageCommand = @"
INSERT INTO package (pid) VALUES (default);
SELECT pid FROM package WHERE pid = (SELECT MAX(pid) from package);";
        private const string InsertCardCommand = "INSERT INTO cards(cid, cardname, elementid, dmg, indeck, intrade, ownerid, packid, type) VALUES (@cid, @cardname, @elementid, @dmg, false, false, null, @packid, @type)";
        //private const string DeletePackageCommand = "DELETE FROM package WHERE pid=@pid";
        private const string DeleteFaultyPackageCommand = @"
DELETE FROM package WHERE pid=@pid;
DELETE FROM cards WHERE packid=@pid";
        private const string BuyPackageCommand = @"
UPDATE users SET coins = @coins  WHERE username = @username;
UPDATE cards SET ownerID = (SELECT username FROM users WHERE username = @username) WHERE packID = (SELECT MIN(pid) FROM package);
DELETE FROM package WHERE pid = (SELECT MIN(pid) FROM package);
SELECT * FROM cards WHERE ownerid = @username AND packID = (SELECT MIN(packID) FROM cards WHERE ownerid = @username);
UPDATE cards SET packID = null WHERE ownerID = @username;
";
        private const string RevertPurchaseCommand = "UPDATE users SET coins = @coins WHERE username = @username";
        private const string CheckMoneyOfUser = "SELECT coins FROM users WHERE username = @username";
        //private const string TransferOwnership = "UPDATE cards SET ownerID=@ownerID, packID=0 where packID=@packID";
        public DatabasePackageDao(string connectionString) : base(connectionString)
        {
        }

        public Package? GetOldestPackage(string username)
        {
            int coins = 0;
            ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(CheckMoneyOfUser, connection);
                cmd.Parameters.AddWithValue("username", username);
                // take the first row, if any
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    coins = Convert.ToInt32(reader["coins"]);
                }
                if(coins < 5) {
                    Console.WriteLine("You are out of cash! Cash: " + coins);
                    throw new NotEnoughMoneyException();
                }
                else
                {
                    coins -= 5;
                }
                return 0;
            });
            return ExecuteWithDbConnection((connection) =>
            {
                Package? package = null;
                List<Card> cards = new List<Card>();
                try
                { 
                    using var cmd = new NpgsqlCommand(BuyPackageCommand, connection);
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("coins", coins);
                    // take the first row, if any
                    using var reader = cmd.ExecuteReader();
                    bool packFound = false;
                    while (reader.Read())
                    {
                        packFound = true;
                        string? cardname = Convert.ToString(reader["cardname"]);
                        string? id = Convert.ToString(reader["cid"]);
                        int dmg = Convert.ToInt32(reader["dmg"]);
                        Console.WriteLine("We found card: " + cardname);
                        Card newCard;
                        if (cardname.Length < 5)
                            newCard = new Monster(id, cardname, dmg);
                        else
                        {
                            if (cardname.Substring(cardname.Length - 5) == "Spell")
                                newCard = new Spell(id, cardname, dmg);
                            else
                                newCard = new Monster(id, cardname, dmg);
                        }
                        newCard.OwnerID = username;
                        cards.Add(newCard);
                    }
                    if (!packFound)
                    {
                        throw new MessageNotFoundException();
                    }
                }
                catch(MessageNotFoundException) 
                {
                    ExecuteWithDbConnection((connection) =>
                    {
                        coins += 5;
                        using var cmd = new NpgsqlCommand(RevertPurchaseCommand, connection);
                        cmd.Parameters.AddWithValue("username", username);
                        cmd.Parameters.AddWithValue("coins", coins);
                        // take the first row, if any
                        cmd.ExecuteNonQuery();
                        return 0;
                    });
                    cards = null;
                    throw new MessageNotFoundException();
                }
                package = new Package(cards, 0);
                return package;
            });
        }

        public Package? AddPackage(List<Card> packContent)
        {
            int packID = 0;
            ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(InsertPackageCommand, connection);
                var result = cmd.ExecuteReader();
                if (result.Read())
                {
                    packID = Convert.ToInt32(result["pid"]);
                }
                return 0;
            });
            return ExecuteWithDbConnection((connection) =>
            {
                Package? package = null;
                try
                {
                    foreach (Card card in packContent)
                    {
                        Console.WriteLine("Adding Card : " + card.Name + " ID: " + card.GuID + " Damage: " + card.Dmg + " Type: " + card.Type + " Element: " + card.ElementID);
                        using var newcmd = new NpgsqlCommand(InsertCardCommand, connection);
                        newcmd.Parameters.AddWithValue("cid", card.GuID);
                        newcmd.Parameters.AddWithValue("cardname", card.Name);
                        newcmd.Parameters.AddWithValue("elementid", card.ElementID);
                        newcmd.Parameters.AddWithValue("dmg", card.Dmg);
                        newcmd.Parameters.AddWithValue("packid", packID);
                        newcmd.Parameters.AddWithValue("type", card.Type);
                        //cmd.Prepare();
                        int affectedRows = newcmd.ExecuteNonQuery();
                        if (affectedRows == 1)
                        {
                            Console.WriteLine("We added a card!");
                        }
                        //cmd.Dispose();

                    }
                }
                catch (PostgresException)
                {
                    ExecuteWithDbConnection((connection) =>
                    {
                        Console.WriteLine("We are deleting the Package");
                        using var cmd = new NpgsqlCommand(DeleteFaultyPackageCommand, connection);
                        cmd.Parameters.AddWithValue("pid", packID);
                        var result = cmd.ExecuteNonQuery();;
                        //cmd.Dispose();
                        return 0;
                    });
                    throw new CardAlreadyExistsException();
                }


                package = new Package(packContent, packID);
                return package;
            });
        }
    }
}