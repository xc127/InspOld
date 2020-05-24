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
using DealComInterface;
using DealLog;

namespace DealRobot
{
    /// <summary>
    /// UCSetTypeRobot.xaml 的交互逻辑
    /// </summary>
    public partial class UCSetTypeRobot : BaseUCRobot
    {
        #region 定义
        //bool
        public BaseUCComInterface g_BaseUCComInterface = null;
        #endregion 定义

        #region 初始化
        public UCSetTypeRobot()
        {
            InitializeComponent();
        }

        public override void Init()
        {
            try
            {
                CreateRobotSetting(ParSetRobot.P_I.TypeRobot_e);//创建参数界面
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCSetTypeRobot", ex);
            }           
        }
        #endregion 初始化

        #region 设定机器人类型      
        /// <summary>
        /// 设定机器人类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboRobotType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                e.Handled = true;
                if (cboRobotType.IsMouseOver)
                {
                    TypeRobot_enum typeRobot_e = (TypeRobot_enum)cboRobotType.SelectedIndex;
                    CreateRobotSetting(typeRobot_e);//创建参数界面
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCSetTypeRobot", ex);
            }
        }
        /// <summary>
        /// 创建参数显示界面
        /// </summary>
        /// <param name="typeRobot_e"></param>
        void CreateRobotSetting(TypeRobot_enum typeRobot_e)
        {
            try
            {
                int intHeight = 140;
                int intWidth = 350;
                switch (typeRobot_e)
                {
                    case TypeRobot_enum.Null:
                        g_BaseUCComInterface = null;
                        break;

                    case TypeRobot_enum.YAMAH_Ethernet:
                        g_BaseUCComInterface = new UCSetInterfaceEthernet();
                        intHeight = 280;
                        intWidth = 435;
                        break;

                    case TypeRobot_enum.YAMAH_Serial:
                        g_BaseUCComInterface = null;
                        break;

                    case TypeRobot_enum.Epsion_Ethernet:
                        g_BaseUCComInterface = new UCSetInterfaceEthernet();
                        intHeight = 280;
                        intWidth = 435;
                        break;

                    case TypeRobot_enum.NaChi_Ethernet:
                        g_BaseUCComInterface = new UCSetInterfaceEthernet();
                        intHeight = 280;
                        intWidth = 435;
                        break;

                    case TypeRobot_enum.Epsion_Serial:
                        g_BaseUCComInterface = null;
                        break;

                }
                //添加控件显示
                AddChildCtr(intHeight, intWidth);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("SetTypeRobot", ex);
            }
        }

        /// <summary>
        /// 添加参数设置控件
        /// </summary>
        /// <param name="intHeight"></param>
        /// <param name="intWidth"></param>
        void AddChildCtr(int intHeight, int intWidth)
        {
            try
            {
                //移除前一个UI
                if (this.gdLayout.Children.Count > 2)
                {
                    this.gdLayout.Children.RemoveAt(2);
                }
                if (g_BaseUCComInterface == null)
                {
                    return;
                }
                //设定布局方式
                g_BaseUCComInterface.HorizontalAlignment = HorizontalAlignment.Center;
                g_BaseUCComInterface.VerticalAlignment = VerticalAlignment.Center;
                g_BaseUCComInterface.Height = intHeight;
                g_BaseUCComInterface.Width = intWidth;
                g_BaseUCComInterface.Margin = new Thickness(0, 0, 0, 0);
                Grid.SetColumn(g_BaseUCComInterface, 1);
                Grid.SetRow(g_BaseUCComInterface, 1);

                //初始化加载              
                g_BaseUCComInterface.Init(ParSetRobot.P_I.g_BaseParInterface);

                //添加控件
                this.gdLayout.Children.Add(g_BaseUCComInterface);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("SetTypeRobot", ex);
            }
        }
        #endregion 设定机器人类型

        #region 保存
        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                //备份文件到本地
                FunBackup.F_I.BackupRobot();

                ParSetRobot.P_I.TypeRobot_e = (TypeRobot_enum)cboRobotType.SelectedIndex;//机器人品牌以及通信方式
                ParSetRobot.P_I.BlAutoConnect = (bool)tsbAutoConnect.IsChecked;//是否自动连接通信
                ParSetRobot.P_I.WriteIniPar();//保存参数
                g_BaseUCComInterface.Save();//保存端口设置参数

                this.btnSave.RefreshDefaultColor("保存成功", true);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCSetTypeRobot", ex);
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnSave保存",
                "设置机器人类型");
            }
        }
        #endregion 保存

        #region 显示
        /// <summary>
        /// 显示参数
        /// </summary>
        public override void ShowPar()
        {
            try
            {
                cboRobotType.Text = ParSetRobot.P_I.TypeRobot_e.ToString();

                tsbAutoConnect.IsChecked = ParSetRobot.P_I.BlAutoConnect;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCSetTypeRobot", ex);
            }
        }
        #endregion 显示

    }
}
