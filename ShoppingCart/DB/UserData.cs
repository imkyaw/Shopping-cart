using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using ShoppingCart.Models;
namespace ShoppingCart.DB
{
    public class UserData
    {
        public static User GetUserName(string username,string password)
        {
            User user = new User();

            using (SqlConnection conn = new SqlConnection(ProductData.c))
            {
                conn.Open();

                string c = @"Select Id,FirstName,LastName,[Password],UserName from [User]";

                SqlDataAdapter sda = new SqlDataAdapter(c, conn);
                SqlParameter param1 = new SqlParameter();
                DataSet ds = new DataSet();
                param1.ParameterName = "@UserName";
                param1.Value = username;
                sda.SelectCommand.Parameters.Add(param1);
                sda.Fill(ds, "dsUser");
                DataTable dt = ds.Tables["dsUser"];

                dt.PrimaryKey = new DataColumn[] { dt.Columns["UserName"] };

                if (dt.Rows.Find(username) != null)
                {
                    DataRow dr = dt.Rows.Find(username);
                    
                    string sessionId = SessionData.CreateSession((int)dr["Id"]);
                    if (Crypto.VerifyHashedPassword((string)dr["Password"], password))
                    {
                        user.UserName = (string)dr["UserName"];
                        user.FirstName = (string)dr["FirstName"];
                        user.Id = (int)dr["Id"];
                        user.LastName = (string)dr["LastName"];
                    }

                }
                else
                {
                    user.UserName = "Can't find User Name";
                }
            }
            return user;
        }

        public static User GetUserDetailsbySessionId(string sessionId)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection(ProductData.c))
            {
                conn.Open();
                string sqlstatement = @"SELECT Id,FirstName, LastName, UserName,Password FROM [User] WHERE sessionId = '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sqlstatement, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User();
                    user.Id = (int)reader["Id"];
                    user.FirstName = (string)reader["FirstName"];
                    user.LastName = (string)reader["LastName"];
                    user.UserName = (string)reader["UserName"];
                    user.Password = (string)reader["Password"];
                }
            }
            return user;

        }
    }
}