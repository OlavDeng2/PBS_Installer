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
        //The paths of the files within this program
        private string installerPath;
        private string temporaryFiles = "\\temp";
        private string modFilesPath = "\\PBS mod";

        //The install paths for the game, and the mod
        private string coldWatersFolderPath = "C:\\Program Files (x86)\\Steam\\SteamApps\\common\\Cold Waters";
        private string modInstallPath = "\\ColdWaters_Data\\StreamingAssets\\override";

        //Improve the naming, this is no good
        private string installModPath;

        private DirectoryInfo installFolder;

        //Vessels list
        private string submarineListPath = "\\override\\vessels\\_vessel_list.txt";
        private string[] vessels;
        List<string> selectedVessels = new List<string>();
        List<string> vesselsToRemove = new List<string>();

        //Missions list
        private string missionPath = "\\override";
        private string missionsListPath = "\\override\\language_en\\mission\\missions_single.txt";
        private string[] missions;
        List<string> selectedMissions = new List<string>();

        //Campaign folder
        private string campaignPath = "\\override\\campaign";
        private string[] campaigns;
        List<string> selectedCampaigns = new List<string>();
        List<string> campaignsToDelete = new List<string>();


        //Initialize the MainWindow
        public MainWindow()
        {
            InitializeComponent();

            installerPath = Directory.GetCurrentDirectory();
            //if directory already exists, clear it so as to not cause unforseen problems
            if(Directory.Exists(Directory.GetCurrentDirectory() + temporaryFiles))
            {
                Directory.Delete(Directory.GetCurrentDirectory() + temporaryFiles, true);
            }
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + temporaryFiles);

            //initialize the right path
            installModPath = coldWatersFolderPath + modInstallPath;
            installFolder = new DirectoryInfo(installModPath);
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + temporaryFiles);
            temporaryFiles = (Directory.GetCurrentDirectory() + temporaryFiles);

            DirectoryCopy(Directory.GetCurrentDirectory() + modFilesPath, System.IO.Path.Combine(Directory.GetCurrentDirectory(), temporaryFiles), true);


            //TODO: Make the bellow into a function
            GetVesselList(Directory.GetCurrentDirectory() + modFilesPath + submarineListPath);
            
            foreach(string vessel in vessels)
            {
                SubmarineListBox.Items.Add(vessel);
            }
            SubmarineListBox.SelectAll();
        }

        //The bellow functions handles events from the main window

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
            //Delete existing mod files to avoid potential errors with the mod
            if(Directory.Exists(installModPath))
            {
                Directory.Delete(installModPath, true);
            }
            Directory.CreateDirectory(installModPath);

            DirectoryCopy(Directory.GetCurrentDirectory() + "\\temp\\override", installModPath, true);
            MessageBox.Show("The More Playable Subs mod is now installed! You can now launch Cold Waters");
            //Directory.Delete(Directory.GetCurrentDirectory() + temporaryFiles, true);
        }

        private void SelectModFile_Click_1(object sender, RoutedEventArgs e)
        {
            //Open the folder dialog to find the zip file for the mod
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
            }
        }

        private void SelectSubmarinesApply_Click(object sender, RoutedEventArgs e)
        {
            selectedVessels.Clear();
            foreach(object selectedItem in SubmarineListBox.SelectedItems)
            {
                selectedVessels.Add(selectedItem.ToString());
            }

            CreateNewVesselsList();
            RemoveVessels();
        }

        private void SubmarineListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void UninstallModButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(installModPath))
            {
                Directory.Delete(installModPath, true);
            }
            MessageBox.Show("The more Playable Subs mod is now uninstalled, you can now play vanilla Cold Waters");
        }

        //The functions bellow are not handling events directly from the main window

        //TODO: bellow functions can easily be consolidated into fewer functions. 
        private void GetVesselList(string vesselListPath)
        {
            vessels = System.IO.File.ReadAllLines(vesselListPath);
        }

        private void CreateNewVesselsList()
        {
            WriteLinesToFile(System.IO.Path.Combine(Directory.GetCurrentDirectory(), temporaryFiles + submarineListPath), selectedVessels);
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

        private void RemoveVessels()
        {
            //add ships not present in the selectedVessels array to the vesselsToRemove list
            vesselsToRemove = vessels.Except(selectedVessels).ToList();

            //Loop through all the files the vessels might appear in, and then remove them
            //The files can be found in following locations:
            // override\campaign\campaign001 etc.
            // override\campaign\maps
            string[] campaignMapFiles = Directory.GetFiles(installerPath + "\\temp\\override\\campaign\\maps");
            string[] campaignSummaryFolder = Directory.GetDirectories(installerPath + "\\temp\\override\\campaign");
            List<string> campaignSummaryFiles = new List<string>();

            //campaign summary folder
            foreach(string folder in campaignSummaryFolder)
            {
                campaignSummaryFiles.AddRange(Directory.GetFiles(folder, "summary.txt"));

                foreach (string summaryFile in campaignSummaryFiles)
                {

                    string[] summaryFileData = System.IO.File.ReadAllLines(summaryFile);
                    List<string> newSummaryFileData = new List<string>();

                    foreach (string line in summaryFileData)
                    {
                        string currentLine = line;
                        //compare the vesselsToRemove list with line, remove whatever matches. (do note that we need to remove the commas at the end, if it exists, as well)
                        foreach (string vessel in vesselsToRemove)
                        {
                            //line = what is in the line and remove the vessel.
                            currentLine = GetDifferenceInString(currentLine, vessel);
                        }
                        //when above foreach loop is done, add the line to the newSummaryFileData
                        newSummaryFileData.Add(currentLine);
                    }
                    //when the above foreach loop is done, write the newSummaryFileData to the file
                    WriteLinesToFile(summaryFile, newSummaryFileData);
                }
            }

            //campaign map files
            foreach (string campaignFile in campaignMapFiles)
            {
                string[] campaignFileData = System.IO.File.ReadAllLines(campaignFile);
                List<string> newCampaignFileData = new List<string>();
                //loop through array
                foreach (string line in campaignFileData)
                {
                    string currentLine = line;
                    //compare the vesselsToRemove list with line, remove whatever matches. (do note that we need to remove the commas at the end, if it exists, as well)
                    foreach (string vessel in vesselsToRemove)
                    {
                        currentLine = GetDifferenceInString(currentLine, vessel);
                    }
                    newCampaignFileData.Add(currentLine);
                }
                WriteLinesToFile(campaignFile, newCampaignFileData);
            }


            //single missions
        }
        
        private string GetDifferenceInString(string initialString, string stringToRemove)
        {

            List<string> diff;

            string[] set1 = initialString.Split(',');
            string[] set2 = stringToRemove.Split(',');

            diff = set1.Except(set2).ToList();

            return string.Join(",", diff);
        }

        private void WriteLinesToFile(string fileLocation, List<string> contents)
        {
            //Remove the last line from the list, add it to new variable
            string lastLine = contents.Last<string>();
            contents.Remove(lastLine);

            //Write the list of all lines and the variable of the last line.
            //the reason for this is to avoid writing a blank new line, which causes cold waters to not start properly.
            System.IO.File.WriteAllLines(fileLocation, contents);
            System.IO.File.AppendAllText(fileLocation, lastLine);
        }
    }
}
