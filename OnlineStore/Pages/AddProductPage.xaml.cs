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
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        public User CurrentUser { get; set; }
        public List<Category> Categories { get; set; }

        public AddProductPage(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            Categories = DBConn.connection.Category.ToList();
            cbCategory.ItemsSource = Categories;
            DataContext = this;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(CurrentUser));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                Product newProduct = new Product
                {
                    Name = tbName.Text.Trim(),
                    Description = tbDescription.Text.Trim(),
                    Price = decimal.Parse(tbPrice.Text),
                    CategoryID = (cbCategory.SelectedItem as Category).CategoryID,
                    StockQuantity = int.Parse(tbQuantity.Text),
                    ImageURL = tbImageUrl.Text.Trim(),
                    AverageRating = 0
                };

                DBConn.connection.Product.Add(newProduct);
                DBConn.connection.SaveChanges();

                MessageBox.Show("Продукт успешно добавлен");
                NavigationService.Navigate(new MainPage(CurrentUser));
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Пожалуйста введите название товара");
                return false;
            }

            if (!decimal.TryParse(tbPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Пожалуйста введите валидную цену");
                return false;
            }

            if (cbCategory.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста выберите категорию");
                return false;
            }

            if (!int.TryParse(tbQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Пожалуйста введите количество");
                return false;
            }

            return true;
        }
    }
}
