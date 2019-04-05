using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using _13AShopCart.DB;
using _13AShopCart.Models;
using System.Data;
using System.Diagnostics;
using _13AShopCart.Util;

namespace _13AShopCart.DB
{
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
			List<CartItem> items = new List<CartItem>();

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();

				string sql = @"SELECT p.Id as ProductId,p.Name as ProductName,p.Description as ProductInfo, p.ProductImage as ImgUrl,p.price as ProductPrice, c.Quantity as Qty
										from Product as p,Cart as c
										where c.ProductId=p.Id AND c.Id=" + cartId;
				SqlCommand cmd = new SqlCommand(sql, conn);

				SqlDataReader reader = cmd.ExecuteReader();
				CartItem item = new CartItem();
				while (reader.Read())
				{
					items.Add(new CartItem() {
						ProductId = (int)reader["ProductId"],
						ProductName = (string)reader["ProductName"],
						ProductDescription = (string)reader["ProductInfo"],
						Price = (Double)reader["ProductPrice"],
						Qty = (int)reader["Qty"],
						ImgUrl = (string)reader["ImgUrl"]
					});        
					
				};
				return items;
			}
			
		}

		public static void WriteItemsToPurchase(List<CartItem> items , int cartID)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();

				// Getting the last purchaseID from Purchase table
				string sql = @"select top 1 PurchaseId as LastPurchaseID from Purchase order by PurchaseId desc ";
				SqlCommand cmd = new SqlCommand(sql, conn);
				SqlDataReader reader = cmd.ExecuteReader();
				int LastPurchaseID=0;
				if (reader.Read())
				{

					{
						LastPurchaseID = (int)reader["LastPurchaseID"];
					};
				};
				int purchaseID = LastPurchaseID + 1;
				reader.Close();

				string sql2 = @"select UserId from Cart where Id='"+cartID+"'";
				cmd = new SqlCommand(sql2, conn);
				SqlDataReader reader2 = cmd.ExecuteReader();
				int UserID = 0;
				if (reader2.Read())
				{

					{
						UserID = (int)reader2["UserId"];
					};
				};

				reader2.Close();
				// Inserting the purchase detail
				// Run this procedure in database
				/*Create procedure [dbo].[AddNewPurchase]  
					(  
					   @PurchaseID int,  
					   @UserID int,  
					   @Date bigint,
					   @ProductID int,  
					   @Quantity int,  
					   @Code varchar (MAX)   
					)  
					as  
					begin  
					   Insert into Purchase values(@PurchaseID,@UserID,@Date,@ProductID,@Quantity,@Code)  
					End 
				 */
				long timestamp;
				foreach (var item in items)
				{
					timestamp = Timestamp.unixTimestamp();
					SqlCommand com = new SqlCommand("AddNewPurchase", conn);
					com.CommandType = CommandType.StoredProcedure;
					com.Parameters.AddWithValue("@PurchaseID", purchaseID);
					com.Parameters.AddWithValue("@UserID",UserID);
					com.Parameters.AddWithValue("@Date", timestamp);
					com.Parameters.AddWithValue("@ProductID", item.ProductId);
					com.Parameters.AddWithValue("@Quantity", item.Qty);
					string s = "";
					string[] str=new string[item.Qty];
					for(int x=0; x<item.Qty; x++)
					{
						string sessionId = Guid.NewGuid().ToString();
						sessionId = string.Concat(sessionId, ",");
						str[x] = sessionId;
						Debug.WriteLine(s);
						
					}
					s=string.Concat(str);
					Debug.WriteLine(s);
					com.Parameters.AddWithValue("@Code", s);
					int i = com.ExecuteNonQuery();
				}                
				conn.Close();
			}
		}

		public static void UpdatePurchase(int cartID, int pid, int qty)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();

				// Getting the last purchaseID from Purchase table
				string sql = @"update Cart set Quantity='" + qty + "'" + " where id='" + cartID + "'" + " and ProductId='" + pid + "'";
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.ExecuteNonQuery();

				conn.Close();
			}
		}

    }
}
