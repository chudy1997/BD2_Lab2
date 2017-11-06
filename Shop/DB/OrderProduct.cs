using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.DB
{
    public class OrderProduct
    {
        public int OrderProductId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }
    }
}