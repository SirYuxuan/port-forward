using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortForward.Bean
{
    class Config
    {
        private int startPort;
        private string bindIp;
        private string url;
        private string writePath;
        private long extractTheInterval;
        private string whiteList;
        private long validTime;

        public int StartPort { get => startPort; set => startPort = value; }
        public string BindIp { get => bindIp; set => bindIp = value; }
        public string Url { get => url; set => url = value; }
        public string WritePath { get => writePath; set => writePath = value; }
        public long ExtractTheInterval { get => extractTheInterval; set => extractTheInterval = value; }
        public string WhiteList { get => whiteList; set => whiteList = value; }
        public long ValidTime { get => validTime; set => validTime = value; }
    }
}
