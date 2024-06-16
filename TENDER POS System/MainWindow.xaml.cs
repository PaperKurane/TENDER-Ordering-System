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

namespace TENDER_POS_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TenderConnDataContext _dbConn = null;

        public MainWindow()
        {
            InitializeComponent();

            _dbConn = new TenderConnDataContext(Properties.Settings.Default.TenderConnectionString);

            LoadMenuItems("1"); // 1 is defaulted to ricemeals
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
                            Item_Price = item.Item_Price,
                            Item_Image = item.Item_Image
                        };

            return items.ToList();
        }

        private void BindMenuItems(List<MenuItem> menuItems)
        {
            for (int x = 0; x < menuItems.Count && x < 12; x++)
            {
                var item = menuItems[x];

                // Set image source
                Image img = (Image)FindName("imgItem" + (x + 1));
                if (img != null)
                {
                    try
                    {
                        string imagePath = $"pack://application:,,,/Resources/Menu Items/{item.Item_Image}";
                        img.Source = new BitmapImage(new Uri(imagePath));
                    }
                    catch
                    {
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Menu Items/defaultimg.png"));
                    }
                }

                // Set item name
                TextBox txt = (TextBox)FindName("lbItem" + (x + 1));
                if (txt != null)
                {
                    txt.Text = item.Item_Name;
                }
            }
        }

        private void imgItem1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrderWindow ow = new OrderWindow();
            ow.Owner = this;
            ow.ShowDialog();

        }
    }
}
