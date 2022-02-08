using CommonLayer;
using CommonLayer.Model;
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
    public class BookRL : IBookRL
    {
        public static string connectionString = @"Data Source = (localdb)\ProjectsV13;Initial Catalog = BookStoreDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //creating object of sqlconnection class and creating connection with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public BookResponse AddBook(InsertBookDetails bookDetails, long UserId)
        {
            try
            {
                using (sqlConnection)
                {
   
                    SqlCommand command = new SqlCommand("spCreateBook", this.sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookName", bookDetails.BookName);
                    command.Parameters.AddWithValue("@BookAuthor", bookDetails.BookAuthor);
                    command.Parameters.AddWithValue("@OriginalPrice", bookDetails.OriginalPrice);
                    command.Parameters.AddWithValue("@DiscountPrice", bookDetails.DiscountPrice);
                    command.Parameters.AddWithValue("@BookQuantity", bookDetails.BookQuantity);
                    command.Parameters.AddWithValue("@BookDetails", bookDetails.BookDetails);
                    command.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result >= 0)
                    {
                        BookResponse newUser = new BookResponse();                     
                        newUser.BookName = bookDetails.BookName;
                        newUser.BookAuthor = bookDetails.BookAuthor;
                        newUser.OriginalPrice = bookDetails.OriginalPrice;
                        newUser.DiscountPrice = bookDetails.DiscountPrice;
                        newUser.BookQuantity = bookDetails.BookQuantity;
                        newUser.BookDetails = bookDetails.BookDetails;
                        newUser.UserId = UserId;
                        return newUser;
                    }
                  
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }


        }
    }
}
