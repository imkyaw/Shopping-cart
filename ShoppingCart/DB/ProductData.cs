using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ShoppingCart.Models;
namespace ShoppingCart.DB
{
    public class ProductData
    {
        public static string c = ConfigurationManager.ConnectionStrings["ShopCart"]
                    .ConnectionString;
        public static List<Product> GetProductList(string search)
        {
            List<Product> list = new List<Product>();
            
            using (SqlConnection con=new SqlConnection(c))
            {
                con.Open();
                string s;
                if(search==null)
                    s = @"select Id as ProductId,Name as ProductName,Description as Description,Price as Price,ImgPath as Image from Product";
                else
                    s= @"select Id as ProductId,Name as ProductName,Description as Description,Price as Price,ImgPath as Image from Product where Name Like '%" + search+"%'";
                SqlCommand cmd = new SqlCommand(s,con);
                SqlDataReader r=cmd.ExecuteReader();
                while (r.Read())
                {
                    Product p = new Product
                    {
                        Id = (int)r["ProductId"],
                        Name = (string)r["ProductName"],
                        Description = (string)r["Description"],
                        Price = (float)(double)r["Price"],
                        ImgPath=(string)r["Image"]
                    };
                    list.Add(p);
                    
                }
            }
            return list;
        }
    }
}