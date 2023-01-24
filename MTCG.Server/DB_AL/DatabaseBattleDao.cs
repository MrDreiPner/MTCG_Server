﻿using MTCG_Server.CardTypes;
using SWE1.MTCG.DAL;
using Npgsql;
using SWE1.MTCG.API.RouteCommands.Messages;
using SWE1.MTCG.Models;
using System.Data;
using System.Linq.Expressions;
using SWE1.MTCG.BLL;
using MTCG_Server.DeckStack;
using System.Reflection.PortableExecutable;
using MTCG_Server.Models;

namespace SWE1.MTCG.DAL
{
    internal class DatabaseBattleDao : DatabaseBaseDao, IBattleDao
    {
        private const string ShowUserStatsCommand = "SELECT elo, wins, losses FROM users WHERE username = @username";
        private const string ShowScoreboardCommand = "SELECT username, elo, wins, losses FROM users WHERE username != 'admin' ORDER BY elo DESC";
        private const string GetPlayerCommand = "SELECT * FROM users WHERE username = @username";
        private const string GetPlayerDeckCommand = "SELECT * FROM cards WHERE ownerid = @username AND inDeck = true";
        public DatabaseBattleDao(string connectionString) : base(connectionString)
        {
        }
        public UserStats ShowUserStats(string username)
        {
            return ExecuteWithDbConnection((connection) => 
            {
                UserStats stats;
                using var cmd = new NpgsqlCommand(ShowUserStatsCommand, connection);
                cmd.Parameters.AddWithValue("username", username);
                using var reader = cmd.ExecuteReader();
                int elo, wins, losses;
                if(reader.Read())
                {
                    elo = Convert.ToInt32(reader["elo"]);
                    wins = Convert.ToInt32(reader["wins"]);
                    losses =Convert.ToInt32(reader["losses"]);
                }
                else
                {
                    throw new MessageNotFoundException();
                }
                stats = new UserStats(username, elo, wins, losses);
                return stats;
            });
        }

        public List<UserStats> ShowScoreboard()
        {
            return ExecuteWithDbConnection((connection) =>
            {
                List<UserStats> stats = new List<UserStats>();
                using var cmd = new NpgsqlCommand(ShowScoreboardCommand, connection);
                using var reader = cmd.ExecuteReader();
                int elo, wins, losses;
                string name;
                while (reader.Read())
                {
                    UserStats newStats;
                    name = Convert.ToString(reader["username"]);
                    elo = Convert.ToInt32(reader["elo"]);
                    wins = Convert.ToInt32(reader["wins"]);
                    losses = Convert.ToInt32(reader["losses"]);
                    newStats = new UserStats(name, elo, wins, losses);
                    stats.Add(newStats);
                }
                return stats;
            });
        }

        public BattleUser StartBattle(string username)
        {
            BattleUser player = new BattleUser();
            ExecuteWithDbConnection((connection) =>
            {
                using var cmd = new NpgsqlCommand(GetPlayerCommand, connection);
                cmd.Parameters.AddWithValue("username", username);
                using var reader = cmd.ExecuteReader();
                int elo, wins, losses;
                if (reader.Read())
                {
                    player.Uid = Convert.ToString(reader["username"]);
                    player.Username = Convert.ToString(reader["name"]);
                    player.Elo = Convert.ToInt32(reader["elo"]);
                    player.Wins = Convert.ToInt32(reader["wins"]);
                    player.Losses = Convert.ToInt32(reader["losses"]);
                }
                else
                {
                    throw new UserNotFoundException();
                }
                return player;
            });
            Console.WriteLine("Passed Player for battle is: " + player.Uid + " their name is: "+ player.Username +" with ELO: " + player.Elo);
            
            List<Card> cards = new List<Card>();
            ExecuteWithDbConnection((connection) =>
            {
                try
                {
                    using var cmd = new NpgsqlCommand(GetPlayerDeckCommand, connection);
                    cmd.Parameters.AddWithValue("username", username);
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
                catch (MessageNotFoundException)
                {
                    throw new MessageNotFoundException();
                }
                return cards;
            });
            player.Deck = new Deck(player.Uid, cards);
            return player;
        }
    }
}


