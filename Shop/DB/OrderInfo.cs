using System;
using System.Collections.Generic;

namespace Shop.DB
{
    public class OrderInfo
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<ProductInfo> ProductInfos { get; set; }

        public class ProductInfo
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
            public decimal Cost { get; set; }
        }
    }
}