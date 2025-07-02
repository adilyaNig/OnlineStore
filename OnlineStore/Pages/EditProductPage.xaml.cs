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
    /// Логика взаимодействия для EditProductPage.xaml
    /// </summary>
    public partial class EditProductPage : Page
    {
        public User CurrentUser { get; set; }
        public Product CurrentProduct { get; set; }
        public List<Category> Categories { get; set; }

        public EditProductPage(User user, Product product)
        {
            InitializeComponent();
            CurrentUser = user;
            CurrentProduct = product;
            Categories = DBConn.connection.Category.ToList();

            cbCategory.ItemsSource = Categories;
            LoadProductData();
            DataContext = this;
        }

        private void LoadProductData()
        {
            tbName.Text = CurrentProduct.Name;
            tbDescription.Text = CurrentProduct.Description;
            tbPrice.Text = CurrentProduct.Price.ToString();
            tbQuantity.Text = CurrentProduct.StockQuantity.ToString();
            tbImageUrl.Text = CurrentProduct.ImageURL;
            tbRating.Text = CurrentProduct.AverageRating?.ToString("0.00") ?? "0.00";

            cbCategory.SelectedItem = Categories.FirstOrDefault(c => c.CategoryID == CurrentProduct.CategoryID);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(CurrentUser));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                CurrentProduct.Name = tbName.Text.Trim();
                CurrentProduct.Description = tbDescription.Text.Trim();
                CurrentProduct.Price = decimal.Parse(tbPrice.Text);
                CurrentProduct.CategoryID = (cbCategory.SelectedItem as Category).CategoryID;
                CurrentProduct.StockQuantity = int.Parse(tbQuantity.Text);
                CurrentProduct.ImageURL = tbImageUrl.Text.Trim();

                DBConn.connection.SaveChanges();
                MessageBox.Show("Товар успешно изменен!");
                NavigationService.Navigate(new MainPage(CurrentUser));
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить этот товар?", "Предупреждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DBConn.connection.Product.Remove(CurrentProduct);
                DBConn.connection.SaveChanges();
                MessageBox.Show("Товар успешно удален!");
                NavigationService.Navigate(new MainPage(CurrentUser));
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Пожалуйста введите название товара!");
                return false;
            }

            if (!decimal.TryParse(tbPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Пожалуйста введите корректную цену!");
                return false;
            }

            if (cbCategory.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста выберите категорию!");
                return false;
            }

            if (!int.TryParse(tbQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Пожалуйста введите кол-во на складе!");
                return false;
            }

            return true;
        }
    }
}
