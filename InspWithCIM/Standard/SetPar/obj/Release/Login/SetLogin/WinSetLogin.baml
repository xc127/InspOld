   M S B A M L   `   `   `  ���� K  GControlLib, Version=1.0.2018.1010, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib  J FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass T PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 
 SetPar   SetPar.WinSetLogin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  	   @d2http://schemas.microsoft.com/expression/blend/2008  	   C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib  	   C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass  	   UMetroControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  	   ;assembly&clr-namespace:System;assembly=mscorlib  	   -     � Title$  设置登录权限�� � WindowStartupLocation$ CenterScreen=�  BasicClass.BaseWindow   ShowMinButton .     ShowMaxRestoreButton .  $	��651��$	��483�����	#CC119EDA$��1q� � 	Resources  �� MergedDictionaries �� Source   Time S� 
MethodName S� 
ObjectType  System.Enum	 S� MethodParameters
 M� Type  BasicClass.Time_enum��  �� $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?��� $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?��� $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�%2   &	        S� $ 	GetValues��"  	 M� "
  .��� $��10q� � RowDefinitions �� $��3* ��� $��* �.+�,  &MahApps.Metro.Controls.MetroTabControl  -   $��MetroTabCreatScaledShapeP��
�������$��2��.@�   ControlLib.CTabItem  $��权限设定��$	��100��$��32��
���   �$	��0,0q�.��� -   $��gdLayout��$	��395��$	��605�� �� $��* ��� $��* ��� $��* ��� $��* �.+��� $��密码设置:��$��60��$
��Left=�$��20,0,0,0q�$��Center=�$	��195��$��36(�$��1���� -   $��label2��$��密码确认:��$��36(�$��60��$
��Left=�$��20,0,0,0q�$��Center=�$	��195��$��2��   ControlLib.ButtonColor  -   $��btnSave�����	#FF66FFFF$��保  存��$��30(�$
��Bold"�$��65��$	��180��$��196,10,190,8q�$��Center=�$��Center=�$��3��   ControlLib.ButtonBase   
EAuthority .    
WAuthority . {�.  G� -   $��passwordBox��$��64��$��Right=�$��0q�$��Center=�$	��367��$��32(�$��1�����	#CC119EDAG� -   $��passwordBoxSure��$��64��$��Right=�$��0q�$��Center=�$	��367��$��32(�$��2�����	#CC119EDA�� $��Right=�$	��0,0q�$��64��$	��370������  �� ImageSource$( "/SetPar;component/Images/Admin.gif���� $��
登录名:��$��36(�$��60��$
��Left=�$��20,0,0,0q�$��Center=�$	��195���� -   $��cboName��$��64��$��Right=�$
��0,17q�$��Center=�$	��315�� �� SelectedIndex$ 0��$��36(����	#CC119EDA.@��� $��	技术员��$��36(��� $��	工程师��$��36(��� $��厂商��$��36(�  $��登录控制��$	��100��$��32��
���   �$��0,0,0,0q�.��� -	   $��	gdLayout1��$	��400��$	��609�� �� $��* ��� $��* ��� $��* �.+��� -
   $��cboTime�����	#CC119EDA$��36(�$��65��$ 0��$	��315��$��
214,0,80,0q� � ToolTip$9 3超过设定时间后，软件会退出登录权限������  �� Source# [    -   $��btnSaveSetting�����	#FF66FFFF$��保  存�� . $��30(�$
��Bold"�$��65��$��Center=�{�.  $��0q�$��Center=� . $	��180��$��2���� -   $��chkManufacturer��$��默认厂商权限��$��1��$��40��$��Center=�$��0q�$��Center=�$��20(�$	��169���� -   $��label1��$��自动注销时间:��$��38��$
��Left=�$��	42,47,0,0q�$	��Top=�$��24(�