using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BasicClass;

namespace SetPar
{
    public class FunHarDisk : BaseClass
    {
        #region 静态类实例
        public static FunHarDisk F_I = new FunHarDisk();
        #endregion 静态类实例

        #region 初始化
        public FunHarDisk()
        {
            NameClass = "FunHarDisk";
        }
        #endregion 初始化

        /// <summary>
        /// 获取存储空间
        /// </summary>
        /// <param name="drive"></param>
        /// <returns></returns>
        public double[] GetSpace(string drive)
        {
            double[] size = new double[2];
            try
            {
                DriveInfo[] dirve = DriveInfo.GetDrives();
                foreach (DriveInfo item in dirve)
                {
                    if (item.Name == drive)
                    {
                        size[0] = Math.Round(item.TotalSize / 1024.0 / 1024.0 / 1024.0, 2);
                        size[1] = Math.Round(item.TotalFreeSpace / 1024.0 / 1024.0 / 1024.0, 2);
                    }
                }
                return size;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return size;
            }
        }
    }
}
