using Shop.DB;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private Utils dbHelper;

        public HomeController(){
            dbHelper = new Utils();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Categories()
        {
            var categories = dbHelper.GetCategories(); 
            return View(categories);
        }

        public ActionResult Products(int categoryId = -1)
        {
            var products = dbHelper.GetProducts();
            if (categoryId != -1)
                products = dbHelper.GetProductsForCategory(categoryId);

            return View(products);
        }

        [HttpPost]
        public void AddToBasket(Dictionary<int, int> orders)
        {
            dbHelper.AddOrder(orders, "Fakturopol");
        }

        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult Basket()
        {
            var productsWithQuantity = dbHelper.GetBasket("Fakturopol").ToArray();

            return View(productsWithQuantity);
        }

        public ActionResult Orders()
        {
            var productsWithQuantity = dbHelper.GetOrders("Fakturopol").ToArray();

            return View(productsWithQuantity);
        }

        [HttpPost]
        public void DiscardBasket()
        {
            dbHelper.DiscardBasket("Fakturopol");
        }

        [HttpPost]
        public void ConfirmBasket()
        {
            dbHelper.ConfirmBasket("Fakturopol");
        }
    }
}
