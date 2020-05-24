using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealFile;
using System.IO;
using BasicClass;


namespace DealConfigFile
{
    partial class ParConfigPar
    {
        #region 定义
        IniFile I_I = new IniFile();
        #endregion 定义

        #region 读取配置参数
        /// <summary>
        /// 读取产品参数
        /// </summary>
        /// <returns></returns>
        public bool ReadIniConfigPar()
        {
            int numError = 0;
            try
            {               
                //读取产品参数
                if (!ReadIniParProduct())
                {
                    numError++;
                }
                //读取相机拍照位置
                if (!ReadIniPosPhoto())
                {
                    numError++;
                }
                if (numError > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 读取配置参数

        #region 读取产品参数
        /// <summary>
        /// 从本地文档中，读取产品参数
        /// </summary>
        /// <returns></returns>
        public bool ReadIniParProduct()
        {
            bool blRight = true;
            try
            {                
                //配置文件名称,产品参数，是首先需要读出的
                ComConfigPar.C_I.NameModel = I_I.ReadIniStr("Model", "Name", ComConfigPar.C_I.PathConfigIni);
                //配置文件序号
                this.No = I_I.ReadIniInt("Model", "No", ComConfigPar.C_I.PathConfigIni);
               
                //除去配置文件名称，和序号
                ParProduct_L.Clear();
                for (int i = 0; i < c_NumParProduct; i++)
                {
                    double value = I_I.ReadIniDbl("ParProduct" + i.ToString(), "Value", ComConfigPar.C_I.PathConfigIni);
                    double min = I_I.ReadIniDbl("ParProduct" + i.ToString(), "Min", ComConfigPar.C_I.PathConfigIni);//最小值

                    double max = I_I.ReadIniDbl("ParProduct" + i.ToString(), "Max", ComConfigPar.C_I.PathConfigIni);//最大值
                    double co = I_I.ReadIniDbl("ParProduct" + i.ToString(), "Co", ComConfigPar.C_I.PathConfigIni);//系数
                    string Annotation = I_I.ReadIniStr("ParProduct" + i.ToString(), "Annotation", ComConfigPar.C_I.PathConfigIni);//注释

                    ParProduct_L.Add(new ParProduct()
                    {
                        No = i,
                        DblValue = value,
                        DblMin = min,
                        DblMax = max,
                        Co = co,
                        Annotation = Annotation
                    });
                }

                //如果不存在配置参数，则先写入
                if (!File.Exists(ComConfigPar.C_I.PathConfigIni))
                {
                    WriteIniParProduct();
                }
            }
            catch (Exception ex)
            {
                blRight = false;
                //ConfigParError_Event();
                Log.L_I.WriteError(NameClass, ex);
            }
            return blRight;
        }
        #endregion 读取产品参数

        #region 读取拍照位置
        /// <summary>
        /// 读取拍照位置
        /// </summary>
        /// <returns></returns>
        public bool ReadIniPosPhoto()
        {
            try
            {
                int num = 0;
                for (int i = 0; i < ParCameraWork.NumCamera; i++)
                {
                    for (int j = 0; j < ParCameraWork.P_I[i + 1]; j++)
                    {
                        string section = "Camera" + (i + 1).ToString() + "Pos" + (j + 1).ToString();

                        //X
                        double x = I_I.ReadIniDbl(section, "X", ComConfigPar.C_I.PathConfigIni);

                        double y = I_I.ReadIniDbl(section, "Y", ComConfigPar.C_I.PathConfigIni);

                        double z = I_I.ReadIniDbl(section, "Z", ComConfigPar.C_I.PathConfigIni);

                        string Annotation = "工位" + (i + 1).ToString() + "拍照位置" + (j + 1).ToString();

                        PosPhoto_L.Add(new PosPhoto()
                        {
                            No = num,
                            X = Convert.ToDouble(x),
                            Y = Convert.ToDouble(y),
                            Z = Convert.ToDouble(z),
                            Pos=j+1,
                            NoCamera = i + 1,
                            Annotation = Annotation
                        });
                        num++;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 读取拍照位置

        #region 读取PLC配置参数注释
        public void ReadPLCAnnotation()
        {
            try
            {
                string path = ParPathRoot.PathRoot+"Store\\PLC\\RegConfigPar.ini";
                for (int i = 10; i < 50; i++)
                {
                    string annotation = I_I.ReadIniStr(i.ToString(), "Annotation", path);
                    double min = I_I.ReadIniDbl(i.ToString(), "Min", path);
                    double max = I_I.ReadIniDbl(i.ToString(), "Max", path);
                    ParProduct_L[i - 10].Annotation = annotation;
                    ParProduct_L[i - 10].DblMin = min;
                    ParProduct_L[i - 10].DblMax = max;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 读取PLC配置参数注释
    }
}
