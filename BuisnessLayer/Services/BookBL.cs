using BuisnessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Model;
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
    }
}
