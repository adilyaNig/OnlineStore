using OnlineStore.DataBase;
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

namespace OnlineStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public User user { get; set; }

        public AuthPage()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text.Trim().Length != 0 && tbPass.Password.Trim().Length != 0)
            {
                string email = tbEmail.Text.Trim();
                string pass = tbPass.Password.Trim();

                
                user = DBConn.connection.User.Where(x => x.Email == email && x.PasswordHash == pass).FirstOrDefault();

                if (user != null)
                {
                    NavigationService.Navigate(new MainPage(user));
                }
                else
                {
                    MessageBox.Show("Email or password is incorrect");
                }
            }
        }

       
    }
}
