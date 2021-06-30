using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Domain;
using Hospital.Domain.DTO;
using Hospital.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hospital.Center.Services.Abstract;

namespace Hospital.Center.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        public PatientController(IPatientService patService)
        {
            patientService = patService;
        }

        [HttpGet("malicious")]
        public IActionResult GetMalicious()
        {
            return Ok(patientService.GetMalicious());
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(patientService.GetAll());
        }

        [HttpGet("block/{patientId}")]
        public IActionResult BlockPatient(int patientId)
        {
            var done = patientService.BlockPatient(patientId);
            if (done) return Ok("Patient blocked.");
            return BadRequest("Error occured.");
        }

        [HttpPost, Route("register")]
        public IActionResult RegisterPatient([FromBody] RegisterPatientDTO registerPatientDTO)
        {
            Patient patient = UserMapper.RegisterPatientDTOToPatient(registerPatientDTO);

            var user = patientService.Register(patient);
            if (user != null) return Ok("User " + patient.FirstName + " registered.");

            return BadRequest("Error occured.");
        }
    }
}
