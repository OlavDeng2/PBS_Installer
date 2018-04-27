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
        private string temporaryFilesFullPath;
        private string modFilesPath = "\\PBS mod";
        private string modFilesFullPath;

        //The install paths for the game, and the mod
        private string coldWatersFolderPath = "C:\\Program Files (x86)\\Steam\\SteamApps\\common\\Cold Waters";
        private string modInstallPath = "\\ColdWaters_Data\\StreamingAssets\\override";
        private string modInstallFullPath;


        //Default Vessels list
        private string defaultSubmarineListPath = "\\override\\vessels\\_vessel_list.txt";
        private string defaultSubmarinesListFullPath;
        private string[] defaultVessels;

        //Optional Vessels list
        private string optionalVesselsListPath = "\\override\\vessels\\_vessel_optional_list.txt";
        private string optionalVesselsListFullPath;
        private string[] optionalVessels;
        List<string> selectedVessels = new List<string>();
        List<string> vesselsToRemove = new List<string>();

        List<string> modVessels = new List<string>();

        //Initialize the MainWindow
        public MainWindow()
        {
            installerPath = Directory.GetCurrentDirectory();

            InitializeComponent();

            //Initialize folder paths
            //installerPath = Directory.GetCurrentDirectory();
            

            temporaryFiles = System.IO.Path.Combine(installerPath, temporaryFiles);
            modFilesFullPath = (installerPath + modFilesPath);//System.IO.Path.Combine(installerPath, modFilesPath);
            modInstallFullPath = (coldWatersFolderPath + modInstallPath);//System.IO.Path.Combine(coldWatersFolderPath, modInstallPath);
            defaultSubmarinesListFullPath = (installerPath + temporaryFiles + defaultSubmarineListPath); //System.IO.Path.Combine(installerPath, temporaryFiles, defaultSubmarineListPath);
            optionalVesselsListFullPath = (installerPath + temporaryFiles + optionalVesselsListPath); //System.IO.Path.Combine(installerPath, temporaryFiles, optionalVesselsListPath);
            temporaryFilesFullPath = (installerPath + temporaryFiles);//System.IO.Path.Combine(installerPath, temporaryFiles);


            //if directory already exists, clear it so as to not cause unforseen problems
            if (Directory.Exists(temporaryFilesFullPath))
            {
                Directory.Delete(temporaryFilesFullPath, true);
            }

            //Create temporary folder
            Directory.CreateDirectory(temporaryFiles);

            DirectoryCopy(modFilesFullPath, temporaryFilesFullPath, true);

            defaultVessels = ReadFile(defaultSubmarinesListFullPath);
            optionalVessels = ReadFile(optionalVesselsListFullPath);
            
            foreach(string vessel in optionalVessels)
            {
                SubmarineListBox.Items.Add(vessel);
            }
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
                modInstallFullPath = coldWatersFolderPath + modInstallPath;
            }
        }

        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            //Delete existing mod files to avoid potential errors with the mod
            if(Directory.Exists(modInstallFullPath))
            {
                Directory.Delete(modInstallFullPath, true);
            }
            Directory.CreateDirectory(modInstallFullPath);

            //Note: "\\override" is required to ensure that the contents get copied correctly in there
            DirectoryCopy(temporaryFilesFullPath + "\\override", modInstallFullPath, true);
            MessageBox.Show("The More Playable Subs mod is now installed! You can now launch Cold Waters");
        }

        private void SelectSubmarinesApply_Click(object sender, RoutedEventArgs e)
        {
            selectedVessels.Clear();
            foreach(object selectedItem in SubmarineListBox.SelectedItems)
            {
                selectedVessels.Add(selectedItem.ToString());
            }

            CreateNewVesselsList();
            addVesselsToCampaign();
            addVesselsToMissions();
        }

        private void SelectAllSubsButton_Click(object sender, RoutedEventArgs e)
        {
            SubmarineListBox.SelectAll();
        }

        private void SubmarineListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void UninstallModButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(modInstallFullPath))
            {
                Directory.Delete(modInstallFullPath, true);
            }
            MessageBox.Show("The more Playable Subs mod is now uninstalled, you can now play vanilla Cold Waters");
        }

        private void WelcomeNextButton_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex = 1;
        }

        private void SubSelectNextButton_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex = 2;
        }

        //The functions bellow are not handling events directly from the main window

        //TODO: bellow functions can easily be consolidated into fewer functions. 
        private string[] ReadFile(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }

        private void CreateNewVesselsList()
        {
            modVessels.Clear();
            modVessels = defaultVessels.ToList();
            modVessels.AddRange(optionalVessels.ToList());

            WriteLinesToFile(installerPath + temporaryFiles + defaultSubmarineListPath, modVessels);
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

        private void addVesselsToCampaign()
        {
            //add ships not present in the selectedVessels array to the vesselsToRemove list
            vesselsToRemove = defaultVessels.Except(selectedVessels).ToList();

            //Loop through all the files the vessels might appear in
            //The files can be found in following locations:
            // override\campaign\campaign001 etc.
            // override\campaign\maps
            // override\
            string[] campaignMapFiles = Directory.GetFiles(installerPath + "\\temp\\override\\campaign\\maps");
            string[] campaignSummaryFolder = Directory.GetDirectories(installerPath + "\\temp\\override\\campaign");
            //List<string> campaignSummaryFiles = new List<string>();

            //campaign summary folder
            foreach (string folder in campaignSummaryFolder)
            {

                //this line ads the files multiple times
                List<string> campaignSummaryFiles = new List<string>();
                campaignSummaryFiles.AddRange(Directory.GetFiles(folder, "summary.txt"));

                foreach (string summaryFile in campaignSummaryFiles)
                {
                    string[] summaryFileData = System.IO.File.ReadAllLines(summaryFile);
                    List<string> newSummaryFileData = new List<string>();

                    foreach (string line in summaryFileData)
                    {
                        string currentLine = line;

                        string firstWord = currentLine.Split('=')[0];
                        //Add the selected vessels in the appropriate files
                        if (firstWord == "PlayerVessels")
                        {
                            //For some reason this causes it to be written to the file 4*, investigate
                            currentLine = (currentLine + "," + string.Join(",", selectedVessels));

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
                    string firstWord = currentLine.Split('=')[0];


                    //add the vessels in the appropriate file
                    if (firstWord == "OtherVessels")
                    {
                        currentLine = (currentLine + "," + string.Join(",", selectedVessels));
                    }
                    newCampaignFileData.Add(currentLine);
                }
                WriteLinesToFile(campaignFile, newCampaignFileData);
            }
        }

        private void addVesselsToMissions()
        {

            string[] singleMissionFiles = Directory.GetFiles(installerPath + "\\temp\\override");
            List<string> singleMissionFilesToEdit = new List<string>();
            List<string> filesToRemoveFromList = singleMissionFiles.ToList();
            filesToRemoveFromList = singleMissionFiles.ToList();
            filesToRemoveFromList.RemoveAll(sm => sm.Contains("single"));

            singleMissionFilesToEdit = singleMissionFiles.Except(filesToRemoveFromList).ToList();

            //Ignores missions 5, 6 and 8, currently hard coded
            singleMissionFilesToEdit.RemoveAll(sm => sm.Contains("single005"));
            singleMissionFilesToEdit.RemoveAll(sm => sm.Contains("single006"));
            singleMissionFilesToEdit.RemoveAll(sm => sm.Contains("single008"));




            //mission files
            foreach (string missionFile in singleMissionFilesToEdit)
            {
                string[] missionFileData = System.IO.File.ReadAllLines(missionFile);
                List<string> newMissionFileData = new List<string>();
                //loop through array
                foreach (string line in missionFileData)
                {
                    string currentLine = line;
                    string firstWord = currentLine.Split('=')[0];


                    //add the vessels in the appropriate file
                    if (firstWord == "PlayerVessels")
                    {
                        currentLine = (currentLine + "," + string.Join(",", selectedVessels));
                    }
                    newMissionFileData.Add(currentLine);
                }
                WriteLinesToFile(missionFile, newMissionFileData);
            }
            
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
