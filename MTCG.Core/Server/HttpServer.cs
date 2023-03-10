using MTCG.MTCG.Core.Response;
using MTCG.MTCG.Core.Routing;
using System.Net;
using System.Net.Sockets;
using HttpClient = MTCG.MTCG.Core.Client.HttpClient;

namespace MTCG.MTCG.Core.Server
{
    public class HttpServer : IServer
    {
        private bool _listening;
 
        private readonly TcpListener _listener;
        private readonly IRouter _router;
        public Thread? clientThread;

        public HttpServer(IPAddress address, int port, IRouter router)
        {
            _listener = new TcpListener(address, port);
            _router = router;
        }

        public void Start()
        {
            _listener.Start();
            _listening = true;

            while (_listening)
            {
                var connection = _listener.AcceptTcpClient();

                clientThread = new Thread(() => ClientConnection(connection));
                clientThread.Start();
            }
        }

        public void ClientConnection(TcpClient connection)
        {
            // create a new disposable handler for the client connection
            var client = new HttpClient(connection);

            var request = client.ReceiveRequest();
            Response.Response response;

            if (request == null)
            {
                // could not parse request
                response = new Response.Response()
                {
                    StatusCode = StatusCode.BadRequest
                };
            }
            else
            {
                try
                {
                    var command = _router.Resolve(request);
                    if (command != null)
                    {
                        // found a command for this request, now execute it
                        response = command.Execute();
                    }
                    else
                    {

                        // could not find a matching command for the request
                        response = new Response.Response()
                        {
                            StatusCode = StatusCode.BadRequest
                        };
                    }
                }
                catch (RouteNotAuthenticatedException)
                {
                    Console.WriteLine("Identity returned is NULL");
                    response = new Response.Response()
                    {
                        StatusCode = Response.StatusCode.Unauthorized
                    };
                }
                catch (InvalidDataException)
                {
                    response = new Response.Response()
                    {
                        StatusCode = Response.StatusCode.BadRequest
                    };
                }
            }

            client.SendResponse(response);
            client.Close();
        }

        public void Stop()
        {
            _listening = false;
            _listener.Stop();
        }
    }
}
