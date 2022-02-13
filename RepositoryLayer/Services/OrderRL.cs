using CommonLayer.CartModel;
using CommonLayer.OrderModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        public static string connectionString = @"Data Source = (localdb)\ProjectsV13;Initial Catalog = BookStoreDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //creating object of sqlconnection class and creating connection with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public OrderResponse OrderPlaced(long BookId, OrderModel model, long UserId)
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                string query = "select BookId,UserId from Books where BookId=@BookId and UserId=@UserId";
                SqlCommand Validcommand = new SqlCommand(query, sqlConnection1);
                ValidationOfIdForCart Cart = new ValidationOfIdForCart();

                sqlConnection1.Open();
                Validcommand.Parameters.AddWithValue("@BookId", BookId);
                Validcommand.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = Validcommand.ExecuteReader();

                // HasRows Gets a value that indicates whether the SqlDataReader contains one or more rows.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Reads next record in the data reader.
                        Cart.BookId = Convert.ToInt32(reader["BookId"]);
                        Cart.UserId = Convert.ToInt32(reader["UserId"]);
                    }
                    using (sqlConnection)
                    {
                        SqlCommand command = new SqlCommand("SP_OrderPlaced", this.sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@BookId", Cart.BookId);
                        command.Parameters.AddWithValue("@AddressId", model.AddressId);              
                        command.Parameters.AddWithValue("@Quantity", model.Quantity);
                        command.Parameters.AddWithValue("@UserId", Cart.UserId);
                        sqlConnection.Open();
                        int result = command.ExecuteNonQuery();
                        sqlConnection.Close();
                        if (result >= 0)
                        {
                            //OrderResponse newOrder = new OrderResponse();
                            //newOrder.BookId = BookId;
                            //newOrder.AddressId = model.AddressId;
                            ////newOrder.Price = model.Price;
                            //newOrder.Quantity = model.Quantity;
                            //newOrder.UserId = UserId;
                            //return newOrder;
                            return GetOrderWithId(BookId, UserId);

                        }
                    }
                }
                sqlConnection1.Close();
                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public OrderResponse GetOrderWithId(long BookId, long UserId)
        {
            try
            {
                OrderResponse responseModel = new();
                SqlCommand command = new("SP_GetOrderDetailsWithBookId", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                this.sqlConnection.Open();
                command.Parameters.AddWithValue("@BookId", BookId);
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        responseModel.BookId = Convert.ToInt32(reader["BookId"]);
                        responseModel.AddressId = Convert.ToInt32(reader["AddressId"]);
                        responseModel.Price = Convert.ToInt32(reader["Price"] == DBNull.Value ? default : reader["Price"]);
                        responseModel.Quantity = Convert.ToInt32(reader["Quantity"] == DBNull.Value ? default : reader["Quantity"]);
                        responseModel.UserId = Convert.ToInt32(reader["UserId"]);
                    }
                    return responseModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public List<OrderResponse> GetAllOrder(long UserId)
        {
            try
            {
                List<OrderResponse> responseModel = new();
                SqlCommand command = new("SP_GetAllOrderDetails", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                this.sqlConnection.Open();
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter dataAdapter = new(command);
                DataTable dataTable = new();
                dataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    responseModel.Add(new OrderResponse
                    {
                        BookId = Convert.ToInt32(dataRow["BookId"]),
                        AddressId = Convert.ToInt32(dataRow["AddressId"]),
                        Price = Convert.ToInt32(dataRow["Price"] == DBNull.Value ? default : dataRow["Price"]),
                        Quantity = Convert.ToInt32(dataRow["Quantity"] == DBNull.Value ? default : dataRow["Quantity"]),
                        UserId = Convert.ToInt32(dataRow["UserId"]),                  
                    }
                    );
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

    }
}
