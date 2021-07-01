using Hospital.Domain;
using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hospital.Center.Repository;
using Hospital.Center.Services;
using System;
using System.Collections.Generic;

namespace Hospital.Center.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;
        public AppointmentController(IAppointmentService repositoryWrapper)
        {
            appointmentService = repositoryWrapper;
        }

        [HttpPost("schedule")]
        public IActionResult Schedule([FromBody] AppointmentDTO dto)
        {
            Appointment appointment = AppointmentMapper.AppointmentDTOToAppointment(dto);

            var done = appointmentService.ScheduleAppointment(appointment, dto.ReferralId);

            if (done)
                return Ok("Appointment created.");

            return BadRequest("Error occured.");
        }

        [HttpGet("user/{userId}")]
        public IActionResult AppointmentHistory(int userId)
        {
            var all = appointmentService.UserAppointments(userId);

            return Ok(AppointmentMapper.AppointmentsToUserAppointmentsDTO(all));

        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] SearchAppointmentsDTO searchDTO)
        {

            SearchParameters searchParameters = AppointmentMapper.SearchDTOToSearchParameters(searchDTO);

            return Ok(AppointmentMapper.AppointmentsToAppointmentsDTOs(appointmentService.SearchAppointments(searchParameters)));
        }

        [HttpGet("cancel/{appointmentId}")]
        public IActionResult Cancel(int appointmentId)
        {
            var done = appointmentService.CancelAppointment(appointmentId);

            if (done)
                return Ok("Appointment canceled.");

            return BadRequest("Error occured.");
        }

    }
}
