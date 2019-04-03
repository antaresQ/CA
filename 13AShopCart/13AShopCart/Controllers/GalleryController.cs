using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _13AShopCart.Models;
using _13AShopCart.DB;
using _13AShopCart.Controllers;

namespace _13AShopCart.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery

        public ActionResult Index()
        {
            List<Product> items = ProductData.GetProducts();

            string sessionId = Guid.NewGuid().ToString();
            string cartId = Guid.NewGuid().ToString();

            double itemCount = items.Count;
            double rows = Math.Ceiling(itemCount / 3);

            ViewData["itemCount"] = itemCount;
            ViewData["rows"] = rows;

            ViewData["sessionId"] = sessionId;
            ViewData["cartId"] = cartId;
            ViewData["products"] = items;

            return View();
        }

        public ActionResult Add(int Id)
        {
            Product item = ProductData.GetProductByProductId(Id);
            
            return RedirectToAction("Cart", "Add");
        }
    }
}