   M S B A M L   `   `   `  ���� H  DPBS_Installer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:PBS_InstallerPBS_Installer      PBS_Installer.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:PBS_Installer       � Title$  PBS Mod Installer��$	��350��$	��525��.��� -    �Name$ PBS_Installer�� � ColumnDefinitions �� $	��43* ��� $
��474* �.+��� -    ��Name$
 Tabs��$
��Left=�$	��319��$	��Top=�$	��517��$��2��.@��� $��Welcome��.��� ���	#FFE5E5E5.+��� $
��Left=�$��	53,10,0,0q�$
��Wrap=�$	��Top=�$	��229��$	��376���� $E�?Welcome to the New Playable Subs mod installer for Cold Waters.���� �� �� �� $���This installer will install the core files for the mod, which includes all of the vanilla playable subs as well as a few new ones that are required by the single missions and campaigns. You may also choose additional optional submarines to install on the "Select Additional Submarines" tab. The installer will automatically add those optional subs to all of the single missions (except special missions) and all of the campaigns.���� �� �� �� $���If you spot any issues or bugs with the mod, please visit the Github page where you downloaded this installer and report the problem on the Issues page, so the mod devs can diagnose and fix it. If you'd like to request that a new sub be added to the optional sub list, you can make a request on the same Issues page on Github.���� -    ��Name$ WelcomeNextButton��$
��Next��$
��Left=�$	��Top=�$��75��$��412,261,0,0q��� -    ��Name$ 
SelectSubs��$"��Select Additional Submarines��.��� -   $ SubmarinesListBox�����	#FFE5E5E5 �� $
��202* ��� $	��60* ��� $	��36* ��� $��6* ��� $��6* ��� $
��201* �.+��� -    ��Name$ SubmarineListBox��$
��Left=�$	��246��$	��Top=�$	��242��$��	10,10,0,0q�$��2�� �� SelectionMode$ Multiple=��� -   $ SelectSubmarinesApply��$��Apply��$
��Left=�$	��Top=�$��54��$��198,261,0,0q�$��2���� $��2��$
��Left=�$��	10,10,0,0q�$
��Wrap=�$����Select any additional subs you wish to install (or choose "Select All" to install all additional subs) and then click "Apply" to finalize your selection.��$	��Top=�$	��189��$	��238��$��4���� -   $ SelectAllSubsButton��$��
Select All��$
��Left=�$��
27,261,0,0q�$	��Top=�$��75���� -	   $ SubSelectNextButton��$
��Next��$��5��$
��Left=�$��116,261,0,0q�$	��Top=�$��75���� $��Install/Uninstall��.��� ���	#FFE5E5E5 �� $	��37* ��� $	��14* ��� $	��68* ��� $
��241* ��� $	��30* ��� $
��121* �.+��� $
��Left=�$��	6,139,0,0q�$
��Wrap=�$	��Top=�$	��342��$��4��$��1��$���{The default Cold Waters installation directory is chosen automatically, but you can change the installation directory here:���� -
    ��Name$ 
FolderPath��$
��Left=�$��	6,192,0,0q�$
��Wrap=�$	��Top=�$��4��$��1���� $?�9C:\Program Files (x86)\Steam\SteamApps\common\Cold Waters���� �� �� -   $ FolderSelect��$��Select Folder��$
��Left=�$	��Top=�$��75��$��
25,192,0,0q�$��4��$��2���� -   $ InstallButton��$��Install Mod��$
��Left=�$	��Top=�$��75��$��9.941,239,0,0q�$��5���� -   $ UninstallModButton��$��2��$��Uninstall Mod��$
��Left=�$	��Top=�$��94��$��2��$��	0,239,0,0q��� $��Mod Credits��$��
-2,-2,-2,0q�.��� ���	#FFE5E5E5 �� $	��37* ��� $	��14* ��� $	��68* ��� $
��241* ��� $
��151* �.+��� $
��Left=�$��	10,10,0,0q�$
��Wrap=�$	��Top=�$��5��$	��271��$	��491���� $!�CaptainX3 	- Lead Developer���� �� $)�#Ramius 		- Assistant Lead Developer���� �� $7�1OlavDeng2 	- Installer Developer / Mod Developer ���� �� $$�SnuffyTheSeal 	- Mod Developer��