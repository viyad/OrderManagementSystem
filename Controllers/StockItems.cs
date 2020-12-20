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
    /// Class name - StockItems
    /// A static class that can be used conveniently without instantiating an object
    /// The general information includes:
    ///    _stockItem is a local variable used in the class itself
    ///    o   GetStockItems() : IEnumerable<StockItem>
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class StockItems
    {
        /// <summary>
        /// To establish the data access to the StockItem table
        /// </summary>
        private static DataAccess.StockItems _stockItem = new DataAccess.StockItems();

        /// <summary>
        /// To get and return all stock items.
        /// </summary>
        /// <returns>
        /// A list of stock items.
        /// </returns>
        public static List<StockItem> GetStockItems()
        {
            List<StockItem> stockItems = (List<StockItem>) _stockItem.GetStockItems();

            return stockItems;
        }
    }
}
