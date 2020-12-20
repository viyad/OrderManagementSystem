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
    /// Class name - Orders
    /// To interact with the database, executing commands, retrieving data, storing data
    ///    _connectionString is a local variable used in the class itself
    ///   o	InsertOrderHeader() : OrderHeader
    ///   o GetOrderHeader(int id) : OrderHeader
    ///   o GetOrderHeaders() : IEnumerable<OrderHeader>
    ///   o UpsertOrderItem(OrderItem orderItem)
    ///   o UpdateOrderState(OrderHeader order)
    ///   o DeleteOrderHeaderAndOrderItems(int orderHeaderId)
    ///   o DeleteOrderItem(int orderHeaderId, int stockItemId)
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class Orders
    {
        /// <summary>
        /// Store the connectin string.
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// The Orders class constructor.
        /// To set the connection string.
        /// </summary>
        public Orders()
        {
            //initialise and assign the correct value for the _connectionString field here using ConfigurationManager 
            /* Initialise the _connectionString field member in the constructor of the Orders class. 
                 * This logic is to access the value of the connection string declared in the App.config file 
                 * via the static ConnectionStringSettingsCollection ConnectionStrings property of ConfigurationManager */
            _connectionString = ConfigurationManager.ConnectionStrings["OrderManagementDb"].ConnectionString;
        }

        /// <summary>
        /// To retrieve and return all order headers from the database.
        /// </summary>
        /// <returns>
        /// A list of order headers
        /// </returns>
        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            var orderHeaders = new List<OrderHeader>();
            int prevId = 0, currId = 0;

            OrderHeader orderHeader = null;

            /* Retrieve all OrderHeader records from the database and populating the list 
             * with the retrieved data and return a collecion of OrderHeader objects */
            try
            {
                // Establish the conection 
                // Use using statement to ensure that the ADO.NET SqlConnection and SqlCommand objects are correctly disposed
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Use stored procedure without any parameter
                    using (SqlCommand cm = new SqlCommand("sp_SelectOrderHeaders", con))
                    {
                        // Set type of the command
                        cm.CommandType = CommandType.StoredProcedure;

                        // Open the connection
                        con.Open();

                        // Execute the SQL stored procedure to get a dataset of people
                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            // Check whether the dataset return any row of OrderHeader records
                            if (dr.HasRows)
                            {
                                // Iterate through the dataset and store each row in the OrderHeader list
                                while (dr.Read())
                                {
                                    currId = Convert.ToInt32(dr["Id"]);
                                    if (prevId != currId)
                                        // Create OrderHeader object
                                        orderHeader = new OrderHeader(Convert.ToInt32(dr["Id"]),
                                                                                Convert.ToDateTime(dr["DateTime"]),
                                                                                Convert.ToInt32(dr["OrderStateId"]));

                                    if (!dr.IsDBNull(3))
                                    {
                                        // Create OrderItem of the current OrderHeader
                                        orderHeader.AddOrderItem(Convert.ToInt32(dr["StockItemId"]),
                                                            Convert.ToDecimal(dr["Price"]), Convert.ToInt32(dr["Quantity"]),
                                                            dr["Description"].ToString());
                                        
                                    }

                                    if (prevId != currId)
                                        // Add the OrderHeader object to the OrderHeaders list
                                        orderHeaders.Add(orderHeader);

                                    prevId = currId;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return orderHeaders;
        }

        /// <summary>
        /// To retrive and return the order header of the given header id.
        /// </summary>
        /// <returns>
        /// An order header
        /// </returns>
        public OrderHeader GetOrderHeader(int id)
        {
            OrderHeader orderHeader = null;

            /* Retrieve an OrderHeader record of the given id from the database and  
             * create an OrderHearder object and return it */
            try
            {
                // Establish the conection 
                // Use using statement to ensure that the ADO.NET SqlConnection and SqlCommand objects are correctly disposed
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Use stored procedure without any parameter
                    using (SqlCommand cm = new SqlCommand("sp_SelectOrderHeaderById", con))
                    {
                        // Set type of the command
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@id", id);

                        // Open the connection
                        con.Open();

                        // Execute the SQL stored procedure to get a dataset of people
                        using (SqlDataReader dr = cm.ExecuteReader())
                        {
                            // Check whether the dataset return any row of OrderHeader records
                            if (dr.HasRows)
                            {
                                // Iterate through the dataset and store each row in the OrderHeader list
                                while (dr.Read())
                                {
                                    if (orderHeader == null)
                                        // Create OrderHeader object
                                        orderHeader = new OrderHeader(Convert.ToInt32(dr["Id"]),
                                                                            Convert.ToDateTime(dr["DateTime"]),
                                                                            Convert.ToInt32(dr["OrderStateId"]));

                                    if (!dr.IsDBNull(3))
                                        // Create OrderItem of the current OrderHeader
                                        orderHeader.AddOrderItem(Convert.ToInt32(dr["StockItemId"]),
                                                            Convert.ToDecimal(dr["Price"]), Convert.ToInt32(dr["Quantity"]),
                                                            dr["Description"].ToString());
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return orderHeader;
        }

        /// <summary>
        /// To insert an new order header into the table.
        /// </summary>
        /// <returns>
        /// An header that has been newly inserted into the table.
        /// </returns>
        public OrderHeader InsertOrderHeader()
        {
            OrderHeader orderHeader = null;
            int id = 0; 
            // Insert an OrderHeader record in the database and return the Id of the new OrderHeader record in the database
            try
            {
                // Establish the conection 
                // Use using statement to ensure that the ADO.NET SqlConnection and SqlCommand objects are correctly disposed
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Use stored procedure without any parameter
                    using (SqlCommand cm = new SqlCommand("sp_InsertOrderHeader", con))
                    {
                        // Set type of the command
                        cm.CommandType = CommandType.StoredProcedure;

                        // Open the connection
                        con.Open();

                        // Return the Id number of the new OrderHeader created in the database
                        id = Convert.ToInt32(cm.ExecuteScalar());

                        // Get OrderHeader by Id
                        orderHeader = GetOrderHeader(id);
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return orderHeader;
        }

        /// <summary>
        /// To update an order item in the table.
        /// </summary>
        /// <param name="orderItem">OrderItem, the order item that will be updated to the table</param>
        public void UpsertOrderItem(OrderItem orderItem)
        {
            try
            {
                // Establish the conection 
                // Use using statement to ensure that the ADO.NET SqlConnection and SqlCommand objects are correctly disposed
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Use stored procedure with parameters
                    using (SqlCommand cm = new SqlCommand("sp_UpsertOrderItem", con))
                    {
                        // Set type of the command
                        cm.CommandType = CommandType.StoredProcedure;

                        // Parameters passing to the stored procedure
                        cm.Parameters.AddWithValue("@orderHeaderId", orderItem.OrderHeaderId);
                        cm.Parameters.AddWithValue("@stockItemId", orderItem.StockItemId);
                        cm.Parameters.AddWithValue("@description", orderItem.Description);
                        cm.Parameters.AddWithValue("@price", orderItem.Price);
                        cm.Parameters.AddWithValue("@quantity", orderItem.Quantity);

                        // Open the connection
                        con.Open();

                        // Execute the SQL stored procedure
                        cm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

        /// <summary>
        /// To update the state of the order
        /// </summary>
        /// <param name="order">OrderHeader, the order header that its stage will be updated in the table</param>
        public void UpdateOrderState(OrderHeader order)
        {
            try
            {
                // Establish the conection 
                // Use using statement to ensure that the ADO.NET SqlConnection and SqlCommand objects are correctly disposed
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Use stored procedure with parameters
                    using (SqlCommand cm = new SqlCommand("sp_UpdateOrderState", con))
                    {
                        // Set type of the command
                        cm.CommandType = CommandType.StoredProcedure;

                        // Parameters passing to the stored procedure
                        cm.Parameters.AddWithValue("@orderHeaderId", order.Id);
                        cm.Parameters.AddWithValue("@stateId", order.State.State);

                        // Open the connection
                        con.Open();

                        // Execute the SQL stored procedure
                        cm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// To delete the order header and its relavant items by a given order header id
        /// </summary>
        /// <param name="orderHeaderId">int, the id of the order header to be deleted</param>
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            try
            {
                // Establish the conection 
                // Use using statement to ensure that the ADO.NET SqlConnection and SqlCommand objects are correctly disposed
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Use stored procedure with parameters
                    using (SqlCommand cm = new SqlCommand("sp_DeleteOrderHeaderAndOrderItems", con))
                    {
                        // Set type of the command
                        cm.CommandType = CommandType.StoredProcedure;

                        // Parameters passing to the stored procedure
                        cm.Parameters.AddWithValue("@orderHeaderId", orderHeaderId);

                        // Open the connection
                        con.Open();

                        // Execute the SQL stored procedure
                        cm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// To delete an order item by a given order header id and a given stock item id
        /// </summary>
        /// <param name="orderHeaderId">int, the id of the order header of that order item to be deleted</param
        /// <param name="stockItemId">int, the id of the stock item of that order item to be deleted</param>
        public void DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
            try
            {
                // Establish the conection 
                // Use using statement to ensure that the ADO.NET SqlConnection and SqlCommand objects are correctly disposed
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Use stored procedure with parameters
                    using (SqlCommand cm = new SqlCommand("sp_DeleteOrderItem", con))
                    {
                        // Set type of the command
                        cm.CommandType = CommandType.StoredProcedure;

                        // Parameters passing to the stored procedure
                        cm.Parameters.AddWithValue("@orderHeaderId", orderHeaderId);
                        cm.Parameters.AddWithValue("@stockItemId", stockItemId);

                        // Open the connection
                        con.Open();

                        // Execute the SQL stored procedure
                        cm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #region ForTesting

        public void ResetDatabase()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cm = new SqlCommand("DELETE FROM OrderItems", con);
                    cm.ExecuteNonQuery();
                    cm = new SqlCommand("DELETE FROM OrderHeaders; DBCC CHECKIDENT ('OrderHeaders', RESEED, 0); ", con);
                    cm.ExecuteNonQuery();
                    cm = new SqlCommand("UPDATE StockItems SET InStock = 10", con);
                    cm.ExecuteNonQuery();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion
    }
}
