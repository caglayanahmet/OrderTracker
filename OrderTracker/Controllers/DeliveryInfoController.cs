using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrderTracker.Models;
using OrderTracker.Services;
using OrderTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace OrderTracker.Controllers
{
    public class DeliveryInfoController : Controller
    {
        private readonly OrderTrackerTaskService _service;

        public DeliveryInfoController(OrderTrackerTaskService service)
        {
            _service = service;
        }

        public IActionResult Index(DateTime? dateFilter)
        {
            var result = _service.GetDeliveryInfoItems(dateFilter);
            return View(new DisplayDeliveryInfoViewModel
            {
                Items = result,
            });
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var model = id != null
                ? new EditDeliveryInfoViewModel(_service.GetDeliveryInfoItem(id.Value))
                : new EditDeliveryInfoViewModel();

            model.Products = _service.GetProductItems().Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            model.Sellers = _service.GetSellerItems().Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View(model);
        }

        [HttpPost]
        public IActionResult Save(EditDeliveryInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                _service.AddUpdateDeliveryInfo(model);
                return RedirectToAction("Index");
            }
            model.Products = _service.GetProductItems().Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            model.Sellers = _service.GetSellerItems().Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View("Edit", model);
        }

        public IActionResult Delete(int? id)
        {
            _service.DeleteDeliveryInfoItem(id.Value);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
