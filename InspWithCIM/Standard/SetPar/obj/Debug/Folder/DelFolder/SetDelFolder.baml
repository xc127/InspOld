   M S B A M L   `   `   `  ����J  FBasicClass, Version=1.0.2020.520, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  5      K GControlLib, Version=1.0.2018.1010, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib 5      
 SetPar   SetPar.SetDelFolder   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     5      C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     5      X TXceed.Wpf.Toolkit, Version=2.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  5      $	��650��5
      $	��800��6     � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�6%   %    5      .��5      � -   5        $��gdLayout��5       � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��6* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
     ControlLib.NGroupBox  $��设置文件删除��6   $��Stretch=�6/      
TitleBrush �	#FF00D1E5$��Stretch=�6d   $��2��6�   .��5       �  5!      �� 5#      $��* �6$   .+�5%      &   System.Windows.Controls.DataGrid  -   5        $��dgDelFolder��5%   E   J� 	AllowDrop .    AreRowDetailsFrozen .    AutoGenerateColumns .  
�������$��1q�6v   	   CanUserAddRows	 .  
   CanUserResizeColumns
 .     CanUserSortColumns .  $��12(�6�      GridLinesVisibility$	 All=�6�   $
��Auto��6     
IsReadOnly .  $��1q�6:  $��Stretch=�6X  ��6"  ��    Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn   Header$ 序号��6D      Width5  /System.Windows.Controls.DataGridLengthConverter$ 45 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *No+6P     5(      $ 路径��6L   $	 380 6X      CanUserSort .     CanUserResize .     CanUserReorder .     
IsReadOnly .  6-   �� *
PathFolder+6�     5)      $ 文件夹数目��6E   $ 80 6T    .   .   .   6-   �� *Num+6�   4  .System.Windows.Controls.DataGridCheckBoxColumn  5*      $ 执行��6N   $ 40 6Z    .   .   .   61   �� *
BlExcute+6�     5+      $ 注释��6L   $	 130 6X    .  .   .   .   6-   �� *
Annotation+6�   5-      5.      5/      � 50   
   -   5        $��grid1��50   F   $��1��6   $��Stretch=�6   $��0q�6;   $��Stretch=�6S    51      �� 52      $��* �6#   �� 53      $��* �6#   �� 54      $��* �6#   �� 55      $��* �6#   .+�57      "  Xceed.Wpf.Toolkit.TimePicker  -   5        $��tpDel1��57   I   $��Center=�6!   $��0q�6>   $��Center=�6W   $��90��6r   $��30��6}      TimeInterval$ 00:01:00h�6�    � ToolTip$ 设置自动删除时间��6�      EndTime$ 11:59:00h�6�      Value$ 7/3/2016 7:00 AMX�6�   $��1��6�   {�6�   ��  �� Path$ 	IsChecked� �� ElementName$ tsbDel1���� 58      -   5        $��label1��58   Y   $��文件删除时间1:��6   $��28��6'   $
��Left=�63   $��1q�6N   $��Center=�6g   	  ControlLib.MTSwitchButton	  59      -   5        $��tsbDel1��59   [   $��30��6(   $��Right=�64   $��0q�6P   $��Center=�6j   $��55��6�    d� 	IsChecked$ FalseY�6�     5:      -   5        $��tpDel2��5:   x   $ 23:59:00h�6!   $��3��64   $��30��6D   $��Center=�6P   $��0q�6m   $ 00:01:00h�6�   $ 设置自动删除时间��6�   $ 5/22/2018 7:00 AMX�6�   $��Center=�6�   $��90��6�      Format$ 	ShortTime=�6�   {�6  �� $ 	IsChecked�$ tsbDel2���� 5;      -   5        $��label2��5;   Y   $��文件删除时间2:��6   $��28��6'   $
��Left=�63   $��0q�6N   $��Center=�6g   $��2��6�   	  5<      -	   5        $��tsbDel2��5<   [   $��30��6(   $��Right=�64   $��0q�6P   $��Center=�6j   $��55��6�   $��2��6�   5=      
  ControlLib.ButtonColor
  5>   
   -
   5        $��btnSave��5>   �   ���	#FF66FFFF$��保  存��68   $��20(�6G   $��Normal$�6U   $��SemiBold"�6h   $��0q�6~   $ 保存对参数的修改��6�   $��40��6�   $��Center=�6�   {�.   ControlLib.ButtonBase   
EAuthority .    
WAuthority .  $	��120��6  $��2��6*  5?      5@      