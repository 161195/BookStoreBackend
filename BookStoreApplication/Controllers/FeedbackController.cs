using BuisnessLayer.Interfaces;
using CommonLayer.FeedbackModel;
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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL BL;
        public FeedbackController(IFeedbackBL BL)
        {
            this.BL = BL;
        }
        [HttpPost("{BookId}/FeedBack")]
        public IActionResult AddBooksToCart(long BookId,FeedbackModel model)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                AddFeedbackResponse Feedback = BL.AddingFeedback(BookId, model, UserId);
                if (Feedback != null)
                {
                    return this.Ok(new { Success = true, message = "Feedback added Successfully.", Feedback });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Feedback has not been added on book." });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException, msg = ex.Message });
            }
        }

        [HttpGet("{BookId}")]
        public IActionResult GettingAllFeedbacks(long BookId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                List<GetBackAllFeedback> Feedback = BL.GetAllWishList(BookId,UserId);
                if (Feedback != null)
                {
                    return this.Ok(new { Success = true, message = "Feedback fetched Successfully.", Feedback });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "invalid." });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException, msg = ex.Message });
            }
        }
    }
}
