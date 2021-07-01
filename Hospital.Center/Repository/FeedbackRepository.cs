using Hospital.Domain.Models;
using Hospital.Center.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository
{
    public class FeedbackRepository : RepositoryBase<Feedback, int>, IFeedbackRepository
    {
        public FeedbackRepository(HospitalDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public Feedback GetById(int id)
        {
            return FindByCondition(d => d.Id == id).FirstOrDefault();
        }

        public List<Feedback> GetPublished()
        {
            return FindByCondition(f => f.Published);
        }
    }
}
