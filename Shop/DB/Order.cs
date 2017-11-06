using System;
using System.Collections.Generic;

namespace Shop.DB
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CompanyName { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
    }
}