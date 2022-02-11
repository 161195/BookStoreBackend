using CommonLayer.FeedbackModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface IFeedbackBL
    {
        public AddFeedbackResponse AddingFeedback(long BookId, FeedbackModel model, long UserId);
    }
}
