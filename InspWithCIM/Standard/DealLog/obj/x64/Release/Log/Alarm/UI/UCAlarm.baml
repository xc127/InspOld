   M S B A M L   `   `   `  ���� J  FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass   DealLog   DealLog.UCAlarm   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass       � 	Resources   �� MergedDictionaries��  %    .��� .+��� -   $��lblClearAlarm��$��清空报警��
���   �$��25��$��Right=�$	��Top=�$��60�� K� ZIndex$ 1��$��0,0,5,0q�{�. ��Z� �� -   $��lblOpenFolder��$��本地日志��
���   �$��25��$��Right=�$	��Top=�$��60��$ 1��$��0,0,65,0q�{�. ��Z� �� -   $��gbAlarm�����	#CC119EDA$��1q�
���   �$��报警&异常��$��Stretch=�$��0q�$��Stretch=�$y�Visible=�.��&   System.Windows.Controls.DataGrid  -   $��dgAlarm��   AutoGenerateColumns .  $��1q�
���  ��   GridLinesVisibility$	 All=�   
IsReadOnly .    SelectionUnit$ FullRow=�
�������   HorizontalGridLinesBrush �	#FFF0F0F0   VerticalGridLinesBrush �	#FFF0F0F0	   Columns	 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn
   Header$
  ��   
Foreground
 �  ��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 30    CanUserReorder .     CanUserResize .     CanUserSort .  1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *No+  
 �  ��$
 报警&异常信息��$ *  .   .   .   �� *Alarm+  
 �  ��$
 时间��$ 70  .   .   .   �� *Time+