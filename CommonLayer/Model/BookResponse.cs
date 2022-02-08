using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
     public class BookResponse
     {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public long TotalRating { get; set; }
        public int NoOfPeopleRated { get; set; }
        public int OriginalPrice { get; set; }
        public long DiscountPrice { get; set; }
        public string BookImage { get; set; }
        public int BookQuantity { get; set; }
        public string BookDetails { get; set; }
        public long UserId { get; set; }
    }
}
