using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_Server
{
    internal class Program_Server
    {
        static void Main()
        {
            Console.WriteLine("Program_Server runs!");
            Server server = new Server();
            server.RunServer();
        }
    }
}