using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.DB;
using ShoppingCart.Models;
using ShoppingCart.filter;
namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [SessionAuthorize]
        public ActionResult Index(string productId, int? id,bool? check,int? count,string searchString)
        {
            List<Product> list = ProductData.GetProductList(searchString);
            string sid = Session["user"] as string;
            User user = UserData.GetUserDetailsbySessionId(sid);

            ViewData["ProductList"] = list;
            if (id != null)
            {
                if(productId==null || productId=="")
                    productId= id.ToString();
                else
                    productId = productId + "," + id;
            }
            if (count == null)
                count = 0;
            ViewData["count"] = count;
            ViewData["add"] = productId;
            ViewData["id"] = id.ToString();
            ViewData["search"] = searchString;
            ViewData["user"] = user;
            Session["count"] = count;
            Session["addlist"] = productId;
            return View();
        }
        [SessionAuthorize]
        public ActionResult ShoppingCart(string productId)
        {
            List<Cart> list = CartData.GetCart(productId);
            ViewData["ProductList"] = list;
            ViewData["Products"] = productId;
            //Session["count"] = count;
            Session["addlist"] = productId;
            return View();
        }
       [SessionAuthorize]
        public ActionResult CheckoutProduct(string products,string qty)
        {
            int s = (int)Session["id"];
            if (products != "")
            {
                CheckOutData.AddPurchase(products, qty,s);
            }
            Session["count"] = null;
            Session["addlist"] = null;
            return RedirectToAction("Index","Product",new { productId =""});
        }
        
    }
}