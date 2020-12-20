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
    /// Class name - AddOrderItem
    /// Interaction logic for AddOrderItem UI
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public partial class AddOrderItem : Page
    {
        /// <summary>
        /// MainWindow object used in this class
        /// </summary>
        private MainWindow mw = (MainWindow)App.Current.MainWindow;

        /// <summary>
        /// The AddOrderItem class constructor without any arguments
        /// </summary>
        public AddOrderItem()
        {
            InitializeComponent();
            
            dgStockItems.ItemsSource = StockItems.GetStockItems();
            mw.btnStockItems.Visibility = Visibility.Hidden;
            mw.btnAddOrder.Visibility = Visibility.Hidden;
            mw.btnOrderView.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// The AddOrderItem class constructor
        /// </summary>
        /// <param name="orderHeader">OrderHeader, an order header object</param>
        public AddOrderItem(OrderHeader orderHeader)
        {
            InitializeComponent();
            mw.btnCancel.Tag = orderHeader;
            mw.btnOrderView.Visibility = Visibility.Visible;
            int quantity = 0;
            bool valid = true;
 
            
            if (int.TryParse(mw.txtOrderQuantity.Text, out quantity))
            {
                if (quantity <= 0)
                { 
                    MessageBox.Show("The Quantity must be greater than zero", "Information",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                    mw.txtOrderQuantity.Clear();
                    mw.txtOrderQuantity.Focus();
                    valid = false;
                }
            }
            else
            {
                MessageBox.Show("Please enter the Quantity", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                mw.txtOrderQuantity.Clear();
                mw.txtOrderQuantity.Focus();
                valid = false;
            }

            if (valid)
            {
                try
                {
                    StockItem stockItem = (StockItem)mw.txtOrderQuantity.Tag;
                    if (stockItem.InStock < quantity)
                        MessageBox.Show("There are currently not enough items in stock. Requested " + quantity.ToString() +
                            ", In stock: " + stockItem.InStock.ToString() + " This order might be rejected if there is " +
                            "not enough stock on hand when the order is being processed.",
                            "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    Controllers.Orders.UpsertOrderItem(orderHeader.Id, stockItem.Id, quantity);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
            else
            {
                dgStockItems.ItemsSource = StockItems.GetStockItems();
            }
        }

        /// <summary>
        /// An event handler when an item of dgStockItems is selected, this method is fired
        /// </summary>
        /// <param name="sender">object, an object from the dgStockItems</param>
        /// <param name="e">SelectionChangedEventArgs, an event arguments from the dgStockItems</param>
        private void dgStockItems_onSelected(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            mw.txtOrderQuantity.Tag = (StockItem)dg.SelectedItem;
            mw.lblOrderQuantity.Visibility = Visibility.Visible;
            mw.txtOrderQuantity.Visibility = Visibility.Visible;
            mw.txtOrderQuantity.Focus();
            mw.btnStockItems.Visibility = Visibility.Hidden;
            mw.btnAddItem.Visibility = Visibility.Visible;
        }
    }
}