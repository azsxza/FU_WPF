   M S B A M L   `   `   `  ���� A  =FU_UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:FU_UWPFU_UWP      FU_UWP.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:FU_UWP     -     � Title$  
MainWindow��$
��1080��$
��1920�� � WindowState$ Normal=� � Topmost .  � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � Left$	 0.0��
 � Top$	 0.0�� � 	Resources    FocusVisual �� Property	 �� Value
 J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ImageButton �� 
TargetType �� 
TargetType ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName   ScrollViewerControlTemplate1 �Name � ColumnDefinitions � RowDefinitions ��Name ��Name �� CanContentScroll �� CanHorizontallyScroll �� CanVerticallyScroll ��Name4  .System.Windows.Automation.AutomationProperties   AutomationId � Cursor  �� ScrollableHeight! � Maximum" � Minimum*# �� #ComputedVerticalScrollBarVisibility$ �� ViewportHeight% �� ViewportSize& � Value' �� Mode( �� RelativeSource) �� ScrollableWidth* �� Orientation,+ �� %ComputedHorizontalScrollBarVisibility, �� ViewportWidth  SliderBackground- �� 
StartPoint. �� EndPoint/ � Offset0 � Color  Slider_RepeatButton  Slider_RepeatButton1  SliderThumb  Slider_Thumb1 �� CornerRadius  Slider_CustomStyle2 J� Effect3  -System.Windows.Media.Effects.DropShadowEffect3   
BlurRadius4   ShadowDepth5 ]�Name6 ]� DecreaseRepeatButton7 ]� IncreaseRepeatButton8 ]� Thumb��%�	  &	        &	 N     &	 c     &	 x     &	 �     &	 �     &	 �     &	 �     &	 �     &		 �     &	
     7   7  7  7  7  7 	 &	 ,    &	 �    &	     7  &	 �    &	     &	 D    7  7  &	 �    7  7  7  �� .���  � ��	 �� .R��� $��2q�
 . #��� ��$��1��$	 1 2R��� $ 	#FFDDDDDD���� $ 	#FF707070���� $ 	#FFBEE6FD���� $ 	#FF3C7FB1���� $ 	#FFC4E5F6���� $ 	#FF2C628B���� $ 	#FFF4F4F4���� $ 	#FFADB2B5���� $ 	#FF838383���� " ��.���  �@�FocusVisualStyle8	   ��  � ��8	  ��  � ��8	  ��  � ��#	 � ����  � ��$	 1q���  � ��$	 Center=���  � ��$	 Center=���  � ��$	 1q���  � ��	 �� " ��.R��� $ border��#��z��#��z��#��z��
 . ._��� $ contentPresenter��|�.  #��z��#��z����. #
 z
 #��z�� P�  �@��IsDefaulted . . ��� $ border��#	 � �� � ��P�  � {� .  . ��� $ border��8	   � ���� $ border��8	   � ���� $ contentPresenter��8	   � ���� " ��.R�� $
 Grid��#��z�� �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $ Corner��$��1��#��� ��$��1���� $! PART_ScrollContentPresenter��# z�� .   .  #��z��#��z��$��0��#��z��$��0���� $ PART_VerticalScrollBar��$ VerticalScrollBar��$ Arrow��$��1��#! z  $" 0Q�$��0��#y�z# #% z$ & �� *VerticalOffset+$' OneWay=�( �� *TemplatedParent+�� $ PART_HorizontalScrollBar��$ HorizontalScrollBar��$ Arrow��$��0��#! z) $" 0Q�$* 
Horizontal=�$��1��#y�z+ #% z, & �� *HorizontalOffset+$' OneWay=�( �� *TemplatedParent+�� $	- 0,0&�$	. 0,1&�.��� $/ 0Q�$0 #59ccfc��� $	/ 0.5Q�$0 #00b3fe��� $/ 1Q�$0 #59ccfc���� " ��.���  � |�	 .  ��  � ��$	 10����  � ��
	 ���� ��  � ��	 �� " ��.R��� 8��  �� " ��.���  � |�	 .  ��  � ��$	 10����  � ��
	 ���� ��  � ��	 �� " ��.R��� 
��������� $	- 0,0&�$	. 0,1&�.��� $/ 0Q�$0 	#FFD9D3E8���� " n�.���  � |�	 .  ��  � ��	 �� " n�.R�� $��20�� �� �� .+��� 8��  �� $��2��$1 4��8�� $��18���� " ��.���  � |�	 .  ��  � ��	 �� " ��.R�� 2   $3 8Q�$4 1Q�.+��� $��1��
���  ��$��0q�$1 0,0,0,0��._�]� $5 
PART_Track��$��1��6 �� 8��  $��Slider.DecreaseLarge��7 �� 8�� $��Slider.IncreaseLarge��8 n� 8�� .��� ���	#FFDFD3D3.+�� -   $ show_bar��$
��Left=�$	��290��$��	0,790,0,0q�$	��Top=�$
��1920�����	#FF7791B6.+�� -   $ lunbo��}�. $
��Left=�$	��200��$��
165,46,0,0q�$	��Top=�$
��1593���� -   9 ��Name$9 FU_NAME��$
��Left=�$	��250��$��	462,0,0,0q�$	��Top=�$
��1000��$'��!images/组成部分/浮生绘.png���� -   : ��Name$: paizhao��; K� ZIndex$; 1��$�� ��$��Center=�$��462,255,1058,0q�$	��Top=�$	��400��$	��400�����	#00707070���	#00000000#��� 
 ���� < �� ImageSource$< images/图标/拍照.png���� -   $: 
shangchuan��$; 1��$�� ��$��Center=�$��1062,250,458,430q�$��Center=�$	��400��$	��400�����	#00707070���	#00000000#��� 
 ���� $< images/图标/上传.png���� -   $9 image1��$
��Left=�$��22��$��	0,438,0,0q�$	��Top=�$
��1920��$'��!images/组成部分/小蓝条.png��$
��Fill=��� -   = ��Name$= paizhaokuang��}�. > J� Opacity$> 0Q�$; 0��$
��Left=�$	��920��$��
350,80,0,0q�$	��Top=�$
��1220������ $'< !images/组成部分/拍照框.png��.+��� -	   $9 image��$
��Left=�$	��900��$��	10,10,0,0q�$	��Top=�$
��1200���� -
   $: chongpai��$; 1��$�� ��$��Center=�$��1920,670,-200,210q�$��Center=�$	��200��$	��200�����	#00707070���	#00000000#��� 
 ���� $< images/图标/重拍.png��� -   $ choose��$
��Left=�$
��1080��$��266,-1080,0,0q�$	��Top=�$	��310�����	#FFBFC1CC.+�� -   $ choose1��$
��Left=�$
��1080��$	��Top=�$	��310��.+��� -   $: fenggemofangbutton��$; 1��$�� ��$
��Left=�$	��Top=�$	��265��$	��265�����	#00707070���	#00000000#��� 
 $��20,0,0,0q����� $$< images/图标/风格模仿.png���� -   $: lvjingbutton��$; 1��$�� ��$
��Left=�$��
20,265,0,0q�$	��Top=�$	��265��$	��265�����	#00707070���	#00000000#��� 
 ���� $< images/图标/滤镜.png���� -   $: tiaozhengbutton��$; 1��$�� ��$
��Left=�$��
20,530,0,0q�$	��Top=�$	��265��$	��265�����	#00707070���	#00000000#��� 
 ���� $$< images/图标/局部调整.png���� -   $: tiezhibutton��$; 1��$�� ��$
��Left=�$��
20,795,0,0q�$	��Top=�$	��265��$	��265�����	#00707070���	#00000000#��� 
 ���� $< images/图标/贴纸.png���� -   ? ��Name$? fenggemofang��@ �� PanningMode$
@ Both=�$
��Left=�$
��1070��$	��Top=�$	��320��$��	310,0,0,0q�#���  .���� -   A ��Name$A stack_fenggemofang��$��0��$	��300���� -   $? lvjing��$
@ Both=�$
��Left=�$
��1070��$	��Top=�$	��320��$��	316,0,0,0q�.���� -   $A stack_lvjing��$��0��$	��300���� -   $? 	tiaozheng��$
@ Both=�$
��Left=�$
��1070��$	��Top=�$	��320��$��	316,0,0,0q����	#FFBFC1CC.���� -   $A stack_tiaozheng��$��0��$	��300���� -   $? tiezhi��$
@ Both=�$
��Left=�$
��1070��$	��Top=�$	��320��$��	310,0,0,0q�.���� -   $A stack_tiezhi��$��0��$	��300�����	#FFBFC1CC�� -   $: chexiao��$; 1��$�� ��$��Center=�$��1470,1080,250,-200q�$��Center=�$	��200��$	��200�����	#00707070���	#00000000#��� 
 ���� $< images/图标/撤销.png��� -   $
 shan��$y�Hidden=�$> 0Q�$
��Left=�$	��900��$��
360,90,0,0q�$	��Top=�$
��1200�����	#BF000000�� -   $: chexiao2��$; 1��$y�Hidden=�$> 0Q�$�� ��$
��Left=�$��
462,10,0,0q�$	��Top=�$	��100��$	��100�����	#00707070���	#00000000#��� 
 ���� $< images/撤销.png���� -   B ��Name$B slider��$> 0Q�$
��Left=�$��806,835,0,0q�$	��Top=�$
��1020��$��38��$
" -100Q�$	! 100Q�#���  