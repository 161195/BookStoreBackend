using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.WishlistModel
{
    public class WishlistResponse
    {
        public long WishListId { get; set; }     
        public long BookId { get; set; }  
        public long UserId { get; set; }
        public string BookName { get; set; } 
        public string BookAuthor { get; set; }     
        public long OriginalPrice { get; set; }    
        public long DiscountPrice { get; set; }
        public string BookImage { get; set; }
        public string BookDetails { get; set; }
    } 
}
