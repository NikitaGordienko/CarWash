   M S B A M L   `   `   `  ����F  BCarWash WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CarWash_WPFCarWash_WPF  5   	       CarWash_WPF.SignUp   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:CarWash_WPF     5   	     � 
ResizeMode$  NoResize=�5   	    � WindowStyle$
 None=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � Title$ Регистрация��5   	   $	��720��6   $
��1280��6*   ��5      �� 5   
    �� ImageSource$&  Resources/carwash_background.jpg��6    j� Stretch$
 None=�6D   .��5      � $��390,74,390,46q�6   
�������.+�5   
   �� -   5         ��Name$ 	btnSignUp��5      $*��$Зарегистрироваться��6$   $��129,482,129,0q�6A   $	��Top=�6X   $��22(�6p   $��Microsoft JhengHei Light)�6~   $��52��6�   ���	#FF007ACC$��1q�6�   
�������6�   �� 5   
   -   5         ��Name$ lblLogin��5      $��Новый логин��6"   $��70,111,214,0q�68   $	��Top=�6N   $��22(�6f   $��Microsoft JhengHei Light)�6t   �� 5   
   -   5        $ lblPassword��5      $��Новый пароль��6%   $��70,198,214,0q�6<   $	��Top=�6R   $��22(�6j   $��Microsoft JhengHei Light)�6x   �� 5   
   -   5         ��Name$ 	textLogin��5      $��36��6%   $��70,155,70,0q�61   	 �� TextWrapping$	 NoWrap=�6F   $	��Top=�6\   $��Microsoft JhengHei Light)�6t   $��18(�6�   $��1q�6�   $��Center=�6�   �� 5   
   $��75��6   $��70,22,174,0q�6   $	��Top=�61   $��Resources/ufrf_logo.png��6I   G� 5   
   -   5        
 G�Name$
 textPassword��5      $��70,242,70,0q�6,   $	��Top=�6A   $��36��6Y   $��18(�6e   $��Center=�6s   $��Microsoft JhengHei Light)�6�   �� 5   
   -   5        $ textFio��5      $
��Left=�6#   $��36��6>   $��
70,327,0,0q�6J   $	 NoWrap=�6^   $	��Top=�6t   $	��354��6�   $��Center=�6�   $��18(�6�   $��Microsoft JhengHei Light)�6�   �� 5   
   -   5        $ lblFio��5      $��ФИО��6    $
��Left=�6.   $��
70,283,0,0q�6I   $	��Top=�6]   $��Microsoft JhengHei Light)�6u   $��22(�6�   $	��210��6�   �� 5   
   $'��!Электронная почта��6   $
��Left=�6,   $��
70,368,0,0q�6G   $	��Top=�6[   $��22(�6s   $��Microsoft JhengHei Light)�6�   �� 5   
   -   5        $ 	textEmail��5      $
��Left=�6%   $��36��6@   $��
70,412,0,0q�6L   $	 NoWrap=�6`   $	��Top=�6v   $	��354��6�   $��Center=�6�   $��18(�6�   $��Microsoft JhengHei Light)�6�   � 5   
   -	   5        $
��Left=�5      $��28��6*   $��-390,-75,-390,647q�66   $��Bottom=�6Q   $
��1280��6l   ���	#FF272727.+�5      �� -
   5        $ btnClose��5      $	����6'   $
��Left=�6:   $��
1248,3,6,0q�6U   $	��Top=�6i   $��26��6�   $��0q�6�   $��22��6�   ���	#FF272727$��Segoe MDL2 Assets)�6�   $��16(�6�   
�������6  �� 5      -   5        $ btnMinimize��5      $��0��6*   $
��Left=�66   $��
1215,3,0,0q�6Q   $��26��6e   $��0q�6p   ���	#FF272727$��Marlett)�6�   $��20(�6�   
�������$��22��6�   $	��Top=�6�   6�   5      5      5       