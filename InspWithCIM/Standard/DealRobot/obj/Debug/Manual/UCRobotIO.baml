   M S B A M L   `   `   `  ����J  FBasicClass, Version=1.0.2020.520, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  5      J FControlLib, Version=1.0.2019.227, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib 5       	DealRobot   DealRobot.UCRobotIO   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     5      C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     5        � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $
��309* �6   �� 5      $	��42* �6   �� 5      $	��38* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5       $��* �6   .+�5"   
   �� $��输入IO个数:��6   $��25��6"   $��0q�6.     ControlLib.DoubleUpDown  5#   
   -   5        $��dudNumInput��5#   x   ���	#FF4BAEEF$��35��6:   $	��100��6G   X TXceed.Wpf.Toolkit, Version=2.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4�  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   	Increment$ 1X�6S   $��0q�6a   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 0X�6l      Value$ 0X�6�   $��1��6�   6�   �� 5$   
   $��输出IO个数:��6   $��25��6"   $��0q�6.   $��2��69     5%   
   -   5        $��dudNumOutput��5%   {   ���	#FF4BAEEF$��3��6:   $��35��6J   $ 1X�6V   $��0q�6d   $ 0X�6o   $ 0X�6�   $	��100��6�   6�     ControlLib.NGroupBox  5'   
   $��1��6      
TitleBrush �	#FF00D1E5
���   �$��Stretch=�6V   $��In��6t   {�. $��2��6�   $��5q�6�   .��5(      �  5)      �� 5*      $
��115* �6'   �� 5+      $
��105* �6'    5-      �� 5.      $
��228* �6$   �� 5/      $	��42* �6$   �� 50      $��40 �6$   .+�52      &   System.Windows.Controls.DataGrid  -   5        $
��dgIn��52   [  	 J� 	AllowDrop	 . 
   AreRowDetailsFrozen
 .    AutoGenerateColumns .  
�������$��1q�6v      CanUserAddRows .     CanUserResizeColumns .     CanUserSortColumns .  $��14(�6�      GridLinesVisibility$	 All=�6�   $��Stretch=�6     
IsReadOnly .  $��0,0,0,7q�6J  $��Stretch=�6g  $��2��6�  ��62  ��    Columns 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 45 6D      
IsReadOnly . 1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6-   �� *No+6O     55         Header$	 值��6G   $ 50 6R    .  6-   �� *Value+6]   4
  .System.Windows.Controls.DataGridCheckBoxColumn
  56      $ 组合��6M   $ 50 6Y    .  61   �� *	BlUnion+6d     57      $ 注释��6L   $ 50 6X    .  6-   �� *
Annotation+6c   59      �� 5:      $��
组合值:��6   $��25��6'   $��2q�63   $��1��6>     5;      -   5        $��doubleUpDown1��5;   t   ���	#FF4BAEEF$��35��6B   $ 11X�6N   $��2q�6]   $ 0X�6h   $ 0X�6�   $	��100��6�   $��1��6�   $��1��6�     ControlLib.ButtonColor  5<      -   5        $��	btnReadIn��5<   �   ���	#FF66FFFF$��读取In��6@   $��16(�6O   $��Normal$�6]   $��SemiBold"�6p   $��	15,0,25,5q�6�    � ToolTip$ 读取机器人IO��6�   $��2��6�   6�   5=      5>        5?   
   
���   �$	��Out��62   {�.  �	#FF00D1E5$��2��6g   $��2��6w   $��1��6�   $��5q�6�   .��5@      �  5A      �� 5B      $
��108* �6'   �� 5C      $
��112* �6'    5E      �� 5F      $
��228* �6$   �� 5G      $	��42* �6$   �� 5H      $��40 �6$   .+�5J        -   5        $��dgOut��5J   [  	 . 
 .  .  
�������$��1q�6v    .   .   .  $��14(�6�   $	 All=�6�   $��Stretch=�6   .  $��0,0,0,8q�6J  $��Stretch=�6h  $��2��6�  ��62  ��  5K        5L      $ 45 6D    .  6-   �� *No+6O     5M      $	 值��6G   $ 50 6R    .  6-   �� *Value+6]   
  5N      $ 组合��6M   $	 140 6Y    .  61   �� *	BlUnion+6e   5P      �� 5Q      $��
组合值:��6   $��25��6'   $��2q�63   $��1��6>     5R      -   5        $��dudComboValue��5R   �   ���	#FF4BAEEF$��1��6B   $��35��6R   $ 11X�6^   $��2q�6m   $ 0X�6x   $ 0X�6�   $	��100��6�   $��1��6�     5S      -   5        $��btnWriteComboOut��5S   �   ���	#FF66FFFF$��	写入Out��6@   $��20(�6P   $��Normal$�6^   $��SemiBold"�6q   $��	20,2,13,2q�6�   $ 写入机器人IO��6�   $��2��6�   6�   5T      5U        5V   
   -	   5        $��btnSave��5V   �   ���	#FF66FFFF$��保存��68   $��16(�6E   $��Normal$�6S   $��SemiBold"�6f   $��
86,0,90,15q�6|   $��2��6�   $��1��6�   $��2��6�   5W      5X      