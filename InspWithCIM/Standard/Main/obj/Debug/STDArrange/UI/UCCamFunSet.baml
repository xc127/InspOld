   M S B A M L   `   `   `  ����J  FBasicClass, Version=1.0.2020.520, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      ] Y机器视觉控制处理软件, Version=1.0.1.29286, Culture=neutral, PublicKeyToken=nullclr-namespace:MainMain 5      K GControlLib, Version=1.0.2018.1010, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib 5	         Main.UCCamFunSet   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      $localclr-namespace:Main     5      C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     5	      -   5        .��5        ControlLib.NGroupBox  ���	#CC119EDA$��2q�63   $��1q�6>   $��相机功能选择��6R       
TitleBrush  �	#FF00D1E5.��5   
   � $	��900��6   $	��450��6    � 	Resources 5       S� 
MethodName   	KeyCamFun S� 
ObjectType  System.Enum S� MethodParameters M� Type  Main.FunCam_Enum��%e   &	        S� 5      $ 	GetValues��6%   "  6N    5      M� 5      "  6!   5      5       � RowDefinitions 5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��1.2* �6#   .+�5       �� $��相机1��6   $��Right=�6"   $��Center=�6y   {�6>   ��  �� Path$ IsEnable�	 �� ElementName$	 
cbxCam1Set��6y   �� 5!      -   5        $��
cbxCam1Set��5!      {�.  $
��Left=�6v   $��Center=�6�   $	��100��6�   $��0��6�   $��1��6�   $��2q�6�   ��6;   �� 
 �� Source#
 [  6�   �� 5#      $��相机2��6   $��Right=�6]   $��Center=�6y   $��1��6�   $��0��6�   {�6"   �� $ IsEnable�$	 
cbxCam2Set��6�   �� 5$      -   5        $��
cbxCam2Set��5$      {�.  $
��Left=�6v   $��Center=�6�   $	��100��6�   $��1��6�   $��1��6�   $��2q�6�   ��6;   �� #
 [  6�   �� 5&      $��相机3��6   $��Right=�6]   $��Center=�6y   $��2��6�   $��0��6�   {�6"   �� $ IsEnable�$	 
cbxCam3Set��6�   �� 5'      -   5        $��
cbxCam3Set��5'      {�.  $
��Left=�6v   $��Center=�6�   $	��100��6�   $��2��6�   $��1��6�   $��2q�6�   ��6;   �� #
 [  6�   �� 5)      $��相机4��6   $��Right=�6]   $��Center=�6y   $��3��6�   $��0��6�   {�6"   �� $ IsEnable�$	 
cbxCam4Set��6�   �� 5*      -   5        $��
cbxCam4Set��5*      {�.  $
��Left=�6u   $��Center=�6�   $	��100��6�   $��3��6�   $��1��6�   $��2q�6�   ��6;   �� #
 [  6�   �� 5,      $��相机5��6   $��Right=�6]   $��Center=�6y   $��4��6�   $��0��6�   {�6"   �� $ IsEnable�$	 
cbxCam5Set��6�   �� 5-      -   5        $��
cbxCam5Set��5-      {�.  $
��Left=�6u   $��Center=�6�   $	��100��6�   $��4��6�   $��1��6�   $��2q�6�   ��6;   �� #
 [  6�   �� 5/      $��相机6��6   $��Right=�6]   $��Center=�6y   $��5��6�   $��0��6�   {�6"   �� $ IsEnable�$	 
cbxCam6Set��6�   �� 50      -   5        $��
cbxCam6Set��50      {�.  $
��Left=�6u   $��Center=�6�   $	��100��6�   $��5��6�   $��1��6�   $��2q�6�   ��6;   �� #
 [  6�   51      52      53      