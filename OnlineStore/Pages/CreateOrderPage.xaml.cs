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
    /// Логика взаимодействия для CreateOrderPage.xaml
    /// </summary>
    public partial class CreateOrderPage : Page
    {
        private User _currentUser;

        public CreateOrderPage(User user)
        {
            InitializeComponent();
            _currentUser = user;

            // Загрузка пользователей
            cbUsers.ItemsSource = DBConn.connection.User.ToList();
            cbUsers.SelectedIndex = 0;

            // Установка текущей даты
            dpOrderDate.SelectedDate = DateTime.Now;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage(_currentUser));
        }
    }
}
