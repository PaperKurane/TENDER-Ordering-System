using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace TENDER_POS_System
{
    /// <summary>
    /// Interaction logic for EndWindow.xaml
    /// </summary>
    public partial class EndWindow : Window
    {
        private bool _takeOut = false;
        private Dictionary<string, (string itemName, int itemPrice, int quantity)> _orderItems;

        public EndWindow()
        {
            InitializeComponent();
        }

        public EndWindow(Dictionary<string, (string itemName, int itemPrice, int quantity)> orderItems)
        {
            InitializeComponent();
            _orderItems = orderItems;
            DisplayOrderItems();
            UpdateTotalPrice();
        }

        private void DisplayOrderItems()
        {
            foreach (var item in _orderItems)
            {
                lbxOrderSummary.Items.Add($"{item.Value.itemName} x {item.Value.quantity} = ₱{item.Value.itemPrice * item.Value.quantity}");
            }
        }

        private void UpdateTotalPrice()
        {
            int total = _orderItems.Sum(item => item.Value.itemPrice * item.Value.quantity);
            lbTotalPrice.Content = $"Total: ₱{total}";
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.TenderConnectionString1))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("InsertOrder", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Order_Date", DateTime.Now);
                        if (_takeOut == true)
                        {
                            cmd.Parameters.AddWithValue("@Total_Amount", _orderItems.Sum(item => item.Value.itemPrice * item.Value.quantity) + 8);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Total_Amount", _orderItems.Sum(item => item.Value.itemPrice * item.Value.quantity));
                        }

                        SqlParameter orderIdParam = new SqlParameter("@Order_ID", System.Data.SqlDbType.Int);
                        orderIdParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(orderIdParam);

                        cmd.ExecuteNonQuery();
                        int orderId = (int)orderIdParam.Value;

                        foreach (var item in _orderItems)
                        {
                            using (SqlCommand itemCmd = new SqlCommand("InsertOrderItem", conn))
                            {
                                itemCmd.CommandType = System.Data.CommandType.StoredProcedure;

                                itemCmd.Parameters.AddWithValue("@Order_ID", orderId);
                                itemCmd.Parameters.AddWithValue("@Item_ID", item.Key);
                                itemCmd.Parameters.AddWithValue("@Quantity", item.Value.quantity);
                                itemCmd.Parameters.AddWithValue("@Price", item.Value.itemPrice);

                                itemCmd.ExecuteNonQuery();
                            }
                        }

                        _orderItems.Clear();
                        lbxOrderSummary.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting order: " + ex.Message);
            }
            ThankYouScreen();
        }

        private void btnTake_Click(object sender, RoutedEventArgs e)
        {
            _takeOut = true;
            btnDine_Click(sender, e);
        }

        private void ThankYouScreen()
        {
            lbxOrderSummary.Visibility = Visibility.Collapsed;
            lbTotalPrice.Visibility = Visibility.Collapsed;
            btnDine.Visibility = Visibility.Collapsed;
            btnTake.Visibility = Visibility.Collapsed;
            btnReturn.Visibility = Visibility.Collapsed;
            lbEndMsg.Content = "Arigathanks Gozaimuch!!!";
            imgLogo.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (imgLogo.Visibility == Visibility.Visible)
            {
                StartWindow sw = new StartWindow();
                sw.Show();
                this.Close();
            }
        }
    }
}
