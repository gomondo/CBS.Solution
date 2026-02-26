using CBS.Client.Helper;
using CBS.Client.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;

namespace CBS.Client.Services
{
    public class IdentityClientService
    {
        private readonly HttpClient _httpClient;

        public IdentityClientService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<KeyValuePair<bool, string>> Register(RegisterModel register)
        {
            // Validate input
            if (register is null)
                throw new ArgumentNullException(nameof(register));

            var validationErrors = InputValidator.ValidateModel(register);
            if (validationErrors.Any())
                return new KeyValuePair<bool, string>(false, string.Join("; ", validationErrors));

            if (string.IsNullOrWhiteSpace(register.Type))
                return new KeyValuePair<bool, string>(false, "Registration type is required.");

            try
            {
                HttpContent httpContent = JsonContent.Create(register);

                var requestUrl = $"{_httpClient.BaseAddress.AbsoluteUri}api/Identity/{register.Type}";
               
                var response = await _httpClient.PostAsync(requestUrl, httpContent);

                var message = await response.Content.ReadAsStringAsync();

                return new KeyValuePair<bool, string>(response.IsSuccessStatusCode, message);
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }

        public async Task<KeyValuePair<bool, string>> Logout()
        {
            try
            {

                var requestUrl = $"{_httpClient.BaseAddress.AbsoluteUri}api/Identity/logout";

                var message = await _httpClient.GetStringAsync(requestUrl);

  
                return new KeyValuePair<bool, string>(true, message);       
            }
            catch (HttpRequestException hex)
            {
                throw hex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<KeyValuePair<bool, string>> Login(LoginModel login)
        {
            // Validate input
            if (login is null)
                throw new ArgumentNullException(nameof(login));

            var validationErrors =InputValidator.ValidateModel(login);
            if (validationErrors.Any())
                return new KeyValuePair<bool, string>(false, string.Join("; ", validationErrors));

            try
            {
                HttpContent httpContent = JsonContent.Create(login);

                var requestUrl = $"{_httpClient.BaseAddress.AbsoluteUri}api/Identity/login";

                var response = await _httpClient.PostAsync(requestUrl, httpContent);

                var message = await response.Content.ReadAsStringAsync();

                return new KeyValuePair<bool, string>(response.IsSuccessStatusCode, message);
            }
            catch (HttpRequestException hex)
            {
                throw hex;
            }catch(Exception ex)
            {
                throw ex;
            }

           
        }
        public async Task<T> GetProfileData<T>(string email, bool isStaff)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Email field is required.");

            T response;

                var requestUrl = string.Empty;

                if (isStaff)
                    requestUrl = $"{_httpClient.BaseAddress.AbsoluteUri}api/Identity/profile/practitioner/{email}";
                else
                    requestUrl = $"{_httpClient.BaseAddress.AbsoluteUri}api/Identity/profile/patient/{email}";

                response = await _httpClient.GetFromJsonAsync<T>(requestUrl);
         
            return response;
        }

    }
}
