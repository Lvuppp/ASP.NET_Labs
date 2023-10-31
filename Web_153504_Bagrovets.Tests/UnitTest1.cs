using Microsoft.AspNetCore.Mvc;
using Web_153504_Bagrovets.API.Controllers;
using Web_153504_Bagrovets_Lab1.Controllers;
using Web_153504_Bagrovets_Lab1.Services.ProductSevices;
using Web_153504_Bagrovets_Lab1.Services.CategoryServices;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Web_153504_Bagrovets.Tests
{
    public class ProductControllerTests
    {
        private readonly IConfiguration _configuration;
        private IProductService _productService;
        private ICategoryService _categoryService;
        public ProductControllerTests(IConfiguration configuration)
        {
            var configBuilder = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json"); // Укажите путь к вашему файлу конфигурации
            var services = new ServiceCollection();

            // Регистрация зависимостей
            services.AddHttpClient<ApiProductService>();
            services.AddHttpClient<ApiCategoryService>();

            var serviceProvider = services.BuildServiceProvider();
            _productService = serviceProvider.GetRequiredService<ApiProductService>();
            _categoryService = serviceProvider.GetRequiredService<ApiCategoryService>();
        }

        [Fact]
        public async Task Index_Returns404_WhenCategoriesListFails()
        {
            // Arrange
            var controller = new Product(productService, categoryService);

            // Act
            var result = await controller.Index("99", 1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Index_Returns404_WhenProductListFails()
        {
            // Arrange
            var productService = new ProductService(); // Замените на ваш сервис
            var categoryService = new CategoryService(); // Замените на ваш сервис
            var controller = new ProductController(productService, categoryService);

            // Act
            var result = await controller.Index(null, 1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Index_ReturnsCorrectViewModel_WhenSuccessful()
        {
            // Arrange
            var productService = new ProductService(); // Замените на ваш сервис
            var categoryService = new CategoryService(); // Замените на ваш сервис
            var controller = new ProductController(productService, categoryService);

            // Act
            var result = await controller.Index("TestCategory", 1) as ViewResult;
            var model = result.Model as YourViewModel; // Замените на вашу модель

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.NotNull(controller.ViewData["categories"]);
            Assert.Equal("TestCategory", controller.ViewData["currentCategory"]);
        }
    }
}