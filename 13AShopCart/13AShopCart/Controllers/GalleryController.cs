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
        int cartId;

        public ActionResult Index(string sessionId, int? cartId)
        {
            Session["browserSession"] = HttpContext.Session.SessionID;
            if (sessionId == null)  //if(sessionId == null)
            {
                sessionId = Guid.NewGuid().ToString();
                //Session["sessionId"] = HttpContext.Session.SessionID;
                //sessionId = Session["sessionId"].ToString();
            }

            List<Product> items = ProductData.GetProducts();
            double itemCount = items.Count;
            double rows = Math.Ceiling(itemCount / 3);

            ViewData["itemCount"] = itemCount;
            ViewData["rows"] = rows;

            ViewData["sessionId"] = sessionId;
            ViewData["cartId"] = cartId;
            ViewData["products"] = items;

            return View();
        }



        public ActionResult ViewCart()
        {

            return RedirectToAction("Cart", "ViewCart");
        }

        public ActionResult Add(int ProductId, int? cartId, string sessionId)
        {
            cartId = CartData.AddtoCart(ProductId, cartId);


            return RedirectToAction("Index", new {sessionId = sessionId, cartId = cartId});
        }
    }
}