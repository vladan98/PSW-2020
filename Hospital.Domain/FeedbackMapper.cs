using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain
{
    public static class FeedbackMapper
    {
        public static Feedback LeaveFeedbackDTOToFeedback(LeaveFeedbackDTO dto)
        {
            return new Feedback()
            {
                PatientId = dto.PatientId,
                Title = dto.Title,
                Content = dto.Content,
                Published = false,
                CreatedAt = DateTime.Now,

            };
        }
    }
}
