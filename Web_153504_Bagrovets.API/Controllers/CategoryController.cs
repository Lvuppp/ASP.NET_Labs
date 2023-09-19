using Microsoft.AspNetCore.Mvc;
using Web_153504_Bagrovets.API.Data.CategoryServices;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_153504_Bagrovets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService=categoryService;
        }

        // GET: api/<Category>
        [HttpGet]
        public async Task<ActionResult<ResponseData<List<Category>>>> Get()
        {
            return Ok(await _categoryService.GetCategoryListAsync());
        }
        // GET api/<Category>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Category>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Category>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Category>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
