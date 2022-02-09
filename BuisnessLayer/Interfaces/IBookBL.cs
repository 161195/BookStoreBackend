using CommonLayer;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface IBookBL
    {
        public BookResponse AddBook(InsertBookDetails bookDetails, long UserId);
        public BookResponse UpdateBookDetails(long BookId,BookUpdate model, long UserId );
        public BookResponse GetBookWithBookId(long bookId, long jwtUserId);
        public BookResponse ImageUpdate(long bookId, IFormFile bookImage, long jwtUserId);
         public BookResponse RatingsUpdate(long bookId, RatingUpdate model, long jwtUserId);
        public List<BookResponse> GetAllBook(long UserId);
        public bool DeletetWithBookId(long bookId, long UserId);
    }
}
