using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealAlgorithm;
using DealResult;
using DealFile;
using Common;
using System.Xml;
using BasicClass;
using DealResult;
using BasicComprehensive;

namespace DealPLC
{
    [Serializable]
    public class ParCommWritePLC : BaseParPLC
    {
        #region 定义
        const int C_Num = 30;

        public ParGetResultFromCell g_ParGetResultFromCell = new ParGetResultFromCell();//从其他单元获取结果

        public List<ResultforWritePLC> ResultforWritePLC_L = new List<ResultforWritePLC>();

        //索引器
        public ResultforWritePLC this[int index]
        {
            get
            {
                return ResultforWritePLC_L[index];
            }
            set
            {
                ResultforWritePLC_L[index] = value;
            }
        }
        #endregion 定义

        #region 初始化
        public ParCommWritePLC()
        {
            NameClass = "ParCommWritePLC";
        }
        #endregion 初始化


        #region 读Xml
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="xDoc">Stream文档</param>
        /// <returns></returns>  
        public override bool ReadXmlRoot(XmlDocument xDoc)
        {
            try
            {
                int numError = 0;
                XmlElement xeRoot = ReadNode(xDoc, "CommWritePLC");

                //读取子元素参数
                if (!ReadXmlChildInit(xeRoot))
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

        /// <summary>
        /// 从本地文档读取参数的时候，读取
        /// </summary>
        /// <param name="xeRoot">根节点</param>
        /// <returns>是否成功</returns>
        public override bool ReadXmlRoot(XmlElement xeRoot)
        {
            int numError = 0;
            try
            {
                XmlElement xeCommWritePLC = ReadNode(xeRoot, "CommWritePLC");
                //调用基类中的方法，加载基础参数
                if (!ReadXmlChild(xeCommWritePLC))
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

        /// <summary>
        /// 读取该算法的参数
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <returns></returns>
        public override bool ReadXmlPar(XmlElement xeRoot)
        {
            try
            {
                XmlElement xePar = ReadNode(xeRoot, "Par");
                XmlElement xeResultforWritePLC = ReadNode(xePar, "ResultforWritePLC");

                for (int i = 0; i < C_Num; i++)
                {
                    XmlElement xeResult = ReadNode(xeResultforWritePLC, "ResultforWritePLC" + i.ToString());
                    if (xeResult == null)
                    {
                        ResultforWritePLC_L.Add(new ResultforWritePLC()
                       {
                           No = i,
                       });
                        continue;
                    }
                    string nameCell = ReadAttributeStr(xeResult, "NameCell");
                    string nameResult = ReadAttributeStr(xeResult, "NameResult");
                    bool blSelect = ReadAttributeBl(xeResult, "BlSelect");
                    string nameReg = ReadAttributeStr(xeResult, "NameReg");
                    ResultforWritePLC_L.Add(new ResultforWritePLC()
                        {
                            No = i,
                            NameCell = nameCell,
                            NameResult = nameResult,
                            BlSelect = blSelect,
                            Reg = nameReg,
                        });
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 读Xml

        #region 写Xml
        /// <summary>
        /// 从Stream资源读取完整节点后，写入参数
        /// </summary>
        /// <param name="xDoc"></param>
        /// <returns></returns>
        public override XmlElement WriteXmlRoot(XmlDocument xDoc)
        {
            try
            {
                //加载标准模板
                XmlElement xeRoot = xDoc.CreateElement("CommWritePLC");
                if (WriteXmlChild(xeRoot))
                {
                    return xeRoot;
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }

        /// <summary>
        /// 从Stream资源读取完整节点后，写入参数
        /// </summary>
        /// <param name="xDoc"></param>
        /// <returns></returns>
        public override bool WriteXmlRoot(XmlElement xeRoot)
        {
            try
            {
                int numError = 0;
                //加载标准模板
                XmlElement xeCommWritePLC = CreateNewXe(xeRoot, "CommWritePLC");
                if (!WriteXmlChild(xeCommWritePLC))
                {
                    numError++;
                }
                xeRoot.AppendChild(xeCommWritePLC);
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

        //写入Xml
        public override bool WriteXmlPar(XmlElement xeRoot)
        {
            int numError = 0;
            try
            {
                XmlElement xePar = CreateNewXe(xeRoot, "Par");
                XmlElement xeResultforWritePLC = CreateNewXe(xePar, "ResultforWritePLC");
                xePar.AppendChild(xeResultforWritePLC);

                for (int i = 0; i < C_Num; i++)
                {
                    if (ResultforWritePLC_L[i].Name != ".")
                    {
                        XmlElement xeResult = CreateNewXe(xePar, "ResultforWritePLC" + i.ToString());
                        if (!WriteAttribute(xeResult, "NameCell", ResultforWritePLC_L[i].NameCell))
                        {
                            numError++;
                        }

                        if (!WriteAttribute(xeResult, "NameResult", ResultforWritePLC_L[i].NameResult))
                        {
                            numError++;
                        }
                        if (!WriteAttribute(xeResult, "NameReg", ResultforWritePLC_L[i].NameReg))
                        {
                            numError++;
                        }
                        if (!WriteAttribute(xeResult, "BlSelect", ResultforWritePLC_L[i].BlSelect.ToString()))
                        {
                            numError++;
                        }
                        xeResultforWritePLC.AppendChild(xeResult);
                    }
                }
                xeRoot.AppendChild(xePar);
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
        #endregion 写Xml
    }

    [Serializable]
    public class ResultforWritePLC : BaseParPLC
    {
        #region 初始化
        public ResultforWritePLC(int no)
        {
            No = no;
        }

        public ResultforWritePLC()
        {
            
        }
        #endregion 初始化

        public string Name
        {
            get
            {
                return NameCell + "." + NameResult;
            }
        }
        public string NameCell { get; set; }//名称
        public string NameResult { get; set; }//名称
        public double Result { set; get; }
        public bool BlSelect { set; get; }

        public string NameReg
        {
            get
            {
                return Reg + "" + TypeReg;
            }
        }
        public string Reg { get; set; } //寄存器名称 
        public string TypeReg { get; set; } //寄存器名称 

        public double DblMax { get; set; } //最大值
        public double DblMin { get; set; } //最小值
        public double Co { get; set; } //换算系数
    }
}
