using Hospital.Domain;
using Hospital.Domain.DTO;
using Hospital.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using Hospital.Center.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Center.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRegisteredUserService userService;
        private readonly IPatientService patientService;
        public AuthService(IRegisteredUserService usrService, IPatientService patService)
        {
            userService = usrService;
            patientService = patService;
        }

        public AuthenticatedUserDTO Login(LoginDTO user)
        {
            var registeredUser = userService.GetByUsername(user.UserName);

            if (registeredUser != null)
            {
                if (registeredUser.Role == Role.PATIENT)
                {
                    var patient = patientService.GetById(registeredUser.Id);
                    if (patient.Blocked)
                        return new AuthenticatedUserDTO() { Id = -1 };
                }
                if (registeredUser.Password == user.Password)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@hospital@hospital@hospital"));
                    var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
                    {
                        new Claim("Name", registeredUser.Username),
                        new Claim("Role", registeredUser.Role)
                    };

                    var tokenOptions = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: credentials
                        );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return UserMapper.UserToAuthenticatedUserDTO(registeredUser, tokenString);
                }
            }
            return null;

        }
    }
}
