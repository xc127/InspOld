   M S B A M L   `   `   `  ���� T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  J FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass E AControlLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib  DealLog   DealLog.WinRecover   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     UMetroControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     -     � Title$  历史参数恢复��$	��600��$
��1200�� � Topmost .   BasicClass.BaseWindow   ShowMaxRestoreButton .   � WindowStartupLocation$ CenterScreen=����	#FF00D1E5$��1q� � 	Resources    TypeRecvoer � Source � XPath  TVIStyle �� 
TargetType �� BasedOn	 �� Property
 �� Value �� XPath�� %�   &	        &	 8     7Q�� $' !../XmlTemplate/XmlTypeRecvoer.xml?�$
 Root���� " Q�8   .��� 	 �@Q�
IsExpanded
 �� $ @IsExpanded��.���  � ColumnDefinitions �� $��* ��� $��4* �.+�� -   $��grid1��$��1��$��Stretch=�$��0q�$��Stretch=� � RowDefinitions �� $
��184* ��� $
��191* �.+�  ControlLib.NGroupBox  $��0��   
TitleBrush �	#FF00D1E5
���   �$��Stretch=�$��文件目录��$��2q����	#FF00D1E5$��1q�.��� -   $��gdLayout��.+�&   System.Windows.Controls.DataGrid  -   $��dgDir�� J� 	AllowDrop .    AreRowDetailsFrozen .    AutoGenerateColumns .  
�������$��1q�   CanUserAddRows .     CanUserResizeColumns .     CanUserSortColumns .  $��13(�   GridLinesVisibility$	 All=�$��Stretch=�   
IsReadOnly . $��Stretch=�$��1q�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$  ��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 35    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *No+  $ 日期��$	 100  .  �� *Date+  $ 时间��$	 100  �� *Time+  $ 备份原因��$	 500  �� *Reason+  $ 文件原路径��$	 550  �� *PathSourceShow+  
���   �$��文件夹目录��$��Stretch=� �	#FF00D1E5$��1��$��2q�.��� -   $��grid2��.+�  -   $��dgFile�� .  .  .  
�������$��1q� .   .   .  $��13(�$	 All=�$��Stretch=� . $��1q�$��Stretch=�����    $  �� . $ 35  �� *No+  $ 名称�� . $	 150  �� *Name+  $ 类型��$ 60  �� *Type+  $ 路径��$	 700  �� *Path+� -   $��grid3��$��Stretch=�$��0q�$��Stretch=� �� $��5* ��� $��* ��� $��* �.+�R� -   $��tvBackup�����	#CC119EDA$��1q�$��2q�$��Stretch=�$��14(�����  �� Source# [  ���� # [ " Q�  ControlLib.ButtonColor  -	   $��
btnRecover�����	#FF66FFFF$��恢复参数��$��14(�$��40��$��Center=�$��0q�$��Center=�$	��120��$��1��  -
   $��btnLook�����	#FF66FFFF$��查看文件夹��$��14(�$��40��$��Center=�$��0q�$��Center=�$	��120��$��2��