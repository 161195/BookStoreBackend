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

    }
}
