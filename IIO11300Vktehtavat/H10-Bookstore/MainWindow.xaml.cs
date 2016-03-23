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

namespace H10_Bookstore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Book> books;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetBooks_Click(object sender, RoutedEventArgs e)
        {
            //Haetaan kirjat tietokannasta ORM=muutetaan Book-olioiksi
            try
            {
                books = BookShop.GetTestBooks();
                dgBooks.DataContext = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Valittu Book-olio tallennetaan kantaan
            try
            {
                Book current = (Book)spBook.DataContext;
                BookShop.UpdateBook(current);
                MessageBox.Show(string.Format("Kirja {0} päivitetty kantaan onnistuneesti", current.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //gridissa valittu olio asetetaan stackpanelin 
            spBook.DataContext = dgBooks.SelectedItem;
        }

        private void btnGetSqlBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                books = BookShop.GetBooks(true);
                dgBooks.DataContext = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
