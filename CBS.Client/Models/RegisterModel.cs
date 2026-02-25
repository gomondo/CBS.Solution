using System.ComponentModel.DataAnnotations;

namespace CBS.Client.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full name is required.")]
        [MinLength(2, ErrorMessage = "Full name must be at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "Full name must be under 100 characters.")]
        public string FullNames { get; set; } = string.Empty;

        // Set programmatically from the route — not shown in the form
        public string Role { get; set; } = string.Empty;
        public string Type { get; set; } = "external";
    }
}
