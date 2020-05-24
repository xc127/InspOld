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
using DealFile;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using Common;
using BasicClass;
using Xceed.Wpf.Toolkit;
using System.IO;

namespace DealConfigFile
{
    public partial class BaseUCAdjust : BaseControl
    {
        #region 定义      
        //path
        public string g_Path { set; get; } //保存路径

        #region bool
        public bool BlChange
        {
            get
            {
                return ParSetAdjust.P_I[this.Name][0].BlChange
                || ParSetAdjust.P_I[this.Name][1].BlChange
                || ParSetAdjust.P_I[this.Name][2].BlChange
                || ParSetAdjust.P_I[this.Name][3].BlChange
                || Value1 != 0
                || Value2 != 0
                || Value3 != 0
                || Value4 != 0;
            }
        }
        #endregion bool

        #region 调整值增量
        public double DblIncrement1
        {
            get
            {
                return GetIncrement((ParSetAdjust.P_I[this.Name])[0].Increment); 
            }
        }

        public double DblIncrement2
        {
            get
            {
                return GetIncrement((ParSetAdjust.P_I[this.Name])[1].Increment); 
            }
        }

        public double DblIncrement3
        {
            get
            {
                return GetIncrement((ParSetAdjust.P_I[this.Name])[2].Increment); 
            }
        }

        public double DblIncrement4
        {
            get
            {

                return GetIncrement((ParSetAdjust.P_I[this.Name])[3].Increment); 
            }
        }
        #endregion 调整值增量

        #region 小数位数
        public int IntDecimals1
        {
            get
            {
                return (ParSetAdjust.P_I[this.Name])[0].Increment;
            }
        }

        public int IntDecimals2
        {
            get
            {
                return (ParSetAdjust.P_I[this.Name])[1].Increment;
            }
        }

        public int IntDecimals3
        {
            get
            {
                return (ParSetAdjust.P_I[this.Name])[2].Increment;
            }
        }

        public int IntDecimals4
        {
            get
            {
                return (ParSetAdjust.P_I[this.Name])[3].Increment;
            }
        }
        #endregion 小数位数
      
        #region value
        public double Value1
        {
            get
            {
                try
                {
                    return AdjustIniBase.A_I.Value1(this.Name, g_Path);
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError("BaseUCAdjust", ex);
                    return 0;
                }
            }
        }

        public double Value2
        {
            get
            {
                try
                {
                    return AdjustIniBase.A_I.Value2(this.Name, g_Path); 
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError("BaseUCAdjust", ex);
                    return 0;
                }
            }
        }

        public double Value3
        {
            get
            {
                try
                {
                    return AdjustIniBase.A_I.Value3(this.Name, g_Path); 
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError("BaseUCAdjust", ex);
                    return 0;
                }
            }
        }

        public double Value4
        {
            get
            {
                try
                {
                    return AdjustIniBase.A_I.Value4(this.Name, g_Path); 
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError("BaseUCAdjust", ex);
                    return 0;
                }
            }
        }
        #endregion value

        #region 权限
        /// <summary>
        /// 设置权限
        /// </summary>
        public Authority_enum Authority1_e
        {
            get
            {
                //工程师权限
                if((ParSetAdjust.P_I[this.Name])[0].Engineer)
                {
                    return Authority_enum.Engineer;
                }
                //技术员权限
                if ((ParSetAdjust.P_I[this.Name])[0].Worker)
                {
                    return Authority_enum.Worker;
                }
                return Authority_enum.Null;
            }
        }
        public Authority_enum Authority2_e
        {
            get
            {
                //工程师权限
                if ((ParSetAdjust.P_I[this.Name])[1].Engineer)
                {
                    return Authority_enum.Engineer;
                }
                //技术员权限
                if ((ParSetAdjust.P_I[this.Name])[1].Worker)
                {
                    return Authority_enum.Worker;
                }
                return Authority_enum.Null;
            }
        }
        public Authority_enum Authority3_e
        {
            get
            {
                //工程师权限
                if ((ParSetAdjust.P_I[this.Name])[2].Engineer)
                {
                    return Authority_enum.Engineer;
                }
                //技术员权限
                if ((ParSetAdjust.P_I[this.Name])[2].Worker)
                {
                    return Authority_enum.Worker;
                }
                return Authority_enum.Null;
            }
        }
        public Authority_enum Authority4_e
        {
            get
            {
                //工程师权限
                if ((ParSetAdjust.P_I[this.Name])[3].Engineer)
                {
                    return Authority_enum.Engineer;
                }
                //技术员权限
                if ((ParSetAdjust.P_I[this.Name])[3].Worker)
                {
                    return Authority_enum.Worker;
                }
                return Authority_enum.Null;
            }
        }
        #endregion 权限

        //string 
        public string Annotation
        {
            get
            {
                try
                {
                    return AdjustIniBase.A_I.Annotation(this.Name, g_Path); 
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError("BaseUCAdjust", ex);
                    return "";
                }
            }
        }

        #region 控件
        //DoubleUpDown
        public DoubleUpDown g_DudAdjust1 = null;
        public DoubleUpDown g_DudAdjust2 = null;
        public DoubleUpDown g_DudAdjust3 = null;
        public DoubleUpDown g_DudAdjust4 = null;

        //lable
        public Label g_LblName1 = null;
        public Label g_LblName2 = null;
        public Label g_LblName3 = null;
        public Label g_LblName4 = null;

        //GroupBox
        public GroupBox g_GpbTitle=null;

        //TextBox
        TextBox g_TxtAnnotation = null;

        #endregion 控件

        //保存路径
        public string PathAdjust
        {
            get
            {
                string root = ComConfigPar.C_I.PathConfigIni.Replace("Product.ini", "调整值");
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                return root + "\\Adjust.ini";
            }
        }
        #endregion 定义

        #region 初始化
        /// <summary>
        /// 初始化DoubleUpDown
        /// </summary>
        /// <param name="dudAdjust1"></param>
        /// <param name="dudAdjust2"></param>
        /// <param name="dudAdjust3"></param>
        /// <param name="dudAdjust4"></param>
        public void InitDoubleUpDown(DoubleUpDown dudAdjust1, DoubleUpDown dudAdjust2, DoubleUpDown dudAdjust3, DoubleUpDown dudAdjust4)
        {
            try
            {
                g_DudAdjust1 = dudAdjust1;
                g_DudAdjust2 = dudAdjust2;
                g_DudAdjust3 = dudAdjust3;
                g_DudAdjust4 = dudAdjust4;
  
                if (dudAdjust1 != null)
                {                       
                    //if ((int)Authority.Authority_e < (int)Authority1_e)//根据权限设置使能
                    //{
                    //    g_DudAdjust1.IsEnabled = false;
                    //}
                    //else
                    //{
                        g_DudAdjust1.Increment = DblIncrement1;
                        g_DudAdjust1.Minimum = (ParSetAdjust.P_I[this.Name])[0].Min;
                        g_DudAdjust1.Maximum = (ParSetAdjust.P_I[this.Name])[0].Max;
                        //g_DudAdjust1.IsEnabled = false;
                    //}
                    if (Value1 != 0)
                    {
                        g_DudAdjust1.Value = Value1;
                    }
                }

                if (dudAdjust2 != null)
                {
                    //if ((int)Authority.Authority_e < (int)Authority2_e)//根据权限设置使能
                    //{
                    //    g_DudAdjust2.IsEnabled = false;
                    //}
                    //else
                    //{
                        g_DudAdjust2.Increment = DblIncrement2;
                        g_DudAdjust2.Minimum = (ParSetAdjust.P_I[this.Name])[1].Min;
                        g_DudAdjust2.Maximum = (ParSetAdjust.P_I[this.Name])[1].Max;
                    //}
                    if (Value2 != 0)
                    {
                        g_DudAdjust2.Value = Value2;
                    }
                }                
               
                if (dudAdjust3 != null)
                {                     
                    //if ((int)Authority.Authority_e < (int)Authority3_e)//根据权限设置使能
                    //{
                    //    g_DudAdjust3.IsEnabled = false;
                    //}
                    //else
                    //{
                        g_DudAdjust3.Increment = DblIncrement3;
                        g_DudAdjust3.Minimum = (ParSetAdjust.P_I[this.Name])[2].Min;
                        g_DudAdjust3.Maximum = (ParSetAdjust.P_I[this.Name])[2].Max;
                    //}
                    if (Value3 != 0)
                    {
                        g_DudAdjust3.Value = Value3;
                    }
                }               
                
                if (dudAdjust4 != null)
                {                      
                    //if ((int)Authority.Authority_e < (int)Authority4_e)//根据权限设置使能
                    //{
                    //    g_DudAdjust4.IsEnabled = false;
                    //}
                    //else
                    //{
                        g_DudAdjust4.Increment = DblIncrement4;
                        g_DudAdjust4.Minimum = (ParSetAdjust.P_I[this.Name])[3].Min;
                        g_DudAdjust4.Maximum = (ParSetAdjust.P_I[this.Name])[3].Max;
                    //}
                    if (Value4 != 0)
                    {
                        g_DudAdjust4.Value = Value4;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCAdjust", ex);
            }
        }
        /// <summary>
        /// 初始化Lable
        /// </summary>
        /// <param name="lblName1"></param>
        /// <param name="lblName2"></param>
        /// <param name="lblName3"></param>
        /// <param name="lblName4"></param>
        public void InitLable(Label lblName1, Label lblName2, Label lblName3, Label lblName4)
        {
            try
            {                
                if ((ParSetAdjust.P_I[this.Name])[0].Name != "Value1")
                {
                    g_LblName1 = lblName1;
                    if (lblName1 != null)
                    {                       
                        g_LblName1.Content = (ParSetAdjust.P_I[this.Name])[0].Name;
                    }
                }

                if ((ParSetAdjust.P_I[this.Name])[1].Name != "Value2")
                {
                    g_LblName2 = lblName2;
                    if (lblName2 != null)
                    {
                        g_LblName2.Content = (ParSetAdjust.P_I[this.Name])[1].Name;
                    }
                }

                if ((ParSetAdjust.P_I[this.Name])[2].Name != "Value3")
                {
                    g_LblName3 = lblName3;
                    if (lblName3 != null)
                    {
                        g_LblName3.Content = (ParSetAdjust.P_I[this.Name])[2].Name;
                    }
                }

                if ((ParSetAdjust.P_I[this.Name])[3].Name != "Value4")
                {

                    g_LblName4 = lblName4;
                    if (lblName4 != null)
                    {
                        g_LblName4.Content = (ParSetAdjust.P_I[this.Name])[3].Name;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCAdjust", ex);
            }
        }

        public void InitInfo(GroupBox gpdTitle, TextBox txtAnnotation)
        {
            try
            {
                g_GpbTitle = gpdTitle;
                if ( ParSetAdjust.P_I.Title !="")
                {
                    g_GpbTitle.Header = ParSetAdjust.P_I.Title + "(" + this.Name + ")";//标题
                }
                else
                {
                    g_GpbTitle.Header = this.Name + "调整值";//标题
                }
                if (Annotation != "")
                {
                    g_TxtAnnotation = txtAnnotation;
                    g_TxtAnnotation.Text = Annotation;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCAdjust", ex);
            }
        }
        #endregion 初始化

        #region Value1
        public void dudAdjust1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (g_DudAdjust1.IsMouseOver)
                {                   
                    g_DudAdjust1.Value = Math.Round((double)g_DudAdjust1.Value, IntDecimals1);
                }
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("BaseUCAdjust", ex);
            }
        }
        #endregion Value1

        #region Value2
        public void dudAdjust2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (g_DudAdjust2.IsMouseOver)
                {
                    g_DudAdjust2.Value = Math.Round((double)g_DudAdjust2.Value, IntDecimals2);
                }
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("BaseUCAdjust", ex);
            }
        }
        #endregion Value2

        #region Value3
        public void dudAdjust3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (g_DudAdjust3.IsMouseOver)
                {                   
                    g_DudAdjust3.Value = Math.Round((double)g_DudAdjust3.Value, IntDecimals3);
                }
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("BaseUCAdjust", ex);
            }
        }
        #endregion Value3

        #region Value4
        public void dudAdjust4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (g_DudAdjust4.IsMouseOver)
                {
                    g_DudAdjust4.Value = Math.Round((double)g_DudAdjust4.Value, IntDecimals4);
                }
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("BaseUCAdjust", ex);
            }
        }
        #endregion Value4
     
        #region 设置配置参数       
        public void gpbAdjust_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {             
                if (g_GpbTitle.IsMouseOver)
                {
                    if (!EngineerReturn())
                    {
                        return;
                    }
                    WinSetParAdjust winSetParAdjust = new WinSetParAdjust();
                    winSetParAdjust.Init(Name);
                    winSetParAdjust.ChangeInfo_event += new Action(winSetParAdjust_ChangeInfo_event);
                    winSetParAdjust.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCAdjust", ex);
            }
        }
        /// <summary>
        /// 重新加载信息
        /// </summary>
        void winSetParAdjust_ChangeInfo_event()
        {
            try
            {
                ShowPar(PathAdjust);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCAdjust", ex);
            }
        }
        #endregion 设置配置参数

        #region 显示
        public virtual void ShowPar(string path)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCAdjust", ex);
            }
        }
        #endregion 显示

        #region 增量
        /// <summary>
        /// 通过小数位数，求取增量
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        double GetIncrement(int num)
        {
            try
            {
                double value = 0;
                switch (num)
                {
                    case 0:
                        value = 1;
                        break;

                    case 1:
                        value = 0.1;
                        break;

                    case 2:
                        value = 0.01;
                        break;

                    case 3:
                        value = 0.001;
                        break;

                    case 4:
                        value = 0.0001;
                        break;

                    case 5:
                        value = 0.00001;
                        break;
                }
                return value;
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError("BaseUCAdjust", ex);
                return 0;
            }
        }
        #endregion 增量

        #region 自定义属性      
        #region Ini
      
        //选择存储路径,为Std文件,不选中，则默认存储在Adjust文件中
        [CategoryAttribute("自定义属性"), DescriptionAttribute("Ini存储的路径")]
        public bool PathStd
        {
            get { return (bool)GetValue(PathStdProperty); }
            set { SetValue(PathStdProperty, value); }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("Ini存储的Path")]
        public static readonly DependencyProperty PathStdProperty =
        DependencyProperty.Register("PathStd", typeof(bool), typeof(BaseUCAdjust), new PropertyMetadata(false));
        #endregion Ini

        #endregion 自定义属性
    }
}
