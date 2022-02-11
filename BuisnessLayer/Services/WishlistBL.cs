using BuisnessLayer.Interfaces;
using CommonLayer.WishlistModel;
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
        public List<WishlistResponse> GetAllWishList(long UserId)
        {
            try
            {
                return this.WishlistRL.GetAllWishList(UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public bool DeletetWithWishlistId(long WishListId, long UserId)
        {
            try
            {
                return this.WishlistRL.DeletetWithWishlistId(WishListId,UserId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}
