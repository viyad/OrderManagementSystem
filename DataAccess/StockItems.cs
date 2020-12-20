using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccess
{
    /// <summary>
    /// Class name - StockItems
    /// To interact with the database, executing commands, retrieving data, storing data
    ///    _connectionString is a local variable used in the class itself
    ///  o GetStockItems() : IEnumerable<StockItem>
    ///  o GetStockItem(int id) : StockItem
    ///  o UpdateStockItemAmount(OrderHeader order)
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class StockItems
    {
        /// <summary>
        /// Store the connectin string.
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// The StockItems class constructor.
        /// To set the connection string.
        /// </summary>
        public StockItems()
        {
            //initialise and assign the correct value for the _connectionString field here using ConfigurationManager. 
            /* Initialise the _connectionString field member in the constructor of the StockItems class. 
                 * This logic is to access the value of the connection string declared in the App.config file 
                 * via the static ConnectionStringSettingsCollection ConnectionStrings property of ConfigurationManager */
            _connectionString = ConfigurationManager.ConnectionStrings["OrderManagementDb"].ConnectionString;
        }

        /// <summary>
        /// To retrieve and return all stock items from the database.
        /// </summary>
        /// <returns>
        /// A list of stock items
        /// </returns>
        public IEnumerable<StockItem> GetStockItems()
        {
            var stockItems = new List<StockItem>();

            /* Retrieve all StockItem records from the database and populating the list 
             * with the retrieved data and return a collecion of StockItem objects */
            try
            {
                // Establish the conection 
                // Use using statement to ensure that the ADO.NET SqlConnection and SqlCommand objects are correctly disposed
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Use stored procedure without any parameter
                    using (SqlCommand cm = new SqlCommand("sp_SelectStockItems", con))
                    {
                        // Set type of the command
                        cm.CommandType = CommandType.StoredProcedure;

                        // Open the connection
                        con.Open();

                        // Execute the SQL stored procedure to get a dataset of people
                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            // Iterate through the dataset and store each row in the StockItem list
                            while (dr.Read())
                            {
                                // Create StockItem object
                                StockItem stockItem = new StockItem(Convert.ToInt32(dr["Id"]), Convert.ToInt32(dr["InStock"]), 
                                                                    dr["Name"].ToString(), Convert.ToDecimal(dr["Price"]));

                                // Add the StockItem object to the StockItem list
                                stockItems.Add(stockItem);
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return stockItems;
        }

        /// <summary>
        /// To retrive and return the stock item of a given stock item id.
        /// </summary>
        /// <returns>
        /// An stock item 
        /// </returns>
        public StockItem GetStockItem(int id)
        {
            StockItem stockItem = null;

            /* Retrieve an StockItem record of the given id from the database and  
             * create an StockItem object and return it */
            try
            {
                // Establish the conection 
                // Use using statement to ensure that the ADO.NET SqlConnection and SqlCommand objects are correctly disposed
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Use stored procedure without any parameter
                    using (SqlCommand cm = new SqlCommand("sp_SelectStockItemById", con))
                    {
                        // Set type of the command
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@id", id);

                        // Open the connection
                        con.Open();

                        // Execute the SQL stored procedure to get a dataset of people
                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            // Check whether the dataset return any row of StockItem records
                            if (dr.HasRows)
                            {
                                // Get the first row
                                dr.Read();

                                // Create StockItem object
                                stockItem = new StockItem(Convert.ToInt32(dr["Id"]), Convert.ToInt32(dr["InStock"]),
                                                                    dr["Name"].ToString(), Convert.ToDecimal(dr["Price"]));
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return stockItem;
        }

        /// <summary>
        /// To update an stock item in the table.
        /// </summary>
        /// <param name="order">OrderHeader, the order header of the order item containing the stock item that will be updated</param>
        public void UpdateStockItemAmount(OrderHeader order)
        {
            try
            {
                // Establish the conection 
                // Use using statement to ensure that the ADO.NET SqlConnection and SqlCommand objects are correctly disposed
                using (SqlConnection con = new SqlConnection(_connectionString))
                { 
                    // Open the connection
                    con.Open();

                    SqlCommand cm = con.CreateCommand();

                    // Start a local transaction
                    SqlTransaction tr = con.BeginTransaction("UpdateStockItemAmount");

                    // Assign transaction and connection objects to Command object for a pending local transaction
                    cm.Connection = con;
                    cm.Transaction = tr;

                    try
                    {
                        cm.CommandText = "sp_UpdateStockItemAmount @id, @amount";
                        // Loop through the OrderHead and get its OrderItem one at a time
                        foreach (OrderItem orderItem in order.OrderItems)
                        {
                            cm.Parameters.Add(new SqlParameter("@id", orderItem.StockItemId));
                            cm.Parameters.Add(new SqlParameter("@amount", -orderItem.Quantity));
                            cm.ExecuteNonQuery();
                            cm.Parameters.Clear();
                        }
                        // Attempt to commit the tranaction
                        tr.Commit();
                    }
                    catch (Exception exp)
                    {
                        // Attempt to roll back the tranaction
                        try
                        {
                            tr.Rollback();
                        }
                        catch (Exception exp1)
                        {
                            throw exp1;
                        }

                        throw exp;
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
