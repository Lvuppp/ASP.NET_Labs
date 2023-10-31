using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Web_153504_Bagrovets.API.Services.ProductServices;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_153504_Bagrovets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private IProductService _productControllerService;
        private string imagePath;
        private string _appUri;
        private ILogger<ProductController> _logger; 
        public ProductController(IProductService ProductControllerService,
            IWebHostEnvironment env,
            IConfiguration configuration,
            ILogger<ProductController> logger) 
        {
            _productControllerService = ProductControllerService;
            imagePath = Path.Combine(env.WebRootPath, "images");
            _appUri = configuration.GetSection("appUri").Value;
            _logger = logger;

        }

        [HttpGet]
        [AllowAnonymous]
        // GET: api/<ProductController>
        public async Task<ActionResult<ResponseData<List<Product>>>> Get(string? category,int pageNo, int pageSize) {
            return Ok(await _productControllerService.GetProductListAsync(
            category,
            pageNo,
            pageSize));
        }

        // POST: api/ProductController/5
        [HttpPost("{id}")]

        public async Task<ActionResult<ResponseData<string>>> PostImage(int id, IFormFile formFile) 
        {
            var response = await _productControllerService.SaveImageAsync(id, formFile);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]

        [AllowAnonymous]
        public async Task<ActionResult<ResponseData<Product>>> Get(int id)
        {
            return Ok(await _productControllerService.GetProductByIdAsync(id));
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ResponseData<Product>>> Post([FromBody] Product product)
        {
            return Ok(await _productControllerService.CreateProductAsync(product));
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task Put(int id,[FromBody] Product product)
        {
            await _productControllerService.UpdateProductAsync(id, product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _productControllerService.DeleteProductAsync(id);
        }
    }
}
