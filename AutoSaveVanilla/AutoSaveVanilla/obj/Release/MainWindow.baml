   M S B A M L   `   `   `  ����   AutoSaveVanilla     AutoSaveVanilla.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$:  4Auto Save for Minecraft Vanilla Server - Mitko Nikov��$	��160��$	��525�� � 
ResizeMode$ NoResize=����	#FF535353 � Icon$ ForIcon.ico��.��� $��	0,0,0,-28q� � ColumnDefinitions �� $
��307* ��� $
��212* �.+��� -   $��worldBox��$
��Left=�$��23��$��	130,6,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��302��$��2���� -   $��	backupBox��$
��Left=�$��23��$��
130,34,0,0q�$
 Wrap=�$	��Top=�$	��302��$��2���� -   $��Browse��$
��Left=�$��	135,8,0,0q�$	��Top=�$��65��$��1���� -   $��Browse��$
��Left=�$��
135,34,0,0q�$	��Top=�$��65��$��1���� $��World to auto-save:��$
��Left=�$��10,6,0,0q�$	��Top=��� $��Backup location:��$
��Left=�$��	10,32,0,0q�$	��Top=��� $��	On every ��$
��Left=�$��	10,62,0,0q�$	��Top=��� -   $��AutoSaveTimeBox��$
��Left=�$��23��$��	76,63,0,0q�$
 Wrap=�$	��Top=�$��49���� $��minutes��$
��Left=�$��
130,62,0,0q�$	��Top=� J� RenderTransformOrigin$ 	2.148,0.5&��� -   $��saveBtn��$��Start Auto-Saving��$
��Left=�$��	76,62,0,0q�$	��Top=�$	��124��$��1���� -   $��checkBox��$��Until stopped manualy��$
��Left=�$��	10,91,0,0q�$	��Top=�$��24���� -	   $��AutoSaveLabel��$��	Auto-Save��$
��Left=�$��
182,86,0,0q�$	��Top=�{�.  �� -
    ��Name$ AutoSaveCounterBox��$
��Left=�$��23��$��
252,86,0,0q�$
 Wrap=�$	��Top=�$��49��{�.  �� -   $��
TimesLabel��$��times��$
��Left=�$��
301,86,0,0q�$	��Top=�{�.  $��2���� -   $��CounterLabel��$��
Counter: 0��$
��Left=�$��
135,89,0,0q�$	��Top=�$��1��