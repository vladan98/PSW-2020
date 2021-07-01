using Hospital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Services.Abstract
{
    public interface IFeedbackService
    {
        public List<Feedback> GetAll();
        public List<Feedback> GetPublished();
        public bool UpdatePublished(int id);
        public bool LeaveFeedback(Feedback feedback);
    }
}
