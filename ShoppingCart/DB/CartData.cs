using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ShoppingCart.Models;

namespace ShoppingCart.DB
{
    public class CartData
    {
        public static List<Cart> GetCart(string productId)
        {
            List<Cart> list = new List<Cart>();
            using (SqlConnection con = new SqlConnection(ProductData.c))
            {
                con.Open();
                string s= @"select Id as ProductId,Name as ProductName,Description as Description,Price as Price, ImgPath as Image from Product where Id in ("+productId+")";

                if (productId != null && productId!="")
                {
                    int[] arr = productId.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                    SqlCommand cmd = new SqlCommand(s, con);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        Cart p = new Cart
                        {
                            Id = (int)r["ProductId"],
                            Name = (string)r["ProductName"],
                            Description = (string)r["Description"],
                            Price = (float)(double)r["Price"],
                            Qty = count(arr, (int)r["ProductId"]),
                            ImgPath=(string)r["Image"]
                        };
                        list.Add(p);

                    }
                }
            }
            return list;
        }
        public static int count(int[] a,int b)
        {
            int count = 0;
            for(int i = 0; i < a.Length; i++)
            {
                if (a[i] == b)
                    count++;
            }
            return count;
        }
        
    }
}