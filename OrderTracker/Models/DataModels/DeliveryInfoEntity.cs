using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTracker.Models.DataModels
{
    public class DeliveryInfoEntity
    {
        [Key]
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public DateTime StartDeliveryDate { get; set; }
        public DateTime EndDeliveryDate { get; set; }
    }
}
