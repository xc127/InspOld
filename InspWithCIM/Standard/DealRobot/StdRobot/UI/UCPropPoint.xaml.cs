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
using DealFile;
using DealLog;
using System.Threading;
using System.Threading.Tasks;

namespace DealRobot
{
    /// <summary>
    /// UCPropPoint.xaml 的交互逻辑
    /// </summary>
    public partial class UCPropPoint : BaseControl
    {
        #region 定义

        public List<string> NoPos_L = new List<string>();//跳转点位列表

        //右键菜单
        public ContextMenu CmOperation = new ContextMenu();

        public MenuItem miShowAllPointsValue = new MenuItem();


        /// 使能控件
        public bool Enable_dudBlow_Wait//吹气延迟
        {
            get
            {
                if (cboBlow.SelectedIndex == 2)
                {
                    return true;
                }
                return false;
            }
            set
            {

            }
        }
        #endregion 定义

        #region 初始化
        public UCPropPoint()
        {
            InitializeComponent();
            //初始化右键菜单
            InitContextMenu();

        }

        public void Init()
        {
            try
            {
                Init_NoPos();

                ShowPar_Invoke();
            }
            catch (Exception ex)
            {

            }
        }

        public void InitContextMenu()
        {
            try
            {
                miShowAllPointsValue.Header = "查看所有点位值";
                miShowAllPointsValue.Click += new RoutedEventHandler(miShowAllPointsValue_Click);
                CmOperation.Items.Add(miShowAllPointsValue);
                gpbPoints.ContextMenu = CmOperation;
            }
            catch (Exception ex)
            {

            }
        }

        void miShowAllPointsValue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new WinAllPointValue().Show();
            }
            catch (Exception ex)
            {
                
            }
        }

        #endregion 初始化

        #region 点位综合

        #region 点位
        /// <summary>
        /// 更改点位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dudNumPoint_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudNumPoint.IsMouseOver)
                {
                    if (RobotStdPoint.R_I.NumPoint < (int)dudNumPoint.Value)
                    {
                        RobotStdPoint.R_I.NumPoint = (int)dudNumPoint.Value;
                        //增加点位
                        RobotStdPoint.R_I.AddPoint();
                    }
                    else if (RobotStdPoint.R_I.NumPoint > (int)dudNumPoint.Value)
                    {
                        RobotStdPoint.R_I.NumPoint = (int)dudNumPoint.Value;
                        //删除点位
                        RobotStdPoint.R_I.DelPoint();
                    }

                    this.dgPoint.SelectedIndex = RobotStdPoint.R_I.NumPoint;//选中最后一个点
                    base.IndexP = this.dgPoint.SelectedIndex;

                    //刷新显示
                    ShowPar_Invoke();

                    //初始化点位选择列表
                    Init_NoPos();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 初始化点位列表
        /// </summary>
        void Init_NoPos()
        {
            try
            {
                NoPos_L.Clear();
                NoPos_L.Add("home");
                for (int i = 0; i < RobotStdPoint.R_I.NumPoint; i++)
                {
                    string name = "pos" + i.ToString();
                    if (name != RobotStdPoint.R_I[base.IndexP].NamePoint)
                    {
                        NoPos_L.Add(name);
                    }
                }

                NoPos_L.Add("null");
                RefreshCboPos();
            }
            catch (Exception ex)
            {

            }
        }

        void RefreshCboPos()
        {
            try
            {
                #region 输入
                cboVaccum_NGPos.ItemsSource = NoPos_L;
                cboVaccum_NGPos.Items.Refresh();

                cboInputIO1_OKPos.ItemsSource = NoPos_L;
                cboInputIO1_OKPos.Items.Refresh();
                cboInputIO1_NGPos.ItemsSource = NoPos_L;
                cboInputIO1_NGPos.Items.Refresh();

                cboInputIO2_OKPos.ItemsSource = NoPos_L;
                cboInputIO2_OKPos.Items.Refresh();
                cboInputIO2_NGPos.ItemsSource = NoPos_L;
                cboInputIO2_NGPos.Items.Refresh();

                cboInputIO3_OKPos.ItemsSource = NoPos_L;
                cboInputIO3_OKPos.Items.Refresh();
                cboInputIO3_NGPos.ItemsSource = NoPos_L;
                cboInputIO3_NGPos.Items.Refresh();

                cboInputIO4_OKPos.ItemsSource = NoPos_L;
                cboInputIO4_OKPos.Items.Refresh();
                cboInputIO4_NGPos.ItemsSource = NoPos_L;
                cboInputIO4_NGPos.Items.Refresh();

                cboPC_NGPos.ItemsSource = NoPos_L;
                cboPC_NGPos.Items.Refresh();
                #endregion 输入


                #region 输出
                cboNextPos.ItemsSource = NoPos_L;
                cboNextPos.Items.Refresh();
                #endregion 输出
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 获取跳转的点位序号
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        int GetNo_NoPos(int index)
        {
            try
            {
                if (index < 0)
                {
                    index = 0;
                }
                int no = 0;
                string str = NoPos_L[index];
                if (str == "home")
                {
                    no = 1000;
                }
                else if (str == "null")
                {
                    no = -1;
                }
                else
                {
                    no = int.Parse(str.Replace("pos", ""));
                }
                return no;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion 点位
        /// <summary>
        /// 增加点位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPoint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dgPoint.SelectedIndex == -1)
                {
                    //增加点位
                    RobotStdPoint.R_I.AddPoint();
                }
                else
                {
                    base.IndexP = this.dgPoint.SelectedIndex;
                    //增加点位
                    RobotStdPoint.R_I.AddPoint(base.IndexP);
                }
                //刷新显示
                RefreshDgPoint();
                //出生点位选择列表
                Init_NoPos();

                ShowPar_Point();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnDelPoint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RobotStdPoint.R_I.NumPoint == 0)
                {
                    btnDelPoint.RefreshDefaultColor("禁止删除",false);
                    return;
                }
                if (this.dgPoint.SelectedIndex == -1)
                {
                    //增加点位
                    RobotStdPoint.R_I.DelPoint();
                }
                else
                {
                    base.IndexP = this.dgPoint.SelectedIndex;
                    //增加点位
                    RobotStdPoint.R_I.DelPoint(base.IndexP);
                }
                //刷新显示
                RefreshDgPoint();
                //出生点位选择列表
                Init_NoPos();

                ShowPar_Point();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 选择点位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgPoint.IsMouseOver)
                {
                    base.IndexP = dgPoint.SelectedIndex;
                    //初始化跳转点位
                    Init_NoPos();
                    //刷新显示
                    ShowPar_Invoke();
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region Print
        private void tsbShowInfo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbShowInfo.IsMouseOver)
                {
                    RobotStdPoint.R_I.P_Print.DblValue1 = 1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbShowInfo_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbShowInfo.IsMouseOver)
                {
                    RobotStdPoint.R_I.P_Print.DblValue1 = -1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbOutPCInfo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbOutPCInfo.IsMouseOver)
                {
                    RobotStdPoint.R_I.P_Print.DblValue2 = 1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbOutPCInfo_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbOutPCInfo.IsMouseOver)
                {
                    RobotStdPoint.R_I.P_Print.DblValue2 = -1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        void ShowPar_Print()
        {
            try
            {
                tsbShowInfo.IsChecked = RobotStdPoint.R_I.P_Print.DblValue1 == 1 ? true : false;
                tsbOutPCInfo.IsChecked = RobotStdPoint.R_I.P_Print.DblValue2 == 1 ? true : false;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion Print

        #region 显示
        /// <summary>
        /// 显示点位设置
        /// </summary>
        void ShowPar_Point()
        {
            try
            {
                dudNumPoint.Value = RobotStdPoint.R_I.NumPoint;
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 显示
        #endregion 点位综合

        #region 输入

        #region 真空
        /// <summary>
        /// 判断真空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbVaccum_In_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbVaccum_In.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Vaccum.DblValue1 = (int)dudVaccum_No.Value;//真空IO的序号

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 不判断真空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbVaccum_In_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbVaccum_In.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Vaccum.DblValue1 = -1;

                    //刷新显示
                    ShowPar_Invoke();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 等待时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dudVaccum_Wait_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudVaccum_Wait.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Vaccum.DblValue2 = (double)dudVaccum_Wait.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// NG报警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dudVaccum_NGIO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudVaccum_NGIO.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Vaccum.DblValue3 = (double)dudVaccum_NGIO.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// NG跳转点位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboVaccum_NGPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboVaccum_NGPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Vaccum.DblValue4 = GetNo_NoPos(this.cboVaccum_NGPos.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtVaccum_Anno_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                RobotStdPoint.R_I[base.IndexP].P_Vaccum.Annotation = txtVaccum_Anno.Text.Trim();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 显示
        /// </summary>
        void ShowPar_Vaccum()
        {
            try
            {
                tsbVaccum_In.IsChecked = RobotStdPoint.R_I[base.IndexP].P_Vaccum.DblValue1 == -1 ? false : true;

                dudVaccum_Wait.Value = RobotStdPoint.R_I[base.IndexP].P_Vaccum.DblValue2;               

                ShowJumpPos(cboVaccum_NGPos, RobotStdPoint.R_I[base.IndexP].P_Vaccum.DblValue4);             

                txtVaccum_Anno.Text = RobotStdPoint.R_I[base.IndexP].P_Vaccum.Annotation;              
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 真空

        #region 输入IO
        #region InputIO1
        /// <summary>
        /// Input1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbInputIO1_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbInputIO1.IsMouseOver)
                {                    
                    RobotStdPoint.R_I[base.IndexP].P_InputIO1.DblValue1 = 1;//真空IO的序号

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbInputIO1_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbInputIO1.IsMouseOver)
                {                   
                    RobotStdPoint.R_I[base.IndexP].P_InputIO1.DblValue1 = -1;//真空IO的序号  

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO1_No_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO1_No.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO1.DblValue2 = (int)dudInputIO1_No.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO1_Wait_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO1_Wait.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO1.DblValue3 = (int)dudInputIO1_Wait.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO1_NGIO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO1_NGIO.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO1.DblValue4 = (int)dudInputIO1_NGIO.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }
        //OK点位跳转
        private void cboInputIO1_OKPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboInputIO1_OKPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_JumpIOP1.DblValue1 = GetNo_NoPos(cboInputIO1_OKPos.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

            }
        }
        //NG点位跳转
        private void cboInputIO1_NGPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboInputIO1_NGPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_JumpIOP1.DblValue2 = GetNo_NoPos(cboInputIO1_NGPos.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtInputIO1_Anno_LostFocus(object sender, RoutedEventArgs e)
        {
            RobotStdPoint.R_I[base.IndexP].P_InputIO1.Annotation = txtInputIO1_Anno.Text.Trim();
        }
        #endregion InputIO1

        #region InputIO2
        /// <summary>
        /// Input2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbInputIO2_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbInputIO2.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO2.DblValue1 = 1;//真空IO的序号
                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbInputIO2_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbInputIO2.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO2.DblValue1 = -1;//真空IO的序号   

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO2_No_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO2_No.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO2.DblValue2 = (int)dudInputIO2_No.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO2_Wait_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO2_Wait.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO2.DblValue3 = (int)dudInputIO2_Wait.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO2_NGIO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO2_NGIO.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO2.DblValue4 = (int)dudInputIO2_NGIO.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }
        //OK点位跳转
        private void cboInputIO2_OKPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboInputIO2_OKPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_JumpIOP2.DblValue1 = GetNo_NoPos(cboInputIO2_OKPos.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

            }
        }
        //NG点位跳转
        private void cboInputIO2_NGPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboInputIO2_NGPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_JumpIOP2.DblValue2 = GetNo_NoPos(cboInputIO2_NGPos.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtInputIO2_Anno_LostFocus(object sender, RoutedEventArgs e)
        {
            RobotStdPoint.R_I[base.IndexP].P_InputIO2.Annotation = txtInputIO2_Anno.Text.Trim();
        }
        #endregion InputIO2

        #region InputIO3
        /// <summary>
        /// Input2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbInputIO3_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbInputIO3.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO3.DblValue1 = 1;//真空IO的序号

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbInputIO3_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbInputIO3.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO3.DblValue1 = -1;//真空IO的序号   

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO3_No_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO3_No.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO3.DblValue2 = (int)dudInputIO3_No.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO3_Wait_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO3_Wait.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO3.DblValue3= (int)dudInputIO3_Wait.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO3_NGIO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO3_NGIO.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO3.DblValue4 = (int)dudInputIO3_NGIO.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }
        //OK点位跳转
        private void cboInputIO3_OKPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboInputIO3_OKPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_JumpIOP3.DblValue1 = GetNo_NoPos(cboInputIO3_OKPos.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

            }
        }
        //NG点位跳转
        private void cboInputIO3_NGPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboInputIO3_NGPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_JumpIOP3.DblValue2 = GetNo_NoPos(cboInputIO3_NGPos.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtInputIO3_Anno_LostFocus(object sender, RoutedEventArgs e)
        {
            RobotStdPoint.R_I[base.IndexP].P_InputIO3.Annotation = txtInputIO3_Anno.Text.Trim();
        }
        #endregion InputIO3

        #region InputIO4
        /// <summary>
        /// InputIO4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbInputIO4_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbInputIO4.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO4.DblValue1 = 1;//真空IO的序号
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbInputIO4_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbInputIO4.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO4.DblValue1 = -1;//真空IO的序号   
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO4_No_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO4_No.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO4.DblValue2 = (int)dudInputIO4_No.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO4_Wait_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO4_Wait.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO4.DblValue3 = (int)dudInputIO4_Wait.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudInputIO4_NGIO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudInputIO4_NGIO.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_InputIO4.DblValue4 = (int)dudInputIO4_NGIO.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }
        //OK点位跳转
        private void cboInputIO4_OKPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                e.Handled = true;
                if (cboInputIO4_OKPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_JumpIOP4.DblValue1 = GetNo_NoPos(cboInputIO4_OKPos.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

            }
        }
        //NG点位跳转
        private void cboInputIO4_NGPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                e.Handled = true;
                if (cboInputIO4_NGPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_JumpIOP4.DblValue2 = GetNo_NoPos(cboInputIO4_NGPos.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtInputIO4_Anno_LostFocus(object sender, RoutedEventArgs e)
        {
            RobotStdPoint.R_I[base.IndexP].P_InputIO4.Annotation = txtInputIO4_Anno.Text.Trim();
        }
        #endregion InputIO4

        /// <summary>
        /// 显示
        /// </summary>
        void ShowPar_InputIO()
        {
            try
            {
                tsbInputIO1.IsChecked = RobotStdPoint.R_I[base.IndexP].P_InputIO1.DblValue1 == 1 ? true : false;
                tsbInputIO2.IsChecked = RobotStdPoint.R_I[base.IndexP].P_InputIO2.DblValue1 == 1 ? true : false;
                tsbInputIO3.IsChecked = RobotStdPoint.R_I[base.IndexP].P_InputIO3.DblValue1 == 1 ? true : false;
                tsbInputIO4.IsChecked = RobotStdPoint.R_I[base.IndexP].P_InputIO4.DblValue1 == 1 ? true : false;

                dudInputIO1_No.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO1.DblValue2;
                dudInputIO2_No.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO2.DblValue2;
                dudInputIO3_No.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO3.DblValue2;
                dudInputIO4_No.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO4.DblValue2;

                dudInputIO1_Wait.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO1.DblValue3;
                dudInputIO2_Wait.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO2.DblValue3;
                dudInputIO3_Wait.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO3.DblValue3;
                dudInputIO4_Wait.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO4.DblValue3;

                dudInputIO1_NGIO.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO1.DblValue4;
                dudInputIO1_NGIO.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO2.DblValue4;
                dudInputIO1_NGIO.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO3.DblValue4;
                dudInputIO1_NGIO.Value = RobotStdPoint.R_I[base.IndexP].P_InputIO4.DblValue4;


                ShowJumpPos(cboInputIO1_OKPos, RobotStdPoint.R_I[base.IndexP].P_JumpIOP1.DblValue1);
                ShowJumpPos(cboInputIO1_NGPos, RobotStdPoint.R_I[base.IndexP].P_JumpIOP1.DblValue2);

                ShowJumpPos(cboInputIO2_OKPos, RobotStdPoint.R_I[base.IndexP].P_JumpIOP2.DblValue1);
                ShowJumpPos(cboInputIO2_NGPos, RobotStdPoint.R_I[base.IndexP].P_JumpIOP2.DblValue2);

                ShowJumpPos(cboInputIO3_OKPos, RobotStdPoint.R_I[base.IndexP].P_JumpIOP3.DblValue1);
                ShowJumpPos(cboInputIO3_NGPos, RobotStdPoint.R_I[base.IndexP].P_JumpIOP3.DblValue2);

                ShowJumpPos(cboInputIO4_OKPos, RobotStdPoint.R_I[base.IndexP].P_JumpIOP4.DblValue1);
                ShowJumpPos(cboInputIO4_NGPos, RobotStdPoint.R_I[base.IndexP].P_JumpIOP4.DblValue2);

                txtInputIO1_Anno.Text = RobotStdPoint.R_I[base.IndexP].P_InputIO1.Annotation;
                txtInputIO2_Anno.Text = RobotStdPoint.R_I[base.IndexP].P_InputIO2.Annotation;
                txtInputIO3_Anno.Text = RobotStdPoint.R_I[base.IndexP].P_InputIO3.Annotation;
                txtInputIO4_Anno.Text = RobotStdPoint.R_I[base.IndexP].P_InputIO4.Annotation;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 输入IO

        #region PC
        /// <summary>
        /// PC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPC_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbPC.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PC.DblValue1 = 1;

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbPC_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbPC.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PC.DblValue1 = -1;

                    //刷新显示
                    ShowPar_Invoke();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPC_Wait_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPC_Wait.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PC.DblValue2 = (int)dudPC_Wait.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPC_NGIO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPC_NGIO.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PC.DblValue3 = (int)dudPC_NGIO.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }
        //NG跳转点位
        private void cboPC_NGPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                e.Handled = true;
                if (cboPC_NGPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_NextPoint.DblValue1 = GetNo_NoPos(cboPC_NGPos.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtPC_Anno_LostFocus(object sender, RoutedEventArgs e)
        {
            RobotStdPoint.R_I[base.IndexP].P_PC.Annotation = txtPC_Anno.Text.Trim();
        }

        /// <summary>
        /// 显示
        /// </summary>
        void ShowPar_PC()
        {
            try
            {
                tsbPC.IsChecked = RobotStdPoint.R_I[base.IndexP].P_PC.DblValue1 == 1 ? true : false;

                dudPC_Wait.Value = RobotStdPoint.R_I[base.IndexP].P_PC.DblValue2;
                dudPC_NGIO.Value = RobotStdPoint.R_I[base.IndexP].P_PC.DblValue3;

                ShowJumpPos(cboPC_NGPos, RobotStdPoint.R_I[base.IndexP].P_PC.DblValue4);

                txtPC_Anno.Text = RobotStdPoint.R_I[base.IndexP].P_InputIO1.Annotation;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion PC

        #region PCCMD
        private void cboPCCMD_X_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboPCCMD_X.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PCCMD.DblValue1 = cboPCCMD_X.SelectedIndex + 1;
                }
            }
            catch (Exception ex)
            {
                
            }            
        }
        private void cboPCCMD_Y_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                e.Handled = true;
                if (cboPCCMD_X.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PCCMD.DblValue2 = cboPCCMD_Y.SelectedIndex + 1;
                }
            }
            catch (Exception ex)
            {

            }     
        }

        private void cboPCCMD_Z_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboPCCMD_Z.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PCCMD.DblValue3 = cboPCCMD_Z.SelectedIndex + 1;
                }
            }
            catch (Exception ex)
            {

            }     
        }

        private void cboPCCMD_U_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboPCCMD_U.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PCCMD.DblValue4 = cboPCCMD_U.SelectedIndex + 1;
                }
            }
            catch (Exception ex)
            {

            }   
        }
        private void cboPCCMD_Arm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboPCCMD_Arm.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PCCMD.DblValue5 = cboPCCMD_Arm.SelectedIndex + 1;
                }
            }
            catch (Exception ex)
            {

            }   
        }
        
        
        /// <summary>
        /// 显示
        /// </summary>
        void ShowPar_PCCMD()
        {
            try
            {
                cboPCCMD_X.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_PCCMD.DblValue1 - 1;
                cboPCCMD_Y.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_PCCMD.DblValue2 - 1;
                cboPCCMD_Z.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_PCCMD.DblValue3 - 1;
                cboPCCMD_U.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_PCCMD.DblValue4 - 1;
                cboPCCMD_Arm.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_PCCMD.DblValue5 - 1;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion PCCMD

        /// <summary>
        /// 显示输出
        /// </summary>
        void ShowPar_Input()
        {
            try
            {
                ShowPar_Vaccum();
                ShowPar_InputIO();
                ShowPar_PC();
                ShowPar_PCCMD();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 输入

        #region 运动

        #region 速度
        private void tsbVel_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbVel.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Vel.DblValue1 = 1;

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void tsbVel_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbVel.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Vel.DblValue1 = -1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cboPower_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboPower.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Vel.DblValue2 = cboPower.SelectedIndex + 1;

                    //刷新列表显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudVel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudVel.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Vel.DblValue3 = (double)dudVel.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudAcc_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudAcc.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Vel.DblValue4 = (double)dudAcc.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 显示
        /// </summary>
        void ShowPar_Vel()
        {
            try
            {
                tsbVel.IsChecked = RobotStdPoint.R_I[base.IndexP].P_Vel.DblValue1 == 1 ? true : false;

                cboPower.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_Vel.DblValue2 - 1;

                dudVel.Value = RobotStdPoint.R_I[base.IndexP].P_Vel.DblValue3;
                dudAcc.Value = RobotStdPoint.R_I[base.IndexP].P_Vel.DblValue4;              
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 速度

        #region 目标点
        private void cboMove_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboMove.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Move.DblValue1 = cboMove.SelectedIndex + 1;
                 
                    //刷新列表
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void dudDesX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudDesX.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Des.DblValue1 = Math.Round((double)dudDesX.Value, 2);
                    dudDesX.Value = RobotStdPoint.R_I[base.IndexP].P_Des.DblValue1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudDesY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudDesY.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Des.DblValue2 = Math.Round((double)dudDesY.Value, 2);
                    dudDesY.Value = RobotStdPoint.R_I[base.IndexP].P_Des.DblValue2;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudDesZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudDesZ.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Des.DblValue3 = Math.Round((double)dudDesZ.Value, 2);
                    dudDesZ.Value = RobotStdPoint.R_I[base.IndexP].P_Des.DblValue3;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudDesR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudDesR.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Des.DblValue4 = Math.Round((double)dudDesR.Value, 2);
                    dudDesR.Value = RobotStdPoint.R_I[base.IndexP].P_Des.DblValue4;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cboArm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboArm.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Des.DblValue5 = cboArm.SelectedIndex + 1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 显示
        /// </summary>
        void ShowPar_Des()
        {
            try
            {
                cboMove.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_Move.DblValue1 - 1;

                dudDesX.Value = RobotStdPoint.R_I[base.IndexP].P_Des.DblValue1;
                dudDesY.Value = RobotStdPoint.R_I[base.IndexP].P_Des.DblValue2;
                dudDesZ.Value = RobotStdPoint.R_I[base.IndexP].P_Des.DblValue3;
                dudDesR.Value = RobotStdPoint.R_I[base.IndexP].P_Des.DblValue4;
                cboArm.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_Des.DblValue5 - 1;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 目标点

        #region 当前点
        
        private void dudHereX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudHereX.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Here.DblValue1 = Math.Round((double)dudHereX.Value, 2);
                    dudHereX.Value = RobotStdPoint.R_I[base.IndexP].P_Here.DblValue1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudHereY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudHereY.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Here.DblValue2 = Math.Round((double)dudHereY.Value, 2);
                    dudHereY.Value = RobotStdPoint.R_I[base.IndexP].P_Here.DblValue2;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudHereZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudHereZ.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Here.DblValue3 = Math.Round((double)dudHereZ.Value, 2);
                    dudHereZ.Value = RobotStdPoint.R_I[base.IndexP].P_Here.DblValue3;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void dudHereR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudHereR.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Here.DblValue4 = Math.Round((double)dudHereR.Value, 2);
                    dudHereR.Value = RobotStdPoint.R_I[base.IndexP].P_Here.DblValue4;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 相对或者是绝对移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboHereZ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboHereZ.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PassCMD.DblValue3 = cboHereZ.SelectedIndex+1;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void cboHereR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboHereR.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_PassCMD.DblValue4 = cboHereR.SelectedIndex+1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        void ShowPar_Here()
        {
            try
            {               
               
                cboHereZ.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_PassCMD.DblValue3-1;
                cboHereR.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_PassCMD.DblValue4-1;


                dudHereX.Value = RobotStdPoint.R_I[base.IndexP].P_Here.DblValue1;
                dudHereY.Value = RobotStdPoint.R_I[base.IndexP].P_Here.DblValue2;
                dudHereZ.Value = RobotStdPoint.R_I[base.IndexP].P_Here.DblValue3;
                dudHereR.Value = RobotStdPoint.R_I[base.IndexP].P_Here.DblValue4;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 当前点

        #region Pass1
        private void tsbPass1_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbPass1.IsMouseOver)
                {
                    //设置Pass点个数
                    SetNumPass();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbPass1_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbPass1.IsMouseOver)
                {
                    tsbPass2.IsChecked = false;
                    tsbPass3.IsChecked = false;
                    //设置Pass点个数
                    SetNumPass();                   
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        private void dudPass1_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass1_X.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue1 = Math.Round((double)dudPass1_X.Value, 2);
                    dudPass1_X.Value = RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass1_Y_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass1_Y.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue2 = Math.Round((double)dudPass1_Y.Value, 2);
                    dudPass1_Y.Value = RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue2;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass1_Z_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass1_Z.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue3 = Math.Round((double)dudPass1_Z.Value, 2);
                    dudPass1_Z.Value = RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue3;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass1_R_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass1_R.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue4 = Math.Round((double)dudPass1_R.Value, 2);
                    dudPass1_R.Value = RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue4;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cboPass1_Arm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboPass1_Arm.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue5 = cboPass1_Arm.SelectedIndex+1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        void ShowPar_Pass1()
        {
            try
            {
                ShowPassEnable();
                dudPass1_X.Value = RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue1;
                dudPass1_Y.Value = RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue2;
                dudPass1_Z.Value = RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue3;
                dudPass1_R.Value = RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue4;
                cboPass1_Arm.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_Pass1.DblValue5-1;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 显示Pass使能
        /// </summary>
        void ShowPassEnable()
        {
            try
            {
                switch ((int)RobotStdPoint.R_I[base.IndexP].P_PassCMD.DblValue1)
                {
                    case 0:
                        tsbPass1.IsChecked = false;
                        tsbPass2.IsChecked = false;
                        tsbPass3.IsChecked = false;
                        break;

                    case 1:
                        tsbPass1.IsChecked = true;
                        tsbPass2.IsChecked = false;
                        tsbPass3.IsChecked = false;
                        break;

                    case 2:
                        tsbPass1.IsChecked = true;
                        tsbPass2.IsChecked = true;
                        tsbPass3.IsChecked = false;
                        break;

                    case 3:
                        tsbPass1.IsChecked = true;
                        tsbPass2.IsChecked = true;
                        tsbPass3.IsChecked = true;
                        break;
                }

            }
            catch (Exception ex)
            {

            }
        }
        #endregion Pass1

        #region Pass2
        private void tsbPass2_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbPass2.IsMouseOver)
                {
                    //设置Pass点个数
                    SetNumPass();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbPass2_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbPass2.IsMouseOver)
                {
                    tsbPass3.IsChecked = false;
                    //设置Pass点个数
                    SetNumPass();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass2_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass2_X.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue1 = Math.Round((double)dudPass2_X.Value, 2);
                    dudPass2_X.Value = RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass2_Y_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass2_Y.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue2 = Math.Round((double)dudPass2_Y.Value, 2);
                    dudPass2_Y.Value = RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue2;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass2_Z_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass2_Z.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue3 = Math.Round((double)dudPass2_Z.Value, 2);
                    dudPass2_Z.Value = RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue3;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass2_R_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass2_R.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue4 = Math.Round((double)dudPass2_R.Value, 2);
                    dudPass2_R.Value = RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue4;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cboPass2_Arm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboPass2_Arm.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue5 = cboPass2_Arm.SelectedIndex + 1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        void ShowPar_Pass2()
        {
            try
            {
                ShowPassEnable();

                dudPass2_X.Value = RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue1;
                dudPass2_Y.Value = RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue2;
                dudPass2_Z.Value = RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue3;
                dudPass2_R.Value = RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue4;
                cboPass2_Arm.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_Pass2.DblValue5 - 1;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion Pass2

        #region Pass3
        private void tsbPass3_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbPass3.IsMouseOver)
                {
                    SetNumPass();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbPass3_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbPass3.IsMouseOver)
                {
                    SetNumPass();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass3_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass3_X.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue1 = Math.Round((double)dudPass3_X.Value, 2);
                    dudPass3_X.Value = RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass3_Y_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass3_Y.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue2 = Math.Round((double)dudPass3_Y.Value, 2);
                    dudPass3_Y.Value = RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue2;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass3_Z_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass3_Z.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue3 = Math.Round((double)dudPass3_Z.Value, 2);
                    dudPass3_Z.Value = RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue3;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudPass3_R_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudPass3_R.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue4 = Math.Round((double)dudPass3_R.Value, 2);
                    dudPass3_R.Value = RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue4;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cboPass3_Arm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboPass3_Arm.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue5 = cboPass3_Arm.SelectedIndex + 1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 设置Pass的点位个数
        /// </summary>
        void SetNumPass()
        {
            try
            {
                int num = 0;
                if ((bool)tsbPass1.IsChecked)
                {
                    num++;
                }
                if ((bool)tsbPass2.IsChecked)
                {
                    num++;
                }
                if ((bool)tsbPass3.IsChecked)
                {
                    num++;
                }
                RobotStdPoint.R_I[base.IndexP].P_PassCMD.DblValue1 = num;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 显示
        /// </summary>
        void ShowPar_Pass3()
        {
            try
            {
                ShowPassEnable();

                dudPass3_X.Value = RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue1;
                dudPass3_Y.Value = RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue2;
                dudPass3_Z.Value = RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue3;
                dudPass3_R.Value = RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue4;
                cboPass3_Arm.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_Pass3.DblValue5-1;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion Pass3

        /// <summary>
        /// 显示Move
        /// </summary>
        void ShowPar_Move()
        {
            try
            {
                ShowPar_Vel();
                ShowPar_Des();
                ShowPar_Here();
                ShowPar_Pass1();
                ShowPar_Pass2();
                ShowPar_Pass3();
               
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 运动

        #region 输出
        #region 输出IO
        private void tsbOutIO_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbOutIO.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_OutIO.DblValue1 = (int)dudOutIO.Value;

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbOutIO_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbOutIO.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_OutIO.DblValue1 = -1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudOutIO_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudOutIO.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_OutIO.DblValue2 = (int)dudOutIO.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtOutIO_Anno_LostFocus(object sender, RoutedEventArgs e)
        {
            RobotStdPoint.R_I[base.IndexP].P_OutIO.Annotation = txtOutIO_Anno.Text.Trim();
        }

        void ShowPar_OutIO()
        {
            try
            {
                tsbOutIO.IsChecked = RobotStdPoint.R_I[base.IndexP].P_OutIO.DblValue1 == 1 ? true : false;
                dudOutIO.Value = RobotStdPoint.R_I[base.IndexP].P_OutIO.DblValue2;
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 输出IO

        #region 真空
        private void cboVaccum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboVaccum.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_OutVaccum.DblValue1 = (int)cboVaccum.SelectedIndex + 1;

                    //刷新显示
                    ShowPar_Invoke();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void cboBlow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboBlow.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_OutVaccum.DblValue2 = (int)cboBlow.SelectedIndex + 1;

                    //刷新显示
                    ShowPar_Invoke();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudBlow_Wait_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudBlow_Wait.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_OutVaccum.DblValue3 = (int)dudBlow_Wait.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }


        void ShowPar_OutVaccum()
        {
            try
            {
                cboVaccum.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_OutVaccum.DblValue1 - 1;
                cboBlow.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_OutVaccum.DblValue2 - 1;

                dudBlow_Wait.Value = RobotStdPoint.R_I[base.IndexP].P_OutVaccum.DblValue3;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 真空

        #region PC
        private void tsbOutPC_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbOutPC.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_OutPC.DblValue1 = 1;

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tsbOutPC_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbOutPC.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_OutPC.DblValue1 = -1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cboTypeOutPC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboTypeOutPC.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_OutPC.DblValue2 = cboTypeOutPC.SelectedIndex + 1;

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudCmdOutPC_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudCmdOutPC.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_OutPC.DblValue3 = (int)dudCmdOutPC.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtOutPC_Anno_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                RobotStdPoint.R_I[base.IndexP].P_OutPC.Annotation = txtOutPC_Anno.Text.Trim();             
            }
            catch (Exception ex)
            {

            }
        }

        void ShowPar_OutPC()
        {
            try
            {
                tsbOutPC.IsChecked = RobotStdPoint.R_I[base.IndexP].P_OutPC.DblValue1 == 1 ? true : false;

                cboTypeOutPC.SelectedIndex = (int)RobotStdPoint.R_I[base.IndexP].P_OutPC.DblValue2 - 1;

                dudCmdOutPC.Value = RobotStdPoint.R_I[base.IndexP].P_OutPC.DblValue3;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion PC

     
        #region 下一个点位
        private void cboNextPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboNextPos.IsMouseOver)
                {
                    RobotStdPoint.R_I[base.IndexP].P_NextPoint.DblValue1 = GetNo_NoPos(cboNextPos.SelectedIndex);

                    //刷新显示
                    RefreshDgPoint();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtNextPos_LostFocus(object sender, RoutedEventArgs e)
        {
            RobotStdPoint.R_I[base.IndexP].P_NextPoint.Annotation = txtNextPos.Text.Trim();
        }

        /// <summary>
        /// 显示
        /// </summary>
        void ShowPar_NextPos()
        {
            try
            {
                if (RobotStdPoint.R_I[base.IndexP].P_NextPoint.DblValue1 == -1)
                {
                    cboNextPos.Text= "null";
                }
                else if (RobotStdPoint.R_I[base.IndexP].P_NextPoint.DblValue1 == 1000)
                {
                     cboNextPos.Text= "home";
                }
                else
                {
                    cboNextPos.Text = "pos" + RobotStdPoint.R_I[base.IndexP].P_NextPoint.DblValue1.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 下一个点位

        /// <summary>
        /// 显示输出
        /// </summary>
        void ShowPar_Out()
        {
            try
            {                
                ShowPar_OutIO();
                ShowPar_OutVaccum();
                ShowPar_OutPC();
                ShowPar_Print();
                ShowPar_NextPos();
            }
            catch (Exception ex)
            {               

            }
        }
        #endregion 输出

        #region 显示
        public override void ShowPar_Invoke()
        {
            ShowPar_Input();
            ShowPar_Move();
            ShowPar_Out();

            ShowPar_Point();//点位综合设置

            ShowTitle();

            RefreshDgPoint();
        }
        public void RefreshDgPoint()
        {
            try
            {
                dgPoint.ItemsSource = RobotStdPoint.R_I.RobotStdPoint_L;
                dgPoint.Items.Refresh();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 显示跳转点位
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="value"></param>
        void ShowJumpPos(ComboBox cbo,double value)
        {
            try
            {
                if (value==-1)
                {
                    int num = cbo.Items.Count;
                    cbo.SelectedIndex = num - 1;
                }
                else if (value == 1000)
                {
                    cbo.SelectedIndex = 1;
                }
                else
                {
                    cbo.SelectedIndex = (int)value + 1;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// 显示抬头
        /// </summary>
        void ShowTitle()
        {
            try
            {
                gpbInputIO.Header=RobotStdPoint.R_I[base.IndexP].NamePoint+"输入IO";
                gpbInputPC.Header = RobotStdPoint.R_I[base.IndexP].NamePoint + "输入PC数据";
                gpbMove.Header = RobotStdPoint.R_I[base.IndexP].NamePoint + "运动参数";
                gpbOut.Header = RobotStdPoint.R_I[base.IndexP].NamePoint + "输出";
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 显示

        #region 保存
        private void btnSaveAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgPoint.Items.Count; i++)
                {
                    RobotStdPoint.R_I[i].Annotation=((RobotStdPoint)dgPoint.Items[i]).Annotation;
                }
                //保存到Xml
                if (RobotStdPoint.R_I.WriteXml())
                {
                    btnSaveAll.RefreshDefaultColor("保存成功", true);
                }
                else
                {
                    btnSaveAll.RefreshDefaultColor("保存失败", false);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 保存

        #region 写入机器人
        private void btnWriteRobot_Click(object sender, RoutedEventArgs e)
        {
            btnSaveAll_Click(null, null);
            new Task(LogicRobot.L_I.WriteConfigParStdRobot).Start();
        }

        private void btnWriteSelect_Click(object sender, RoutedEventArgs e)
        {
            btnSaveAll_Click(null, null);
            int index = this.dgPoint.SelectedIndex;
            new Task(LogicRobot.L_I.WriteConfigParStdRobot, index).Start();
        }
        #endregion 写入机器人

       

        

       
    }
}
