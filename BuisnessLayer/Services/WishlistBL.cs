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
        public bool AddToWishlist(long BookId, long UserId)
        {
            try
            {
                return this.WishlistRL.AddToWishlist(BookId,UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
