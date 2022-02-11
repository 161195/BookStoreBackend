using BuisnessLayer.Interfaces;
using CommonLayer.FeedbackModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }
        public AddFeedbackResponse AddingFeedback(long BookId, FeedbackModel model, long UserId)
        {
            try
            {
                return this.feedbackRL.AddingFeedback(BookId, model, UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
