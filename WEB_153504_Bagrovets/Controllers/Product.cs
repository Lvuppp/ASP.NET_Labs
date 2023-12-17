using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Extension;
using Web_153504_Bagrovets_Lab1.Services;
using Web_153504_Bagrovets_Lab1.Services.CategoryServices;
using Web_153504_Bagrovets_Lab1.Services.ProductSevices;

namespace Web_153504_Bagrovets_Lab1.Controllers
{
    public class Product : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        // GET: Product
        public Product(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [Route("Product")]
        [Route("Product/{category}")]
        public async Task<ActionResult> Index(string category, int pageNo)
        {
            var productResponse = await _productService.GetProductListAsync(
                category, pageNo);
            
            if (!productResponse.Success)
                return NotFound(productResponse.ErrorMessage);

            var categoriesResponse = (await _categoryService.GetCategoryListAsync()).Data;

            if (categoriesResponse.Count == 0)
                return NotFound(productResponse.ErrorMessage);

            ViewData["categories"] = categoriesResponse;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CatalogPartial", productResponse.Data);
            }

            return View(productResponse.Data);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
