using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Models
{
    public class PurchasedHistory
    {
        public int PurchaseId { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public int ProductID { get; set; }
        public int Qty { get; set; }

    }
}