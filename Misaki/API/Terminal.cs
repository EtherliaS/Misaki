using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.API // continue with server - server
{
    internal class Terminal
    {
        
        TcpClient client;
        int port;
        string host = "127.0.0.1";
        public Terminal()
        {
            port = FindFreePort();
        }
        public Task Listen()
        {
            return Task.FromResult(0);
        }
        public Task<T> Request<T>()
        {

            return default;
        }
        public Task Start()
        {
            client = new TcpClient(host, port);
            return Task.CompletedTask;
        }

        static int FindFreePort()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }
}
