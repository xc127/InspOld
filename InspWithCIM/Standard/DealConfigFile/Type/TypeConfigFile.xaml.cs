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
using System.Xml;
using Common;

namespace DealConfigFile
{
    /// <summary>
    /// TypeLocation.xaml 的交互逻辑
    /// </summary>
    public partial class TypeConfigFile : BaseControl
    {
        #region 定义

        #region 定义事件

        #endregion 定义事件
        #endregion 定义

        #region 初始化
        public TypeConfigFile()
        {
            InitializeComponent();
        }
        #endregion 初始化

        #region 选择类型
        private void tvType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                XmlElement xmlElement = (XmlElement)this.tvType.SelectedItem;
                XmlElement xmlParent = (XmlElement)xmlElement.ParentNode;
                string strParent = xmlParent.Attributes["Name"].Value;
                string strChild = xmlElement.Attributes["Name"].Value;
                Select(strParent, strChild);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("TypeConfigFile", ex);
            }
        }

        void Select(string strParent, string strChild)
        {
            try
            {
                if (strChild == "配置文件")
                {
                    ComTypeAlgorithm.C_I.TypeChild = "";
                }
                else
                {
                    ComTypeAlgorithm.C_I.TypeChild = strParent + ":" + strChild;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("TypeConfigFile", ex);
            }
        }
        #endregion 选择类型

    }
}
