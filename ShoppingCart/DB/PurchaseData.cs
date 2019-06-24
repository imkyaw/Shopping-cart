using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ShoppingCart.Models;
using ShoppingCart.util;
namespace ShoppingCart.DB
{
    public class PurchaseData
    {
        public static List<Purchase> GetListPurchase(int id)
        {
            List<Purchase> list = new List<Purchase>();
            List<Purchase> newlist = new List<Purchase>();
            List<int> arr = new List<int>();
            //List<long> Darr = new List<long>();
            using (SqlConnection con = new SqlConnection(ProductData.c))
            {
                con.Open();
                string s = @"select * from Purchase where UserId =" + id;
                
                SqlCommand cmd = new SqlCommand(s, con);
                SqlDataReader r = cmd.ExecuteReader();
                
                while (r.Read())
                {
                    string ActivationCode = Guid.NewGuid().ToString();
                    Purchase p = new Purchase()
                    {
                        ProductId = (int)r["ProductId"],
                        Date = (long)r["DatePurchased"],
                        ac = ActivationCode,
                        DatePurchase = TimeStamp.dateFromTimestamp((long)r["DatePurchased"]),
                    };
                    list.Add(p);
                }   
            }
          
            newlist = GetListTest(list);
            return newlist;
        }
        public static List<Purchase> GetListTest(List<Purchase> a)
        {
            List<int> arr = new List<int>();
            List<Purchase> newlist = new List<Purchase>();
            List<string> result = new List<string>();
            foreach (Purchase p1 in a)
            {
                if (!result.Contains(p1.DatePurchase))
                {
                    result.Add(p1.DatePurchase);
                }
            }
            foreach (var p in result)
            {
                int count = 0;
                foreach(Purchase d in a)
                {
                    Product getName = GetProductData(d.ProductId);
                    if (p == d.DatePurchase)
                    {
                        if (!arr.Contains(d.ProductId))
                        {
                            List<string> newCode = new List<string>();
                            newCode.Add(d.ac);
                            arr.Add(d.ProductId);
                            newlist.Add(new Purchase()
                            {
                                ProductId = d.ProductId,
                                Name = getName.Name,
                                Description = getName.Description,
                                Qty = count + 1,
                                Code = newCode,
                                Price = getName.Price,
                                Date = d.Date,
                                DatePurchase = d.DatePurchase,
                                Image = getName.ImgPath
                            });
                        }
                        else
                        {
                            foreach (var n in newlist)
                            {
                                if (n.DatePurchase == d.DatePurchase && n.ProductId == d.ProductId)
                                {
                                    n.Qty = n.Qty + 1;
                                    n.Code.Add(d.ac);
                                }
                            }
                        }
                    }
                }

            }
            return newlist;
        }
        public static Product GetProductData(int productId)
        {
            Product p = new Product();
            using (SqlConnection con = new SqlConnection(ProductData.c))
            {
                con.Open();
                string s = @"Select * from Product Where Id=" + productId;
                SqlCommand cmd = new SqlCommand(s, con);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    p.Name = (string)r["Name"];
                    p.Price = (float)(double)r["Price"];
                    p.Description = (string)r["Description"];
                    p.ImgPath = (string)r["ImgPath"];
                }       
            }
            return p;
        }
    }
}