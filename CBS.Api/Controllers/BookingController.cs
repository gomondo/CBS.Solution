using CBS.BLL.Services;
using CBS.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ITimeslotService _timeslotService;

        public BookingController(IAppointmentService appointmentService, ITimeslotService timeslotService)
        {
            _appointmentService = appointmentService;
            _timeslotService = timeslotService;
        }

        // ---------------- Appointment Endpoints ----------------

        [HttpPost("book/{timeSlotId}")]
        [Authorize(Roles = "Admin,Staff,User")]
        public async Task<ActionResult<AppointmentDto>> Book(long timeSlotId, [FromBody] AppointmentDto appointment)
        {
            var result = await _appointmentService.BookAppointment(timeSlotId, appointment);
            return Ok(result);
        }

        [HttpPost("cancel/{appointmentId}")]
        [Authorize(Roles = "Admin,Staff,User")]
        public async Task<ActionResult> Cancel(long appointmentId)
        {
            var success = await _appointmentService.CancelAppointment(appointmentId);
            if (!success) return NotFound();
            return Ok();
        }

        [HttpPost("reschedule/{appointmentId}")]
        [Authorize(Roles = "Admin,Staff,User")]
        public async Task<ActionResult<AppointmentDto>> Reschedule(long appointmentId, [FromBody] AppointmentDto appointment)
        {
            var result = await _appointmentService.ReschedulingAppointment(appointmentId, appointment);
            return Ok(result);
        }

        // ---------------- Timeslot Endpoints ----------------

        [HttpGet("timeslots/clinic/{clinicId}")]
        [Authorize(Roles = "Admin,Staff,User")]
        public async Task<ActionResult<IEnumerable<TimeSlotDto>>> GetAvailableByClinic(long clinicId)
        {
            var slots = await _timeslotService.GetAvailableTimeslots(clinicId);
            return Ok(slots);
        }

        [HttpGet("timeslots/staff/{staffId}")]
        [Authorize(Roles = "Admin,Staff,User")]
        public async Task<ActionResult<IEnumerable<TimeSlotDto>>> GetAvailableByStaff(long staffId)
        {
            var slots = await _timeslotService.GetAvailableTimeslotsByStaffId(staffId);
            return Ok(slots);
        }
    }
}