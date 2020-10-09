using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderTracker.Models;
using OrderTracker.Models.ViewModels;
using OrderTracker.Services;
using System.Diagnostics;

namespace OrderTracker.Controllers
{
    public class SellerController : Controller
    {
        private readonly OrderTrackerTaskService _service;

        public SellerController(OrderTrackerTaskService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var result = _service.GetSellerItems();
            var item = new DisplaySellersViewModel
            {
                Items = result
            };
            return View(item);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id!=null)
            {
                var item = _service.GetSellerItem(id.Value);
                return View(item);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Save(SellerViewModel item)
        {
            if (ModelState.IsValid)
            {
                _service.AddUpdateSeller(item);
                return RedirectToAction("Index");
            }
            return View("Edit");
        }
        public IActionResult Delete(int? id)
        {
            _service.DeleteSellerItem(id.Value);
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}