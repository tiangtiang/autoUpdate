using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using FluentFTP;
namespace UI
{
    /// <summary>
    /// 从服务器上更新文件
    /// </summary>
    /// <author>tiang</author>
    /// <date>2017/5/11</date>
    public class UpdateFile
    {
        [Obsolete]
        private string url;     //服务器地址(已废弃）
        private string host;        //服务器地址
        private string username;    //用户名
        private string password;    //密码
        private FtpClient client;
        /// <summary>
        /// 初始化成员变量
        /// </summary>
        public UpdateFile()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["FtpConnection"];
            //解密连接字符串
            string str = Decrypt(settings.ConnectionString, "jwbmis12", "12345678");
            string[] strs = str.Split(';');
            if (strs.Length == 3)
            {
                host = strs[0];
                username = strs[1];
                password = strs[2];
            }
            client = new FtpClient();
            client.Host = host;
            client.Encoding = Encoding.GetEncoding(936);
            client.Credentials = new NetworkCredential(username, password);
            
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="encryptedString">加密后的字符串</param>
        /// <param name="key">密码</param>
        /// <param name="iv">偏移量</param>
        /// <returns>解密后的结果</returns>
        private static string Decrypt(string encryptedString, string key, string iv)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(key);

            byte[] btIV = Encoding.UTF8.GetBytes(iv);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);

                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch
                {
                    return encryptedString;
                }
            }
        }
        /// <summary>
        /// 下载文件到本地
        /// </summary>
        /// <param name="remotePath">服务器上相对位置</param>
        /// <param name="localPath">本地位置</param>
        /// 原来的下载文件方法，现在已被废弃，url变量的值也变成了host
        [Obsolete]
        public bool downLoadPast(string remotePath, string localPath)
        {
            FtpWebRequest ftp;
            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }
            //获取路径中的目录信息
            String dir = Path.GetDirectoryName(localPath);
            if (!Directory.Exists(dir))     //如果目录不存在，则创建新的目录
            {
                Directory.CreateDirectory(dir);
            }
            FileStream outstream = new FileStream(localPath, FileMode.OpenOrCreate);
            try
            {
                string[] paths = remotePath.Split('\\');
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < paths.Count(); i++)
                {
                    sb.Append(System.Web.HttpUtility.UrlEncode(paths[i], Encoding.GetEncoding("GBK")));
                    sb.Append('/');
                }
                sb = sb.Remove(sb.Length - 1, 1);
                remotePath = sb.ToString();
                //remotePath = System.Web.HttpUtility.UrlEncode(remotePath, Encoding.GetEncoding("GBK"));
                String str = url + remotePath;
                
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(str));
                ftp.Method = WebRequestMethods.Ftp.DownloadFile;
                ftp.UseBinary = true;
                ftp.Credentials = new NetworkCredential(username, password);
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();

                Stream inStream = response.GetResponseStream();
                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];
                int readCount;
                while ((readCount = inStream.Read(buffer, 0, bufferSize)) > 0)
                {
                    outstream.Write(buffer, 0, readCount);
                }
                inStream.Close();
                outstream.Close();
                response.Close();
                return true;
            }
            catch (Exception)
            {
                outstream.Close();
                File.Delete(localPath);
                Console.WriteLine(url + remotePath);
                return false;
            }
        }
        /// <summary>
        /// 下载文件到本地
        /// </summary>
        /// <param name="remotePath">服务器上文件的位置</param>
        /// <param name="localPath">本地保存位置</param>
        /// <returns>是否下载成功</returns>
        public bool downLoad(string remotePath, string localPath)
        {
            string remoteFolder = "release/";
            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }
            //获取路径中的目录信息
            String dir = Path.GetDirectoryName(localPath);
            if (!Directory.Exists(dir))     //如果目录不存在，则创建新的目录
            {
                Directory.CreateDirectory(dir);
            }
            FileStream outstream = new FileStream(localPath, FileMode.OpenOrCreate);
            try
            {
                if(!client.IsConnected)
                    client.Connect();   
                client.ReadTimeout = 30000;
                client.DownloadFile(outstream, remoteFolder + remotePath);
                outstream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                outstream.Close();
                File.Delete(localPath);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void closeConnect()
        {
            client.Disconnect();
        }
    }
        
}
