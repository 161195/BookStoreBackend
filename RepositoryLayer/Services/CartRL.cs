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



    }

}
