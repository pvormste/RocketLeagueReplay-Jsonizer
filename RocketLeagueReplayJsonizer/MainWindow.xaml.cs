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

        Microsoft.Win32.FileDialog fileDlg = null;

        public MainWindow() {
            InitializeComponent();
        }

        private void btnFileDlg_Click(object sender, RoutedEventArgs e) {
            resetGUI(true);
            fileDlg = new Microsoft.Win32.OpenFileDialog();

            fileDlg.InitialDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"My Games\Rocket League\TAGame\Demos\");
            fileDlg.Filter = "RL-Replay Files (*.replay)|*.replay";
            if(fileDlg.ShowDialog() == true) {
                txtBoxFileName.Text = System.IO.Path.GetFileName(fileDlg.FileName);
                lblCreatedAt.Content = File.GetLastWriteTime(fileDlg.FileName).ToString();
            }



        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e) {
            resetGUI(false);
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

        private void btnGenerate_Click(object sender, RoutedEventArgs e) {
            txtBoxJSON.Text = "Generating. Please Wait ...";

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string JSON = serializer.Serialize(new ReplayJSON(fileDlg.FileName));

            txtBoxJSON.Text = JSON;
        }

        private void resetGUI(bool resetAll) {
            lblSaved.Content = "";

            if(resetAll) {
                txtBoxFileName.Text = "";
                txtBoxJSON.Text = "";
                lblCreatedAt.Content = "";
                fileDlg = null;
            }
        }
    }
}
