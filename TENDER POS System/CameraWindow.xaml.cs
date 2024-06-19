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

namespace TENDER_POS_System
{
    /// <summary>
    /// Interaction logic for CameraWindow.xaml
    /// </summary>
    public partial class CameraWindow : Window
    {
        private MenuItem _menuItem;

        FilterInfoCollection fic = null;
        VideoCaptureDevice vcd = null;

        public CameraWindow()
        {
            InitializeComponent();
        }

        public CameraWindow(MenuItem menuItem)
        {
            InitializeComponent();

            _menuItem = menuItem;

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

        private void btnBackC_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCameraC_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
            vcd = new VideoCaptureDevice();
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

            vcd.SignalToStop();
            vcd = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (vcd.IsRunning)
            {
                //ImageToFile("Test.png");
                vcd.SignalToStop(); // tells camera to stop working
                vcd.WaitForStop();
                vcd = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
