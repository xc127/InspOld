using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealFile;
using BasicClass;
using DealRobot;
using DealConfigFile;
using DealComInterface;

namespace DealRobot
{
    partial class LogicRobot
    {
        #region 定义
        //int 
        public int g_intCameraNo = 1;//相机序号

        #region 事件
        public event StrAction DataError_event;//数据超出范围,方法，当前值，标准值
        #endregion 事件
        #endregion 定义

        #region 写入结果
        //第一次拍照结果
        public void WriteData1(Point4D point)
        {
            try
            {
                //数据超限
                if (!BlDataOverFlow(point, 1))
                {
                    return;
                }
                string cmd = NoCamera.ToString() + "1";//命令

                string value = Head
                    + point.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                    + "0".PadLeft(7, ' ') + " "
                    + cmd.PadLeft(7, ' ');

                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        //第二次拍照结果
        public void WriteData2(Point4D point)
        {
            try
            {
                //数据超限
                if (!BlDataOverFlow(point, 2))
                {
                    return;
                }
                string cmd = NoCamera.ToString() + "2";//命令

                string value = Head
                    + point.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                    + "0".PadLeft(7, ' ') + " "
                    + cmd.PadLeft(7, ' ');

                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        //第三次拍照结果
        public void WriteData3(Point4D point)
        {
            try
            {
                //数据超限
                if (!BlDataOverFlow(point, 1))
                {
                    return;
                }
                string cmd = NoCamera.ToString() + "3";//命令

                string value = Head
                    + point.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                    + "0".PadLeft(7, ' ') + " "
                    + cmd.PadLeft(7, ' ');

                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        //第四次拍照结果
        public void WriteData4(Point4D point)
        {
            try
            {
                //数据超限
                if (!BlDataOverFlow(point, 2))
                {
                    return;
                }
                string cmd = NoCamera.ToString() + "4";//命令

                string value = Head
                    + point.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                    + "0".PadLeft(7, ' ') + " "
                    + cmd.PadLeft(7, ' ');
                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        //NGHome
        public void WriteData8(Point4D p)
        {
            try
            {
                string cmd = NoCamera.ToString() + "8";//命令
                string str = Head
                      + p.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                      + "0".PadLeft(7, ' ') + " "
                      + cmd.PadLeft(7, ' '); 
                g_PortRobotBase.WriteData(str);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        #region NG
        //NG
        public void SendDataNG(Point4D p)
        {
            try
            {
                string cmd = NoCamera.ToString() + "9";//命令
                string str = Head
                      + p.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                      + "0".PadLeft(7, ' ') + " "
                      + cmd.PadLeft(7, ' '); 
                g_PortRobotBase.WriteData(str);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        public void SendDataNG()
        {
            try
            {
                SendDataNG(new Point4D());
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        /// <summary>
        /// 包含点位的NG
        /// </summary>
        /// <param name="noCamera"></param>
        /// <param name="p"></param>
        public void SendDataNG90_Camera(Point4D p)
        {
            try
            {
                string cmd = NoCamera.ToString() + "90";//命令
                string value = Head
                      + p.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                      + "0".PadLeft(7, ' ') + " "
                      + cmd.PadLeft(7, ' ');
                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        public void SendDataNG91_Camera(Point4D p)
        {
            try
            {
                string cmd = NoCamera.ToString() + "91";//命令
                string value = Head
                      + p.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                      + "0".PadLeft(7, ' ') + " "
                      + cmd.PadLeft(7, ' ');
                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        public void SendDataNG92_Camera(Point4D p)
        {
            try
            {
                string cmd = NoCamera.ToString() + "92";//命令
                string value = Head
                      + p.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                      + "0".PadLeft(7, ' ') + " "
                      + cmd.PadLeft(7, ' ');
                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        public void SendDataNG93_Camera(Point4D p)
        {
            try
            {
                string cmd = NoCamera.ToString() + "93";//命令
                string value = Head
                      + p.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                      + p.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                      + "0".PadLeft(7, ' ') + " "
                      + cmd.PadLeft(7, ' ');
                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        /// <summary>
        /// 无参数NG报警
        /// </summary>
        /// <param name="noCamera"></param>
        /// <param name="p"></param>
        public void SendDataNG90_Camera()
        {
            try
            {
                SendDataNG90_Camera(new Point4D());
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        public void SendDataNG91_Camera()
        {
            try
            {
                SendDataNG91_Camera(new Point4D());
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("RobotLWriteBase", ex);
            }
        }
        public void SendDataNG92_Camera()
        {
            try
            {
                SendDataNG92_Camera(new Point4D());
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        public void SendDataNG93_Camera()
        {
            try
            {
                SendDataNG93_Camera(new Point4D());
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        #endregion NG
        #endregion 写入结果

        #region 其他
        public void WriteOthers1(Point4D point)
        {
            try
            {
                //数据超限
                if (!BlDataOverFlow(point, 1))
                {
                    return;
                }
                string cmd = "201";//命令

                string value = Head
                    + point.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                    + "0".PadLeft(7, ' ') + " "
                    + cmd.PadLeft(7, ' ');

                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        public void WriteOthers2(Point4D point)
        {
            try
            {
                //数据超限
                if (!BlDataOverFlow(point, 1))
                {
                    return;
                }
                string cmd = "202";//命令

                string value = Head
                    + point.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                    + "0".PadLeft(7, ' ') + " "
                    + cmd.PadLeft(7, ' ');

                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        public void WriteOthers3(Point4D point)
        {
            try
            {
                //数据超限
                if (!BlDataOverFlow(point, 1))
                {
                    return;
                }
                string cmd = "203";//命令

                string value = Head
                    + point.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                    + "0".PadLeft(7, ' ') + " "
                    + cmd.PadLeft(7, ' ');

                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        #endregion 其他

        #region 数据超限判断
        bool BlDataOverFlow(Point4D p, int pos)
        {
            try
            {
                return true;
                int index = NoCamera * ParCameraWork.P_I[NoCamera] + pos;

                if (p.DblValue1 < ParSetRobot.P_I.dataLimitRobot_L[index].DblXMin
                    || p.DblValue1 > ParSetRobot.P_I.dataLimitRobot_L[index].DblXMax

                    || p.DblValue2 < ParSetRobot.P_I.dataLimitRobot_L[index].DblYMin
                    || p.DblValue2 > ParSetRobot.P_I.dataLimitRobot_L[index].DblYMax

                    || p.DblValue3 < ParSetRobot.P_I.dataLimitRobot_L[index].DblRMin
                    || p.DblValue3 > ParSetRobot.P_I.dataLimitRobot_L[index].DblRMax

                    || p.DblValue4 < ParSetRobot.P_I.dataLimitRobot_L[index].DblTMin
                    || p.DblValue4 > ParSetRobot.P_I.dataLimitRobot_L[index].DblTMax
                    )
                {
                    string strStd = ParSetRobot.P_I.dataLimitRobot_L[index].DblXMin.ToString() + ","
                        + ParSetRobot.P_I.dataLimitRobot_L[index].DblXMax.ToString() + ","
                        + ParSetRobot.P_I.dataLimitRobot_L[index].DblYMin.ToString() + ","
                        + ParSetRobot.P_I.dataLimitRobot_L[index].DblYMax.ToString() + ","
                        + ParSetRobot.P_I.dataLimitRobot_L[index].DblRMin.ToString() + ","
                        + ParSetRobot.P_I.dataLimitRobot_L[index].DblRMax.ToString() + ","
                        + ParSetRobot.P_I.dataLimitRobot_L[index].DblTMin.ToString() + ","
                        + ParSetRobot.P_I.dataLimitRobot_L[index].DblTMax.ToString();
                    if (ParSetRobot.P_I.blDataOverflow)
                    {
                        string strError = "Camera" + NoCamera.ToString() + "Pos" + pos.ToString() + ":Error:" + p.strValue + ",Std:" + strStd;
                        DataError_event(strError);
                        LogComInterface.L_I.WriteData("Overflow", strError);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }
        #endregion 数据超限判断       
    }
}
