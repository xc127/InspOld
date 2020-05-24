using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using BasicClass;

namespace SetPar
{
    public partial class ParFolderAttribute : BaseClass
    {
        #region 静态类实例
        public static ParFolderAttribute V_I = new ParFolderAttribute();
        #endregion 静态类实例

        #region 定义
        #region Path
        /// <summary>
        /// 保存路径
        /// </summary>
        public string PathFolderAttribute
        {
            get
            {
                return ComValue.c_PathSetPar + "FolderAttribute.ini";
            }
        }
        #endregion Path

        #region 相关文件类
        /// <summary>
        /// 相机参数
        /// </summary>
        FolderAttribute FolderAttribute_Camera = new FolderAttribute();       

        /// <summary>
        /// PLC参数
        /// </summary>
        FolderAttribute FolderAttribute_PLC = new FolderAttribute();       

        /// <summary>
        /// 机器人参数
        /// </summary>
        FolderAttribute FolderAttribute_Robot = new FolderAttribute();
       

        /// <summary>
        /// SysInit参数
        /// </summary>
        FolderAttribute FolderAttribute_SysInit = new FolderAttribute();
      

        /// <summary>
        /// SetPar参数
        /// </summary>
        FolderAttribute FolderAttribute_SetPar = new FolderAttribute();
       
        #endregion 相关文件类

        //List
        public List<FolderAttribute> FolderAttribute_L = new List<FolderAttribute>();//文件的相关属性
        #endregion 定义

        #region 初始化
        ParFolderAttribute()
        {
            Init();

            NameClass = "ParFolderAttribute";
        }


        void Init()
        {
            try
            {
                FolderAttribute_Camera.Name = "相机参数";
                FolderAttribute_Camera.Path = ComValue.c_PathCamera;

                FolderAttribute_PLC.Name = "PLC参数";
                FolderAttribute_PLC.Path = ComValue.c_PathPLC;

                FolderAttribute_Robot.Name = "机器人参数";
                FolderAttribute_Robot.Path = ComValue.c_PathRobot;

                FolderAttribute_SysInit.Name = "SysInit参数";
                FolderAttribute_SysInit.Path = ComValue.c_PathSysInit;

                FolderAttribute_SetPar.Name = "SetPar参数";
                FolderAttribute_SetPar.Path = ComValue.c_PathSetPar;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化
    }

    public class FolderAttribute
    {
        public int No { set; get; }//序号
        public string Name { set; get; }//名称
        public string Path { set; get; }//路径
        public bool BlHidden { set; get; }//隐藏
    }
}
