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
using JAMK.IT.IIO11300;

namespace H3_mittausdata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Luodaan kokoelma mittausolioille
        List<MittausData> mitatut;

        public MainWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }
        private void IniMyStuff()
        {
            txtToday.Text = DateTime.Today.ToShortDateString();
            mitatut = new List<MittausData>();

        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            //Luodaan uusi mittausdata olio ja näytetään se käyttäjälle
            MittausData md = new MittausData(txtClock.Text, txtData.Text);
            //lbData.Items.Add(md); //Alkuperäinen tapa
            //Lisätään mittausolio kokoelmaan
            mitatut.Add(md);
            ApplyChanges();
        }
        private void ApplyChanges()
        {
            //Päivitetään UI vastaamaan kokoelman tietoja
            lbData.ItemsSource = null;
            lbData.ItemsSource = mitatut;
        }

        private void btnWriteOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MittausData.SaveDataToFile(mitatut, txtFileName.Text);
                MessageBox.Show("Data saved to " + txtFileName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadFromFile_Click(object sender, RoutedEventArgs e)
        {
            //Luetaan data käyttäjän antamasta tiedostosta
            try
            {
                mitatut = null;

                mitatut = MittausData.ReadDataFromFile(txtFileName.Text);
                ApplyChanges();
                MessageBox.Show("Data read from " + txtFileName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveToXML_Click(object sender, RoutedEventArgs e)
        {
            JAMK.IT.IIO11300.Serialisointi.SerialisoiXml(@txtFileName.Text, mitatut);
        }
    }
    
}
