using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace DealPLC
{
    partial class LogicPLC
    {
        #region 定义
        string IP = "127.0.0.1";
        int Port = 8080;

        TcpClient g_tcpClientRead = null;
        BinaryReader g_BRRead = null;
        BinaryWriter g_BWWrite = null;

        bool g_BlCycRead = false;

        Hashtable g_HtPLCData = new Hashtable();

        Mutex Mt_Read = new Mutex();
        Mutex Mt_Write = new Mutex();

        public event Action DisConnectAnnotherPLC_event; 
        #endregion 定义


        /// <summary>
        /// 打开客户端口
        /// </summary>
        bool OpenClient()
        {
            try
            {
                g_tcpClientRead = new TcpClient();
                g_tcpClientRead.Connect(IP, Port);

                if (g_tcpClientRead != null)
                {
                    NetworkStream networkStream = g_tcpClientRead.GetStream();
                    g_BRRead = new BinaryReader(networkStream);
                    g_BWWrite = new BinaryWriter(networkStream);
                }

                g_BlCycRead = true;
                new Task(new Action(ReadData)).Start();
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }


        #region 监控Finevision指令
        public void ReadData()
        {
            try
            {
                string data = "";
                while (g_BlCycRead)
                {
                    Thread.Sleep(2);
                    if (g_BRRead == null)
                    {
                        return;
                    }

                    string str = g_BRRead.ReadString();
                    data += str;

                    if (data.Contains("#"))
                    {
                        string[] part = data.Split('#');

                        //对分割得字符进行处理
                        SplitData(part[0]);

                        data = "";
                        for (int i = 1; i < part.Length; i++)
                        {
                            data += part[i];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //DisConnectAnnotherPLC_event();
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void SplitData(string data)
        {
            try
            {
                string[] info = data.Replace("#", "").Split('*');
                string reg = info[0];
                int num = int.Parse(info[1]);
                string time = info[2];
                string[] allData = info[3].Split(',');

                int[] value = new int[allData.Length];

                for (int i = 0; i < allData.Length - 1; i++)
                {
                    value[i] = int.Parse(allData[i]);
                }
                AddPLCData(time, value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 将结果添加到哈希表
        /// </summary>
        void AddPLCData(string key, int[] value)
        {
            try
            {
                if (g_HtPLCData.Contains(key))
                {
                    g_HtPLCData.Remove(key);
                }
                g_HtPLCData.Add(key,value);
                
            }
            catch (Exception ex)
            {
                 Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 监控Finevision指令

        #region 读数据
        bool ReadData(ComDataPLC data,out int value)
        {
            value = 0;
            try
            {
                if (WriteData(data))
                {
                    //记录日志
                    if (ParSetPLC.P_I.BlAnnotherPLCLog)
                    {
                        LogPLC.L_I.WritePLC("PLCWrite_FineVision", data.NameFun, data.Reg.Replace("\n", ",").PadLeft(7, ' '), data.Value.ToString(), "0");
                    }

                    string key = data.Reg + data.Time;
                    bool blContains = false;
                    if (g_HtPLCData.Contains(key))
                    {
                        blContains = true;
                    }

                    int i = 0;
                    while (!blContains)
                    {
                        Thread.Sleep(10);
                        i++;
                        if (i > 100)
                        {
                            return false;
                        }
                        if (g_HtPLCData.Contains(key))
                        {
                            blContains = true;
                            value = (int)g_HtPLCData[key];

                            //记录日志
                            if (ParSetPLC.P_I.BlAnnotherPLCLog)
                            {
                                LogPLC.L_I.WritePLC("PLCWrite_FineVision", data.NameFun, "", value.ToString(), "0");
                            }
                            return true;
                        }
                        else
                        {

                        }
                    }
                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        bool ReadData(ComDataPLC data, out int[] value)
        {
            value = null;
            try
            {
                if (WriteData(data))
                {
                    //记录日志
                    if (ParSetPLC.P_I.BlAnnotherPLCLog)
                    {
                        LogPLC.L_I.WritePLC("PLCReadData_FineVision", data.NameFun, data.Reg.Replace("\n", ",").PadLeft(7, ' '), data.Value.ToString(), "0");
                    }

                    string key = data.Time;
                   
                    if (g_HtPLCData.Contains(key))
                    {
                        value = (int[])g_HtPLCData[key];
                        g_HtPLCData.Remove(key);
                        return true;                      
                    }

                    int i = 0;
                    while (!g_HtPLCData.Contains(key))
                    {
                        Thread.Sleep(10);
                        i++;
                        if (i > 50)
                        {
                            return false;
                        }                       
                    }
                    if (g_HtPLCData.Contains(key))
                    {
                        value = (int[])g_HtPLCData[key];
                        g_HtPLCData.Remove(key);

                        string strValue = "";
                        for (int j = 0; j < value.Length; j++)
                        {
                            strValue += value[j].ToString();
                        }

                        //记录日志
                        if (ParSetPLC.P_I.BlAnnotherPLCLog)
                        {
                            LogPLC.L_I.WritePLC("PLCReadData_FineVision", data.NameFun, "", strValue, "0");
                        }
                        return true;
                    }
                    else
                    {
                        Log.L_I.WriteError(NameClass, "接收到结果得HtResult有未读取得PLC值");
                    }
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 读数据

        #region 写数据
        bool WriteData(ComDataPLC data)
        {
            try
            {
                bool blResult = WriteData(data.AllData);
                if (ParSetPLC.P_I.BlAnnotherPLCLog)
                {
                    LogPLC.L_I.WritePLC("PLCWriteData_FineVision", data.NameFun, data.Reg.Replace("\n", ",").PadLeft(7, ' '), data.Value.ToString(), "0");
                }
                return blResult;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        public bool WriteData(string data)
        {
            Mt_Write.WaitOne();
            try
            {
                if (g_BWWrite == null)
                {
                    return false;
                }
                byte[] byte_arr = Encoding.Default.GetBytes(data);
                g_BWWrite.Write(data);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                Mt_Write.ReleaseMutex();
            }
        }
        #endregion 写数据

        #region 关闭
        public void CloseClient()
        {
            try
            {
                if (!ParSetPLC.P_I.BlAnnotherPLC)
                {
                    return;
                }
                if (g_tcpClientRead != null)
                {
                    g_tcpClientRead.Close();
                }
                if (g_BRRead != null)
                {
                    g_BRRead.Close();
                }
                if (g_BWWrite != null)
                {
                    g_BWWrite.Close();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 关闭
    }

    public class ComDataPLC
    {
        public FunPLC_enum FunPLC_e = FunPLC_enum.ReadReg;//PLC本身的函数
        public string Reg = "";
        public string Num = "";
        public string Time = "";
        public string TimeFeedBack = "";
        public string NameFun = "";//调用的函数名称
        public string Value = "";

        public string AllData
        {
            get
            {
                string data = "";
                data = FunPLC_e.ToString() + "*" + Reg + "*" + Num + "*" + Time + "*" + NameFun + "*" + Value + "#";
                return data;
            }
        }

        #region 初始化
        public ComDataPLC(FunPLC_enum funPLC_e, string reg, int num, string nameFun, int[] value)
        {
            try
            {
                FunPLC_e = funPLC_e;
                Reg = reg;
                Num = num.ToString();
                NameFun = nameFun;

                Time = DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + DateTime.Now.Millisecond.ToString();
                for (int i = 0; i < value.Length; i++)
                {
                    Value += value[i].ToString() + ",";
                }
            }
            catch (Exception ex)
            {

            }
        }

        public ComDataPLC(FunPLC_enum funPLC_e, string reg, int num, string nameFun, int value)
        {
            try
            {
                FunPLC_e = funPLC_e;
                Reg = reg;
                Num = num.ToString();
                NameFun = nameFun;

                Time = DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + DateTime.Now.Millisecond.ToString();
                Value = value.ToString();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 初始化
    }

    public enum FunPLC_enum
    {
        ReadReg,
        ReadBlockReg,
        ReadBlockReg_Continue,

        WriteReg,
        WriteBlockReg,
        WriteBlockReg_Long,
    }
}
