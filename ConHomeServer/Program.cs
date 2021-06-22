using System;
using System.Net.Sockets;

namespace ConHomeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConServer server = new  ConServer(5000);
            
            while (server.isRuning)
            {
                //SomeCode
            }
        }
    }
}
