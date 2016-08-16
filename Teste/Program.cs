using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Teste.TCP;

namespace Teste
{
    class Program
    {
        public static void Main(string[] args)
        {
            Server sv = new Server();
            new Thread(sv.createListener).Start();

            Thread.Sleep(1000);
            Client cli = new Client();
            cli.Connect("localhost", "porra");
        }        
    }
}
