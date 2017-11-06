using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Shop.DB
{
    public class Utils
    {
        private ShopContext db;
        public Utils()
        {
            db = new ShopContext();
        }

        ~Utils()
        {
            db.Dispose();
        }

        public IEnumerable<Category> GetCategories()
        {
            return db.Categories;
        }

        public IEnumerable<Product> GetProductsForCategory(int id)
        {
            return db.Products.Where(p => p.CategoryId == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return db.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            return db.Products.SingleOrDefault(p => p.ProductId == id);
        }

        public Category GetCategoryForProduct(int id)
        {
            return db.Categories.Include(c => c.Products).FirstOrDefault(c => c.Products.Select(p => p.ProductId).Contains(id));
        }

        public void AddOrder(Dictionary<int, int> order, string customerName, bool status = false)
        {
            var orderProducts = new List<OrderProduct>();
            foreach (var product in db.Products.AsEnumerable())
            {
                if (order.ContainsKey(product.ProductId) && order[product.ProductId] <= product.UnitsInStock)
                    orderProducts.Add(new OrderProduct() { ProductId = product.ProductId, Quantity = order[product.ProductId], Cost = product.UnitPrice * order[product.ProductId] });
            }

            if (orderProducts.Count == 0)
                return;

            var newOrder = new Order() { CompanyName = customerName, Status = status, Date = DateTime.UtcNow };
            db.Orders.Add(newOrder);
            db.OrderProduct.AddRange(orderProducts);

            var products = db.Products.Where(p => order.Keys.ToList().Any(k => k == p.ProductId));
            foreach (var prod in products)
                prod.UnitsInStock = db.Products.First(p => p.ProductId == prod.ProductId).UnitsInStock - order[prod.ProductId];

            db.SaveChanges();
        }

        public void ConfirmBasket(string companyName)
        {
            var orders = from o in db.Orders
                         where o.CompanyName == companyName
                         select o;

            foreach (var o in orders)
                o.Status = true;

            db.SaveChanges();
        }

        public void DiscardBasket(string companyName)
        {
            var orders = from o in db.Orders
                         where o.CompanyName == companyName && o.Status == false
                         select o;

            db.Orders.RemoveRange(orders);
            db.SaveChanges();
        }

        public IEnumerable<OrderInfo> GetBasket(string companyName)
        {
            return db.Orders.Where(o => o.CompanyName == companyName && !o.Status)
                .Select(o => new OrderInfo
                {
                    OrderId = o.OrderId,
                    Date = o.Date,
                    ProductInfos = db.Orders
                        .Where(a => a.CompanyName == companyName && a.OrderId == o.OrderId)
                        .SelectMany(a => a.OrderProducts)
                        .Join(db.Products,
                            a => a.ProductId,
                            p => p.ProductId,
                            (a, p) => new OrderInfo.ProductInfo { Product = p, Cost = a.Cost, Quantity = a.Quantity })
                });
        }

        public IEnumerable<OrderInfo> GetOrders(string companyName)
        {
            return db.Orders.Where(o => o.CompanyName == companyName && o.Status)
                .Select(o => new OrderInfo
                {
                    OrderId = o.OrderId,
                    Date = o.Date,
                    ProductInfos = db.Orders
                        .Where(a => a.CompanyName == companyName && a.OrderId == o.OrderId)
                        .SelectMany(a => a.OrderProducts)
                        .Join(db.Products,
                            a => a.ProductId,
                            p => p.ProductId,
                            (a, p) => new OrderInfo.ProductInfo { Product = p, Cost = a.Cost, Quantity = a.Quantity })
                });
        }
    }
}
