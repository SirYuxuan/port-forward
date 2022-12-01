using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PortForward.Common
{
    class RequestHandler
    {
        public RequestHandler(Socket sock)
        {
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            List<byte> requestBytesList = new List<byte>();

            // Get the HTTP headers first
            Header = "";
            while (true)
            {
                RequestBytes = new byte[1];
                int bytesRec = sock.Receive(RequestBytes);
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

            Uri uri = new Uri(Resource);

            Host = uri.Host;
            Port = uri.Port;

            for (int i = 1; i < headers.Length; i++)
            {
                RequestHeaders += headers[i] + "\r\n";
            }

            clientSock = sock;

            RequestBytes = requestBytesList.ToArray();
            /*
            Console.WriteLine("Request method:" + RequestMethod);
            Console.WriteLine("Resource: " + Resource);
            Console.WriteLine("HttpVersion:" + HttpVersion);
            Console.WriteLine("Host: " + Host);
            Console.WriteLine("Request Headers: \n" + RequestHeaders); 
                */
        }
        public void DoRequest()
        {
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            const int BUFSZ = 1024;

            try
            {
                // IPAddress and IPEndPoint represent the endpoint that will
                //   receive the request.
                // Get the first IPAddress in the list using DNS.

                IPAddress hostadd = Dns.GetHostEntry(Host).AddressList[0];
                if (Host == "localhost")
                {
                    hostadd = IPAddress.Loopback;
                }
                IPEndPoint remoteEndPoint = new IPEndPoint(hostadd, Port);

                //Console.WriteLine(remoteEndPoint.ToString());

                //Creates the Socket for sending data over TCP.
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.ReceiveTimeout = 20 * 1000; // Set time out (milliseconds)

                // Connects to the host using IPEndPoint.
                s.Connect(remoteEndPoint);
                s.Send(RequestBytes, RequestBytes.Length, SocketFlags.None);

                // Get the HTTP headers first
                List<byte> ResponseBytesList = new List<byte>();
                string header = "";
                while (true)
                {

                    byte[] bytes = new byte[1];
                    int bytesRec = s.Receive(bytes);
                    //if (bytesRec != 0)
                    //    Console.WriteLine("Receive some bytes");
                    header += System.Text.Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    for (int i = 0; i < bytesRec; i++)
                    {
                        ResponseBytesList.Add(bytes[i]);
                    }

                    if (header.IndexOf("\r\n\r\n") > -1 || header.IndexOf("\n\n") > -1)
                    {
                        //Console.WriteLine("Blank line found");
                        break;
                    }
                }
                Console.WriteLine(header);

                // Break up the headers
                string[] headers = header.Split(new char[] { '\n' });

                // Get the HTTP payload next
                string data = header;
                int contentLength = GetContentLength(headers);

                //Console.WriteLine(utf8.GetString(ResponseBytes, 0, ResponseBytes.Length));

                if (RequestMethod == "HEAD")
                {
                    contentLength = 0; // no contents for "HEAD"
                }
                try
                {
                    if (contentLength > 0)
                    {
                        int bytesRecd = 0;
                        while (bytesRecd < contentLength)
                        {
                            byte[] bytes = new byte[BUFSZ];
                            int bytesNowGot = s.Receive(bytes);
                            for (int i = 0; i < bytesNowGot; i++)
                            {
                                ResponseBytesList.Add(bytes[i]);
                            }
                            bytesRecd += bytesNowGot;
                            data += System.Text.Encoding.UTF8.GetString(bytes, 0, bytesNowGot);
                        }
                    }
                    else
                    {
                        int bytesNowGot = 0;
                        do
                        {
                            byte[] bytes = new byte[BUFSZ];
                            bytesNowGot = s.Receive(bytes);
                            for (int i = 0; i < bytesNowGot; i++)
                            {
                                ResponseBytesList.Add(bytes[i]);
                            }
                            data += System.Text.Encoding.UTF8.GetString(bytes, 0, bytesNowGot);
                        } while (bytesNowGot > 0);
                    }
                    ResponseBytes = ResponseBytesList.ToArray();
                    clientSock.Send(ResponseBytes);
                }
                catch (System.Exception)
                {

                }
                //Console.WriteLine("data \n" + data);
                s.Shutdown(SocketShutdown.Both);
                s.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());

            }
        }// DoRequest

        private static int GetContentLength(string[] headers)
        {
            int rval = 0;
            for (int i = 1; i < headers.Length; ++i) // 0 = requestLine
            {
                string[] hElem = headers[i].Split(new char[] { ':' });
                if (hElem.Length >= 2)
                {
                    if (hElem[0].Trim().ToLowerInvariant().Equals("content-length"))
                    {
                        rval = Int32.Parse(hElem[1].Trim());
                    }
                }
            }
            return rval;
        } // GetContentLength
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
