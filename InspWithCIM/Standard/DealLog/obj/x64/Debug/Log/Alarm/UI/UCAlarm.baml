   M S B A M L   `   `   `  ����J  FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  5       DealLog   DealLog.UCAlarm   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     5        � 	Resources  5	       �� MergedDictionaries�� 5
   
    5      %    5      .��5      � .+�5   
   �� -   5        $��lblClearAlarm��5   �   $��清空报警��6   
���   �$��25��62   $��Right=�6>   $	��Top=�6Z   $��60��6r    K� ZIndex$ 1��6�   $��0,0,5,0q�6�   {�. ��6}   Z� 6�   �� 5   
   -   5        $��lblOpenFolder��5   �   $��本地日志��6   
���   �$��25��62   $��Right=�6>   $	��Top=�6Z   $��60��6r   $ 1��6�   $��0,0,65,0q�6�   {�. ��6}   Z� 6�   �� 5   
   -   5        $��gbAlarm��5   �   ���	#CC119EDA$��1q�6+   
���   �$��报警&异常��6R   $��Stretch=�6e   $��0q�6�   $��Stretch=�6�   $y�Visible=�6�   .��5      &   System.Windows.Controls.DataGrid  -   5        $��dgAlarm��5   �      AutoGenerateColumns .  $��1q�63   
���  ��   GridLinesVisibility$	 All=�6X      
IsReadOnly .    SelectionUnit$ FullRow=�6�   
�������   HorizontalGridLinesBrush �	#FFF0F0F0   VerticalGridLinesBrush �	#FFF0F0F0	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
  ��6@      
Foreground
 �  ��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 6\      CanUserReorder .     CanUserResize .     CanUserSort .  1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *No+6�     5      
 �  ��$
 报警&异常信息��6T   $ * 6i    .   .   .   6)   �� *Alarm+6�     5      
 �  ��$
 时间��6S   $ 70 6_    .   .   .   6)   �� *Time+6�   5      5      5       5!      