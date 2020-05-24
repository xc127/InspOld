using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using BasicClass;

namespace SetPar
{
    partial class ParFolderAttribute
    {
        /// <summary>
        /// 读取文件属性设置参数
        /// </summary>
        public void ReadFolderAttributeIni()
        {
            try
            {
                //相机参数
                base.StrBool = IniFile.I_I.ReadIniStr("Camera", "Hidden", PathFolderAttribute);
                FolderAttribute_Camera.BlHidden = Convert.ToBoolean(base.StrBool);
             
                //PLC参数
                base.StrBool = IniFile.I_I.ReadIniStr("PLC", "Hidden", PathFolderAttribute);
                FolderAttribute_PLC.BlHidden = Convert.ToBoolean(base.StrBool);
              
                //机器人参数
                base.StrBool = IniFile.I_I.ReadIniStr("Robot", "Hidden", PathFolderAttribute);
                FolderAttribute_Robot.BlHidden = Convert.ToBoolean(base.StrBool);
                
                //SysInit参数
                base.StrBool = IniFile.I_I.ReadIniStr("SysInit", "Hidden", PathFolderAttribute);
                FolderAttribute_SysInit.BlHidden = Convert.ToBoolean(base.StrBool);
                

                //SetPar参数
                base.StrBool = IniFile.I_I.ReadIniStr("SetPar", "Hidden", PathFolderAttribute);
                FolderAttribute_SetPar.BlHidden = Convert.ToBoolean(base.StrBool);             

                FolderAttribute_L.Add(FolderAttribute_Camera);
                FolderAttribute_L.Add(FolderAttribute_PLC);
                FolderAttribute_L.Add(FolderAttribute_Robot);
                FolderAttribute_L.Add(FolderAttribute_SysInit);
                FolderAttribute_L.Add(FolderAttribute_SetPar);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
    }
       
}
