using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace TENDER_POS_System
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private MenuItem _menuItem;

        public OrderWindow()
        {
            InitializeComponent();
        }

        public OrderWindow(MenuItem menuItem)
        {
            InitializeComponent();

            _menuItem = menuItem;

            DisplayItemDetails();
        }
        private void DisplayItemDetails()
        {
            // Display the details of the selected item in the OrderWindow
            tbMealName.Text = _menuItem.Item_Name;
            tbMealPrice.Text = $"Price: {_menuItem.Item_Price.ToString("C", new CultureInfo("fil-PH"))}";

            try
            {
                string imagePath = $"pack://application:,,,/Resources/Menu Items/{_menuItem.Item_Image}";
                imgPicture.Source = new BitmapImage(new Uri(imagePath));
            }
            catch
            {
                imgPicture.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Menu Items/defaultimg.png"));
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
