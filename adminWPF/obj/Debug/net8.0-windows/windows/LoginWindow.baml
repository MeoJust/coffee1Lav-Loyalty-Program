   M S B A M L   `   `   `  ����C  ?adminWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:adminWPF.windowsadminWPF.windows  5   	   "    adminWPF.windows.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:adminWPF.windows     5   	     � Title$  LoginWindow��5   	   $	��600��6   $	��920��6*    � WindowStyle$
 None=�5	       � 
ResizeMode$ NoResize=�5
      
������  � AllowsTransparency$
 True��5      .��5      � .+�5   
   ��  �� CornerRadius$ 20��6   $
}�True��6#   ._�5      �� $B��<D:\_art\_csharp\coffeOneLoveProj\_background\02_loginIMG.jpg��6   $
��Fill=�5      5      �� 5   
   -   5        
������ $ 20��5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5       � RowDefinitions 5!      �� 5"      $	��200 �6    �� 5#      .+�5&      �� $��1��5'      $��0��5(      .+�5)      �� $	��200��6    $V��P     Добро пожаловать!
 Введите логин и пароль��5*       $��comic sans ms)�5+       $��55(�5,       
�������5-       5/      �� 51      $��1��6   $��1��6)   .+�53      �� -   5         ��Name$ loginTXT��53       �� TextWrapping$
 Wrap=�54      $�� ��55      $	��120��56      $��0,50,0,0q�57         TextBoxTheme#��[  58      
	 � Tag$	 
логин��59      �� 5:      -   5        $ passwordTXT��5:      $
 Wrap=�5;      $�� ��5<      $	��120��5=      $��0,50,0,0q�5>      #��[  5?      $	 пароль��5@      �� 5B      -   5        
 ��Name$
 loginBTN��5B      $��
Войти��5C      $��Center=�5E      $��	0,111,0,0q�5F      $	��Top=�5G      $��25��5H      ���#99bf28  ButtonTheme#��[ 5J      $��comic sans ms)�5K      $��17(�5L      
�������5M      �� 5O      -   5        $
 exitBTN��5O      $��
ВЫХОД��5P      $��Center=�5R      $��
555,45,0,0q�5S      $	��Top=�5T      $��25��5U      
���  ��$
��Bold"�5W      #��[ 5X      $��comic sans ms)�5Y      $��17(�5Z      
�������5[      5]      5_      5`      5a      5b      