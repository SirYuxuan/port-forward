using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PortForward.Util
{
    /// <summary>
    /// 读写ini配置文件工具类
    /// </summary>
    class IniUtil
    {
        //声明写INI文件的API函数
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        //声明读INI文件的API函数
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private static readonly string path = Environment.CurrentDirectory + "/Config/Config.ini";

        //写INI文件
        public static void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        //读取INI文件
        public static string ReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            _ = GetPrivateProfileString(Section, Key, "", temp, 255, path);
            return temp.ToString();
        }

    }
}
