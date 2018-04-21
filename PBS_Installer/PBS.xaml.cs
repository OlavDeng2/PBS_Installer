using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WinForms = System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

namespace PBS_Installer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string temporaryFiles = "\\temp";
        private string modFilesPath = "\\override";

        private string coldWatersFolderPath = "C:\\Program Files (x86)\\Steam\\SteamApps\\common\\Cold Waters";
        private string modInstallPath = "\\ColdWaters_Data\\StreamingAssets\\override";

        //Improve the naming, this is no good
        private string installModPath;

        private DirectoryInfo installFolder;

        public MainWindow()
        {
            InitializeComponent();

            //initialize the right path
            installModPath = coldWatersFolderPath + modInstallPath;
            installFolder = new DirectoryInfo(installModPath);
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + temporaryFiles);
            temporaryFiles = (Directory.GetCurrentDirectory() + temporaryFiles);

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            WinForms.DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                //----< Selected Folder >----
                //< Selected Path >
                coldWatersFolderPath = folderDialog.SelectedPath;
                FolderPath.Text = coldWatersFolderPath;
                //</ Selected Path >
                installModPath = coldWatersFolderPath + modInstallPath;
                installFolder = new DirectoryInfo(installModPath);

            }
        }

        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(installModPath);
            Directory.Delete(installModPath, true);
            Directory.CreateDirectory(installModPath);
            System.IO.Compression.ZipFile.ExtractToDirectory(modFilesPath, installModPath);


            //DirectoryCopy(Directory.GetCurrentDirectory() + modFilesPath, installModPath, true);
            MessageBox.Show(installModPath);
            Directory.Delete(temporaryFiles, true);
        }


        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = System.IO.Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void SelectModFile_Click_1(object sender, RoutedEventArgs e)
        {
            WinForms.OpenFileDialog fileDialog = new WinForms.OpenFileDialog();
            fileDialog.DefaultExt = "zip";
            fileDialog.Filter = "zip files (*.zip)|*.zip";
            fileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

            WinForms.DialogResult result = fileDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                //----< Selected Folder >----
                //< Selected Path >
                modFilesPath = fileDialog.FileName;
                //modFilesPath = folderDialog.SelectedPath;
                //ModFilesPath.Text = modFilesPath;

                System.IO.Compression.ZipFile.ExtractToDirectory(modFilesPath, temporaryFiles);

            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }




        //Code to handle Vessels
    }


}
