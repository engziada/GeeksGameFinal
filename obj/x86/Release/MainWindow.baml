   M S B A M L   `   `   `  ����   DragAndDropControls(    "DragAndDropApp.DragAndDropControls   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  
mainWindow�� � Title$ Geeks' Game��$	��782��$	��376�� � WindowStyle$
 None=�$��2q� � Icon$ Box.png�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � 
ResizeMode$ CanMinimize=�.��� -    �Name$ 	grid_Main�����	#FF3F3B3B � RowDefinitions �� $	��20* ��� $
��118* ��� $	��69* ��� $
��114* ��� $	��26* �	 � ColumnDefinitions	 �� $��0* ��� $	��88* ��� $	��61* ��� $
��171* ��� $	��57* ��� $	��11* �.+� � -   
  �Name$
 dz_Korea��
 � Tag$ Korean��$��1��$��4��$��	0,10,10,0q� K� ZIndex$ 2��$��Right=�$��100.0��$��100.0��$	��Top=�����  �� ImageSource$ KoreanBox.png�� � -   $
 dz_China��$ Chinese��$��1��$��2��$��	10,10,0,0q�$ 2��$
��Left=�$��100.0��$��100.0��$	��Top=����� $ ChineseBox.png�� � -   $
 dz_Japan��$ Japanese��$��3��$��2��$��	10,0,0,10q�$ 2��$
��Left=�$��100.0��$��100.0��$��Bottom=����� $ JapaneseBox.png�� � -   $
 dz_Thailand��$
 Thai��$��3��$��4��$��	0,0,10,10q�$ 2��$��Right=�$��100.0��$��100.0��$��Bottom=����� $ ThaiBox.png���� -   $��DesigningCanvas��$��1.2,0.4,2.8,-0.2q����	#FFEBEBEB$��4��$��2��$��3��$��1�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�   ShadowDepth$ 10Q�   	Direction$ 45Q��� $��10,0,5.447,0.05q�$��2��$��0��$��3��$��1��.+��� $��Geeks' Game��$��24(�$��Showcard Gothic)�$��Center=�$��Center=�$��Center=�$��Center=���Z�       Color$ White������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ Black�� � Offset$ 0Q�� $ 	#FF5D0C0C��$ 1Q��� -    ��Name$ 	btn_Start��$��Start��$
��32.8��$��10,35,10.247,0q�$��Center=�$��18(�$��Showcard Gothic)��� -	   $ btn_Exit��$
��Exit��$
��32.8��$��10,15,10.247,0q�$��Center=�$��18(�$��Showcard Gothic)��� $��3��$��66.8,0.2,67.4,-0.2q�$��4�� �� Orientation$ 
Horizontal=�$��Center=�$��28��$	��Top=�.+��� $��Time: ��$��Arial Black)�$
��Left=�$��Bottom=�$
��Left=�$��Bottom=�
���   ���Z� �� -
    ��Name$ lbl_Seconds��$��00��$��Arial Black)�$
��Left=�$��Bottom=�$��Right=�$��Bottom=�
���   �$	��0,5q���Z� �� $	�� : ��$��Arial Black)�$
��Left=�$��Bottom=�$��Center=�$��Bottom=�
���   �$	��0,5q���Z� �� -   $ lbl_MilliSeconds��$��00��$��Arial Black)�$
��Left=�$��Bottom=�$��Bottom=�
���   �$	��0,5q���Z� �� $��Score: ��$��Arial Black)�$��Right=�$��Bottom=�$
��Left=�$��Bottom=�
���   �$��35,0,0,0q�$��0,5,5,5q���Z� �� -   $ 	lbl_Score��$��0��$��Arial Black)�$��Right=�$��Bottom=�$��Right=�$��Bottom=�
���   �$	��0,5q���Z� �� $	�� / ��$��Arial Black)�$��Right=�$��Bottom=�$��Center=�$��Bottom=�
���   �$	��0,5q���Z� �� -   $ 	lbl_Total��$��0��$��Arial Black)�$��Right=�$��Bottom=�$
��Left=�$��Bottom=�
���   �$	��0,5q���Z� 