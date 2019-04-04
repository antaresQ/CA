using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _13AShopCart.Models;
using System.Data.SqlClient;

namespace _13AShopCart.DB
{
    public class PurchaseData : Data
    {
        public static List<Purchase> GetPurchaseHistory()
        {
            List<Purchase> purchases = new List<Purchase>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT Purchase.PurchaseId as PurchaseId, Purchase.UserId as UserId,
                                        Purchase.Date as Date, Purchase.ProductId as ProductId,
                                        Purchase.Quantity as Qty, Purchase.Code as Code
                                        FROM Purchase";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Purchase purchase = new Purchase()
                    {
                        PurchaseId = (int)reader["PurchaseId"],
                        UserId = (int)reader["UserId"],
                        Date = (Int64)reader["Date"],
                        ProductId = (int)reader["ProductId"],
                        Qty = (int)reader["Qty"],
                        Code = (string)reader["Code"]
                    };
                    purchases.Add(purchase);
                };

                foreach (Purchase purchase in purchases)
                {
                    // To pull out Product data from Product DB
                }
            };
            return purchases;
        }
    }
}