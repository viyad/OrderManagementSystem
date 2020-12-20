using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Class name - OrderItem
    /// OrderItem is a class to keep data of an order item and its order header
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Store for its order header
        /// </summary>
        private OrderHeader _orderHeader;

        /// <summary>
        /// Store for its stock item id
        /// </summary>
        private int _stockItemId;

        /// <summary>
        /// Store for its order header id
        /// </summary>
        private int _orderHeaderId;

        /// <summary>
        /// Store for its price
        /// </summary>
        private decimal _price;

        /// <summary>
        /// Store for its quantity
        /// </summary>
        private int _quantity;

        /// <summary>
        /// Store for its description
        /// </summary>
        private string _description;

        /// <summary>
        /// The OrderItem class constructor.
        /// </summary>
        /// <param name="orderHeader">OrderHeader, its order header object</param>
        /// <param name="stockItemId">An integer, the stock id of this order item</param>
        /// <param name="price">A deecimal, the price of this order item</param>
        /// <param name="quantity">An integer, the quantity of this order item</param>
        /// <param name="description">A string, the description of this order item</param>
        public OrderItem(OrderHeader orderHeader, int stockItemId, 
                        decimal price, int quantity, string description)
        {
            _orderHeader = orderHeader;
            _stockItemId = stockItemId;
            _orderHeaderId = orderHeader.Id;
            _price = price;
            _quantity = quantity;
            _description = description;
        }

        /// <summary>
        /// OrderHeader property - get and set the value of the order header
        /// </summary>
        /// <value>
        /// OrderHeader
        /// </value>
        public OrderHeader OrderHeader
        {
            get { return _orderHeader; }
            set { _orderHeader = value; }
        }

        /// <summary>
        /// StockItemId property - get and set the value of the stock item id
        /// </summary>
        /// <value>
        /// Integer
        /// </value>
        public int StockItemId
        {
            get { return _stockItemId; }
            private set { _stockItemId = value; }
        }

        /// <summary>
        /// OrderHeaderId property - get and set the value of the order header id
        /// </summary>
        /// <value>
        /// Integer
        /// </value>
        public int OrderHeaderId
        {
            get { return _orderHeaderId; }
            private set { _orderHeaderId = value; }
        }

        /// <summary>
        /// Price property - get and set the value of the price
        /// </summary>
        /// <value>
        /// Decimal
        /// </value>
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        /// <summary>
        /// Quantity property - get and set the value of the quantity
        /// </summary>
        /// <value>
        /// Integer
        /// </value>
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        /// <summary>
        /// Description property - get and set the value of the description
        /// </summary>
        /// <value>
        /// String
        /// </value>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Total property - get and set the value of the total
        /// </summary>
        /// <value>
        /// Decimal
        /// </value>
        public decimal Total
        {
            get { return _quantity * _price; }
            set { }
        }
    }
}