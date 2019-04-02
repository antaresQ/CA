using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using _13AShopCart.Models;

namespace _13AShopCart.DB
{
    public class UserData : Data
    {
        public static User GetUserByUsername(string username)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT Id, Username, Password from User
                             WHERE FirstName = '" + username + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = (int)reader["Id"],
                        Password = (string)reader["Password"]
                    };
                }
            }
            return user;
        }
    }
}