using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using _13AShopCart.DB;
using _13AShopCart.Models;
using System.Diagnostics;

namespace _13AShopCart.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string Username, string Password)
        {
            if (Username == null) return View();

            User user = UserData.GetUserByUsername(Username);
            if (user == null) return View();
            if (user.Password != Password)
            return View();

            string sessionId = SessionData.CreateSession(user.UserId);
            return RedirectToAction("Products", "Login", new { sessionId });
        }

        public ActionResult Products()
        {
            return View();
        }
    }
}