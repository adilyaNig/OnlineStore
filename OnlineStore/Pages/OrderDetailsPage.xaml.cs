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
    /// Логика взаимодействия для OrderDetailsPage.xaml
    /// </summary>
    public partial class OrderDetailsPage : Page
    {
        public User CurrentUser { get; set; }
        public Order Order { get; set; }

        public OrderDetailsPage(User user, Order order)
        {
            InitializeComponent();
            CurrentUser = user;
            Order = order;
            DataContext = this;
            LoadOrderItems();
        }

        private void LoadOrderItems()
        {
            lvOrderItems.ItemsSource = DBConn.connection.OrderItem
                .Where(oi => oi.OrderID == Order.OrderID)
                .ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage(CurrentUser));
        }
    }
}
