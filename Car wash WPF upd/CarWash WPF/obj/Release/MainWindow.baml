   M S B A M L   `   `   `  ���� F  BCarWash WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CarWash_WPFCarWash_WPF      CarWash_WPF.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:CarWash_WPF       � 
ResizeMode$  NoResize=� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � Title$ Главное меню��$	��720��$
��1280����Z� .��� -    �Name$ MainMenuWindow��
�������.+�� -   $
��Left=�$��28��$��	0,0,0,692q�$��Bottom=�$
��1280�����	#FF272727.+��� -    ��Name$ btnClose��$	����$��
1248,3,0,0q�$	��Top=�$��0q�$��22�����	#FF272727$��Segoe MDL2 Assets)�$��16(�
�������$
��Left=�$��26���� -   $ btnMinimize��$��0��$
��Left=�$��
1215,3,0,0q�$��26��$��0q����	#FF272727$��Marlett)�$��20(�
�������$��22��$	��Top=��� $
��Left=�$��80��$��	30,33,0,0q�$	��Top=�$	��250��$��Resources/ufrf_logo.png���� -   $ 
btnClients��$��Клиенты��$
��Left=�$��30,145,0,490q�$	��250��$��Microsoft JhengHei Light)�$��36(���Z� �� -   $ btnAppointments��$��Записи��$
��Left=�$��30,248,0,387q�$	��250��$��Microsoft JhengHei Light)�$��36(���Z� �� -   $ 
btnReports��$��Отчеты��$
��Left=�$��30,454,0,182q�$	��250��$��Microsoft JhengHei Light)�$��36(���Z� �� -   $ btnFeedback��$��Отзывы��$
��Left=�$��30,351,0,285q�$	��250��$��Microsoft JhengHei Light)�$��36(���Z� �� -	    ��Name$ mwTab��$	��677��$��322,33,28,0q�$	��Top=���Z� .@��� -
    ��Name$ 
tabClients��$��Клиенты��$��-40,-31,36,29q�.��� -   $ pageClients��
�������.+�&   System.Windows.Controls.DataGrid  -     Name$ 	DGClients��$
��Left=�$	��566��$	��Top=�$	��924��$��15(�$��Microsoft JhengHei Light)�	   CanUserResizeColumns	 .  
   CanUserReorderColumns
 .  ���	#FF878787�� -   $ btnChangeClients��$��Изменить��$
��Left=�$��	0,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ btnDeleteClients��$��Удалить��$
��Left=�$��195,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ btnApplyClientChanges��$��Применить��$
��Left=�$��389,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ btnSendClientQuery��$��Отправить��$
��Left=�$��582,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ tabAppointments��$��Записи��$��-37,-31,33,29q�.��� -   $ pageAppointments��
�������.+�  -   $ DGAppointments��$
��Left=�$	��566��$	��Top=�$	��924��$��Microsoft JhengHei Light)�$��15(�
�������   SelectionUnit$ FullRow=��� -   $ btnChangeAppointments��$��Изменить��$
��Left=�$��	0,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ btnDeleteAppointments��$��Удалить��$
��Left=�$��195,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $  btnApplyAppointmentChanges��$��Применить��$
��Left=�$��389,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ btnSendAppointmentQuery��$��Отправить��$
��Left=�$��582,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ tabFeedback��$��Отзывы��$��-33,-31,29,29q����	#FF878787.��� -   $ pageFeedback��
�������.+�  -   $ 
DGFeedback��$
��Left=�$	��566��$	��Top=�$	��924��$��15(�$��Microsoft JhengHei Light)����	#FF878787�� -   $ btnChangeFeedback��$��Изменить��$
��Left=�$��	0,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ btnDeleteFeedback��$��Удалить��$
��Left=�$��195,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ btnApplyFeedbackChanges��$��Применить��$
��Left=�$��389,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ btnSendFeedbackQuery��$��Отправить��$
��Left=�$��582,588,0,0q�$	��Top=�$	��154�����	#FF007ACC
�������$��39��$��Microsoft JhengHei Light)�$��18(�$��0q��� -   $ 
tabReports��$��Отчеты��$��-29,-31,25,29q����	#FF878787.��� -    $ pageReports���� ���	#FF878787$
��Left=�$	��564��$��
301,59,0,0q�
���   �$	��Top=�$��1�� J� Opacity$	 0.2Q�