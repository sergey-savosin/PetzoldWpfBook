using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Petzold.RecurceDirectoriesIncrementally
{
    public class DirectoryTreeViewItem : ImagedTreeViewItem
    {
        DirectoryInfo dir;

        public DirectoryTreeViewItem(DirectoryInfo dir)
        {
            this.dir = dir;
            Text = dir.Name;
            SelectedImage = new BitmapImage(
                new Uri("pack://application:,,/Images/FolderOpen_16x.png"));
            UnselectedImage = new BitmapImage(
                new Uri("pack://application:,,/Images/Folder_16x.png"));
        }

        // public property
        public DirectoryInfo DirectoryInfo
        {
            get { return dir; }
        }

        // public method - filling nodes
        public void Populate()
        {
            DirectoryInfo[] dirs;
            try
            {
                dirs = dir.GetDirectories();
            }
            catch
            {
                return;
            }

            foreach (DirectoryInfo dirChild in dirs)
                Items.Add(new DirectoryTreeViewItem(dirChild));
        }

        // override event for fill child
        protected override void OnExpanded(RoutedEventArgs e)
        {
            base.OnExpanded(e);
            foreach(object obj in Items)
            {
                DirectoryTreeViewItem item = obj as DirectoryTreeViewItem;
                item.Populate();
            }
        }
    }
}
