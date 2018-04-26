using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Petzold.RecurceDirectoriesIncrementally
{
    public class DirectoryTreeView : TreeView
    {
        public DirectoryTreeView()
        {
            RefreshTree();
        }

        public void RefreshTree()
        {
            BeginInit();
            Items.Clear();

            // get drive's info
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach(DriveInfo drive in drives)
            {
                char chDrive = drive.Name.ToUpper()[0];
                DirectoryTreeViewItem item = new DirectoryTreeViewItem(drive.RootDirectory);

                if (chDrive != 'A' && chDrive != 'B' && drive.IsReady && drive.VolumeLabel.Length > 0)
                    item.Text = String.Format("{0} ({1})", drive.VolumeLabel, drive.Name);
                else
                    item.Text = String.Format("{0} ({1})", drive.DriveType, drive.Name);

                // drive image
                if (chDrive == 'A' || chDrive == 'B')
                    item.SelectedImage = item.UnselectedImage =
                        new BitmapImage(
                            new Uri("pack://application:,,/Images/FloppyDrive_16x.png"));
                else if (drive.DriveType == DriveType.CDRom)
                    item.SelectedImage = item.UnselectedImage =
                        new BitmapImage(
                            new Uri("pack://application:,,/Images/CDDrive_16x.png"));
                else if (drive.DriveType == DriveType.Ram)
                    item.SelectedImage = item.UnselectedImage =
                        new BitmapImage(
                            new Uri("pack://application:,,/Images/Memory_16x.png"));
                else if (drive.DriveType == DriveType.Removable)
                    item.SelectedImage = item.UnselectedImage =
                        new BitmapImage(
                            new Uri("pack://application:,,/Images/Release_16x.png"));
                else if (drive.DriveType == DriveType.Unknown)
                    item.SelectedImage = item.UnselectedImage =
                        new BitmapImage(
                            new Uri("pack://application:,,/Images/UnknownProject_16x.png"));
                else if (drive.DriveType == DriveType.Fixed)
                    item.SelectedImage = item.UnselectedImage =
                        new BitmapImage(
                            new Uri("pack://application:,,/Images/HardDrive_16x.png"));
                else
                    item.SelectedImage = item.UnselectedImage =
                        new BitmapImage(
                            new Uri("pack://application:,,/Images/Question_16x.png"));

                Items.Add(item);

                // fill directories info
                if (chDrive != 'A' && chDrive != 'B' && drive.IsReady)
                    item.Populate();
            }
            EndInit();
        }
    }
}
