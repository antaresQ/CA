using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using _13AShopCart.DB;
using _13AShopCart.Models;

    public class CartData : Data
    {
        public static bool IsActiveSessionId(string sessionId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) FROM User1
                    WHERE sessionId = '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                int count = (int)cmd.ExecuteScalar();
                return (count == 1);
            }
        }

        public static List<CartItem> SetCart()
        {
        List<CartItem> cartitems = new List<CartItem>();

            CartItem c1 = new CartItem();
            c1.ProductId = 101;
            c1.ProductName = ".NET Charts";
            c1.ProductDescription = "Brings powerful charting capabilities to your .NET applications";
            c1.Price = 99;
            c1.Qty = 2;

            CartItem c2 = new CartItem();
            c2.ProductId = 102;
            c2.ProductName = ".NET Numerics";
            c2.ProductDescription = "Powerful numerical methods for your .NET simulations";
            c2.Price = 199;
            c2.Qty = 2;

            cartitems.Add(c1);
            cartitems.Add(c2);

            return cartitems;
        }

        public static List<CartItem> GetCartDetailsByCartId(int cartId)
        {
            List<CartItem> items = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT Product.Name as ProductName, Product.Description, Product.ProductId
                    Cart.Qty, Product.Price
                        FROM Cart, Product
                            WHERE Cart.ProductId = Product.ProductId
                                AND Cart.Id = " + cartId;
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                CartItem item = new CartItem();
                if (reader.Read())
                {
                    
                    {
                        item.ProductId = (int)reader["ProductId"];
                        item.ProductName = (string)reader["ProductName"];
                        item.ProductDescription = (string)reader["Description"];
                        item.Price = (int)reader["Price"];
                        item.Qty = (int)reader["Qty"];
                    };
                    items.Add(item);
                };
            }
            return items;
        }

        public static void WriteItemsToPurchase(List<CartItem> items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT LAST_INSERT_ID() From Purchase";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                int LastPurchaseID=0;
                if (reader.Read())
                {

                    {
                        LastPurchaseID = (int)reader["ProductId"];
                    };
                };
                int purchaseID = LastPurchaseID + 1;
                sql = @"INSERT INTO Purchase
                        VALUES purchaseID, items.ProductID,items.Qty";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
            }
        }

    }

