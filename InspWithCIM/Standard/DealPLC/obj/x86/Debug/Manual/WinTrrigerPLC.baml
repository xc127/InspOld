   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   J FControlLib, Version=1.0.2019.119, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib 5   	   J FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass 5   	    DealPLC   DealPLC.WinTrrigerPLC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   UMetroControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     5   	   C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     5   	   -   5          � Title$5  /模拟PLC触发指令(产生的结果写入PLC)��5   	   $	��590��6'   $	��450��64   (   "MahApps.Metro.Controls.MetroWindow   ShowMaxRestoreButton .     ShowMinButton .   � WindowStartupLocation$ CenterScreen=�5   l   $��1q�6�   ���	#CC119EDA � 	Resources 5	       �� MergedDictionaries �� Source3  -System.Windows.Media.Effects.DropShadowEffect   ShadowWindow   Color   	Direction	   ShadowDepth
   
BlurRadius   Opacity�� 5
   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�6%   %_   &	          5      $
 Gray��64   $	 270Q�6A   $	 2Q�6Q   $
 15Q�6a   $ 1Q�6q   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $	��14* �6   �� 5      $��* �6   �� 5      $
��1.2* �6   .+�5    
     ControlLib.NGroupBox  $��触发寄存器��6   $��Stretch=�6.      
TitleBrush �	#FF00D1E5$��Stretch=�6c   $��2q�6�   $��3��6�   .��5!      � .+�5"      &   System.Windows.Controls.DataGrid  -   5        $��dgTrrigerReg��5"   )   J� 	AllowDrop .    AreRowDetailsFrozen .    AutoGenerateColumns .  
�������   CanUserAddRows .     CanUserResizeColumns .     CanUserSortColumns .  $��13(�6�      GridLinesVisibility$	 All=�6�      
IsReadOnly .  $��2,2,0,0q�6     SelectionUnit$ FullRow=�6=  $��Stretch=�6U  ��6   ��    Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$ 序号��6D      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *No+6P     5%      $ 	寄存器��6I   $	 150 6V    6-   �� *	NameReg+6V     5&      $ 数值��6J   $ 60 6V    6-   �� *
DblValue+6V     5'      $ 注释��6L   $	 150 6X    6-   �� *
Annotation+6X     5(      $ 说明��6I   $	 150 6U    6-   �� *	Explain+6U   5*      5+      5,      	  ControlLib.ButtonBase	  5-   
   -   5        $��
btnTrriger��5-   �   ���	#FFF7F7F7���	#AA119EDA$��触 发��6O   $��20(�6]   $��Normal$�6k   $��SemiBold"�6~   $��0q�6�   $	��120��6�   $��35��6�   $��2��6�   $��1��6�   	  5.   
   -   5        $��btnClose��5.   �   ���	#FFF7F7F7���	#AA119EDA$��退 出��6O   $��20(�6]   $��Normal$�6k   $��SemiBold"�6~   $��35��6�   $��0q�6�   $	��120��6�   $��2��6�   $��2��6�   6�   �� 5/   
   -   5        $��
chkTopMost��5/   j   $��窗体始终在最前��6   $��20��6%   $��Center=�61    d� 	IsChecked$
 TrueY�6N   $��0q�6_   $��Center=�6|   $��2��6�   6�   �� 50   
   -   5        $��chkClearReserve��50   j   $��清空读值保留��6   $��25��6$   $��Center=�60   $ FalseY�6M   $��0q�6_   $��Center=�6�   $��1��6�   6�   51      52      