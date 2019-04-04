using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _13AShopCart.Models;
using _13AShopCart.DB;


    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        /*public ActionResult ViewCart(List<CartItem> c)
        {
            List<CartItem> items = CartItem.GetCart();

            ViewData["Items"] = items;
            return View();
        }*/
        public ActionResult ViewCart()
        {
            List<CartItem> items = CartData.SetCart();

            ViewData["Items"] = items;
            return View();
        }

        /*public ActionResult ViewCart(int cartID)
        {
            List<CartItem> items = CartData.GetCartDetailsByCartId(cartID);

            ViewData["Items"] = items;
            return View();
        }*/
        public ActionResult Checkout(int cartID)
        {
            List<CartItem> items = CartData.GetCartDetailsByCartId(cartID);
            CartData.WriteItemsToPurchase(items);
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
    }
