using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_153504_Bagrovets_Lab1.Extension;
using Web_153504_Bagrovets_Lab1.Services.ProductSevices;

namespace Web_153504_Bagrovets_Lab1.Controllers
{
    public class Cart : Controller
    {
        // GET: Cart
        private IProductService _productService;
        private Web_153504_Bagrovets.Domain.Entities.Cart _cart;
        public Cart(IProductService productService, Web_153504_Bagrovets.Domain.Entities.Cart cart)
        {
            _productService = productService;
            _cart = cart;
        }

        [Authorize]
        [Route("[controller]/add/{id:int}")]
        public async Task<ActionResult> Add(int id, string returnUrl)
        {
            var cart = HttpContext.Session.Get<Web_153504_Bagrovets.Domain.Entities.Cart>("cart") ?? new();
            var data = await _productService.GetProductByIdAsync(id);
            if (data.Success)
            {
                _cart.AddItem(data.Data, 1);
                HttpContext.Session.Set("cart", _cart);
            }
            return Redirect(returnUrl);
        }

        [Authorize]
        public async Task<ActionResult> RemoveItem(int id, string returnUrl)
        {
            var data = await _productService.GetProductByIdAsync(id);
            if (data.Success)
            {
                _cart.RemoveItem(data.Data);
                HttpContext.Session.Set("cart", _cart);
            }
            return Redirect(returnUrl);
        }
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CartPartial", _cart);
            }

            return View(_cart);
        }


    }
}
