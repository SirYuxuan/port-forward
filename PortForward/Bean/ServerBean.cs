using PortForward.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortForward.Bean
{
    class ServerBean
    {
        private int id;
        private int port;
        private string oldIp;
        private long createTime;

        public int Id { get => id; set => id = value; }
        public int Port { get => port; set => port = value; }
        public string OldIp { get => oldIp; set => oldIp = value; }
        public long CreateTime { get => createTime; set => createTime = value; }
    }
}
