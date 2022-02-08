using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class InsertBookDetails
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public int OriginalPrice { get; set; }
        public long DiscountPrice { get; set; }
        public int BookQuantity { get; set; }
        public string BookDetails { get; set; }
    

    }
}
