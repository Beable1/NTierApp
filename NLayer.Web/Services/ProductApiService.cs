using NLayer.Core.DTOs;
using System.Net.Http.Json;

namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategory()
        {

            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("product/GetProductsWithCategory");
            return response.Data;   
        }

        public async Task<ProductDto> Save(ProductDto newProduct)
        {
            var response= await _httpClient.PostAsJsonAsync("product",newProduct);
            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
            return responseBody.Data;
        }

        public async Task<bool> Update(ProductDto newProduct)
        {
            var response = await _httpClient.PutAsJsonAsync("product", newProduct);
            
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"product/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"product/{id}");
            return response.Data;
        }



    }
}
