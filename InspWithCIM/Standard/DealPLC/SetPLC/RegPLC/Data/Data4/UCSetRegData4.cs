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
    public class UCSetRegData4 : BaseUCReg
    {
        #region 初始化
        public override void Init()
        {
            base.Init(RegData4.R_I, "数据寄存器4");
            dudNumReg.Value = RegData4.R_I.NumReg;
        }
        #endregion 初始化


        /// <summary>
        /// 初始创建寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strReg = txtRegStart.Text.Trim().ToUpper();//将字母全部转换为大写
                bool blMatch = Regex.IsMatch(strReg, g_strRegex);
                if (!blMatch)
                {
                    g_RegPLCBase.regStart = "";
                    MessageBox.Show("寄存器起始地址不正确！");
                    return;
                }
                g_RegPLCBase.Init();
                g_RegPLCBase.regStart = strReg;

                CreateRegTwo();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("PLCSetReg", ex);
                MessageBox.Show("配置出错！");
            }
            finally
            {
                //刷新数据显示
                RefreshDG();
            }
        }
        #region 显示

        #endregion 显示
    }
}
