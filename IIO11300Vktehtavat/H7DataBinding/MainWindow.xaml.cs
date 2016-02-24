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

namespace H7DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //moduulitason muuttuja
        HockeyLeague smliiga;
        List<HockeyTeam> joukkuuet;
        int clicked = 0;
        public MainWindow()
        {
            InitializeComponent();
            FillMyCombocox();
            smliiga = new HockeyLeague();
            joukkuuet = smliiga.GetTeams();
        }

        private void FillMyCombocox()
        {
            cbCourses2.Items.Add("IIO12110 Ohjelmistotuotanto");
            cbCourses2.Items.Add("Ruotti");
            cbCourses2.Items.Add("J2EE ");
        }
       
        private void btnSetUser_Click(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = "Hello " + H7DataBinding.Properties.Settings.Default.UserName;
        }

        private void btnBind_Click(object sender, RoutedEventArgs e)
        {
            grid1.DataContext = joukkuuet;
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            clicked++;
            grid1.DataContext = joukkuuet[clicked];
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            clicked--;
            grid1.DataContext = joukkuuet[clicked];
        }
    }

    
}
