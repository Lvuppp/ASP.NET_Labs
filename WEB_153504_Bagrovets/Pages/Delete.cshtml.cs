using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_153504_Bagrovets.API.Data;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Services.ProductSevices;

namespace Web_153504_Bagrovets_Lab1.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;   
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var products = (await _productService.GetProductListAsync(null)).Data.Items;

            if (id == null || products == null)
            {
                return NotFound();
            }

            var product = products.FirstOrDefault(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var products = (await _productService.GetProductListAsync(null)).Data.Items;

            if (id == null || products == null)
            {
                return NotFound();
            }
            var product = products.Find(p => p.Id == id);

            if (product != null)
            {
                Product = product;
                await _productService.DeleteProductAsync((int)id);
            }

            return RedirectToPage("./Index");
        }
    }
}
