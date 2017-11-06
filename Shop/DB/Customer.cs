using System.ComponentModel.DataAnnotations;

namespace Shop.DB
{
    public class Customer
    {
        [Key]
        public string CompanyName { get; set; }

        public string Description { get; set; }
    }
}
