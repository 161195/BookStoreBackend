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

    }
}
