   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   K GControlLib, Version=1.0.2018.1010, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib 5   	   F BSetPar, Version=1.0.2018.926, Culture=neutral, PublicKeyToken=nullclr-namespace:SetParSetPar 5   	   F BCommon, Version=1.0.2018.926, Culture=neutral, PublicKeyToken=null/$clr-namespace:Common;assembly=CommonCommon 5	         SetPar.WinLogin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   UMetroControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     5   	   'SetParclr-namespace:SetPar     5   	   3my$clr-namespace:Common;assembly=Common     5	        � Title$  登录��5   	   $	��325��6   $	��485��6!    � WindowStartupLocation$ CenterScreen=�6-   ���	#CC119EDA$��1q�5   !   (   "MahApps.Metro.Controls.MetroWindow   ShowMinButton .     ShowMaxRestoreButton .   � 
ResizeMode$ NoResize=�5	   	    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   	 � RowDefinitions	 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��0.8* �6   .+�5    
   �� $��60,0,0,0q�6   $
��Left=�6"   $��Center=�6=   $��58��6X   $	��365��6d   $��2��6p   ��5!      �� 5"      
 �� ImageSource$(
 "/SetPar;component/Images/Admin.gif��6   6P   5$      �� 5%   
   $��60,0,0,0q�6   $
��Left=�6"   $��Center=�6=   $��58��6X   $	��365��6d   $��1��6p   $��2��6}   ��5&      �� 5'      $+
 %/SetPar;component/Images/Password.gif��6   6S   5)      G� 5*   
   -   5        $��psbLogin��5*   �   *   $MahApps.Metro.Controls.TextBoxHelper   ClearTextButton . $��40(�6I   $��58��6W   $
��Left=�6c   $��	112,0,0,0q�6~   $��Center=�6�   $	��312��6�    � ToolTip$ 输入登录密码��6�   $��1��6�   $��2��6�     ControlLib.ButtonColor  5+   
   -   5        $��btnSure��5+   r   ���	#FF66FFFF$��确  认��68   $��30(�6G   $
��Bold"�6U   $��0q�6g   $��2��6�   $��Center=�6�   $��Center=�6�   $	��180��6�   $��60��6�   �� 5,   
   -   5        $��lbInfo��5,   D   $��40��6   $��Center=�6   $��0q�69   $��Center=�6R   $	��200��6m   $��24(�6y   $��3��6�   $��2��6�   �� 5-   
   -   5        $��cboName��5-   [   $��36(�6   $��58��6!   $
��Left=�6-   $��	112,0,0,0q�6H    �� SelectedIndex$ 0��6j   $��Center=�6|   $	��312��6�   $��2��6�   .@�5.      �� $��	技术员��6   $��36(�6)   �� 5/      $��	工程师��6   $��36(�6)   �� 50      $��厂商��6   $��36(�6(   51        53   
   -   5        $��btnClose��53   �   ���	#FF66FFFF$��退  出��68   $��30(�6G   $
��Bold"�6U   $��60��6g   $��Center=�6s   $��0q�6�   $��Center=�6�   $	��180��6�   $��2��6�   $��1��6�   6�     Common.UIKeyBoard  54   
   -   5        $��
uIKeyBoard��54   o   $��1��6   $��3��6(   $��50��65   $
��Left=�6A   $��	150,0,0,0q�6\   $��Center=�6�   55      56      