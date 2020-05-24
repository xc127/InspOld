using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealFile;
using BasicClass;
using System.Diagnostics;

namespace DealConfigFile
{
    public partial class ParConfigPar
    {
        #region 保存配置参数
        /// <summary>
        /// 将产品参数保存到ini
        /// </summary>
        /// <returns></returns>
        public bool WriteConfigIni()
        {
            try
            {
                int numError = 0;
                //产品参数
                if (!WriteIniParProduct())
                {
                    numError++;
                }
                //拍照位置
                if (!WriteIniPosPhoto())
                {
                    numError++;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion 保存配置参数

        #region 保存产品参数
        /// <summary>
        /// 保存产品参数到Ini
        /// </summary>
        /// <returns></returns>
        public bool WriteIniParProduct()
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //配置文件名称
                I_I.WriteIni("Model", "Name", ComConfigPar.C_I.NameModel, ComConfigPar.C_I.PathConfigIni);
                //配置文件序号
                I_I.WriteIni("Model", "No", this.No.ToString(), ComConfigPar.C_I.PathConfigIni);
                //配置参数
                for (int i = 0; i < c_NumParProduct; i++)
                {
                    I_I.WriteIni("ParProduct" + i.ToString(), "Value", this.ParProduct_L[i].DblValue.ToString(), ComConfigPar.C_I.PathConfigIni);//写入值
                    I_I.WriteIni("ParProduct" + i.ToString(), "Min", this.ParProduct_L[i].DblMin.ToString(), ComConfigPar.C_I.PathConfigIni);//写入最小值
                    I_I.WriteIni("ParProduct" + i.ToString(), "Max", this.ParProduct_L[i].DblMax.ToString(), ComConfigPar.C_I.PathConfigIni);//写入最大值
                    I_I.WriteIni("ParProduct" + i.ToString(), "Co", this.ParProduct_L[i].Co.ToString(), ComConfigPar.C_I.PathConfigIni);//写入系数
                    I_I.WriteIni("ParProduct" + i.ToString(), "Annotation", this.ParProduct_L[i].Annotation.ToString(), ComConfigPar.C_I.PathConfigIni);//写入注释
                }
                sw.Stop();
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 保存产品参数

        #region 保存拍照位置
        /// <summary>
        /// 保存拍照位置参数到ini
        /// </summary>
        /// <returns></returns>
        public bool WriteIniPosPhoto()
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                int num = 0;
                for (int i = 0; i < ParCameraWork.NumCamera; i++)
                {
                    for (int j = 0; j < ParCameraWork.P_I[i + 1]; j++)
                    {
                        string section = "Camera" + (i + 1).ToString() + "Pos" + (j + 1).ToString();

                        I_I.WriteIni(section, "X", PosPhoto_L[num].X.ToString(), ComConfigPar.C_I.PathConfigIni);
                        I_I.WriteIni(section, "Y", PosPhoto_L[num].Y.ToString(), ComConfigPar.C_I.PathConfigIni);

                        num++;
                    }
                }
                sw.Stop();
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 保存拍照位置
    }
}
