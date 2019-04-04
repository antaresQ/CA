using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int UserId { get; set; }
        public Int64 Date { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public string Code { get; set; }

    }
}