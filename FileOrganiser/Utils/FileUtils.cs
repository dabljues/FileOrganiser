using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FileOrganiser.Utils
{
    public class FileUtils
    {
        public static string SelectRootFolder()
        {
            var dialog = new CommonOpenFileDialog
            {
                    InitialDirectory = @"C:\", IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            else return string.Empty;
        }
    }
}
