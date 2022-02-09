using BuisnessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class BookBL :IBookBL
    {
        IBookRL BookRL;
        public BookBL(IBookRL bookRL)
        {
            this.BookRL = bookRL;
        }
        public BookResponse AddBook(InsertBookDetails bookDetails, long UserId)
        {
            try
            {
                return this.BookRL.AddBook(bookDetails, UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public BookResponse UpdateBookDetails(long BookId, BookUpdate model, long UserId)
        {
            try
            {
                return this.BookRL.UpdateBookDetails(BookId,model,UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public BookResponse GetBookWithBookId(long bookId, long jwtUserId)
        {
            try
            {
                return this.BookRL.GetBookWithBookId(bookId, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public BookResponse ImageUpdate(long bookId, IFormFile bookImage, long jwtUserId)
        {
            try
            {
                return this.BookRL.ImageUpdate(bookId, bookImage, jwtUserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
