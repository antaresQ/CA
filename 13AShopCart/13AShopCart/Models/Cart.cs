using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Models
{
    public class Cart
    {
        public int UserID { get; set; }
        public int CartID { get; set; }
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public int Qty { get; set; }
    }
}