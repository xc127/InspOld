   M S B A M L   `   `   `  ���� T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  J FControlLib, Version=1.0.2019.119, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib J FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  DealConfigFile!   DealConfigFile.WinConfigPar   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     UMetroControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     X	 TXceed.Wpf.Toolkit, Version=2.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit 	 -   $	��680��$	��680��  � Title$%  设置产品参数&拍照位置�� � WindowStartupLocation$ CenterScreen=����	#FF00D1E5$��1q� � 
ResizeMode$ NoResize=� � 	Resources  �� MergedDictionaries �� Source��  �� $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?��� $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?��� $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�%    .��� -   $��gdlayout�� � RowDefinitions �� $	��10* ��� $��1* � � ColumnDefinitions .+�]� $��Stretch=�$��1q�$��Stretch=�.+�9� $	��Top=����	#FF00D1E5$��拍照位置��$��Stretch=�$��2q����	#FF00D1E5 9� 
IsExpanded .  .��� $	��450��.+�&   System.Windows.Controls.DataGrid  -   $��
dgPosPhoto��	 J� 	AllowDrop	 . 
   AreRowDetailsFrozen
 .    AutoGenerateColumns .  
�������   CanUserAddRows .     CanUserResizeColumns .     CanUserSortColumns .  $��13(�   GridLinesVisibility$	 All=�   
IsReadOnly .     SelectionUnit$ FullRow=�$��Stretch=�$��1q�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 序号��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *No+  $ X��$ 90  �� *X+  $ Y��$ 90  �� *Y+  $ Z��$ 90  �� *Z+  $ 相机序号��$ 80  �� *
NoCamera+  $ 注释��$	 200  �� *
Annotation+9� $	��Top=����	#FF00D1E5$��产品参数��$��Stretch=�$��2q����	#FF00D1E5 . .��� -   $��	gdButton1��$	��500��  .+�  -   $��dgConfigPar��	 . 
 .  .  
������� .   .   .  $��13(�$	 All=� .  $ FullRow=�$��Stretch=�$��1q�$��1,1,1,8q�����    $ 序号��$ 50  �� *No+  $ 数值��$ 90  �� *
DblValue+  $	 Min��$	 100  �� *DblMin+  $	 Max��$	 100  �� *DblMax+  $ 注释��$	 200  �� *
Annotation+� -   $��gdBtn��$��1�� �� $��* ��� $��1* ��� $��5* ��� $��2* ��� $��2* �.+��� -   $��label1��$��
文件名:��$��25��$��0q�$��Center=�$��1���� -   $��txtModel��$��30�����	#FF00D1E5$��Center=�$��0q�$��Center=�$	��290��$��2��$��14(� �� 
IsReadOnly .   ControlLib.ButtonColor  -	   $��btnSave��$��保存��$��0q�$	��110��$��40�����	#FF66FFFF$��20(�$��Normal$�$��SemiBold"�{�. $��4��  -
   $��
btnPLCAnno�����	#FF66FFFF$��引用PLC注释��$��16(�$��Normal$�$��SemiBold"�$��40��{�. $��0q�$	��110��$��3��$y�Hidden=��� -   $��
chkTopMost��$	��Top��$��25�� d� 	IsChecked$
 trueY�$��0q�$��Center=�$��Center=�