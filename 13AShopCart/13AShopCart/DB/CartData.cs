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
                        item.Price = (double)reader["Price"];
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

        public static int CreateCart()
        {
            int cartId = 0;
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT MAX(Cart.Id) as CartId FROM Cart";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    cartId = (int)reader["CartId"];
                }
                conn.Close();

                cartId++;

                conn.Open();
                string sql2 = @"INSERT INTO Cart (Id) VALUES (" + cartId + @")";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.ExecuteNonQuery();

                conn.Close();
            }

            return cartId;
        }
        
        public static int AddtoCart(int ProductId, int? cartId)
        {
            
            if (cartId == null)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT MAX(Cart.Id) as CartId FROM Cart";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cartId = (int)reader["CartId"];
                    }
                    conn.Close();

                    cartId++;

                    conn.Open();
                    string sqlAddNew = @"INSERT INTO Cart (Id, ProductId, Quantity) VALUES (" + cartId + @", " + ProductId + @", 1)";
                    SqlCommand cmd2 = new SqlCommand(sqlAddNew, conn);
                    cmd2.ExecuteNonQuery();

                    conn.Close();
                }
            }

            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    int idMatch = 0;
                    conn.Open();
                    string sql = @"SELECT Cart.ProductId as CartProductId, Cart.Quantity FROM Cart WHERE Cart.Id =" + cartId;
                    
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if(ProductId == (int)reader["CartProductId"])
                        {
                            idMatch = ProductId;
                        }
                    }
                    conn.Close();

                    conn.Open();

                    if (idMatch != 0)
                    {
                        string sqlIncrement = @"Update Cart Set Cart.Quantity = Cart.Quantity + 1 WHERE Cart.Id =" + cartId + @" AND Cart.ProductId =" + idMatch;
                        SqlCommand cmd2 = new SqlCommand(sqlIncrement, conn);
                        cmd2.ExecuteNonQuery();
                    }

                    else
                    {
                        string sqlAddNew = @"INSERT INTO Cart (Id, ProductId, Quantity) VALUES (" + cartId + @"," + ProductId + @", 1)";
                        SqlCommand cmd3 = new SqlCommand(sqlAddNew, conn);
                        cmd3.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            
            

            return (int) cartId;
        }

    }

