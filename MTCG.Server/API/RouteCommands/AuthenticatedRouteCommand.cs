using MTCG_Server.MTCG.Core.Response;
using MTCG_Server.MTCG.Core.Routing;
using MTCG_Server.MTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server.MTCG.API.RouteCommands
{
    internal abstract class AuthenticatedRouteCommand : IRouteCommand
    {
        public User Identity { get; private set; }

        public AuthenticatedRouteCommand(User identity)
        {
            Identity = identity;
        }

        public abstract Response Execute();
    }
}
