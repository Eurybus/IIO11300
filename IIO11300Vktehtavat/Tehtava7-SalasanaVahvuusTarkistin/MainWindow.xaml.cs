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
using System.Security.Cryptography;

namespace Tehtava7_SalasanaVahvuusTarkistin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string pw;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void pwbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pw = pwbPassword.Password;
            tbCharCount.Text = "Merkkejä: " + pw.Length;
            tbCapChars.Text = "Isoja kirjaimia: " + CapitalCharCount(pw);
            tbMinorChars.Text = "Pieniä kirjaimia: " + LowerCharCount(pw);
            tbNumbers.Text = "Numeroita: " + NumberCount(pw);
            tbSpecialChars.Text = "Erikoismerkkejä: " + SpecialCharCount(pw);
            tbSha1.Text = "";
            //tbSha1.Text = "Sha1: " + Hash(pw);
            InformUserPwEntropy(pw);
        }

        static string Hash(string input)//For calculating Sha1 of the password
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        private int CapitalCharCount(string text)
        {
            return text.Count(char.IsUpper);
            
        }
        private int LowerCharCount(string text)
        {
            return text.Count(char.IsLower);
        }

        private int NumberCount(string text)
        {
            return text.Count(char.IsNumber);
        }

        private int SpecialCharCount(string text)
        {
            return text.Count(char.IsSymbol) + text.Count(char.IsWhiteSpace) + text.Count(char.IsPunctuation);
        }
        private void InformUserPwEntropy(string text)
        {
            switch (PwEntropy.CheckStrength(text))
            {
                case PwEntropy.PasswordScore.Blank:
                    tbIndicator.Background = new SolidColorBrush(Colors.Gray);
                    tbIndicator.Text = "Anna salasana";
                    break;

                case PwEntropy.PasswordScore.TooShort:
                    tbIndicator.Background = new SolidColorBrush(Colors.Gray);
                    tbIndicator.Text = "Liian lyhyt";
                    break;

                case PwEntropy.PasswordScore.VeryWeak:
                    tbIndicator.Background = new SolidColorBrush(Colors.OrangeRed);
                    tbIndicator.Text = "Ala-arvoinen";
                    break;
                case PwEntropy.PasswordScore.Weak:
                    tbIndicator.Background = new SolidColorBrush(Colors.Orange);
                    tbIndicator.Text = "Heikko";
                    break;

                case PwEntropy.PasswordScore.Fair:
                    tbIndicator.Background = new SolidColorBrush(Colors.Yellow);
                    tbIndicator.Text = "Välttävä";
                    break;

                case PwEntropy.PasswordScore.Medium:
                    tbIndicator.Background = new SolidColorBrush(Colors.LightGreen);
                    tbIndicator.Text = "Tyydyttävä";
                    break;

                case PwEntropy.PasswordScore.Strong:
                    tbIndicator.Background = new SolidColorBrush(Colors.Green);
                    tbIndicator.Text = "Hyvä";
                    break;

                case PwEntropy.PasswordScore.VeryStrong:
                    tbIndicator.Background = new SolidColorBrush(Colors.DarkGreen);
                    tbIndicator.Text = "Kiitettävä";
                    break;

                default:
                    tbIndicator.Background = new SolidColorBrush(Colors.Gray);
                    break;
            }
        }
    }
}
