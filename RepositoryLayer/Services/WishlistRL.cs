using CommonLayer.CartModel;
using CommonLayer.WishlistModel;
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
        public List<WishlistResponse> GetAllWishList(long UserId)
        {
            try
            {
                List<WishlistResponse> responseModel = new();
                SqlCommand command = new("SP_GetAllWishList", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                this.sqlConnection.Open();
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter dataAdapter = new(command);
                DataTable dataTable = new();
                dataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    responseModel.Add(
                         new WishlistResponse
                         {
                             WishListId = Convert.ToInt32(dataRow["WishListId"]),
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
        public bool DeletetWithWishlistId(long WishListId, long UserId)
        {
            try
            {
                SqlCommand command = new("SP_DeleteBookFromWishListwithId", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@WishListId", WishListId);
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

    }
}
