using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hospital.Center.Services.Abstract;

namespace Hospital.Center.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService doctorService;
        public DoctorController(IDoctorService docService)
        {
            doctorService = docService;
        }

        [HttpGet("get")]
        public IActionResult GetGeneral()
        {
            var doctors = doctorService.GetGeneral();
            if (doctors != null)
                return Ok(doctors);
            return BadRequest("Error occured.");
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(doctorService.GetAll());
        }
    }
}
