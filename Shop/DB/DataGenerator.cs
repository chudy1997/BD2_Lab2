using System.Collections.Generic;
using System.Linq;

namespace Shop.DB
{
    public class DataGenerator
    {
        private static HashSet<Category> categories;
        private HashSet<Product> products;
        private static HashSet<Customer> customers;

        private int GetCategoryId(string name)
        {
            using (var db = new ShopContext())
            {
                return db.Categories.FirstOrDefault(c => c.Name == name).CategoryId;
            }
        }

        private static IEnumerable<Category> GetCategories(int count = int.MaxValue)
        {
            return categories.Take(count);
        }

        private void CreateCategories()
        {
            using (var db = new ShopContext())
            {
                categories = new HashSet<Category>
                {
                    new Category { Name="Jedzenie", Description = "Pieczywo, nabiał, słodycze...", PhotoLink = "https://medichouse.pl/wp-content/uploads/2016/08/jedzenie-kompulsywne-1140x893.png"},
                    new Category { Name="Napoje", Description="Woda, soki...", PhotoLink = "http://beta.interesariusze.pl/wp-content/uploads/2013/05/Nowy-obraz.jpg" },
                    new Category { Name="Chemia", Description="Środki czystości, środki higieniczne...", PhotoLink = "http://habson.pl/images/chemia.JPG"},
                    new Category { Name="Używki", Description="Alkohol, papierosy, kawa, herbata...", PhotoLink = "https://previews.123rf.com/images/icefront/icefront0801/icefront080100051/2407640-Three-dangerous-items-coffee-cigarette-smoking-and-alcohol-Stock-Photo.jpg" }
                };
                var existingCategories = db.Categories;
                db.Categories.RemoveRange(existingCategories);

                var newCategories = GetCategories(10);
                db.Categories.AddRange(newCategories);
                db.SaveChanges();
            }
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

        private IEnumerable<Product> GetProducts(int count = int.MaxValue)
        {
            return products.Take(count);
        }

        private static IEnumerable<Customer> GetCustomers(int count = int.MaxValue)
        {
            return customers.Take(count);
        }

        private void CreateProducts()
        {
            using (var db = new ShopContext())
            {
                products = new HashSet<Product>
                {
                    new Product { Name="Chleb razowy", UnitsInStock=10, CategoryId=GetCategoryId("Jedzenie"), UnitPrice=2.50M },
                    new Product { Name="Ser żółty 0.5 kg", UnitsInStock=9, CategoryId=GetCategoryId("Jedzenie"), UnitPrice=10.50M },
                    new Product { Name="Ciastko z jabłkami", UnitsInStock=11, CategoryId=GetCategoryId("Jedzenie"), UnitPrice=1.50M },
                    new Product { Name="Woda gazowana 0.5 l", UnitsInStock=12, CategoryId=GetCategoryId("Napoje"), UnitPrice=1.70M },
                    new Product { Name="Sok jabłkowy 0.5 l", UnitsInStock=8, CategoryId=GetCategoryId("Napoje"), UnitPrice=2.50M },
                    new Product { Name="Domestos", UnitsInStock=13, CategoryId=GetCategoryId("Chemia"), UnitPrice=7.50M },
                    new Product { Name="Dezodorant AXE", UnitsInStock=7, CategoryId=GetCategoryId("Chemia"), UnitPrice=6.50M },
                    new Product { Name="Wódka Finlandia 0.5 l", UnitsInStock=14, CategoryId=GetCategoryId("Używki"), UnitPrice=32.50M },
                    new Product { Name="Papierosy Marlboro", UnitsInStock=6, CategoryId=GetCategoryId("Używki"), UnitPrice=14.50M },
                    new Product { Name="Kawa Tchibo", UnitsInStock=15, CategoryId=GetCategoryId("Używki"), UnitPrice=17.50M },
                    new Product { Name="Herbata Minutka", UnitsInStock=5, CategoryId=GetCategoryId("Używki"), UnitPrice=4.30M },
                };
                var existingProducts = db.Products;
                db.Products.RemoveRange(existingProducts);

                var newProducts = GetProducts();
                db.Products.AddRange(newProducts);
                db.SaveChanges();
            }
        }

        private static void CreateCustomers()
        {
            using (var db = new ShopContext())
            {
                customers = new HashSet<Customer>
                {
                    new Customer { CompanyName = "Fakturopol", Description = "Open daily 7-17"}
                    
                };
                var existingCustomers = db.Customers;
                db.Customers.RemoveRange(existingCustomers);

                var newCustomers = GetCustomers();
                db.Customers.AddRange(newCustomers);
                db.SaveChanges();
            }
        }

        public void GenerateData()
        {
            CreateCategories();
            CreateProducts();
            CreateCustomers();
        }
    }
}
