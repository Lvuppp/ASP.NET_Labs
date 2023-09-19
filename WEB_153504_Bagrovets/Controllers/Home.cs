using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_153504_Bagrovets_Lab1.Controllers;
using Web_153504_Bagrovets_Lab1.Entities;

namespace Web_153504_Bagrovets.Controllers
{
    public class Home : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<ListData> products = new List<ListData>
            {
                new ListData { Id = 1, Name = "Продукт 1" },
                new ListData { Id = 2, Name = "Продукт 2" },
                new ListData { Id = 3, Name = "Продукт 3" }
            };

            SelectList productList = new SelectList(products, "Id", "Name");

            ViewData["ItemList"] = productList;
            ViewData["LabName"] = "Лабараторная работа№2";
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
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

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
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

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
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
