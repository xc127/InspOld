   M S B A M L   `   `   `  ����J  FBasicClass, Version=1.0.2020.520, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  5      K GControlLib, Version=1.0.2018.1010, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib 5      T PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      B >DealPLC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DealPLCDealPLC 5	         DealPLC.WinSetPLC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     5      C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     5      UMetroControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      )DealPLCclr-namespace:DealPLC     5	      -   5          � Title$  	设置PLC��5
      $	��600��5      $
��1090��6      BasicClass.BaseWindow   ShowMinButton .     ShowMaxRestoreButton .  ���	#CC119EDA$��1q�6s    � WindowStartupLocation$ CenterScreen=�6�      ShowCloseButton .  � 	Resources 5       �� MergedDictionaries �� Source   DealPLC � Source	 � XPath
 �� DataType �� ItemsSource �� XPath  TVIStyle �� 
TargetType �� BasedOn �� Property �� Value�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�6%   %i  &	        (�� E     5   '   *Root+)(�� �     5   '   *Title+)(�� +    5!   '   *
Function+)&	 �    7Q�� 5      $! ./XmlTemplate/XmlSetPLC.xml?�6.   $
	 Root��6S   �� 5      $

 Root��6'    67   �� $ Title��.R�5      �� ��6   �� $ @Name��6;   5      �� 5      $
 Title��6'    68   �� $ Function��.R�5      �� ��6   �� $ @Name��6;   5      �� 5!      $
 Function��6'   .R�5"      �� ��6   �� $ @Name��6;   5#      �� 5%      " Q�6%   8   .�5&      ��  �@Q�
IsExpanded 6/   �� $ @IsExpanded��6U   5'      5(      .��5+      � -   5        $��gdLayout��5+       � ColumnDefinitions 5,   
   �� 5-      $��* �6   �� 5.      $��4* �6   .+�50   
     ControlLib.NGroupBox  -   5        $��
epdSetBase��50   X   $��	PLC设置��6      
TitleBrush �	#FF00D1E5
���   �$��Center=�6j   $��Center=�6�   $��5,154,5,110q�6�   $	��200��6�   $	��295��6�   .��51      � $	��262��6   
�������.+�52      R� -   5        $��tvSetPLC��52   }   $��Stretch=�6S   $��2q�6r   $��Stretch=�6�   $��1q�6�   ���	#CC119EDA{�. $��14(�6  $��2��6'  ��6   ��  �� Source# [  ��53      �� 54      " Q�6    # [ 6;   56      57      58      59      5:      