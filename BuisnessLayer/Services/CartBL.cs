using BuisnessLayer.Interfaces;
using CommonLayer.CartModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class CartBL :ICartBL
    {
        ICartRL CartRL;
        public CartBL(ICartRL cartRL)
        {
            this.CartRL = cartRL;
        }
        public AddToCartResponse AddToCart(long BookId, CartModel model, long UserId)
        {
            try
            {
                return this.CartRL.AddToCart(BookId,model,UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public CartResponse UpdateCart(long CartId, CartModel model, long UserId)
        {
            try
            {
                return this.CartRL.UpdateCart(CartId, model, UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public bool DeletetWithCartId(long CartId, long UserId)
        {
            try
            {
                return this.CartRL.DeletetWithCartId(CartId,UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public List<CartResponse> GetAllCart(long UserId)
        {
            try
            {
                return this.CartRL.GetAllCart(UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}
