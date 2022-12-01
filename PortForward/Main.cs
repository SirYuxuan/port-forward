using CCWin;
using PortForward.Bean;
using PortForward.Common;
using PortForward.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PortForward
{
    public partial class Main : Skin_VS
    {


        public Main()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e) => Init();
        /// <summary>
        /// 程序启动初始化
        /// </summary>
        private void Init()
        {
            LogUtil log = LogUtil.GetLog(textBox_log);
            log.WriteLog("程序开始初始化...");
            //加载配置文件
            Common.Common.Config = LoadConfig();
            //给所有的配置框TextBox 绑定更改事件
            SaveTextBoxChangedEvent();
            log.WriteLog("程序初始化完毕...");
            new Thread(timer_Tick).Start();

        }

        private void timer_Tick() => timer_Tick(null, null);

        /// <summary>
        /// 给所有的TextBox配置框添加Changed事件用来保存配置文件
        /// </summary>
        private void SaveTextBoxChangedEvent()
        {
            foreach (Control ctl in this.groupBox.Controls)
            {
                if (ctl is TextBox textbox && !"textBox_log".Equals(textbox.Name))
                {
                    textbox.TextChanged += new EventHandler(TextBoxConfigChanged);
                }
            }
        }
        /// <summary>
        /// 配置项被修改的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxConfigChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                //通过反射修改全局的Config内容
                PropertyInfo[] properties = typeof(Config).GetProperties();

                foreach (var property in properties)
                {
                    if (property.Name.Equals(textBox.Tag.ToString()))
                    {
                        try
                        {
                            property.SetValue(Common.Common.Config, textBox.Text);
                        }
                        catch (Exception)
                        {
                            property.SetValue(Common.Common.Config, Convert.ToInt32(textBox.Text));
                        }
                    }
                }


                IniUtil.WriteValue("Config", textBox.Tag.ToString(), textBox.Text);
                if ("ExtractTheInterval".Equals(textBox.Tag.ToString()))
                {
                    //间隔事件变动,调整timer的时间
                    int interval = Convert.ToInt32(textBox.Text);
                    if (interval > 0)
                    {
                        timer.Enabled = true;
                        timer.Interval = interval * 1000 ;
                    }

                }
            }
        }

        /// <summary>
        /// 加载程序配置文件
        /// </summary>
        /// <returns>配置文件</returns>
        private Config LoadConfig()
        {
            Config config = new Config();

            var startPort = IniUtil.ReadValue("Config", "StartPort");
            config.StartPort = startPort.Length == 0 ? 0 : Convert.ToInt32(startPort);

            var extractTheInterval = IniUtil.ReadValue("Config", "ExtractTheInterval");
            config.ExtractTheInterval = extractTheInterval.Length == 0 ? 0 : Convert.ToInt64(extractTheInterval);

            if (config.ExtractTheInterval > 0)
            {
                timer.Enabled = true;
                timer.Interval = Convert.ToInt32(config.ExtractTheInterval * 1000);
            }

            var validTime = IniUtil.ReadValue("Config", "ValidTime");
            config.ValidTime = validTime.Length == 0 ? 0 : Convert.ToInt64(validTime);

            config.BindIp = IniUtil.ReadValue("Config", "BindIp");
            config.Url = IniUtil.ReadValue("Config", "Url");
            config.WritePath = IniUtil.ReadValue("Config", "WritePath");
            config.WhiteList = IniUtil.ReadValue("Config", "WhiteList");

            //写入窗口控件
            textBox_bindIp.Text = config.BindIp;

            textBox_extractTheInterval.Text = config.ExtractTheInterval == 0 ? "" : config.ExtractTheInterval.ToString();
            textBox_startPort.Text = config.StartPort == 0 ? "" : config.StartPort.ToString();
            textBox_url.Text = config.Url;
            textBox_validTime.Text = config.ValidTime == 0 ? "" : config.ValidTime.ToString();
            textBox_whiteList.Text = config.WhiteList;
            textBox_writePath.Text = config.WritePath;

            return config;
        }



        //限制输入框只能输入数字

        private void textBox_extractTheInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void textBox_startPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void textBox_validTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            LogUtil log = LogUtil.GetLog(textBox_log);

            Config config = Common.Common.Config;
            if (config.StartPort < 1 || config.BindIp.Length == 0 || config.Url.Length == 0 || config.ValidTime < 1)
            {
                log.WriteLog("参数不完成,无法开始工作");
                return;
            }
            string resultStr = HttpUtil.Http(config.Url);
            if (resultStr.Trim().Length != 0)
            {
                string[] oldIps = resultStr.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                int port = Common.Common.Config.StartPort + 1;
                foreach (string ip in oldIps)
                {
                    if (String.IsNullOrEmpty(ip))
                    {
                        continue;
                    }

                    //判断这个IP是否已经存在与系统的中转代理中
                    foreach (KeyValuePair<Int32, SocketServer> keyValue in Common.Common.serverBeanPairs)
                    {
                        if (keyValue.Value.serverBean.OldIp.Equals(ip))
                        {
                            //更新创建时间？
                            keyValue.Value.serverBean.CreateTime = Common.Common.GetTimeStamp();
                            goto IPFOR;
                        }

                    }
                    //构建ServerBean进行启动服务


                    while (Common.Common.PortInUse(port))
                    {
                        port++;
                    }


                    ServerBean serverBean = new ServerBean();
                    serverBean.Id = Common.Common.index++;
                    serverBean.OldIp = ip;
                    serverBean.Port = port;
                    serverBean.CreateTime = Common.Common.GetTimeStamp();
                    log.WriteLog("端口:" + port + "转发成功");
                    SocketServer socketServer = new SocketServer(serverBean, textBox_log);
                    bool isSuccess = socketServer.Start();

                    Common.Common.serverBeanPairs.Add(serverBean.Id, socketServer);

                    Ping pingSender = new Ping();
                    PingReply reply = pingSender.Send(Common.Common.ToIP(ip), 120);

                    ListViewItem item = new ListViewItem();
                    item.Text = serverBean.Id.ToString();
                    item.SubItems.Add(port.ToString());
                    item.SubItems.Add(ip);
                    item.SubItems.Add(Common.Common.Config.BindIp + ":" + port);
                    item.SubItems.Add(isSuccess.ToString());
                    item.SubItems.Add(reply.Status.ToString());
                    item.Selected = true;

                    list_view_main.Items.Add(item);
                    item.EnsureVisible();
                IPFOR:;
                }

            }

        }

        private void timer_check_Tick(object sender, EventArgs e)
        {
            LogUtil log = LogUtil.GetLog(textBox_log);
            List<string> writeText = new List<string>();
            try
            {
                File.Delete(Common.Common.Config.WritePath);
            }
            catch (Exception) { }
            for (var i = 0; i < list_view_main.Items.Count; i++)
            {
                int id = Convert.ToInt32(list_view_main.Items[i].SubItems[0].Text);
                long createTime = Common.Common.serverBeanPairs[id].serverBean.CreateTime;
                if (Common.Common.GetTimeStamp() - createTime >= Common.Common.Config.ValidTime * 1000 )
                {
                    log.WriteLog("端口:" + list_view_main.Items[i].SubItems[1].Text + "已过期,转发关闭");
                    list_view_main.Items.RemoveAt(i);
                    Common.Common.serverBeanPairs[id].Close();
                }
                else
                {
                    writeText.Add(list_view_main.Items[i].SubItems[3].Text);
                }
            }
            try
            {
                System.IO.File.WriteAllLines(Common.Common.Config.WritePath, writeText, Encoding.UTF8);
            }
            catch (Exception)
            {
            }


        }
    }
}
