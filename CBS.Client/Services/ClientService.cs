using CBS.Client.Helper;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;

namespace CBS.Client.Services
{
    public class ClientService<T> : IClientService<T> where T : class
    {
        private HttpClient HttpClient { get; set; }
        public string EndPoint { get; set; } = string.Empty;
        public string HttpErrorMessage { get; set; } = string.Empty;

        public ClientService(HttpClient httpClient)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        // ──────────────────────────────────────────────────────────────────
        // READ
        // ──────────────────────────────────────────────────────────────────

        public async Task<IEnumerable<T>> List()
        {
            IEnumerable<T> response = Enumerable.Empty<T>();
            HttpErrorMessage = string.Empty;

            try
            {
                var requestUrl = HttpClient.BaseAddress + EndPoint;
                var httpResponse = HttpClient.GetFromJsonAsAsyncEnumerable<T>(requestUrl);

                if (httpResponse != null)
                {
                    var results = new List<T>();
                    await foreach (var item in httpResponse)
                    {
                        if (item != null)
                            results.Add(item);
                    }
                    response = results;
                }
            }
            catch (HttpRequestException ex)
            {
                HttpErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<T> Find(long id)
        {
            T response = null;
            HttpErrorMessage = string.Empty;

            try
            {
                var requestUrl = $"{HttpClient.BaseAddress + EndPoint}/{id}";
                var httpResponse = await HttpClient.GetFromJsonAsync<T>(requestUrl);

                if (httpResponse != null)
                    response = httpResponse;
            }
            catch (HttpRequestException ex)
            {
                HttpErrorMessage = ex.Message;
            }

            return response;
        }

        // ──────────────────────────────────────────────────────────────────
        // CREATE  —  validate first, then POST
        // ──────────────────────────────────────────────────────────────────

        public async Task<KeyValuePair<bool, string>> AddNew(T model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "No data passed.");

            HttpErrorMessage = string.Empty;

            // 1. DataAnnotations validation (same approach as IdentityClientService)
            var validationErrors = InputValidator.ValidateModel(model);
            if (validationErrors.Any())
                return new KeyValuePair<bool, string>(false, string.Join("; ", validationErrors));

            // 2. POST to API
            try
            {
                HttpContent httpContent = JsonContent.Create(model);
                var http = await HttpClient.PostAsync(HttpClient.BaseAddress + EndPoint, httpContent);
                var message = await http.Content.ReadAsStringAsync();

                if (!http.IsSuccessStatusCode)
                {
                    HttpErrorMessage = message;
                    return new KeyValuePair<bool, string>(false, message);
                }

                return new KeyValuePair<bool, string>(true, message);
            }
            catch (HttpRequestException ex)
            {
                HttpErrorMessage = ex.Message;
                throw;
            }
        }

        // ──────────────────────────────────────────────────────────────────
        // UPDATE  —  validate first, then PUT
        // ──────────────────────────────────────────────────────────────────

        public async Task<KeyValuePair<bool, string>> Edit(long id, T model)
        {
            if (id == 0)
                throw new ArgumentException("Required system Id not found — try refreshing.", nameof(id));

            if (model is null)
                throw new ArgumentNullException(nameof(model), "No data passed.");

            HttpErrorMessage = string.Empty;

            // 1. DataAnnotations validation
            var validationErrors = InputValidator.ValidateModel(model);
            if (validationErrors.Any())
                return new KeyValuePair<bool, string>(false, string.Join("; ", validationErrors));

            // 2. PUT to API
            try
            {
                var requestUrl = $"{HttpClient.BaseAddress + EndPoint}/{id}";
                HttpContent httpContent = JsonContent.Create(model);
                var http = await HttpClient.PutAsync(requestUrl, httpContent);
                var message = await http.Content.ReadAsStringAsync();

                if (!http.IsSuccessStatusCode)
                {
                    HttpErrorMessage = message;
                    return new KeyValuePair<bool, string>(false, message);
                }

                return new KeyValuePair<bool, string>(true, message);
            }
            catch (HttpRequestException ex)
            {
                HttpErrorMessage = ex.Message;
                throw;
            }
        }

        // ──────────────────────────────────────────────────────────────────
        // DELETE
        // ──────────────────────────────────────────────────────────────────

        public async Task<KeyValuePair<bool, string>> Delete(long id)
        {
            HttpErrorMessage = string.Empty;

            try
            {
                var requestUrl = $"{HttpClient.BaseAddress + EndPoint}/{id}";
                var http = await HttpClient.DeleteAsync(requestUrl);
                var message = await http.Content.ReadAsStringAsync();

                if (!http.IsSuccessStatusCode)
                {
                    HttpErrorMessage = message;
                    return new KeyValuePair<bool, string>(false, message);
                }

                return new KeyValuePair<bool, string>(true, message);
            }
            catch (HttpRequestException ex)
            {
                HttpErrorMessage = ex.Message;
                throw;
            }
        }
    }
     
}
