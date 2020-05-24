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
using System.Windows.Shapes;
using BasicClass;
using SetPar;
using System.Threading;
using System.Threading.Tasks;
using BasicComprehensive;
using DealConfigFile;
using DealPLC;
using DealLog;

namespace Main
{
    /// <summary>
    /// WinTrrigerComprehensive.xaml 的交互逻辑
    /// </summary>
    public partial class WinTrrigerComprehensive : Window
    {
        #region 窗体单实例
        private static WinTrrigerComprehensive g_WinTrrigerComprehensive = null;
        public static WinTrrigerComprehensive GetWinInst(out bool blNew)//是否新创建实例
        {
            blNew = false;
            if (g_WinTrrigerComprehensive == null)
            {
                blNew = true;
                g_WinTrrigerComprehensive = new WinTrrigerComprehensive();
            }
            return g_WinTrrigerComprehensive;
        }
        #endregion 窗体单实例

        #region 定义
        //List
        public List<CmdTrringer> CmdTrringer_L = new List<CmdTrringer>();

        //bool 
       
        //int 
     

        //定义事件      
       
        #endregion 定义

        #region 初始化
        public WinTrrigerComprehensive()
        {
            InitializeComponent();

            //初始化控件位置
            //LocationRight();
            Login_Event();//事件注册
        }

        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerComprehensive", ex);
            }
        }

        public void Init(UCStateWork uCStateWork)
        {
            try
            {
                //baseUCTrrigerComprehensive1.Init(uCStateWork);
                //baseUCTrrigerComprehensive2.Init(uCStateWork);
                //baseUCTrrigerComprehensive3.Init(uCStateWork);
                //baseUCTrrigerComprehensive4.Init(uCStateWork);
                //baseUCTrrigerComprehensive5.Init(uCStateWork);
                //baseUCTrrigerComprehensive6.Init(uCStateWork);
                //baseUCTrrigerComprehensive7.Init(uCStateWork);
                //baseUCTrrigerComprehensive8.Init(uCStateWork);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerComprehensive", ex);
            }
        }

        #region 事件注册
        void Login_Event()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerComprehensive", ex);
            }
        }
        #endregion 事件注册
        #endregion 初始化

        #region 触发响应方法          
     
        #endregion 触发响应方法

        #region 显示

        #endregion 显示

        #region 退出
        /// <summary>
        /// 退出窗体，先退出实时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //baseUCTrrigerComprehensive1.Close();
                //baseUCTrrigerComprehensive2.Close();
                //baseUCTrrigerComprehensive3.Close();
                //baseUCTrrigerComprehensive4.Close();
                //baseUCTrrigerComprehensive5.Close();
                //baseUCTrrigerComprehensive6.Close();
                //baseUCTrrigerComprehensive7.Close();
                //baseUCTrrigerComprehensive8.Close();
                Thread.Sleep(300);
                this.Close();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerComprehensive", ex);
            }
           
        }
        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                g_WinTrrigerComprehensive = null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerRobot", ex);
            }
        }
        #endregion 退出

    }

    public class CmdTrringer
    {
        public int No { get; set; }
        public int NoCamera { get; set; }
        public int NoPos { get; set; }
        public string Annotation { get; set; }
    }
}
