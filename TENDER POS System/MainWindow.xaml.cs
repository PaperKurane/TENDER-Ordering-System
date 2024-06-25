using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TENDER_POS_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TenderConnDataContext _dbConn = null;
        bool _EmployeeMode;
        private string _currentCategoryID = "1"; // 1 is defaulted to ricemeals

        private Dictionary<string, (string itemName, int itemPrice, int quantity)> itemsDictionary = new Dictionary<string, (string itemName, int itemPrice, int quantity)>();
        private string _selectedItem = "";
        private string _itemName = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(bool EmployeeMode)
        {
            InitializeComponent();

            _EmployeeMode = EmployeeMode;

            _dbConn = new TenderConnDataContext(Properties.Settings.Default.TenderConnectionString);
            LoadMenuItems();
        }

        private void LoadMenuItems()
        {
            List<MenuItem> menuItems = GetMenuItems();
            BindMenuItems(menuItems);
        }

        private List<MenuItem> GetMenuItems()
        {
            var items = from item in _dbConn.Menu_Items
                        where item.Category_ID == _currentCategoryID
                        select new MenuItem
                        {
                            Item_ID = item.Item_ID,
                            Item_Name = item.Item_Name,
                            Category_ID = item.Category_ID,
                            Item_Description = item.Item_Description,
                            Item_Price = item.Item_Price,
                            Item_Image = item.Item_Image
                        };

            return items.ToList();
        }

        private void BindMenuItems(List<MenuItem> menuItems)
        {
            for (int x = 0; x <= 12; x++)
            {
                Image img = (Image)FindName("imgItem" + x);
                TextBox text = (TextBox)FindName("lbItem" + x);

                if (img != null) img.Visibility = Visibility.Collapsed;
                if (text != null) text.Visibility = Visibility.Collapsed;
            }

            for (int x = 0; x < menuItems.Count && x < 12; x++)
            {
                var item = menuItems[x];

                // Set image source
                Image img = (Image)FindName("imgItem" + (x + 1));
                if (img != null)
                {
                    try
                    {
                        string imagePath = $"E:/ProgrammingShit/TENDER Ordering System/Menu Items/{item.Item_Image}";
                        BitmapImage bmi = LoadImage(imagePath);
                        img.Source = bmi;
                    }
                    catch
                    {
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Menu Items/defaultimg.png"));
                    }

                    img.Tag = item;
                    img.Visibility = Visibility.Visible;
                }

                // Set item name
                TextBox text = (TextBox)FindName("lbItem" + (x + 1));
                if (text != null)
                {
                    text.Text = item.Item_Name;
                    text.Visibility = Visibility.Visible;
                }
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

        private void CategoryChanger(object sender, MouseButtonEventArgs e)
        {
            Image category = sender as Image;
            _currentCategoryID = category.Tag.ToString();

            LoadMenuItems();

            switch (_currentCategoryID)
            {
                case "1":
                    lbStatusbar.Content = "Now viewing rice meals...";
                    break;
                case "2":
                    lbStatusbar.Content = "Now viewing the ramen selection...";
                    break;
                case "3":
                    lbStatusbar.Content = "Now viewing our collection of beverages...";
                    break;
                case "4":
                    lbStatusbar.Content = "Now viewing the list of side dishes...";
                    break;
            }
        }

        private void StatusBarHandler(bool UpdateSuccess, MenuItem item)
        {
            if (UpdateSuccess == true)
            {
                lbStatusbar.Content = "Successfully Updated " + item.Item_Name + "!";
            }
        }

        private void imgItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            MenuItem item = img.Tag as MenuItem;

            if (item != null)
            {
                OrderWindow ow = new OrderWindow(item, _dbConn, _EmployeeMode);
                ow.Owner = this;
                ow.ShowDialog();

                StatusBarHandler(ow.UpdateSuccess, item);

                LoadMenuItems();
            }
        }

        private void btnExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        #region Receipt System
        public void AddItemToListBox(string itemName, int itemPrice)
        {
            if (itemsDictionary.ContainsKey(itemName))
            {
                if (itemsDictionary[_itemName].quantity < 10)
                {
                    itemsDictionary[itemName] = (itemName, itemPrice, itemsDictionary[itemName].quantity + 1);
                }
            }
            else
            {
                itemsDictionary[itemName] = (itemName, itemPrice, 1);
            }

            UpdateListBox();
            UpdateTotalPrice();
        }

        private void UpdateListBox()
        {
            lbxOrderList.Items.Clear();
            foreach (var item in itemsDictionary)
            {
                if (item.Value.quantity > 10)
                    MessageBox.Show("Cannot order more than 10!");
                else
                    lbxOrderList.Items.Add($"{item.Value.itemName} x {item.Value.quantity} = ₱{item.Value.itemPrice * item.Value.quantity}");
                //lbxOrderList.Items.Add($"{item.Value.itemName} - ${item.Value.itemPrice} x {item.Value.quantity} = ${item.Value.itemPrice * item.Value.quantity}");
            }
        }

        private void UpdateTotalPrice()
        {
            int totalPrice = itemsDictionary.Sum(item => item.Value.itemPrice * item.Value.quantity);
            lblTotalPrice.Content = $"Total Price: ₱{totalPrice:F2}";
        }

        private void btnIncrementQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (lbxOrderList.SelectedItem != null)
            {
                _selectedItem = lbxOrderList.SelectedItem.ToString();
                _itemName = _selectedItem.Split('x')[0].Trim();

                if (itemsDictionary.ContainsKey(_itemName))
                {
                    if (itemsDictionary[_itemName].quantity < 10)
                    {
                        itemsDictionary[_itemName] = (itemsDictionary[_itemName].itemName, itemsDictionary[_itemName].itemPrice, itemsDictionary[_itemName].quantity + 1);
                        UpdateListBox();
                        UpdateTotalPrice();
                    }
                }
            }
            else
            {
                if (itemsDictionary.ContainsKey(_itemName))
                {
                    if (itemsDictionary[_itemName].quantity < 10)
                    {
                        itemsDictionary[_itemName] = (itemsDictionary[_itemName].itemName, itemsDictionary[_itemName].itemPrice, itemsDictionary[_itemName].quantity + 1);
                        UpdateListBox();
                        UpdateTotalPrice();
                    }
                }
            }
        }

        private void btnDecrementQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (lbxOrderList.SelectedItem != null)
            {
                _selectedItem = lbxOrderList.SelectedItem.ToString();
                _itemName = _selectedItem.Split('x')[0].Trim();

                if (itemsDictionary.ContainsKey(_itemName))
                {
                    var currentItem = itemsDictionary[_itemName];
                    if (currentItem.quantity > 1)
                    {
                        itemsDictionary[_itemName] = (currentItem.itemName, currentItem.itemPrice, currentItem.quantity - 1);
                    }
                    else
                    {
                        itemsDictionary.Remove(_itemName);
                    }

                    UpdateListBox();
                    UpdateTotalPrice();
                }
            }
            else
            {
                if (itemsDictionary.ContainsKey(_itemName))
                {
                    var currentItem = itemsDictionary[_itemName];
                    if (currentItem.quantity > 1)
                    {
                        itemsDictionary[_itemName] = (currentItem.itemName, currentItem.itemPrice, currentItem.quantity - 1);
                    }
                    else
                    {
                        itemsDictionary.Remove(_itemName);
                    }

                    UpdateListBox();
                    UpdateTotalPrice();
                }
            }
        }

        private void btnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (itemsDictionary.Count > 0)
            {
                EndWindow ew = new EndWindow(itemsDictionary);
                ew.ShowDialog();
            }
            else
            {
                //Make it so that the place order button cannot be clicked until itemsDictionary contains atleast 1 item.
                MessageBox.Show("No items in the order to submit.");
            }
        }
        #endregion
    }
}