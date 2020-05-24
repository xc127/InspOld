using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using System.IO;
using BasicClass;

namespace SetPar
{
    partial class ParFolderAttribute
    {
        /// <summary>
        /// 写入文件属性设置参数
        /// </summary>
        /// <returns></returns>
        public bool WriteFolderAttributeIni()
        {
            try
            {
                //删除旧文件
                if (File.Exists(PathFolderAttribute))
                {
                    File.Delete(PathFolderAttribute);
                }

                IniFile.I_I.WriteIni("Camera", "Hidden", FolderAttribute_L[0].BlHidden.ToString(), PathFolderAttribute);
             
                IniFile.I_I.WriteIni("PLC", "Hidden", FolderAttribute_L[1].BlHidden.ToString(), PathFolderAttribute);
               
                IniFile.I_I.WriteIni("Robot", "Hidden", FolderAttribute_L[2].BlHidden.ToString(), PathFolderAttribute);
               
                IniFile.I_I.WriteIni("SysInit", "Hidden", FolderAttribute_L[3].BlHidden.ToString(), PathFolderAttribute);
                
                IniFile.I_I.WriteIni("SetPar", "Hidden", FolderAttribute_L[4].BlHidden.ToString(), PathFolderAttribute);
                
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
    }
}
