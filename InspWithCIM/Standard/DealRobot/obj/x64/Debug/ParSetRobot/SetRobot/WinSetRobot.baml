   M S B A M L   `   `   `  ����J  FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  5      T PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5       	DealRobot   DealRobot.WinSetRobot   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     5      UMetroControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      -   5        $	��600��5	      $
��1090��6     � Title$  设置机器人��6(   (  "MahApps.Metro.Controls.MetroWindow   ShowMinButton .     ShowMaxRestoreButton .  $��1q�6�   ���	#CC119EDA � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5       �� MergedDictionaries �� Source   	DealRobot � Source � XPath	 �� DataType
 �� ItemsSource �� XPath  TVIStyle �� 
TargetType �� BasedOn �� Property �� Value�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   %i  &	        (�� E     5   '   *Root+)(�� �     5   '   *Title+)(�� +    5   '   *
Function+)&	 �    7Q�� 5      $! XmlTemplate/XmlSetRobot.xml?�60   $
 Root��6U   �� 5      $
	 Root��6'   
 67   �� $ Title��.R�5      �� ��6   �� $ @Name��6;   5      �� 5      $	 Title��6'   
 68   �� $ Function��.R�5      �� ��6   �� $ @Name��6;   5      �� 5      $	 Function��6'   .R�5      �� ��6   �� $ @Name��6;   5       �� 5"      " Q�6%   8   .�5#      ��  �@Q�
IsExpanded 6/   �� $ @IsExpanded��6U   5$      5%      .��5(      � -   5        $��gdLayout��5(       � ColumnDefinitions 5)   
   �� 5*      $��* �6   �� 5+      $��4* �6   .+�5-   
   9� -   5        $��
epdSetBase��5-   ^   $��设置机器人��6   ���	#FF00D1E5
���   � 9� 
IsExpanded . $��Center=�6p   $��Center=�6�   $	��5,0q�6�   $	��200��6�   $	��250��6�   .��5.      � $	��210��6   
�������.+�5/      R� -   5        $��
tvSetRobot��5/      $��Stretch=�6U   $��2q�6t   $��Stretch=�6�   $��1q�6�   ���	#CC119EDA{�. ��6   ��  �� Source# [  ��50      �� 51      " Q�6    # [ 6;   53      54      55      56      57      