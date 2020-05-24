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
using System.Text.RegularExpressions;
using DealFile;
using Common;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using BasicClass;

namespace DealPLC
{
    public class UCSetRegCameraData : BaseUCReg
    {
        #region 定义
        public override event Action SaveReg_event;//触发保存事件
        #endregion 定义

        #region 初始化
        public override void Init()
        {
            try
            {
                base.dudNumReg.ToolTip = "";
                base.dudNumReg.IsEnabled = false;

                base.Init(RegCameraData.R_I, "数据寄存器");
            }
            catch (Exception ex)
            {
                
            }            
        }
        #endregion 初始化        

        #region 触发保存事件
        protected override void SaveEvent()
        {
            try
            {
                SaveReg_event();
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 触发保存事件

        #region 显示

        #endregion 显示
    }
}
