using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTracker.Models.ViewModels
{
    public class EditDeliveryInfoViewModel: DeliveryInfoViewModel
    {
        public IEnumerable<SelectListItem> Sellers { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }

        public EditDeliveryInfoViewModel(DeliveryInfoViewModel model)
        {
            Id = model.Id;
            Seller = model.Seller;
            Product = model.Product;
            StartDeliveryDate = model.StartDeliveryDate;
            EndDeliveryDate = model.EndDeliveryDate;
        }
        public EditDeliveryInfoViewModel()
        {

        }
    } 
}
