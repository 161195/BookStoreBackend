using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface IWishlistBL
    {
        public bool AddToWishlist(long BookId, long UserId);

    }
}
