   M S B A M L   `   `   `  ���� J  FBasicClass, Version=1.0.2019.131, Culture=neutral, PublicKeyToken=null;,clr-namespace:BasicClass;assembly=BasicClass
BasicClass  J FControlLib, Version=1.0.2019.119, Culture=neutral, PublicKeyToken=null;,clr-namespace:ControlLib;assembly=ControlLib
ControlLib  	DealRobot   DealRobot.UCTechRobot   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     C
BasicClass,clr-namespace:BasicClass;assembly=BasicClass     C
ControlLib,clr-namespace:ControlLib;assembly=ControlLib     -   $	��350��$	��400��  � 	Resources     styleTxt �� 
TargetType �� Property �� Value��%t   &	        �� " ��.���  � ��$ Center=���  � ��$ Right=���  � ��$ 0����  � ��$ 2q�.��� .+��� .+�� $	��100�� � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � � RowDefinitions �� $��* ��� $��* �.+��� -   $��
btnCurrPos��$��获取当前点��$��60��$��30���� -   $
��txtX��#��[  $��1���� -   $
��txtY��#��[  $��2���� -   $
��txtZ��#��[  $��3���� -   $
��txtU��#��[  $��4���� -   $��cbxHand��$��Center=�$��Center=�$��60��$��5�� �� SelectedIndex$ 0��{�.  .@��� $��L���� $��R��  ControlLib.ButtonColor  -   $��btnSetPowerOn�����	#FF66FFFF$��上电��$��1��$��70��$��30��$��1��$��2��  -	   $��btnSetPowerOff�����	#FF66FFFF$��断电��$��1��$��70��$��30��$��3��$��2��� -
   $��gdCtr��$	��250��{�.   �� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $��btnDecX��$��X-��$��2��$��0��$��Center=�$��Center=��� -   $��btnAddX��$��X+��$��2��$��2��$��Center=�$��Center=��� -   $��btnDecY��$��Y-��$��1��$��1��$��Center=�$��Center=��� -   $��btnAddY��$��Y+��$��3��$��1��$��Center=�$��Center=��� -   $��btnDecU��$��R+��$��2��$��1��$
��auto��$��20��$��
70,31,9,31q��� -   $��btnAddU��$��R-��$��2��$��1��$
��auto��$��20��$��
9,31,70,31q��� -   $��btnDecZ��$��Z+��$��1��$��2��$��Center=�$��Center=��� -   $��btnAddZ��$��Z-��$��3��$��2��$��Center=�$��Center=�  ControlLib.DoubleUpDown  -   $��dudStepMove��X TXceed.Wpf.Toolkit, Version=2.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4�  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   	Increment$
 0.01X��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Maximum$	 100X�	   Minimum$		 100X�
   Value$
 1X�$��2��$��3��$��Right=�$��Center=�$��60�� � ToolTip$ 移动步长��  -   $��dudStepAngle��$
 0.01X�$	 100X�$		 100X�$
 1X�$��3��$��3��$��Right=�$��Center=�$��60��$ 角度步长��