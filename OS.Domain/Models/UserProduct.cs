using OS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OS.Domain.Models
{
    public class UserProduct : EmptyTable
    {
        public Product? Product { get; set; }
        public User? User { get; set; }
        public DateTime? SelectedAt { get; set; }
        public bool? IsBought { get; set; }
        public DateTime? BoughtAt { get; set; }
        public double? BoughtMoney { get; set; }
        public bool? IsDelivery { get; set; }
        public DateTime? DeliveryAt { get; set; }
    }
}
