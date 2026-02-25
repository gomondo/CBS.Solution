using CBS.Api.Data;
using CBS.Api.Models;
using CBS.BLL.Services;
using CBS.Data.Entities;
using CBS.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CBS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly ILogger<IdentityController> _logger;
        private IService<Patient, PatientDto> PatientProfile;
        private IService<Staff, StaffDto> StaffProfile;

        public IdentityController(
            UserManager<ApplicationUser>  userManager,
            SignInManager<ApplicationUser> signInManager,
            
            IConfiguration                config,
            ILogger<IdentityController>   logger, IService<Patient, PatientDto> patientProfile, IService<Staff, StaffDto> staffProfile)
        {
            _userManager   = userManager;
            _signInManager = signInManager;
            _config        = config;
            _logger        = logger;
            PatientProfile = patientProfile;
            StaffProfile = staffProfile;
        }

        // 🔹 Register new user (external /patients)
        [HttpPost("external")]
        public async Task<IActionResult> RegisterExternal([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");           

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email,
                FullNames=model.FullNames };
            var resultUser = await _userManager.CreateAsync(user, model.Password);
            var resultRole = await _userManager.AddToRoleAsync(user, "User");

            if (resultUser.Succeeded && resultRole.Succeeded)
            {
                await PatientProfile.Create(new PatientDto() { Email = user.Email, FullNames = user.FullNames });

                return Ok("User registered successfully");
            }

            return BadRequest($"{resultUser.Errors} \n {resultRole.Errors}");
        }

        // Register new user (internal — Admin only)
        [HttpPost("internal")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterInternal([FromForm] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");

            var user       = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var resultUser = await _userManager.CreateAsync(user, model.Password);
            var resultRole = await _userManager.AddToRoleAsync(user, model.Role);

            if (resultUser.Succeeded && resultRole.Succeeded)
            {
                await PatientProfile.Create(new PatientDto() { Email = user.Email, FullNames = user.FullNames });

                return Ok("User registered successfully");

            }

            return StatusCode(500, $"{resultUser.Errors} \n {resultRole.Errors}");
        }

        //  Login — issues a self-signed JWT
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");

            try
            {
                // 1. Find user
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return Unauthorized("Invalid login attempt");

                // 2. Validate password (no cookie created)

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                //var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
                if (!result.Succeeded)
                    return Unauthorized("Invalid login attempt");

                // 3. Build claims — include the api.read scope required by the API policy
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email,          user.Email!),
                    new Claim(ClaimTypes.Name,           user.UserName!),
                    new Claim("scope",                   "api.read")
                };

                // Add role claims
                var roles = await _userManager.GetRolesAsync(user);
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

                // 4. Sign the token
                var key    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
                var creds  = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.UtcNow.AddHours(double.Parse(_config["Jwt:ExpiryHours"] ?? "8"));

                var token = new JwtSecurityToken(
                    issuer:             _config["Jwt:Issuer"],
                    audience:           _config["Jwt:Audience"],
                    claims:             claims,
                    expires:            expiry,
                    signingCredentials: creds);

                // 5. Return OAuth-style response expected by the Blazor client
                return Ok(new
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    token_type   = "Bearer",
                    expires_in   = (int)(expiry - DateTime.UtcNow).TotalSeconds
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error for {Email}", model.Email);
                return StatusCode(500, "An unexpected error occurred");
            }
        }
        [HttpGet("profile/practitioner/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StaffDto>>GetStaffProfile(string email)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return NotFound("User not found");

            StaffDto profile = new();

            if (User.IsInRole("Staff")|| User.IsInRole("Admin"))
            {
                var staffs = await StaffProfile.All();

                if (staffs == null)
                    return NotFound("Staff profiles not found");

                var staff = staffs.FirstOrDefault(e => e.Email == email);
                if(staff== null)
                    return NotFound("Staff profile not found");

                profile = staff;
            }


            return Ok(profile);
           
        }
        [HttpGet("profile/patient/{email}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<PatientDto>> GetPatientProfile(string email)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return NotFound("User not found");

            PatientDto profile = new();

          
            if (User.IsInRole("User")|| User.IsInRole("Admin"))
            {
                var patients = await PatientProfile.All();

                if (patients == null)
                    return NotFound("Patient profiles not found");

                var patient = patients.FirstOrDefault(e => e.Email == email);
                if (patient == null)
                    return NotFound("Patient profile not found");

                profile = patient;
            }
            if (profile == null)
                return NotFound("not found");

            return Ok(profile);


        }

        // Logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully");
        }

        // Change Password
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound("User not found");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
                return Ok("Password changed successfully");

            return BadRequest(result.Errors);
        }
    }
}
