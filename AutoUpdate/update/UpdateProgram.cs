using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace UI
{
    /// <summary>
    /// 从服务器更新程序
    /// </summary>
    /// <author>tiang</author>
    /// <date>2017/5/13</date>
    public class UpdateProgram
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        private UpdateFile update;
        /// <summary>
        /// 生成文件
        /// </summary>
        private GenerateVersion generate;
        /// <summary>
        /// 临时文件路径
        /// </summary>
        private string tempPath;
        /// <summary>
        /// 本地版本信息
        /// </summary>
        private Dictionary<string, VersionNode> local;
        /// <summary>
        /// 服务器版本信息
        /// </summary>
        private Dictionary<string, VersionNode> remote;
        
        private List<string> updateList = new List<string>();
        /// <summary>
        /// 需要更新的文件列表
        /// </summary>
        public List<string> UpdateList
        {
            get { return updateList; }
            set { updateList = value; }
        }
        private bool netIsOk = true;
        /// <summary>
        /// 初始化成员变量
        /// </summary>
        public UpdateProgram()
        {
            update = new UpdateFile();
        }
        /// <summary>
        /// 与远程服务器版本进行比较，并得到需要更新的文件列表
        /// </summary>
        public void init()
        {
            generate = new GenerateVersion();
            tempPath = System.Windows.Forms.Application.StartupPath + Path.DirectorySeparatorChar + "temp";
            netIsOk = downloadVersionFile();
            local = generate.readUpdateFiles("program/version.xml");
            remote = generate.readUpdateFiles(tempPath + Path.DirectorySeparatorChar + "version.xml");
            getUpdateList();
        }
        /// <summary>
        /// 下载服务器的版本文件
        /// </summary>
        private bool downloadVersionFile()
        {
            //是否存在temp文件夹
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            //下载服务器端的版本文件到本地
            return update.downLoad("version.xml", tempPath +Path.DirectorySeparatorChar+ "version.xml");
        }
        /// <summary>
        /// 获取需要更新的文件列表
        /// </summary>
        private void getUpdateList()
        {
            if (remote == null)
                return;
            //遍历远程需要更新的文件列表，与本地想比较，如果需要更新，则加入更新列表
            foreach (var remoteItem in remote.Keys)
            {
                if (local == null || !local.ContainsKey(remoteItem))
                {
                    updateList.Add(remoteItem);
                    
                }
                else
                {
                    string localVersion = local[remoteItem].version;
                    string remoteVersion = remote[remoteItem].version;
                    if (compareVersion(localVersion, remoteVersion))
                        updateList.Add(remoteItem);
                }
            }
        }
        /// <summary>
        /// 判断是否需要更新
        /// </summary>
        /// <returns></returns>
        public int needUpdate()
        {
            if (netIsOk)
                if (updateList.Count == 0)
                {       //可以联网但是不需要更新
                    return 0;
                }
                else
                {       //可以联网而且需要更新
                    return 1;
                }
            else
                return -1;      //无法连接至网络
        }
        /// <summary>
        /// 比较版本
        /// </summary>
        /// <param name="lv">本地版本</param>
        /// <param name="rv">服务器版本</param>
        /// <returns>是否需要更新</returns>
        private bool compareVersion(String lv, string rv)
        {
            string[] lvs = lv.Split('.');
            string[] rvs = rv.Split('.');
            int lastnum1 = Convert.ToInt32(lvs[lvs.Length-1]);
            int lastnum2 = Convert.ToInt32(rvs[rvs.Length - 1]);
            if (lastnum2 > lastnum1)        //服务器版本号高，应该更新
                return true;
            else
                return false;
        }
        /// <summary>
        /// 下载需要更新的所有文件
        /// </summary>
        [Obsolete]
        public void downloadFiles()
        {
            foreach (string name in updateList)
            {
                update.downLoad(name, tempPath + Path.DirectorySeparatorChar + name);
            }
        }
        /// <summary>
        /// 下载指定文件到本地
        /// </summary>
        /// <param name="name">文件名</param>
        public bool downloadFile(string name)
        {
            return update.downLoad(name, tempPath + Path.DirectorySeparatorChar + name);
        }
        /// <summary>
        /// 将缓冲区中的文件替换到原来的执行文件中
        /// </summary>
        [Obsolete]
        private void moveAndCover()
        {
            //获取缓冲区的文件
            string[] files = Directory.GetFiles(tempPath);
            foreach (var file in files)
            {
                if (File.Exists(Path.GetFileName(file)))
                {
                    File.Delete(Path.GetFileName(file));
                }
                File.Move(file, Path.GetFileName(file));
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void closeConnect()
        {
            update.closeConnect();
        }
    }
}
