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
using ControlLib;

namespace DealRobot
{
    /// <summary>
    /// UCTechRobot.xaml 的交互逻辑
    /// </summary>
    public partial class UCTechRobot : BaseControl
    {
        #region 定义
        public Point4D posRobot = new Point4D();
        #endregion

        #region 初始化
        public UCTechRobot()
        {
            InitializeComponent();
        }

        private void BaseControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetBtnPower(true);
        }

        #region 参数显示
        public override void ShowPar()
        {
            try
            {
                txtX.Text = posRobot.DblValue1.ToString();
                txtY.Text = posRobot.DblValue2.ToString();
                txtZ.Text = posRobot.DblValue3.ToString();
                txtU.Text = posRobot.DblValue4.ToString();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #endregion

        #region Jog
        /// <summary>
        /// 移动步长
        /// </summary>
        public double StepMove
        {
            get
            {
                return (double)dudStepMove.Value;
            }
        }

        /// <summary>
        /// 角度步长
        /// </summary>
        public double StepAngle
        {
            get
            {
                return (double)dudStepAngle.Value;
            }
        }

        /// <summary>
        /// 单步运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJogRobot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Point4D deltaP = GetDeltaPoint(sender);
                LogicRobot.L_I.WriteRobotCMD(deltaP, "4001");
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 通过按钮来计算当前坐标变化
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private Point4D GetDeltaPoint(object sender)
        {
            Point4D deltaP = new Point4D();
            try
            {
                Button btn = sender as Button;
                string content = btn.Content.ToString();
                switch (content)
                {
                    case "X+":
                        deltaP.DblValue1 = StepMove;
                        break;
                    case "X-":
                        deltaP.DblValue1 = -StepMove;
                        break;
                    case "Y+":
                        deltaP.DblValue1 = StepMove;
                        break;
                    case "Y-":
                        deltaP.DblValue1 = -StepMove;
                        break;
                    case "Z+":
                        deltaP.DblValue1 = StepMove;
                        break;
                    case "Z-":
                        deltaP.DblValue1 = -StepMove;
                        break;
                    case "R+":
                        deltaP.DblValue1 = StepAngle;
                        break;
                    case "R-":
                        deltaP.DblValue1 = -StepAngle;
                        break;
                    default:
                        MessageBox.Show("步长信息错误" + content);
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            return deltaP;
        }

        private void btnCurrPos_Click(object sender, RoutedEventArgs e)
        {
            LogicRobot.L_I.WriteRobotCMD("4000");
        }

        #endregion

        #region 事件处理
        /// <summary>
        /// 机器人发送当前点位过来时的事件处理程序
        /// </summary>
        /// <param name="value"></param>
        public void JogPosCurr_EventHandler(double[] value)
        {
            posRobot = new Point4D(value[0], value[1], value[2], value[3]);
            ShowPar_Invoke();
            this.Dispatcher.Invoke(new Action(() => { this.gdCtr.IsEnabled = true; }));
        }
        #endregion        

        #region Power
        private void btnSetPowerOff_Click(object sender, RoutedEventArgs e)
        {
            LogicRobot.L_I.WriteRobotCMD("4500");
            if (LogicRobot.L_I.WaitForOK())
            {
                SetBtnPower(false);
            }
        }

        private void btnSetPowerOn_Click(object sender, RoutedEventArgs e)
        {
            LogicRobot.L_I.WriteRobotCMD("4501");
            if (LogicRobot.L_I.WaitForOK())
            {
                SetBtnPower(true);
            }
        }

        void SetBtnPower(bool blPower)
        {
            try
            {
                if (blPower)
                {
                    btnSetPowerOn.Background = Brushes.Yellow;
                    btnSetPowerOn.IsEnabled = false;
                    btnSetPowerOff.Background = new SolidColorBrush(btnSetPowerOff.ButtonColor_Basic);
                    btnSetPowerOff.IsEnabled = true;
                }
                else
                {
                    btnSetPowerOff.Background = Brushes.Yellow;
                    btnSetPowerOff.IsEnabled = false;
                    btnSetPowerOn.Background = new SolidColorBrush(btnSetPowerOff.ButtonColor_Basic);
                    btnSetPowerOn.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


    }
}
