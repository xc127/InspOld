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
using System.Collections;
using System.ComponentModel;
using System.Management;
using System.Data;
using BasicClass;
using BasicDisplay;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace Camera
{
    /// <summary>
    /// WinRestartNet.xaml 的交互逻辑
    /// </summary>
    public partial class WinRestartNet : BaseMetroWindow
    {
        #region 窗体单实例
        public static WinRestartNet g_WinRestartNet = null;
        public static WinRestartNet GetWinInst()
        {
            try
            {
                if (g_WinRestartNet == null)
                {
                    g_WinRestartNet = new WinRestartNet();
                }
                return g_WinRestartNet;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRestartNet", ex);
                return null;
            }            
        }
        #endregion 窗体单实例

        #region 定义
        List<NetWork> NetWorkShow_L = new List<NetWork>();
        List<ManagementObject> ManagementObject_L = new List<ManagementObject>();
        ManagementObjectCollection g_ManagementObjectCollection = null;
        #endregion 定义

        #region 初始化
        public WinRestartNet()
        {
            InitializeComponent();
        }

        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {            
            NetWorkList();
            RefreshDgNet();
        }

        #endregion 初始化

        /// <summary>
        /// 网卡列表
        /// </summary>
        public void NetWorkList()
        {
            try
            {
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                string manage = "SELECT * From Win32_NetworkAdapter";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(manage);
                ManagementObjectCollection managementObjectCollection = searcher.Get();

                for (int i = 0; i < adapters.Length; i++)
                {
                    if (!adapters[i].Description.Contains("Intel")
                        && !adapters[i].Description.Contains("Realtek"))
                    {
                        continue;
                    }
                    foreach (ManagementObject item in managementObjectCollection)
                    {
                        if (item["MACAddress"] != null)
                        {
                            string mac = item["MACAddress"].ToString().Replace(":","");
                            string ip = item["IPAddress"].ToString().Replace(":", "");
                            if (mac == adapters[i].GetPhysicalAddress().ToString())
                            {
                                NetWorkShow_L.Add(new NetWork()
                                {
                                    Name = adapters[i].Name,
                                    Model = adapters[i].Description,
                                    Mac = mac,
                                    IP = ip,
                                });

                                ManagementObject_L.Add(item);
                            }
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }            
        }

       

        
        /// <summary>
        /// 网卡状态
        /// </summary>
        /// <param name="netWorkName">网卡名</param>
        /// <returns></returns>
        public bool NetWorkState(string netWorkName)
        {
            string netState = "SELECT * From Win32_NetworkAdapter";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            ManagementObjectCollection collection = searcher.Get();
            foreach (ManagementObject manage in collection)
            {
                if (manage["Name"].ToString() == netWorkName)
                {
                    return true;
                }
            }
            return false;
        }

      
        #region 启动
        /// <summary>
        /// 启用网卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartNet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                base.IndexP = this.dgNet.SelectedIndex;
                NetWork inst = NetWorkShow_L[base.IndexP];
                if (NetWorkState(inst.Model))
                {
                    if (!EnableNetWork())
                    {
                        MessageBox.Show("开启网卡失败!");
                    }
                    else
                    {
                        MessageBox.Show("开启网卡成功!");
                    }
                }
                else
                {
                    MessageBox.Show("网卡己开启!");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 启用网卡
        /// </summary>
        /// <param name="name">网卡名</param>
        /// <returns></returns>
        public bool EnableNetWork()
        {
            try
            {                
                base.IndexP = this.dgNet.SelectedIndex;
                ManagementObject_L[base.IndexP].InvokeMethod("Enable", null); 
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion 启动

        #region 禁用
        /// <summary>
        /// 禁止网卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopNet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                base.IndexP = this.dgNet.SelectedIndex;
                NetWork inst = NetWorkShow_L[base.IndexP];

                if (NetWorkState(inst.Model))
                {
                    if (!DisableNetWork(inst.Model))
                    {
                        MessageBox.Show("禁用网卡失败!");
                    }
                    else
                    {
                        MessageBox.Show("禁用网卡成功!");
                    }
                }
                else
                {
                    MessageBox.Show("网卡己禁用!");
                }

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }            
        }


        /// <summary>
        /// 禁用网卡
        /// </summary>5
        /// <param name="name">网卡名</param>
        /// <returns></returns>
        public bool DisableNetWork(string name)
        {
            try
            {
                base.IndexP = this.dgNet.SelectedIndex;
                ManagementObject_L[base.IndexP].InvokeMethod("disable", null); 
               
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion 禁用

        #region 重启
        /// <summary>
        /// 重启网卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestartNet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                base.IndexP = this.dgNet.SelectedIndex;
                NetWork inst = NetWorkShow_L[base.IndexP];
                if (DisableNetWork(inst.Model))
                {
                    EnableNetWork();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 重启


        #region 显示
        public override void ShowPar_Invoke()
        {
            try
            {
                this.Dispatcher.Invoke(new Action(ShowPar));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        public override void ShowPar()
        {

        }

        void RefreshDgNet()
        {
            try
            {

                dgNet.ItemsSource = NetWorkShow_L;
                dgNet.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示


        #region 关闭
        private void BaseMetroWindow_Closing(object sender, CancelEventArgs e)
        {
            g_WinRestartNet = null;
        }
        #endregion 关闭

    }

    public class NetWork:BaseClass
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Mac { get; set; }
        public string IP { get; set; }
    }
}
