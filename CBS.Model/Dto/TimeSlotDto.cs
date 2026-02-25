using System.ComponentModel.DataAnnotations;

namespace CBS.Model.Dto
{
    public class TimeSlotDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string DescriptionSlot { get; set; } = string.Empty;
        [Required(ErrorMessage = "From date and time are required.")]
        public DateTime FromDateTime { get; set; }
        [Required(ErrorMessage = "To date and time are required.")]
        public DateTime ToDateTime { get;  set; }
        public TimeSpan Duration { get; set; }
        public long ClinicId { get; set; }
        public string ClinicName { get; set; } = string.Empty;
       public long StaffId { get; set; }
        public string FullNames { get; set; } = string.Empty;   
        public bool TimeSlotOpen { get; set; }
        public long? AppointmentId { get; set; }
        public bool IsAvailable { get { return TimeSlotOpen && AppointmentId == null; } }
    }
}
