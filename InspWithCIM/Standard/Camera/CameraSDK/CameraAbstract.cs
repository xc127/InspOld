using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using BasicClass;

namespace Camera
{
    //定义一个相机类虚基类
    public abstract class CameraAbstract
    {
        #region 定义
        //类名称
        public abstract string NameClass { get; set; }

        public abstract TriggerSourceCamera_enum TriggerSource_e { get; }

        public event Action Trigger_event;//触发模式事件
        #endregion 定义

        /// <summary>
        /// 打开相机,并传入相机设置参数
        //public abstract bool OpenCamera(string SerialNumber, ParCameraSetting parCameraSetting);

        public abstract bool OpenCamera(BaseParCamera baseParCamera);

        public abstract bool TriggerSoftware();
        public abstract bool TriggerSoftware(bool blBitmap);

        public abstract bool SetTriggerDelay(double delay);

        //抓取相机的图像
        public abstract ImageAll GrabImageAll();
        public abstract ImageAll GrabImageHal();

        //调节相机
        public abstract bool SetExposure(double dblValue); //曝光       

        //增益
        public abstract bool SetGain(double dblValue);

        //保存参数到通道
        public abstract bool SaveParChannel();      

        //关闭相机
        public abstract bool CloseCamera();

        //设置软触发
        public abstract bool SetTrriggerMode();
          
    }
    /// <summary>
    /// 相机配置
    /// </summary>
    public class ParCameraSetting
    {
        public bool BlSoftTrriger { set; get; }//是否处于软触发
        public TriggerSourceCamera_enum TriggerSource_e = TriggerSourceCamera_enum.Software;
    }
}
