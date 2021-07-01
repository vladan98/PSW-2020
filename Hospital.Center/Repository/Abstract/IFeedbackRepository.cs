using Hospital.Domain.Models;
using Hospital.Domain.Models.Users;
using Hospital.Center.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository.Abstract
{
    public interface IFeedbackRepository : IRepositoryBase<Feedback, int>
    {
        Feedback GetById(int id);
        List<Feedback> GetPublished();
    }

}
