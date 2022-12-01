using PortForward.Bean;
using PortForward.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace PortForward.Common
{
    /// <summary>
    /// Socket Server 核心服务 中转服务
    /// </summary>
    class SocketServer
    {
        public readonly ServerBean serverBean;
        private readonly TextBox textBox;
        //此变量标识此服务是否正常工作
        private bool isRuning = true;
        public SocketServer(ServerBean serverBean, TextBox textBox)
        {
            this.serverBean = serverBean;
            this.textBox = textBox;
        }

        private TcpListener listener;
        private HttpListener httpListener;

        /// <summary>
        /// 启动一个新的监听
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            _ = LogUtil.GetLog(textBox);
            //默认值处理
            string ipStr = Common.Config.BindIp;
            if (!CheckIP(ipStr))
            {
                ipStr = Common.DEFAULT_IP;
            }
            int port = serverBean.Port;
            if (port is 0)
            {
                port = 19730;
            }
            IPAddress ip = IPAddress.Parse("0.0.0.0");
            listener = new TcpListener(ip, port);
            listener.Start();
            Thread t = new Thread(AcceptTcpClient);
            t.Start();
            /*HttpListener httpListener =new HttpListener();
            httpListener.Prefixes.Add("http://127.0.0.1:" + port + "/");
            httpListener.Start();
            new Thread(httpListenerM).Start(httpListener);*/
            return true;
        }
        public void Close()
        {
            this.isRuning = false;
            this.listener.Stop();
            Common.serverBeanPairs.Remove(serverBean.Id);
        }
        void httpListenerM(object data) 
        {
            while (true) 
            {
                HttpListener httpListener = data as HttpListener;
                HttpListenerContext context = httpListener.GetContext();//阻塞
                HttpListenerRequest request = context.Request;
                string postData = new StreamReader(request.InputStream).ReadToEnd();
                foreach (string key in request.Headers.AllKeys) 
                {
                    Console.WriteLine("Key:" + key + "  value:" + request.Headers[key]);
                }
                string result = request.HttpMethod.ToString() +" "+ request.Url.PathAndQuery.ToString() + " HTTP/1.1 \r\n";
                Console.WriteLine(result);
                Console.WriteLine("收到请求：" + postData);
                Console.WriteLine("收到请求：" + request.ToString());
                HttpListenerResponse response = context.Response;//响应
                string responseBody = "响应";
                response.ContentLength64 = System.Text.Encoding.UTF8.GetByteCount(responseBody);
                response.ContentType = "text/html; Charset=UTF-8";
                //输出响应内容
                Stream output = response.OutputStream;
                using (StreamWriter sw = new StreamWriter(output))
                {
                    sw.Write(responseBody);
                }
                Console.WriteLine("响应结束");
            }
        }
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
        }
        void sendMsg(object data1) 
        {
            TcpClient client = data1 as TcpClient;
            //获取客户端IP 判断是否在白名单中
            string clientIp = client.Client.RemoteEndPoint.ToString();
            clientIp = clientIp.Contains(":") ? clientIp.Split(new char[1] { ':' })[0] : clientIp;


            byte[] buffer = new byte[client.ReceiveBufferSize];
            NetworkStream stream = client.GetStream();//获取网络流  
            stream.Read(buffer, 0, buffer.Length);//读取网络流中的数据  
            string receiveString = Encoding.Default.GetString(buffer).Trim('\0');//转换成字符串  


            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.ReceiveTimeout = 20 * 1000; // Set time out (milliseconds)

            // Connects to the host using IPEndPoint.
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(Common.ToIP(serverBean.OldIp)), Convert.ToInt32(Common.ToPort(serverBean.OldIp)));
            s.Connect(remoteEndPoint);
            s.Send(buffer, buffer.Length, SocketFlags.None);

            List<byte> ResponseBytesList = new List<byte>();
            string header = "";
            while (true)
            {
                try
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
                catch (Exception) { }
            }
            Console.WriteLine(header);

            string[] headers = header.Split(new char[] { '\n' });
            const int BUFSZ = 1024;
            // Get the HTTP payload next
            string data = header;
            int contentLength = GetContentLength(headers);
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
                client.Client.Send(Encoding.UTF8.GetBytes(data));
            }
            catch (System.Exception)
            {

            }
            //Console.WriteLine("data \n" + data);
            s.Shutdown(SocketShutdown.Both);
            s.Close();

            /* //链接源地址发送请求
             TcpClient clientSocket = new TcpClient();
             try
             {
                 clientSocket.Connect(Common.ToIP(serverBean.OldIp), Convert.ToInt32(Common.ToPort(serverBean.OldIp)));
                 byte[] request = Encoding.UTF8.GetBytes(receiveString);
                 clientSocket.Client.Send(request);
                 byte[] responseByte = new byte[1024000];
                 int len = clientSocket.Client.Receive(responseByte);
                 clientSocket.Close();

                 //给访问者推送数据
                 stream.Write(responseByte, 0, len);

                 stream.Close();//关闭流  
                 client.Close();//关闭Client 

             }
             catch (Exception)
             {
                 stream.Close();//关闭流  
                 client.Close();//关闭Client  
             }*/
        }
        public byte[] ResponseBytes
        {
            get;
            private set;
        }
        void AcceptTcpClient(object data)
        {
            while (isRuning)
            {

                try
                {
                    //等待接受客户链接
                    Socket socket = listener.AcceptSocket();//接受一个Client  
                    Proxy proxy = new Proxy(socket);
                    //Proxy类实例化
                    Thread thread = new Thread(new ThreadStart(proxy.Run));
                    //创建线程
                    thread.Start();
                }
                catch (Exception) { }
            }
        }
        private bool CheckIP(string ipStr)
        {
            bool blnTest = false;
            Regex regex = new Regex("^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$");
            blnTest = regex.IsMatch(ipStr);
            if (blnTest == true)
            {
                string[] strTemp = ipStr.Split(new char[] { '.' }); // textBox1.Text.Split(new char[] { ‘.’ });
                int nDotCount = strTemp.Length - 1; //字符串中.的数量，若.的数量小于3，则是非法的ip地址
                if (3 == nDotCount)//判断字符串中.的数量
                {
                    for (int i = 0; i < strTemp.Length; i++)
                    {
                        if (Convert.ToInt32(strTemp[i]) > 255)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }




    }
}
