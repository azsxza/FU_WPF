   M S B A M L   `   `   `  ����A  =FU_UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:FU_UWPFU_UWP  5   	       FU_UWP.show_process   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:FU_UWP     5   	     � AllowsTransparency  .  � Title$ Show_Progres��5      $	��200��6%   $	��200��62    � WindowStyle$
 None=�6>   
������  � 	Resources 5	      $  FU_UWP.ValueToProcessConverter   ValueToProcessConverter��%   &	          5
   
   6(   5      .��5      � ���	#00000000.+�5   
   A� 
���   � J� Opacity$	 0.7Q�6!   ��5      �� 5       �� ImageSource$ images/无标题.png��6    �� Opacity$	 0.6Q�6:   6K   5      � 5   
    K� ZIndex$ 1��6    � Minimum$ 0Q�6'   	 � Maximum$		 100Q�5      
 � Value
 5      �� *SuccessRate+ �� Mode$ OneWay=���5      �� 5       �� 
TargetType" �6"   .R�5      �� ��6   �� *Value+ �� 	Converter# [   �� ConverterParameter$	 200��5      5      5      5      