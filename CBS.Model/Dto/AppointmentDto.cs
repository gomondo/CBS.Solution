using System.ComponentModel.DataAnnotations;

namespace CBS.Model.Dto
{
    public class AppointmentDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Patient is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select a valid patient.")]
        public long PatientId { get; set; }

        [Required(ErrorMessage = "Staff member is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select a valid staff member.")]
        public long StaffId { get; set; }

        /// <summary>
        /// Booking vs Appointment flag — same model, different lifecycle state.
        /// false = Booking   → pending, not yet accepted by the practitioner.
        /// true  = Appointment → accepted / confirmed by the practitioner.
        /// Bookings are promoted to Appointments by setting IsActive = true via Edit().
        /// </summary>
        public bool IsActive { get; set; } = false;

        [Required(ErrorMessage = "Clinic is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select a valid clinic.")]
        public long ClinicId { get; set; }

        [Required(ErrorMessage = "Timeslot is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Please select a valid timeslot.")]
        public long TimeSlotId { get; set; }
    }
}
