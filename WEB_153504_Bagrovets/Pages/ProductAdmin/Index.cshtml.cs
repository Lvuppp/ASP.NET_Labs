using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;
using Web_153504_Bagrovets_Lab1.Services.ProductSevices;

namespace Web_153504_Bagrovets_Lab1.Pages.ProductAdmin
{
    public class IndexModel : PageModel
    {
        private  IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<Product> Product { get; set; } = default!;
        //public ListModel ListModel { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var products = (await _productService.GetProductListAsync(null)).Data.Items;

            if (products != null)
            {
                Product = products;
            }
        }
    }
}
