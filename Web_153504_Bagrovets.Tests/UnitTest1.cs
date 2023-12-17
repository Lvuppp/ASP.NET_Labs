using Microsoft.AspNetCore.Mvc;
using Web_153504_Bagrovets_Lab1.Controllers;
using Web_153504_Bagrovets_Lab1.Models;
using Web_153504_Bagrovets_Lab1.Services.ProductSevices;
using Web_153504_Bagrovets_Lab1.Services.CategoryServices;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Web_153504_Bagrovets.Domain.Entities;
using Xunit.Sdk;
using Xunit;

namespace Web_153504_Bagrovets.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task Index_ReturnsNotFound_WhenCategoryServiceFails()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetCategoryListAsync()).ReturnsAsync(new ResponseData<List<Category>> { Success = false });

            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.GetProductListAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new ResponseData<ListModel<Web_153504_Bagrovets.Domain.Entities.Product>> { Success = false });

            var controller = new Web_153504_Bagrovets_Lab1.Controllers.Product(productServiceMock.Object, categoryServiceMock.Object); // Инициализируйте контроллер

            // Act
            var result = await controller.Index("oifjksnkdflk", 2);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

       // [Fact]
        //public async Task Index_ReturnsNotFound_WhenProductServiceFails()
        //{
        //    // Arrange
        //    var categoryServiceMock = new Mock<ICategoryService>();
        //    categoryServiceMock.Setup(x => x.GetCategoryListAsync()).ReturnsAsync(new ResponseData<List<Category>> { Success = false });

        //    var productServiceMock = new Mock<IProductService>();
        //    productServiceMock.Setup(x => x.GetProductListAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new ResponseData<ListModel<Web_153504_Bagrovets.Domain.Entities.Product>> { Success = false });

        //    var controller = new Web_153504_Bagrovets_Lab1.Controllers.Product(productServiceMock.Object, categoryServiceMock.Object); // Инициализируйте контроллер

        //    // Act
        //    var result = await controller.Index("machine", 2);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}

        //[Fact]
        //public async Task Index_ReturnsViewWithCategoriesAndModel_WhenSuccessful()
        //{
        //    var categoryServiceMock = new Mock<ICategoryService>();
        //    var productServiceMock = new Mock<IProductService>();

        //    categoryServiceMock.Setup(x => x.GetCategoryListAsync()).ReturnsAsync(new ResponseData<List<Category>> { Success = true, Data = new List<Category>() });
        //    productServiceMock.Setup(x => x.GetProductListAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new ResponseData<ListModel<Web_153504_Bagrovets.Domain.Entities.Product>> { Success = true, Data = new ListModel<Web_153504_Bagrovets.Domain.Entities.Product>() });

        //    var controller = new Web_153504_Bagrovets_Lab1.Controllers.Product(productServiceMock.Object, categoryServiceMock.Object);

        //    var result = await controller.Index("technic", 1);

        //    Assert.IsType<ViewResult>(result);
        //    var viewResult = (ViewResult)result;

        //    Assert.NotNull(viewResult.ViewData["categories"]);
        //    Assert.Equal("technic", viewResult.ViewData["category"]);

        //    Assert.NotNull(viewResult.Model);
        //    Assert.IsType<ListModel<Web_153504_Bagrovets.Domain.Entities.Product>>(viewResult.Model);
        //}

        //[Fact]
        //public void ServiceReturnsFirstPageOfThreeItems()
        //{
        //    using var context = CreateContext();
        //    var service = new ProductServise(context, null, null);
        //    var result = service.GetProductListAsync(null).Result;
        //    Assert.IsType<ResponseData<ListModel<Web_153504_Bagrovets.Domain.Entities.Product>>>(result);
        //    Assert.True(result.Success);
        //    Assert.Equal(1, result.Data.CurrentPage);
        //    Assert.Equal(3, result.Data.Items.Count);
        //    Assert.Equal(2, result.Data.TotalPages);
        //    Assert.Equal(context.Dishes.First(), result.Data.Items[0]);
        //}
    }
}