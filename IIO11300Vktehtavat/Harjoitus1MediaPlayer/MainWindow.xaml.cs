using Microsoft.Win32;
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

namespace Harjoitus1MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadMediaFile()
        {
            try
            {
                //Ladataan käyttäjän antama mediatiedostoa
                //string file = @"D:\Downloads\Doctor.Who.2005.2015.Christmas.Special.720p.mHD.DailyFliX.XviD.avi";
                string file = txtFileName.Text;
                //tutkitaan onko tiedosto olemassa
                if (System.IO.File.Exists(file))
                {
                    mediaElement.Source = new Uri(file);
                }
                else
                {
                    MessageBox.Show("File " + file + "not found");
                }
                //MessageBox.Show("Musiken!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            LoadMediaFile();
            mediaElement.Play();
            ChangeButtonsState();
            
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            ChangeButtonsState();

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            ChangeButtonsState();

        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            //avataan vakio open-dialogi jotta käyttäjä voi valita yhden tiedoston
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "D:\\";
            dlg.Filter = "Music files (*.mp3)|*.mp3|Video files (*.wmv, *.avi)|*.wmv;*.avi|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                txtFileName.Text = dlg.FileName;
            }
        }
        private void ChangeButtonsState()
        {
            //muutetaan nappuloitten tilaa
            btnPause.IsEnabled = !btnPause.IsEnabled;
            btnStop.IsEnabled = !btnStop.IsEnabled;
            btnPlay.IsEnabled = !btnPlay.IsEnabled;
        }
    }
}
