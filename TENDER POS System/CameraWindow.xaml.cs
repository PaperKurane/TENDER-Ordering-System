using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Win32;

namespace TENDER_POS_System
{
    /// <summary>
    /// Interaction logic for CameraWindow.xaml
    /// </summary>
    public partial class CameraWindow : Window
    {
        private MenuItem _menuItem;

        TenderConnDataContext _dbConn = null;

        FilterInfoCollection fic = null;
        VideoCaptureDevice vcd = null;

        BitmapImage _default = new BitmapImage();

        bool _cameraMode = false;

        public CameraWindow()
        {
            InitializeComponent();
        }

        public CameraWindow(MenuItem menuItem, TenderConnDataContext connection)
        {
            InitializeComponent();

            _menuItem = menuItem;

            _dbConn = connection;

            DisplayItemDetails();
        }

        private void DisplayItemDetails()
        {
            lbStatusbar.Content = "Select an Image for " + _menuItem.Item_Name + "...";

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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Camera
        private void btnCamera_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _cameraMode = true;
            grdCameraMode.Visibility = Visibility.Visible;
            grdUploadMode.Visibility = Visibility.Collapsed;

            vcd = new VideoCaptureDevice(fic[cmbCameras.SelectedIndex].MonikerString);
            vcd.NewFrame += Vcd_NewFrame;
            vcd.Start();
        }

        private void Vcd_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //pic.Source = eventArgs.Frame.Clone();

            // This line allows the event to modify the content of the image box
            // without this block of code, the thread holding the Image box and the thread
            // holding the image will not talk to each other
            this.Dispatcher.Invoke(() =>
            {
                /*
                 * This convoluted piece of code will convert the bitmap image of the video
                 * source (Your selected webcam) into something the Imagebox can load.
                 * I suggest not to change this section of the code.
                 */
                imgPicture.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        ((Bitmap)eventArgs.Frame.Clone()).GetHbitmap(),
                        IntPtr.Zero,
                        System.Windows.Int32Rect.Empty,
                        BitmapSizeOptions.FromWidthAndHeight((int)imgPicture.Width, (int)imgPicture.Height));
            });

            //MessageBox.Show(eventArgs.Frame.Clone().ToString());
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo fi in fic)
                cmbCameras.Items.Add(fi.Name);
            cmbCameras.SelectedIndex = 0;
            //vcd = new VideoCaptureDevice();
        }

        public void ImageToFile(string filePath)
        {
            var image = imgPicture.Source;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image));
                encoder.Save(fileStream);
            }
        }

        private void btnCaptureImage_Click(object sender, RoutedEventArgs e)
        {
            //if (vcd.IsRunning)
            //{
            //    ImageToFile("Test.png");
            //    vcd.WaitForStop();
            //    vcd = null;
            //    //vcd.Stop();
            //}
            ImageToFile("Test.png");

            // Place path to file here for sql submitting

            CloseCamera();

            this.Close();
        }

        private void CloseCamera()
        {
            if (_cameraMode == true)
            {
                //ImageToFile("Test.png");
                vcd.SignalToStop();
                vcd.WaitForStop();
                vcd = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                _cameraMode = false;
            }
        }
        #endregion 

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseCamera();
        }

        private void btnUpload_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseCamera();
            grdUploadMode.Visibility = Visibility.Visible;
            grdCameraMode.Visibility = Visibility.Collapsed;

            DisplayItemDetails();

            if (_cameraMode == false)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Browse Photos...";
                ofd.DefaultExt = "png";
                ofd.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" +
                    "All files (*.*)|*.*";

                ofd.ShowDialog();

                if (ofd.FileName.Length > 0)
                {
                    lbFileName.Content = ofd.FileName;
                }
            }
        }

        private void btnConfirmC_Click(object sender, RoutedEventArgs e)
        {
            if (imgPicture.Source != null)
            {
                //string fileName = $"{_menuItem.Item_Name}_{DateTime.Now:yyyyMMddHH}.png";
                string fileName = $"{_menuItem.Item_Name.Replace(" ","").ToLower()}.jpg";

                // Determine the correct project directory
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string relativePath = @"..\..\..\TENDER POS System\Resources\Menu Items";
                string directoryPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(projectDirectory, relativePath));

                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Complete file path
                string localPath = System.IO.Path.Combine(directoryPath, fileName);


                SaveImageToFile(localPath);

                _menuItem.Item_Image = fileName;

                UpdateMenuItemImageInDatabase(fileName);

                CloseCamera();
                this.Close();
            }
            else
            {
                MessageBox.Show("No image to save.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveImageToFile(string localPath)
        {
            try
            {
                // Ensure the directory exists
                string directoryPath = System.IO.Path.GetDirectoryName(localPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Save the image to the specified path
                using (var fileStream = new FileStream(localPath, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgPicture.Source));
                    encoder.Save(fileStream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving image: {ex.Message}");
            }
        }

        private void UpdateMenuItemImageInDatabase(string fileName)
        {
            var item = _dbConn.Menu_Items.FirstOrDefault(i => i.Item_ID == _menuItem.Item_ID);
            if (item != null)
            {
                item.Item_Image = fileName;
                _dbConn.SubmitChanges();
            }
        }

        private void btnConfirmU_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
