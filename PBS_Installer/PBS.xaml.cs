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

            Directory.Delete(Directory.GetCurrentDirectory() + temporaryFiles, true);



            //TODO: Make the bellow into a function
            GetVesselList(Directory.GetCurrentDirectory() + modFilesPath + submarineListPath);
            GetMissionsList(Directory.GetCurrentDirectory() + modFilesPath + missionsListPath);
            GetCampaignList();
            
            foreach(string vessel in vessels)
            {
                SubmarineListBox.Items.Add(vessel);
            }
            SubmarineListBox.SelectAll();

            foreach(string mission in missions)
            {
                MissionListBox.Items.Add(mission);
            }
            MissionListBox.SelectAll();

            foreach(string campaign in campaigns)
            {
                CampaignListBox.Items.Add(campaign);
            }
            CampaignListBox.SelectAll();

            //initialize the right path
            installModPath = coldWatersFolderPath + modInstallPath;
            installFolder = new DirectoryInfo(installModPath);
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + temporaryFiles);
            temporaryFiles = (Directory.GetCurrentDirectory() + temporaryFiles);

            DirectoryCopy(Directory.GetCurrentDirectory() + modFilesPath, System.IO.Path.Combine(Directory.GetCurrentDirectory(), temporaryFiles), true);


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
            Directory.Delete(installModPath, true);
            Directory.CreateDirectory(installModPath);

            DirectoryCopy(Directory.GetCurrentDirectory() + "\\temp\\override", installModPath, true);
            MessageBox.Show("The More Playable Subs mod is now installed! You can now launch Cold Waters");
            //Directory.Delete(Directory.GetCurrentDirectory() + temporaryFiles, true);
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
        }

        private void UninstallModButton_Click(object sender, RoutedEventArgs e)
        {
            Directory.Delete(installModPath, true);
            MessageBox.Show("The more Playable Subs mod is now uninstalled, you can now play vanilla Cold Waters");
        }

        private void SubmarineListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CampaignApplyButton_Click(object sender, RoutedEventArgs e)
        {
            //The code for the campaigns
            selectedCampaigns.Clear();
            foreach(object selectedItem in CampaignListBox.SelectedItems)
            {
                selectedCampaigns.Add(selectedItem.ToString());
            }
            //CreateNewCampaignList();


            //the code for missions
            selectedMissions.Clear();
            foreach (object selectedItem in MissionListBox.SelectedItems)
            {
                selectedMissions.Add(selectedItem.ToString());
            }
            //CreateNewMissionsList();
        }

        //The functions bellow are not handling events directly from the main window

        //TODO: bellow functions can easily be consolidated into fewer functions. 
        private void GetVesselList(string vesselListPath)
        {
            vessels = System.IO.File.ReadAllLines(vesselListPath);
        }

        private void CreateNewVesselsList()
        {
            System.IO.File.WriteAllLines(System.IO.Path.Combine(Directory.GetCurrentDirectory(), temporaryFiles + submarineListPath), selectedVessels);
        }

        private void GetMissionsList(string missionListLocation)
        {
            missions = System.IO.File.ReadAllLines(missionListLocation);
        }

        private void CreateNewMissionsList()
        {
            System.IO.File.WriteAllLines(System.IO.Path.Combine(Directory.GetCurrentDirectory(), temporaryFiles + missionsListPath), selectedMissions);
        }

        private void GetCampaignList()
        {
            campaigns = Directory.GetDirectories(System.IO.Path.Combine(Directory.GetCurrentDirectory(), temporaryFiles + campaignPath), "camp*");
        }

        private void CreateNewCampaignList()
        {
            //this will just delete the campaigns that we do not want)

        }


        private void ModifyCampaignFiles()
        {
            //This function should modify the campaign files to take into account the subs that have been added/removed
            GetCampaignList();
            foreach(string campaign in campaigns)
            {
                //do the thing
            }
        }

        private void ModifyMissionFiles()
        {
            //This function should modify the single missions files to take into account the subs that have been added/removed
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

        
    }

    
}
