   M S B A M L   `   `   `  ����H  DPBS_Installer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:PBS_InstallerPBS_Installer  5   	       PBS_Installer.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:PBS_Installer     5   	     � Title$  PBS Mod Installer��5   	   $	��350��6#   $	��525��60   .��5	      � -   5         �Name$ PBS_Installer��5	       � ColumnDefinitions 5
   
   �� 5      $	��43* �6   �� 5      $
��474* �6   .+�5   
   �� -   5         ��Name$
 Tabs��5      $
��Left=�6#   $	��319��6>   $	��Top=�6K   $	��517��6c   $��2��6o   .@�5      �� $��Welcome��6   .��5      � ���	#FFE5E5E5.+�5      �� $
��Left=�6    $��	53,10,0,0q�6;   $
��Wrap=�6N   $	��Top=�6b   $	��229��6z   $	��376��6�   �� 6�   $E�?Welcome to the New Playable Subs mod installer for Cold Waters.��6�   �� 6�   �� 6�   �� 6�   �� 6�   $���This installer will install the core files for the mod, which includes all of the vanilla playable subs as well as a few new ones that are required by the single missions and campaigns. You may also choose additional optional submarines to install on the "Select Additional Submarines" tab. The installer will automatically add those optional subs to all of the single missions (except special missions) and all of the campaigns.��6  �� 6�  �� 6�  �� 6�  �� 6�  $���If you spot any issues or bugs with the mod, please visit the Github page where you downloaded this installer and report the problem on the Issues page, so the mod devs can diagnose and fix it. If you'd like to request that a new sub be added to the optional sub list, you can make a request on the same Issues page on Github.��6�  67  �� 5      -   5         ��Name$ WelcomeNextButton��5      $
��Next��68   $
��Left=�6G   $	��Top=�6b   $��75��6z   $��412,261,0,0q�6�   6�   5      5      �� 5      -   5         ��Name$ 
SelectSubs��5      $"��Select Additional Submarines��6*   .��5      � -   5        $ SubmarinesListBox��5      ���	#FFE5E5E5 5      �� 5      $
��202* �6+   �� 5      $	��60* �6+   �� 5      $	��36* �6+   �� 5      $��6* �6+   �� 5      $��6* �6+   �� 5      $
��201* �6+   .+�5      �� -   5         ��Name$ SubmarineListBox��5      $
��Left=�68   $	��246��6S   $	��Top=�6`   $	��242��6x   $��	10,10,0,0q�6�   $��2��6�    �� SelectionMode$ Multiple=�6�   �� 5       -   5        $ SelectSubmarinesApply��5       $��Apply��6<   $
��Left=�6L   $	��Top=�6g   $��54��6   $��198,261,0,0q�6�   $��2��6�   �� 5!      $��2��6    $
��Left=�60   $��	10,10,0,0q�6K   $
��Wrap=�6^   $����Select any additional subs you wish to install (or choose "Select All" to install all additional subs) and then click "Apply" to finalize your selection.��6r   $	��Top=�6'  $	��189��6?  $	��238��6L  $��4��6X  �� 5"      -   5        $ SelectAllSubsButton��5"      $��
Select All��6:   $
��Left=�6O   $��
27,261,0,0q�6j   $	��Top=�6~   $��75��6�   6�   �� 5#      -	   5        $ SubSelectNextButton��5#      $
��Next��6:   $��5��6I   $
��Left=�6Y   $��116,261,0,0q�6t   $	��Top=�6�   $��75��6�   6�   5$      5&      �� 5'      $��Install/Uninstall��6   .��5(      � ���	#FFE5E5E5 5)      �� 5*      $	��37* �6+   �� 5+      $	��14* �6+   �� 5,      $	��68* �6+   �� 5-      $
��241* �6+   �� 5.      $	��30* �6+   �� 5/      $
��121* �6+   .+�51      �� $
��Left=�6    $��	6,139,0,0q�6;   $
��Wrap=�6N   $	��Top=�6b   $	��342��6z   $��4��6�   $��1��6�   $���{The default Cold Waters installation directory is chosen automatically, but you can change the installation directory here:��6�   �� 52      -
   5         ��Name$ 
FolderPath��52       $
��Left=�64   $��	6,192,0,0q�6O   $
��Wrap=�6b   $	��Top=�6v   $��4��6�   $��1��6�   �� 6�   $?�9C:\Program Files (x86)\Steam\SteamApps\common\Cold Waters��6�   �� 6�   �� 6  6  �� 53      -   5        $ FolderSelect��53      $��Select Folder��63   $
��Left=�6K   $	��Top=�6f   $��75��6~   $��
25,192,0,0q�6�   $��4��6�   $��2��6�   �� 54      -   5        $ InstallButton��54      $��Install Mod��64   $
��Left=�6J   $	��Top=�6e   $��75��6}   $��9.941,239,0,0q�6�   $��5��6�   �� 55      -   5        $ UninstallModButton��55      $��2��69   $��Uninstall Mod��6M   $
��Left=�6e   $	��Top=�6�   $��94��6�   $��2��6�   $��	0,239,0,0q�6�   6�   56      58      �� 59      $��Mod Credits��6   $��
-2,-2,-2,0q�6+   .��5:      � ���	#FFE5E5E5 5;      �� 5<      $	��37* �6+   �� 5=      $	��14* �6+   �� 5>      $	��68* �6+   �� 5?      $
��241* �6+   �� 5@      $
��151* �6+   .+�5B      �� $
��Left=�6    $��	10,10,0,0q�6;   $
��Wrap=�6N   $	��Top=�6b   $��5��6z   $	��271��6�   $	��491��6�   �� 6�   $!�CaptainX3 	- Lead Developer��6�   �� 6�   �� 6�   $)�#Ramius 		- Assistant Lead Developer��6�   �� 6  �� 6&  $7�1OlavDeng2 	- Installer Developer / Mod Developer ��6*  �� 6i  �� 6u  $$�SnuffyTheSeal 	- Mod Developer��6y  6�  5C      5E      5F      5H      5I      