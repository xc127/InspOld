   M S B A M L   `   `   `  ����J  FBasicClass, Version=1.0.2020.520, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  5      K GControlLib, Version=1.0.2018.1010, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib 5      " 机器视觉控制处理软件   Main.UCResultIns   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     5      -   5          � 	Resources  5
       �� MergedDictionaries�� 5   
    5      %    5      .��5      �� $��2q�6   $��1��6   $��检测结果��6*   .��5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��2.5* �6    �� 5      $
��2.5* �6     � ColumnDefinitions 5      �� 5      $��55 �6#   �� 5      $��* �6#   .+�5       �� $��二维码：��6   $��Center=�6#   $
��Left=�6>   $��55��6Y   $��2q�6d   �� 5!      -   5        $��lblCode��5!   &   $��Init...��6   $��Center=�65   $
��Left=�6P   $��1��6k   $��2q�6{   �� 5"      -   5        $��	lblResult��5"      $��1��6%   $��2q�62   $	��100��6=   $��2��6J   $��80(�6^   $��Init..��6l   $��Center=�6}   $��Stretch=�6�   $	��Top=�6�   $
��Auto��6�   �� 5#      -   5        $��lblClear��5#   !   $��清空��6   ���	#FF00D1E5$��1��6H   $��50��6X   $��2q�6c   $��Center=�6n   $��Right=�6�   $��Center=�6�   6�   � 5$      $��2��6   $��2��6     5%      �� 5&      $��* �6$   �� 5'      $��* �6$   �� 5(      $��* �6$   �� 5)      $��* �6$   �� 5*      $��* �6$   �� 5+      $��* �6$    5-      �� 5.      $��* �6'   �� 5/      $��* �6'   �� 50      $��2* �6'   .+�53      �� $��总数:��6   $
��Left=�6&   $��Stretch=�6A   $��Center=�6]   $��12(�6   �� 54      -   5        $��lblTotalNum��54   '   $
��0000��6   $��1��6:   $��Stretch=�6J   $
��Left=�6h   $��Stretch=�6�   $��Center=�6�   $��12(�6�   $��2q�6�   �� 55      $��	检测NG:��6   $��1��6(   $
��Left=�65   $��Stretch=�6P   $��Center=�6l   $��12(�6�   �� 56      -   5        $��lblInspNGNum��56   '   $
��0000��6   $��1��6;   $��1��6H   $��Stretch=�6X   $
��Left=�6v   $��Stretch=�6�   $��Center=�6�   $��12(�6�   $��2q�6�   �� 57      $��	读码NG:��6   $
��Left=�6(   $��2��6C   $��Stretch=�6P   $��Center=�6l   $��12(�6�   �� 58      -   5        $��lblCodeNGNum��58   '   $
��0000��6   $��1��6;   $��2��6K   $��Stretch=�6X   $
��Left=�6v   $��Stretch=�6�   $��Center=�6�   $��2q�6�   �� 59      -   5        $��lblShift��59   $   $��D��6   
����  �$��3��6G   $��0��6W   $��4��6d   $��Stretch=�6u   $��Center=�6�   $	��Top=�6�   $	��Top=�6�   $��55(�6�   $��2q�6�   �� 5:      $��贝壳:��6   $
��Left=�6&   $��3��6A   $��Stretch=�6N   $��Center=�6j   $��12(�6�   �� 5;      -	   5        $��lblNGCorner��5;   '   $
��0000��6   $��1��6:   $��4��6J   $��Stretch=�6W   $
��Left=�6u   $��Stretch=�6�   $��Center=�6�   $��2q�6�   �� 5<      $��破角:��6   $
��Left=�6&   $��4��6A   $��Stretch=�6N   $��Center=�6j   $��12(�6�   �� 5=      -
   5        $��
lblNGShell��5=   '   $
��0000��6   $��1��69   $��3��6I   $��Stretch=�6V   $
��Left=�6t   $��Stretch=�6�   $��Center=�6�   $��2q�6�   �� 5>      $��
凹凸边:��6   $
��Left=�6'   $��5��6B   $��Stretch=�6O   $��Center=�6k   $��12(�6�   �� 5?      -   5        $��
lblNGOther��5?   '   $
��0000��6   $��1��69   $��5��6I   $��Stretch=�6V   $
��Left=�6t   $��Stretch=�6�   $��Center=�6�   $��2q�6�   5@      5A      5B      5C      