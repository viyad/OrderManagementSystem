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
    /// Class name - Orders
    /// Interaction logic for Orders UI
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public partial class Orders : Page
    {
        /// <summary>
        /// MainWindow object used in this class
        /// </summary>
        private MainWindow mw = (MainWindow)App.Current.MainWindow;

        /// <summary>
        /// The Orders class constructor without any arguments
        /// </summary>
        public Orders()
        {
            InitializeComponent();
            dgOrder.ItemsSource = Controllers.Orders.GetOrderHeaders();

            mw.lblOrderId.Content = mw.lblDateTime.Content = mw.lblTotal.Content = mw.lblState.Content = ""; 
            mw.lblOrderQuantity.Visibility = mw.txtOrderQuantity.Visibility = Visibility.Hidden;
            mw.btnAddItem.Visibility = mw.btnStockItems.Visibility = mw.btnOrderView.Visibility = Visibility.Hidden;
            mw.btnSubmit.Visibility = mw.btnProcess.Visibility = mw.btnCancel.Visibility = Visibility.Hidden;
            mw.btnAddOrder.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// An event handler when this button is clicked, this method is fired
        /// </summary>
        /// <param name="sender">object, an object from the button</param>
        /// <param name="e">RoutedEventArgs, an event arguments from the btnOrderView</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderHeader orderHeader = (OrderHeader)dgOrder.SelectedItem;
            NavigationService.Navigate(new OrderDetails(orderHeader));
        }
    }
}
