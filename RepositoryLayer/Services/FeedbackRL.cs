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

    }
}
