using System.ComponentModel.DataAnnotations;

namespace CBS.Model.Dto
{
    public class ClinicDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Clinic name is required.")]
        public string ClinicName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Clinic E-mail is required.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Clinic phone number is required.")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Clinic address is required.")]
        public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "Clinic phone number is required.")]
        public string PostalCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Clinic surbarbs is required.")]
        public string Surbarbs { get; set; } = string.Empty;
        [Required(ErrorMessage = "Clinic city is required.")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "Clinic province is required.")]
        public string Province { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
