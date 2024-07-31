using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    public class CategoryApiService
    {

        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("categories");

            return response.Data;
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto newCategory)
        {
            var response = await _httpClient.PostAsJsonAsync("categories", newCategory);
            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<CategoryDto>>();
            return responseBody.Data;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(CategoryDto newCategory)
        {
            var response = await _httpClient.PutAsJsonAsync("categories", newCategory);

            return response.IsSuccessStatusCode;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<CategoryDto>>($"categories/{id}");
            return response.Data;
        }
    }
}
