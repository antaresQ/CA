using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _13AShopCart.Models;
using _13AShopCart.DB;

namespace _13AShopCart.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        /*public ActionResult Index()
        {
            List<CartItem> items = CartData.SetCart();

            ViewData["Items"] = items;
            return View();
        }*/

        /*public ActionResult ViewCart(List<CartItem> c)
        {
            List<CartItem> items = CartItem.GetCart();

            ViewData["Items"] = items;
            return View();
        }*/
        /*public ActionResult ViewCart(List<CartItem> cartItems)
        {
            List<CartItem> items = CartData.SetCart(cartItems);

            ViewData["Items"] = items;
            return View();
        }*/

        /*public ActionResult ViewCart()
        {
            List<CartItem> items = CartData.SetCart();

            ViewData["Items"] = items;
            return View();
        }*/

        public ActionResult Index(int cartID)
        {
            List<CartItem> items = CartData.GetCartDetailsByCartId(cartID);

            ViewData["Items"] = items;
            ViewData["cartID"] = cartID;
            return View();
        }
        public ActionResult UpdateQty(int cartID, int pid, int qty)
        {
            CartData.UpdatePurchase(cartID,pid,qty);
            return null;

        }
        public ActionResult Checkout(int cartID, int qty)
        {
            List<CartItem> items = CartData.GetCartDetailsByCartId(cartID);
            CartData.WriteItemsToPurchase(items,cartID);
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
    }
}
