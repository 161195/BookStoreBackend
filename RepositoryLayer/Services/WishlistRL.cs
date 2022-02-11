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
     public class WishlistRL :IWishlistRL
     {
        public static string connectionString = @"Data Source = (localdb)\ProjectsV13;Initial Catalog = BookStoreDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //creating object of sqlconnection class and creating connection with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);

        public bool AddToWishlist (long BookId,long UserId)
        {
            try
            {
                SqlConnection sqlConnection1 = new(connectionString);
                string query = "select BookId,UserId from Books where BookId=@BookId and UserId=@UserId ";
                SqlCommand validateCommand = new(query, sqlConnection1);
                ValidationOfIdForCart validationModel = new();

                sqlConnection1.Open();
                validateCommand.Parameters.AddWithValue("@BookId", BookId);
                validateCommand.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = validateCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        validationModel.BookId = Convert.ToInt32(reader["BookId"]);
                        validationModel.UserId = Convert.ToInt32(reader["UserId"]);
                    }
                    using (sqlConnection)
                    {
                        SqlCommand command = new("SP_AddToWishList", sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookId", BookId);
                        command.Parameters.AddWithValue("@UserId", UserId);

                        this.sqlConnection.Open();
                        int result = command.ExecuteNonQuery();
                        this.sqlConnection.Close();

                        if (result >= 0)
                        {
                            return true;
                        }
                    }
                }
                sqlConnection1.Close();
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

     }
}
