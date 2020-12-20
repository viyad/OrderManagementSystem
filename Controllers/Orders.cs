using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DataAccess;

namespace Controllers
{
    /// <summary>
    /// Class name - Orders
    /// A static class that can be used conveniently without instantiating an object
    /// The general information includes:
    ///    _order and _stockItem are local variables used in the class itself
    ///    o   GetOrderHeaders() : IEnumerable<OrderHeader>
    ///    o   GetOrderHeaderById() : OrderHeader
    ///    o   CreateNewOrderHeader() : OrderHeader
    ///    o   UpsertOrderItem(int orderHeaderId, int stockItemId, int quantity) : OrderHeader
    ///    o   SubmitOrder(int orderHeaderId) : OrderHeader
    ///    o   ProcessOrder(int orderHeaderId) : OrderHeader
    ///    o   DeleteOrderHeaderAndOrderItems(int orderHeaderId)
    ///    o   DeleteOrderItem(int orderHeaderId, int stockItemId)
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class Orders
    {
        /// <summary>
        /// To establish the data access to the tables.
        /// </summary>
        private static DataAccess.Orders _order = new DataAccess.Orders();

        /// <summary>
        /// To get the connection string.
        /// </summary>
        private static DataAccess.StockItems _stockItem = new DataAccess.StockItems();

        /// <summary>
        /// To get and return all order headers.
        /// </summary>
        /// <returns>
        /// A list of order headers
        /// </returns>
        public static IEnumerable<OrderHeader> GetOrderHeaders()
        {
            List<OrderHeader> orderHeaders = (List<OrderHeader>)_order.GetOrderHeaders();
            return orderHeaders;
        }

        /// <summary>
        /// To get and return an order header of a given order header id.
        /// </summary>
        /// <returns>
        /// An header
        /// </returns>
        public static OrderHeader GetOrderHeaderById(int orderHeaderId)
        {
            OrderHeader orderHeader = _order.GetOrderHeader(orderHeaderId);
            return orderHeader;
        }

        /// <summary>
        /// To create a new order header
        /// </summary>
        /// <returns>
        /// An header
        /// </returns>
        public static OrderHeader CreateNewOrderHeader()
        {
           OrderHeader orderHeader = _order.InsertOrderHeader();
            return orderHeader;
        }

        /// <summary>
        /// To update an existing order item if it exists in the table
        /// Or to insert a new order item if it does not exist in the table
        /// </summary>
        /// <param name="orderHeaderId">An integer, a unique number that used for identifying the order header</param>
        /// <param name="stockItemId">An integer, the stock item id</param>
        /// <param name="quantity">An integer, the quantity of this item that will be taken</param>
        /// <returns>
        /// The order header of this item that has been added to.
        /// </returns>
        public static OrderHeader UpsertOrderItem(int orderHeaderId, int stockItemId, int quantity)
        {
            OrderHeader orderHeader = _order.GetOrderHeader(orderHeaderId);
            OrderItem orderItem = orderHeader.OrderItems.FirstOrDefault(i => i.StockItemId == stockItemId);
            if (orderItem != null)
            {
                orderItem.Quantity += quantity;
            }
            else
            {
                StockItem stockItem = _stockItem.GetStockItem(stockItemId);
                orderHeader.AddOrderItem(stockItemId, stockItem.Price, quantity, stockItem.Name);
            }
            _order.UpsertOrderItem(orderHeader.OrderItems.FirstOrDefault(i => i.StockItemId == stockItemId));
            return orderHeader;
        }

        /// <summary>
        /// To submit the order.
        /// </summary>
        /// <param name="orderHeaderId">An integer, a unique number that used for identifying the order header</param>
        /// <returns>
        /// The order header that has been submitted.
        /// </returns>
        public static OrderHeader SubmitOrder(int orderHeaderId)
        {
            OrderHeader orderHeader = _order.GetOrderHeader(orderHeaderId);
            orderHeader.Submit();
            _order.UpdateOrderState(orderHeader);

            return orderHeader;
        }

        /// <summary>
        /// To process the order
        /// </summary>
        /// <param name="orderHeaderId">An integer, a unique number that used for identifying the order header</param>
        /// <returns>
        /// The order header that has been processed.
        /// </returns>
        public static OrderHeader ProcessOrder(int orderHeaderId)
        {
            OrderHeader orderHeader = _order.GetOrderHeader(orderHeaderId);
            bool reject = false;

            if (orderHeader != null)
            {
                foreach (OrderItem orderItem in orderHeader.OrderItems)
                {
                    StockItem stockItem = _stockItem.GetStockItem(orderItem.StockItemId);
                    if (stockItem.InStock < orderItem.Quantity)
                    {
                        reject = true;
                        break;
                    }
                }

                if (!reject)
                {
                    _stockItem.UpdateStockItemAmount(orderHeader);
                    orderHeader.Complete();
                }
                else
                {
                    orderHeader.Reject();
                }

                _order.UpdateOrderState(orderHeader);
            }

            return orderHeader;
        }

        /// <summary>
        /// To delete the order header and the items that belong to this header from the tables
        /// </summary>
        /// <param name="orderHeaderId">An integer, a unique number that used for identifying the order header</param>
        public static void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            _order.DeleteOrderHeaderAndOrderItems(orderHeaderId);
        }

        /// <summary>
        /// To delete an order item from the table
        /// </summary>
        /// <param name="orderHeaderId">An integer, a unique number that used for identifying the order header</param>
        /// <param name="stockItemId">An integer, the stock item id of the item that will be deleted from the table</param>
        public void DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
            _order.DeleteOrderItem(orderHeaderId, stockItemId);
        
        }


        public static void ResetDatabase()
        {
            _order.ResetDatabase();

        }

    }
}
