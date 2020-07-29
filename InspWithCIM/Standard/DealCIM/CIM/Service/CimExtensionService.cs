using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DealCIM
{
    class CimExtensionService
    {
        #region singleton
        static object _locker = new object();
        static CimExtensionService _instance = null;

        public static CimExtensionService GetInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                        _instance = new CimExtensionService();
                }
            }
            return _instance;
        }

        private CimExtensionService()
        {
            Load();
        }
        #endregion

        const string Path = @"D:\Store\Custom\Cim\" + @"extension.xml";

        private CommonService.Serializer _serializer = new CommonService.Serializer_Xml();

        private ObservableCollection<string> _contents = new ObservableCollection<string>();

        public ObservableCollection<string> GetContents() => _contents;

        public void Save()
        {
            _serializer.Serialize<ObservableCollection<string>>(_contents, Path);
        }

        void Load()
        {
            _contents = _serializer.DeSerialize<ObservableCollection<string>>(Path);
            if (_contents == null) _contents = new ObservableCollection<string>();
        }
    }
}
