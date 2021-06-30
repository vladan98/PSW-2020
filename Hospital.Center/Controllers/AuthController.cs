using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain;
using Hospital.Domain.DTO;
using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Hospital.Center.Services.Abstract;

namespace Hospital.Center.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginDTO user)
        {
            if (user == null)
                return BadRequest("Invalid client request");

            var userDTO = authService.Login(user);

            if (userDTO == null)
                return BadRequest("Username or password is incorrect");

            return Ok(userDTO);
        }

    }
}
