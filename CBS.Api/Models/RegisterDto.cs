namespace CBS.Api.Models
{
    public class RegisterDto
    {
        public string Email { get; set; }=string.Empty;
        public string Password { get; set; }=string.Empty;
        public string FullNames { get; set; }=string.Empty;
        public string Role { get; set; }=string.Empty;      
        public List<String> Roles { get { return new List<string>() { "Admin", "Staff", "User" }; } }
    }
}
