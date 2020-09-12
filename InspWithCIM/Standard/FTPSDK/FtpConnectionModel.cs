using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FTPSDK
{
    [Serializable]
    public class FtpConnectionModel:ICloneable
    {
        private string _host = string.Empty;
        public string Host
        {
            get => _host ?? string.Empty;
            set => _host = value;
        }

        private string _username = string.Empty;
        public string Username
        {
            get => _username ?? string.Empty;
            set => _username = value;
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password ?? string.Empty;
            set => _password = value;
        }

        IWebProxy _proxy = null;
        public IWebProxy Proxy
        {
            get => _proxy;
            set => _proxy = value;
        }

        private int _port = 21;
        public int Port
        {
            get => _port;
            set => _port = value;
        }

        private bool _enableSsl = false;
        public bool EnableSsl
        {
            get => _enableSsl;
            set => _enableSsl = value;
        }

        private bool _usePassive = true;
        public bool UsePassive
        {
            get => _usePassive;
            set => _usePassive = value;
        }

        private bool _useBinary = true;
        public bool UserBinary
        {
            get => _useBinary;
            set => _useBinary = value;
        }

        private string _remotePath = string.Empty;
        public string RemotePath
        {
            get=> _remotePath;
            
            set
            {
                //string result = rootPath;
                //if (!string.IsNullOrEmpty(value) && value != rootPath)
                //{
                //    result = Path.Combine(Path.Combine(rootPath, value.TrimStart('/').TrimEnd('/')), "/"); // 进行路径的拼接
                //}
                //this.remotePath = result;
                _remotePath = value;
            }
        }

        private int _timeout = 300000;
        public int Timeout
        {
            get => _timeout;
            set => _timeout = value;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
