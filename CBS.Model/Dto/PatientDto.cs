using System.ComponentModel.DataAnnotations;

namespace CBS.Model.Dto
{
    public class PatientDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Full names is required.")]
        public string FullNames { get; set; } = string.Empty;
        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Birth date is required.")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Condition is required.")]
        public string Condition { get; set; } = string.Empty;
        public bool HasMedicalAid { get; set; }
        public string MedicalAidName { get; set; } = string.Empty;
        public string PolicyNumber { get; set; } = string.Empty;


    }
}
