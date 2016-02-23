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
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace Tehtava6_ViiniKellari
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        XElement xe;
        public Window1()
        {
            InitializeComponent();
        }

        private void btnGetWines_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                xe = XElement.Load(GetFileName());
                dgWines.DataContext = xe.Elements("wine");
                initCombobox(cmbWines);
                tbPath.Text = GetFileName();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString() + "StackTrace: " +ex.StackTrace);
            }
        }
        private string GetFileName()
        {
            //älä kovakoodaa muutuvia asioita koodiin
            //return @"Z:\windows-ohjelmointi\IIO11300Vktehtavat\Harjoitus_04_konsoli_xml\tyontekijat.xml";
            //parempi tpa on sijoittaa ne App.Config
            return Tehtava6_ViiniKellari.Properties.Settings.Default.XMLfile;
        }

        private void initCombobox(ComboBox cmb)
        {
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Content = xe.Element("maa");
            cmb.Items.Add(item1);
        }
        private void cmbWines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
