using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _13AShopCart.Models;
using System.Data.SqlClient;

namespace _13AShopCart.DB
{
    public class ProductData : Data
    {
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT *
                                FROM Product";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Id = (int)reader["ProductId"],
                        Name = (string)reader["ProductName"],
                        Description = (string)reader["Description"],
                        Price = (float)reader["Price"],
                        Qty = (int)reader["Qty"]

                    };

                    products.Add(product);
                }
            }

            return products;
        }

        public static List<Product> GetProductByProductId (int ProductId)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT Product.Id as ProductId, Product.Name, Product.Description, 
                                Product.Price, Product.Quantity
                                FROM Product";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Id = (int)reader["ProductId"],
                        Name = (string)reader["ProductName"],
                        Description = (string)reader["Description"],
                        Price = (float)reader["Price"],
                        Qty = (int)reader["Qty"]

                    };

                    products.Add(product);
                }
            }

            return products;
        }

    }
}