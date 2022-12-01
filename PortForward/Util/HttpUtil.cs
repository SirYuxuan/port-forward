using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PortForward.Util
{
    class HttpUtil
    {
        /// <summary>
        /// 不做catch处理，需要在外部做
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method">默认GET，空则补充为GET</param>
        /// <param name="contenttype">默认json，空则补充为json</param>
        /// <param name="header">请求头部</param>
        /// <param name="data">请求body内容</param>
        /// <returns></returns>
        public static string Http(string url, string method = "GET", string contenttype = "application/json;charset=utf-8", Hashtable header = null, string data = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = string.IsNullOrEmpty(method) ? "GET" : method;
            request.ContentType = string.IsNullOrEmpty(contenttype) ? "application/json;charset=utf-8" : contenttype;
            if (header != null)
            {
                foreach (var i in header.Keys)
                {
                    request.Headers.Add(i.ToString(), header[i].ToString());
                }
            }
            if (!string.IsNullOrEmpty(data))
            {
                Stream RequestStream = request.GetRequestStream();
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                RequestStream.Write(bytes, 0, bytes.Length);
                RequestStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream ResponseStream = response.GetResponseStream();
            StreamReader StreamReader = new StreamReader(ResponseStream, Encoding.GetEncoding("utf-8"));
            string re = StreamReader.ReadToEnd();
            StreamReader.Close();
            ResponseStream.Close();
            return re;
        }
    }
}
