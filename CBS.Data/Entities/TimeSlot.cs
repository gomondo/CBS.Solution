
using CBS.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CBS.Data.Entities
{
    [Table("TimeSlots")]
    public class TimeSlot : Audit
    {
        public long Id { get; set; }
        [MaxLength(300)]
        public string DescriptionSlot { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public long StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; } = new();
        public long? ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public Clinic Clinic { get; set; } = new();
        public bool IsTimeSlotOpen { get; set; }
    }
}
