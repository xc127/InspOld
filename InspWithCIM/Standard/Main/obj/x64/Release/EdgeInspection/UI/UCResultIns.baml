   M S B A M L   `   `   `  ���� J  FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  J FControlLib, Version=1.0.2019.119, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib " 机器视觉控制处理软件   Main.UCResultIns   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     @d2http://schemas.microsoft.com/expression/blend/2008     C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     -     � 	Resources   �� MergedDictionaries��  %    .���� $��2q�$��1��$��检测结果��.���  � RowDefinitions �� $��* ��� $
��2.5* ��� $
��2.5* � � ColumnDefinitions �� $��55 ��� $��* �.+��� $��二维码：��$��Center=�$
��Left=�$��55��$��2q��� -   $��lblCode��$��Init...��$��Center=�$
��Left=�$��1��$��2q��� -   $��	lblResult��$��1��$��2q�$	��100��$��2��$��80(�$��Init..��$��Center=�$��Stretch=�$	��Top=�$
��Auto���� -   $��lblClear��$��清空�����	#FF00D1E5$��1��$��50��$��2q�$��Center=�$��Right=�$��Center=�� $��2��$��2�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $��* ��� $��2* �.+��� $��总数:��$
��Left=�$��Stretch=�$��Center=�$��12(��� -   $��lblTotalNum��$
��0000��$��1��$��Stretch=�$
��Left=�$��Stretch=�$��Center=�$��12(�$��2q��� $��OK数:��$��1��$
��Left=�$��Stretch=�$��Center=�$��12(��� -   $��lblOKNum��$
��0000��$��1��$��1��$��Stretch=�$
��Left=�$��Stretch=�$��Center=�$��12(�$��2q��� $��NG数:��$
��Left=�$��2��$��Stretch=�$��Center=�$��12(��� -   $��lblNGNum��$
��0000��$��1��$��2��$��Stretch=�$
��Left=�$��Stretch=�$��Center=�$��2q��� -   $��lblShift��$��D��
����  �$��3��$��0��$��4��$��Stretch=�$��Center=�$	��Top=�$	��Top=�$��55(�$��2q��� $��贝壳:��$
��Left=�$��3��$��Stretch=�$��Center=�$��12(��� -	   $��lblNGCorner��$
��0000��$��1��$��4��$��Stretch=�$
��Left=�$��Stretch=�$��Center=�$��2q��� $��破角:��$
��Left=�$��4��$��Stretch=�$��Center=�$��12(��� -
   $��
lblNGShell��$
��0000��$��1��$��3��$��Stretch=�$
��Left=�$��Stretch=�$��Center=�$��2q��� $��其他:��$
��Left=�$��5��$��Stretch=�$��Center=�$��12(��� -   $��
lblNGOther��$
��0000��$��1��$��5��$��Stretch=�$
��Left=�$��Stretch=�$��Center=�$��2q�