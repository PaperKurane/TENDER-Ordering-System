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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lbItem1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void imgItem1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrderWindow ow = new OrderWindow();
            ow.Owner = this;
            ow.ShowDialog();

        }
    }
}
