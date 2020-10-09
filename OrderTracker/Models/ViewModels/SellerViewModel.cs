using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace OrderTracker.Models.ViewModels
{
    public class SellerViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage ="Please type a seller name!")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please type a seller country!")]
        [StringLength(50)]
        public string Country { get; set; }
    }
}
