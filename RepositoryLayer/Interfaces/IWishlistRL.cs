using CommonLayer.WishlistModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IWishlistRL
    {
        public bool AddToWishlist(long BookId, long UserId);
        public List<WishlistResponse> GetAllWishList(long UserId);
        public bool DeletetWithWishlistId(long WishListId, long UserId);
    }
}
