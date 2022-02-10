using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration config;
        public BookRL(IConfiguration config)
        {
            this.config = config;
        }
        public static string connectionString = @"Data Source = (localdb)\ProjectsV13;Initial Catalog = BookStoreDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //creating object of sqlconnection class and creating connection with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);

        /// <summary>
        /// Adds the book.
        /// </summary>
        /// <param name="bookDetails">The book details.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the book details.
        /// </summary>
        /// <param name="BookId">The book identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public BookResponse UpdateBookDetails(long BookId, BookUpdate model, long UserId )
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand command = new("SP_UpdateBook", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookId", BookId);
                    command.Parameters.AddWithValue("@BookName", model.BookName);
                    command.Parameters.AddWithValue("@BookAuthor", model.BookAuthor);
                    command.Parameters.AddWithValue("@OriginalPrice", model.OriginalPrice);
                    command.Parameters.AddWithValue("@DiscountPrice", model.DiscountPrice);
                    command.Parameters.AddWithValue("@BookQuantity", model.BookQuantity);
                    command.Parameters.AddWithValue("@BookDetails", model.BookDetails);
                    command.Parameters.AddWithValue("@UserId", UserId);
                    this.sqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (result >= 0)
                    {
                        //BookResponse response = new()
                        //{
                        //    BookId = BookId,
                        //    BookName = model.BookName,
                        //    BookAuthor = model.BookAuthor,
                        //    OriginalPrice = model.OriginalPrice,
                        //    DiscountPrice = model.DiscountPrice,
                        //    BookQuantity = model.BookQuantity,
                        //    BookDetails = model.BookDetails,
                        //    UserId = UserId
                        //};
                        //return response;
                        return GetWithBookId(BookId, UserId);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        /// <summary>
        /// Gets the book with book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public BookResponse GetBookWithBookId(long bookId, long jwtUserId)
        {
            try
            {
                BookResponse responseModel = new();
                SqlCommand command = new("spGetBookWithBookId", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                this.sqlConnection.Open();
                command.Parameters.AddWithValue("@BookId", bookId);
                command.Parameters.AddWithValue("@UserId", jwtUserId);
                 SqlDataReader reader = command.ExecuteReader();
                // HasRows Gets a value that indicates whether the SqlDataReader contains one or more rows.

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Reads next record in the data reader.
                        responseModel.BookId = Convert.ToInt32(reader["BookId"]);
                        responseModel.BookName = reader["BookName"].ToString();
                        responseModel.BookAuthor = reader["BookAuthor"].ToString();
                        responseModel.TotalRating = Convert.ToInt32(reader["TotalRating"] == DBNull.Value ? default : reader["TotalRating"]);
                        responseModel.NoOfPeopleRated = Convert.ToInt32(reader["NoOfPeopleRated"] == DBNull.Value ? default : reader["NoOfPeopleRated"]);
                        responseModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"] == DBNull.Value ? default : reader["OriginalPrice"]);
                        responseModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"] == DBNull.Value ? default : reader["DiscountPrice"]);
                        responseModel.BookImage = reader["BookImage"].ToString();
                        responseModel.BookQuantity = Convert.ToInt32(reader["BookQuantity"] == DBNull.Value ? default : reader["BookQuantity"]);
                        responseModel.BookDetails = reader["BookDetails"].ToString();
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
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Gets the with book identifier...using this method for specific ID in other methods like Update.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public BookResponse GetWithBookId(long bookId, long jwtUserId)
        {
            try
            {
                BookResponse responseModel = new();
                SqlCommand command = new("spGetBookWithBookId", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                this.sqlConnection.Open();
                command.Parameters.AddWithValue("@BookId", bookId);
                command.Parameters.AddWithValue("@UserId", jwtUserId);
                SqlDataReader reader = command.ExecuteReader();
                // HasRows Gets a value that indicates whether the SqlDataReader contains one or more rows.
                if (reader.HasRows)
                {
                   // Reads next record in the data reader.
                    while (reader.Read())
                    {
                        responseModel.BookId = Convert.ToInt32(reader["BookId"]);
                        responseModel.BookName = reader["BookName"].ToString();
                        responseModel.BookAuthor = reader["BookAuthor"].ToString();
                        responseModel.TotalRating = Convert.ToInt32(reader["TotalRating"] == DBNull.Value ? default : reader["TotalRating"]);
                        responseModel.NoOfPeopleRated = Convert.ToInt32(reader["NoOfPeopleRated"] == DBNull.Value ? default : reader["NoOfPeopleRated"]);
                        responseModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"] == DBNull.Value ? default : reader["OriginalPrice"]);
                        responseModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"] == DBNull.Value ? default : reader["DiscountPrice"]);
                        responseModel.BookImage = reader["BookImage"].ToString();
                        responseModel.BookQuantity = Convert.ToInt32(reader["BookQuantity"] == DBNull.Value ? default : reader["BookQuantity"]);
                        responseModel.BookDetails = reader["BookDetails"].ToString();
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
                sqlConnection.Close();
            }

        }

        /// <summary>
        /// Images the update.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="bookImage">The book image.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public BookResponse ImageUpdate(long bookId, IFormFile bookImage, long jwtUserId)
        {
            try
            {
                Account account = new(this.config["CloudinaryAccount:CloudName"], this.config["CloudinaryAccount:ApiKey"], this.config["CloudinaryAccount:APISecret"]);
                var imagePath = bookImage.OpenReadStream();
                Cloudinary cloudinary = new(account);
                ImageUploadParams imageParams = new()
                {
                    File = new FileDescription(bookImage.FileName, imagePath)
                };
                string uploadImage = cloudinary.Upload(imageParams).Url.ToString();
                using (sqlConnection)
                {
                    SqlCommand command = new("spBookImageUpdate", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookId", bookId);
                    command.Parameters.AddWithValue("@BookImage", uploadImage);
                    command.Parameters.AddWithValue("@UserId", jwtUserId);
                    this.sqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (result >= 0)
                    {
                        return GetWithBookId(bookId, jwtUserId);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

        /// <summary>
        /// Ratingses the update.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="jwtUserId">The JWT user identifier.</param>
        /// <returns></returns>
        public BookResponse RatingsUpdate(long bookId, RatingUpdate model, long jwtUserId)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand command = new("spRatingsUpdate", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookId", bookId);
                    command.Parameters.AddWithValue("@TotalRating", model.TotalRating);
                    command.Parameters.AddWithValue("@NoOfPeopleRated", model.NoOfPeopleRated);
                    command.Parameters.AddWithValue("@UserId", jwtUserId);
                    this.sqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (result >= 0)
                    {
                        return GetWithBookId(bookId, jwtUserId);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all book.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public List<BookResponse> GetAllBook(long UserId)
        {
            try
            {
                List<BookResponse> responseModel = new();
                SqlCommand command = new("SP_GetAllBooks", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                this.sqlConnection.Open();
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter dataAdapter = new(command);
                DataTable dataTable = new();
                dataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    responseModel.Add(new BookResponse
                         {
                             BookId = Convert.ToInt32(dataRow["BookId"]),
                             BookName = Convert.ToString(dataRow["BookName"]),
                             BookAuthor = Convert.ToString(dataRow["BookAuthor"]),
                             TotalRating = Convert.ToInt32(dataRow["TotalRating"] == DBNull.Value ? default : dataRow["TotalRating"]),
                             NoOfPeopleRated = Convert.ToInt32(dataRow["NoOfPeopleRated"] == DBNull.Value ? default : dataRow["NoOfPeopleRated"]),
                             OriginalPrice = Convert.ToInt32(dataRow["OriginalPrice"] == DBNull.Value ? default : dataRow["OriginalPrice"]),
                             DiscountPrice = Convert.ToInt32(dataRow["DiscountPrice"] == DBNull.Value ? default : dataRow["DiscountPrice"]),
                             BookImage = Convert.ToString(dataRow["BookImage"]),
                             BookQuantity = Convert.ToInt32(dataRow["BookQuantity"] == DBNull.Value ? default : dataRow["BookQuantity"]),
                             BookDetails = Convert.ToString(dataRow["BookDetails"]),
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

        /// <summary>
        /// Deletets the with book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public bool DeletetWithBookId(long bookId, long UserId)
        {
            try
            {
                SqlCommand command = new("SP_DeleteBookWithBookId", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookId", bookId);
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
        //SP_GetAllCartDetails

    }
}
