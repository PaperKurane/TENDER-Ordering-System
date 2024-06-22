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
using System.Net.NetworkInformation;
using Path = System.IO.Path;

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

        string imagePath = null;

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
                imagePath = $"E:/ProgrammingShit/TENDER Ordering System/Menu Items/{_menuItem.Item_Image}";
                BitmapImage bmi = LoadImage(imagePath);
                imgPicture.Source = bmi;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}");
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

        #region Camera
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo fi in fic)
                cmbCameras.Items.Add(fi.Name);
            cmbCameras.SelectedIndex = 0;
        }

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

        private void CloseCamera()
        {
            if (_cameraMode == true)
            {
                vcd.SignalToStop();
                //vcd.WaitForStop();
                vcd = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                _cameraMode = false;
            }
        }

        private void btnConfirmC_Click(object sender, RoutedEventArgs e)
        {
            if (_cameraMode == true)
            {
                try
                {
                    string ext = "jpg";
                    string tempFilePath = $"E:/ProgrammingShit/TENDER Ordering System/Temp/" + _menuItem.Item_Name.Replace(" ", "").ToLower() + "." + ext;

                    SaveCapturedImage(tempFilePath);

                    File.Copy(tempFilePath, imagePath, true);
                    _dbConn.uspUpdatePicturePath(_menuItem.Item_Name, _menuItem.Item_Name.Replace(" ", "").ToLower() + "." + ext);

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving image: {ex.Message}");
                }
            }
        }

        private void SaveCapturedImage(string tempFilePath)
        {
            if (imgPicture.Source is BitmapSource bitmapSource)
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }
            else
            {
                MessageBox.Show("Image source is not a valid BitmapSource.");
            }
        }
        #endregion 

        #region Upload
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

        private void btnConfirmU_Click(object sender, RoutedEventArgs e)
        {
            if (_cameraMode == false)
            {
                try
                {
                    // Get the file path from lbFileName.Content
                    string sourceFilePath = lbFileName.Content.ToString();

                    // Ensure the source file path is valid
                    if (!File.Exists(sourceFilePath))
                    {
                        MessageBox.Show("The selected file does not exist.");
                        return;
                    }

                    string ext = "jpg";

                    // Define the target file path
                    string targetFileName = _menuItem.Item_Name.Replace(" ", "").ToLower() + "." + ext;
                    string targetFilePath = Path.Combine("E:/ProgrammingShit/TENDER Ordering System/Menu Items/", targetFileName);

                    File.Copy(sourceFilePath, targetFilePath, true);

                    _dbConn.uspUpdatePicturePath(_menuItem.Item_Name, targetFileName);

                    imgPicture.Source = LoadImage(targetFilePath);

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving image: {ex.Message}");
                }
            }
        }
        #endregion

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseCamera();
        }
    }
}
