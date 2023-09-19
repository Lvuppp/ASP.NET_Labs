using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Services.CategoryServices;
using Web_153504_Bagrovets_Lab1.Services.ProductSevices;

namespace Web_153504_Bagrovets_Lab1.Pages.ProductAdmin
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public EditModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var products = (await _productService.GetProductListAsync(null)).Data.Items;

            if (id == null || products == null)
            {
                return NotFound();
            }

            var product =  products.FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            var categories = (await _categoryService.GetCategoryListAsync()).Data;

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                await _productService.UpdateProductAsync(Product.Id, Product, Image);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
          return (_productService.GetProductListAsync(null).
                Result.Data.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
