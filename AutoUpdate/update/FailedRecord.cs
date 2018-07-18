using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AutoUpdate
{
    /// <summary>
    /// 下载失败的文件记录和读取
    /// </summary>
    /// <author>tiang</author>
    /// <date>20171114</date>
    class FailedRecord
    {
        private const string FilePath = "temp";
        private const string FileName = "failed";
        public List<String> failedList = new List<string>();
        /// <summary>
        /// 判断是否有下载失败的文件
        /// </summary>
        /// <returns></returns>
        public bool hasFailedContent()
        {
            if (!Directory.Exists(FilePath))
                return false;
            if (!File.Exists(FilePath + Path.DirectorySeparatorChar + FileName))
                return false;
            using (StreamReader reader = new StreamReader(FilePath + Path.DirectorySeparatorChar + FileName))
            {
                string line = reader.ReadLine();
                while (line != null && line != "")
                {
                    failedList.Add(line);
                    line = reader.ReadLine();
                }
                reader.Close();
            }
            return failedList.Count != 0;
        }
        /// <summary>
        /// 将下载失败的文件列表写入失败文件中
        /// </summary>
        /// <param name="list"></param>
        public void writeToFailed(List<String> list)
        {
            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);
            using (FileStream outstream = new FileStream(FilePath+Path.DirectorySeparatorChar+FileName, FileMode.OpenOrCreate))
            {
                StreamWriter writer = new StreamWriter(outstream);
                foreach (var str in list)
                {
                    writer.WriteLine(str);
                }
                writer.Close();
            }
        }
    }
}
