using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Models
{
    public class CartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDecription { get; set; }
        public float Price { get; set; }
        public int Qty { get; set; }
        public float Total { get; set; }
    }
}