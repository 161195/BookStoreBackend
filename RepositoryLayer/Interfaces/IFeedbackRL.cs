using CommonLayer.FeedbackModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRL
    {
        public AddFeedbackResponse AddingFeedback(long BookId, FeedbackModel model, long UserId);
    }
}
