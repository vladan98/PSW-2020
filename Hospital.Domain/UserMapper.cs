using Hospital.Domain.DTO;
using Hospital.Domain.Enums;
using Hospital.Domain.Models.Users;

namespace Hospital.Domain
{
    public static class UserMapper
    {
        public static Patient RegisterPatientDTOToPatient(RegisterPatientDTO dto)
        {
            return new Patient()
            {
                Username = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                Gender = CastGenderOfPatient(dto.Gender),
                Role = Role.PATIENT,
                ChosenDoctorId = dto.ChosenDoctorId,
                ShouldBeBlocked = false,
                Blocked = false,
            };
        }
        public static AuthenticatedUserDTO UserToAuthenticatedUserDTO(RegisteredUser user, string token)
        {
            return new AuthenticatedUserDTO()
            {
                Username = user.Username,
                Role = user.Role,
                Id = user.Id,
                Token = token
            };
        }
        private static Gender CastGenderOfPatient(int type)
        {
            if (type == 1)
                return Gender.FEMALE;
            return Gender.MALE;
        }
    }
}
