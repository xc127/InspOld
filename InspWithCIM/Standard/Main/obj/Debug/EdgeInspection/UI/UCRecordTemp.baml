   M S B A M L   `   `   `  ����K  GControlLib, Version=1.0.2018.1010, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib  5      " 机器视觉控制处理软件   Main.UCRecordTemp   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     5      .��5	      �� $��检测记录��6   $��1q�6   $��Stretch=�6(   $��Stretch=�6D   $
��auto��6b   .��5
   
   �� $
��Auto=�6   .��5      &   System.Windows.Controls.DataGrid  -   5        $��dgRecordSingle��5      $��1q�6-     J� 	AllowDrop  .    GridLinesVisibility$	 All=�6I   "   VerticalScrollBarVisibility$ Visible=�6�      AutoGenerateColumns .  $��2q�6�   
�������   CanUserReorderColumns .     CanUserResizeColumns .    CanUserSortColumns .     CanUserResizeRows .     
IsReadOnly . 	   SelectionUnit$	 FullRow=�6�  ��6�   �� 
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 缺陷��6;      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6G   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *FaultTypeStr+6G     5      $ 规格��6;   $
 Auto 6G    6   �� *FaultInfoStr+6G     5      $ 路径��68   $
 Auto 6D    6   �� *	ImagePath+6D   5      5      5      5      