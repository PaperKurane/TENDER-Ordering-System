using System;
using System.Collections.Generic;
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

        public OrderWindow()
        {
            InitializeComponent();
        }

        public OrderWindow(MenuItem menuItem, TenderConnDataContext connection)
        {
            InitializeComponent();

            _menuItem = menuItem;

            _dbConn = connection;

            DisplayItemDetails();
        }

        private void DisplayItemDetails()
        {
            tbMealName.Text = _menuItem.Item_Name;
            tbMealPrice.Text = $"Price: {_menuItem.Item_Price.ToString("C", new CultureInfo("fil-PH"))}";

            try
            {
                //string imagePath = $"pack://application:,,,/Resources/Menu Items/{_menuItem.Item_Image}";
                string imagePath = $"E:/ProgrammingShit/TENDER Ordering System/Menu Items/{_menuItem.Item_Image}";
                BitmapImage bmi = LoadImage(imagePath);
                //imgPicture.Source = new BitmapImage(new Uri(imagePath));
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void imgPicture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CameraWindow cw = new CameraWindow(_menuItem, _dbConn);
            //this.Hide();
            //cw.ShowDialog();
            //this.Show();
            cw.Owner = this;
            cw.ShowDialog();

            DisplayItemDetails();
        }
    }
}
