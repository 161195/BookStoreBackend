using BuisnessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        IWishlistBL BL;
        public WishlistController(IWishlistBL BL)
        {
            this.BL = BL;
        }

        [HttpPost("{BookId}")]
        public IActionResult AddBooksToWishlist(long BookId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool Wishlist = BL.AddToWishlist(BookId, UserId);
                if (Wishlist == false)
                {
                    return NotFound(new { Success = false, message = "Invalid BookId" });
                }

                return Ok(new { Success = true, message = "Book Added to Wishlist successfully ", Wishlist });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
