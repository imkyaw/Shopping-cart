using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ShoppingCart.DB
{
    public class SessionData
    {
        public static string CreateSession(int userId)
        {
            string sessionId = Guid.NewGuid().ToString();
            
            using (SqlConnection conn = new SqlConnection(ProductData.c))
            {
                conn.Open();
                string sql = @"UPDATE [User] SET SessionId = '" + sessionId + "'" +
                    "WHERE [User].Id = " + userId;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            return sessionId;
        }

        public static void RemoveSession(string sessionId)
        {
            using (SqlConnection conn = new SqlConnection(ProductData.c))
            {
                conn.Open();
                string sqlstatement = @"UPDATE [User] SET SessionId = NULL WHERE SessionId = '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sqlstatement, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}