using Hospital.Domain;
using Hospital.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Hospital.Center.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Controllers
{
    [ApiController]
    [Route("api/referral")]
    public class ReferralController : ControllerBase
    {
        private readonly IReferralService referralService;
        public ReferralController(IReferralService refService)
        {
            referralService = refService;
        }

        [HttpGet("patient/{patientId}")]
        public IActionResult PatientReferrals(int patientId)
        {
            var all = referralService.GetByPatientId(patientId);

            return Ok(all);
        }

        [HttpGet("update/{referralId}")]
        public IActionResult UpdateUsed(int referralId)
        {
            var done = referralService.UpdateUsed(referralId);
            if (done)
                return Ok("Updated.");
            return BadRequest("Error occured.");
        }

        [HttpPost("add")]
        public IActionResult CreateReferral(ReferralCreateDTO createDTO)
        {
            var referral = ReferralMapper.CreateReferralDTOToReferral(createDTO);
            var done = referralService.Create(referral);
            if (done)
                return Ok("Referral created.");
            return BadRequest();
        }
    }
}
