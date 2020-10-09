using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTracker.Models.ViewModels
{
    public class ProductViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage ="Please type a product name!")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
