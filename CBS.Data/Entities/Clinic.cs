
using CBS.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CBS.Data.Entities
{
    [Table("Clinics")]
    public class Clinic:Audit
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(50)]
        public string ClinicName { get; set; }
        [MaxLength(300)]
        public string Address { get; set; }
        [MaxLength(5)]
        public string PostalCode { get; set; }
        [MaxLength(50)]
        public string Subarb { get; set; }
        [MaxLength(50)]
        public string City { get; set; }

        public string Province { get; set; }
    }
}
