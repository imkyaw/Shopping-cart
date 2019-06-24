using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShoppingCart.DB
{
    public class CheckOutData
    {
        public static void AddPurchase(string products,string qty,int id)
        {
            int[] productArr = products.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            int[] qtyArr = qty.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            using (SqlConnection con = new SqlConnection(ProductData.c))
            {
                con.Open();
                string s = @"Insert into Purchase(ActivationCode,ProductId,DatePurchased,UserId) Values (@Activate,@Id,@Date,@UserId)";
                for (int i = 0; i < qtyArr.Length; i++)
                {
                    while (qtyArr[i] > 0)
                    {
                        string ActivationCode = Guid.NewGuid().ToString();
                       
                        SqlParameter a_param = new SqlParameter();
                        a_param.ParameterName = "@Activate";
                        a_param.Value = ActivationCode;
                        SqlParameter p_param = new SqlParameter();
                        p_param.ParameterName = "@Id";
                        p_param.Value = productArr[i];
                        SqlParameter D_param = new SqlParameter();
                        D_param.ParameterName = "@Date";
                        D_param.Value = unixTimestamp;
                        SqlParameter U_param = new SqlParameter();
                        U_param.ParameterName = "@UserId";
                        U_param.Value = id;
                        SqlCommand cmd = new SqlCommand(s, con);
                        cmd.Parameters.Add(a_param);
                        cmd.Parameters.Add(p_param);
                        cmd.Parameters.Add(D_param);
                        cmd.Parameters.Add(U_param);
                        cmd.ExecuteNonQuery();
                        qtyArr[i]--;
                    }
                }
            }
        }
    }
}