using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OrderTracker.Models;
using OrderTracker.Models.ViewModels;
using OrderTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OrderTracker.Controllers
{
    public class ProductController : Controller
    {
        private readonly OrderTrackerTaskService _service;

        public ProductController(OrderTrackerTaskService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var result = _service.GetProductItems();
            return View(new DisplayProductsViewModel
            {
                Items = result
            });
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id!=null)
            {
                var item = _service.GetProductItem(id.Value);
                return View(item);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Save(ProductViewModel item)
        {
            if (ModelState.IsValid)
            {
                _service.AddUpdateProduct(item);
                return RedirectToAction("Index");
            }
            return View("Edit");
        }
        public IActionResult Delete(int? id)
        {
            _service.DeleteProductItem(id.Value);
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}