   M S B A M L   `   `   `  ���� J  FControlLib, Version=1.0.2019.119, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib  " 机器视觉控制处理软件   Main.WinParInsp   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     -     � Title$  
WinParInsp��$	��680��$
��1000�� � WindowStartupLocation$ CenterScreen=�.���  � 	Resources  �� 
TargetType   LblStyle �� Property �� Value��%z   &	        �� " ��.���  � ��$ Center=���  � ��$ 10(���  � ��$ Right=���  � ��$ Center=� � ColumnDefinitions �� $
��0.3* ��� $��* ��� $
��0.3* �.+�� $��1��.+��� $��0,0,0,50q�.+��  �� $��* ��� $��* �.+�	� -   $��RdbSide1��$��参数1��$��Center=�$��20(�$��0��	� -   $��RdbSide2��$��参数2��$��Center=�$��20(�$��1��   ControlLib.NGroupBox  $	��Top=�$��判定设置��   
TitleBrush �	#FF00D1E5.���  � RowDefinitions �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $��* ��� $��* ��� $��* ��� $
��1.2* �.+��� #��[  $��贝壳深度阈值：���� #��[  $��贝壳宽度阈值：��$��2���� #��[  $��破角深度阈值：��$��1���� #��[  $��破角宽度阈值：��$��1��$��2���� #��[  $��凸边深度阈值：��$��2���� #��[  $��剔除毛刺：��$��4��$��0���� #��[  $��电极灰度阈值：��$��3��$��0���� #��[  $��电极检测扩张度：��$��3��$��2���� #��[  $��凸边宽度阈值：��$��2��$��2���� #��[  $��角图片起始张数��$��7��$��0���� #��[  $��角图片结束张数��$��7��$��2��   ControlLib.DoubleUpDown  -   $��DudThShellY��X TXceed.Wpf.Toolkit, Version=2.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4�  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]	   Minimum$	 0X��  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
   	Increment$

 0.01X�$��1��   Value ��  �� Path$ ThShellY� �� Mode$ TwoWay=�  -   $��DudThShellX��$	 0X�$

 0.01X�$��3�� �� $ ThShellX�$ TwoWay=�  -   $��DudThCornerY��$	 0X�$

 0.01X�$��1��$��1�� �� $ 	ThCornerY�$ TwoWay=�  -   $��DudThCornerX��$	 0X�$

 0.01X�$��1��$��3�� �� $ 	ThCornerX�$ TwoWay=��� $!��使用“或”判断破角��$��4��$��1��$��Center=�$��Center=� d� 	IsChecked �� *UsingORToCorner+  -   $��DudThPreiectionY��$

 0.01X�$	 0X�$��2��$��1�� �� $ ThPreiectionY�$ TwoWay=�  -	   $��DudThPreiectionX��$	 0X�$

 0.01X�$��2��$��3�� �� $ ThPreiectionX�$ TwoWay=�  -
   $��DudThITO��$
 1X�   Maximum$	 255X�$	 0X�$��3��$��1�� �� $ ThITO�$ TwoWay=�  -   $��DudDblOutRate��$ 3X�$	 0X�$	
 0.1X�$��3��$��3�� �� $ 
DblOutRate�$ TwoWay=�  -   $��	DudSmooth��$ 20X�$	 0X�$
 1X�$��4��$��1�� �� $ SmoothWidth�$ TwoWay=�  -     Name$ DudSmooth_Copy��$ 20X�$	 0X�$
 1X�$��4��$��2�� �� $ SmoothHeight�$ TwoWay=�  -   $��DudNumCornerStart��$ 20X�$	 0X�$
 1X�$��7��$��1�� �� $ IntNumDefCornerWhenStart�$ TwoWay=�  -   $��DudNumCornerEnd��$ 20X�$	 0X�$
 1X�$��7��$��3�� �� $ IntNumDefCornerWhenEnd�$ TwoWay=��� -   $��CbxIgnoreSide1Fault��$��忽略此边所有缺陷��$��Center=�$��8(�$��14��$��5��$��3��$��Center=� �� *BlIgnoreThisSideFault+$ TwoWay=��� -   $��CbxIsCFSide��$��1q�$��检测CF面��$��5��$��2��$��1��$��Center=�$
��Left=� �� *
BlInspCF+$ TwoWay=��� #��[  $��边序号设置：��$��6��$��0���� -   $��TxtSideMatch��$
��auto��$��Stretch=�$��6��$��1��$��Right=�$��Stretch=�$��Center=� � ToolTip$ 多个边需要用','������ *StrSidesMatch+$ TwoWay=��� -   $��LblState��$��6��$��2��
���  ���� -   $��CbxIgnoreMark��$#��通过识别Mark来判断OKNG��$��1q�$��8��$��2��$��1��$��Center=�$
��Left=�$T N勾选时，识别为破角的图片如果mark能正常识别，会判定为OK�� �� *JudgeMentMark+$ TwoWay=�  $��参数�� �	#FF00D1E5$��0,2,0,0q�.���  �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $	��100 ��� $��* ��� $	��100 �.+��� #��[  $��玻璃阈值：��$��2���� #��[  $��边界节点数：��$��2��$��1���� #��[  $��奇点剔除比例：��$��1���� #��[  $��轮廓阈值：��$��2q��� #��[  $��放大系数：��$��5��$��0���� #��[  $��矩形度阈值：��$��5��$��2��  -   $��DudThresholdGlass��$
 1X�$	 255X�$	 0X�$��0��$��3��$��0q� �� $ ThresholdGlass�$ TwoWay=�  -   $��
DudNumRect��{�.  $
 1X�$	 255X�$	 0X�$��1��$��3�� �� $ NumRect�$ TwoWay=�  -   $��DudRatioDelete��{�.  $	
 0.1X�$ 1X�$	 0X�$��1��$��1�� �� $ RatioDelete�$ TwoWay=�  -   $��DudThresholdEdge��$	 255X�$	 0X�$
 1X�$��1�� �� $ ThresholdEdge�$ TwoWay=�  -   $��DudAmp��$	 0X�$
 0.001X�$��5��$��1�� �� $	 Amp�$ TwoWay=�  -   $��DudThRectangleularity��$	
 0.1X�$ 1X�$	 0X�$��5��$��3�� �� $ ThRectangleularity�$ TwoWay=��� -   $��BtnSave��$��保存��$��Bottom=�$	��100��$��48�����	#FF00D1E5