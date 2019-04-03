using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _13AShopCart.Models;
using _13AShopCart.DB;

namespace _13AShopCart.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery

        public ActionResult Index()
        {
            List<Product> products = ProductData.GetProducts();

            string sessionId = Guid.NewGuid().ToString();
            string cartId = Guid.NewGuid().ToString();

            double itemCount = products.Count;
            double rows = Math.Ceiling(itemCount / 3);

            ViewData["itemCount"] = itemCount;
            ViewData["rows"] = rows;

            ViewData["sessionId"] = sessionId;
            ViewData["cartId"] = cartId;
            ViewData["products"] = products;

            return View();
        }
    }
}