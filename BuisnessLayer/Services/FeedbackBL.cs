using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class FeedbackBL :IFeedbackRL
    {
        IFeedbackRL FeedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.FeedbackRL = feedbackRL;
        }
    }
}
