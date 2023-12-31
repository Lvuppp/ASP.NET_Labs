﻿using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
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
        private HttpContext _httpContext;

        public ApiProductService(HttpClient httpClient, IConfiguration configuration,
            ILogger<ApiProductService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<ResponseData<Product>> CreateProductAsync(Product product,
            IFormFile? formFile)
        {
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("bearer", token);

            var uri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}{_configuration.GetSection("apiProductUri").Value}");
            var response = await _httpClient.PostAsJsonAsync(uri, product);

            if (response.IsSuccessStatusCode)
            {
                var data = await response
                .Content
                .ReadFromJsonAsync<ResponseData<Product>>
                (_serializerOptions);
                
                if(formFile != null)
                    await SaveImageAsync(data.Data.Id, formFile);
                
                return data;
            }
            _logger.LogError($"-----> object not created. Error:{response.StatusCode.ToString()}");
            return new ResponseData<Product>
            {
                Success = false,
                ErrorMessage = $"Объект не добавлен. Error:{response.StatusCode.ToString()}"
            };
        }
        public async Task DeleteProductAsync(int id)
        {
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("bearer", token);

            var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress.AbsoluteUri}{_configuration.GetSection("apiProductUri").Value}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            _logger.LogError($"-----> object not created. Error:{response.StatusCode.ToString()}");
        }

        public async Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}{_configuration.GetSection("apiProductUri").Value}/{id}");
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response
                    .Content
                    .ReadFromJsonAsync<ResponseData<Product>>
                    (_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<Product>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {ex.Message}"
                    };
                }
            }

            _logger.LogError($"-----> Данные не получены от сервера. Error:{response.StatusCode.ToString()}");
            return new ResponseData<Product>
            {
                Success = false,
                ErrorMessage = $"Данные не получены от сервера. Error: {response.StatusCode.ToString()}"
            };
        }

        public async Task<ResponseData<ListModel<Product>>> GetProductListAsync(string? categoryNormalizedName, int pageNo)
        {

            // подготовка URL запроса
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}{_configuration.GetSection("apiProductUri").Value}");
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

        public async Task UpdateProductAsync(int id, Product product, IFormFile? formFile)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}{_configuration.GetSection("apiProductUri").Value}/{id}"),
            };

            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("bearer", token);

            var response = await _httpClient.PutAsJsonAsync(request.RequestUri, product);

            if (response.IsSuccessStatusCode)
            {
                if (formFile != null)
                    await SaveImageAsync(id, formFile);
                return;
            }

            _logger.LogError($"-----> object not update. Error:{response.StatusCode.ToString()}");
        }

        private async Task SaveImageAsync(int id, IFormFile image)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}Products/{id}"),
            };

            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("bearer", token);
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(image.OpenReadStream());
            content.Add(streamContent, "formFile", image.FileName);
            request.Content = content;
            await _httpClient.SendAsync(request);
        }
     
    }
}
