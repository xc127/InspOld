using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BasicClass;
using DealConfigFile;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace DealConfigFile
{
    /// <summary>
    /// UCParProduct.xaml 的交互逻辑
    /// </summary>
    public partial class UCParProduct : BaseControl
    {        
        #region 初始化
        public UCParProduct()
        {
            InitializeComponent();
        }
        
        public void Init()
        {
            ShowModelName_Invoke();
        }
        #endregion 初始化

        #region 显示
        #region 产品参数
        public void ShowModelName_Invoke()
        {
            ShowLabel_Invoke(lblModelName, ComConfigPar.C_I.NameModel);
        }
    
        #endregion 产品参数
        #endregion 显示
    }
}
