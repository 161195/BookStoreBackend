using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.FeedbackModel
{
    public class GetBackAllFeedback
    {
        public long FeedBackId { get; set; }      
        public long BookId { get; set; }
        public long UserId { get; set; }
        public string FeedBack { get; set; }       
        public long Ratings { get; set; }
        
    }
}
