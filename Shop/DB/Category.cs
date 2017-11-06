using System.Collections.Generic;

namespace Shop.DB
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoLink { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
