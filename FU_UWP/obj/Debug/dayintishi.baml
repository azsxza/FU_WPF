   M S B A M L   `   `   `  ����A  =FU_UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:FU_UWPFU_UWP  5   	       FU_UWP.dayintishi   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:FU_UWP     5   	     � Title$  
dayintishi��5   	   $
��1080��6   $
��1920��6*    � WindowState$ Normal=�67    � AllowsTransparency .  � Topmost .  � 
ResizeMode$ NoResize=�5	   	    � WindowStyle$
 None=�6    � Left$	 0.0��62   
 � Top$	 0.0��6=   
������  � 	Resources 5
         FocusVisual	 �� Property
 �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonStyle1 �� 
TargetType �� 
TargetType ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName��%  &	        &	 �     &	 �     &	 �     &	     &	 6    &	 Y    &	 |    &	 �    &		 �    &	
 �    7   7  7  7  7  7 	 �� 5   
   .�5      �� 	 � ��
 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ 	#FF707070��67   �� 5   
   $ 	#FFBEE6FD��6>   �� 5   
   $ 	#FF3C7FB1��6:   �� 5   
   $ 	#FFC4E5F6��6<   �� 5   
   $ 	#FF2C628B��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6%   .�5      �� 	 �@�FocusVisualStyle8
   61   �� 5      	 � ��8
  6+   �� 5       	 � ��8
  6,   �� 5!      	 � ��#
 � ��6+   �� 5"      	 � ��$
 1q�60   �� 5#      	 � ��$
 Center=�6;   �� 5$      	 � ��$
 Center=�69   �� 5%      	 � ��$
 1q�6(   �� 5&      	 � ��
 5'      �� 5(      " ��6&   .R�5)      �� $ border��6!   #��z��61   #��z��6]   #��z��6�    . ._�5*      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5+       5,      P� 5-       �@��IsDefaulted . . �5.   "   �� $ border��6@   #
 � ��6T   	 � ��5/      P� 50       � {� .  . �51   "   �� $ border��6?   8
  	 � ��6S   �� 52   "   $ border��6@   8
  	 � ��6T   �� 53   "   $ contentPresenter��6K   8
  	 � ��6i   54      56      58      59      5:      .��5;      � -   5         �Name$ 	main_grid��5;       J� Opacity$ 1Q�6   ���	#99000000.+�5<   
   � $
��Left=�6   $	��354��6*   $��612,370,0,0q�67   $	��Top=�6L   $	��694��6d   ��5=      �� 5>       �� ImageSource$' !images/组成部分/提示框.png��6   .+�5@      �� -   5         ��Name$ button��5@      $�� ��6%   $
��Left=�60   $��
570,10,0,0q�6K   $	��Top=�6_   $	��100��6w   $	��100��6�   #��� 
 6�   ���	#00707070���	#00000000��5A      �� 5B      $ images/图标/×.png��6!   5D      �� 5E      -   5         ��Name$ 	textBlock��5E      $
��Left=�6+   $��
44,110,0,0q�6F   $
��Wrap=�6Z   $	��Top=�6n   $	��136��6�   $	��598��6�   $��FZZhongQian-M16S)�6�   $��36(�6�   �� 6�   $A�;正在打印中...请在30秒后取出照片，点击关闭��6�   �� 6�   $	�后��6�   �� 6   $!�将会返回至主页面。��6  6  5F      5G      5H      