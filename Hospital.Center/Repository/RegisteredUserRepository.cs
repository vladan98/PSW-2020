using Hospital.Domain.Models.Users;
using Hospital.Center.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository
{
    public class RegisteredUserRepository : RepositoryBase<RegisteredUser, int>, IRegisteredUserRepository
    {
        public RegisteredUserRepository(HospitalDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public List<RegisteredUser> GetAll()
            => FindAll();

        public RegisteredUser GetByUsername(string username)
            => FindByCondition(user => user.Username == username).FirstOrDefault();

        public RegisteredUser RegisterUser(RegisteredUser user)
            => Create(user);

    }
}
