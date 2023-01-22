using SWE1.MTCG.BLL;
using SWE1.MTCG.Core.Response;
using SWE1.MTCG.Models;

namespace SWE1.MTCG.API.RouteCommands.Messages
{
    internal class ShowMessageCommand : AuthenticatedRouteCommand
    {
        private readonly IMessageManager _messageManager;
        private readonly int _messageId;

        public ShowMessageCommand(IMessageManager messageManager, User identity, int messageId) : base(identity)
        {
            _messageId = messageId;
            _messageManager = messageManager;
        }

        public override Response Execute()
        {
            Message? message;
            try
            {
                message = _messageManager.ShowMessage(Identity, _messageId);
            }
            catch (MessageNotFoundException)
            {
                message = null;
            }

            var response = new Response();
            if (message == null)
            {
                response.StatusCode = StatusCode.NotFound;
            }
            else
            {
                response.Payload = message.Content;
                response.StatusCode = StatusCode.Ok;
            }

            return response;
        }
    }
}
