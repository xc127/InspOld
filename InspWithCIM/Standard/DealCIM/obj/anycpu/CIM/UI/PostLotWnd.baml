   M S B A M L   `   `   `  ���� D  @Panuon.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:Panuon.UI;assembly=Panuon.UI	Panuon.UI  B >DealCIM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DealCIMDealCIM    DealCIM.PostLotWnd   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9PU*clr-namespace:Panuon.UI;assembly=Panuon.UI     'localclr-namespace:DealCIM     -     � Title$  
PostLotWnd��$	��350��$	��650��$��20q� � AllowsTransparency .  � WindowStyle$
 None=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 15Q�   Opacity$	 0.3Q�   ShadowDepth$ 1Q�.���  +� FocusedElement ��  �� ElementName$ tbLot��	 � RowDefinitions	 �� $��* ��� $��8* �.+�� $��0��.+��� 
����d�$��Stretch=�$��Stretch=�$��2���� -   $��10,0,0,0q�$��RunCard��$��18(�
�������$��Center=�$��Black"�$��2��� $��1��$��	10,0,10,0q�   $ 4Q�
   Color$
 	#FFF0EBEB��$	 0.8Q� � ColumnDefinitions �� $��* ��� $
��1.5* ��� $��* ��� $
��1.5* �	 �� $��* ��� $��6 ��� $��* ��� $��6 ��� $��* ��� $��6 ��� $��* �.+��� $
��Fab:��$��2��$��0��$��Center=�$��Center=�$��Black"�$��20(��� -    ��Name$ tbFab��$��3��$��0��
�������$	��120��$��Center=�$��20(�$��Center=�$��Center=����� *StrFab+ �� Mode$ OneWay=��� $��Area:��$��0��$��0��$��Center=�$��Center=�$��Black"�$��20(��� -   $ tbArea��$��1��$��0��
�������$	��120��$��Center=�$��20(�$��Center=�$��Center=����� *	StrArea+$ OneWay=��� $��Line:��$��2��$��2��$��Center=�$��Center=�$��Black"�$��20(��� -   $ tbLine��$��3��$��2��
�������$	��120��$��Center=�$��20(�$��Center=�$��Center=����� *	StrLine+$ OneWay=��� $��
Operation:��$��0��$��2��$��Center=�$��Center=�$��Black"�$��20(��� -   $ tbOperation��$��1��$��2��
�������$	��120��$��Center=�$��20(�$��Center=�$��Center=����� *StrOperation+$ OneWay=��� $��3��$��4��$��	10,0,10,0q�
����d�$��5q��� $��UserID:��$��0��$��4��$��Center=�$��Center=�$��Black"�$��20(��� -    ��Name$ tbUserID��$��1��$��4��$	��150��$��Center=�$��20(�$��Center=�$��Center=����� *	StrUserID+$ TwoWay=��� $��ModelNo:��$��2��$��4��$��Center=�$��Center=�$��Black"�$��20(��� -   $ 	tbModelNo��$��3��$��4��$	��150��$��Center=�$��20(�$��Center=�$��Center=����� *
StrModelNo+$ TwoWay=��� $
��Lot:��$��6��$��Center=�$��Center=�$��Black"�$��20(��� -	   $��tbLot��$��6��$��1��$��2��$��20(�$
��Left=�$��Center=�$��Center=�$	��180��$��40��   Panuon.UI.PUButton  -
   $��
btnPostLot��$��上 报��   ButtonStyle$ Hollow=�
����d�   BorderCornerRadius$ 3��   
CoverBrush
 ��d�
����d�$��6��$��2��$	��100��$��Black"�$��16(�$��32��$��Center=�$
��Left=�$��60,0,0,0q�$��2��  -   $��	btnCancel��$��退 出��$ Outline=�
����d�$ 3��
 ��d�
����d�$��6��$��3��$	��100��$��16(�$��32��$��Center=�$��Right=�$��0,0,20,0q�