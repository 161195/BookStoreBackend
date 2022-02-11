using CommonLayer.CartModel;
using CommonLayer.FeedbackModel;
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
   public class FeedbackRL : IFeedbackRL
   {
        public static string connectionString = @"Data Source = (localdb)\ProjectsV13;Initial Catalog = BookStoreDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //creating object of sqlconnection class and creating connection with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
         
        public AddFeedbackResponse AddingFeedback(long BookId, FeedbackModel model, long UserId)
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

                        SqlCommand command = new SqlCommand("SP_AddFeedback", this.sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookId", BookId);
                        command.Parameters.AddWithValue("@FeedBack", model.FeedBack);
                        command.Parameters.AddWithValue("@Ratings", model.Ratings);
                        command.Parameters.AddWithValue("@UserId", UserId);
                        sqlConnection.Open();
                        int result = command.ExecuteNonQuery();
                        sqlConnection.Close();
                        if (result >= 0)
                        {
                            AddFeedbackResponse newFeedback = new AddFeedbackResponse();
                            newFeedback.BookId = BookId;
                            newFeedback.FeedBack = model.FeedBack;
                            newFeedback.Ratings = model.Ratings;
                            newFeedback.UserId = UserId;
                            return newFeedback;
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
        public List<GetBackAllFeedback> GetAllWishList(long BookId, long UserId)
        {
            try
            {
                SqlConnection sqlConnection1 = new(connectionString);
                string query = "select BookId,UserId from FeedBackTable where BookId=@BookId and UserId=@UserId ";
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
                    List<GetBackAllFeedback> responseModel = new();
                    SqlCommand command = new("SP_GetAllFeedback", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    this.sqlConnection.Open();
                    command.Parameters.AddWithValue("@BookId", BookId);
                    command.Parameters.AddWithValue("@UserId", UserId);
                    SqlDataAdapter dataAdapter = new(command);
                    DataTable dataTable = new();
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        responseModel.Add(
                             new GetBackAllFeedback
                             {
                                 FeedBackId = Convert.ToInt32(dataRow["FeedBackId"]),
                                 BookId = Convert.ToInt32(dataRow["BookId"]),
                                 UserId = Convert.ToInt32(dataRow["UserId"]),
                                 FeedBack = dataRow["FeedBack"].ToString(),
                                 Ratings = Convert.ToInt32(dataRow["Ratings"] == DBNull.Value ? default : dataRow["Ratings"]),
                             }
                         );
                    }
                    return responseModel;
                }
                sqlConnection1.Close();
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
    }

}

