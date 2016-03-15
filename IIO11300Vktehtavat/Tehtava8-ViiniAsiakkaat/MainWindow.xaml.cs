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
using System.Data;
using System.Data.SqlClient;

namespace Tehtava8_ViiniAsiakkaat
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

        private void GetData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Tehtava8_ViiniAsiakkaat.Properties.Settings.Default.Tietokanta))
                {
                    string sql = "SELECT firstname, lastname, address, city FROM vCustomers";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable("viiniAsiakkaat");
                    da.Fill(dt);
                    myGrid.DataContext = dt;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void populateUI()
        {
            lbCustomers.DataContext = myGrid.DataContext;
            lbCustomers.DisplayMemberPath = "firstname " + "lastname";
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            GetData();
            populateUI();
        }
    }

}
