using BuisnessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        IBookBL BL;
        public BooksController(IBookBL BL)
        {
            this.BL = BL;
        }
        [HttpPost("Book")]
        public IActionResult CreateBookDetails(InsertBookDetails bookDetails)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                BookResponse BookDetails = BL.AddBook(bookDetails, UserId);
                if (BookDetails != null)
                {
                    return this.Ok(new { Success = true, message = "book added Successfully", BookDetails });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book added Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException, msg = ex.Message });
            }
        }
        [HttpPut("{BookId}")]
        public IActionResult UpdateBookDetails(long BookId, BookUpdate model)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                BookResponse book = BL.UpdateBookDetails(BookId, model, UserId);
                if (book == null)
                {
                    return NotFound(new { Success = false, message = "Invalid BookId to update" });
                }

                return Ok(new { Success = true, message = "Book Updated Successfully ", book });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("{bookId}")]
        public IActionResult GetBookWithBookId(long bookId)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                BookResponse book = BL.GetBookWithBookId(bookId, jwtUserId);
                if (book == null)
                {
                    return NotFound(new { Success = false, message = "Invalid BookId" });
                }

                return Ok(new { Success = true, message = "Retrived Book BooId ", book });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("{bookId}/image")]
        public IActionResult ImageUpdate(long bookId, IFormFile bookImage)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                BookResponse book = BL.ImageUpdate(bookId, bookImage, jwtUserId);
                if (book == null)
                {
                    return NotFound(new { Success = false, message = "Invalid BookId to update image" });
                }

                return Ok(new { Success = true, message = "BookImage Updated Successfully ", book });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("{bookId}/ratings")]
        public IActionResult RatingsUpdate(long bookId, RatingUpdate model)
        {
            try
            {
                long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                BookResponse book = BL.RatingsUpdate(bookId, model, jwtUserId);
                if (book == null)
                {
                    return NotFound(new { Success = false, message = "Invalid BookId to update ratings" });
                }

                return Ok(new { Success = true, message = "Book Ratings Updated Successfully ", book });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                List<BookResponse> book = BL.GetAllBook(UserId);
                if (book == null)
                {
                    return NotFound(new { Success = false, message = "Invalid BookId" });
                }

                return Ok(new { Success = true, message = "Retrived Books successfully ", book });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete]
        public IActionResult BookDelete(long bookId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool book = BL.DeletetWithBookId(bookId,UserId);
                if (book == false)
                {
                    return NotFound(new { Success = false, message = "Invalid BookId" });
                }

                return Ok(new { Success = true, message = "Book deleted successfully ", book });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
