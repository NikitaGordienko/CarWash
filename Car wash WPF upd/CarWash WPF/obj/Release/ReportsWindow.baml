   M S B A M L   `   `   `  ���� F  BCarWash WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CarWash_WPFCarWash_WPF      CarWash_WPF.ReportsWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:CarWash_WPF       � 
ResizeMode$  NoResize=� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � Title$ ReportsWindow��$	��600��$	��800��.��� -    �Name$ ReportFormWindow��.+�� -   $��28��$��	0,0,0,572q�$��Bottom=����	#FF272727.+��� -    ��Name$ btnClose��$	����$��	768,3,6,0q�$	��Top=�$��0q�$��22�����	#FF272727$��Segoe MDL2 Assets)�$��16(�
��������� -   $ btnMinimize��$��0��$
��Left=�$��	735,3,0,0q�$��26��$��0q����	#FF272727$��Marlett)�$��20(�
�������$��22��$	��Top=��� -    ��Name$ rwTab��$
��Left=�$	��549��$��0,51,0,0q�$	��Top=�$	��800��.@��� $(��"Отчеты по клиентам��.��� -   $ ClientsRepPage��.+��� $��с��$
��Left=�$��	10,53,0,0q�$	��Top=�$��Microsoft JhengHei Light)�$��16(��� $
��по��$
��Left=�$��
151,53,0,0q�$	��Top=�$��Microsoft JhengHei Light)�$��16(��� -   $ ShowClientByRegDate��$��Отобразить��$
��Left=�$��
332,56,0,0q�$	��Top=�$	��124�����	#FF007ACC
�������$��30��$��Microsoft JhengHei Light)�$��15(�$��0q�(  "System.Windows.Controls.DatePicker  -     Name$ datePickerForClientsFrom��$
��Left=�$��	39,58,0,0q�$	��Top=�  -	   $ datePickerForClientsTo��$
��Left=�$��
193,58,0,0q�$	��Top=�&   System.Windows.Controls.DataGrid  -
     Name$ DGClientsByDate��$
��Left=�$	��401��$��
10,110,0,0q�$	��Top=�$	��774��
��������� -   $ FormClientsExcelReport��$��Отчет в Excel��$
��Left=�$��
494,49,0,0q�$	��Top=�$	��124�����	#FF007ACC
�������$��0q�$��45��$��15(�$��Microsoft JhengHei Light)��� $&�� Отчеты по записям��
�������.��� -   $ AppointmentsRepPage��.+�  -   $" DGAppointmentsByDateAndPrice��$
��Left=�$	��401��$��
10,110,0,0q�$	��Top=�$	��774��
�������$��Microsoft JhengHei Light)�$��14(��� -   	 ��Name$	 cbTurnOnDateSort��$�� ��$
��Left=�$��	10,23,0,0q�$	��Top=�  -   $  datePickerAppointmentsFrom��$
��Left=�$��	47,16,0,0q�$	��Top=�$��Microsoft JhengHei Light)�$��14(�$��1q�  -   $ datePickerAppointmentsTo��$
��Left=�$��
187,16,0,0q�$	��Top=�$��Microsoft JhengHei Light)�$��14(��� -   $	 cbTurnOnPriceSort��$�� ��$
��Left=�$��	10,66,0,0q�$	��Top=��� $��с��$
��Left=�$��	27,14,0,0q�$	��Top=�$��14(�$��Microsoft JhengHei Light)��� $
��по��$
��Left=�$��
160,15,0,0q�$	��Top=�$��14(�$��Microsoft JhengHei Light)��� -   
 ��Name$
 SignForPriceSort��$
��Left=�$��	90,61,0,0q�$	��Top=�$��49��$��Microsoft JhengHei Light)�$��14(��� $��Цена��$
��Left=�$��	41,58,0,0q�$	��Top=�$��14(�$��Microsoft JhengHei Light)��� -   $$ ShowAppointmentsByDateAndPrice��$��Отобразить��$
��Left=�$��
679,14,0,0q�$	��Top=�$	��105�����	#FF007ACC
�������$��0q�$��29��$��15(�$��Microsoft JhengHei Light)��� -    ��Name$ PriceBox��$
��Left=�$��25��$��
156,61,0,0q� �� TextWrapping$ NoWrap=�$�� ��$	��Top=�$��80��$��Center=�$��Microsoft JhengHei Light)�$��14(� �� 	MaxLength$ 4���� -   $! FormAppointmentsExcelReport��$��Отчет в Excel��$
��Left=�$��
679,54,0,0q�$	��Top=�$	��105�����	#FF007ACC
�������$��0q�$��40��$��15(�$��Microsoft JhengHei Light)��� $��Диагностика��$
��Left=�$��
330,15,0,0q�$	��Top=�$��14(�$��Microsoft JhengHei Light)��� $%��Чистка интерьера��$
��Left=�$��
330,58,0,0q�$	��Top=�$��Microsoft JhengHei Light)�$��14(��� -   $	 cbDiagnosticsCheck��$�� ��$
��Left=�$��
458,16,0,0q�$	��Top=�$��16�� � LayoutTransform ��  �� ScaleX$ 2Q� �� ScaleY$ 2Q��� -   $	 cbTurnOnInterior��$�� ��$
��Left=�$��
314,66,0,0q�$	��Top=��� -   $	 cbInteriorCheck��$�� ��$
��Left=�$��
458,58,0,0q�$	��Top=�$��16�� �� $ 2Q�$ 2Q��� -   $	 cbTurnOnDiagnostics��$�� ��$
��Left=�$��
314,23,0,0q�$	��Top=��� ���	#FFF4F4F5$
��Left=�$��98��$��	307,5,0,0q�
���   �$	��Top=�$��1�� J� Opacity$	 0.2Q��� ���	#FFF4F4F5$
��Left=�$��98��$��	496,5,0,0q�
���   �$	��Top=�$��1��$	 0.2Q��� $��Номер бокса��$
��Left=�$��
520,15,0,0q�$	��Top=�$��14(�$��Microsoft JhengHei Light)��� $��Класс машины��$
��Left=�$��
520,58,0,0q�$	��Top=�$��Microsoft JhengHei Light)�$��14(��� -   $	 cbTurnOnClass��$�� ��$
��Left=�$��
504,66,0,0q�$	��Top=��� -   $	 cbTurnOnBox��$�� ��$
��Left=�$��
504,23,0,0q�$	��Top=��� -   $ BoxBox��$
��Left=�$��24��$��
630,19,0,0q�$
 Wrap=�$	��Top=�$��30��$��14(�$��Microsoft JhengHei Light)�$ 1��$��Center=��� -   $ ClassBox��$
��Left=�$��24��$��
630,62,0,0q�$
 Wrap=�$	��Top=�$��30��$��14(�$��Microsoft JhengHei Light)�$ 1��$��Center=��� ���	#FFF4F4F5$
��Left=�$��98��$��	669,5,0,0q�
���   �$	��Top=�$��1��$	 0.2Q��� $&�� Отчеты по отзывам��
�������.��� -   $ FeedbackRepPage��.+�  -   $ DGFeedbackByRate��$
��Left=�$	��401��$��
10,110,0,0q�$	��Top=�$	��774��
��������� $��Оценка��$
��Left=�$��	10,61,0,0q�$	��Top=�$��Microsoft JhengHei Light)�$��14(��� -    $
 SignForRateSort��$
��Left=�$��	77,64,0,0q�$	��Top=�$��43��$��26���� -!   $ RateBox��$
��Left=�$��26��$��
136,64,0,0q�$ NoWrap=�$�� ��$	��Top=�$��29��$��Center=�$ 1��$��Microsoft JhengHei Light)�$��14(��� -"   $ ShowFeedBackByRate��$��Отобразить��$
��Left=�$��
228,61,0,0q�$	��Top=�$	��111�����	#FF007ACC
�������$��0q�$��29��$��15(�$��Microsoft JhengHei Light)��� -#   $ FormFeedbackExcelReport��$��Отчет в Excel��$
��Left=�$��
398,53,0,0q�$	��Top=�$	��135�����	#FF007ACC
�������$��0q�$��45��$��15(�$��Microsoft JhengHei Light)�