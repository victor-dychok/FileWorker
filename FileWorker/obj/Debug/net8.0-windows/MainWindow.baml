   M S B A M L   `   `   `  ����E  AFileWorker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:FileWorker
FileWorker  5   	       FileWorker.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:FileWorker     5   	     � Title$  
MainWindow��5   	   $	��730��6   $	��400��6)   .��5      �� .+�5   
   � $��20 10q�6   .+�5      �� $��Files created:��6   $��Center=�5      $��18(�5      �� 5      $��Right=�5      $��Center=�5      $��22(�5      ��5      �� *NumberOfCreatedFiles+5      �� 5   
   -   5         ��Name$ CreateFilesButton��5      $��Create files��5      $��18(�5      $��10q�5      $	��200��5      ��5      �� *CreateFiles+�� 5!   
   $��18(�6   $��Delete strings with text:��5"      $��20 10q�5#      �� 5%   
   $��18(�6   $
��20 0q�5&      ��5'      �� *TextToDelete+� 5)   
   $��20 10q�6   .+�5+      �� $��Files joined:��6   $��Center=�5,      $��18(�5-      �� 5/      $��Right=�50      $��Center=�51      $��22(�52      ��53      �� *NumberOfJoinedFiles+55      � 56   
   $
��20 0q�6   .+�58      �� $��Strings deleted:��6   $��Center=�59      $��18(�5:      �� 5<      $��Right=�5=      $��Center=�5>      $��22(�5?      ��5@      �� *NumberOfDeletedStrings+5B      �� 5E   
   $��
Join files��6   $��18(�5F      $��10q�5G      $	��200��5H      ��5I      �� *	JoinFiles+� 5K   
   $��
20 40 20 0q�6   .+�5N      �� $��Files exported:��6   $��Center=�5O      $��18(�5P      �� 5R      $��Right=�6   $��Center=�5S      $��22(�5T      ��5U      �� *ExportedFiles+5W      � 5Y   
   $
��20 0q�6   .+�5\      �� $��Strings exported:��6   $��Center=�5]      $��18(�5^      �� 5`      $��Right=�6   $��Center=�5a      $��22(�5b      ��5c      �� *ExportedStrings+5e      � 5h   
   $
��20 0q�6   .+�5k      �� $��Strings deleted:��6   $��Center=�5l      $��18(�5m      �� 5o      $��Right=�5p      $��Center=�5q      $��22(�5r      ��5s      �� *!NumberOfDeletedStrings_DBExport+5u      �� 5w   
   $��18(�5x      $��20q�5y      ��5z      �� *ExportingToDatabaseStatus+�� 5|   
   $��Export to DB��6   $��18(�5}      $��20q�5~      $	��200��5      ��5�      �� *ExportFiles+�� 5�   
   $��18(�5�      $��
20 20 20 0q�5�      ��5�      �� *CountStatus+�� 5�   
   $��Request Procedure��6   $��18(�5�      $	��200��5�      ��5�      �� *RequestProcedure+5�      5�      