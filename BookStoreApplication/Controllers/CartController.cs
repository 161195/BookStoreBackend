using BuisnessLayer.Interfaces;
using CommonLayer.CartModel;
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
    public class CartController : ControllerBase
    {
        ICartBL BL;
        public CartController(ICartBL BL)
        {
            this.BL = BL;
        }
        [HttpPost("{BookId}/Cart")]
        public IActionResult AddBooksToCart(long BookId,CartModel model)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                AddToCartResponse CartDetails = BL.AddToCart(BookId,model,UserId);
                if (CartDetails != null)
                {
                    return this.Ok(new { Success = true, message = "book added to cart Successfully.", CartDetails });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book has not been added to cart." });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException, msg = ex.Message });
            }
        }
      
        [HttpPut("{CartId}")]
        public IActionResult UpdateCart(long CartId, CartModel model)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                CartResponse cartCheck = BL.UpdateCart(CartId, model, UserId);
                if (cartCheck == null)
                {
                    return NotFound(new { Success = false, message = "Invalid cartId to update" });
                }

                return Ok(new { Success = true, message = "Cart Updated Successfully ", cartCheck });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete("{CartId}")]
        public IActionResult BookDelete(long CartId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool book = BL.DeletetWithCartId(CartId, UserId);
                if (book == false)
                {
                    return NotFound(new { Success = false, message = "Invalid CartId" });
                }

                return Ok(new { Success = true, message = "Book deleted successfully from cart ", book });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAllCarts()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                List<CartResponse> cart = BL.GetAllCart(UserId);
                if (cart == null)
                {
                    return NotFound(new { Success = false, message = "Invalid" });
                }

                return Ok(new { Success = true, message = "Retrived Cart successfully ", cart });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


    }
}
