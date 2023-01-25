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
    public class DatabaseCardDao : DatabaseBaseDao, ICardDao
    {
        private const string GetUserCardsCommand = "SELECT * FROM cards WHERE ownerid = @username";
        private const string GetUserDeckCommand = "SELECT * FROM cards WHERE ownerid = @username AND inDeck = true";
        private const string CheckOwnerCommand = "SELECT * FROM cards WHERE ownerid = @username AND inTrade = false AND cid = @cid";
        private const string ClearDeckCommand = "UPDATE cards SET inDeck = false WHERE ownerid = @username AND inDeck = true";
        private const string UpdateDeckCommand = "UPDATE cards SET inDeck = true WHERE cid = @cid";
        public DatabaseCardDao(string connectionString) : base(connectionString)
        {
        }

        public List<Card>? ShowCards(string username)
        {
            List<Card> cards = new List<Card>();
            return ExecuteWithDbConnection((connection) =>
            {
                try
                {
                    using var cmd = new NpgsqlCommand(GetUserCardsCommand, connection);
                    cmd.Parameters.AddWithValue("username", username);
                    using var reader = cmd.ExecuteReader();
                    bool packFound = false;
                    while (reader.Read())
                    {
                        packFound = true;
                        string? cardname = Convert.ToString(reader["cardname"]);
                        string? id = Convert.ToString(reader["cid"]);
                        int dmg = Convert.ToInt32(reader["dmg"]);
                        //Console.WriteLine("We found card: " + cardname);
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
                catch (MessageNotFoundException)
                {
                    throw new MessageNotFoundException();
                }
                return cards;
            });
        }

        public List<Card>? ShowDeck(string username)
        {
            List<Card> cards = new List<Card>();
            return ExecuteWithDbConnection((connection) =>
            {
                try
                {
                    using var cmd = new NpgsqlCommand(GetUserDeckCommand, connection);
                    cmd.Parameters.AddWithValue("username", username);
                    using var reader = cmd.ExecuteReader();
                    bool packFound = false;
                    while (reader.Read())
                    {
                        packFound = true;
                        string? cardname = Convert.ToString(reader["cardname"]);
                        string? id = Convert.ToString(reader["cid"]);
                        int dmg = Convert.ToInt32(reader["dmg"]);
                        //Console.WriteLine("We found card: " + cardname);
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
                catch (MessageNotFoundException)
                {
                    throw new MessageNotFoundException();
                }
                return cards;
            });
        }

        public List<Card>? ConfigureDeck(string username, List<string> arguments)
        {
            ExecuteWithDbConnection((connection) =>
            {
                try
                {
                    Console.WriteLine("Checking owner of passed ID");
                    int check = 0;
                    foreach (string arg in arguments)
                    {
                        Console.WriteLine("Checking ID" + arg);
                        using var cmd = new NpgsqlCommand(CheckOwnerCommand, connection);
                        cmd.Parameters.AddWithValue("username", username);
                        cmd.Parameters.AddWithValue("cid", arg);
                        using var reader = cmd.ExecuteReader();
                        if (reader.Read() == true)
                            check++;
                    }
                    if (check < 4)
                    {
                        throw new WrongCardOwnerException();
                    }
                }
                catch (WrongCardOwnerException)
                {
                    throw new WrongCardOwnerException();
                }
                return 0;
            });
            ExecuteWithDbConnection((connection) =>
            {
                try
                {
                    Console.WriteLine("Clearing Deck cards");
                    using var cmd = new NpgsqlCommand(ClearDeckCommand, connection);
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.ExecuteNonQuery();
                }
                catch (WrongCardOwnerException)
                {
                    throw new WrongCardOwnerException();
                }
                return 0;
            });
            ExecuteWithDbConnection((connection) =>
            {
                Console.WriteLine("Updating Deck cards");
                foreach (string arg in arguments)
                {
                    Console.WriteLine("Updating CID: " + arg);
                    using var cmd = new NpgsqlCommand(UpdateDeckCommand, connection);
                    cmd.Parameters.AddWithValue("cid", arg);
                    cmd.ExecuteNonQuery();
                }
                return 0;
            });
            return ExecuteWithDbConnection((connection) =>
            {
                List<Card> cards = new List<Card>();
                try
                {
                    Console.WriteLine("Fetching new cards");
                    using var cmd = new NpgsqlCommand(GetUserDeckCommand, connection);
                    cmd.Parameters.AddWithValue("username", username);
                    using var reader = cmd.ExecuteReader();
                    bool packFound = false;
                    while (reader.Read())
                    {
                        packFound = true;
                        string? cardname = Convert.ToString(reader["cardname"]);
                        string? id = Convert.ToString(reader["cid"]);
                        int dmg = Convert.ToInt32(reader["dmg"]);
                        //Console.WriteLine("We found card: " + cardname);
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
                        newCard.InDeck = true;
                        newCard.OwnerID = username;
                        cards.Add(newCard);
                    }
                    if (!packFound)
                    {
                        throw new MessageNotFoundException();
                    }
                }
                catch (MessageNotFoundException)
                {
                    throw new MessageNotFoundException();
                }
                return cards;
            });
        }
    }
}


