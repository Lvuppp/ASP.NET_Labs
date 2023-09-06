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
    public class ProductController : ControllerBase
    {
        private IProductService _productControllerService;
        public ProductController(IProductService ProductControllerService) {
            _productControllerService = ProductControllerService;
        }

        [HttpGet]
        // GET: api/<ProductController>
        public async Task<ActionResult<ResponseData<List<Product>>>> Get(string? category,int pageNo = 1, int pageSize = 3) {
            return Ok(await _productControllerService.GetProductListAsync(
            category,
            pageNo,
            pageSize));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
