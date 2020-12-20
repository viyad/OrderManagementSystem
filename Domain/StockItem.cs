using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Class name - StockItem
    /// StockItem is a class to keep data of an stock item
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class StockItem
    {
        /// <summary>
        /// Store for its stock item id
        /// </summary>
        private int _id;

        /// <summary>
        /// Store for its quantity on hand
        /// </summary>
        private int _inStock;

        /// <summary>
        /// Store for its name
        /// </summary>
        private string _name;

        /// <summary>
        /// Store for its price
        /// </summary>
        private decimal _price;

        /// <summary>
        /// The StockItem class constructor.
        /// </summary>
        /// <param name="id">An integer, the stock item id/param>
        /// <param name="inStock">An integer, the quantity on hand of this stock item</param>
        /// <param name="name">A string, the name of this stock item</param>
        /// <param name="price">A deecimal, the price of this stock item</param>
        public StockItem(int id, int inStock, string name, decimal price)
        {
            _id = id;
            _inStock = inStock;
            _name = name;
            _price = price;
        }

        /// <summary>
        /// Store the stock item id
        /// </summary>
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Store the quantity on hand
        /// </summary>
        public int InStock
        {
            get { return _inStock; }
        }

        /// <summary>
        /// Store the name of this stock item
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Store the price of this stock item
        /// </summary>
        public decimal Price
        {
            get { return _price; }
        }
       
    }
}