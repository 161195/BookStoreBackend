using BuisnessLayer.Interfaces;
using CommonLayer.OrderModel;
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
    public class OrderController : ControllerBase
    {
        IOrderBL BL;
        public OrderController(IOrderBL BL)
        {
            this.BL = BL;
        }
        [HttpPost("{BookId}")]
        public IActionResult OrderPlacing(long BookId, OrderModel model)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                OrderResponse Order = BL.OrderPlaced(BookId, model, UserId);
                if (Order == null)
                {
                    return NotFound(new { Success = false, message = "Invalid BookId, can not placed order" });
                }

                return Ok(new { Success = true, message = "Order Placed successfully ", Order });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                List<OrderResponse> order = BL.GetAllOrder(UserId);
                if (order == null)
                {
                    return NotFound(new { Success = false, message = "Invalid" });
                }

                return Ok(new { Success = true, message = "Retrived orders successfully ", order });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


    }
}
