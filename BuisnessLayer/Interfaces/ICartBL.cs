using CommonLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface ICartBL
    {
        public AddToCartResponse AddToCart(long BookId, CartModel model, long UserId);
        public CartResponse UpdateCart(long CartId, CartModel model, long UserId);
        public bool DeletetWithCartId(long CartId, long UserId);
        public List<CartResponse> GetAllCart(long UserId);


    }
}
