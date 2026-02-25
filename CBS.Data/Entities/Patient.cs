
using CBS.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CBS.Data.Entities
{
    [Table("Patients")]
    public  class Patient:Audit
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(150)]
        public string FullNames { get; set; }
        public DateTime BirthDate { get; set; }
        [MaxLength(300)]
        public string Condition { get; set; }
        public bool HasMedicalAid { get; set; }
        [MaxLength(13)]
        public string MedicalAidName { get; set; }
        public string Policyumber { get; set; }
        [NotMapped]
        public int Age { get { return GetAge(BirthDate); } }
        private int GetAge(DateTime birthDay)
        {
            return DateTime.Now.Year - birthDay.Year;
        }
    }
}

