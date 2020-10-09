using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OrderTracker.Models.ViewModels
{
    public class DisplayDeliveryInfoViewModel:DeliveryInfoViewModel
    {
        public IEnumerable<DisplayDeliveryInfoViewModelItem> Items { get; set; }
    }

    public class DisplayDeliveryInfoViewModelItem
    {
        public int Id { get; set; }
        public string Seller { get; set; }
        public string Product { get; set; }
        public string StartWeek { get; set; }
        public string EndWeek { get; set; }
        public int DayDiff { get; set; }
        public string DayColor { get; set; }
        public int WeekDiff { get; set; }
        public string WeekColor { get; set; }
    }
}
