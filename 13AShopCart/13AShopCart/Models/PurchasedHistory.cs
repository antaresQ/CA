using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Models
{
    public class PurchasedHistory
    {
        public string UserID { get; set; }
        public DateTime Date { get; set; }
        public string ProductID { get; set; }
        public int Qty { get; set; }
        public string ActCode { get; set; }

    }
}