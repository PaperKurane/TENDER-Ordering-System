using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TENDER_POS_System
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private MenuItem _menuItem;
        TenderConnDataContext _dbConn = null;
        bool _EmployeeMode = false;

        public bool UpdateSuccess = false;

        public OrderWindow()
        {
            InitializeComponent();
        }

        public OrderWindow(MenuItem menuItem, TenderConnDataContext connection, bool EmployeeMode)
        {
            InitializeComponent();

            _menuItem = menuItem;
            _dbConn = connection;
            _EmployeeMode = EmployeeMode;

            DisplayItemDetails();

            AccessController();
        }

        private void DisplayItemDetails()
        {
            tbMealName.Text = _menuItem.Item_Name;
            tbMealPrice.Text = $"{_menuItem.Item_Price.ToString()}";
            tbMealDesc.Text = _menuItem.Item_Description;

            try
            {
                string imagePath = $"E:/ProgrammingShit/TENDER Ordering System/Menu Items/{_menuItem.Item_Image}";
                BitmapImage bmi = LoadImage(imagePath);
                imgPicture.Source = bmi;
            }
            catch
            {
                imgPicture.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Menu Items/defaultimg.png"));
            }
        }

        private BitmapImage LoadImage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
            }
            bitmap.Freeze(); // Ensure the image is decoupled from the file
            return bitmap;
        }

        private void AccessController()
        {
            if (_EmployeeMode == true)
            {
                tbMealName.IsReadOnly = false;
                tbMealPrice.IsReadOnly = false;
                tbMealDesc.IsReadOnly = false;
            }
            else
            {
                tbMealName.IsReadOnly = true;
                tbMealPrice.IsReadOnly = true;
                tbMealDesc.IsReadOnly = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (_EmployeeMode == true)
            {
                string itemName = tbMealName.Text;
                int itemPrice = int.Parse(tbMealPrice.Text);
                string itemDescription = tbMealDesc.Text;

                if (itemName.Length > "Tonkotsu Miso Overload".Length)
                {
                    MessageBox.Show("Item Name cannot be longer than 'Tonkotsu Miso Overload'.");
                    return;
                }

                // Validate Item_Price
                if (!int.TryParse(tbMealPrice.Text, out itemPrice) || tbMealPrice.Text.Length > 5)
                {
                    MessageBox.Show("Item Price must be a number and cannot be longer than 5 digits.");
                    return;
                }

                // Validate Item_Description
                if (itemDescription.Length > 100)
                {
                    MessageBox.Show("Item Description cannot be longer than this!.");
                    return;
                }

                _menuItem.Item_Name = itemName;
                _menuItem.Item_Price = itemPrice;
                _menuItem.Item_Description = itemDescription;

                try
                {
                    _dbConn.UpdateMenuItem(_menuItem.Item_ID, _menuItem.Item_Name, _menuItem.Category_ID, _menuItem.Item_Description, _menuItem.Item_Price, _menuItem.Item_Image);
                    //MessageBox.Show("Menu item updated successfully.");

                    UpdateSuccess = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating menu item: {ex.Message}");
                }
            }
            else // NEW SHIT
            {
                string itemName = tbMealName.Text;
                int itemPrice = int.Parse(tbMealPrice.Text);

                MainWindow mainWindow = this.Owner as MainWindow;

                if (mainWindow != null)
                {
                    mainWindow.AddItemToListBox(itemName, itemPrice);
                }

                this.Close();
            }
        }

        private void imgPicture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_EmployeeMode == true)
            {
                CameraWindow cw = new CameraWindow(_menuItem, _dbConn);
                cw.Owner = this;
                cw.ShowDialog();

                DisplayItemDetails();
            }
        }
    }
}
