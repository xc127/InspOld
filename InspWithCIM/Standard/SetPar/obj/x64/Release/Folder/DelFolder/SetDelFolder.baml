   M S B A M L   `   `   `  ���� J  FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  K GControlLib, Version=1.0.2018.1010, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib 
 SetPar   SetPar.SetDelFolder   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     X TXceed.Wpf.Toolkit, Version=2.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  $	��650��$	��800��  � 	Resources   �� MergedDictionaries �� Source��  �� $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?��� $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?��� $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�%    .��� -   $��gdLayout�� � ColumnDefinitions �� $��* � � RowDefinitions �� $��6* ��� $��* ��� $��* �.+�  ControlLib.NGroupBox  $��设置文件删除��$��Stretch=�   
TitleBrush �	#FF00D1E5$��Stretch=�$��2��.���  �� $��* �.+�&   System.Windows.Controls.DataGrid  -   $��dgDelFolder�� J� 	AllowDrop .    AreRowDetailsFrozen .    AutoGenerateColumns .  
�������$��1q�	   CanUserAddRows	 .  
   CanUserResizeColumns
 .     CanUserSortColumns .  $��12(�   GridLinesVisibility$	 All=�$
��Auto��   
IsReadOnly .  $��1q�$��Stretch=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 序号��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 45 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *No+  $ 路径��$	 380    CanUserSort .     CanUserResize .     CanUserReorder .     
IsReadOnly .  �� *
PathFolder+  $ 文件夹数目��$ 80  .   .   .   �� *Num+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ 执行��$ 40  .   .   .   �� *
BlExcute+  $ 注释��$	 130  .  .   .   .   �� *
Annotation+� -   $��grid1��$��1��$��Stretch=�$��0q�$��Stretch=� �� $��* ��� $��* ��� $��* ��� $��* �.+�"  Xceed.Wpf.Toolkit.TimePicker  -   $��tpDel1��$��Center=�$��0q�$��Center=�$��90��$��30��   TimeInterval$ 00:01:00h� � ToolTip$ 设置自动删除时间��   EndTime$ 11:59:00h�   Value$ 7/3/2016 7:00 AMX�$��1��{���  �� Path$ 	IsChecked� �� ElementName$ tsbDel1���� -   $��label1��$��文件删除时间1:��$��28��$
��Left=�$��1q�$��Center=�	  ControlLib.MTSwitchButton	  -   $��tsbDel1��$��30��$��Right=�$��0q�$��Center=�$��55�� d� 	IsChecked$ FalseY�  -   $��tpDel2��$ 23:59:00h�$��3��$��30��$��Center=�$��0q�$ 00:01:00h�$ 设置自动删除时间��$ 5/22/2018 7:00 AMX�$��Center=�$��90��   Format$ 	ShortTime=�{��� $ 	IsChecked�$ tsbDel2���� -   $��label2��$��文件删除时间2:��$��28��$
��Left=�$��0q�$��Center=�$��2��	  -	   $��tsbDel2��$��30��$��Right=�$��0q�$��Center=�$��55��$��2��
  ControlLib.ButtonColor
  -
   $��btnSave�����	#FF66FFFF$��保  存��$��20(�$��Normal$�$��SemiBold"�$��0q�$ 保存对参数的修改��$��40��$��Center=�{�.   ControlLib.ButtonBase   
EAuthority .    
WAuthority .  $	��120��$��2��