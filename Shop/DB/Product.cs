using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.DB
{

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int UnitsInStock { get; set; }
        public int CategoryId { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
    }
}
