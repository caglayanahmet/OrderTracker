using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTracker.Models.ViewModels
{
    public class DisplayProductsViewModel
    {
        public IEnumerable<ProductViewModel> Items { get; set; }
    }
}
