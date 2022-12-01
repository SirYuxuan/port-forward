using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PortForward.Util
{
    class SocketServer
    {
        private TcpListener listener;
        private int port;
        private string bindIp;
        public SocketServer(int port)
        {
            this.port = port;
        }

        [Obsolete]
        public bool start() 
        {
            listener = new TcpListener(port);
            listener.Start();
            return false;
        }
    }
}
