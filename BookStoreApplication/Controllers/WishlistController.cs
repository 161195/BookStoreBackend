using BuisnessLayer.Interfaces;
using CommonLayer.WishlistModel;
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
        [HttpGet]
        public IActionResult GetAllWishlist()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                List<WishlistResponse> wishlist = BL.GetAllWishList(UserId);
                if (wishlist == null)
                {
                    return NotFound(new { Success = false, message = "Invalid" });
                }

                return Ok(new { Success = true, message = "Retrived wishlist successfully ", wishlist });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete("{WishListId}")]
        public IActionResult BookDeleteFromWishList(long WishListId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool WishList = BL.DeletetWithWishlistId(WishListId, UserId);
                if (WishList == false)
                {
                    return NotFound(new { Success = false, message = "Invalid WishListId" });
                }

                return Ok(new { Success = true, message = "Book deleted successfully from WishList ", WishList });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
