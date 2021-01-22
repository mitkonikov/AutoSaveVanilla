using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;

namespace AutoSaveVanilla
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string worldPath = "";
        string backupPath = "";
        string backupPathFinal = "";
        bool isSaving = false;
        int counter = 0;
        System.Windows.Threading.DispatcherTimer dTimerAutoSave = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dTimerAutoSave.Tick += dispatcherTimer_Tick;

            // Default settings
            checkBox.IsChecked = true;
            AutoSaveTimeBox.Text = "10";
        }

        private void Browse_World_Path(object sender, RoutedEventArgs e) // Browse for the world
        {
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.IsFolderPicker = true;
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                worldPath = fileDialog.FileName;
                worldBox.Text = worldPath;
            }
        }

        private void Browse_Backup_Path(object sender, RoutedEventArgs e) // Browse for the backup location
        {
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.IsFolderPicker = true;
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                backupPath = fileDialog.FileName;
                backupBox.Text = backupPath;
            }
        }

        private void Start_AutoSave(object sender, RoutedEventArgs e) // Start Auto-Saving
        {
            if (isSaving == false)
            {
                    if (worldBox.Text == "")
                    {
                        worldBox.Background = new SolidColorBrush(Color.FromRgb((byte)212, (byte)58, (byte)58));
                    }
                    else
                    {
                        // Reset color
                        worldBox.Background = new SolidColorBrush(Color.FromRgb((byte)83, (byte)83, (byte)83));

                        if (backupBox.Text == "")
                        {
                            backupBox.Background = new SolidColorBrush(Color.FromRgb((byte)212, (byte)58, (byte)58));
                        }
                        else
                        {
                            // Reset color
                            backupBox.Background = new SolidColorBrush(Color.FromRgb((byte)83, (byte)83, (byte)83));

                            if (AutoSaveTimeBox.Text == "")
                            {
                                AutoSaveTimeBox.Background = new SolidColorBrush(Color.FromRgb((byte)212, (byte)58, (byte)58));
                            }
                            else
                            {
                                try
                                {
                                    // Reset color
                                    AutoSaveTimeBox.Background = new SolidColorBrush(Color.FromRgb((byte)83, (byte)83, (byte)83));

                                    dTimerAutoSave.Interval = new TimeSpan(0, Convert.ToInt16(AutoSaveTimeBox.Text), 0);
                                    dTimerAutoSave.Start();
                                    CounterLabel.Content = "Counter: 0";
                                    Saving();
                                    saveBtn.Content = "Stop Auto-Saving";
                                    isSaving = true;
                                } catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                    }
            }
            else
            {
                dTimerAutoSave.Stop();
                saveBtn.Content = "Start Auto-Saving";
                isSaving = false;
                counter = 0;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                Saving();
            }
            else
            {
                if (counter != Convert.ToInt16(AutoSaveCounterBox.Text))
                {
                    Saving();
                }
                else
                {
                    dTimerAutoSave.Stop();
                    saveBtn.Content = "Start Auto-Saving";
                    isSaving = false;
                    counter = 0;
                    CounterLabel.Content = "Counter: 0";
                }
            }
        }

        private void Saving()
        {
            try
            {
                string[] worldDirNameArray = worldPath.Split((char)92);
                string worldDirName = worldDirNameArray[worldDirNameArray.Length - 1];
                string datetimeNow = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                string datetimeMod = datetimeNow.Replace('.', '-').Replace(':', '-').Replace('/', '-');
                backupPathFinal = backupPath + @"\" + worldDirName + "-" + datetimeMod;
                string status = DirectoryCopy(worldPath, backupPathFinal, true);
                if (status == "Done")
                {
                    counter++;
                    CounterLabel.Content = "Counter: " + counter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BackupPath: " + backupPathFinal + " " + ex.ToString());
            }
        }

        private static string DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                return "ExistProblem";
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

            return "Done";
        }
        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            AutoSaveLabel.IsEnabled = false;
            AutoSaveCounterBox.IsEnabled = false;
            TimesLabel.IsEnabled = false;
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            AutoSaveLabel.IsEnabled = true;
            AutoSaveCounterBox.IsEnabled = true;
            TimesLabel.IsEnabled = true;
        }

        private void worldBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            worldPath = worldBox.Text;
        }

        private void backupBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            backupPath = backupBox.Text;
        }
    }
}
