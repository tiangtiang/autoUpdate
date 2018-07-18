using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace UI
{
    /// <summary>
    /// 生成配置文件
    /// </summary>
    /// <author>tiang</author>
    /// <date>2017/5/12</date>
    public class GenerateVersion
    {
        /// <summary>
        /// 系统路径
        /// </summary>
        private string systemPath = System.Windows.Forms.Application.StartupPath;
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private string configPath;
        /// <summary>
        /// 需要更新的文件列表
        /// </summary>
        private Dictionary<string, VersionNode> updateList = new Dictionary<string,VersionNode>();
        /// <summary>
        /// 忽略文件列表
        /// </summary>
        private List<String> ignoreList = new List<string>();
        /// <summary>
        /// 初始化配置文件路径
        /// </summary>
        public GenerateVersion()
        {
            configPath = systemPath + Path.DirectorySeparatorChar + "version.xml";
        }
        /// <summary>
        /// 重新生成配置文件
        /// </summary>
        public void generateFile()
        {
            if (File.Exists(configPath))
            {
                readFile();
            }
            writeFile();
        }
        /// <summary>
        /// 更新配置文件
        /// </summary>
        public void writeFile()
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("version");
            XElement update = new XElement("UpdateFiles");
            XElement ignore = new XElement("IgnoreFiles");
            string[] files = Directory.GetFiles(systemPath);
            foreach (var file in files)
            {
                XElement element = new XElement("file");
                string shortName = Path.GetFileName(file);
                element.SetAttributeValue("name", shortName);
                if (ignoreList.Contains(shortName))
                {
                    ignore.Add(element);
                }
                else
                {
                    FileInfo info = new FileInfo(file);
                    DateTime modifyTime = info.LastWriteTime;
                    element.SetAttributeValue("modifyTime", info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    if(!updateList.ContainsKey(shortName))
                    {
                        element.Value = "1.0";
                    }
                    else
                    {
                        VersionNode node = updateList[shortName];
                        //int result = DateTime.Compare(modifyTime, node.modifyTime);
                        int result = compareTime(modifyTime, node.modifyTime);
                        if (result > 0)
                        {
                            element.Value = addVersion(node.version);
                        }
                        else
                        {
                            element.Value = node.version;
                        }
                    }
                    update.Add(element);
                }
            }
            root.Add(update);
            root.Add(ignore);
            doc.Add(root);
            doc.Save(configPath);
        }
        public int compareTime(DateTime time1, DateTime time2)
        {
            int result = 1;
            if (time1.ToShortDateString() == time2.ToShortDateString())
            {
                if (time1.ToShortTimeString() == time2.ToShortTimeString())
                {
                    return 0;
                }
            }
            return result;
        }
        /// <summary>
        /// 在原来版本的基础上+1
        /// </summary>
        /// <param name="ver">原来的版本号</param>
        /// <returns>增加后的版本号</returns>
        public string addVersion(string ver)
        {
            string[] temp = ver.Split('.');
            int lastVer = Convert.ToInt32(temp[temp.Length - 1])+1;
            string result = temp[0];
            for (int i = 1; i < temp.Length - 1; i++)
            {
                result += "."+temp[i];
            }
            result += "." + lastVer;
            return result;
        }
        /// <summary>
        /// 从原有的配置文件中读取数据
        /// </summary>
        public void readFile()
        {
            XDocument doc = XDocument.Load(configPath);

            var itemList = from item in doc.Descendants("UpdateFiles").Elements("file")
                           select new
                           {
                               fileName = item.Attribute("name").Value,
                               modifyTime = item.Attribute("modifyTime").Value,
                               ver = item.Value
                           };
            foreach (var item in itemList)
            {
                string ver = Convert.ToString(item.ver);
                string fileName = Convert.ToString(item.fileName);
                //DateTime modifyTime = DateTime.ParseExact(Convert.ToString(item.modifyTime), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                DateTime modifyTime = Convert.ToDateTime(Convert.ToString(item.modifyTime));
                updateList.Add(fileName, new VersionNode()
                {
                    version = ver,
                    name = fileName,
                    modifyTime = modifyTime
                });
            }

            var ignores = from item in doc.Descendants("IgnoreFiles").Elements("file")
                             select new
                             {
                                 name = item.Attribute("name").Value
                             };

            foreach (var item in ignores)
            {
                ignoreList.Add(Convert.ToString(item.name));
            }
        }
        /// <summary>
        /// 从指定配置文件中读取更新文件列表
        /// </summary>
        /// <param name="path">配置文件路径</param>
        /// <returns>更新文件列表</returns>
        public Dictionary<string, VersionNode> readUpdateFiles(string path)
        {
            Dictionary<string, VersionNode> dic = new Dictionary<string, VersionNode>();
            if (!File.Exists(path))
                return null;
            XDocument doc = XDocument.Load(path);

            var itemList = from item in doc.Descendants("UpdateFiles").Elements("file")
                           select new
                           {
                               fileName = item.Attribute("name").Value,
                               modifyTime = item.Attribute("modifyTime").Value,
                               ver = item.Value
                           };
            foreach (var item in itemList)
            {
                string ver = Convert.ToString(item.ver);
                string fileName = Convert.ToString(item.fileName);
                //DateTime modifyTime = DateTime.ParseExact(Convert.ToString(item.modifyTime), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                DateTime modifyTime = Convert.ToDateTime(Convert.ToString(item.modifyTime));
                dic.Add(fileName, new VersionNode()
                {
                    version = ver,
                    name = fileName,
                    modifyTime = modifyTime
                });
            }
            return dic;
        }
    }
    /// <summary>
    /// 文件版本节点类，包括文件名，文件最后修改时间和文件版本
    /// </summary>
    public class VersionNode
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string name;
        /// <summary>
        /// 版本号
        /// </summary>
        public string version;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime modifyTime;
    }
}
