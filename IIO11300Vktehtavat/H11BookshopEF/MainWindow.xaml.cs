using System;
using System.Collections;
using System.Collections.ObjectModel; //for observable collection
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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

namespace H11BookshopEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookShopEntities ctx;
        ObservableCollection<Book> localBooks;
        ICollectionView view; //DataGridin filteröintiä varten
        bool IsBooks; //onko grididissä kirjat vai asiakkaat
        

        public MainWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }
        private void IniMyStuff()
        {
            //Luodaan conteksti = datasisältö
            ctx = new BookShopEntities();
            //Ladataan kirjatiedot paikalliseksi
            ctx.Books.Load();
            localBooks = ctx.Books.Local;
            //täytetään comboxi kirjailijoiden maiden nimillä
            cbCountries.DataContext = localBooks.Select(n => n.country).Distinct();
            view = CollectionViewSource.GetDefaultView(localBooks);
        }
        private void btnAsiakkaanTilaukset_Click(object sender, RoutedEventArgs e)
        {
            //haetaan EDM:n navigaatio-ominaisuuksien avulla valitun asiakkaan tilaukset ja sen kirjat
            string msg = "";
            Customer current = (Customer)spCustomer.DataContext;
            msg += string.Format("Asiakkaan {0} tilaukset \n", current.lastname);
            foreach (var tilaus in current.Orders)
            {
                msg += string.Format("Tilaus {0} sisältää {1} tilausriviä:\n", tilaus.odate, tilaus.Orderitems.Count);
                //loopitetaan tilauksen tilaus rivit
                foreach (var tilausrivi in tilaus.Orderitems)
                {
                    msg += string.Format("- kirja {0}\n", tilausrivi.Book.name);
                }
            }
            MessageBox.Show(msg);
        }


        private void btnGetClients_Click(object sender, RoutedEventArgs e)
        {
            //haetaan asiakkaat
            dgBooks.DataContext = ctx.Customers.ToList();
            IsBooks = false;
            
        }

        private void btnGetBooks_Click(object sender, RoutedEventArgs e)
        {
            //vaihtoehto 1
            //dgBooks.DataContext = ctx.Books.ToList();
            //vaihtoeho 2, käytetään paikallista muuttujaa
            dgBooks.DataContext = localBooks;
            IsBooks = true;
            //mahdollinen filteröinti pois
            cbCountries.SelectedIndex = -1;
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //näytetään valitun entiteetin tiedot stackpanelissa
            if (IsBooks)
                spBook.DataContext = dgBooks.SelectedItem;
            else
                spCustomer.DataContext = dgBooks.SelectedItem;
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //asetetaan filtteri päälle
            view.Filter = MyCountryFilter;
        }

        private bool MyCountryFilter(object item)
        {
            if (cbCountries.SelectedIndex == -1)
                return true;
            else
                return (item as Book).country.Contains(cbCountries.SelectedItem.ToString());
        }
    }
}
