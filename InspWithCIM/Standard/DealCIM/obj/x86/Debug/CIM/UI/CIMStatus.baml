   M S B A M L   `   `   `  ����B  >DealCIM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DealCIMDealCIM  5          DealCIM.CIMStatus   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:DealCIM     5      
�������.��5	      �   � ColumnDefinitions  5
      �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5      �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5      �� $��
CIM连接:��6
   $��0��6   $��0��6(   $��Center=�68   $��Center=�6U   �� 5      -   5        $��lblCIMStatus��5   
   $��	未连接��6   $��0��6,   $��1��69   $��Center=�6I   $��Center=�6f   �� 5      $��二维码连接:��6
   $��0��6   $��2��6(   $��Center=�68   $��Center=�6U   �� 5      -   5        $��lblCodeStatus��5   
   $��	未连接��6   $��0��6-   $��3��6:   $��Center=�6J   $��Center=�6g   �� 5      $��
ArmChipID:��6
   $��1��6   $��0��6,   $��Center=�6<   $��Center=�6Y   �� 5      -   5        $��lblArmChipID��5   
   $
��...:��6   $��1��6-   $��1��6:   $��Center=�6J   $��Center=�6g   �� 5      $��ForkChipID:��6
   $��1��6    $��2��6-   $��Center=�6=   $��Center=�6Z   �� 5      -   5        $��lblForkChipID��5   
   $
��...:��6   $��1��6.   $��3��6;   $��Center=�6K   $��Center=�6h   �� 5      $��
当前Lot:��6
   $��2��6   $��0��6(   $��Center=�68   $��Center=�6U   �� 5      -   5        $��lblLot��5   
   $
��...:��6   $��2��6'   $��1��64   $��Center=�6D   $��Center=�6a   �� 5       $��ChipID结果:��6
   $��2��6   $��2��6+   $��Center=�6;   $��Center=�6X   �� 5!      -   5        $��lblChipIDResult��5!   
   $
��...:��6!   $��2��60   $��3��6=   $��Center=�6M   $��Center=�6j   �� 5"      $��
Lot结果:��6
   $��3��6   $��0��6(   $��Center=�68   $��Center=�6U   �� 5#      -   5        $��lblLotResult��5#   
   $
��...:��6   $��3��6-   $��1��6:   $��Center=�6J   $��Center=�6g   �� 5$      $��Trackout结果:��6
   $��3��6    $��2��6-   $��Center=�6=   $��Center=�6Z   �� 5%      -   5        $��lblTrackoutResult��5%   
   $
��...:��6#   $��3��62   $��3��6?   $��Center=�6O   $��Center=�6l   5&      5'      