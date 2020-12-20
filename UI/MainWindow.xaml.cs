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
using Domain;
using Controllers;

namespace UI
{
    /// <summary>
    /// Class name - MainWindow
    /// Interaction logic for MainWindow UI
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The MainWindow class constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            frame.Navigate(new UI.Orders());
        }

        /// <summary>
        /// An event handler when btnAddOrder is clicked, this method is fired
        /// </summary>
        /// <param name="sender">object, an object from the btnAddOrder</param>
        /// <param name="e">RoutedEventArgs, an event arguments from the btnAddOrder</param>
        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderHeader orderHeader = Controllers.Orders.CreateNewOrderHeader();
                frame.Navigate(new AddOrder(orderHeader));
            }
            catch (InvalidOrderStateException exp)
            {
                MessageBox.Show(exp.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// An event handler when btnAddItem is clicked, this method is fired
        /// </summary>
        /// <param name="sender">object, an object from the btnAddItem</param>
        /// <param name="e">RoutedEventArgs, an event arguments from the btnAddItem</param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            OrderHeader orderHeader = (OrderHeader)((Button)sender).Tag;
            int quantity = 0;

            frame.Navigate(new AddOrderItem(orderHeader));
            //MessageBox.Show(orderHeader.OrderItems.ToString());
            if (int.TryParse(txtOrderQuantity.Text, out quantity))
            {
                if (quantity > 0)
                    frame.Navigate(new AddOrder(orderHeader));
            }
            txtOrderQuantity.Clear();
        }

        /// <summary>
        /// An event handler when btnStockItems is clicked, this method is fired
        /// </summary>
        /// <param name="sender">object, an object from the btnStockItems</param>
        /// <param name="e">RoutedEventArgs, an event arguments from the btnStockItems</param>
        private void btnStockItems_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AddOrderItem());
        }

        /// <summary>
        /// An event handler when btnSubmit is clicked, this method is fired
        /// </summary>
        /// <param name="sender">object, an object from the btnSubmit</param>
        /// <param name="e">RoutedEventArgs, an event arguments from the btnSubmit</param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderHeader orderHeader = (OrderHeader)((Button)sender).Tag;
                Controllers.Orders.SubmitOrder(orderHeader.Id);
                frame.Navigate(new UI.Orders());
            }
            catch (InvalidOrderStateException exp)
            {
                MessageBox.Show(exp.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// An event handler when btnProcess is clicked, this method is fired
        /// </summary>
        /// <param name="sender">object, an object from the btnProcess</param>
        /// <param name="e">RoutedEventArgs, an event arguments from the btnProcess</param>
        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderHeader orderHeader = (OrderHeader)((Button)sender).Tag;
                Controllers.Orders.ProcessOrder(orderHeader.Id);
                frame.Navigate(new UI.Orders());
            }
            catch (InvalidOrderStateException exp)
            {
                MessageBox.Show(exp.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// An event handler when btnCancelOrder is clicked, this method is fired
        /// </summary>
        /// <param name="sender">object, an object from the btnCancelOrder</param>
        /// <param name="e">RoutedEventArgs, an event arguments from the btnCancelOrder</param>
        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderHeader orderHeader = (OrderHeader)((Button)sender).Tag;
                Controllers.Orders.DeleteOrderHeaderAndOrderItems(orderHeader.Id);
                frame.Navigate(new UI.Orders());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// An event handler when btnOrderView is clicked, this method is fired
        /// </summary>
        /// <param name="sender">object, an object from the btnOrderView</param>
        /// <param name="e">RoutedEventArgs, an event arguments from the btnOrderView</param>
        private void btnOrderView_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new UI.Orders());
        }
    }
}
