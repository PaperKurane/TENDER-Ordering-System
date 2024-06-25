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
using System.Windows.Shapes;
using System.Threading;
using System.Drawing;

namespace TENDER_POS_System
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private List<int> correctPIN = new List<int> {1, 1, 0, 5};
        private List<int> pin = new List<int>();
        private List<Ellipse> ellipses;

        public StartWindow()
        {
            InitializeComponent();

            ellipses = new List<Ellipse> {ePos1, ePos2, ePos3, ePos4};
        }

        private void btnCustomerButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mw = new MainWindow(false);
            mw.Owner = this;
            mw.ShowDialog();

            pin.Clear();
            lbPINStatus.Content = "Enter Correct MPIN";
            lbPINStatus.Foreground = System.Windows.Media.Brushes.White;
            UpdateEllipses();

            grdPIN.Visibility = Visibility.Collapsed;
        }

        private void btnEmployeeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (grdPIN.Visibility == Visibility.Visible)
            {
                grdPIN.Visibility = Visibility.Collapsed;

                pin.Clear();
                lbPINStatus.Content = "Enter Correct MPIN";
                lbPINStatus.Foreground = System.Windows.Media.Brushes.White;
                UpdateEllipses();
            }
            else
                grdPIN.Visibility = Visibility.Visible;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (pin.Count < 4)
            {
                Button button = sender as Button;
                int number = int.Parse(button.Content.ToString());
                pin.Add(number);
                UpdateEllipses();

                if (pin.Count == 4)
                    EmployeeLogin();
            }
        }

        private void btnBkSpc_Click(object sender, RoutedEventArgs e)
        {
            if (pin.Count > 0)
            {
                pin.RemoveAt(pin.Count - 1);
                UpdateEllipses();
            }
        }

        private void UpdateEllipses()
        {
            for (int i = 0; i < ellipses.Count; i++)
            {
                if (i < pin.Count)
                    ellipses[i].Fill = System.Windows.Media.Brushes.White;
                else
                    ellipses[i].Fill = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString("#FFCF7255");
            }
        }

        private void EmployeeLogin()
        {
            bool isCorrect = true;
            for (int i = 0; i < correctPIN.Count; i++)
            {
                if (pin[i] != correctPIN[i])
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                MainWindow mw = new MainWindow(true);
                mw.Owner = this;
                mw.ShowDialog();

                pin.Clear();
                lbPINStatus.Content = "Enter Correct MPIN";
                lbPINStatus.Foreground = System.Windows.Media.Brushes.White;
                UpdateEllipses();

                grdPIN.Visibility = Visibility.Collapsed;
            }
            else
            {
                lbPINStatus.Content = "Incorrect MPIN!";
                lbPINStatus.Foreground = System.Windows.Media.Brushes.Red;

                pin.Clear();
                UpdateEllipses();
            }
        }
    }
}
