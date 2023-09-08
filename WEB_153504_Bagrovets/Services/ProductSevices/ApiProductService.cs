using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;

namespace Web_153504_Bagrovets_Lab1.Services.ProductSevices
{
    public class ApiProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private JsonSerializerOptions _serializerOptions;
        private ILogger<ApiProductService> _logger;

        public ApiProductService(HttpClient httpClient, IConfiguration configuration,
            ILogger<ApiProductService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        public async Task<ResponseData<Product>> CreateProductAsync(Product product,
            IFormFile? formFile)
        {
            var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + "Products");
            var response = await _httpClient.PostAsJsonAsync(
            uri,
            product,
            _serializerOptions);
            if (response.IsSuccessStatusCode)
            {
                var data = await response
                .Content
                .ReadFromJsonAsync<ResponseData<Product>>
                (_serializerOptions);
                return data; // Product;
            }
            _logger
            .LogError($"-----> object not created. Error:{response.StatusCode.ToString()}");
            return new ResponseData<Product>
            {
                Success = false,
                ErrorMessage = $"Объект не добавлен. Error:{response.StatusCode.ToString()}"
            };
        }
        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<ListModel<Product>>> GetProductListAsync(string? categoryNormalizedName, int pageNo)
        {
            // подготовка URL запроса
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}Product");
            var pageSize = _configuration.GetSection("ItemsPerPage").Value;
            // добавить категорию в маршрут
            if (categoryNormalizedName != null)
            {
                urlString.Append($"?category={categoryNormalizedName}&");
            };
            // добавить номер страницы в маршрут
            if(pageNo >  1 && categoryNormalizedName == null)
            {
                urlString.Append($"?pageNo={pageNo}&");
            }
            else if (pageNo > 1)
            {
                urlString.Append($"pageNo={pageNo}&");
            };
            // добавить размер страницы в строку запроса
            if (!pageSize.Equals("3"))
            {
                urlString.Append($"?pageSize={pageSize}");
            }
            // отправить запрос к API
            string url = urlString.ToString();
            var response = await _httpClient.GetAsync(new Uri(url));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response
                    .Content
                    .ReadFromJsonAsync<ResponseData<ListModel<Product>>>
                    (_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<ListModel<Product>>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {ex.Message}"
                    };
                }
            }

            _logger.LogError($"-----> Данные не получены от сервера. Error:{response.StatusCode.ToString()}");
            return new ResponseData<ListModel<Product>>
            {
                Success = false,
                ErrorMessage = $"Данные не получены от сервера. Error: {response.StatusCode.ToString()}"
            };
        }

        public Task UpdateProductAsync(int id, Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
