
using CBS.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CBS.Data.Entities
{
    [Table("Staff")]
    public class Staff:Audit
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(150)]
        public string FullNames { get; set; }
        [MaxLength(50)]
        public string JobTitle { get; set; }
        [MaxLength(100)]
        public string  Specility { get; set; }
        [MaxLength(300)]
        public string Experiance { get; set; }
        public long ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public Clinic Clinic { get; set; } 
    }
}
