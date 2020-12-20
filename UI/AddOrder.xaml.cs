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
    /// Class name - AddOrder
    /// Interaction logic for AddOrder UI
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public partial class AddOrder : Page
    {
        /// <summary>
        /// MainWindow object used in this class
        /// </summary>
        private MainWindow mw = (MainWindow)App.Current.MainWindow;

        /// <summary>
        /// The AddOrder class constructor
        /// </summary>
        /// <param name="orderHeader">OrderHeader, an order header object</param>
        public AddOrder(OrderHeader orderHeader)
        {
            
            InitializeComponent();

            OrderHeader oH = Controllers.Orders.GetOrderHeaderById(orderHeader.Id);
            dgOrderItems.ItemsSource = oH.OrderItems;
            mw.btnAddItem.Tag = oH;
            mw.btnCancel.Tag = oH;

            mw.lblOrderId.Content = "Order Id: " + oH.Id;
            mw.lblDateTime.Content = "Datetime: " + oH.DateTime.ToShortDateString().ToString() + " " + oH.DateTime.ToLongTimeString().ToString();
            mw.lblTotal.Content = "Total: " + oH.Total.ToString("c"); ;
            mw.lblState.Content = "State: " + oH.State.State;

            mw.btnSubmit.Visibility = Visibility.Hidden;
            mw.btnAddItem.Visibility = Visibility.Hidden;
            mw.lblOrderQuantity.Visibility = Visibility.Hidden;
            mw.txtOrderQuantity.Visibility = Visibility.Hidden;

            mw.btnCancel.Visibility = Visibility.Visible;
            
            if (oH.State.State == OrderStates.New)
            {
                mw.btnStockItems.Visibility = Visibility.Visible;

                if (oH.OrderItems.Count > 0)
                {
                    mw.btnSubmit.Visibility = Visibility.Visible;
                    mw.btnSubmit.Tag = oH;
                }
            }

            
        }
    }
}
