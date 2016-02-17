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
using System.Xml;
using System.Xml.Linq;

namespace Harjoitus_05_WPF_XML {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        XElement xe;
        public MainWindow() {
            InitializeComponent();
        }

        private void btnGetXML_Click(object sender, RoutedEventArgs e) {
            //ladataan xml-tiedosto ja asetetaan se Datagridin data kontekstiksi.
            try {
                xe = XElement.Load(GetFileName());
                dgData.DataContext = xe.Elements("tyontekija");
                //lasketaan työntekijöitten määrä ja palkkasumma ja näytetään ne käyttäjälle.
                int lkm = 0;
                lkm = xe.Elements().Count();
                tbMessage.Text = String.Format("Akun tehtaalla on {0} työntekijää, joista vakituisia {2} joiden palkat yhteensä {1} €" , lkm, CalculateSalarySum(), CountWorkers("vakituinen"));
            }
            catch (Exception ex) {      
                MessageBox.Show(ex.ToString());
            }
        }

        private string GetFileName() {
            //älä kovakoodaa muutuvia asioita koodiin
            //return @"Z:\windows-ohjelmointi\IIO11300Vktehtavat\Harjoitus_04_konsoli_xml\tyontekijat.xml";
            //parempi tpa on sijoittaa ne App.Config
            return Harjoitus_05_WPF_XML.Properties.Settings.Default.XMLTiedosto;
        }
        private decimal CalculateSalarySum() {
            decimal result = 0;
            //haetaan työntekijöiden  palkat XML:stä (XElement-olioon) LINQ-Kyselyllä.
            var palkat = from ele in xe.Elements() select ele.Element("palkka");
            foreach (var item in palkat) {
                result += decimal.Parse(item.Value);
            }
            return result;
        }
        private int CountWorkers(string tyosuhde) {
            //lasketaan annetun työsuhteen mukaiset työntekijät LINQ-kyselylle
            var tyontekijat = from ele in xe.Elements()
                              where ele.Element("tyosuhde").Value == tyosuhde
                              select ele.Element("etunimi");
            //palautetaan lukumäärä
            return tyontekijat.Count();
        }
    }
}
