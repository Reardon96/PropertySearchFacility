   M S B A M L   `   `   `  ���� U  QSykesCottagesInterviewTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SykesCottagesInterviewTestSykesCottagesInterviewTest  +    %SykesCottagesInterviewTest.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:SykesCottagesInterviewTest       � Title$  
MainWindow��$	��600��$	��720�� � 
ResizeMode$ CanMinimize=�.���  � ColumnDefinitions �� $��* ��� $��* ��� $
��auto ��� $��* ��� $��* � � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $��* �.+��� $��0��$��0��$��5��$��Center=�$��Center=�$$��SYKES COTTAGES SEARCH FACILITY��$
��0,20q�$��20(�$
��Bold"��� $��1��$��0��$��5��$	��Top=� �� Orientation$ 
Horizontal=�$��0,0,0,40q�$��Center=�.+��� $
��10,0q�.+��� $��Location Search��$
��0,10q��� -    ��Name$ LocationSearchFilter���� $
��10,0q�.+��� $��
Near Beach��$
��0,10q��� -    ��Name$ NearBeachFilter��.@��� $	��Yes���� $��No�� �� 
IsSelected$
 True���� $
��10,0q�.+��� $��Accepts Pets��$
��0,10q��� -   $ AcceptsPetsFilter��.@��� $	��Yes���� $��No��$
 True���� $
��10,0q�.+��� $��Sleeps (Minimum)��$
��0,10q��� -   $ SleepsMinimumFilter���� $
��10,0q�.+��� $��Beds (Minimum)��$
��0,10q��� -   $ BedsMinimumFilter���� $
��10,0q�.+��� $��Available From��$
��0,10q�(  "System.Windows.Controls.DatePicker  -   $��AvailabilityFromFilter���� $
��10,0q�.+��� $�� ��$
��0,10q��� -    ��Name$ FilterSearch��$��Search��&   System.Windows.Controls.DataGrid  -   	  Name$	 PropertyGrid��$��2��$��2��
   
IsReadOnly$

 True��� $��3��$��5��$��Bottom=�$��0,0,0,10q�.+��� $ 
Horizontal=�$��Center=�.+��� -	   $��btnFirst��$��23��$
��Left=�$��Bottom=�$��40��$��<<�� J� Opacity$
 0.75Q��� -
   $��btnPrev��$��23��$��Bottom=�$��40��$
 0.75Q�$��<���� -    ��Name$ lblpageInformation��$��Center=�$
��Left=�$��30��$	��100��$��Center=�$��Bottom=��� -   $��btnNext��$��23��$��Right=�$��Bottom=�$��40��$��>��$
 0.75Q��� -   $��btnLast��$��23��$��Right=�$��Bottom=�$��40��$
 0.75Q�$��>>���� -   $ cbNumberOfRecords��$
��Left=�$��Bottom=�$	��120�� �� SelectedIndex$ 0��$
 0.75Q�� $��4��.+��� $ 
Horizontal=�$��Center=�$��412,62,430,-62q�.+��� -   $��	btnCancel��$��30��$��Center=�$��Bottom=�$��50��$
 0.75Q�$��Cancel��