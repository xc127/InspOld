using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using DealLog;

namespace Main
{
    public partial class ParInspection
    {
        public void WriteIni(int index)
        {
            IniFile.I_I.WriteIni(NameCam, "ThShellX_" + index, ThShellX, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThShellY_" + index, ThShellY, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThCornerX_" + index, ThCornerX, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThCornerY_" + index, ThCornerY, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThPreiectionX_" + index, ThPreiectionX, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThPreiectionY_" + index, ThPreiectionY, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThCrackX_" + index, ThCrackX, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThCrackY_" + index, ThCrackY, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThresholdGlass_" + index, ThresholdGlass, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThresholdEdge_" + index, ThresholdEdge, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SmoothWidth_" + index, SmoothWidth, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SmoothHeight_" + index, SmoothHeight, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "DblOutRate_" + index, DblOutRate, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThITO_" + index, ThITO, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "Amp_" + index, Amp, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "ThRectangleularity_" + index, ThRectangleularity, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "BlIgnoreThisSideFault_" + index, BlIgnoreThisSideFault, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "BlInspCF_" + index, BlInspCF, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "UsingORToCorner_" + index, UsingORToCorner, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "IsCrackEnabled_" + index, IsCrackEnabled, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "StrSidesMatch_" + index, StrSidesMatch, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "IntNumDefCornerWhenStart_" + index, IntNumDefCornerWhenStart, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "IntNumDefCornerWhenEnd_" + index, IntNumDefCornerWhenEnd, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "JudgeMentMark_" + index, JudgeMentMark, BasePathIni);

            IniFile.I_I.WriteIni(NameCam, "SplStartIndex1_" + index, SplStartIndex1, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplEndIndex1_" + index, SplEndIndex1, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplStartIndex2_" + index, SplStartIndex2, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplEndIndex2_" + index, SplEndIndex2, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplStartIndex3_" + index, SplStartIndex3, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplEndIndex3_" + index, SplEndIndex3, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplStartIndex4_" + index, SplStartIndex4, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplEndIndex4_" + index, SplEndIndex4, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplStartIndex5_" + index, SplStartIndex5, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplEndIndex5_" + index, SplEndIndex5, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplStartIndex6_" + index, SplStartIndex6, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplEndIndex6_" + index, SplEndIndex6, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplStartIndex7_" + index, SplStartIndex7, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplEndIndex7_" + index, SplEndIndex7, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplStartIndex8_" + index, SplStartIndex8, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplEndIndex8_" + index, SplEndIndex8, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplStartIndex9_" + index, SplStartIndex9, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplEndIndex9_" + index, SplEndIndex9, BasePathIni);

            IniFile.I_I.WriteIni(NameCam, "SplThShellY1_" + index, SplThShellY1, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellX1_" + index, SplThShellX1, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellY2_" + index, SplThShellY2, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellX2_" + index, SplThShellX2, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellY3_" + index, SplThShellY3, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellX3_" + index, SplThShellX3, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellY4_" + index, SplThShellY4, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellX4_" + index, SplThShellX4, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellY5_" + index, SplThShellY5, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellX5_" + index, SplThShellX5, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellY6_" + index, SplThShellY6, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellX6_" + index, SplThShellX6, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellY7_" + index, SplThShellY7, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellX7_" + index, SplThShellX7, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellY8_" + index, SplThShellY8, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellX8_" + index, SplThShellX8, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellY9_" + index, SplThShellY9, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThShellX9_" + index, SplThShellX9, BasePathIni);

            IniFile.I_I.WriteIni(NameCam, "SplThPerY1_" + index, SplThPerY1, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThPerY2_" + index, SplThPerY2, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThPerY3_" + index, SplThPerY3, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThPerY4_" + index, SplThPerY4, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThPerY5_" + index, SplThPerY5, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThPerY6_" + index, SplThPerY6, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThPerY7_" + index, SplThPerY7, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThPerY8_" + index, SplThPerY8, BasePathIni);
            IniFile.I_I.WriteIni(NameCam, "SplThPerY9_" + index, SplThPerY9, BasePathIni);

        }

        public void WriteIni_Sample(int index, string path)
        {
            IniFile.I_I.WriteIni(NameCam, "ThShellX_" + index, ThShellX, path);
            IniFile.I_I.WriteIni(NameCam, "ThShellY_" + index, ThShellY, path);
            IniFile.I_I.WriteIni(NameCam, "ThCornerX_" + index, ThCornerX, path);
            IniFile.I_I.WriteIni(NameCam, "ThCornerY_" + index, ThCornerY, path);
            IniFile.I_I.WriteIni(NameCam, "ThPreiectionX_" + index, ThPreiectionX, path);
            IniFile.I_I.WriteIni(NameCam, "ThPreiectionY_" + index, ThPreiectionY, path);
            IniFile.I_I.WriteIni(NameCam, "ThCrackX_" + index, ThCrackX, path);
            IniFile.I_I.WriteIni(NameCam, "ThCrackY_" + index, ThCrackY, path);
            IniFile.I_I.WriteIni(NameCam, "ThresholdGlass_" + index, ThresholdGlass, path);
            IniFile.I_I.WriteIni(NameCam, "ThresholdEdge_" + index, ThresholdEdge, path);
            IniFile.I_I.WriteIni(NameCam, "SmoothWidth_" + index, SmoothWidth, path);
            IniFile.I_I.WriteIni(NameCam, "SmoothHeight_" + index, SmoothHeight, path);
            IniFile.I_I.WriteIni(NameCam, "DblOutRate_" + index, DblOutRate, path);
            IniFile.I_I.WriteIni(NameCam, "ThITO_" + index, ThITO, path);
            IniFile.I_I.WriteIni(NameCam, "Amp_" + index, Amp, path);
            IniFile.I_I.WriteIni(NameCam, "ThRectangleularity_" + index, ThRectangleularity, path);
            IniFile.I_I.WriteIni(NameCam, "BlIgnoreThisSideFault_" + index, BlIgnoreThisSideFault, path);
            IniFile.I_I.WriteIni(NameCam, "BlInspCF_" + index, BlInspCF, path);
            IniFile.I_I.WriteIni(NameCam, "UsingORToCorner_" + index, UsingORToCorner, path);
            IniFile.I_I.WriteIni(NameCam, "IsCrackEnabled_" + index, IsCrackEnabled, path);
            IniFile.I_I.WriteIni(NameCam, "StrSidesMatch_" + index, StrSidesMatch, path);
            IniFile.I_I.WriteIni(NameCam, "IntNumDefCornerWhenStart_" + index, IntNumDefCornerWhenStart, path);
            IniFile.I_I.WriteIni(NameCam, "IntNumDefCornerWhenEnd_" + index, IntNumDefCornerWhenEnd, path);
            IniFile.I_I.WriteIni(NameCam, "JudgeMentMark_" + index, JudgeMentMark, path);
        }

        public void ReadIni(int index)
        {
            try
            {
                ThShellX = IniFile.I_I.ReadIniDbl(NameCam, "ThShellX_" + index, 0.12, BasePathIni);
                ThShellY = IniFile.I_I.ReadIniDbl(NameCam, "ThShellY_" + index, 3, BasePathIni);
                ThCornerX = IniFile.I_I.ReadIniDbl(NameCam, "ThCornerX_" + index, 0.5, BasePathIni);
                ThCornerY = IniFile.I_I.ReadIniDbl(NameCam, "ThCornerY_" + index, 0.5, BasePathIni);
                ThPreiectionX = IniFile.I_I.ReadIniDbl(NameCam, "ThPreiectionX_" + index, 0.5, BasePathIni);
                ThPreiectionY = IniFile.I_I.ReadIniDbl(NameCam, "ThPreiectionY_" + index, 0.5, BasePathIni);
                ThCrackX = IniFile.I_I.ReadIniDbl(NameCam, "ThCrackX_" + index, 0.5, BasePathIni);
                ThCrackY = IniFile.I_I.ReadIniDbl(NameCam, "ThCrackY_" + index, 0.5, BasePathIni);
                ThresholdGlass = IniFile.I_I.ReadIniInt(NameCam, "ThresholdGlass_" + index, 35, BasePathIni);

                ThresholdEdge = IniFile.I_I.ReadIniInt(NameCam, "ThresholdEdge_" + index, 40, BasePathIni);

                SmoothWidth = IniFile.I_I.ReadIniDbl(NameCam, "SmoothWidth_" + index, 5, BasePathIni);
                SmoothHeight = IniFile.I_I.ReadIniDbl(NameCam, "SmoothHeight_" + index, 5, BasePathIni);
                DblOutRate = IniFile.I_I.ReadIniDbl(NameCam, "DblOutRate_" + index, 5, BasePathIni);
                ThITO = IniFile.I_I.ReadIniInt(NameCam, "ThITO_" + index, 225, BasePathIni);
                Amp = IniFile.I_I.ReadIniDbl(NameCam, "Amp_" + index, 0.005, BasePathIni);
                ThRectangleularity = IniFile.I_I.ReadIniDbl(NameCam, "ThRectangleularity_" + index, 0.9, BasePathIni);

                BlIgnoreThisSideFault = IniFile.I_I.ReadIniBl(NameCam, "BlIgnoreThisSideFault_" + index, BasePathIni);
                BlInspCF = IniFile.I_I.ReadIniBl(NameCam, "BlInspCF_" + index, BasePathIni);
                UsingORToCorner = IniFile.I_I.ReadIniBl(NameCam, "UsingORToCorner_" + index, BasePathIni);
                IsCrackEnabled = IniFile.I_I.ReadIniBl(NameCam, "IsCrackEnabled_" + index, BasePathIni);
                StrSidesMatch = IniFile.I_I.ReadIniStr(NameCam, "StrSidesMatch_" + index, BasePathIni);
                IntNumDefCornerWhenStart = IniFile.I_I.ReadIniInt(NameCam, "IntNumDefCornerWhenStart_" + index, 5, BasePathIni);
                IntNumDefCornerWhenEnd = IniFile.I_I.ReadIniInt(NameCam, "IntNumDefCornerWhenEnd_" + index, 5, BasePathIni);
                JudgeMentMark = IniFile.I_I.ReadIniBl(NameCam, "JudgeMentMark_" + index, BasePathIni);

                SplStartIndex1 = IniFile.I_I.ReadIniInt(NameCam, "SplStartIndex1_" + index, BasePathIni);
                SplEndIndex1 = IniFile.I_I.ReadIniInt(NameCam, "SplEndIndex1_" + index, BasePathIni);
                SplStartIndex2 = IniFile.I_I.ReadIniInt(NameCam, "SplStartIndex2_" + index, BasePathIni);
                SplEndIndex2 = IniFile.I_I.ReadIniInt(NameCam, "SplEndIndex2_" + index, BasePathIni);
                SplStartIndex3 = IniFile.I_I.ReadIniInt(NameCam, "SplStartIndex3_" + index, BasePathIni);
                SplEndIndex3 = IniFile.I_I.ReadIniInt(NameCam, "SplEndIndex3_" + index, BasePathIni);
                SplStartIndex4 = IniFile.I_I.ReadIniInt(NameCam, "SplStartIndex4_" + index, BasePathIni);
                SplEndIndex4 = IniFile.I_I.ReadIniInt(NameCam, "SplEndIndex4_" + index, BasePathIni);
                SplStartIndex5 = IniFile.I_I.ReadIniInt(NameCam, "SplStartIndex5_" + index, BasePathIni);
                SplEndIndex5 = IniFile.I_I.ReadIniInt(NameCam, "SplEndIndex5_" + index, BasePathIni);
                SplStartIndex6 = IniFile.I_I.ReadIniInt(NameCam, "SplStartIndex6_" + index, BasePathIni);
                SplEndIndex6 = IniFile.I_I.ReadIniInt(NameCam, "SplEndIndex6_" + index, BasePathIni);
                SplStartIndex7 = IniFile.I_I.ReadIniInt(NameCam, "SplStartIndex7_" + index, BasePathIni);
                SplEndIndex7 = IniFile.I_I.ReadIniInt(NameCam, "SplEndIndex7_" + index, BasePathIni);
                SplStartIndex8 = IniFile.I_I.ReadIniInt(NameCam, "SplStartIndex8_" + index, BasePathIni);
                SplEndIndex8 = IniFile.I_I.ReadIniInt(NameCam, "SplEndIndex8_" + index, BasePathIni);
                SplStartIndex9 = IniFile.I_I.ReadIniInt(NameCam, "SplStartIndex9_" + index, BasePathIni);
                SplEndIndex9 = IniFile.I_I.ReadIniInt(NameCam, "SplEndIndex9_" + index, BasePathIni);


                SplThShellY1 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellY1_" + index, BasePathIni);
                SplThShellX1 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellX1_" + index, BasePathIni);
                SplThShellY2 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellY2_" + index, BasePathIni);
                SplThShellX2 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellX2_" + index, BasePathIni);
                SplThShellY3 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellY3_" + index, BasePathIni);
                SplThShellX3 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellX3_" + index, BasePathIni);
                SplThShellY4 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellY4_" + index, BasePathIni);
                SplThShellX4 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellX4_" + index, BasePathIni);
                SplThShellY5 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellY5_" + index, BasePathIni);
                SplThShellX5 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellX5_" + index, BasePathIni);
                SplThShellY6 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellY6_" + index, BasePathIni);
                SplThShellX6 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellX6_" + index, BasePathIni);
                SplThShellY7 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellY7_" + index, BasePathIni);
                SplThShellX7 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellX7_" + index, BasePathIni);
                SplThShellY8 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellY8_" + index, BasePathIni);
                SplThShellX8 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellX8_" + index, BasePathIni);
                SplThShellY9 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellY9_" + index, BasePathIni);
                SplThShellX9 = IniFile.I_I.ReadIniDbl(NameCam, "SplThShellX9_" + index, BasePathIni);

                SplThPerY1 = IniFile.I_I.ReadIniDbl(NameCam, "SplThPerY1_" + index, BasePathIni);
                SplThPerY2 = IniFile.I_I.ReadIniDbl(NameCam, "SplThPerY2_" + index, BasePathIni);
                SplThPerY3 = IniFile.I_I.ReadIniDbl(NameCam, "SplThPerY3_" + index, BasePathIni);
                SplThPerY4 = IniFile.I_I.ReadIniDbl(NameCam, "SplThPerY4_" + index, BasePathIni);
                SplThPerY5 = IniFile.I_I.ReadIniDbl(NameCam, "SplThPerY5_" + index, BasePathIni);
                SplThPerY6 = IniFile.I_I.ReadIniDbl(NameCam, "SplThPerY6_" + index, BasePathIni);
                SplThPerY7 = IniFile.I_I.ReadIniDbl(NameCam, "SplThPerY7_" + index, BasePathIni);
                SplThPerY8 = IniFile.I_I.ReadIniDbl(NameCam, "SplThPerY8_" + index, BasePathIni);
                SplThPerY9 = IniFile.I_I.ReadIniDbl(NameCam, "SplThPerY9_" + index, BasePathIni);
            }
            catch (Exception ex)
            {
                WinError.GetWinInst().ShowError("参数读取获取失败！");
            }
        }
    }
}

