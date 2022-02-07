using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
     public class UserModel
     {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId
        {
            get; set;
        }
        //feeding this attributes into UserTable.
        [Required(ErrorMessage = "Full Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "EmailId is required")]
        [DataType(DataType.Text)]
        [Display(Name = "EmailId")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password contains six character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "MobileNumber is required")]
        [DataType(DataType.Text)]
        [Display(Name = "MobileNumber")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "MobileNumber contains 10 character")]
        public string MobileNumber { get; set; }
     }
}
