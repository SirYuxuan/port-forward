using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PortForward.Common
{
    class Proxy
    {
        Socket m_sockClient;
        public Proxy(Socket sockClient)
        {
            m_sockClient = sockClient;
        }
       
        Byte[] readBuf = new Byte[1024];
        Byte[] buffer = null;
        Encoding ASCII = Encoding.ASCII;
        public void Run()
        {
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            List<byte> requestBytesList = new List<byte>();

            // Get the HTTP headers first
            Header = "";
            while (true)
            {
                RequestBytes = new byte[1];
                int bytesRec = m_sockClient.Receive(RequestBytes);
                requestBytesList.Add(RequestBytes[0]);
                Header += System.Text.Encoding.ASCII.GetString(RequestBytes, 0, bytesRec);
                if (Header.IndexOf("\r\n\r\n") > -1 || Header.IndexOf("\n\n") > -1)
                {
                    break;
                }
            }
            System.Console.WriteLine("*** Headers ***\n " + Header);

            // Break up the headers
            string[] headers = Header.Split(new char[] { '\n' });
            string requestLine = headers[0];
            string[] requestLineElements = requestLine.Split(new char[] { ' ' });

            RequestMethod = requestLineElements[0];
            Resource = requestLineElements[1];
            HttpVersion = requestLineElements[2];


       
            for (int i = 1; i < headers.Length; i++)
            {
                RequestHeaders += headers[i] + "\r\n";
            }
            RequestBytes = requestBytesList.ToArray();

            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("127.0.0.1", 30004);
            clientSocket.Client.Send(RequestBytes);
            byte[] responseByte = new byte[1024000];
            int len = clientSocket.Client.Receive(responseByte);
            clientSocket.Close();

            //给访问者推送数据
            Console.WriteLine("Out: \r\n" + Encoding.UTF8.GetString(responseByte));

        }
        public string Host
        {
            get;
            private set;
        } // Host
        public int Port
        {
            get;
            private set;
        } // Port
        public string HttpVersion
        {
            get;
            set;
        } // HttpVersion
        public string Resource
        {
            get;
            private set;
        } // Resource
        public string RequestMethod
        {
            get;
            private set;
        } // RequestMethod
        public string RequestHeaders
        {
            get;
            private set;
        } // RequestHeaders
        public string Response
        {
            get;
            set;
        } // Response
        public string Header
        {
            get;
            private set;
        }
        public Socket clientSock
        {
            get;
            private set;
        }
        public byte[] RequestBytes
        {
            get;
            private set;
        }
        public byte[] ResponseBytes
        {
            get;
            private set;
        }
    }
}
