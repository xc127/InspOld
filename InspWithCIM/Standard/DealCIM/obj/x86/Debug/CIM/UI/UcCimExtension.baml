   M S B A M L   `   `   `  ����B  >DealCIM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DealCIMDealCIM  5   	       DealCIM.UcCimExtension   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:DealCIM     5   	     � DataContext  5
      #  DealCIM.CimExtensionViewModel  5   
    � 	Resources 5       �� 
TargetType �� Property �� Value��%w   '��       �� 5   
   " ��6   .�5      ��  � ��$	 120��6&   �� 5       � ��$ 32��6'   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��75 �6   �� 5      $��75 �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��Center=�6   .+�5      �� $��关键字：��6   $��16(�5       �� 5!      $��24��5"      $��1��5#      $��16(�5$      $	��120��5%      ��5!      �� *	Content+5%      5&      �� 5'   
   $	��Add��6   $��1��5)      ��5(      �� *
AddCommand+5)      �� 5+   
   $��Delete��6   $��1��5-      ��5,      �� *DeleteCommand+5-      �� 5.   
   $
��Save��6   $��1��50      $��1��51      ��5/      �� *SaveCommand+51      &   System.Windows.Controls.DataGrid  52   
      
IsReadOnly . $��2��55      $��2��56      ��52      �� *
Contents+ �� SelectedItem 53      �� *SelectedContent+	   Columns	 57      0  *System.Windows.Controls.DataGridTextColumn  58      ,  &System.Windows.Controls.DataGridColumn
   Header$
 content��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� 5:      5<      5=      