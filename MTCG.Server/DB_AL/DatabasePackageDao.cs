using MTCG_Server.CardTypes;
using SWE1.MTCG.DAL;
using Npgsql;
using SWE1.MTCG.API.RouteCommands.Messages;
using SWE1.MTCG.Models;
using System.Data;
using System.Linq.Expressions;
using SWE1.MTCG.BLL;

namespace SWE1.MTCG.DAL
{
    internal class DatabasePackageDao : DatabaseBaseDao, IPackageDao
    {
        private const string InsertPackageCommand = @"
INSERT INTO package (pid) VALUES (default);
SELECT pid FROM package WHERE pid = (SELECT MAX(pid) from package);";
        private const string InsertCardCommand = "INSERT INTO cards(cid, cardname, elementid, dmg, indeck, intrade, ownerid, packid, type) VALUES (@cid, @cardname, @elementid, @dmg, false, false, null, @packid, @type)";
        private const string DeletePackageCommand = "DELETE FROM packages WHERE pid=@pid";
        private const string CheckPresenceCommand = "SELECT cid FROM cards WHERE cid = @cid";
        private const string SelectOldestPackage = "SELECT * FROM cards WHERE pid = (SELECT MIN(packID) FROM cards)";
        private const string TransferOwnership = "UPDATE cards SET ownerID=@ownerID, packID=0 where packID=@packID";
        public DatabasePackageDao(string connectionString) : base(connectionString)
        {
        }

        public Package? GetOldestPackage()
        {
            return ExecuteWithDbConnection((connection) =>
            {
                List<Card> cards = new List<Card>();
                Package? package = null;

                using var cmd = new NpgsqlCommand(SelectOldestPackage, connection);

                // take the first row, if any
                using var reader = cmd.ExecuteReader();
                int packID = 0;
                while (reader.Read())
                {
                    packID = Convert.ToInt32(reader["packID"]);
                    string? cardname = Convert.ToString(reader["cardname"]);
                    string? id = Convert.ToString(reader["cid"]);
                    int dmg = Convert.ToInt32(reader["dmg"]);
                    Card newCard;
                    if (cardname.Substring(cardname.Length - 5) == "Spell")
                    {
                        newCard = new Spell(id, cardname, dmg);
                    }
                    else
                    {
                        newCard = new Monster(id, cardname, dmg);
                    }
                    cards.Add(newCard);
                }
                foreach (Card card in cards)
                {

                }
                package = new Package(cards, packID);
                return package;
            });
        }

        public Package? AddPackage(List<Card> packContent)
        {
            int packID = 0;
            ExecuteWithDbConnection((connection) =>
            {
                Console.WriteLine("We are in the PackageDao");
                using var cmd = new NpgsqlCommand(InsertPackageCommand, connection);
                var result = cmd.ExecuteReader();
                if (result.Read())
                {
                    packID = Convert.ToInt32(result["pid"]);
                }
                Console.WriteLine("Returned PID is: " + packID);
                //cmd.Dispose();
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
                    throw new CardAlreadyExistsException();
                }


                package = new Package(packContent, packID);
                return package;
            });
        }
    }
}