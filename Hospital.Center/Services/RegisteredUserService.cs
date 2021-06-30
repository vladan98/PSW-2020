using Hospital.Domain.Models.Users;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Services
{
    public class RegisteredUserService : IRegisteredUserService
    {
        private readonly IRegisteredUserRepository userRepository;

        public RegisteredUserService(IRegisteredUserRepository userRepo)
        {
            userRepository = userRepo;
        }

        public List<RegisteredUser> GetAll()
            => userRepository.FindAll();

        public RegisteredUser GetByUsername(string username)
            => userRepository.GetByUsername(username);

        public RegisteredUser RegisterUser(RegisteredUser user)
            => userRepository.RegisterUser(user);
    }
}
