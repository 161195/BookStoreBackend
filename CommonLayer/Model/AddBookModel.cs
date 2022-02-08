using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer
{
     public class AddBookModel
     {

        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Book name is required.")]
        [DataType(DataType.Text)]
        [Display(Name = "BookName")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Author name is required.")]
        [DataType(DataType.Text)]
        [Display(Name = "BookAuthor")]
        public string BookAuthor { get; set; }

        [Required(ErrorMessage = "Total ratings are required.")]
        [DataType(DataType.Text)]
        [Display(Name = "Total Ratings")]
        public long TotalRating { get; set; }

        [Required(ErrorMessage = "Number of people rated is required.")]
        [DataType(DataType.Text)]
        [Display(Name = "NoOfPeopleRated")]
        public int NoOfPeopleRated { get; set; }

        [Required(ErrorMessage = "Original price is required.")]
        [DataType(DataType.Text)]
        [Display(Name = "Original Price")]
        public int OriginalPrice { get; set; }

        [Required(ErrorMessage = "Discount price is required.")]
        [DataType(DataType.Text)]
        [Display(Name = "DiscountPrice")]
        public long DiscountPrice { get; set; }

        [Required(ErrorMessage = "Book image is required.")]
        [DataType(DataType.Text)]
        [Display(Name = "BookImage")]
        public string BookImage { get; set; }

        [Required(ErrorMessage = "Book quantity is required.")]
        [DataType(DataType.Text)]
        [Display(Name = "BookQuantity")]
        public int BookQuantity { get; set; }

        [Required(ErrorMessage = "Book details are required.")]
        [DataType(DataType.Text)]
        [Display(Name = "BookDetails")]
        public string BookDetails { get; set; }
        public long UserId { get; set; }

    }
}
