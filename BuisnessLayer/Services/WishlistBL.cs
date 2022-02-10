using BuisnessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class WishlistBL: IWishlistBL
    {
        IWishlistRL WishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.WishlistRL = wishlistRL;
        }
    }
}
