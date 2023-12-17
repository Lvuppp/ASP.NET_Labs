
namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Index_ReturnsNotFound_WhenCategoryServiceFails()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetCategoryListAsync()).ReturnsAsync(new ResponseData<List<Category>> { Success = false });

            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.GetProductListAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new ResponseData<ListModel<Web_153504_Bagrovets.Domain.Entities.Product>> { Success = false });

            var controller = new Web_153504_Bagrovets_Lab1.Controllers.Product(productServiceMock.Object, categoryServiceMock.Object); // Инициализируйте контроллер

            // Act
            var result = controller.Index("oifjksnkdflk", 2).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}