using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public int CartId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}