
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Univercity.Client.Model;

namespace Univercity.Client.Service
{
    public class UniService
    {
        private readonly HttpClient _httpClient;

        public UniService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all majors
        public async Task<IEnumerable<MajorDto>> GetAllMajorsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<MajorDto>>("api/Major");
        }

        // Get major by ID
        public async Task<MajorDto> GetMajorByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<MajorDto>($"api/Major/{id}");
        }

        // Create a new major
        public async Task<Response> CreateMajorAsync(MajorDto majorDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Major", majorDto);
            return await response.Content.ReadFromJsonAsync<Response>();
        }

        // Update an existing major
        public async Task<Response> UpdateMajorAsync(int id, MajorDto majorDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Major/{id}", majorDto);
            return await response.Content.ReadFromJsonAsync<Response>();
        }

        // Delete a major by ID
        public async Task<Response> DeleteMajorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Major/{id}");
            return await response.Content.ReadFromJsonAsync<Response>();
        }
    }
}
