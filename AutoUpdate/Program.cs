using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace AutoUpdate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;
            FailedRecord failed = new FailedRecord();
            UI.Progress bar = new UI.Progress();
            bar.SuccessOrNot += new UI.Progress.UpdateSuccess(flag =>
            {
                if (flag)
                {
                    copyAndPaste();
                }
                else
                {
                    failed.writeToFailed(bar.FailedList);
                }
            });
            if(!failed.hasFailedContent())
                bar.setUpdateProgram();
            else
            {
                UI.UpdateProgram up = new UI.UpdateProgram();
                up.UpdateList = failed.failedList;
                bar.setUpdateProgram(up);
            }
            int nu = bar.needUpdate();
            if (nu == 1)    //需要更新文件
            {
                bar.downloadFiles();
                Application.Run(bar);
            }
            else
            {
                if (nu == -1)
                {
                    UI.PromptForm promt = new UI.PromptForm(UI.PromptState.Fail, "无法连接至服务器！将启动未更新前的系统");
                    promt.ShowDialog();
                }
                Application.Exit();
            }
            //UI.UpdateFile up = new UI.UpdateFile();
            //up.list();
            //up.download();
        }
        #region 移动替换文件
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        private static string tempPath = "temp";
        private static string programpath = "program";

        /// <summary>
        /// 查看文件是否被占用
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsFileOccupied(string filePath)
        {
            IntPtr vHandle = _lopen(filePath, OF_READWRITE | OF_SHARE_DENY_NONE);
            CloseHandle(vHandle);
            return vHandle == HFILE_ERROR ? true : false;
        }
        /// <summary>
        /// 复制并替换更新文件
        /// </summary>
        static void copyAndPaste()
        {
            copyAndPaste(tempPath, programpath);
        }

        private static void copyAndPaste(string directory, string aimPath)
        {
            //获取缓冲区的文件
            string[] files = Directory.GetFiles(directory);
            moveFile(files, aimPath);
            //获取所有的子目录
            string[] dirs = Directory.GetDirectories(directory);
            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo info = new DirectoryInfo(dirs[i]);
                //移动子目录的文件
                copyAndPaste(directory + Path.DirectorySeparatorChar + info.Name, aimPath + Path.DirectorySeparatorChar + info.Name);
            }
        }
        /// <summary>
        /// 忽略移动的文件
        /// </summary>
        private static List<string> ignoreList = new List<string>() { "temp\\failed" };
        /// <summary>
        /// 将子目录下的文件移动到目录下
        /// </summary>
        /// <param name="dirinfo"></param>
        private static void moveFile(string[] files, string aimPath)
        {
            if (!Directory.Exists(aimPath))
            {
                Directory.CreateDirectory(aimPath);
            }
            
            if (files.Length <= 0)
            {
                return;
            }

            while (IsFileOccupied(files[0]))
            {
                Thread.Sleep(1000);
            }
            try
            {
                foreach (var file in files)
                {
                    if (ignoreList.Contains(file))
                    {
                        File.Delete(file);
                        continue;
                    }
                    if (File.Exists(aimPath + Path.DirectorySeparatorChar + Path.GetFileName(file)))
                    {
                        File.Delete(aimPath + Path.DirectorySeparatorChar + Path.GetFileName(file));
                    }
                    File.Move(file, aimPath + Path.DirectorySeparatorChar + Path.GetFileName(file));
                }
            }
            catch (Exception)
            {
                copyAndPaste();
            }
        }
        
        #endregion
        /// <summary>
        /// 当程序结束时触发,Application.Run()函数结束之后即触发，所有run函数之后的任何操作都会在此操作之后进行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            //copyAndPaste();
            //throw new NotImplementedException();
            //试图关闭所有的线程，但是并没有什么卵用
            Application.ExitThread();
            //启动后台进程，在程序结束之后替换文件
            Process temp = new Process();
            if(!File.Exists(@"program\UI.exe")){
                return;
            }
            temp.StartInfo.FileName = @"program\UI.exe";
            //temp.StartInfo.CreateNoWindow = false;
            //temp.StartInfo.UseShellExecute = false; 
            temp.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            temp.StartInfo.Verb = "runas";
            try
            {
                temp.Start();
            }
            catch (Exception) { }
        }
    }
}
