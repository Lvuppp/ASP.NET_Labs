using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Web_153504_Bagrovets.Domain.Entities;

namespace Web_153504_Bagrovets_Lab1.Views.Components
{
    public class CartViewComponent : ViewComponent
    {
        public Cart Cart { get; set; }

        public CartViewComponent(Cart cart)
        {
            Cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(Cart);
        }
    }
}
