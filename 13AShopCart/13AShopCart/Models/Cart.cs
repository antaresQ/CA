using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Models
{
    public class Cart
    {
        public string UserID { get; set; }
        public string CartID { get; set; }
        public string ProductName { get; set; }
        public string ProductID { get; set; }
        public int Qty { get; set; }
    }
}