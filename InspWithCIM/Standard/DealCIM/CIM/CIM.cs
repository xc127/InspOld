#define TRACE

using BasicClass;
using McmqApi;
using System;
using System.Security.Cryptography;
using System.Threading;

namespace DealCIM
{
    public partial class CIM
    {
        #region 静态类实例
        public static CIM C_I = new CIM();

        /// <summary>
        /// 私有构造函数，避免在外部手动创建实例
        /// </summary>
        private CIM()
        {
            //InitParams();
        }
        #endregion 静态类实例

        #region 定义
        static string ClassName = "CIM";
        const string LogName = "MCMQ";
        /// <summary>
        /// 友达提供的库，接口实例
        /// </summary>
        cMcmqCommApi mcmq = new cMcmqCommApi();
        /// <summary>
        /// Unique ID，每次通信时的唯一ID
        /// </summary>
        string correlationID = string.Empty;
        /// <summary>
        /// 重连次数n，则实际重连n-1次
        /// </summary>
        const int ReconnectCnt = 2;
        object locker = new object();
        #endregion 定义

        #region 连接断开
        /// <summary>
        /// 使用mcmq与服务器进行连接
        /// </summary>
        /// <returns></returns>
        public string Connect()
        {
            try
            {
                Log.L_I.WriteError(LogName, "方法：Connect");
                string strResult = mcmq.connectMCMQ(PostParams.P_I.StrIP, Int16.Parse(PostParams.P_I.StrPort));
                Log.L_I.WriteError(LogName, "Connect Completed:" + strResult);
                return strResult;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
                return "";
            }
        }

        /// <summary>
        /// 断开与服务器的连接
        /// </summary>
        /// <returns></returns>
        public string DisConnect()
        {
            string strResult = string.Empty;
            try
            {
                Log.L_I.WriteError(LogName, "方法：Disconnect");
                strResult = mcmq.disconnect();
                Log.L_I.WriteError(LogName, "Disconnect Completed:" + strResult);
                return strResult;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
                return strResult;
            }
        }

        /// <summary>
        /// 重连
        /// </summary>
        public bool ReConnect()
        {
            try
            {
                lock (locker)
                {
                    return DisConnect() == "0000" && Connect() == "0000";
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
                return false;
            }
        }

        void ReOpenQueue()
        {
            try
            {
                lock(locker)
                {
                    CloseQueue();
                    DisConnect();
                    Connect();
                    OpenQueue();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);                
            }
        }
        #endregion 连接断开

        #region 发送数据
        /// <summary>
        /// 发送数到服务器，并抛出当前通信连接的unique id，用于接收结果时进行校验
        /// </summary>
        /// <param name="strData">需要发送的数据</param>
        /// <param name="key">unique id</param>
        /// <returns>返回值信息，包含return code/error code/exception msg</returns>
        public string WriteData(string strData, out string key)
        {
            lock (locker)
            {
                key = string.Empty;
                try
                {
                    //以防万一，基本不可能抛异常
                    if (!GenerateID(ref key))
                    {
                        return "Generate ID Error";
                    }

                    string strResult = string.Empty;
                    int i = 0;
                    //格式转换
                    byte[] bytes = System.Text.Encoding.Default.GetBytes(strData);

                    do
                    {
                        try
                        {
                            //计数超过1则表明获取反馈结果失败，所以重连之后再读取
                            if (i++ > 0)
                                ReConnect();
#if TRACE
                            Log.L_I.WriteError(LogName, "PutQueue", "sendQueue:" +
                                PostParams.P_I.StrSendQueue + '\n' +
                                " readQueue:" + PostParams.P_I.StrReadQueue + '\n' +
                                " correlationID:" + key + '\n' + strData);
#endif
                            strResult = mcmq.putQueue(PostParams.P_I.StrSendQueue, bytes, PostParams.P_I.StrReadQueue, key);
#if TRACE
                            Log.L_I.WriteError(LogName, "PutQueueReturnValue:" + strResult + '\n' +
                                "WriteQueueHandle:" + mcmq.strQueueHandle);
#endif
                        }
                        catch (Exception ex)
                        {
                            Log.L_I.WriteError(LogName, "PutQueue:" + "\nQueueHandle：" + mcmq.strQueueHandle +
                                "\nCorrelationID：" + mcmq.getCorrelationID + '\n' + ex);
                            Log.L_I.WriteError(ClassName, ex);
                        }
                        //返回结果OK时得到的返回值是0000
                    } while (strResult != "0000" && i < ReconnectCnt);

                    return strResult;
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(ClassName, ex);
                    return string.Empty;
                }
            }
        }
        #endregion 发送数据

        #region 读取数据
        /// <summary>
        /// 打开队列
        /// </summary>
        /// <returns></returns>
        private string OpenQueue()
        {
            string strResult = string.Empty;
            try
            {
                Log.L_I.WriteError(LogName, "方法：OpenQueue");
                strResult = mcmq.openQueue(PostParams.P_I.StrReadQueue, 1000000, 5000, 500000, 2500, 130, false, "TEST_QUEUE");
                Log.L_I.WriteError(LogName, "OpenQueue Completed：" + strResult);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }

            return strResult;
        }

        /// <summary>
        /// 从队列中获取反馈结果
        /// </summary>
        /// <returns></returns>
        private string GetQueue()
        {
            int i = 0;

            string strResult = string.Empty;
            //线程锁
            do
            {
                try
                {
                    if (i++ > 0)
                        ReOpenQueue();
                    //lock (locker)
                    //{
#if TRACE
                        Log.L_I.WriteError(LogName, "方法：GetQueue" + "\nReadQueueHandle:" + mcmq.strQueueHandle);
#endif
                        strResult = mcmq.getQueue(mcmq.strQueueHandle, 20000);
#if TRACE
                        Log.L_I.WriteError(LogName, "GetQueueReturnValue:" + strResult + "\nQueueHandle:" + mcmq.strQueueHandle
                            + "\nCorrelationID:" + mcmq.getCorrelationID);
#endif
                   // }
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(LogName, "GetQueue:" + "\nQueueHandle：" + mcmq.strQueueHandle +
                            "\nCorrelationID：" + mcmq.getCorrelationID + '\n' + ex);
                    Log.L_I.WriteError(ClassName, ex);
                }
            } while (strResult != "0000" && i < ReconnectCnt);
            Log.L_I.WriteError(LogName, "finish get queue");
            return strResult;
        }

        /// <summary>
        /// 关闭队列
        /// </summary>
        /// <returns></returns>
        private string CloseQueue()
        {
            string strResult = string.Empty;
            try
            {
                Log.L_I.WriteError(LogName, "方法：CloseQueue");
                strResult = mcmq.closeQueue(mcmq.strQueueHandle);
                Log.L_I.WriteError(LogName, "CloseQueue:" + strResult);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }

            return strResult;
        }

        /// <summary>
        /// 新版本使用的读取队列中的结果，并且返回读取的数据以及unique id
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="correlationID"></param>
        /// <returns></returns>
        public string ReadData(out string strData, out string correlationID)
        {
            lock (locker)
            {
                strData = "";
                correlationID = string.Empty;
                try
                {
                    //打开队列
                    string strResult = OpenQueue();
                    if (strResult != "0000")
                    {
                        return strResult;
                    }
                    //尝试从队列中获取数据
                    strResult = GetQueue();
                    if (strResult != "0000")
                    {
                        return strResult;
                    }
                    //如果能够读取数据，判断读取的数据是否为空
                    byte[] reply_message = mcmq.getBinMessage;
                    if (!(reply_message.Length > 0))
                        return "BinMsg is Empty";
                    //格式转换，顺便做一次判断，估计没用，加了看看
                    strData = System.Text.Encoding.ASCII.GetString(reply_message);
                    if (!(strData.Length > 0))
                        return "BinMsg to String is Empty";
                    Log.L_I.WriteError(LogName, "BinMsg:\n" + strData);
                    //获取当前信息对应的unique id
                    correlationID = mcmq.getCorrelationID;
                    //读取完之后要清队列
                    mcmq.cleanQueue(mcmq.strQueueHandle);
                    //关闭队列
                    return CloseQueue();
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(ClassName, ex);
                    return "";
                }
            }
        }
        #endregion 读取数据

        /// <summary>
        /// 生成随机的correlationID
        /// </summary>
        /// <returns></returns>
        private bool GenerateID(ref string id)
        {
            try
            {
                byte[] randomBytes = new byte[4];
                RNGCryptoServiceProvider rngServiceProvider = new RNGCryptoServiceProvider();
                rngServiceProvider.GetBytes(randomBytes);
                id = BitConverter.ToInt32(randomBytes, 0).ToString();
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
                return false;
            }
        }
    }
}
