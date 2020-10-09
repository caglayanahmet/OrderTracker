using System;
using System.ComponentModel.DataAnnotations;

namespace OrderTracker.Models.ViewModels
{
    public class DeliveryInfoViewModel
    {
        public int? Id { get; set; }
        public int Seller { get; set; }
        public int Product { get; set; }

        [Required(ErrorMessage = "Please choose a date!")]
        public DateTime? StartDeliveryDate { get; set; }
        [Required(ErrorMessage = "Please choose a date")]
        public DateTime? EndDeliveryDate { get; set; }
    }
}
