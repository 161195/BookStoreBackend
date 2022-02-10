using CommonLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        public AddToCartResponse AddToCart(long BookId, CartModel model, long UserId);
        public CartResponse UpdateCart(long CartId, CartModel model, long UserId);

    }
}
