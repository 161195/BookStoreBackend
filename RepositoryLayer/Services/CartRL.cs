using CommonLayer.CartModel;
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
    public class CartRL :ICartRL
    {
        public static string connectionString = @"Data Source = (localdb)\ProjectsV13;Initial Catalog = BookStoreDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //creating object of sqlconnection class and creating connection with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public AddToCartResponse AddToCart(long BookId,CartModel model,long UserId)
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                string query = "select BookId,UserId from Books where BookId=@BookId and UserId=@UserId";
                SqlCommand Validcommand = new SqlCommand(query,sqlConnection1);
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

                        SqlCommand command = new SqlCommand("SP_AddToCart", this.sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookId", BookId);
                        command.Parameters.AddWithValue("@Quantity", model.Quantity);                     
                        command.Parameters.AddWithValue("@UserId", UserId); 
                        sqlConnection.Open();
                        int result = command.ExecuteNonQuery();
                        sqlConnection.Close();
                        if (result >= 0)
                        {
                            AddToCartResponse newCart = new AddToCartResponse();                           
                            newCart.BookId = BookId;
                            newCart.Quantity = model.Quantity;
                            newCart.UserId = UserId;
                            return newCart;
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
        public CartResponse GetCartWithId(long CartId, long UserId)
        {
            try
            {
                CartResponse responseModel = new();
                SqlCommand command = new("SP_GetCartDetailsWithCartId", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                this.sqlConnection.Open();
                command.Parameters.AddWithValue("@CartId", CartId);
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        responseModel.CartId = Convert.ToInt32(reader["CartId"]);
                        responseModel.Quantity = Convert.ToInt32(reader["Quantity"] == DBNull.Value ? default : reader["Quantity"]);
                        responseModel.BookId = Convert.ToInt32(reader["BookId"]);                
                        responseModel.UserId = Convert.ToInt32(reader["UserId"]);
                        responseModel.BookName = reader["BookName"].ToString();
                        responseModel.BookAuthor = reader["BookAuthor"].ToString();                      
                        responseModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"] == DBNull.Value ? default : reader["OriginalPrice"]);
                        responseModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"] == DBNull.Value ? default : reader["DiscountPrice"]);
                        responseModel.BookImage = reader["BookImage"].ToString();                     
                        responseModel.BookDetails = reader["BookDetails"].ToString();
                
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
        public CartResponse UpdateCart(long CartId, CartModel model, long UserId)
        {
            try
            {
                SqlConnection sqlConnection1 = new(connectionString);
                string query = "select UserId from UserTable where UserId=@UserId ";
                SqlCommand validateCommand = new(query, sqlConnection1);
                ValidationOfIdForCart validationModel = new();

                sqlConnection1.Open();
                validateCommand.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = validateCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        validationModel.UserId = Convert.ToInt32(reader["UserId"]);
                    }
                    using (sqlConnection)
                    {
                        SqlCommand command = new("SP_UpdateCart", sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CartId", CartId);
                        command.Parameters.AddWithValue("@Quantity", model.Quantity);
                        command.Parameters.AddWithValue("@UserId", UserId);
                        this.sqlConnection.Open();
                        int result = command.ExecuteNonQuery();
                        this.sqlConnection.Close();
                        if (result >= 0)
                        {
                            UpdateCartResponse response = new()
                            {
                                BookId = validationModel.BookId,
                                Quantity = model.Quantity,
                                UserId = validationModel.UserId
                            };
                            return GetCartWithId(CartId,UserId);
                        }
                    }
                }
                sqlConnection1.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool DeletetWithCartId(long CartId, long UserId)
        {
            try
            {
                SqlCommand command = new("SP_DeleteBookFromCartwithCartId", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CartId", CartId);
                command.Parameters.AddWithValue("@UserId", UserId);

                this.sqlConnection.Open();
                int result = command.ExecuteNonQuery();
                if (result >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        public List<CartResponse> GetAllCart(long UserId)
        {
            try
            {
                List<CartResponse> responseModel = new();
                SqlCommand command = new("SP_GetAllCartDetails", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                this.sqlConnection.Open();
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter dataAdapter = new(command);
                DataTable dataTable = new();
                dataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    responseModel.Add(new CartResponse
                    {
                             CartId = Convert.ToInt32(dataRow["CartId"]),
                             Quantity = Convert.ToInt32(dataRow["Quantity"] == DBNull.Value ? default : dataRow["Quantity"]),
                             BookId = Convert.ToInt32(dataRow["BookId"]),
                             UserId = Convert.ToInt32(dataRow["UserId"]),
                             BookName = dataRow["BookName"].ToString(),
                             BookAuthor = dataRow["BookAuthor"].ToString(),
                             OriginalPrice = Convert.ToInt32(dataRow["OriginalPrice"] == DBNull.Value ? default : dataRow["OriginalPrice"]),
                             DiscountPrice = Convert.ToInt32(dataRow["DiscountPrice"] == DBNull.Value ? default : dataRow["DiscountPrice"]),
                             BookImage = dataRow["BookImage"].ToString(),
                             BookDetails = dataRow["BookDetails"].ToString(),
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




