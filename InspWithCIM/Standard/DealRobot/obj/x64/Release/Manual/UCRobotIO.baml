   M S B A M L   `   `   `  ���� J  FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  J FControlLib, Version=1.0.2019.119, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib  	DealRobot   DealRobot.UCRobotIO   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib       � 	Resources   �� MergedDictionaries �� Source��  �� $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?��� $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?��� $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?��� $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?��� $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�%    .���  � RowDefinitions �� $��50 ��� $
��309* ��� $	��42* ��� $	��38* � � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* �.+��� $��输入IO个数:��$��25��$��0q�  ControlLib.DoubleUpDown  -   $��dudNumInput�����	#FF4BAEEF$��35��$	��100��X TXceed.Wpf.Toolkit, Version=2.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4�  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   	Increment$ 1X�$��0q��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 0X�   Value$ 0X�$��1���� $��输出IO个数:��$��25��$��0q�$��2��  -   $��dudNumOutput�����	#FF4BAEEF$��3��$��35��$ 1X�$��0q�$ 0X�$ 0X�$	��100��  ControlLib.NGroupBox  $��1��   
TitleBrush �	#FF00D1E5
���   �$��Stretch=�$��In��{�. $��2��$��5q�.���  �� $
��115* ��� $
��105* � �� $
��228* ��� $	��42* ��� $��40 �.+�&   System.Windows.Controls.DataGrid  -   $
��dgIn��	 J� 	AllowDrop	 . 
   AreRowDetailsFrozen
 .    AutoGenerateColumns .  
�������$��1q�   CanUserAddRows .     CanUserResizeColumns .     CanUserSortColumns .  $��14(�   GridLinesVisibility$	 All=�$��Stretch=�   
IsReadOnly .  $��0,0,0,7q�$��Stretch=�$��2������    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 45    
IsReadOnly . 1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding �� *No+     Header$	 值��$ 50  .  �� *Value+4
  .System.Windows.Controls.DataGridCheckBoxColumn
  $ 组合��$ 50  .  �� *	BlUnion+  $ 注释��$ 50  .  �� *
Annotation+�� $��
组合值:��$��25��$��2q�$��1��  -   $��doubleUpDown1�����	#FF4BAEEF$��35��$ 11X�$��2q�$ 0X�$ 0X�$	��100��$��1��$��1��  ControlLib.ButtonColor  -   $��	btnReadIn�����	#FF66FFFF$��读取In��$��16(�$��Normal$�$��SemiBold"�$��	15,0,25,5q� � ToolTip$ 读取机器人IO��$��2��  
���   �$	��Out��{�.  �	#FF00D1E5$��2��$��2��$��1��$��5q�.���  �� $
��108* ��� $
��112* � �� $
��228* ��� $	��42* ��� $��40 �.+�  -   $��dgOut��	 . 
 .  .  
�������$��1q� .   .   .  $��14(�$	 All=�$��Stretch=� .  $��0,0,0,8q�$��Stretch=�$��2������    $ 45  .  �� *No+  $	 值��$ 50  .  �� *Value+
  $ 组合��$	 140  .  �� *	BlUnion+�� $��
组合值:��$��25��$��2q�$��1��  -   $��dudComboValue�����	#FF4BAEEF$��1��$��35��$ 11X�$��2q�$ 0X�$ 0X�$	��100��$��1��  -   $��btnWriteComboOut�����	#FF66FFFF$��	写入Out��$��20(�$��Normal$�$��SemiBold"�$��	20,2,13,2q�$ 写入机器人IO��$��2��  -	   $��btnSave�����	#FF66FFFF$��保存��$��16(�$��Normal$�$��SemiBold"�$��
86,0,90,15q�$��2��$��1��$��2��