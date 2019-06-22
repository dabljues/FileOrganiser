using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using FileOrganiser.Utils;
using MahApps.Metro.Controls;
using Path = System.IO.Path;

namespace FileOrganiser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private string RootDir;
        private string[] files;
        public MainWindow()
        {
            InitializeComponent();
            while (true)
            {
                RootDir = FileUtils.SelectRootFolder();
                if (RootDir == string.Empty) MessageBox.Show("Select a root folder!");
                else break;
            }
            LoadFiles();
        }

        private void LoadFiles()
        {
            files = Directory.GetFiles(RootDir, "*.*", SearchOption.AllDirectories);
            var videos = files.Select(file => new Video(file, Path.GetFileName(file), false)).ToList();
            FilesList.ItemsSource = videos;
        }

        private void FilesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilesList.SelectedItem == null)
            {
                if (FilesList.Items.Count > 0) FilesList.SelectedIndex = 0;
                Media.Source = null;
                return;
            }
            var selection = ((Video) FilesList.SelectedItem);
            selection.MarkAsSeen();
            var path = selection.GetFullPathUri();

            Media.Source = path;
        }

        private void ButtonSort_OnClick(object sender, RoutedEventArgs e)
        {
            var items = FilesList.ItemsSource.Cast<Video>().ToList();
            var toKeep = items.Where(i => i.Keep);
            var toRemove = items.Where(i => i.Keep == false && i.AlreadySeen());

            // toKeep
            if (!Directory.Exists($@"{RootDir}\..\saved")) Directory.CreateDirectory($@"{RootDir}\..\saved");
            foreach (var video in toKeep)
            {
                File.Move($@"{video.GetFullPath()}", $@"{RootDir}\..\saved\{video.Filename}");
            }

            // toRemove
            if (!Directory.Exists($@"{RootDir}\..\remove")) Directory.CreateDirectory($@"{RootDir}\..\remove");
            foreach (var video in toRemove)
            {
                File.Move($@"{video.GetFullPath()}", $@"{RootDir}\..\remove\{video.Filename}");
            }
            LoadFiles();
        }
    }
}
