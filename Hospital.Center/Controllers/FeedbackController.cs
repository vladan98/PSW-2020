using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Domain;
using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hospital.Center.Services.Abstract;

namespace Hospital.Center.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {

        private readonly IFeedbackService feedbackService;
        public FeedbackController(IFeedbackService _feedbackService)
        {
            feedbackService = _feedbackService;
        }

        [HttpPost("post")]
        public IActionResult LeaveFeedback([FromBody] LeaveFeedbackDTO dto)
        {
            var feedback = FeedbackMapper.LeaveFeedbackDTOToFeedback(dto);
            var done = feedbackService.LeaveFeedback(feedback);
            if (done)
                return Ok("Feedback sent.");
            return BadRequest("Error occured.");
        }

        [HttpGet("update-published/{feedbackId}")]
        public IActionResult UpdatePublished(int feedbackId)
        {
            var done = feedbackService.UpdatePublished(feedbackId);
            if (done)
                return Ok("Updated.");
            return BadRequest("Error occured.");
        }

        [HttpGet("get-published")]
        public IActionResult GetPublished()
        {
            var published = feedbackService.GetPublished();
            if (published != null)
                return Ok(published);
            return BadRequest("Error occured.");
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var all = feedbackService.GetAll();
            if (all != null)
                return Ok(all);
            return BadRequest("Error occured.");
        }
    }
}
