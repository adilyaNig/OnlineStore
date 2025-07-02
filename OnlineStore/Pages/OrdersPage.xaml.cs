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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        private User _currentUser;

        public OrdersPage(User user)
        {
            InitializeComponent();
            _currentUser = user;
            LoadOrders();

            // Показываем/скрываем кнопки в зависимости от роли
            btnCreate.Visibility = (_currentUser.Role == "Admin" || _currentUser.Role == "Manager")
                ? Visibility.Visible : Visibility.Collapsed;
            btnEdit.Visibility = btnCreate.Visibility;
            btnDelete.Visibility = btnCreate.Visibility;
        }

        private void LoadOrders()
        {
            try
            {
                if (_currentUser.Role == "Admin" || _currentUser.Role == "Manager")
                {
                    lvOrders.ItemsSource = DBConn.connection.Order.ToList();
                }
                else
                {
                    lvOrders.ItemsSource = DBConn.connection.Order
                        .Where(o => o.UserID == _currentUser.UserID).ToList();
                }
                tbStatus.Text = $"Загружено заказов: {lvOrders.Items.Count}";
            }
            catch (Exception ex)
            {
                tbStatus.Text = "Ошибка загрузки заказов: " + ex.Message;
            }
        }

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool hasSelection = lvOrders.SelectedItem != null;
            btnEdit.IsEnabled = hasSelection;
            btnDelete.IsEnabled = hasSelection;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_currentUser));
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateOrderPage(_currentUser));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvOrders.SelectedItem is Order selectedOrder)
            {
                NavigationService.Navigate(new EditOrderPage(_currentUser, selectedOrder));
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvOrders.SelectedItem is Order selectedOrder)
            {
                if (MessageBox.Show("Удалить этот заказ?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        DBConn.connection.Order.Remove(selectedOrder);
                        DBConn.connection.SaveChanges();
                        LoadOrders();
                        tbStatus.Text = "Заказ успешно удален";
                    }
                    catch (Exception ex)
                    {
                        tbStatus.Text = "Ошибка удаления: " + ex.Message;
                    }
                }
            }
        }
    }
}
