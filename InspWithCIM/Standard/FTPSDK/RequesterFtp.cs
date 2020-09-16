using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

namespace FTPSDK
{
    public class RequesterFtp
    {
        private readonly FtpConnectionModel connectionData;
        private FtpWebRequest ftp = null;

        #region constructor
        public RequesterFtp(string host, string username, string password,
            int port, IWebProxy proxy, bool enableSsl, bool useBinary,
            bool usePassive, int timeout)
        {
            connectionData = new FtpConnectionModel
            {
                Host = host.ToLower().StartsWith("ftp://") ? host : "ftp://" + host,
                Username = username,
                Password = password,
                Port = port,
                Proxy = proxy,
                EnableSsl = enableSsl,
                UserBinary = useBinary,
                UsePassive = usePassive,
                Timeout = timeout
            };
        }

        public RequesterFtp(string host = "", string username = "", string password = "")
            : this(host, username, password, 21, null, false, true, true, 300000)
        {
        }

        /// <summary>
        /// 传入整个data，其中host应当为ftp://ip/的格式，如果是ftp://dirName/格式，会异常
        /// </summary>
        /// <param name="ftpConnectionModel"></param>
        public RequesterFtp(FtpConnectionModel ftpConnectionModel)
        {
            connectionData = ftpConnectionModel.Clone() as FtpConnectionModel;
            string host = connectionData.Host;
            connectionData.Host = host.ToLower().StartsWith("ftp://") ? host : "ftp://" + host;
        }
        #endregion

        void ConnectUrl(string url)
        {
            try
            {
                ftp = WebRequest.Create(new Uri(url)) as FtpWebRequest;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;
                ftp.UseBinary = true;
                ftp.UsePassive = false;
                ftp.Credentials = new NetworkCredential(connectionData.Username, connectionData.Password);
            }
            catch(Exception ex) { }
        }

        /// <summary>
        /// 上传文件到ftp服务器的指定目录下
        /// </summary>
        /// <param name="localFile">本地文件路径</param>
        /// <param name="remoteFilePath">远程存储路径，需要dir（不含ftp://ip/）+filename</param>
        /// <returns></returns>
        public bool Upload(FileInfo localFile, string remoteFilePath)
        {
            //加载本地图片
            if (!localFile.Exists)
                return false;

            //创建远程目录
            string dir = Path.GetDirectoryName(remoteFilePath);
            if (!string.IsNullOrEmpty(dir))
                CreateRemoteDir(dir);

            try
            {
                //做ftp连接
                string url = connectionData.Host + '/' + remoteFilePath;
                ConnectUrl(url);

                //进行文件的读取和远程写入
                using (Stream s = ftp.GetRequestStream())
                using (FileStream fs = localFile.OpenRead())
                {
                    byte[] buffer = new byte[4096];
                    int cnt;
                    do
                    {
                        cnt = fs.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, cnt);
                    }
                    while (!(cnt < buffer.Length));
                    fs.Close();
                }
            }
            catch (Exception ex) { }        
            return true;
        }

        public bool Download(string fileName, string localName)
        {
            try
            {
                using (FileStream fs = new FileStream(localName, FileMode.OpenOrCreate))
                {
                    string url = UrlCombine(fileName);
                    ConnectUrl(url);

                    ftp.ContentOffset = fs.Length;
                    using (FtpWebResponse response = ftp.GetResponse() as FtpWebResponse)
                    {
                        fs.Position = fs.Length;
                        byte[] buffer = new byte[4096];
                        int cnt = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                        while (cnt > 0)
                        {
                            fs.Write(buffer, 0, cnt);
                            cnt = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                        }
                        response.GetResponseStream().Close();
                    }
                }
            }
            catch { return false; }
            return true;
        }

        string UrlCombine(string fileName) => new Uri(new Uri(new Uri(
            connectionData.Host.TrimEnd('/')),
            connectionData.RemotePath),
            fileName).ToString();

        void CreateRemoteDir(string dir)
        {
            string uri = connectionData.Host + '/' + dir;
            if (!IsRemotoDirExists(uri))
                return;
            ConnectUrl(uri);
            ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
            ftp.KeepAlive = false;
            //?
            FtpWebResponse response = ftp.GetResponse() as FtpWebResponse;
            response.Close();
        }

        bool IsRemotoDirExists(string url)
        {
            ConnectUrl(url);
            
            try
            {
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse response = ftp.GetResponse() as FtpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string line = reader.ReadLine();

                reader.Close();
                response.Close();
                return line != null;
            }
            catch(Exception ex)
            {
                return false;
            }

            //网上看到，未经测试
            //try
            //{
            //    ftp.Method = WebRequestMethods.Ftp.GetFileSize;
            //    FtpWebResponse response = ftp.GetResponse() as FtpWebResponse;
            //}
            //catch(WebException ex)
            //{
            //    FtpWebResponse response = (FtpWebResponse)ex.Response;
            //    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
            //        return false;
            //}
        }
    }
}
