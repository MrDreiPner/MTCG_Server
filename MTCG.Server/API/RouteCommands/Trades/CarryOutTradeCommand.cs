﻿using MTCG.MTCG.API.RouteCommands;
using MTCG.MTCG.BLL;
using MTCG.MTCG.Core.Response;
using MTCG.MTCG.Models;
using MTCG.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.MTCG.DAL;
using Newtonsoft.Json;
using Microsoft.VisualBasic;

namespace MTCG.API.RouteCommands.Trades
{
    internal class CarryOutTradeCommand : AuthenticatedRouteCommand
    {
        private readonly ITradeManager _tradeManager;
        private readonly string _tradeID;
        private readonly string _cardID;
        private readonly int _checkArgs;

        public CarryOutTradeCommand(ITradeManager tradeManager, User identity, List<string> cardID, string tradeID) : base(identity)
        {
            if (Identity.Username != null)
            {
                List<string> cards = new List<string>();
                cards = cardID;
                _checkArgs= cards.Count;
                _tradeManager = tradeManager;
                _tradeID = tradeID;
                _cardID = cards[0];
            }
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                if (_checkArgs == 1)
                {
                    _tradeManager.Trade(_tradeID, _cardID, Identity.Username);
                    response.StatusCode = StatusCode.Ok;
                }
                else
                    throw new TooFewArgumentsException();
            }
            catch (Exception ex)
            {
                if (ex is UserNotFoundException)
                    response.StatusCode = StatusCode.Unauthorized;
                else if (ex is WrongCardOwnerException)
                    response.StatusCode = StatusCode.Forbidden;
                else if (ex is MessageNotFoundException)
                    response.StatusCode = StatusCode.NotFound;
                else if (ex is TooFewArgumentsException)
                    response.StatusCode = StatusCode.BadRequest;

            }
            return response;
        }
    }
}
