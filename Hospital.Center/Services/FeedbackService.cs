using Hospital.Domain.Models;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IPatientRepository patientRepository;

        public FeedbackService(IFeedbackRepository fdbkRepo, IPatientRepository patRepo)
        {
            feedbackRepository = fdbkRepo;
            patientRepository = patRepo;
        }

        public List<Feedback> GetAll()
        {
            var allFeedback = feedbackRepository.FindAll();
            foreach (Feedback feedback in allFeedback)
                BindFeedbackProperties(feedback);
            return allFeedback;
        }

        public List<Feedback> GetPublished()
        {
            var publishedFeedback = feedbackRepository.GetPublished();
            foreach (Feedback feedback in publishedFeedback)
                BindFeedbackProperties(feedback);
            return publishedFeedback;
        }

        public bool UpdatePublished(int id)
        {
            var feedback = feedbackRepository.GetById(id);

            if (feedback != null)
            {
                feedback.Published = !feedback.Published;
                var done = feedbackRepository.Update(feedback);

                if (done != null)
                    return true;

            }

            return false;
        }
        public bool LeaveFeedback(Feedback feedback)
        {
            var created = feedbackRepository.Create(feedback);

            if (created != null)
                return true;

            return false;
        }

        private void BindFeedbackProperties(Feedback feedback)
        {
            feedback.Patient = patientRepository.GetById(feedback.PatientId);
        }

    }
}
