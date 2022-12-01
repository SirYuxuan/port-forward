using System.Windows.Forms;

namespace PortForward
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.list_view_main = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.oldProxy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.newProxy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.isSuceess = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.isCon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.textBox_validTime = new System.Windows.Forms.TextBox();
            this.textBox_startPort = new System.Windows.Forms.TextBox();
            this.textBox_whiteList = new System.Windows.Forms.TextBox();
            this.textBox_extractTheInterval = new System.Windows.Forms.TextBox();
            this.textBox_bindIp = new System.Windows.Forms.TextBox();
            this.textBox_writePath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_log = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer_check = new System.Windows.Forms.Timer(this.components);
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_view_main
            // 
            this.list_view_main.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.startPort,
            this.oldProxy,
            this.newProxy,
            this.isSuceess,
            this.isCon});
            this.list_view_main.FullRowSelect = true;
            this.list_view_main.GridLines = true;
            this.list_view_main.HideSelection = false;
            this.list_view_main.Location = new System.Drawing.Point(3, 40);
            this.list_view_main.MultiSelect = false;
            this.list_view_main.Name = "list_view_main";
            this.list_view_main.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.list_view_main.Size = new System.Drawing.Size(671, 260);
            this.list_view_main.TabIndex = 0;
            this.list_view_main.UseCompatibleStateImageBehavior = false;
            this.list_view_main.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            // 
            // startPort
            // 
            this.startPort.Text = "启动端口";
            // 
            // oldProxy
            // 
            this.oldProxy.Text = "二级代理";
            this.oldProxy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.oldProxy.Width = 150;
            // 
            // newProxy
            // 
            this.newProxy.Text = "中转后代理";
            this.newProxy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.newProxy.Width = 150;
            // 
            // isSuceess
            // 
            this.isSuceess.Text = "是否中转成功";
            this.isSuceess.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.isSuceess.Width = 110;
            // 
            // isCon
            // 
            this.isCon.Text = "代理是否可用";
            this.isCon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.isCon.Width = 110;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.textBox_validTime);
            this.groupBox.Controls.Add(this.textBox_startPort);
            this.groupBox.Controls.Add(this.textBox_whiteList);
            this.groupBox.Controls.Add(this.textBox_extractTheInterval);
            this.groupBox.Controls.Add(this.textBox_bindIp);
            this.groupBox.Controls.Add(this.textBox_writePath);
            this.groupBox.Controls.Add(this.label7);
            this.groupBox.Controls.Add(this.label6);
            this.groupBox.Controls.Add(this.label5);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.textBox_url);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new System.Drawing.Point(5, 307);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(355, 223);
            this.groupBox.TabIndex = 1;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "选项配置";
            // 
            // textBox_validTime
            // 
            this.textBox_validTime.Location = new System.Drawing.Point(88, 186);
            this.textBox_validTime.Name = "textBox_validTime";
            this.textBox_validTime.Size = new System.Drawing.Size(261, 21);
            this.textBox_validTime.TabIndex = 22;
            this.textBox_validTime.Tag = "ValidTime";
            this.textBox_validTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_validTime_KeyPress);
            // 
            // textBox_startPort
            // 
            this.textBox_startPort.Location = new System.Drawing.Point(88, 159);
            this.textBox_startPort.Name = "textBox_startPort";
            this.textBox_startPort.Size = new System.Drawing.Size(261, 21);
            this.textBox_startPort.TabIndex = 21;
            this.textBox_startPort.Tag = "StartPort";
            this.textBox_startPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_startPort_KeyPress);
            // 
            // textBox_whiteList
            // 
            this.textBox_whiteList.Location = new System.Drawing.Point(88, 131);
            this.textBox_whiteList.Name = "textBox_whiteList";
            this.textBox_whiteList.Size = new System.Drawing.Size(261, 21);
            this.textBox_whiteList.TabIndex = 20;
            this.textBox_whiteList.Tag = "WhiteList";
            // 
            // textBox_extractTheInterval
            // 
            this.textBox_extractTheInterval.Location = new System.Drawing.Point(88, 104);
            this.textBox_extractTheInterval.Name = "textBox_extractTheInterval";
            this.textBox_extractTheInterval.Size = new System.Drawing.Size(261, 21);
            this.textBox_extractTheInterval.TabIndex = 19;
            this.textBox_extractTheInterval.Tag = "ExtractTheInterval";
            this.textBox_extractTheInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_extractTheInterval_KeyPress);
            // 
            // textBox_bindIp
            // 
            this.textBox_bindIp.Location = new System.Drawing.Point(88, 77);
            this.textBox_bindIp.Name = "textBox_bindIp";
            this.textBox_bindIp.Size = new System.Drawing.Size(261, 21);
            this.textBox_bindIp.TabIndex = 18;
            this.textBox_bindIp.Tag = "BindIp";
            // 
            // textBox_writePath
            // 
            this.textBox_writePath.Location = new System.Drawing.Point(88, 50);
            this.textBox_writePath.Name = "textBox_writePath";
            this.textBox_writePath.Size = new System.Drawing.Size(261, 21);
            this.textBox_writePath.TabIndex = 17;
            this.textBox_writePath.Tag = "WritePath";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "有效时间:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "端口开始:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "白名单:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "提取间隔:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "绑定IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "写出路径:";
            // 
            // textBox_url
            // 
            this.textBox_url.Location = new System.Drawing.Point(88, 23);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(261, 21);
            this.textBox_url.TabIndex = 10;
            this.textBox_url.Tag = "Url";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "提取URL:";
            // 
            // textBox_log
            // 
            this.textBox_log.Location = new System.Drawing.Point(366, 314);
            this.textBox_log.Multiline = true;
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.ReadOnly = true;
            this.textBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_log.Size = new System.Drawing.Size(301, 215);
            this.textBox_log.TabIndex = 2;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timer_check
            // 
            this.timer_check.Enabled = true;
            this.timer_check.Interval = 1000;
            this.timer_check.Tick += new System.EventHandler(this.timer_check_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 535);
            this.Controls.Add(this.textBox_log);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.list_view_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "端口转发系统";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader startPort;
        private System.Windows.Forms.ColumnHeader oldProxy;
        private System.Windows.Forms.ColumnHeader newProxy;
        private System.Windows.Forms.ColumnHeader isSuceess;
        private System.Windows.Forms.ColumnHeader isCon;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_validTime;
        private System.Windows.Forms.TextBox textBox_startPort;
        private System.Windows.Forms.TextBox textBox_whiteList;
        private System.Windows.Forms.TextBox textBox_extractTheInterval;
        private System.Windows.Forms.TextBox textBox_bindIp;
        private System.Windows.Forms.TextBox textBox_writePath;
        public System.Windows.Forms.TextBox textBox_log;
        private System.Windows.Forms.Timer timer;
        public System.Windows.Forms.ListView list_view_main;
        private Timer timer_check;
    }
}

