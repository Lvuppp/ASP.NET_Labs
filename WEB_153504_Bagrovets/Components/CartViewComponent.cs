using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Web_153504_Bagrovets_Lab1.Entities;

namespace Web_153504_Bagrovets_Lab1.Views.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(double price)
        {
            ViewData["price"] = price; 
            return View();
        }
    }
}
