using PortForward.Bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PortForward.Common
{
    class Common
    {
        public static int index = 1;
        private static Config config;
        public static bool isSort = false;
        public static string DEFAULT_IP = "127.0.0.1";
        public static Dictionary<Int32, SocketServer> serverBeanPairs = new Dictionary<int, SocketServer>();

        internal static Config Config { get => config; set => config = value; }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns>时间戳</returns>
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        /// <summary>
        /// 判断指定端口是否被占用
        /// </summary>
        /// <param name="port">端口</param>
        /// <returns>是否占用</returns>
        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }

        public static string ToIP(string str) 
        {
            return str.Contains(":") ? str.Split(new char[1] { ':' })[0] : str;
        }
        public static string ToPort(string str) 
        {
            return str.Contains(":") ? str.Split(new char[1] { ':' })[1] : str;
        }
    }
}
