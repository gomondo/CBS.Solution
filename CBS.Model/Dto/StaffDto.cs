using System.ComponentModel.DataAnnotations;

namespace CBS.Model.Dto
{
    public class StaffDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }= string.Empty;
        [Required(ErrorMessage = "Full names aer required.")]
        public string FullNames { get; set; } = string.Empty;
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Specility is required.")]
        public string Specility { get; set; } = string.Empty;
        public string Experiance { get; set; } = string.Empty;
        public long ClinicId { get; set; }
        public string ClinicName { get;set; }= string.Empty;
        public bool HasClinic { get; set; }
        public bool IsActive { get; set; }=true;
    }
}
