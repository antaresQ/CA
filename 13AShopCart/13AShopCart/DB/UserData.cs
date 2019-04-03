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

                string sql = @"SELECT UserId, Username, Password from User1
                             WHERE Username = '" + username + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        UserId = (int)reader["UserId"],
                        Password = (string)reader["Password"]
                    };
                }
            }
            return user;
        }

        public static User GetUserBySessionId(string SessionId)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string q = @"SELECT User.UserId, User.Firstname, 
                    User.Lastname, FROM User,
                           AND User.SessionId = '" + SessionId + "'";

                SqlCommand cmd = new SqlCommand(q, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        UserId = (int)reader["UserId"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                    };
                }
            }

            return user;
        }
    }
}