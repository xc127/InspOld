using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DealRobot;
using DealPLC;
using Common;
using SetPar;
using DealFile;
using BasicClass;
using DealConfigFile;
using DealCIM;

namespace Main
{
    partial class MainWindow
    {
        #region 定义

        #endregion

        #region 实现方法选择
        void R_Inst_Others_event(int index)
        {
            switch (index)
            {
                case 1:
                    RobotOthers1();
                    break;

                case 2:
                    RobotOthers2();
                    break;

                case 3:
                    RobotOthers3();
                    break;

                case 4:
                    RobotOthers4();
                    break;

                case 5:
                    RobotOthers5();
                    break;

                case 6:
                    RobotOthers6();
                    break;
            }
        }
        #endregion 实现方法选择

        /// <summary>
        /// 捡片机专用，机器人触发读码，且需要一个读码完成信号
        /// </summary>
        /// <returns></returns>
        public bool RobotOthers1()
        {
            try
            {
                TriggerGetCode(false);
                LogicRobot.L_I.WriteRobotCMD(new Point4D(1, 0, 0, 0), "34");
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
                return false;
            }
        }


        /// <summary>
        /// 捡片机专用，机器人交接完成
        /// </summary>
        /// <returns></returns>
        public bool RobotOthers2()
        {
            try
            {
                TransForkCode(RegeditMain.R_I.CodePlat);
                ShowState("Robot从巡边平台交接到插栏臂成功");
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool RobotOthers3()
        {

            try
            {
                RegeditMain.R_I.CodeArm2 = RegeditMain.R_I.CodePlat;
                ShowState("Robot从巡边平台取片成功" + RegeditMain.R_I.CodeArm2);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 捡片机有读码完成这样一个信号
        /// </summary>
        /// <returns></returns>
        public bool RobotOthers4()
        {
            try
            {
                ShowState("Robot触发读取二维码");
                TriggerGetCode(false);
                
                LogicRobot.L_I.WriteRobotCMD(new Point4D(1, 0, 0, 0), "34");
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
                return false;
            }
        }
        void SendCSTEnable()
        {
            LogicRobot.L_I.WriteRobotCMD("102");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool RobotOthers5()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool RobotOthers6()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
                return false;
            }
        }

    }
}
