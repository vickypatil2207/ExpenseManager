using ExpenseManager.Shared;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ExpenseManager.Web.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Sets the authorization token for API requests
        /// </summary>
        private void SetAuthorizationHeader(string? token = null)
        {
            token ??= _configuration["TempToken"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        /// <summary>
        /// GET request
        /// </summary>
        public async Task<ServiceResult<T>> GetAsync<T>(string endpoint, string? token = null) where T : class
        {
            try
            {                
                SetAuthorizationHeader(token);
                var response = await _httpClient.GetAsync(endpoint);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// POST request
        /// </summary>
        public async Task<ServiceResult<T>> PostAsync<T>(string endpoint, object? body = null, string? token = null) where T : class
        {
            try
            {
                SetAuthorizationHeader(token);
                var content = body != null 
                    ? new StringContent(JsonSerializer.Serialize(body, _jsonSerializerOptions), System.Text.Encoding.UTF8, "application/json")
                    : null;

                var response = await _httpClient.PostAsync(endpoint, content);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// PUT request
        /// </summary>
        public async Task<ServiceResult<T>> PutAsync<T>(string endpoint, object? body = null, string? token = null) where T : class
        {
            try
            {
                SetAuthorizationHeader(token);
                var content = body != null
                    ? new StringContent(JsonSerializer.Serialize(body, _jsonSerializerOptions), System.Text.Encoding.UTF8, "application/json")
                    : null;

                var response = await _httpClient.PutAsync(endpoint, content);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// DELETE request
        /// </summary>
        public async Task<ServiceResult<T>> DeleteAsync<T>(string endpoint, string? token = null) where T : class
        {
            try
            {
                SetAuthorizationHeader(token);
                var response = await _httpClient.DeleteAsync(endpoint);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// PATCH request
        /// </summary>
        public async Task<ServiceResult<T>> PatchAsync<T>(string endpoint, object? body = null, string? token = null) where T : class
        {
            try
            {
                SetAuthorizationHeader(token);
                var content = body != null
                    ? new StringContent(JsonSerializer.Serialize(body, _jsonSerializerOptions), System.Text.Encoding.UTF8, "application/json")
                    : null;

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), endpoint) { Content = content };
                var response = await _httpClient.SendAsync(request);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Handle HTTP response and deserialize to ServiceResult<T>
        /// </summary>
        private async Task<ServiceResult<T>> HandleResponse<T>(HttpResponseMessage response) where T : class
        {
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                if (string.IsNullOrEmpty(content))
                    return ServiceResult<T>.Ok(null!);

                var result = JsonSerializer.Deserialize<ServiceResult<T>>(content, _jsonSerializerOptions);
                return result ?? ServiceResult<T>.Ok(null!);
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ServiceResult<T>>(content, _jsonSerializerOptions);
                    if (errorResult != null)
                        return errorResult;
                }
                catch { }

                return ServiceResult<T>.Fail($"Request failed with status code {response.StatusCode}");
            }
        }
    }
}
