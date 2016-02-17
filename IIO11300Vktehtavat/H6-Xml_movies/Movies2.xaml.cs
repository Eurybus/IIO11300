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
namespace H6_Xml_movies
{
    /// <summary>
    /// Interaction logic for Movies2.xaml
    /// </summary>
    public partial class Movies2 : Window
    {
        public Movies2()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Tallennetaan muuttunut tieto XML-tiedostoon
            try
            {
                string filu = xdpMovies.Source.LocalPath;
                xdpMovies.Document.Save(filu);
                MessageBox.Show("Muutokset tallennettu");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            //Lisätään XmlDocumenttiin uusi elementti
            //Huom textboxit ja listbox bindattu dataan
            try
            {
                if(lbMovies.SelectedIndex > -1){
                    lbMovies.SelectedIndex = -1;
                }
                else {
                    //Lisätään uusi node
                    string filu = xdpMovies.Source.LocalPath;
                    //Viittaus juurielementtiin
                    XmlDocument doc = xdpMovies.Document;
                    XmlNode root = doc.SelectSingleNode("/Movies");
                    //Luodaan uusi node
                    XmlNode newMovie = doc.CreateElement("Movie");
                    //Lisataan attribuutit
                    XmlAttribute attr = doc.CreateAttribute("Name");
                    attr.Value = txtName.Text;
                    newMovie.Attributes.Append(attr);
                    XmlAttribute attr2 = doc.CreateAttribute("Director");
                    attr2.Value = txtDirector.Text;
                    newMovie.Attributes.Append(attr2);
                    XmlAttribute attr3 = doc.CreateAttribute("Country");
                    attr3.Value = txtCountry.Text;
                    newMovie.Attributes.Append(attr3);
                    root.AppendChild(newMovie);
                    xdpMovies.Document.Save(filu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XmlDocument doc = xdpMovies.Document;
                XmlNode root = doc.SelectSingleNode("/Movies");
                XmlNode target = doc.SelectSingleNode("/Movies/Movie");
                root.RemoveChild(target);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
