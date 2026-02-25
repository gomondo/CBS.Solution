
using CBS.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CBS.Data.Entities
{
    [Table("Appointments")]
    public class Appointment: Audit
    {
        [Key]
        public long Id { get; set; }
        public long PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public long TimeSlotId { get; set; }
        [ForeignKey("TimeSlotId")]
        public TimeSlot TimeSlot { get; set; }  
        public bool IsActive { get; set; } = false;
    }

}

