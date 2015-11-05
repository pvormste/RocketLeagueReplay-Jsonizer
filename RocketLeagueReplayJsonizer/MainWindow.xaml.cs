using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RocketLeagueReplayJsonizer {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void btnFileDlg_Click(object sender, RoutedEventArgs e) {
            lblSaved.Content = "";
            Microsoft.Win32.OpenFileDialog fileDlg = new Microsoft.Win32.OpenFileDialog();

            fileDlg.InitialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"My Games\Rocket League\TAGame\Demos\");
            fileDlg.Filter = "RL-Replay Files (*.replay)|*.replay";
            if(fileDlg.ShowDialog() == true) {
                txtBoxFileName.Text = System.IO.Path.GetFileName(fileDlg.FileName);
                lblCreatedAt.Content = File.GetLastWriteTime(fileDlg.FileName).ToString();

            }



        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e) {
            lblSaved.Content = "";
            Microsoft.Win32.SaveFileDialog saveFileDlg = new Microsoft.Win32.SaveFileDialog();

            saveFileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            saveFileDlg.Filter = "JSON-File (*.json)|*.json";

            if(saveFileDlg.ShowDialog() == true) {
                File.WriteAllText(saveFileDlg.FileName, txtBoxJSON.Text);
                lblSaved.Content = "Saved!";
            }
        }

        private void btnCopyToClipboard_Click(object sender, RoutedEventArgs e) {
            Clipboard.SetText(txtBoxJSON.Text);
            lblSaved.Content = "Copied!";
        }
    }
}
