using BuisnessLayer.Interfaces;
using CommonLayer.AddressModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressBL BL;
        public AddressController(IAddressBL BL)
        {
            this.BL = BL;
        }
        [HttpPost("{TypeId}")]
        public IActionResult AddressAdd(long TypeId,AddressModel model)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                AddressResponse Address = BL.AddressAdding(TypeId, model, UserId);
                if (Address == null)
                {
                    return NotFound(new { Success = false, message = "Invalid TypeId" });
                }

                return Ok(new { Success = true, message = "Address added successfully ", Address });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet()]
        public IActionResult GettingAddressOfUser()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                List<AddressResponse> address = BL.GetAddress(UserId);
                if (address != null)
                {
                    return this.Ok(new { Success = true, message = "Address fetched Successfully.", address });
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

        [HttpPut("{AddressId}")]
        public IActionResult AddressEditing(long AddressId, UpdateModel model)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                AddressUpdateResponse Address = BL.AddressEdit(AddressId, model, UserId);
                if (Address == null)
                {
                    return NotFound(new { Success = false, message = "Invalid AddressId" });
                }

                return Ok(new { Success = true, message = "Address Updated successfully ", Address });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
