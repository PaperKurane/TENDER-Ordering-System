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
        bool EmployeeMode = false;
        private string _currentCategoryID = "1"; // 1 is defaulted to ricemeals

        public MainWindow()
        {
            InitializeComponent();

            _dbConn = new TenderConnDataContext(Properties.Settings.Default.TenderConnectionString);
            LoadMenuItems(_currentCategoryID);
        }

        private void LoadMenuItems(string categoryID)
        {
            List<MenuItem> menuItems = GetMenuItems(categoryID);
            BindMenuItems(menuItems);
        }

        private List<MenuItem> GetMenuItems(string categoryID)
        {
            var items = from item in _dbConn.Menu_Items
                        where item.Category_ID == categoryID
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
                        //string imagePath = $"pack://application:,,,/Resources/Menu Items/{item.Item_Image}";
                        string imagePath = $"E:/ProgrammingShit/TENDER Ordering System/Menu Items/{item.Item_Image}";
                        BitmapImage bmi = LoadImage(imagePath);
                        //img.Source = new BitmapImage(new Uri(imagePath));
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

            LoadMenuItems(_currentCategoryID);

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

        private void imgItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            MenuItem item = img.Tag as MenuItem;

            if (item != null)
            {
                OrderWindow ow = new OrderWindow(item, _dbConn);
                ow.Owner = this;
                ow.ShowDialog();

                LoadMenuItems(_currentCategoryID);
            }
        }
    }
}