using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace ConsoleApp3
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string keys;
            var path = new OpenFileDialog()
            {
                Filter = "文本文件|*.txt",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            Func<string> func = () =>
            {
                if (path.ShowDialog() == DialogResult.OK)
                {
                    return path.FileName;
                }
                else
                {
                    return "\a";
                }
            };
            try
            {
                keys = File.ReadAllText(func.Invoke());
            }
            catch
            {
                keys = string.Empty;
                Console.WriteLine("错误选择，请重新打开！");
                Thread.Sleep(5000);
                return;
            }
            Thread.Sleep(5000);
            for (int i = 2; i < keys.Length; i++)
            {
                try
                {
                    if (keys[i] == ' ')
                    {
                        Thread.Sleep((Convert.ToInt32(keys[1]) - 48) * 10);
                    }
                    else if (keys[i] == '(')
                    {
                        i++;
                        for (;keys[i] != ')';i++)
                        {
                            SendKeys.SendWait(keys[i].ToString());
                        }
                        Thread.Sleep((Convert.ToInt32(keys[0]) - 48) * 100);
                        continue;
                    }
                    else if(keys[i] == ')')
                    {
                        continue;
                    }
                    else
                    {
                        Key.Press(keys[i].ToString(), (Convert.ToInt32(keys[0]) - 48) * 100);
                    }
                }
                catch
                {
                    continue;
                }
                
            }
        }
    }
    class Key
    {
        public static void Press(string keys,int time)
        {
            foreach (var item in keys)
            {
                SendKeys.SendWait(item.ToString());
            }
            Thread.Sleep(time);
        }
    }
}
